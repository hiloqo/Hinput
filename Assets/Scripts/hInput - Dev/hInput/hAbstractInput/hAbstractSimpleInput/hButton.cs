using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hButton : hAbstractSimpleInput {
	public hButton (int gamepadIndex, string gamepadFullName, string inputName) {
		this._name = inputName;
		this._gamepadIndex = gamepadIndex;
		this._fullName = gamepadFullName+"_"+inputName;
	}

	protected override void UpdatePositionRaw () {
		try {
			if (Input.GetButton(_fullName)) _positionRaw = 1;
			else _positionRaw = 0;
		} catch { _positionRaw = 0; }
	}
}