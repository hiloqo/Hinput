using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hButton : hAbstractSimpleInput {
	public hButton (int gamepadIndex, string gamepadFullName, string inputName) {
		this._name = inputName;
		this._gamepadIndex = gamepadIndex;
		this._fullName = gamepadFullName+"_"+inputName;
	}

	public override float positionRaw { 
		get { 
			try {
				if (Input.GetButton(_fullName)) return 1;
				else return 0;
			} catch { return 0; }
		} 
	}
}