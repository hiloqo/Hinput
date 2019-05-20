using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class hAbstractStickDirection : hAbstractInput {
	protected hAbstractStick hAbstractStick;
	protected float angle;

	public override bool pressed { 
		get { 
			return ( hAbstractStick.inTriggerZone 
				&& (Mathf.Abs(angle - hAbstractStick.angle) <= hInput.directionAngle/2));
		} 
	}

	public override bool inDeadZone { 
		get { 
			return (hAbstractStick.distance < hInput.deadZone
				|| (Mathf.Abs(angle - hAbstractStick.angle) > hInput.directionAngle/2));
		} 
	}
}
