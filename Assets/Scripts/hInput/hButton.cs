using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hButton : hAbstractInput {
	string buttonName;
	string axisName;
	string fullButtonName;
	string fullAxisName;
	public hStick hStick;
	public hGamepad gamepad;
	public bool negative = false;
	float angle;

	//For buttons
	public hButton(string buttonName, hGamepad gamepad) {
		this.gamepad = gamepad;
		this.buttonName = buttonName;
		this.fullButtonName = gamepad.name+"_"+this.buttonName;
	}

	//For sticks
	public hButton(string axisName, hGamepad gamepad, hStick hStick, bool negative, float angle) {
		this.gamepad = gamepad;
		this.fullAxisName = gamepad.name+"_"+axisName;
		this.axisName = axisName;
		this.hStick = hStick;
		this.negative = negative;
		this.angle = angle;
	}

	//For DPad
	public hButton(string buttonName, string axisName, hGamepad gamepad, hStick hStick, bool negative) {
		this.buttonName = buttonName;
		this.gamepad = gamepad;
		this.axisName = axisName;
		this.fullButtonName = gamepad.name+"_"+buttonName;
		this.fullAxisName = gamepad.name+"_"+axisName;
		this.hStick = hStick;
		this.negative = negative;
	}

	float GetButton () {
		if (Input.GetButton(fullButtonName)) return 1;
		else return 0;
	}

	float GetAxis () {
		if (negative) return -Input.GetAxisRaw(fullAxisName);
		return Input.GetAxisRaw(fullAxisName);
	}

	float GetInput () {
		float axisValue = 0f;
		float buttonValue = 0f;

		try { axisValue = GetAxis(); } catch { }
		try { buttonValue = GetButton(); } catch { }

		return (axisValue + buttonValue);
	}

	public override float positionRaw { 
		get { 
			if (hInput.os == "Mac" || hInput.os == "Linux") {
				if (buttonName == "LeftTrigger") return (GetInput() + 1)/2;
				if (buttonName == "RightTrigger") return (GetInput() + 1)/2;
			}

			return GetInput();
		} 
	}

	public override float position { 
		get { 
			if (hStick == null) {
				float posRaw = positionRaw;
				float deadZone = hInput.deadZone;

				if (Mathf.Abs(posRaw) < deadZone) return 0f;
				else return ((posRaw - deadZone*Mathf.Sign(posRaw))/(1 - deadZone));
			} else {
				if (hStick.distanceRaw < hInput.deadZone) return 0f;
				else if (axisName == "Horizontal") return hStick.position.x;
				else if (axisName == "Vertical") return hStick.position.y;
				else Debug.Log("Erreur : axe non reconnu : "+axisName); return 0f;
			}
		} 
	}

	public override bool pressed { 
		get { 
			if (hStick == null) return position >= hInput.triggerZone; 
			else return ((hStick.distance >= hInput.triggerZone) && (Mathf.Abs(angle - hStick.angle) <= hInput.directionAngle));
		} 
	}
}
