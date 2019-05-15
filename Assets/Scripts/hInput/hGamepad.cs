using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hGamepad {
	public string name;

	public hAbstractInput A;
	public hStick leftStick;
	public hStick dPad;
	public hAbstractInput leftTrigger;

	public hGamepad (string name) {
		this.name = name;

		A = new hButton ("A", this);
		leftStick = new hStick ("LeftStick", this);
		dPad = new hStick (this, "DPad");
		leftTrigger = new hButton (this, "LeftTrigger");
	}

	public void Update () {
		A.Update();
		leftStick.Update();
		dPad.Update();
		leftTrigger.Update();
	}
}