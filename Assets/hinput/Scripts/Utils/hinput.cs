// Author : Henri Couvreur for hiloqo, 2019
// Contact : couvreurhenri@gmail.com, hiloqo.games@gmail.com

using System.Collections.Generic;

/// <summary>
/// The main class of the hinput package, from which you can access gamepads.
/// </summary>
public static class hinput {
	// --------------------
	// GAMEPADS
	// --------------------

	private static hGamepad _anyGamepad;
	/// <summary>
	/// A virtual gamepad that returns the inputs of every gamepad at once.
	/// </summary>
	/// <remarks>
	/// This gamepad returns the biggest absolute value for each input (and each axis in the case of hSticks).
	/// </remarks>
	/// <example>
	/// - If player 1 pushed their A button and player 2 pushed their B button,
	/// both the A and the B button of anyGamepad will be pressed.
	/// - If player 1 pushed their left trigger by 0.24 and player 2 pushed theirs by 0.46,
	/// the left trigger of anyGamepad will have a position of 0.46.
	/// - If player 1 positioned their right stick at (-0.21, -0.78) and player 2 has theirs at (0.47, 0.55),
	/// the right stick of anyGamepad will have a position of (0.47, -0.78).
	/// </example>
	public static hGamepad anyGamepad { 
		get { 
			if (_anyGamepad == null) {
				_anyGamepad = new hGamepad(-1);
			} else {
				hUpdater.UpdateGamepads ();
			}

			return _anyGamepad; 
		}
	}

	private static List<hGamepad> _gamepad;
	/// <summary>
	/// An array of 8 gamepads, labelled 0 to 7.
	/// </summary>
	/// <remarks>
	/// Gamepad disconnects are handled by the driver, and as such will yield different results depending on your operating system.
	/// </remarks>
	public static List<hGamepad> gamepad { 
		get {
			if (_gamepad == null) {
				_gamepad = new List<hGamepad>();
				for (int i=0; i<hUtils.maxGamepads; i++) _gamepad.Add(new hGamepad(i));
			} else {
				hUpdater.UpdateGamepads ();
			} 

			return _gamepad; 
		} 
	}
}