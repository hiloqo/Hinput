using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hStickDirection : hAbstractStickDirection {
	public hStickDirection (string fullStickName, string axisName, hStick hStick, bool negative, float angle) {
		this.fullAxisName = fullStickName+"_"+axisName;
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