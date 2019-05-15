using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hTester : MonoBehaviour {
	[Header("GENERAL")]
	public bool basics;

	[Header("HSTICK")]
	public bool stickPosition;
	public bool stickPositionRaw;
	public bool horizontal;
	public bool horizontalRaw;
	public bool vertical;
	public bool verticalRaw;
	public bool angle;
	public bool distance;
	public bool distanceRaw;
	public bool stickInDeadZone;
	public bool stickInTriggerZone;
	public bool worldPosition;
	public bool worldPositionFlat;
	public bool stickDirections;

	[Header("HBUTTON")]
	public bool implicitCast;
	public bool buttonPosition;
	public bool buttonPositionRaw;
	public bool pressed;
	public bool released;
	public bool justPressed;
	public bool	justReleased;
	public bool lastPress;
	public bool lastPressStart;
	public bool lastReleased;
	public bool buttonInDeadZone;
	public bool doublePress;
	public bool doublePressJustPressed;
	public bool doublePressJustReleased;
	public bool longPress;
	public bool longPressJustReleased;
	public bool pressDuration;
	public bool releaseDuration;

	[Header("WORLDPOSITION")]
	public Transform redCube;
	public Transform blueSphere;
	public bool moveRedCube;
	public bool moveBlueSphere;
	public float moveSpeed;


	void Start () {
		if (basics) {
			Debug.Log("hInput gameObject name is : "+hInput.instance.name);
			Debug.Log("OS is : "+hInput.os);
			Debug.Log(hInput.worldCamera.name);
		}
	}

	void Update () {
		for (int i=0; i<hInput.maxGamepads; i++) {
			hStick leftStick = hInput.gamepad[i].leftStick;

			if (!leftStick.inDeadZone) {
				if (stickPosition) Debug.Log("Gamepad "+(i+1)+" Left Stick : "+leftStick.position);
				if (horizontal) Debug.Log("Gamepad "+(i+1)+" Left Stick : "+leftStick.horizontal);
				if (vertical) Debug.Log("Gamepad "+(i+1)+" Left Stick : "+leftStick.vertical);
				if (angle) Debug.Log("Gamepad "+(i+1)+" Left Stick : "+leftStick.angle);
				if (distance) Debug.Log("Gamepad "+(i+1)+" Left Stick : "+leftStick.distance);
				if (stickInTriggerZone) Debug.Log("Gamepad "+(i+1)+" Left Stick : "+leftStick.inTriggerZone);
				if (worldPosition) Debug.Log("Gamepad "+(i+1)+" Left Stick : "+leftStick.worldPosition);
				if (worldPositionFlat) Debug.Log("Gamepad "+(i+1)+" Left Stick : "+leftStick.worldPositionFlat);
				if (moveBlueSphere) blueSphere.transform.position += leftStick.worldPosition * Time.deltaTime * moveSpeed;
				if (moveRedCube) redCube.transform.position += leftStick.worldPositionFlat * Time.deltaTime * moveSpeed;
			}

			if (stickPositionRaw) Debug.Log("Gamepad "+(i+1)+" Left Stick : "+leftStick.positionRaw);
			if (horizontalRaw) Debug.Log("Gamepad "+(i+1)+" Left Stick : "+leftStick.horizontalRaw);
			if (verticalRaw) Debug.Log("Gamepad "+(i+1)+" Left Stick : "+leftStick.verticalRaw);
			if (distanceRaw) Debug.Log("Gamepad "+(i+1)+" Left Stick : "+leftStick.distanceRaw);
			if (stickInDeadZone) Debug.Log("Gamepad "+(i+1)+" Left Stick : "+leftStick.inDeadZone);

			if (stickDirections) {
				if (leftStick.up) Debug.Log("Gamepad "+(i+1)+" Left Stick up");
				if (leftStick.down) Debug.Log("Gamepad "+(i+1)+" Left Stick down");
				if (leftStick.left) Debug.Log("Gamepad "+(i+1)+" Left Stick left");
				if (leftStick.right) Debug.Log("Gamepad "+(i+1)+" Left Stick right");
			}


			hAbstractInput A = hInput.gamepad[i].leftTrigger;

			if (implicitCast) Debug.Log("Gamepad "+(i+1)+" A : "+((bool)A));
			if (buttonPosition) Debug.Log ("Gamepad "+(i+1)+" A : "+A.position);
			if (buttonPositionRaw) Debug.Log("Gamepad "+(i+1)+" A : "+A.positionRaw);
			if (pressed) Debug.Log ("Gamepad "+(i+1)+" A : "+A.pressed);
			if (released) Debug.Log("Gamepad "+(i+1)+" A : "+A.released);
			if (justPressed) Debug.Log ("Gamepad "+(i+1)+" A : "+A.justPressed);
			if (justReleased) Debug.Log ("Gamepad "+(i+1)+" A : "+A.justReleased);
			if (lastPress) Debug.Log("Gamepad "+(i+1)+" A : "+A.lastPressed);
			if (lastPressStart) Debug.Log ("Gamepad "+(i+1)+" A : "+A.lastPressStart);
			if (lastReleased) Debug.Log("Gamepad "+(i+1)+" A : "+A.lastReleased);
			if (buttonInDeadZone) Debug.Log ("Gamepad "+(i+1)+" A : "+A.inDeadZone);
			if (doublePress) Debug.Log("Gamepad "+(i+1)+" A : "+A.doublePress);
			if (doublePressJustPressed) Debug.Log ("Gamepad "+(i+1)+" A : "+A.doublePressJustPressed);
			if (doublePressJustReleased) Debug.Log("Gamepad "+(i+1)+" A : "+A.doublePressJustReleased);
			if (longPress) Debug.Log ("Gamepad "+(i+1)+" A : "+A.longPress);
			if (longPressJustReleased) Debug.Log("Gamepad "+(i+1)+" A : "+A.longPressJustReleased);
			if (pressDuration) Debug.Log ("Gamepad "+(i+1)+" A : "+A.pressDuration);
			if (releaseDuration) Debug.Log("Gamepad "+(i+1)+" A : "+A.releaseDuration);
		}
	}
}