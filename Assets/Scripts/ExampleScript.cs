using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScript : MonoBehaviour {

	private hGamepad _gamepad;
	private hGamepad gamepad {
		get  {
			if (_gamepad == null) {
				_gamepad = hinput.gamepad[0];
			}

			return _gamepad;
		}
	}
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//This is a simple character controller for a 3D game
		float speed = 5;
		transform.position += hinput.gamepad[0].leftStick.worldPositionFlat * speed * Time.deltaTime;

		//This is how to check if a button has just been pressed
		if (hinput.anyGamepad.A.justPressed) {
			//Jump
		}

		//Here is how to check if a button is pressed
		if (hinput.gamepad[0].start) {
			//Pause
		}

		if (hinput.gamepad[1].B) {
			//Dash
		}

		if (hinput.gamepad[3].X.doublePress) {
			//HeavyAttack
		}

		if (hinput.gamepad[2].rightTrigger) {
			//Shoot
		}

		if (hinput.anyGamepad.dPad.up.justPressed) {
			//Emote
		}

		if (hinput.gamepad[0].rightStick.angle < 0) {
			//MoveBack
		}

		if (hinput.gamepad[0].Y.longPress) {
			//Heal
		}

		if (hinput.anyGamepad.rightStickClick.justReleased) {
			//ChangeWeapon
		}

		if (hinput.gamepad[2].leftStick.distance > 0.8f) {
			//Sprint
		}
		
	}
}
