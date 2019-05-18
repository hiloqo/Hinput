using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class hAbstractStick {
	// --------------------
	// STICK DIRECTIONS
	// --------------------

	protected hAbstractInput _up;
	protected hAbstractInput _down;
	protected hAbstractInput _left;
	protected hAbstractInput _right;

	protected hStickDiagonal _leftUp;
	protected hStickDiagonal _leftDown;
	protected hStickDiagonal _rightUp;
	protected hStickDiagonal _rightDown;
	
	public void Update () {
		_up.Update();
		_down.Update();
		_left.Update();
		_right.Update();
		
		_leftUp.Update();
		_leftDown.Update();
		_rightUp.Update();
		_rightDown.Update();
	}

	public hAbstractInput up { get { return _up; } }
	public hAbstractInput down { get { return _down; } }
	public hAbstractInput left { get { return _left; } }
	public hAbstractInput right { get { return _right; } }
	
	public hStickDiagonal leftUp { get { return _leftUp; } }
	public hStickDiagonal leftDown { get { return _leftDown; } }
	public hStickDiagonal rightUp { get { return _rightUp; } }
	public hStickDiagonal rightDown { get { return _rightDown; } }

	
	// --------------------
	// PUBLIC PROPERTIES
	// --------------------

	public float horizontalRaw { get { return right.positionRaw; } }
	public float verticalRaw { get { return up.positionRaw; } }
	public Vector2 positionRaw { get { return new Vector2 (horizontalRaw, verticalRaw); } }
	public float distanceRaw { get { return positionRaw.magnitude; } }
	public bool inDeadZone { get { return distanceRaw < hInput.deadZone; } }

	public Vector2 position { 
		get {  
			if (inDeadZone) return Vector2.zero;
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
	public bool inTriggerZone { get { return distance >= hInput.triggerZone; } }

	public float angleRaw { get { return Vector2.SignedAngle(Vector2.right, positionRaw); } }
	public float angle { get { return Vector2.SignedAngle(Vector2.right, position); } }


	public Vector3 cameraPosition { get { return (hInput.worldCamera.right*horizontal + hInput.worldCamera.up*vertical); } }
	public Vector3 worldPositionFlat { get { return new Vector3 (horizontal, 0, vertical); } }

	public Vector3 cameraPositionRaw { get { return (hInput.worldCamera.right*horizontalRaw + hInput.worldCamera.up*verticalRaw); } }
	public Vector3 worldPositionFlatRaw { get { return new Vector3 (horizontalRaw, 0, verticalRaw); } }
}