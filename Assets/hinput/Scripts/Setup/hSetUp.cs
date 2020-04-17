#if UNITY_EDITOR
using System;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Linq;

public static class hSetup {
	// The location of the project's input manager file.
	private const string inputManagerPath = "./ProjectSettings/InputManager.asset";

	// The name of hinput's input array.
	private const string hinputInputArrayName = "hinput_8Controllers_inputManager";
	
	
	// --------------------
	// SETUP
	// --------------------
	
	// Add hinput's input array at the end of inputManagerDir
	[MenuItem("Tools/hinput/Set up hinput")]
	public static void hinputSetup () {
		try {
			Debug.Log("Setting up hinput... ");

			using (StreamWriter sw = File.AppendText(inputManagerPath)) {
				sw.Write(hinputInputArray());
			}

			Confirm("set up", "You can start coding !");
		} catch (Exception e) {
			Debug.LogWarning("Error while setting up hinput. Try reinstalling the plugin and rebooting your" +
			                 " computer. If the problem persists, please contact me at couvreurhenri@gmail.com.");
			throw e;
		}
	}

	// Allows to set up hinput only if it is not installed.
	[MenuItem("Tools/hinput/Set up hinput", true)]
	public static bool hinputSetupValidation () {
		return !hinputIsInstalled();
	}
	
	
	// --------------------
	// UNINSTALL
	// --------------------
	
	// Remove hinput's input array from the end of inputManagerDir
	[MenuItem("Tools/hinput/Uninstall hinput")]
	public static void hinputUninstall () {
		try {
			Debug.Log("Uninstalling hinput... ");

			string hinputInputArray = hSetup.hinputInputArray();
			string currentInputArray = File.ReadAllText(inputManagerPath);

			File.WriteAllText(inputManagerPath, currentInputArray.Replace(hinputInputArray, ""));

			Confirm("uninstalled", "Bye bye !");
		} catch (Exception e) {
			Debug.LogWarning("Error while uninstalling hinput. Try reinstalling the plugin and rebooting your" +
			                 " computer. If the problem persists, please contact me at couvreurhenri@gmail.com.");
			throw e;
		}
	}

	// Allows to uninstall hinput only if it is installed.
	[MenuItem("Tools/hinput/Uninstall hinput", true)]
	public static bool hinputUninstallValidation () {
		return hinputIsInstalled();
	}
	
	
	// --------------------
	// UTILS
	// --------------------

	private static void Confirm(string action, string catchphrase) {
		AssetDatabase.Refresh();
		try {
			File.Delete("./Library/SourceAssetDB");
			Debug.Log("hinput has been "+action+" successfully. "+catchphrase);
		} catch (IOException) {
			Debug.LogWarning("[ACTION NEEDED] You need to reimport your asset database to confirm the " + 
			                 action.Replace(" ", "") + " of hinput. Please click \"Reimport all\"" +
			                 " in the Assets menu.");
		}
	}

	// Returns true if hinput is currently installed, false otherwise.
	public static bool hinputIsInstalled() {
		string hinputInputArray = hSetup.hinputInputArray();		
		string currentInputArray = File.ReadAllText(inputManagerPath);

		return (currentInputArray.Contains(hinputInputArray));
	}

	// Returns the path to hinput's input array. Logs an error if it is not present.
	private static string hinputInputArray () {
		string filePath = PathToInputArray ("./Assets/hinput/Scripts/Setup");
		
		if (filePath == null) filePath = PathToInputArray ("./Assets/hinput/Scripts");
		if (filePath == null) filePath = PathToInputArray ("./Assets/hinput");
		if (filePath == null) filePath = PathToInputArray ("./Assets");
		if (filePath == null) filePath = PathToInputArray (".");

		try {
			return File.ReadAllText(filePath);
		} catch {
			Debug.LogError("hinput setup error : /Assets/hinput/Scripts/Setup/hinput_8Controllers_inputManager" +
				" not found. Make sure this file is present in your project, or reinstall the package.");
		}

		return null;
	}

	// Returns hinput's input array in argument directory if it present, null otherwise.
	private static string PathToInputArray (string directory) {
		try {
			return Directory.GetFiles(directory, hinputInputArrayName, SearchOption.AllDirectories).FirstOrDefault();
		} catch { 
			return null;
		}
	}
}
#endif