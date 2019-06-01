using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class hSetUp : MonoBehaviour {
	// The location of the project's input manager file.
	private static string inputManagerDir = "./ProjectSettings/inputManager.asset";

	// The location of hInput's input array.
	private static string newInputsDir = "./Assets/Scripts/hInput/Setup/InputManager";


	// Add newInputsDir at the end of inputManagerDir
	[MenuItem("hInput/Set up hInput")]
	public static void hInputSetup () {
		Debug.LogWarning("Setting up hInput... ");

		using (StreamWriter sw = File.AppendText(inputManagerDir)) {
			sw.Write(File.ReadAllText(newInputsDir));
		}

		AssetDatabase.Refresh();

		Debug.LogWarning("hInput has been set up properly. Get coding !");
	}

	// Allow to use hInputSetup only if it has not been clicked before.
	[MenuItem("hInput/Set up hInput", true)]
	public static bool hInputSetupValidation () {
		string gamepadInputs = File.ReadAllText(newInputsDir);		
		string oldGamepadInputs = File.ReadAllText(inputManagerDir);

		return (!oldGamepadInputs.Contains(gamepadInputs));
	}
}