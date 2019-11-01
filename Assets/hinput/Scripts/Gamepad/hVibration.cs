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
	private float currentLeft;
	private float currentRight;
	private float prevLeft;
	private float prevRight;


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
		if (Mathf.Abs(currentLeft - prevLeft) < hUtils.floatEpsilon && 
			Mathf.Abs(currentRight - prevRight) < hUtils.floatEpsilon) return;
		
		prevLeft = currentLeft;
		prevRight = currentRight;
		DoVibrate(index, currentLeft, currentRight);
	}


	// --------------------
	// PUBLIC METHODS
	// --------------------

	public void Vibrate (float left, float right, float duration) {
		hUtils.Coroutine(_Vibrate(
			left, 
			right, 
			duration
		));
	}

	public void VibrateAdvanced (float left, float right) {
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

	private IEnumerator _Vibrate (float left, float right, float duration) {
		if (canVibrate) {
			this.currentRight += right;
			this.currentLeft += left;

			yield return new WaitForSecondsRealtime (duration);

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