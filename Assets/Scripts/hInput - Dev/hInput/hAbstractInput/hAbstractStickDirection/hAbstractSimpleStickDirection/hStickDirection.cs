using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hStickDirection : hAbstractSimpleStickDirection {
	public hStickDirection (int gamepadIndex, int stickIndex, string stickName, string stickFullName, string buttonName, string axisName, bool negative, float angle) {
		this._name = stickName+"_"+buttonName;
		this._gamepadIndex = gamepadIndex;
		this._fullName = stickFullName+"_"+buttonName;
		this._stickIndex = stickIndex;

		this.fullAxisName = stickFullName+"_"+axisName;
		this.axisName = axisName;

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