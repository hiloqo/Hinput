using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// hinput class representing a gamepad stick, such as the left stick, the right stick, or the D-pad.<br/><br/>
/// If no property of the hStick is used, it will automatically be cast to a Vector2 with the value (position). 
/// For instance, (hinput.gamepad[0].leftStick) will return (hinput.gamepad[0].leftStick.position).
/// </summary>
public class hStick {
	// --------------------
	// NAME
	// --------------------

	private string _name;
	/// <summary>
	/// Returns the name of the stick, like “LeftStick” or “DPad”.
	/// </summary>
	public string name { get { return _name; } }

	private string _fullName;
	/// <summary>
	/// Returns the full name of the stick, like “Mac_Gamepad2_RightStick”<br/><br/>
	/// Note : the number at the end of the gamepad’s name is the one used by Unity, not by hinput. 
	/// It is NOT equal to (index), but to (index)+1.
	/// </summary>
	public string fullName { get { return _fullName; } }

	private int _gamepadIndex;
	/// <summary>
	/// Returns the index of the gamepad this stick is attached to.
	/// </summary>
	public int gamepadIndex { get { return _gamepadIndex; } }

	/// <summary>
	/// Returns the gamepad this stick is attached to.
	/// </summary>
	public hGamepad gamepad { 
		get { 
			if (gamepadIndex >= 0) return hinput.gamepad[gamepadIndex]; 
			else return hinput.anyGamepad;
		} 
	}

	private int _index;
	/// <summary>
	/// Returns the index of the stick on its gamepad (0 for a left stick, 1 for a right stick, 2 for a D-pad).
	/// </summary>
	public int index { get { return _index; } }

	
	// --------------------
	// IMPLICIT CONVERSION
	// --------------------

	public static implicit operator Vector2 (hStick hStick) { return hStick.position; }


	// --------------------
	// CONSTRUCTORS
	// --------------------

	// For sticks
	public hStick (string name, hGamepad gamepad, int index) {
		this._name = name;
		this._gamepadIndex = gamepad.index;
		this._fullName = gamepad.fullName+"_"+name;
		this._index = index;

		horizontalAxis = new hAxis (fullName+"_Horizontal");
		verticalAxis = new hAxis (fullName+"_Vertical");
	}

	// For the D-pad
	public hStick (string name, hGamepad gamepad) {
		this._name = name;
		this._gamepadIndex = gamepad.index;
		this._fullName = gamepad.fullName+"_"+name;
		this._index = 2;

		horizontalAxis = new hAxis (fullName+"_Horizontal", fullName+"_Left", fullName+"_Right");
		verticalAxis = new hAxis (fullName+"_Vertical", fullName+"_Down", fullName+"_Up");
	}


	// --------------------
	// AXES
	// --------------------

	private hAxis horizontalAxis;
	private hAxis verticalAxis;


	// --------------------
	// DIRECTIONS
	// --------------------

	private hDirection _up;
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a 90 degree angle with the horizontal axis.
	/// </summary>
	public hDirection up { 
		get {
			if (_up == null) _up = new hDirection ("Up", 90, this);
			return _up;
		} 
	}

	private hDirection _down;
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a -90 degree angle with the horizontal axis.
	/// </summary>
	public hDirection down { 
		get {
			if (_down == null) _down = new hDirection ("Down", -90, this);
			return _down;
		} 
	}

	private hDirection _left;
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a 180 degree angle with the horizontal axis.
	/// </summary>
	public hDirection left { 
		get {
			if (_left == null) _left = new hDirection ("Left", 180, this);
			return _left;
		} 
	}

	private hDirection _right;
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along the horizontal axis.
	/// </summary>
	public hDirection right { 
		get {
			if (_right == null) _right = new hDirection ("Right", 0, this);
			return _right;
		} 
	}

	
	private hDirection _upLeft;
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a 135 degree angle with the horizontal axis.
	/// </summary>
	public hDirection leftUp { get { return upLeft; } }
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a 135 degree angle with the horizontal axis.
	/// </summary>
	public hDirection upLeft { 
		get {
			if (_upLeft == null) _upLeft = new hDirection ("UpLeft", 135, this);
			return _upLeft;
		} 
	}

	private hDirection _downLeft;
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a -135 degree angle with the horizontal axis.
	/// </summary>
	public hDirection leftDown { get { return downLeft; } }
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a -135 degree angle with the horizontal axis.
	/// </summary>
	public hDirection downLeft { 
		get {
			if (_downLeft == null) _downLeft = new hDirection ("DownLeft", -135, this);
			return _downLeft;
		} 
	}

	private hDirection _upRight;
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a 45 degree angle with the horizontal axis.
	/// </summary>
	public hDirection rightUp { get { return upRight; } }
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a 45 degree angle with the horizontal axis.
	/// </summary>
	public hDirection upRight { 
		get {
			if (_upRight == null) _upRight = new hDirection ("UpRight", 45, this);
			return _upRight;
		} 
	}

	private hDirection _downRight;
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a -45 degree angle with the horizontal axis.
	/// </summary>
	public hDirection rightDown { get { return downRight; } }
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a -45 degree angle with the horizontal axis.
	/// </summary>
	public hDirection downRight { 
		get {
			if (_downRight == null) _downRight = new hDirection ("DownRight", -45, this);
			return _downRight;
		} 
	}

	/// <summary>
	/// Please never call that.
	/// </summary>
	public void BuildDirections () {
		if (up.gamepadIndex == 0
		|| down.gamepadIndex == 0
		|| left.gamepadIndex == 0
		|| right.gamepadIndex == 0
		|| upLeft.gamepadIndex == 0
		|| upRight.gamepadIndex == 0
		|| downLeft.gamepadIndex == 0
		|| downRight.gamepadIndex == 0) {
			// Do nothing, I'm just looking them up so that they are assigned.
		}
	}

	private void UpdateDirections () {
		if ((hDirection)_up != null) _up.Update();
		if ((hDirection)_down != null) _down.Update();
		if ((hDirection)_left != null) _left.Update();
		if ((hDirection)_right != null) _right.Update();
		
		if ((hDirection)_upLeft != null) _upLeft.Update();
		if ((hDirection)_downLeft != null) _downLeft.Update();
		if ((hDirection)_upRight != null) _upRight.Update();
		if ((hDirection)_downRight != null) _downRight.Update();
	}
	

	
	// --------------------
	// UPDATE
	// --------------------
	
	/// <summary>
	/// Please never call that.
	/// </summary>
	public void Update () {
		UpdateAxes ();
		UpdateDirections ();
	}

	
	// --------------------
	// PUBLIC PROPERTIES - RAW
	// --------------------

	private void UpdateAxes () {
		_horizontalRaw = horizontalAxis.positionRaw;
		_verticalRaw = verticalAxis.positionRaw;
	}

	private float _horizontalRaw;
	/// <summary>
	/// Returns the x coordinate of the stick. The dead zone is not taken into account.
	/// </summary>
	public float horizontalRaw { get { return _horizontalRaw; } }

	private float _verticalRaw;
	/// <summary>
	/// Returns the y coordinate of the stick. The dead zone is not taken into account.
	/// </summary>
	public float verticalRaw { get { return _verticalRaw; } }

	/// <summary>
	/// Returns the coordinates of the stick in the shape of a Vector2. The dead zone is not taken into account.
	/// </summary>
	public Vector2 positionRaw { get { return new Vector2 (horizontalRaw, verticalRaw); } }

	private float _distanceRaw;
	private float _distanceRawDate;
	/// <summary>
	/// Returns the current distance of the stick to its origin. The dead zone is not taken into account.
	/// </summary>
	public float distanceRaw { 
		get { 
			float time = Time.time;
			if (time == 0 || _distanceRawDate != time) {
				_distanceRaw = positionRaw.magnitude;
				_distanceRawDate = time;
			}
			return _distanceRaw; 
		} 
	}

	private float _angleRaw;
	private float _angleRawDate;
	/// <summary>
	/// Returns the value of the angle between the current position of the stick and the horizontal axis 
	/// (In degrees : left=180, up=90, right=0, down=-90). 
	/// The dead zone is not taken into account.
	/// </summary>
	public float angleRaw { 
		get { 
			float time = Time.time;
			if (time == 0 || _angleRawDate != time) {
				_angleRaw = Vector2.SignedAngle(Vector2.right, positionRaw);
				_angleRawDate = time;
			}
			return _angleRaw;
		} 
	}

	/// <summary>
	/// Returns the coordinates of the stick as a Vector3 facing (hinput.worldCamera). 
	/// The stick’s horizontal and vertical axes are interpreted as the camera’s right and up directions. 
	/// The dead zone is not taken into account.
	/// </summary>
	public Vector3 worldPositionCameraRaw { get { return (hSettings.worldCamera.right*horizontalRaw + hSettings.worldCamera.up*verticalRaw); } }

	/// <summary>
	/// Returns the coordinates of the stick as a Vector3 with a y value of 0. 
	/// The stick’s horizontal and vertical axes are interpreted as the absolute right and forward directions. 
	/// The dead zone is not taken into account.
	/// </summary>
	public Vector3 worldPositionFlatRaw { get { return new Vector3 (horizontalRaw, 0, verticalRaw); } }

	
	// --------------------
	// PUBLIC PROPERTIES - DEADZONED
	// --------------------

	/// <summary>
	/// Returns true if the current position of the stick is within a distance of (hinput.deadZone) of its origin. 
	/// Returns false otherwise.
	/// </summary>
	public bool inDeadZone { get { return distanceRaw < hSettings.deadZone; } }

	private Vector2 _position;
	private float _positionDate;
	/// <summary>
	/// Returns the coordinates of the stick in the shape of a Vector2.
	/// </summary>
	public Vector2 position { 
		get {
			float time = Time.time;
			if (time == 0 || _positionDate != time) {
				if (inDeadZone) _position = Vector2.zero;
				else {
					Vector2 deadZonedPos = ((1 + hUtils.distanceIncrease)*
						(positionRaw - positionRaw.normalized*hSettings.deadZone)/(1 - hSettings.deadZone));
					_position = new Vector2 (Mathf.Clamp(deadZonedPos.x, -1, 1), Mathf.Clamp(deadZonedPos.y, -1, 1));
				}
				_positionDate = time;
			}
			return _position; 
		} 
	}

	/// <summary>
	/// Returns the x coordinate of the stick.
	/// </summary>
	public float horizontal { get { return position.x; } }

	/// <summary>
	/// Returns the y coordinate of the stick.
	/// </summary>
	public float vertical { get { return position.y; } }

	private float _distance;
	private float _distanceDate;
	/// <summary>
	/// Returns the current distance of the stick to its origin.
	/// </summary>
	public float distance { 
		get { 
			float time = Time.time;
			if (time == 0 || _distanceDate != time) {
				_distance = Mathf.Clamp01(position.magnitude);
				_distanceDate = time;
			}
			return _distance; 
		} 
	}

	/// <summary>
	/// Returns true if the current position of the stick is beyond a distance of (hinput.triggerZone) of its origin. 
	/// Returns false otherwise.
	/// </summary>
	public bool inTriggerZone { get { return distance >= hSettings.triggerZone; } }

	private float _angle;
	private float _angleDate;
	/// <summary>
	/// Returns the value of the angle between the current position of the stick and the horizontal axis 
	/// (In degrees : left=180, up=90, right=0, down=-90).
	/// </summary>
	public float angle { 
		get { 
			float time = Time.time;
			if (time == 0 || _angleDate != time) {
				_angle = Vector2.SignedAngle(Vector2.right, position);
				_angleDate = time;
			}
			return _angle;
		} 
	}

	/// <summary>
	/// Returns the coordinates of the stick as a Vector3 facing (hinput.worldCamera). 
	/// The stick’s horizontal and vertical axes are interpreted as the camera’s right and up directions.
	/// </summary>
	public Vector3 worldPositionCamera { get { return (hSettings.worldCamera.right*horizontal + hSettings.worldCamera.up*vertical); } }

	/// <summary>
	/// Returns the coordinates of the stick as a Vector3 with a y value of 0. 
	/// The stick’s horizontal and vertical axes are interpreted as the absolute right and forward directions.
	/// </summary>
	public Vector3 worldPositionFlat { get { return new Vector3 (horizontal, 0, vertical); } }
}