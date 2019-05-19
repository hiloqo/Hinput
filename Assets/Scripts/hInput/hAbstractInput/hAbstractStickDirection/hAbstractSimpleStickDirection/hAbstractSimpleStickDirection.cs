using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class hAbstractSimpleStickDirection : hAbstractStickDirection {
	protected string axisName;
	protected string fullAxisName;
	protected bool negative;

	public override float position { 
		get { 
			if (hAbstractStick.distanceRaw < hInput.deadZone) return 0f;
			else if (axisName == "Horizontal") return hAbstractStick.position.x;
			else if (axisName == "Vertical") return hAbstractStick.position.y;
			else Debug.Log("Erreur : axe non reconnu : "+axisName); return 0f;
		} 
	}
}
