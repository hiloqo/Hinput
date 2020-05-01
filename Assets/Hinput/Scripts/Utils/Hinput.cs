// Author : Henri Couvreur for hiloqo, 2019
// Contact : couvreurhenri@gmail.com, hiloqo.games@gmail.com

using System.Collections.Generic;
using System.Linq;
using HinputClasses;
using HinputClasses.Internal;
using UnityEngine;

/// <summary>
/// The main class of the Hinput package, from which you can access gamepads.
/// </summary>
public static class Hinput {
	// --------------------
	// GAMEPAD
	// --------------------

	private static List<Gamepad> _gamepad;
	/// <summary>
	/// A list of 8 gamepads, labelled 0 to 7.
	/// </summary>
	public static List<Gamepad> gamepad { 
		get {
			Updater.CheckInstance();
			if (_gamepad == null) {
				_gamepad = new List<Gamepad>();
				for (int i=0; i<Utils.maxGamepads; i++) _gamepad.Add(new Gamepad(i));
			} else {
				Updater.UpdateGamepads ();
			} 

			return _gamepad; 
		} 
	}
	
	
	// --------------------
	// ANYGAMEPAD
	// --------------------

	private static AnyGamepad _anyGamepad;
	/// <summary>
	/// A virtual gamepad that returns the inputs of every gamepad at once.<BR/> <BR/>
	///
	/// The position of a button on AnyGamepad is the highest position for that button on all gamepads.<BR/>
	/// The position of a stick on AnyGamepad is the average position for that stick on all gamepads.<BR/>
	/// Vibrating AnyGamepad vibrates all gamepads.<BR/> <BR/>
	/// 
	/// - If player 1 pushed their A button and player 2 pushed their B button, both the A and the B button of
	/// anyGamepad will be pressed.<BR/>
	/// - If player 1 pushed their left trigger by 0.24 and player 2 pushed theirs by 0.46, the left trigger of
	/// anyGamepad will have a position of 0.46.<BR/>
	/// - If player 1 positioned their right stick at (-0.21, 0.88) and player 2 has theirs at (0.67, 0.26), the
	/// position of the right stick of anyGamepad will be the average of both positions, (0.23, 0.57).
	/// </summary>
	public static AnyGamepad anyGamepad { 
		get { 
			Updater.CheckInstance();
			if (_anyGamepad == null) {
				_anyGamepad = new AnyGamepad();
			} else {
				Updater.UpdateGamepads ();
			}

			return _anyGamepad; 
		}
	}


	// --------------------
	// ANYINPUT
	// --------------------
	
	/// <summary>
	/// A virtual button that returns every input of every gamepad at once.
	/// </summary>
	public static Pressable anyInput { get { return anyGamepad.anyInput; } }
	
	
	// --------------------
	// ACTIVE GAMEPADS
	// --------------------

	private static List<Gamepad> _activeGamepads;
	private static int _lastActiveGamepadsUpdateFrame = -1;
	/// <summary>
	/// The list of all gamepads on which at least one button is currently being pressed.
	/// </summary>
	public static List<Gamepad> activeGamepads {
		get {
			if (_lastActiveGamepadsUpdateFrame == Time.frameCount) return _activeGamepads;
                
			_activeGamepads = gamepad.Where(g => g.anyInput.simplePress.justPressed).ToList();
			_lastActiveGamepadsUpdateFrame = Time.frameCount;
			return _activeGamepads;
		}
	}
}