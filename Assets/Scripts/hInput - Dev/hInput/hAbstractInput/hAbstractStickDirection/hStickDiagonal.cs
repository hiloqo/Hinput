using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hStickDiagonal : hAbstractStickDirection {

	public hStickDiagonal (int gamepadIndex, int stickIndex, string stickName, string stickFullName, string buttonName, float angle) {
		this._name = stickName+"_"+buttonName;
		this._gamepadIndex = gamepadIndex;
		this._fullName = stickFullName+"_"+buttonName;

		this._stickIndex = stickIndex;

		this.angle = angle;
	}

	private float positionToDiagonal (Vector2 position) {
		float x = position.x;
		float y = position.y;

		if (angle < 0) y = -y;
		if (Mathf.Abs(angle) > 90) x = -x;
		 
		return Mathf.Clamp01((1 + hInput.diagonalIncrease)*(x+y)/Mathf.Sqrt(2));
	}

	protected override void UpdatePositionRaw () {
		_positionRaw = positionToDiagonal (hStick.positionRaw);
	}

	public override float position { get { return positionToDiagonal (hStick.position); } }
}