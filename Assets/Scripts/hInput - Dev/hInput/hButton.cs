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
	// PROPERTIES
	// --------------------
	
	public override float positionRaw { 
		get { 
			try {
				if (Input.GetButton(fullName)) return 1;
			} catch { } // Dont care if error here
			return 0;
		} 
	}
	
	public override float position { get { return positionRaw; } }

	public override bool pressed { get { return position == 1; } }

	public override bool inDeadZone { get { return !pressed; } }
}