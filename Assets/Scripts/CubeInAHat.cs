using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (hinput.gamepad[0].start) Pause ();

		if (hinput.gamepad[1].B) Dash ();

		transform.position += hinput.gamepad[0].leftStick.worldPositionFlat * speed;

		if (hinput.anyGamepad.A.justPressed) Jump ();

		if (hinput.gamepad[3].X.doublePress) HeavyAttack ();

		if (hinput.gamepad[2].rightTrigger) Shoot ();

		if (hinput.anyGamepad.dPad.up.justPressed) Emote ();

		if (hinput.gamepad[0].rightStick.angle < 0) MoveBack ();

		if (hinput.gamepad[0].Y.longPress) Heal ();

		if (hinput.anyGamepad.rightStickClick.justReleased) ChangeWeapon (); 

		if (hinput.gamepad[2].leftStick.distance > 0.8f) Sprint ();







		if (hinput.gamepad[0].start) Pause (); if (hinput.gamepad[1]. B) Dash 
		(); transform.position += hinput.gamepad[0].leftStick.worldPositionFlat 
		* speed; if (hinput.anyGamepad.A.justPressed) Jump (); if (hinput.
		gamepad [3].X.doublePress) HeavyAttack (); if (hinput.gamepad[2].
		rightTrigger) Shoot (); if (hinput.anyGamepad. dPad.up) Emote (); if 
		(hinput.gamepad[0].rightStick.angle < 0) MoveBack (); if (hinput.gamepad
		[0].Y.longPress) Heal (); if (hinput.anyGamepad.rightStickClick.justReleased) 
		ChangeWeapon (); if (hinput.gamepad [2].leftStick.distance > 0.5f) Sprint ();

	}

void Sprint () {}
void ChangeWeapon () {}
void Heal () {}
void MoveBack () {}
void Emote () {}
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