using UnityEngine;

/// <summary>
/// hinput class representing a gamepad stick, such as the left stick, the right stick, or the D-pad.<br/>
/// If no property of the hStick is used, it will automatically be cast to a Vector2 with the value position. 
/// For instance, hinput.gamepad[0].leftStick will return hinput.gamepad[0].leftStick.position.
/// </summary>
public class hStick {
	// --------------------
	// NAME
	// --------------------

	/// <summary>
	/// Returns the name of the stick, like “LeftStick” or “DPad”.
	/// </summary>
	public string name { get; }

	/// <summary>
	/// Returns the full name of the stick, like “Mac_Gamepad2_RightStick”
	/// </summary>
	public string fullName { get; }

	/// <summary>
	/// Returns the index of the gamepad this stick is attached to.
	/// </summary>
	public int gamepadIndex { get; }

	/// <summary>
	/// Returns the gamepad this stick is attached to.
	/// </summary>
	public hGamepad gamepad { 
		get { 
			if (gamepadIndex >= 0) return hinput.gamepad[gamepadIndex]; 
			else return hinput.anyGamepad;
		} 
	}

	/// <summary>
	/// Returns the index of the stick on its gamepad (0 for a left stick, 1 for a right stick, 2 for a D-pad).
	/// </summary>
	public int index { get; }

	
	// --------------------
	// IMPLICIT CONVERSION
	// --------------------

	public static implicit operator Vector2 (hStick hStick) { return hStick.position; }
	public static implicit operator hPressable (hStick hStick) { return hStick.inPressedZone; }


	// --------------------
	// PRIVATE VARIABLES
	// --------------------


	/// <summary>
	/// Returns true if the current position of the stick is beyond a distance of hSettings.stickPressedZone of its origin. 
	/// Returns false otherwise.
	/// </summary>
	public readonly hStickPressedZone inPressedZone;


	// --------------------
	// CONSTRUCTORS
	// --------------------

	// For sticks
	public hStick (string name, hGamepad gamepad, int index) {
		this.name = name;
		gamepadIndex = gamepad.index;
		fullName = gamepad.fullName+"_"+name;
		this.index = index;
		
		inPressedZone = new hStickPressedZone("PressedZone", this);

		horizontalAxis = new hAxis (fullName+"_Horizontal");
		verticalAxis = new hAxis (fullName+"_Vertical");
	}

	// For the D-pad
	public hStick (string name, hGamepad gamepad) {
		this.name = name;
		gamepadIndex = gamepad.index;
		fullName = gamepad.fullName+"_"+name;
		index = 2;
		
		inPressedZone = new hStickPressedZone("pressedZone", this);

		horizontalAxis = new hAxis (fullName+"_Horizontal", fullName+"_Left", fullName+"_Right");
		verticalAxis = new hAxis (fullName+"_Vertical", fullName+"_Down", fullName+"_Up");
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
		_horizontalRaw = horizontalAxis.positionRaw;
		_verticalRaw = verticalAxis.positionRaw;
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
	/// Returns the coordinates of the stick as a Vector3 facing hSettings.worldCamera. 
	/// The stick’s horizontal and vertical axes are interpreted as the camera’s right and up directions. 
	/// The dead zone is not taken into account.
	/// </summary>
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
	/// Returns true if the current position of the stick is within a distance of hSettings.stickDeadZone of its origin. 
	/// Returns false otherwise.
	/// </summary>
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
	/// Returns the coordinates of the stick as a Vector3 facing hSettings.worldCamera. 
	/// The stick’s horizontal and vertical axes are interpreted as the camera’s right and up directions.
	/// </summary>
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