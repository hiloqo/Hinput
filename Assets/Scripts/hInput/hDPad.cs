using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hDPad : hAbstractStick {
	public hDPad (string fullDPadName) {
		_up = new hDPadDirection (fullDPadName, "Up", "Vertical", this, true, 90);
		_down = new hDPadDirection (fullDPadName, "Down", "Vertical", this, false, -90);
		_left = new hDPadDirection (fullDPadName, "Left", "Horizontal", this, true, 180);
		_right = new hDPadDirection (fullDPadName, "Right", "Horizontal", this, false, 0);

		_leftUp = new hStickDiagonal (this, 135);
		_leftDown =  new hStickDiagonal (this, -135);
		_rightUp = new hStickDiagonal (this, 45);
		_rightDown = new hStickDiagonal (this, -45);
	}
}