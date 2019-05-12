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
}