using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hGamepad {
	public string name;

	public hAbstractInput A;
	public hStick leftStick;

	public hGamepad (string name) {
		this.name = name;

		A = new hButton ("A", this);
		leftStick = new hStick ("LeftStick", this);
	}

	public void Update () {
		A.Update();
		leftStick.Update();
	}
}