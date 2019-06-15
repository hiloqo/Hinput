using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Linq;

public class hSetup {
	// The location of the project's input manager file.
	private static string inputManagerDir = "./ProjectSettings/inputManager.asset";

	// The name of hinput's input array.
	private static string fileName = "hinput_8Controllers_inputManager";


	// Add newInputsDir at the end of inputManagerDir
	[MenuItem("Tools/hinput/Setup hinput")]
	public static void hinputSetup () {
		Debug.LogWarning("Setting up hinput... ");

		using (StreamWriter sw = File.AppendText(inputManagerDir)) {
			sw.Write(GetInputs());
		}

		AssetDatabase.Refresh();

		Debug.LogWarning("hinput has been set up properly. You can start coding !");
	}

	// Allow to use hinputSetup only if it has not been clicked before.
	[MenuItem("Tools/hinput/Setup hinput", true)]
	public static bool hinputSetupValidation () {
		string gamepadInputs = GetInputs();		
		string oldGamepadInputs = File.ReadAllText(inputManagerDir);

		return (!oldGamepadInputs.Contains(gamepadInputs));
	}

	private static string GetInputs () {
		string filePath;
		filePath = FindFromDirectory ("./Assets/hinput/Scripts/Setup");
		if (filePath == null) filePath = FindFromDirectory ("./Assets/hinput/Scripts");
		if (filePath == null) filePath = FindFromDirectory ("./Assets/hinput");
		if (filePath == null) filePath = FindFromDirectory ("./Assets");
		if (filePath == null) filePath = FindFromDirectory (".");

		try {
			return File.ReadAllText(filePath);
		} catch {
			Debug.Log("hinput setup error : hinput_8Controllers_inputManager not found. "+
			"Make sure this file is present in your project, or reinstall the package.");
		}

		return null;
	}

	private static string FindFromDirectory (string directory) {
		try {
			return Directory.GetFiles(directory, fileName, SearchOption.AllDirectories).FirstOrDefault();
		} catch { } // Ignore errors here.

		return null;
	}

}