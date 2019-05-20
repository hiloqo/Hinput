using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hDPad : hAbstractStick {
	public hDPad (string gamepadName, string dPadName) {
		_name = dPadName;
		_fullName = gamepadName+"_"+dPadName;

		_up = new hDPadDirection (_name, "Up", "Vertical", this, true, 90);
		_down = new hDPadDirection (_name, "Down", "Vertical", this, false, -90);
		_left = new hDPadDirection (_name, "Left", "Horizontal", this, true, 180);
		_right = new hDPadDirection (_name, "Right", "Horizontal", this, false, 0);

		_upLeft = new hStickDiagonal (this, "UpLeft", 135);
		_downLeft =  new hStickDiagonal (this, "DownLeft", -135);
		_upRight = new hStickDiagonal (this, "UpRight", 45);
		_downRight = new hStickDiagonal (this, "DownRight", -45);
	}
}