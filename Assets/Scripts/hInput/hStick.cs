using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hStick : hAbstractStick {
	public hStick (string fullStickName) {
		_up = new hStickDirection (fullStickName, "Vertical", this, true, 90);
		_down = new hStickDirection (fullStickName, "Vertical", this, false, -90);
		_left = new hStickDirection (fullStickName, "Horizontal", this, true, 180);
		_right = new hStickDirection (fullStickName, "Horizontal", this, false, 0);

		_leftUp = new hStickDiagonal (this, 135);
		_leftDown =  new hStickDiagonal (this, -135);
		_rightUp = new hStickDiagonal (this, 45);
		_rightDown = new hStickDiagonal (this, -45);
	}
}