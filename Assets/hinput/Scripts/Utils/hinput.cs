// Author : Henri Couvreur for hiloqo, 2019
// Contact : couvreurhenri@gmail.com, hiloqo.games@gmail.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The main class of the hinput package, from which you can access gamepads.
/// </summary>
public static class hinput {
	// --------------------
	// GAMEPADS
	// --------------------

	private static hGamepad _anyGamepad;
	/// <summary>
	/// A virtual gamepad that returns the biggest absolute value for each control of all connected gamepads.
	/// </summary>
	public static hGamepad anyGamepad { 
		get { 
			if (_anyGamepad == null) {
				_anyGamepad = new hGamepad(hUtils.os, -1);
			} else {
				hUpdater.instance.UpdateGamepads ();
			}

			return _anyGamepad; 
		}
	}

	private static List<hGamepad> _gamepad;
	/// <summary>
	/// An array of 8 gamepads, labelled 0 to 7.
	/// </summary>
	public static List<hGamepad> gamepad { 
		get {
			if (_gamepad == null) {
				_gamepad = new List<hGamepad>();
				for (int i=0; i<hUtils.maxGamepads; i++) gamepad.Add(new hGamepad(hUtils.os, i));
			} else {
				hUpdater.instance.UpdateGamepads ();
			} 

			return _gamepad; 
		} 
	}
}