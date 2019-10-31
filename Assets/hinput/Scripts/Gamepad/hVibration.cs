using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class hVibration {
	// --------------------
	// PRIVATE VARIABLES
	// --------------------

	private readonly List<PlayerIndex> index;
	private readonly bool canVibrate;
	private double currentLeft;
	private double currentRight;
	private double prevLeft;
	private double prevRight;


	// --------------------
	// CONSTRUCTOR
	// --------------------

    public hVibration (int index) {
	    if (hUtils.os != "Windows") return;
	    if (index > 3) return;
	    
	    if (index == 0) this.index = new List<PlayerIndex>() { PlayerIndex.One };
	    else if (index == 1) this.index = new List<PlayerIndex>() { PlayerIndex.Two };
		else if (index == 2) this.index = new List<PlayerIndex>() { PlayerIndex.Three };
	    else if (index == 3) this.index = new List<PlayerIndex>() { PlayerIndex.Four };
	    else if (index == -1) this.index = new List<PlayerIndex>() {
		    PlayerIndex.One, 
		    PlayerIndex.Two, 
		    PlayerIndex.Three, 
		    PlayerIndex.Four
	    };
	    
		canVibrate = true;
    }


	// --------------------
	// UPDATE
	// --------------------

	public void Update () {
		if (Mathf.Abs((float)currentLeft - (float)prevLeft) < hUtils.floatEpsilon && 
			Mathf.Abs((float)currentRight - (float)prevRight) < hUtils.floatEpsilon) return;
		
		prevLeft = currentLeft;
		prevRight = currentRight;
		DoVibrate(index, (float)currentLeft, (float)currentRight);
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
		this.currentLeft += left;
		this.currentRight += right;
	}

	public void StopVibration () {
		currentLeft = 0;
		currentRight = 0;
		DoVibrate(index, 0, 0);
		hUtils.StopRoutines();
	}


	// --------------------
	// PRIVATE METHODS
	// --------------------

	private IEnumerator _Vibrate (double left, double right, double duration) {
		if (canVibrate) {
			this.currentRight += right;
			this.currentLeft += left;

			yield return new WaitForSecondsRealtime ((float)duration);

			this.currentRight -= right;
			this.currentLeft -= left;
		} else {
			if (hUtils.os != "Windows") {
				Debug.LogWarning("hinput warning : vibration is only supported on Windows computers.");
			} else {
				Debug.LogWarning("hinput warning : vibration is only supported on four controllers.");
			}
		}
	}

	private static void DoVibrate(List<PlayerIndex> indices, double left, double right) {
		try {
			foreach (PlayerIndex playerIndex in indices) {
				GamePad.SetVibration(playerIndex, (float)left, (float)right);
			}
		} catch { /*Ignore errors here*/ }
	}
}