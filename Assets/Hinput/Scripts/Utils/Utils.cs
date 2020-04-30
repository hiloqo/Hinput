﻿using System.Collections;
using UnityEngine;

namespace HinputClasses.Internal {
    // This class gathers a couple of useful variable and methods.
	public static class Utils {
		// --------------------
		// INTERNAL SETTINGS
		// --------------------

		//The maximum amount of gamepads supported by Hinput
		public const float maxGamepads = 8;

		//By how much to increase diagonals (in %), because otherwise the max stick distance is sometimes less than 1.
		//Does not affect raw inputs.
		public const float distanceIncrease = 0.01f;


		// --------------------
		// BUTTONS AND AXES
		// --------------------

		public static bool GetButton (string fullName, bool logError) {
			try {
				return Input.GetButton (fullName);
			} catch {
				if (logError) HinputNotSetUpError ();
				return false;
			}
		}

		public static float GetAxis (string fullName) { return GetAxis (fullName, true); }
		public static float GetAxis (string fullName, bool logError) {
			try {
				return Input.GetAxisRaw (fullName);
			} catch {
				if (logError) HinputNotSetUpError ();
				return 0;
			}
		}

		private static void HinputNotSetUpError () {
			Debug.LogWarning("Warning : Hinput has not been set up, so gamepad inputs cannot be recorded. "+
			"To set it up, go to the Tools menu and click \"Hinput > Set up Hinput\".");
		}


		// --------------------
		// STICKS
		// --------------------

		// The dot product of a stick position by a unit vector defined by an angle.
		// (i.e. the projected distance to the origin of a stick position on the line defined by the point (0,0) and an angle.)
		public static float DotProduct (Vector2 position, float angle) {
			float radStickAngle = angle * Mathf.Deg2Rad;
			float sin = Mathf.Sin(radStickAngle);
			float cos = Mathf.Cos(radStickAngle);
			return Mathf.Clamp01(cos*position.x + sin*position.y);
		}

		// Returns true if the stick is currently within a (Settings.directionAngle) degree cone from this direction
		public static bool StickWithinAngle (Stick stick, float angle) { 
			float distanceToAngle = Mathf.Abs(Mathf.DeltaAngle(angle, stick.angle));
			int maxDistance = (int)Settings.stickType;
			return (distanceToAngle <= maxDistance); 
		}


		// --------------------
		// VIBRATION
		// --------------------

		// A way of delegating StartCoroutine for classes that don't inherit MonoBehaviour.
		public static void Coroutine (IEnumerator coroutine) {
			Settings.instance.StartCoroutine(coroutine);
		}

		// A way of delegating StopAllCoroutines for classes that don't inherit MonoBehaviour.
		public static void StopRoutines () {
			Settings.instance.StopAllCoroutines();
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

		public static bool IsSuperiorOrEqualTo(this float target, float other) {
			return target - other > -Mathf.Epsilon;
		}
	}
}