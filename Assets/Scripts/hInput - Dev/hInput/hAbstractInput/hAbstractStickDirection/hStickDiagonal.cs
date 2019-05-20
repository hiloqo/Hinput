using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hStickDiagonal : hAbstractStickDirection {

	public hStickDiagonal (hAbstractStick hAbstractStick, string buttonName, float angle) {
		this._name = hAbstractStick.name+"_"+buttonName;
		this._fullName = hAbstractStick.fullName+"_"+buttonName;

		this.hAbstractStick = hAbstractStick;
		this.angle = angle;
	}

	private float positionToDiagonal (Vector2 position) {
		float x = position.x;
		float y = position.y;

		if (angle < 0) y = -y;
		if (Mathf.Abs(angle) > 90) x = -x;
		 
		return Mathf.Clamp01((1 + hInput.diagonalIncrease)*(x+y)/Mathf.Sqrt(2));
	}

	public override float positionRaw { get { return positionToDiagonal (hAbstractStick.positionRaw); } }

	public override float position { get { return positionToDiagonal (hAbstractStick.position); } }
}