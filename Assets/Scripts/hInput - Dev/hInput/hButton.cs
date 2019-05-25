using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hButton : hAbstractPressableButNotADirection {
	public hButton (string name, hGamepad gamepad) {
		this._name = name;
		this._gamepadIndex = gamepad.index;
		this._fullName = gamepad.fullName+"_"+name;
	}
	
	public override float positionRaw { 
		get { 
			try {
				if (Input.GetButton(fullName)) return 1;
			} catch { } // Dont care if error here
			return 0;
		} 
	}
}