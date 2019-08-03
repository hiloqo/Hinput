using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#

public class test : MonoBehaviour
{
	private PlayerIndex index = PlayerIndex.One;
	public float left;
	public float right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKey(KeyCode.Z)) {
    //         GamePad.SetVibration(PlayerIndex.One, 1, 1);
    //         Debug.Log("go");
    //     }
        
    // }

	public void Update () {
        if (Input.GetKeyDown(KeyCode.A)) {
            hSettings.instance.StartCoroutine(_Vibrate(0.5f));
        }
		GamePad.SetVibration(index, left, right);
	}

	public IEnumerator _Vibrate (float duration) {
		left += 1;
		right += 1;

		yield return new WaitForSeconds (duration);
		
		left -= 1;
		right -= 1;
	}
}
