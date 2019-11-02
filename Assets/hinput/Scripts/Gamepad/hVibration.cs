using System.Collections;
using UnityEngine;
#if UNITY_WEBGL
#else
	using System.Collections.Generic;
	using XInputDotNetPure;
#endif

public class hVibration {
	// --------------------
	// PRIVATE VARIABLES
	// --------------------

	#if UNITY_WEBGL
	#else
		private readonly List<PlayerIndex> index;
	#endif
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
	    
		#if UNITY_WEBGL
		#else
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
		#endif
	    
    }


	// --------------------
	// UPDATE
	// --------------------

	public void Update () {
		if (currentLeft.IsEqualTo(prevLeft) && currentRight.IsEqualTo(prevRight)) return;
		
		prevLeft = currentLeft;
		prevRight = currentRight;
		#if UNITY_WEBGL
		#else
			DoVibrate(index, currentLeft, currentRight);
		#endif
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
		currentLeft += left;
		currentRight += right;
	}

	public void StopVibration () {
		currentLeft = 0;
		currentRight = 0;
		#if UNITY_WEBGL
		#else
			DoVibrate(index, 0, 0);
		#endif
		hUtils.StopRoutines();
	}


	// --------------------
	// PRIVATE METHODS
	// --------------------

	private IEnumerator _Vibrate (float left, float right, float duration) {
		if (canVibrate) {
			currentRight += right;
			currentLeft += left;

			yield return new WaitForSecondsRealtime (duration);

			currentRight -= right;
			currentLeft -= left;
		} else {
			#if UNITY_WEBGL
				Debug.LogWarning("hinput warning: vibration is not supported in WebGL");
			#endif
			if (hUtils.os != "Windows") {
				Debug.LogWarning("hinput warning : vibration is only supported on Windows computers.");
			} else {
				Debug.LogWarning("hinput warning : vibration is only supported on four controllers.");
			}
		}
	}

	#if UNITY_WEBGL
	#else
		private static void DoVibrate(List<PlayerIndex> indices, double left, double right) {
			try {
				foreach (PlayerIndex playerIndex in indices) {
					GamePad.SetVibration(playerIndex, (float)left, (float)right);
				}
			} catch { /*Ignore errors here*/ }
		}
	#endif
}