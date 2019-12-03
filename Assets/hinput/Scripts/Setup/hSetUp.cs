#if UNITY_EDITOR
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Linq;

public static class hSetup {
	// The location of the project's input manager file.
	private const string inputManagerPath = "./ProjectSettings/inputManager.asset";

	// The name of hinput's input array.
	private const string hinputInputArrayName = "hinput_8Controllers_inputManager";
	
	
	// --------------------
	// SETUP
	// --------------------
	
	// Add hinput's input array at the end of inputManagerDir
	[MenuItem("Tools/hinput/Setup hinput")]
	public static void hinputSetup () {
		Debug.Log("Setting up hinput... ");

		using (StreamWriter sw = File.AppendText(inputManagerPath)) {
			sw.Write(hinputInputArray());
		}

		AssetDatabase.Refresh();
		Debug.Log("hinput has been set up properly. You can start coding !");
	}

	// Allows to set up hinput only if it is not installed.
	[MenuItem("Tools/hinput/Setup hinput", true)]
	public static bool hinputSetupValidation () {
		return !hinputIsInstalled();
	}
	
	
	// --------------------
	// UNINSTALL
	// --------------------
	
	// Remove hinput's input array from the end of inputManagerDir
	[MenuItem("Tools/hinput/Uninstall hinput")]
	public static void hinputUninstall () {
		Debug.Log("Uninstalling hinput... ");

		string hinputInputArray = hSetup.hinputInputArray();
		string currentInputArray = File.ReadAllText(inputManagerPath);
		
		File.WriteAllText(inputManagerPath, currentInputArray.Replace(hinputInputArray, ""));

		AssetDatabase.Refresh();
		Debug.Log("hinput has been uninstalled properly. Bye bye !");
	}

	// Allows to uninstall hinput only if it is installed.
	[MenuItem("Tools/hinput/Uninstall hinput", true)]
	public static bool hinputUninstallValidation () {
		return hinputIsInstalled();
	}
	
	
	// --------------------
	// UTILS
	// --------------------

	// Returns true if hinput is currently installed, false otherwise.
	private static bool hinputIsInstalled() {
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

	// Returns hipnut's input array in argument directory if it present, null otherwise.
	private static string PathToInputArray (string directory) {
		try {
			return Directory.GetFiles(directory, hinputInputArrayName, SearchOption.AllDirectories).FirstOrDefault();
		} catch { 
			return null;
		}
	}
}
#endif