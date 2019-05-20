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
			else {
				float position = 0f;

				if (axisName == "Horizontal") {
					position = hAbstractStick.position.x;
					if (negative) position = -position;
				} 
				else if (axisName == "Vertical") {
					position = hAbstractStick.position.y;
					if (!negative) position = -position;
				}
				else Debug.Log("Erreur : axe non reconnu : "+axisName);


				return position;
			}
		} 
	}
}