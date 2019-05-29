using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hAxis {
	// --------------------
	// NAME
	// --------------------

	private string fullAxisName;
	private string fullPositiveButtonName;
	private string fullNegativeButtonName;


	// --------------------
	// CONSTRUCTORS
	// --------------------

	public hAxis (string fullAxisName, string fullPositiveButtonName, string fullNegativeButtonName) {
		this.fullAxisName = fullAxisName;
		this.fullPositiveButtonName = fullPositiveButtonName;
		this.fullNegativeButtonName = fullNegativeButtonName;
	}

	public hAxis (string fullAxisName) {
		this.fullAxisName = fullAxisName;
		this.fullPositiveButtonName = "";
		this.fullNegativeButtonName = "";
	}

	
	// --------------------
	// PROPERTIES
	// --------------------

	private float _positionRaw;
	public float positionRaw { 
		get {
			float axisValue = 0f;
			float buttonValue = 0f;

			try { 
				axisValue = Input.GetAxisRaw(fullAxisName);
			} catch { } //Dont care if error here

			if (fullPositiveButtonName != "" && fullNegativeButtonName != "") {
				try { 
					if (Input.GetButton(fullPositiveButtonName)) buttonValue = 1;
					if (Input.GetButton(fullNegativeButtonName)) buttonValue = -1;
				} catch { } //Dont care if error here
			}

			return (axisValue + buttonValue);
		} 
	}
}