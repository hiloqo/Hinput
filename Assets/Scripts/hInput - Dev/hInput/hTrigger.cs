using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hTrigger : hAbstractPressableButNotADirection {
	private float initialValue;
	private bool hasBeenMoved;

	public hTrigger (string name, hGamepad gamepad) {
		this._name = name;
		this._gamepadIndex = gamepad.index;
		this._fullName = gamepad.fullName+"_"+name;

		initialValue = measuredPosition;
	}

	private float measuredPosition { 
		get {
			if (hInput.os == "Windows") return Input.GetAxisRaw(fullName);
			return (Input.GetAxisRaw(fullName) + 1)/2;	
		}
	}

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
}