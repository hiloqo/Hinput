using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Linq;

public class hSetUp {
	// The location of the project's input manager file.
	private static string inputManagerDir = "./ProjectSettings/inputManager.asset";

	// The name of hinput's input array.
	private static string fileName = "hinput_8controllers_inputManager";


	// Add newInputsDir at the end of inputManagerDir
	[MenuItem("hinput/Setup hinput")]
	public static void hinputSetup () {
		Debug.LogWarning("Setting up hinput... ");

		using (StreamWriter sw = File.AppendText(inputManagerDir)) {
			sw.Write(File.ReadAllText(GetFilePath()));
		}

		AssetDatabase.Refresh();

		Debug.LogWarning("hinput has been set up properly. Get coding !");
	}

	// Allow to use hinputSetup only if it has not been clicked before.
	[MenuItem("hinput/Setup hinput", true)]
	public static bool hinputSetupValidation () {
		string gamepadInputs = File.ReadAllText(GetFilePath());		
		string oldGamepadInputs = File.ReadAllText(inputManagerDir);

		return (!oldGamepadInputs.Contains(gamepadInputs));
	}

	private static string GetFilePath () {
		string filePath;
		filePath = FindFromDirectory ("./Assets/hinput/Setup");
		if (filePath == null) filePath = FindFromDirectory ("./Assets/hinput");
		if (filePath == null) filePath = FindFromDirectory ("./Assets");
		if (filePath == null) filePath = FindFromDirectory (".");
		if (filePath == null) Debug.Log("hinput error : Assets/hinput/Setup/hinput_8controllers_inputManager not found. "+
		"Make sure this file is present in your project, or reinstall the package.");

		return filePath;
	}

	private static string FindFromDirectory (string directory) {
		return Directory.GetFiles(directory, fileName, SearchOption.AllDirectories).FirstOrDefault();
	}

	
}