using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class hSetUp : MonoBehaviour {
	// The location of the project's input manager file.
	private static string inputManagerDir = "./ProjectSettings/inputManager.asset";

	// The location of hinput's input array.
	private static string newInputsDir = "./Assets/Scripts/hinput/Setup/InputManager";


	// Add newInputsDir at the end of inputManagerDir
	[MenuItem("hinput/Setup hinput")]
	public static void hinputSetup () {
		Debug.LogWarning("Setting up hinput... ");

		using (StreamWriter sw = File.AppendText(inputManagerDir)) {
			sw.Write(File.ReadAllText(newInputsDir));
		}

		AssetDatabase.Refresh();

		Debug.LogWarning("hinput has been set up properly. Get coding !");
	}

	// Allow to use hinputSetup only if it has not been clicked before.
	[MenuItem("hinput/Set up hinput", true)]
	public static bool hinputSetupValidation () {
		string gamepadInputs = File.ReadAllText(newInputsDir);		
		string oldGamepadInputs = File.ReadAllText(inputManagerDir);

		return (!oldGamepadInputs.Contains(gamepadInputs));
	}
}