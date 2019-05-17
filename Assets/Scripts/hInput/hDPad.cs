using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hDPad : hAbstractStick {
	public hDPad (string fullName) {
		_up = new hDPadDirection (fullName, "Up", "Vertical", this, true, 90);
		_down = new hDPadDirection (fullName, "Down", "Vertical", this, false, -90);
		_left = new hDPadDirection (fullName, "Left", "Horizontal", this, true, 180);
		_right = new hDPadDirection (fullName, "Right", "Horizontal", this, false, 0);
	}
}