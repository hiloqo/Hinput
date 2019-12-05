using UnityEngine;

/// <summary>
/// hinput class representing a gamepad stick, such as the left stick, the right stick, or the D-pad.<br/>
/// If no property of the hStick is used, it will automatically be cast to a Vector2 with the value position. 
/// For instance, hinput.gamepad[0].leftStick will return hinput.gamepad[0].leftStick.position.
/// </summary>
public class hStick {
	// --------------------
	// ID
	// --------------------

	/// <summary>
	/// Returns the index of a stick on its gamepad (0 for a left stick, 1 for a right stick, 2 for a D-pad).
	/// </summary>
	public int index { get; }

	/// <summary>
	/// Returns the name of a stick, like “LeftStick” or “DPad”.
	/// </summary>
	public string name { get; }

	/// <summary>
	/// Returns the real full name of a stick, like "Linux_Gamepad4_DPad".
	/// </summary>
	/// <remarks>
	/// If this stick is attached to anyGamepad, returns something like "Linux_AnyGamepad_DPad".
	/// </remarks>
	public readonly string internalFullName;

	/// <summary>
	/// Returns the full name of a stick, like "Linux_Gamepad4_DPad".
	/// </summary>
	/// <remarks>
	/// If this stick is attached to anyGamepad, returns its full name on the gamepad that is currently being pressed.
	/// </remarks>
	public string fullName { get { return gamepad.fullName + "_" + name; } }

	/// <summary>
	/// Returns the real gamepad a stick is attached to.
	/// </summary>
	/// <remarks>
	/// If this stick is attached to anyGamepad, returns anyGamepad.
	/// </remarks>
	public readonly hGamepad internalGamepad;

	/// <summary>
	/// Returns the gamepad a stick is attached to.
	/// </summary>
	/// <remarks>
	/// If this stick is attached to anyGamepad, returns the gamepad that is currently being pressed.
	/// </remarks>
	public virtual hGamepad gamepad { get { return internalGamepad; } }
	
	/// <summary>
	/// Returns the real full name of the real gamepad a stick is attached to.
	/// </summary>
	/// <remarks>
	/// If this stick is attached to anyGamepad, returns something like "Linux_AnyGamepad"
	/// </remarks>
	public string internalGamepadFullName { get { return internalGamepad.internalFullName; } }
	
	/// <summary>
	/// Returns the full name of the gamepad a stick is attached to.
	/// </summary>
	/// <remarks>
	/// If this stick is attached to anyGamepad, returns the full name of the gamepad that is currently being pressed.
	/// </remarks>
	public string gamepadFullName { get { return gamepad.fullName; } }
	
	/// <summary>
	/// Returns the real index of the real gamepad a stick is attached to.
	/// </summary>
	/// <remarks>
	/// If this stick is attached to anyGamepad, returns -1.
	/// </remarks>
	public int internalGamepadIndex { get { return internalGamepad.internalIndex; } }

	/// <summary>
	/// Returns the index of the gamepad a stick is attached to.
	/// </summary>
	/// <remarks>
	/// If this stick is attached to anyGamepad, returns the index of the gamepad that is currently being pressed.
	/// </remarks>
	public int gamepadIndex { get { return gamepad.index; } }

	
	// --------------------
	// IMPLICIT CONVERSION
	// --------------------

	public static implicit operator Vector2 (hStick hStick) { return hStick.position; }
	public static implicit operator hPressable (hStick hStick) { return hStick.inPressedZone; }
	public static implicit operator hStick (hStickPressedZone hStickPressedZone) { return hStickPressedZone.stick; }


	// --------------------
	// PRIVATE VARIABLES
	// --------------------


	/// <summary>
	/// Returns true if the current position of the stick is beyond the limit of its pressed zone. 
	/// Returns false otherwise.
	/// </summary>
	/// <remarks>
	/// The size of the pressed zone of the sticks can be changed with the stickPressedZone property of hSettings.
	/// </remarks>
	public readonly hStickPressedZone inPressedZone;


	// --------------------
	// CONSTRUCTORS
	// --------------------

	public hStick(string name, hGamepad internalGamepad, int index) 
		: this(name, internalGamepad, index, false) { }

	protected hStick (string name, hGamepad internalGamepad, int index, bool isAnyGamepad) {
		this.name = name;
		internalFullName = internalGamepad.internalFullName + "_" + name;
		this.internalGamepad = internalGamepad;
		this.index = index;

		inPressedZone = new hStickPressedZone("PressedZone", this);

		if (isAnyGamepad) return; // Axes are unnecessary for anyGamepad
		
		if (index == 0 || index == 1) { // Sticks
			horizontalAxis = new hAxis (internalFullName+"_Horizontal");
			verticalAxis = new hAxis (internalFullName+"_Vertical");
		}

		if (index == 2) { // DPad
			horizontalAxis = new hAxis (internalFullName+"_Horizontal", 
				internalFullName+"_Right", 
				internalFullName+"_Left");
			verticalAxis = new hAxis (internalFullName+"_Vertical", 
				internalFullName+"_Up", 
				internalFullName+"_Down");
		}
	}
	

	
	// --------------------
	// UPDATE
	// --------------------
	
	public void Update () {
		if (inPressedZone != null) inPressedZone.Update();
		UpdateAxes ();
		UpdateDirections ();
	}


	// --------------------
	// AXES
	// --------------------

	private readonly hAxis horizontalAxis;
	private readonly hAxis verticalAxis;

	private void UpdateAxes () {
		if (horizontalAxis == null || verticalAxis == null) return;
		
		horizontalRaw = horizontalAxis.positionRaw;
		verticalRaw = verticalAxis.positionRaw;
	}


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
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a 135 degree
	/// angle with the horizontal axis.
	/// </summary>
	public hDirection leftUp { get { return upLeft; } }
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a 135 degree
	/// angle with the horizontal axis.
	/// </summary>
	public hDirection upLeft { 
		get {
			if (_upLeft == null) _upLeft = new hDirection ("UpLeft", 135, this);
			return _upLeft;
		} 
	}

	private hDirection _downLeft;
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a -135 degree
	/// angle with the horizontal axis.
	/// </summary>
	public hDirection leftDown { get { return downLeft; } }
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a -135 degree
	/// angle with the horizontal axis.
	/// </summary>
	public hDirection downLeft { 
		get {
			if (_downLeft == null) _downLeft = new hDirection ("DownLeft", -135, this);
			return _downLeft;
		} 
	}

	private hDirection _upRight;
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a 45 degree
	/// angle with the horizontal axis.
	/// </summary>
	public hDirection rightUp { get { return upRight; } }
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a 45 degree
	/// angle with the horizontal axis.
	/// </summary>
	public hDirection upRight { 
		get {
			if (_upRight == null) _upRight = new hDirection ("UpRight", 45, this);
			return _upRight;
		} 
	}

	private hDirection _downRight;
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a -45 degree
	/// angle with the horizontal axis.
	/// </summary>
	public hDirection rightDown { get { return downRight; } }
	/// <summary>
	/// Returns a virtual button defined by the stick’s projected position along a direction that has a -45 degree
	/// angle with the horizontal axis.
	/// </summary>
	public hDirection downRight { 
		get {
			if (_downRight == null) _downRight = new hDirection ("DownRight", -45, this);
			return _downRight;
		} 
	}

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
		if (_up != null) _up.Update();
		if (_down != null) _down.Update();
		if (_left != null) _left.Update();
		if (_right != null) _right.Update();
		
		if (_upLeft != null) _upLeft.Update();
		if (_downLeft != null) _downLeft.Update();
		if (_upRight != null) _upRight.Update();
		if (_downRight != null) _downRight.Update();
	}

	
	// --------------------
	// PUBLIC PROPERTIES - RAW
	// --------------------

	/// <summary>
	/// Returns the x coordinate of the stick. The dead zone is not taken into account.
	/// </summary>
	public virtual float horizontalRaw { get; private set; }

	/// <summary>
	/// Returns the y coordinate of the stick. The dead zone is not taken into account.
	/// </summary>
	public virtual float verticalRaw { get; private set; }

	/// <summary>
	/// Returns the coordinates of the stick. The dead zone is not taken into account.
	/// </summary>
	public Vector2 positionRaw { get { return new Vector2 (horizontalRaw, verticalRaw); } }

	private float _distanceRaw;
	private float _distanceRawDate;
	/// <summary>
	/// Returns the current distance of the stick to its origin. The dead zone is not taken into account.
	/// </summary>
	public float distanceRaw { 
		get { 
			float time = Time.unscaledTime;
			if (time > 0 && time.IsEqualTo(_distanceRawDate)) return _distanceRaw;
			
			_distanceRaw = positionRaw.magnitude;
			_distanceRawDate = time;
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
			float time = Time.unscaledTime;
			if (time > 0 && time.IsEqualTo(_angleRawDate)) return _angleRaw;
			
			_angleRaw = Vector2.SignedAngle(Vector2.right, positionRaw);
			_angleRawDate = time;
			return _angleRaw;
		} 
	}

	/// <summary>
	/// Returns the coordinates of the stick as a Vector3 facing the camera. 
	/// The stick’s horizontal and vertical axes are interpreted as the camera’s right and up directions. 
	/// The dead zone is not taken into account.
	/// </summary>
	/// <remarks>
	/// The camera that is being used can be changed with the worldCamera property of hSettings.
	/// </remarks>
	public Vector3 worldPositionCameraRaw  { 
		get { 
			try { return (hSettings.worldCamera.right*horizontalRaw + hSettings.worldCamera.up*verticalRaw); }
			catch { 
				Debug.LogError ("hinput error : No camera found !");
				return Vector2.zero;
			}
		} 
	}

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
	/// Returns true if the current position of the stick is within the limit of its dead zone. 
	/// Returns false otherwise.
	/// </summary>
	/// <remarks>
	/// The size of the dead zone of the sticks can be changed with the stickDeadZone property of hSettings.
	/// </remarks>
	public bool inDeadZone { get { return distanceRaw < hSettings.stickDeadZone; } }

	private Vector2 _position;
	private float _positionDate;
	/// <summary>
	/// Returns the coordinates of the stick.
	/// </summary>
	public Vector2 position {
		get {
			float time = Time.unscaledTime;
			if (time > 0 && time.IsEqualTo(_positionDate)) return _position;
			
			if (inDeadZone) _position = Vector2.zero;
			else {
				Vector2 deadZonedPos = (1 + hUtils.distanceIncrease)/(1 - hSettings.stickDeadZone)*
					(positionRaw - positionRaw.normalized*hSettings.stickDeadZone);
				
				_position = new Vector2 (
					Mathf.Clamp(deadZonedPos.x, -1, 1), 
					Mathf.Clamp(deadZonedPos.y, -1, 1));
			}
			_positionDate = time;
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
			float time = Time.unscaledTime;
			if (time > 0 && time.IsEqualTo(_distanceDate)) return _distance;
			
			_distance = Mathf.Clamp01(position.magnitude);
			_distanceDate = time;
			return _distance; 
		} 
	}

	private float _angle;
	private float _angleDate;
	/// <summary>
	/// Returns the value of the angle between the current position of the stick and the horizontal axis 
	/// (In degrees : left=180, up=90, right=0, down=-90).
	/// </summary>
	public float angle { 
		get { 
			float time = Time.unscaledTime;
			if (time > 0 && time.IsEqualTo(_angleDate)) return _angle;
			
			_angle = Vector2.SignedAngle(Vector2.right, position);
			_angleDate = time;
			return _angle;
		} 
	}

	/// <summary>
	/// Returns the coordinates of the stick as a Vector3 facing the camera. 
	/// The stick’s horizontal and vertical axes are interpreted as the camera’s right and up directions.
	/// </summary>
	/// <remarks>
	/// The camera that is being used can be changed with the worldCamera property of hSettings.
	/// </remarks>
	public Vector3 worldPositionCamera { 
		get { 
			try { return (hSettings.worldCamera.right*horizontal + hSettings.worldCamera.up*vertical); }
			catch { 
				Debug.LogError ("hinput error : No camera found !");
				return Vector2.zero;
			}
		} 
	}

	/// <summary>
	/// Returns the coordinates of the stick as a Vector3 with a y value of 0. 
	/// The stick’s horizontal and vertical axes are interpreted as the absolute right and forward directions.
	/// </summary>
	public Vector3 worldPositionFlat { get { return new Vector3 (horizontal, 0, vertical); } }
}