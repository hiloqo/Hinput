using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class hAbstractStickDirection : hAbstractInput {
	protected hAbstractStick hAbstractStick;
	protected string axisName;
	protected string fullAxisName;
	protected bool negative;
	protected float angle;

	public override float position { 
		get { 
			if (hAbstractStick.distanceRaw < hInput.deadZone) return 0f;
			else if (axisName == "Horizontal") return hAbstractStick.position.x;
			else if (axisName == "Vertical") return hAbstractStick.position.y;
			else Debug.Log("Erreur : axe non reconnu : "+axisName); return 0f;
		} 
	}

	public override bool pressed { 
		get { 
			return ((hAbstractStick.distance >= hInput.triggerZone) 
				&& (Mathf.Abs(angle - hAbstractStick.angle) <= hInput.directionAngle/2));
		} 
	}

	public override bool inDeadZone { get { return hAbstractStick.distance <= hInput.deadZone; } }
}
