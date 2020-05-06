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
		[MenuItem("Tools/Hinput/Set Up Hinput")]
		public static void HinputSetup () {
			try {
				using (StreamWriter sw = File.AppendText(inputManagerPath)) sw.Write(HinputInputArray());
				Confirm("set up", "You can start coding !");
			} catch (Exception e) {
				Debug.LogWarning("Error while setting up Hinput. Try reinstalling the plugin and rebooting your" +
				                 " computer. If the problem persists, please contact me at couvreurhenri@gmail.com.");
				throw e;
			}
		}

		// Allows to set up Hinput only if it is not installed.
		[MenuItem("Tools/Hinput/Set Up Hinput", true)]
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
				File.WriteAllText(inputManagerPath, 
					File.ReadAllText(inputManagerPath).Replace(HinputInputArray(), ""));
				Confirm("uninstalled", "Bye bye !");
			} catch (Exception e) {
				Debug.LogWarning("Error while uninstalling Hinput. Try reinstalling the plugin and rebooting " +
				                 "your computer. If the problem persists, please contact me at " +
				                 "couvreurhenri@gmail.com.");
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
			return (File.ReadAllText(inputManagerPath).Contains(HinputInputArray()));
		}

		// Locates the input array of Hinput, and returns its contents as a string. Logs an error if it is not present.
		private static string HinputInputArray () {
			string filePath;
			try {
				filePath = Directory.GetFiles("./Assets/Hinput/Scripts/SetUp", hinputInputArrayName)
					.FirstOrDefault();
			} 
			catch { filePath = null; }

			try { return File.ReadAllText(filePath); } 
			catch {
				Debug.LogError("Hinput setup error : /Assets/Hinput/Scripts/SetUp/Hinput_8Controllers_inputManager" +
					" not found. Make sure this file is present in your project, or reinstall the package.");
			}

			return null;
		}
	}
}
#endif