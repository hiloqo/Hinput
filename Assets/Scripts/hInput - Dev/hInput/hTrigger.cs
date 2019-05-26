﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hTrigger : hAbstractPressable {
	// --------------------
	// CONSTRUCTOR
	// --------------------

	public hTrigger (string name, hGamepad gamepad) {
		this._name = name;
		this._gamepadIndex = gamepad.index;
		this._fullName = gamepad.fullName+"_"+name;

		initialValue = measuredPosition;
	}


	// --------------------
	// INITIAL VALUE
	// --------------------
	
	private float initialValue;
	private bool hasBeenMoved;

	private float measuredPosition { 
		get {
			if (hInput.os == "Windows") return Input.GetAxisRaw(fullName);
			return (Input.GetAxisRaw(fullName) + 1)/2;	
		}
	}


	// --------------------
	// PROPERTIES
	// --------------------

	public override float positionRaw { 
		get { 
			if (hasBeenMoved) {
				return measuredPosition;
			} else if (measuredPosition != initialValue) {
				hasBeenMoved = true;
				return measuredPosition;
			}
			else return 0f;
		}
	}

	public override float position { 
		get { 
			float posRaw = positionRaw;

			if (posRaw < hInput.deadZone) return 0f;
			else return ((posRaw - hInput.deadZone)/(1 - hInput.deadZone));
		} 
	}

	public override bool pressed { get { return position >= hInput.triggerZone; } }

	public override bool inDeadZone { get { return position < hInput.deadZone; } }
}