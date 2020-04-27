using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class ExampleScript : MonoBehaviour {	
	// This is a "speed" variable that you can change in your Unity editor
	public float speed = 5;

	[Range(0,1)]
	public float strength;
	[Range(0,3)]
	public float duration;

	// Use this for initialization
	void Start () {
		
	}

	// public IEnumerator Rumble (float strength, float duration) {
	// 	GamePad.SetVibration(PlayerIndex.One, strength, strength);
	// 	yield return new WaitForSeconds(duration);
	// 	GamePad.SetVibration(PlayerIndex.One, 0, 0);
	// }

	// public IEnumerator RumbleIncrease (float strength, float duration) {
	// 	float timeLeft = duration;
	// 	while (timeLeft > 0) {
	// 		float str = strength*((timeLeft)/duration);
	// 		GamePad.SetVibration(PlayerIndex.One, str, str);
	// 		timeLeft -= Time.deltaTime;
	// 		yield return new WaitForEndOfFrame ();
	// 	}
	// 	GamePad.SetVibration(PlayerIndex.One, 0, 0);
	// }
	
	// Update is called once per frame
	void Update () {

		//GamePad.SetVibration(PlayerIndex.One, left, right);

		// if (Input.GetKeyDown(KeyCode.A)) StartCoroutine(Rumble (strength, duration));

		if (Input.GetKeyDown(KeyCode.Z)) {
			GamePad.SetVibration(PlayerIndex.One, strength, duration);
			GamePad.SetVibration(PlayerIndex.Two, strength, duration);
			GamePad.SetVibration(PlayerIndex.Three, strength, duration);
			GamePad.SetVibration(PlayerIndex.Four, strength, duration);
			Debug.Log("bro");
		} 

		// if (Input.GetKeyDown(KeyCode.E)) GamePad.SetVibration(PlayerIndex.One, 0, 0);

		// if (Input.GetKeyDown(KeyCode.R)) StartCoroutine(RumbleIncrease (strength, duration));



		// To make a simple character controller, you can do this
		// (for a 3D game or a top-down 2D game)
		transform.position += 
			Hinput.gamepad[0].leftStick.worldPositionFlat * speed * Time.deltaTime;

		// OR you can do this
		// (for a side scrolling 2D game, or to move a cursor)
		transform.position += 
			Hinput.gamepad[0].leftStick.worldPositionCamera * speed * Time.deltaTime;






		if (Hinput.gamepad[0].A.justPressed) {
			//Jump
		}

		if (Hinput.anyGamepad.dPad.up.justPressed) {
			//Emote
		}

		if (Hinput.anyGamepad.A.released) {
			//Fall
		}

		if (Hinput.anyGamepad.rightStickClick.justReleased) {
			//ChangeWeapon
		}

		if (Hinput.gamepad[3].X.doublePress) {
			//HeavyAttack
		}

		if (Hinput.gamepad[2].rightTrigger.longPress) {
			//Shoot
		}

		if (Hinput.gamepad[0].Y.longPress) {
			//Heal
		}

		if (Hinput.gamepad[0].rightStick.vertical < 0) {
			//MoveBack
		}

		if (Hinput.gamepad[2].leftStick.distance > 0.8f) {
			//Sprint
		}

		if (Hinput.gamepad[0].start) {
			//Pause
		}

		if (Hinput.gamepad[1].B) {
			//Dash
		}
		
	}
}
