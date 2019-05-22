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

	protected override void UpdatePositionRaw () {
		if (negative) _positionRaw = -Input.GetAxisRaw(fullAxisName);
		else _positionRaw = Input.GetAxisRaw(fullAxisName);
	}
}