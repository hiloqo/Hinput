using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScript : MonoBehaviour {	
	// This is a "speed" variable that you can change in your Unity editor
	public float speed = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// To make a simple character controller, you can do this
		// (for a 3D game or a top-down 2D game)
		transform.position += 
			hinput.gamepad[0].leftStick.worldPositionFlat * speed * Time.deltaTime;

		// OR you can do this
		// (for a side scrolling 2D game, or to move a cursor)
		transform.position += 
			hinput.gamepad[0].leftStick.worldPositionCamera * speed * Time.deltaTime;






		if (hinput.gamepad[0].A.justPressed) {
			//Jump
		}

		if (hinput.anyGamepad.dPad.up.justPressed) {
			//Emote
		}

		if (hinput.anyGamepad.A.released) {
			//Fall
		}

		if (hinput.anyGamepad.rightStickClick.justReleased) {
			//ChangeWeapon
		}

		if (hinput.gamepad[3].X.doublePress) {
			//HeavyAttack
		}

		if (hinput.gamepad[2].rightTrigger.longPress) {
			//Shoot
		}

		if (hinput.gamepad[0].Y.longPress) {
			//Heal
		}

		if (hinput.gamepad[0].rightStick.vertical < 0) {
			//MoveBack
		}

		if (hinput.gamepad[2].leftStick.distance > 0.8f) {
			//Sprint
		}

		if (hinput.gamepad[0].start) {
			//Pause
		}

		if (hinput.gamepad[1].B) {
			//Dash
		}
		
	}
}
