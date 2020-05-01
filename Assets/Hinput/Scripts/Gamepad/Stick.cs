using UnityEngine;
using HinputClasses.Internal;

namespace HinputClasses {
	/// <summary>
	/// Hinput class representing a gamepad stick, such as the left stick, the right stick, or the D-pad.<br/><br/>
	/// If no property of the Stick is used, it will automatically be cast to a Vector2 with the value position. 
	/// For instance, Hinput.gamepad[0].leftStick will return Hinput.gamepad[0].leftStick.position.
	/// </summary>
	public class Stick {
		// --------------------
		// ID
		// --------------------

		/// <summary>
		/// The index of a stick on its gamepad (0 for a left stick, 1 for a right stick, 2 for a D-pad).
		/// </summary>
		public readonly int index;

		/// <summary>
		/// The name of a stick, like “LeftStick” or “DPad”.
		/// </summary>
		public readonly string name;

		/// <summary>
		/// The full name of a stick, like "Mac_Gamepad0_LeftStick" or "Windows_AnyGamepad_DPad".
		/// </summary>
		public readonly string fullName;
		
		/// <summary>
		/// The gamepad a stick is attached to.
		/// </summary>
		public readonly Gamepad gamepad;
		
		
		// --------------------
		// ENABLED
		// --------------------
		
		/// <summary>
		/// Returns true if a stick is being tracked by Hinput. Returns false otherwise.
		/// </summary>
		public bool isEnabled { get; private set; }
		
		/// <summary>
		/// Enable a stick so that Hinput starts tracking it.
		/// </summary>
		public void Enable() {
			isEnabled = true;
		}

		/// <summary>
		/// Reset and disable a stick so that Hinput stops tracking it. This may improve performances.
		/// </summary>
		public void Disable() {
			Reset();
			isEnabled = false;
		}

		/// <summary>
		/// Reset the position of a stick and erase its history.
		/// </summary>
		public void Reset() {
			horizontalRaw = 0;
			verticalRaw = 0;
			
			up.Reset();
			down.Reset();
			left.Reset();
			right.Reset();
			upLeft.Reset();
			downLeft.Reset();
			upRight.Reset();
			downRight.Reset();
			inPressedZone.Reset();
		}

		
		// --------------------
		// IMPLICIT CONVERSION
		// --------------------

		public static implicit operator Vector2 (Stick stick) { return stick.position; }
		public static implicit operator Pressable (Stick stick) { return stick.inPressedZone; }
		public static implicit operator bool (Stick stick) { return stick.inPressedZone; }


		// --------------------
		// CONSTRUCTORS
		// --------------------

		public Stick(string name, Gamepad gamepad, int index, bool isEnabled) {
			this.name = name;
			fullName = gamepad.fullName + "_" + name;
			this.gamepad = gamepad;
			this.index = index;
			this.isEnabled = isEnabled;

			up = new Direction ("Up", 90, this);
			down = new Direction ("Down", -90, this);
			left = new Direction ("Left", 180, this);
			right = new Direction ("Right", 0, this);
			upLeft = new Direction ("UpLeft", 135, this);
			downLeft = new Direction ("DownLeft", -135, this);
			upRight = new Direction ("UpRight", 45, this);
			downRight = new Direction ("DownRight", -45, this);
			inPressedZone = new StickPressedZone("PressedZone", this);
			
			if (index == 0 || index == 1) { // Sticks
				horizontalAxis = new Axis (fullName+"_Horizontal");
				verticalAxis = new Axis (fullName+"_Vertical");
			}
			if (index == 2) { // DPad
				horizontalAxis = new Axis (fullName+"_Horizontal", 
					fullName+"_Right", 
					fullName+"_Left");
				verticalAxis = new Axis (fullName+"_Vertical", 
					fullName+"_Up", 
					fullName+"_Down");
			}
		}


		// --------------------
		// UPDATE
		// --------------------
		
		public void Update () {
			if (!isEnabled) return;
			
			UpdateAxes ();
			UpdateDirections ();
			inPressedZone.Update();
		}


		// --------------------
		// AXES
		// --------------------

		private readonly Axis horizontalAxis;
		private readonly Axis verticalAxis;

		private void UpdateAxes () {
			if (horizontalAxis == null || verticalAxis == null) return;
			
			horizontalRaw = horizontalAxis.positionRaw;
			verticalRaw = verticalAxis.positionRaw;
		}


		// --------------------
		// DIRECTIONS
		// --------------------

		/// <summary>
		/// A virtual button defined by the stick’s projected position along a direction that has a 90 degree
		/// angle with the horizontal axis.
		/// </summary>
		public readonly Direction up;

		/// <summary>
		/// A virtual button defined by the stick’s projected position along a direction that has a -90 degree
		/// angle with the horizontal axis.
		/// </summary>
		public readonly Direction down;

		/// <summary>
		/// A virtual button defined by the stick’s projected position along a direction that has a 180 degree
		/// angle with the horizontal axis.
		/// </summary>
		public readonly Direction left;

		/// <summary>
		/// A virtual button defined by the stick’s projected position along the horizontal axis.
		/// </summary>
		public readonly Direction right;

		/// <summary>
		/// A virtual button defined by the stick’s projected position along a direction that has a 135 degree
		/// angle with the horizontal axis.
		/// </summary>
		public readonly Direction upLeft;
		/// <summary>
		/// A virtual button defined by the stick’s projected position along a direction that has a 135 degree
		/// angle with the horizontal axis.
		/// </summary>
		public Direction leftUp { get { return upLeft; } }

		/// <summary>
		/// A virtual button defined by the stick’s projected position along a direction that has a -135 degree
		/// angle with the horizontal axis.
		/// </summary>
		public readonly Direction downLeft;
		/// <summary>
		/// A virtual button defined by the stick’s projected position along a direction that has a -135 degree
		/// angle with the horizontal axis.
		/// </summary>
		public Direction leftDown { get { return downLeft; } }

		/// <summary>
		/// A virtual button defined by the stick’s projected position along a direction that has a 45 degree
		/// angle with the horizontal axis.
		/// </summary>
		public readonly Direction upRight;
		/// <summary>
		/// A virtual button defined by the stick’s projected position along a direction that has a 45 degree
		/// angle with the horizontal axis.
		/// </summary>
		public Direction rightUp { get { return upRight; } }

		/// <summary>
		/// A virtual button defined by the stick’s projected position along a direction that has a -45 degree
		/// angle with the horizontal axis.
		/// </summary>
		public readonly Direction downRight;
		/// <summary>
		/// A virtual button defined by the stick’s projected position along a direction that has a -45 degree
		/// angle with the horizontal axis.
		/// </summary>
		public Direction rightDown { get { return downRight; } }

		private void UpdateDirections () {
			up.Update();
			down.Update();
			left.Update();
			right.Update();
			
			upLeft.Update();
			downLeft.Update();
			upRight.Update();
			downRight.Update();
		}

		
		// --------------------
		// PUBLIC PROPERTIES - RAW
		// --------------------

		/// <summary>
		/// The x coordinate of the stick. The dead zone is not taken into account.
		/// </summary>
		public virtual float horizontalRaw { get; private set; }

		/// <summary>
		/// The y coordinate of the stick. The dead zone is not taken into account.
		/// </summary>
		public virtual float verticalRaw { get; private set; }

		/// <summary>
		/// The coordinates of the stick. The dead zone is not taken into account.
		/// </summary>
		public Vector2 positionRaw { get { return new Vector2 (horizontalRaw, verticalRaw); } }

		private float _distanceRaw;
		private int _lastDistanceRawUpdateFrame = -1;
		/// <summary>
		/// The current distance of the stick to its origin. The dead zone is not taken into account.
		/// </summary>
		public float distanceRaw { 
			get { 
				if (_lastDistanceRawUpdateFrame == Time.frameCount) return _distanceRaw;
				
				_distanceRaw = positionRaw.magnitude;
				_lastDistanceRawUpdateFrame = Time.frameCount;
				return _distanceRaw; 
			} 
		}

		private float _angleRaw;
		private int _lastAngleRawUpdateFrame = -1;
		/// <summary>
		/// The value of the angle between the current position of the stick and the horizontal axis 
		/// (In degrees : left=180, up=90, right=0, down=-90). 
		/// The dead zone is not taken into account.
		/// </summary>
		public float angleRaw { 
			get { 
				if (_lastAngleRawUpdateFrame == Time.frameCount) return _angleRaw;
				
				_angleRaw = Vector2.SignedAngle(Vector2.right, positionRaw);
				_lastAngleRawUpdateFrame = Time.frameCount;
				return _angleRaw;
			} 
		}

		/// <summary>
		/// The coordinates of the stick as a Vector3 facing the camera. 
		/// The stick’s horizontal and vertical axes are interpreted as the camera’s right and up directions. 
		/// The dead zone is not taken into account.
		/// </summary>
		/// <remarks>
		/// The camera that is being used can be changed with the worldCamera property of Settings.
		/// </remarks>
		public Vector3 worldPositionCameraRaw  { 
			get { 
				try { return (Settings.worldCamera.right*horizontalRaw + Settings.worldCamera.up*verticalRaw); }
				catch { 
					Debug.LogError ("Hinput error : No camera found !");
					return Vector2.zero;
				}
			} 
		}

		/// <summary>
		/// The coordinates of the stick as a Vector3 with a y value of 0. 
		/// The stick’s horizontal and vertical axes are interpreted as the absolute right and forward directions. 
		/// The dead zone is not taken into account.
		/// </summary>
		public Vector3 worldPositionFlatRaw { get { return new Vector3 (horizontalRaw, 0, verticalRaw); } }

		
		// --------------------
		// PUBLIC PROPERTIES - DEADZONED
		// --------------------

		/// <summary>
		/// Returns true if the current position of the stick is beyond the limit of its pressed zone. 
		/// Returns false otherwise.
		/// </summary>
		/// <remarks>
		/// The size of the pressed zone of the sticks can be changed with the stickPressedZone property of Settings.
		/// </remarks>
		public readonly StickPressedZone inPressedZone;

		/// <summary>
		/// Returns true if the current position of the stick is within the limit of its dead zone. 
		/// Returns false otherwise.
		/// </summary>
		/// <remarks>
		/// The size of the dead zone of the sticks can be changed with the stickDeadZone property of Settings.
		/// </remarks>
		public bool inDeadZone { get { return distanceRaw < Settings.stickDeadZone; } }

		private Vector2 _position;
		private int _lastPositionUpdateFrame = -1;
		/// <summary>
		/// The coordinates of the stick.
		/// </summary>
		public Vector2 position {
			get {
				if (_lastPositionUpdateFrame == Time.frameCount) return _position;
				
				if (inDeadZone) _position = Vector2.zero;
				else {
					Vector2 deadZonedPos = (1 + Utils.distanceIncrease)/(1 - Settings.stickDeadZone)*
						(positionRaw - positionRaw.normalized*Settings.stickDeadZone);
					
					_position = new Vector2 (
						Mathf.Clamp(deadZonedPos.x, -1, 1), 
						Mathf.Clamp(deadZonedPos.y, -1, 1));
				}
				_lastPositionUpdateFrame = Time.frameCount;
				return _position; 
			} 
		}

		/// <summary>
		/// The x coordinate of the stick.
		/// </summary>
		public float horizontal { get { return position.x; } }

		/// <summary>
		/// The y coordinate of the stick.
		/// </summary>
		public float vertical { get { return position.y; } }

		private float _distance;
		private int _lastDistanceUpdateFrame = -1;
		/// <summary>
		/// The current distance of the stick to its origin.
		/// </summary>
		public float distance { 
			get { 
				if (_lastDistanceUpdateFrame == Time.frameCount) return _distance;
				
				_distance = Mathf.Clamp01(position.magnitude);
				_lastDistanceUpdateFrame = Time.frameCount;
				return _distance; 
			} 
		}

		private float _angle;
		private int _lastAngleUpdateFrame = -1;
		/// <summary>
		/// The value of the angle between the current position of the stick and the horizontal axis 
		/// (In degrees : left=180, up=90, right=0, down=-90).
		/// </summary>
		public float angle { 
			get { 
				if (_lastAngleUpdateFrame == Time.frameCount) return _angle;
				
				_angle = Vector2.SignedAngle(Vector2.right, position);
				_lastAngleUpdateFrame = Time.frameCount;
				return _angle;
			} 
		}

		/// <summary>
		/// The coordinates of the stick as a Vector3 facing the camera. 
		/// The stick’s horizontal and vertical axes are interpreted as the camera’s right and up directions.
		/// </summary>
		/// <remarks>
		/// The camera that is being used can be changed with the worldCamera property of Settings.
		/// </remarks>
		public Vector3 worldPositionCamera { 
			get { 
				try { return (Settings.worldCamera.right*horizontal + Settings.worldCamera.up*vertical); }
				catch { 
					Debug.LogError ("Hinput error : No camera found !");
					return Vector2.zero;
				}
			} 
		}

		/// <summary>
		/// The coordinates of the stick as a Vector3 with a y value of 0. 
		/// The stick’s horizontal and vertical axes are interpreted as the absolute right and forward directions.
		/// </summary>
		public Vector3 worldPositionFlat { get { return new Vector3 (horizontal, 0, vertical); } }
	}
}