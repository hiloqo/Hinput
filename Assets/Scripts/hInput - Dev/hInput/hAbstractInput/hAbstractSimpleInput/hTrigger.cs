using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hTrigger : hAbstractSimpleInput {
	public hTrigger (int gamepadIndex, string gamepadFullName, string inputName) {
		this._name = inputName;
		this._gamepadIndex = gamepadIndex;
		this._fullName = gamepadFullName+"_"+inputName;
	}

	public override float positionRaw { 
		get { 
			if (hInput.os == "Windows") return Input.GetAxisRaw(_fullName);
			return (Input.GetAxisRaw(_fullName) + 1)/2;			
		} 
	}
}