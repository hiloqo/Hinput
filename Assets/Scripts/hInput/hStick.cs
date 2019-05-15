using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hStick {
	private hAbstractInput _up;
	private hAbstractInput _down;
	private hAbstractInput _left;
	private hAbstractInput _right;

	//For stick
	public hStick (string name, hGamepad gamepad) {
		_up = new hButton("Vertical", gamepad, this, name, true, 90);
		_down = new hButton("Vertical", gamepad, this, name, false, -90);
		_left = new hButton("Horizontal", gamepad, this, name, true, 180);
		_right = new hButton("Horizontal", gamepad, this, name, false, 0);
	}

	//For dpad
	public hStick (hGamepad gamepad, string name) {
		_up = new hButton("Up", "Vertical", gamepad, this, name, true, 90);
		_down = new hButton("Down", "Vertical", gamepad, this, name, false, -90);
		_left = new hButton("Left", "Horizontal", gamepad, this, name, true, 180);
		_right = new hButton("Right", "Horizontal", gamepad, this, name, false, 0);
	}
	
	public void Update () {
		_up.Update();
		_down.Update();
		_left.Update();
		_right.Update();
	}

	public hAbstractInput up { get { return _up; } }
	public hAbstractInput down { get { return _down; } }
	public hAbstractInput left { get { return _left; } }
	public hAbstractInput right { get { return _right; } }

	public float horizontalRaw { get { return right.positionRaw; } }
	public float verticalRaw { get { return up.positionRaw; } }
	public Vector2 positionRaw { get { return new Vector2 (horizontalRaw, verticalRaw); } }
	public float distanceRaw { get { return positionRaw.magnitude; } }

	public Vector2 position { 
		get {  
			if (distanceRaw < hInput.deadZone) return Vector2.zero;
			else {
				Vector2 pos = positionRaw;
				Vector2 deadZonedPos = ((pos - pos.normalized*hInput.deadZone)/(1 - hInput.deadZone));
				return new Vector2 (Mathf.Clamp(deadZonedPos.x, -1, 1), Mathf.Clamp(deadZonedPos.y, -1, 1));
			}
		} 
	}

	public float horizontal { get { return position.x; } }
	public float vertical { get { return position.y; } }
	public float distance { get { return Mathf.Clamp01(position.magnitude); } }

	public float angle { get { return Vector2.SignedAngle(Vector2.right, position); } }

	public bool inDeadZone { get { return distance <= hInput.deadZone; } }
	public bool inTriggerZone { get { return distance >= hInput.triggerZone; } }

	public Vector3 worldPosition { get { return (hInput.worldCamera.right*horizontal + hInput.worldCamera.up*vertical); } }
	public Vector3 worldPositionFlat { get { return new Vector3 (horizontal, 0, vertical); } }

	public Vector3 worldPositionRaw { get { return (hInput.worldCamera.right*horizontalRaw + hInput.worldCamera.up*verticalRaw); } }
	public Vector3 worldPositionFlatRaw { get { return new Vector3 (horizontalRaw, 0, verticalRaw); } }
}
