using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			if (Input.GetButton(fullName)) _positionRaw = 1;
			else _positionRaw = 0;
		} catch {
			_positionRaw = 0;
		}
	}


	// --------------------
	// PROPERTIES
	// --------------------
	
	public override float position { get { return positionRaw; } }

	public override bool pressed { get { return position == 1; } }

	public override bool inDeadZone { get { return !pressed; } }
}