using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class hSetUp : MonoBehaviour {
	[MenuItem("hInput/Set up hInput")]
	public static void hInputSetup () {
		string gamepadInputs = "./Assets/Scripts/hInput/Setup/InputManager";
		string gamepadInputsMeta = gamepadInputs+" .meta";
		if (File.Exists(gamepadInputs)) {
			using (StreamWriter sw = File.AppendText("./ProjectSettings/inputManager.asset")) {
				sw.Write(File.ReadAllText(gamepadInputs));
				File.Delete(gamepadInputs);
				File.Delete(gamepadInputsMeta);
			}
		}
	}

	[MenuItem("hInput/Set up hInput", true)]
	public static bool hInputSetupValidation () {
		//Return true if the operation is allowed.
		return true;
	}
}
