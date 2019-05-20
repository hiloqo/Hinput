using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hButton : hAbstractSimpleInput {
	public hButton (string gamepadName, string inputName) {
		this._name = inputName;
		this._fullName = gamepadName+"_"+inputName;
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