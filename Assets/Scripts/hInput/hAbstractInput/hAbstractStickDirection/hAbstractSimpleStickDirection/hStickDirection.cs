using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hStickDirection : hAbstractSimpleStickDirection {
	public hStickDirection (string stickName, string buttonName, string axisName, hStick hStick, bool negative, float angle) {
		this._name = stickName+"_"+buttonName;
		this._fullName = hStick.fullName+"_"+buttonName;

		this.fullAxisName = hStick.fullName+"_"+axisName;
		this.axisName = axisName;
		this.hAbstractStick = hStick;
		this.negative = negative;
		this.angle = angle;
	}

	public override float positionRaw { 
		get { 
			if (negative) return -Input.GetAxisRaw(fullAxisName);
			return Input.GetAxisRaw(fullAxisName);
		} 
	}
}