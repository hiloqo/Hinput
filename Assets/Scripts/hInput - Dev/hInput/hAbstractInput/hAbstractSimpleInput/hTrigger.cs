using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hTrigger : hAbstractSimpleInput {
	public hTrigger (string gamepadName, string inputName) {
		this._name = inputName;
		this._fullName = gamepadName+"_"+inputName;
	}

	public override float positionRaw { 
		get { 
			if (hInput.os == "Windows") return Input.GetAxisRaw(_fullName);
			return (Input.GetAxisRaw(_fullName) + 1)/2;			
		} 
	}
}