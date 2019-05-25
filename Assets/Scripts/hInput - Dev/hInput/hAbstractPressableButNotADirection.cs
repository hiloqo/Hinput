using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class hAbstractPressableButNotADirection : hAbstractPressable {

	public override float position { 
		get { 
			float posRaw = positionRaw;
			float deadZone = hInput.deadZone;

			if (posRaw < deadZone) return 0f;
			else return ((posRaw - deadZone)/(1 - deadZone));
		} 
	}

	public override bool pressed { get { return position >= hInput.triggerZone; } }

	public override bool inDeadZone { get { return position < hInput.deadZone; } }
}
