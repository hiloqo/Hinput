using System.Collections;
using System.IO;
using UnityEngine;

namespace HinputClasses.Internal {
    // This class gathers a couple of useful variables and methods.
	public static class Utils {
		// --------------------
		// INTERNAL SETTINGS
		// --------------------

		//The maximum amount of gamepads supported by Hinput
		public const float maxGamepads = 8;

		//By how much to increase stick position (otherwise max stick distance is sometimes less than 1).
		public const float stickPositionMultiplier = 1.01f;
		
		// The location of the project's input manager file.
		public const string inputManagerPath = "./ProjectSettings/InputManager.asset";


		// --------------------
		// SETUP
		// --------------------

		// Returns true if Hinput is currently installed, false otherwise.
		public static bool HinputIsInstalled() {
			return (File.ReadAllText(inputManagerPath).Contains(HinputInputArray()));
		}

		// Locates the input array of Hinput, and returns its contents as a string. Logs an error if it is not present.
		public static string HinputInputArray () {
			try { return File.ReadAllText("./Assets/Hinput/Scripts/Setup/Hinput_8Controllers_inputManager"); } 
			catch { MissingInputArrayError(); }
			return null;
		}


		// --------------------
		// BUTTONS AND AXES
		// --------------------

		public static bool GetButton (string fullName, bool logError) {
			try { return Input.GetButton (fullName); } 
			catch { if (logError) HinputNotSetUpError (); }
			return false;
		}

		public static float GetAxis (string fullName) { return GetAxis (fullName, true); }
		public static float GetAxis (string fullName, bool logError) {
			try { return Input.GetAxisRaw (fullName); } 
			catch { if (logError) HinputNotSetUpError (); }
			return 0;
		}


		// --------------------
		// STICKS
		// --------------------

		// The dot product of a stick position by a unit vector defined by an angle.
		// (i.e. the projected distance to the origin of a stick position on the line defined by the point (0,0) and an angle.)
		public static float DotProduct (Vector2 position, float angle) {
			float radAngle = angle * Mathf.Deg2Rad;
			return Mathf.Clamp01(Mathf.Cos(radAngle)*position.x + Mathf.Sin(radAngle)*position.y);
		}

		// Returns true if stick is currently within a (Settings.stickType) degree arc oriented at angle.
		public static bool PushedTowards (this Stick stick, float angle) { 
			return (Mathf.Abs(Mathf.DeltaAngle(angle, stick.angle)) < ((int)Settings.stickType) / 2); 
		}


		// --------------------
		// COROUTINES
		// --------------------

		// A way of delegating StartCoroutine for classes that don't inherit MonoBehaviour.
		public static void Coroutine (IEnumerator coroutine) {
			Updater.instance.StartCoroutine(coroutine);
		}

		// A way of delegating StopAllCoroutines for classes that don't inherit MonoBehaviour.
		public static void StopRoutines () {
			Updater.instance.StopAllCoroutines();
		}


		// --------------------
		// OPERATING SYSTEM
		// --------------------

		//The user's operating system. Assigned when first called.
		private static string _os;
		public static string os { 
			get { 
				if (_os == null) {
					#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
						_os = "Windows";
					#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
						_os = "Mac";
					#elif UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
						_os = "Linux";
					#elif UNITY_WEBGL
						_os = "Windows";
					#else
						Debug.LogError("Hinput Error : Unknown OS !");
					#endif
				}

				return _os;
			} 
		}


		// --------------------
		// FLOAT COMPARISON
		// --------------------

		public static bool IsEqualTo (this float target, float other) {
			return Mathf.Abs(target - other) < Mathf.Epsilon;
		}

		public static bool IsNotEqualTo (this float target, float other) {
			return Mathf.Abs(target - other) > Mathf.Epsilon;
		}

		public static Vector2 Clamp(this Vector2 target, float min, float max) {
			return new Vector2(
				Mathf.Clamp(target.x, min, max),
				Mathf.Clamp(target.y, min, max));
		}


		// --------------------
		// ERROR MESSAGES
		// --------------------

		private static void HinputNotSetUpError () {
			Debug.LogWarning("Warning : Hinput has not been set up, so gamepad inputs cannot be recorded. "+
			                 "To set it up, go to the Tools menu and click \"Hinput > Set up Hinput\".");
		}

		public static void MissingInputArrayError() {
			Debug.LogError("Hinput setup error : /Assets/Hinput/Scripts/SetUp/Hinput_8Controllers_inputManager" +
			               " not found. Make sure this file is present in your project, or reinstall the package.");
		}

		public static void SetupError() {
			Debug.LogWarning("Error while setting up Hinput. Try reinstalling the plugin and rebooting your" +
			                 " computer. If the problem persists, please contact me at couvreurhenri@gmail.com.");
		}

		public static void UninstallError() {
			Debug.LogWarning("Error while uninstalling Hinput. Try reinstalling the plugin and rebooting " +
			                 "your computer. If the problem persists, please contact me at " +
			                 "couvreurhenri@gmail.com.");
		}

		public static void VibrationNotAvailableError() {
			if (os != "Windows") Debug.LogWarning("Hinput warning : vibration is only supported on Windows computers.");
			else Debug.LogWarning("Hinput warning : vibration is only supported on four controllers.");
		}
	}
}