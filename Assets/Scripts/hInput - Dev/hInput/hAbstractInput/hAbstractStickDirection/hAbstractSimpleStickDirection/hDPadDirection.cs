using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hDPadDirection : hAbstractSimpleStickDirection {
	public hDPadDirection (string stickName, string buttonName, string axisName, hDPad hDPad, bool negative, float angle) {
		this._name = stickName+"_"+buttonName;
		this._fullName = hDPad.fullName+"_"+buttonName;

		this.fullAxisName = hDPad.fullName+"_"+axisName;
		this.axisName = axisName;
		this.hAbstractStick = hDPad;
		this.negative = negative;
		this.angle = angle;
	}

	public override float positionRaw { 
		get { 
			float axisValue = 0f;
			try { 
				if (negative) axisValue = -Input.GetAxisRaw(fullAxisName);
				else axisValue = Input.GetAxisRaw(fullAxisName);
			 } catch { } //Dont care if error here

			float buttonValue = 0f;
			try { 
				if (Input.GetButton(_fullName)) buttonValue = 1;
			 } catch { } //Dont care if error here

			return (axisValue + buttonValue);
		} 
	}
}