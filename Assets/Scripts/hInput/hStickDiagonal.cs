using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hStickDiagonal : hAbstractInput {
	protected hAbstractStick hAbstractStick;
	protected float angle;

	public hStickDiagonal (hAbstractStick hAbstractStick, float angle) {
		this.hAbstractStick = hAbstractStick;
		this.angle = angle;
	}

	public override float positionRaw { 
		get { 
			return Mathf.Cos(hAbstractStick.angleRaw - angle);
		} 
	}

	public override float position { 
		get { 
			if (hAbstractStick.distanceRaw < hInput.deadZone) return 0f;
			return Mathf.Cos(hAbstractStick.angle - angle);
		} 
	}

	public override bool pressed { 
		get { 
			return ( hAbstractStick.inTriggerZone && (Mathf.Abs(angle - hAbstractStick.angle) <= hInput.directionAngle/2));
		} 
	}

	public override bool inDeadZone { 
		get { 
			return hAbstractStick.distance <= hInput.deadZone; 
		} 
	}
}