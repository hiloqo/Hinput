using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInAHat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (hInput.gamepad[0].start) Pause ();

		if (hInput.gamepad[1].B) Dash ();

		transform.position += hInput.gamepad[0].leftStick.worldPositionFlat * speed;

		if (hInput.anyGamepad.A.justPressed) Jump ();

		if (hInput.gamepad[3].X.doublePress) HeavyAttack ();

		if (hInput.gamepad[2].rightTrigger) Shoot ();

		if (hInput.anyGamepad.dPad.up) GoUp ();

		if (hInput.gamepad[0].rightStick.angle < 0) MoveBack ();

		if (hInput.gamepad[0].Y.longPress) Heal ();

		if (hInput.anyGamepad.rightStickClick.justReleased) ChangeWeapon (); 

		if (hInput.gamepad[2].leftStick.distance > 0.5f) Sprint ();







		if (hInput.gamepad[0].start) Pause (); if (hInput.gamepad[1]. B) Dash 
		(); transform.position += hInput.gamepad[0].leftStick.worldPositionFlat 
		* speed; if (hInput.anyGamepad.A.justPressed) Jump (); if (hInput.
		gamepad [3].X.doublePress) HeavyAttack (); if (hInput.gamepad[2].
		rightTrigger) Shoot (); if (hInput.anyGamepad. dPad.up) GoUp (); if 
		(hInput.gamepad[0].rightStick.angle < 0) MoveBack (); if (hInput.gamepad
		[0].Y.longPress) Heal (); if (hInput.anyGamepad.rightStickClick.justReleased) 
		ChangeWeapon (); if (hInput.gamepad [2].leftStick.distance > 0.5f) Sprint ();

	}

void Sprint () {}
void ChangeWeapon () {}
void Heal () {}
void MoveBack () {}
void GoUp () {}
void HeavyAttack () {}
void Dash () {}

void Shoot () {}

	public float speed;
	public Rigidbody rigidbae;
	public void Pause () {
		rigidbae.useGravity = true;
	}

	public void Jump () {
		if (transform.position.y < 0.6f) rigidbae.AddForce(Vector3.up * 200);
	}

	public void DoubleJump () {
		rigidbae.AddForce(Vector3.up * 200);
		rigidbae.AddTorque(transform.right * -65);
	}

}