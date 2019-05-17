using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hTrigger : hAbstractSimpleInput {
	public hTrigger (string fullInputName) {
		this.fullInputName = fullInputName;
	}

	public override float positionRaw { 
		get { 
			if (hInput.os == "Windows") return Input.GetAxisRaw(fullInputName);
			return (Input.GetAxisRaw(fullInputName) + 1)/2;			
		} 
	}
}