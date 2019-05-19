using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hStick : hAbstractStick {
	public hStick (string gamepadName, string stickName) {
		_name = stickName;
		_fullName = gamepadName+"_"+stickName;

		_up = new hStickDirection (_name, "Up", "Vertical", this, true, 90);
		_down = new hStickDirection (_name, "Down", "Vertical", this, false, -90);
		_left = new hStickDirection (_name, "Left", "Horizontal", this, true, 180);
		_right = new hStickDirection (_name, "Right", "Horizontal", this, false, 0);

		_upLeft = new hStickDiagonal (this, "UpLeft", 135);
		_downLeft =  new hStickDiagonal (this, "DownLeft", -135);
		_upRight = new hStickDiagonal (this, "UpRight", 45);
		_downRight = new hStickDiagonal (this, "DownRight", -45);
	}
}