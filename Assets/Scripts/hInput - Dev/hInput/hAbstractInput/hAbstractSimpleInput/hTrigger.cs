using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hTrigger : hAbstractSimpleInput {
	public hTrigger (int gamepadIndex, string gamepadFullName, string inputName) {
		this._name = inputName;
		this._gamepadIndex = gamepadIndex;
		this._fullName = gamepadFullName+"_"+inputName;

		initialValue = measuredPosition;
	}

	private float initialValue;
	private bool hasBeenMoved;

	protected override void UpdatePositionRaw () {
		if (hasBeenMoved || measuredPosition != initialValue) {
			hasBeenMoved = true;
			_positionRaw = measuredPosition;
		}
	}

	private float measuredPosition { 
		get {
			if (hInput.os == "Windows") return Input.GetAxisRaw(_fullName);
			return (Input.GetAxisRaw(_fullName) + 1)/2;	
		}
	}
}