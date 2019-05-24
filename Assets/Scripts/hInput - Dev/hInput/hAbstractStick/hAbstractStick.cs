using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class hAbstractStick {
	// --------------------
	// NAME
	// --------------------

	protected string _name;
	public string name { get { return _name; } }

	protected string _fullName;
	public string fullName { get { return _fullName; } }

	protected int _gamepadIndex;
	public int gamepadIndex { get { return _gamepadIndex; } }

	protected int _stickIndex;
	public int stickIndex { get { return _stickIndex; } }

	public hGamepad gamepad { 
		get { 
			if (gamepadIndex >= 0) return hInput.gamepad[gamepadIndex]; 
			else return hInput.anyGamepad;
		} 
	}

	
	// --------------------
	// IMPLICIT CONVERSION
	// --------------------

	public static implicit operator Vector2 (hAbstractStick hAbstractStick) { return hAbstractStick.position; }


	// --------------------
	// VERTICALS AND HORIZONTALS
	// --------------------

	protected hAbstractInput _up;
	protected hAbstractInput _down;
	protected hAbstractInput _left;
	protected hAbstractInput _right;

	public abstract hAbstractInput up { get; }
	public abstract hAbstractInput down { get; }
	public abstract hAbstractInput left { get; }
	public abstract hAbstractInput right { get; }

	
	// --------------------
	// DIAGONALS
	// --------------------

	protected hStickDiagonal _upLeft;
	protected hStickDiagonal _downLeft;
	protected hStickDiagonal _upRight;
	protected hStickDiagonal _downRight;
	
	public hStickDiagonal leftUp { get { return upLeft; } }
	public hStickDiagonal upLeft { 
		get {
			if (_upLeft == null) _upLeft = new hStickDiagonal (gamepadIndex, stickIndex, name, fullName, "UpLeft", 135);
			return _upLeft;
		} 
	}

	public hStickDiagonal leftDown { get { return downLeft; } }
	public hStickDiagonal downLeft { 
		get {
			if (_downLeft == null) _downLeft = new hStickDiagonal (gamepadIndex, stickIndex, name, fullName, "DownLeft", -135);
			return _downLeft;
		} 
	}

	public hStickDiagonal rightUp { get { return upRight; } }
	public hStickDiagonal upRight { 
		get {
			if (_upRight == null) _upRight = new hStickDiagonal (gamepadIndex, stickIndex, name, fullName, "UpRight", 45);
			return _upRight;
		} 
	}

	public hStickDiagonal rightDown { get { return downRight; } }
	public hStickDiagonal downRight { 
		get {
			if (_downRight == null) _downRight = new hStickDiagonal (gamepadIndex, stickIndex, name, fullName, "DownRight", -45);
			return _downRight;
		} 
	}

	
	// --------------------
	// BUILD ALL
	// --------------------

	public void BuildAll () {
		int indices = up.gamepadIndex;
		indices = down.gamepadIndex;
		indices = left.gamepadIndex;
		indices = right.gamepadIndex;
		indices = upLeft.gamepadIndex;
		indices = upRight.gamepadIndex;
		indices = downLeft.gamepadIndex;
		indices = downRight.gamepadIndex;
	}
	

	
	// --------------------
	// UPDATE
	// --------------------
	
	public void Update () {
		UpdatePositionRaw ();
		UpdatePosition ();

		if ((hAbstractStickDirection)_up != null) _up.Update();
		if ((hAbstractStickDirection)_down != null) _down.Update();
		if ((hAbstractStickDirection)_left != null) _left.Update();
		if ((hAbstractStickDirection)_right != null) _right.Update();
		
		if ((hAbstractStickDirection)_upLeft != null) _upLeft.Update();
		if ((hAbstractStickDirection)_downLeft != null) _downLeft.Update();
		if ((hAbstractStickDirection)_upRight != null) _upRight.Update();
		if ((hAbstractStickDirection)_downRight != null) _downRight.Update();
	}

	private void UpdatePositionRaw() {
		_horizontalRaw = right.positionRaw;
		_verticalRaw = up.positionRaw;
	}

	private void UpdatePosition() {
	}

	
	// --------------------
	// POSITION RAW
	// --------------------

	private float _horizontalRaw;
	public float horizontalRaw { get { return _horizontalRaw; } }

	private float _verticalRaw;
	public float verticalRaw { get { return _verticalRaw; } }

	public Vector2 positionRaw { get { return new Vector2 (horizontalRaw, verticalRaw); } }

	
	// --------------------
	// PUBLIC PROPERTIES
	// --------------------

	public float distanceRaw { get { return positionRaw.magnitude; } }
	public bool inDeadZone { get { return distanceRaw < hInput.deadZone; } }

	private Vector2 _position;
	private float _positionDate;
	public Vector2 position { 
		get {
			float time = Time.time;
			if (time == 0 || _positionDate != time) {
				if (inDeadZone) _position = Vector2.zero;
				else {
					Vector2 deadZonedPos = ((positionRaw - positionRaw.normalized*hInput.deadZone)/(1 - hInput.deadZone));
					_position = new Vector2 (Mathf.Clamp(deadZonedPos.x, -1, 1), Mathf.Clamp(deadZonedPos.y, -1, 1));
				}
				_positionDate = time;
			}
			return _position; 
		} 
	}

	public float horizontal { get { return position.x; } }
	public float vertical { get { return position.y; } }
	public float distance { get { return Mathf.Clamp01((1 + hInput.diagonalIncrease)*position.magnitude); } }
	public bool inTriggerZone { get { return distance >= hInput.triggerZone; } }

	public float angleRaw { get { return Vector2.SignedAngle(Vector2.right, positionRaw); } }
	public float angle { get { return Vector2.SignedAngle(Vector2.right, position); } }


	public Vector3 worldPositionCamera { get { return (hInput.worldCamera.right*horizontal + hInput.worldCamera.up*vertical); } }
	public Vector3 worldPositionFlat { get { return new Vector3 (horizontal, 0, vertical); } }

	public Vector3 worldPositionCameraRaw { get { return (hInput.worldCamera.right*horizontalRaw + hInput.worldCamera.up*verticalRaw); } }
	public Vector3 worldPositionFlatRaw { get { return new Vector3 (horizontalRaw, 0, verticalRaw); } }
}