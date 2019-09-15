using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class hVibration {
	// --------------------
	// PRIVATE VARIABLES
	// --------------------

	private PlayerIndex index;
	private bool canVibrate;
	private double left;
	private double right;
	private double prevLeft;
	private double prevRight;


	// --------------------
	// CONSTRUCTOR
	// --------------------

    public hVibration (int index, hGamepad gamepad) {
		if (hUtils.os == "Windows") {
			if (index == 0) this.index = PlayerIndex.One;
			else if (index == 1) this.index = PlayerIndex.Two;
			else if (index == 2) this.index = PlayerIndex.Three;
			else if (index == 3) this.index = PlayerIndex.Four;
			else return;

			canVibrate = true;
		}
    }


	// --------------------
	// UPDATE
	// --------------------

	public void Update () {
		if (left != prevLeft || right != prevRight) {
			prevLeft = left;
			prevRight = right;
			GamePad.SetVibration(index, (float)left, (float)right);
		}
	}


	// --------------------
	// PUBLIC METHODS
	// --------------------

	public void Vibrate (double duration) {
		hUtils.Coroutine(_Vibrate(
			hSettings.leftVibrationIntensity, 
			hSettings.rightVibrationIntensity, 
			duration
		));
	}

	public void VibrateLeft (double duration) {
		hUtils.Coroutine(_Vibrate(
			hSettings.leftVibrationIntensity, 
			0, 
			duration
		));
	}

	public void VibrateRight (double duration) {
		hUtils.Coroutine(_Vibrate(
			0, 
			hSettings.rightVibrationIntensity, 
			duration
		));
	}

	public void VibrateAdvanced (double left, double right, double duration) {
		hUtils.Coroutine(_Vibrate(
			left, 
			right, 
			duration
		));
	}

	public void VibrateAdvanced (double left, double right) {
		this.left += left;
		this.right += right;
	}

	public void StopVibration () {
		left = 0;
		right = 0;
		hUtils.StopRoutines();
	}


	// --------------------
	// PRIVATE METHOD
	// --------------------

	private IEnumerator _Vibrate (double left, double right, double duration) {
		if (canVibrate) {
			this.right += right;
			this.left += left;

			yield return new WaitForSecondsRealtime ((float)duration);

			this.right -= right;
			this.left -= left;
		} else {
			if (hUtils.os != "Windows") {
				Debug.LogWarning("hinput warning : vibration is only supported on Windows computers.");
			} else {
				Debug.LogWarning("hinput warning : vibration is only supported on four controllers.");
			}
		}
	}
}