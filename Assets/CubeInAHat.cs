using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInAHat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (hInput.gamepad[0].start) Spawn ();

		transform.position += hInput.gamepad[0].leftStick.worldPositionFlat * speed;

		if (hInput.gamepad[0].A.justPressed) Jump ();

		if (hInput.gamepad[0].A.doublePressJustPressed) DoubleJump ();
	}











	public float speed;
	public Rigidbody rigidbae;
	public void Spawn () {
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