using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class gathers a couple of useful variable and methods.
public static class hUtils {
	// --------------------
	// INTERNAL SETTINGS
	// --------------------

	//The maximum amount of gamepads supported by hinput
	public static float maxGamepads { get { return 8; } }

	//By how much to increase diagonals (in %), because otherwise the max stick distance is sometimes less than 1.
	//Does not affect raw inputs.
	public static float distanceIncrease { get { return 0.01f; } }

	//By how much to increase deltaTime (in %) when comparing it, to account for rounding errors.
	public static float deltaTimeEpsilon { get { return 0.1f; } }


	// --------------------
	// BUTTONS AND AXES
	// --------------------

	public static bool GetButton (string fullName) { return GetButton (fullName, true); }
	public static bool GetButton (string fullName, bool logError) {
		try {
			return Input.GetButton (fullName);
		} catch {
			if (logError) hinputNotSetUpError ();
			return false;
		}
	}

	public static float GetAxis (string fullName) { return GetAxis (fullName, true); }
	public static float GetAxis (string fullName, bool logError) {
		try {
			return Input.GetAxisRaw (fullName);
		} catch {
			if (logError) hinputNotSetUpError ();
			return 0;
		}
	}

	private static void hinputNotSetUpError () {
		Debug.LogWarning("Warning : hinput has not been set up, so gamepad inputs cannot be recorded. "+
		"To set it up, go to the Tools menu and click \"hinput > Setup hinput\".");
	}


	// --------------------
	// STICKS
	// --------------------

	// Returns the dot product of a stick position by a unit vector defined by an angle.
	// (i.e. the projected distance to the origin of a stick position on the line defined by the point (0,0) and an angle.)
	public static float DotProduct (Vector2 position, float angle) {
		float radStickAngle = angle * Mathf.Deg2Rad;
		float sin = Mathf.Sin(radStickAngle);
		float cos = Mathf.Cos(radStickAngle);
		return Mathf.Clamp01(cos*position.x + sin*position.y);
	}

	// True if the stick is currently within a (hSettings.directionAngle) degree cone from this direction
	public static bool StickWithinAngle (hStick stick, float angle) { 
		float distanceToAngle = Mathf.Abs(Mathf.DeltaAngle(angle, stick.angle));
		float maxDistance = hSettings.directionAngle/2;
		return (distanceToAngle <= maxDistance); 
	}


	// --------------------
	// VIBRATION
	// --------------------

	// A way of delegating StartCoroutine for classes that don't inherit MonoBehaviour.
	public static void Coroutine (IEnumerator coroutine) {
		hSettings.instance.StartCoroutine(coroutine);
	}

	// A way of delegating StopAllCoroutines for classes that don't inherit MonoBehaviour.
	public static void StopRoutines () {
		hSettings.instance.StopAllCoroutines();
	}


	// --------------------
	// TIME
	// --------------------

	//The time it was last time the game was updated
	public static float lastUpdated;

	//The duration it took to process the previous frame
	private static float deltaTime;

	public static void UpdateTime () {
		float currentTime = Time.time;
		deltaTime = currentTime - lastUpdated;
		lastUpdated = currentTime;
	}

	//The previous frame was processed in less than this duration.
	public static float maxDeltaTime { get { return (deltaTime)*(1 + deltaTimeEpsilon); } }

	public static bool isUpToDate { get { return lastUpdated == Time.time; } }


	// --------------------
	// OPERATING SYSTEM
	// --------------------

	//The user's operating system. Assigned when first called.
	private static string _os;
	public static string os { 
		get { 
			if (_os == null) {
				#if UNITY_EDITOR_WIN
					_os = "Windows";
				#elif UNITY_STANDALONE_WIN
					_os = "Windows";
				#elif UNITY_EDITOR_OSX
					_os = "Mac";
				#elif UNITY_STANDALONE_OSX
					_os = "Mac";
				#elif UNITY_EDITOR_LINUX
					_os = "Linux";
				#elif UNITY_STANDALONE_LINUX
					_os = "Linux";
				#else
					Debug.LogError("hinput Error : Unknown OS !");
				#endif
			}

			return _os;
		} 
	}
}