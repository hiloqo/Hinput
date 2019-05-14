using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hTester : MonoBehaviour {
	public bool basics;

	void Start () {
		if (basics) {
			Debug.Log("hInput gameObject name is : "+hInput.instance.name);
			Debug.Log("OS is : "+hInput.os);
		}
	}

	void Update () {
		Vector2 v2 = hInput.gamepad[0].leftStick.positionRaw;
		Debug.Log("("+v2.x+"  ,  "+v2.y+")");
	}
}