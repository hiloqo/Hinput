using System.Collections;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
	using System.Collections.Generic;
	using XInputDotNetPure;
#endif

public class hVibration {
	// --------------------
	// PRIVATE VARIABLES
	// --------------------

	#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
		private readonly List<PlayerIndex> index;
	#endif
	private readonly bool canVibrate = false;
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
	    
		
		#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
		    if (index == 0) this.index = new List<PlayerIndex> { PlayerIndex.One };
		    else if (index == 1) this.index = new List<PlayerIndex> { PlayerIndex.Two };
			else if (index == 2) this.index = new List<PlayerIndex> { PlayerIndex.Three };
		    else if (index == 3) this.index = new List<PlayerIndex> { PlayerIndex.Four };
		    else if (index == -1) this.index = new List<PlayerIndex> {
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
		
		#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
			DoVibrate(index, currentLeft, currentRight);
		#endif
	}


	// --------------------
	// PUBLIC METHODS
	// --------------------

	public void Vibrate (float left, float right, float duration) {
		hUtils.Coroutine(_Vibrate(left, right, duration));
	}

	public void Vibrate(AnimationCurve leftCurve, AnimationCurve rightCurve) {
		hUtils.Coroutine(_Vibrate(leftCurve, rightCurve));
	}

	public void VibrateAdvanced (float left, float right) {
		currentLeft += left;
		currentRight += right;
	}

	public void StopVibration () {
		currentLeft = 0;
		currentRight = 0;
		
		#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
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
			if (hUtils.os != "Windows") {
				Debug.LogWarning("hinput warning : vibration is only supported on Windows computers.");
			} else {
				Debug.LogWarning("hinput warning : vibration is only supported on four controllers.");
			}
		}
	}

	private IEnumerator _Vibrate(AnimationCurve leftCurve, AnimationCurve rightCurve) {
		float time = 0;
		bool leftCurveIsOver = (leftCurve.keys.Length == 0);
		bool rightCurveIsOver = (rightCurve.keys.Length == 0);

		while(!leftCurveIsOver || !rightCurveIsOver) {
			float leftCurveValue;
			if (leftCurveIsOver) leftCurveValue = 0;
			else leftCurveValue = leftCurve.Evaluate(time);

			float rightCurveValue;
			if (rightCurveIsOver) rightCurveValue = 0;
			else rightCurveValue = rightCurve.Evaluate(time);

			time += Time.deltaTime;
			leftCurveIsOver = (time > leftCurve.keys.Last().time);
			rightCurveIsOver = (time > rightCurve.keys.Last().time);
			
			currentLeft += leftCurveValue;
			currentRight += rightCurveValue;
			yield return new WaitForEndOfFrame();
			currentLeft -= leftCurveValue;
			currentRight -= rightCurveValue;
		}

		yield return null;
	}

	
	#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
		private static void DoVibrate(List<PlayerIndex> indices, float left, float right) {
			try {
				indices.ForEach(playerIndex => GamePad.SetVibration(playerIndex, left, right));
			} catch { /*Ignore errors here*/ }
		}
	#endif
}