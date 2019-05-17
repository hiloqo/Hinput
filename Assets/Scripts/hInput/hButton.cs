using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hButton : hAbstractSimpleInput {
	public hButton (string fullInputName) {
		this.fullInputName = fullInputName;
	}

	public override float positionRaw { 
		get { 
			if (Input.GetButton(fullInputName)) return 1;
			else return 0;
		} 
	}
}