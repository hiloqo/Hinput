using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class hAbstractStickDirection : hAbstractInput {
	protected int _stickIndex;
	public int stickIndex { get { return _stickIndex; } }

	public hAbstractStick hStick { get { return gamepad.sticks[stickIndex]; } }

	protected float angle;

	public override bool pressed { 
		get { 
			return (hStick.inTriggerZone 
				&& (Mathf.Abs(Mathf.DeltaAngle(angle, hStick.angle)) <= hInput.directionAngle/2));
		} 
	}

	public override bool inDeadZone { 
		get { 
			return (hStick.distance < hInput.deadZone
				|| (Mathf.Abs(Mathf.DeltaAngle(angle, hStick.angle)) > hInput.directionAngle/2));
		} 
	}
}
