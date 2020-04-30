#if UNITY_EDITOR
    
using System;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Linq;

namespace HinputClasses.Internal {
	// Hinput class responsible for setting up and uninstalling the plugin.
	public static class Setup {
		// The location of the project's input manager file.
		private const string inputManagerPath = "./ProjectSettings/InputManager.asset";

		// The name of Hinput's input array.
		private const string hinputInputArrayName = "Hinput_8Controllers_inputManager";
		
		
		// --------------------
		// SETUP
		// --------------------
		
		// Add Hinput's input array at the end of inputManagerDir
		[MenuItem("Tools/Hinput/Set up Hinput")]
		public static void HinputSetup () {
			try {
				Debug.Log("Setting up Hinput... ");

				using (StreamWriter sw = File.AppendText(inputManagerPath)) {
					sw.Write(HinputInputArray());
				}

				Confirm("set up", "You can start coding !");
			} catch (Exception e) {
				Debug.LogWarning("Error while setting up Hinput. Try reinstalling the plugin and rebooting your" +
				                 " computer. If the problem persists, please contact me at couvreurhenri@gmail.com.");
				throw e;
			}
		}

		// Allows to set up Hinput only if it is not installed.
		[MenuItem("Tools/Hinput/Set up Hinput", true)]
		public static bool HinputSetupValidation () {
			return !HinputIsInstalled();
		}
		
		
		// --------------------
		// UNINSTALL
		// --------------------
		
		// Remove Hinput's input array from the end of inputManagerDir
		[MenuItem("Tools/Hinput/Uninstall Hinput")]
		public static void HinputUninstall () {
			try {
				Debug.Log("Uninstalling Hinput... ");

				string hinputInputArray = Setup.HinputInputArray();
				string currentInputArray = File.ReadAllText(inputManagerPath);

				File.WriteAllText(inputManagerPath, currentInputArray.Replace(hinputInputArray, ""));

				Confirm("uninstalled", "Bye bye !");
			} catch (Exception e) {
				Debug.LogWarning("Error while uninstalling Hinput. Try reinstalling the plugin and rebooting " +
				                 "your computer. If the problem persists, please contact me at couvreurhenri@gmail.com.");
				throw e;
			}
		}

		// Allows to uninstall Hinput only if it is installed.
		[MenuItem("Tools/Hinput/Uninstall Hinput", true)]
		public static bool HinputUninstallValidation () {
			return HinputIsInstalled();
		}
		
		
		// --------------------
		// UTILS
		// --------------------

		private static void Confirm(string action, string catchphrase) {
			AssetDatabase.Refresh();
			try {
				File.Delete("./Library/SourceAssetDB");
				Debug.Log("Hinput has been "+action+" successfully. "+catchphrase);
			} catch (IOException) {
				Debug.LogWarning("[ACTION NEEDED] You need to reimport your asset database to confirm the " + 
				                 action.Replace(" ", "").Replace("ed","") + 
				                 " of Hinput. Please click \"Reimport all\" in the Assets menu.");
			}
		}

		// Returns true if Hinput is currently installed, false otherwise.
		public static bool HinputIsInstalled() {
			string hinputInputArray = Setup.HinputInputArray();		
			string currentInputArray = File.ReadAllText(inputManagerPath);

			return (currentInputArray.Contains(hinputInputArray));
		}

		// The path to Hinput's input array. Logs an error if it is not present.
		private static string HinputInputArray () {
			string filePath = PathToInputArray ("./Assets/Hinput/Scripts/SetUp");
			
			if (filePath == null) filePath = PathToInputArray ("./Assets/Hinput/Scripts");
			if (filePath == null) filePath = PathToInputArray ("./Assets/Hinput");
			if (filePath == null) filePath = PathToInputArray ("./Assets");
			if (filePath == null) filePath = PathToInputArray (".");

			try {
				return File.ReadAllText(filePath);
			} catch {
				Debug.LogError("Hinput setup error : /Assets/Hinput/Scripts/SetUp/Hinput_8Controllers_inputManager" +
					" not found. Make sure this file is present in your project, or reinstall the package.");
			}

			return null;
		}

		// Returns Hinput's input array in argument directory if it present, null otherwise.
		private static string PathToInputArray (string directory) {
			try {
				return Directory.GetFiles(directory, hinputInputArrayName, SearchOption.AllDirectories).FirstOrDefault();
			} catch { 
				return null;
			}
		}
	}
}
#endif