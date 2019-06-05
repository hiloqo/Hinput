using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// hinput class representing a physical button of the controller, such as the A button, the bumpers or the stick clicks.
/// </summary>
public class hButton : hAbstractPressable {
	// --------------------
	// CONSTRUCTOR
	// --------------------
	
	public hButton (string name, hGamepad gamepad) {
		this._name = name;
		this._gamepadIndex = gamepad.index;
		this._fullName = gamepad.fullName+"_"+name;
	}

	
	// --------------------
	// UPDATE
	// --------------------

	protected override void UpdatePositionRaw() {
		try {
			if (hUtils.GetButton(fullName, (name !="XBoxButton"))) _positionRaw = 1;
			else _positionRaw = 0;
		} catch {
			_positionRaw = 0;
		}
	}


	// --------------------
	// PROPERTIES
	// --------------------
	
	/// <summary>
	/// Returns 1 if the button is currently pressed. Returns 0 otherwise.
	/// </summary>
	public override float position { get { return positionRaw; } }

	/// <summary>
	/// Returns true if the button is currently pressed. Returns false otherwise.
	/// </summary>
	public override bool pressed { get { return position == 1; } }

	/// <summary>
	/// Returns true if the button is not (pressed). Returns false otherwise.
	/// </summary>
	public override bool inDeadZone { get { return !pressed; } }
}