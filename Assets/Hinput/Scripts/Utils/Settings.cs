using UnityEngine;
using HinputClasses.Internal;
using UnityEngine.Serialization;

namespace HinputClasses {
    /// <summary>
	/// Hinput class responsible for handling settings.<br/>
	/// You can attach it to a GameObject to expose settings. 
	/// If you don’t, it will automatically be instantiated at runtime the first time you call Hinput, with default settings.
	/// </summary>
	public class Settings : MonoBehaviour {
		// --------------------
		// SINGLETON PATTERN
		// --------------------

		//The instance of HinputSettings. Assigned when first called.
		private static Settings _instance;
		public static Settings instance { 
			get {
				CheckInstance();
				return _instance;
			} 
		}

		private static void CheckInstance() {
			if (_instance != null) return;
			
			GameObject go = new GameObject {name = "HinputSettings"};
			_instance = go.AddComponent<Settings>();
		}

		private void Awake () {
			if (_instance == null) _instance = this;
			if (_instance != this) Destroy(this);
			DontDestroyOnLoad (this);
		}


		// --------------------
		// SETTINGS
		// --------------------

		public enum StickTypeEnum { FourDirections = 90, EightDirections = 45 }
		[Header("Sticks")]

		[SerializeField]
		[Tooltip("The type of stick to use.\n\n"+
		         "Set it to Four Directions for 4-directional sticks, with buttons that are 90 degrees wide (The use of " +
		         "diagonals is not recommended in this case). Set it to Eight Directions for 8-directional sticks, with " +
		         "buttons that are 45 degrees wide.")]
		private StickTypeEnum _stickType = StickTypeEnum.EightDirections;
		/// <summary>
		/// The type of stick to use. <br/>
		/// Set it to Four Directions for 4-directional sticks, with buttons that are 90 degrees wide (The use of
		/// diagonals is not recommended in this case). Set it to Eight Directions for 8-directional sticks, with
		/// buttons that are 45 degrees wide.
		/// </summary>
		public static StickTypeEnum stickType { 
			get { return instance._stickType; } 
			set { instance._stickType = value; }  
		}

		[SerializeField]
		[Range(0,1)]
		[Tooltip("The distance from the origin beyond which stick inputs start being registered (except for raw inputs).")]
		private float _stickDeadZone = 0.2f;
		/// <summary>
		/// The distance from the origin beyond which stick inputs start being registered (except for raw inputs).
		/// </summary>
		public static float stickDeadZone { 
			get { return instance._stickDeadZone; } 
			set { instance._stickDeadZone = value; } 
		}

		[SerializeField]
		[Range(0,1)]
		[Tooltip("The distance from the end of the dead zone beyond which stick inputs are considered pushed.")]
		private float _stickPressedZone = 0.5f;
		/// <summary>
		/// The distance from the end of the dead zone beyond which stick inputs are considered pushed.
		/// </summary>
		public static float stickPressedZone { 
			get { return instance._stickPressedZone; } 
			set { instance._stickPressedZone = value; }  
		}

		[SerializeField]
		[Tooltip("The Camera on which the worldPositionCamera and worldPositionCameraRaw properties of Stick should be calculated. " 
		         +"If no Camera is set, Hinput will try to find one on your scene.")]
		private Transform _worldCamera = null;
		/// <summary>
		/// The Camera on which the worldPositionCamera and worldPositionCameraRaw properties of Stick should be calculated. 
		/// If no Camera is set, Hinput will try to find one on your scene.
		/// </summary>
		/// <remarks>
		/// Hinput will first try to get the gameobject tagged “MainCamera”. 
		/// If there isn’t one, Hinput will get the first gameobject on the game scene that has a Camera component.
		/// If there is no Camera on the scene, Hinput will return an error whenever you call a worldPositionCamera 
		/// or worldPositionCameraRaw property.
		/// </remarks>
		public static Transform worldCamera { 
			get { 
				if (instance._worldCamera == null) {
					if (Camera.main != null) instance._worldCamera = Camera.main.transform;
					else if (FindObjectOfType<Camera>() != null) 
						instance._worldCamera = FindObjectOfType<Camera>().transform;
					else return null;
				}

				return instance._worldCamera;
			} 
			set { instance._worldCamera = value; } 
		}

		[Header("Triggers")]

		[SerializeField]
		[Range(0,1)]
		[Tooltip("The distance from the origin beyond which trigger inputs start being registered (except for raw inputs).")]
		private float _triggerDeadZone = 0.1f;
		/// <summary>
		/// The distance from the origin beyond which trigger inputs start being registered (except for raw inputs).
		/// </summary>
		public static float triggerDeadZone { 
			get { return instance._triggerDeadZone; } 
			set { instance._triggerDeadZone = value; } 
		}

		[SerializeField]
		[Range(0,1)]
		[Tooltip("The distance from the end of the dead zone beyond which trigger inputs are considered pushed.")]
		private float _triggerPressedZone = 0.5f;
		/// <summary>
		/// The distance from the end of the dead zone beyond which trigger inputs are considered pushed.
		/// </summary>
		public static float triggerPressedZone { 
			get { return instance._triggerPressedZone; } 
			set { instance._triggerPressedZone = value; }  
		}
		
		[Header("Buttons")]

		[SerializeField]
		[Range(0,2)]
		[Tooltip("The maximum duration between the start of two presses for them to be considered a double press.")]
		private float _doublePressDuration = 0.3f;
		/// <summary>
		/// The maximum duration between the start of two presses for them to be considered a double press.
		/// </summary>
		public static float doublePressDuration { 
			get { return instance._doublePressDuration; } 
			set { instance._doublePressDuration = value; }  
		}

		[SerializeField]
		[Range(0,2)]
		[Tooltip("The minimum duration of a press for it to be considered a long press.")]
		private float _longPressDuration = 0.3f;
		/// <summary>
		/// The minimum duration of a press for it to be considered a long press.
		/// </summary>
		public static float longPressDuration { 
			get { return instance._longPressDuration; } 
			set { instance._longPressDuration = value; }  
		}

		[Header("Vibration Defaults")]

		[SerializeField]
		[Range(0,2)]
		[Tooltip("The default duration of gamepad vibration.")]
		private float _vibrationDefaultDuration = 0.5f;
		/// <summary>
		/// The default duration of gamepad vibration.
		/// </summary>
		public static float vibrationDefaultDuration { 
			get { return instance._vibrationDefaultDuration; } 
			set { instance._vibrationDefaultDuration = value; }  
		}

		[SerializeField]
		[Range(0,1)]
		[Tooltip("The default intensity of the left (low-frequency) motor when controllers vibrate.")]
		private float _vibrationDefaultLeftIntensity = 1f;
		/// <summary>
		/// The default intensity of the left (low-frequency) motor when controllers vibrate.
		/// </summary>
		/// <remarks>
		/// The left motor is a low-frequency rumble motor.
		/// </remarks>
		public static float vibrationDefaultLeftIntensity { 
			get { return instance._vibrationDefaultLeftIntensity; } 
			set { instance._vibrationDefaultLeftIntensity = value; }  
		}

		[SerializeField]
		[Range(0,1)]
		[Tooltip("The default intensity of the right (high-frequency) motor when controllers vibrate.")]
		private float _vibrationDefaultRightIntensity = 1f;
		/// <summary>
		/// The default intensity of the right (high-frequency) motor when controllers vibrate.
		/// </summary>
		/// <remarks>
		/// The right motor is a high-frequency rumble motor.
		/// </remarks>
		public static float vibrationDefaultRightIntensity { 
			get { return instance._vibrationDefaultRightIntensity; } 
			set { instance._vibrationDefaultRightIntensity = value; }  
		}


		// --------------------
		// PERFORMANCE
		// --------------------

		[Header("Performance")]
		[SerializeField]
		[Range(0,8)]
		[Tooltip("The maximum amount of gamepads to be tracked by Hinput. Any gamepad disabled by this control can " +
		         "only be reactivated by it. Reducing this may improve performance.")]
		private int _amountOfGamepads = 8;
		/// <summary>
		/// The maximum amount of gamepads to be tracked by Hinput. Any gamepad disabled by this control can only be
		/// reactivated by it. Reducing this may improve performance.
		/// </summary>
		public static int amountOfGamepads { 
			get { return instance._amountOfGamepads; } 
			set { instance._amountOfGamepads = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause anyGamepad to not be tracked by Hinput. Any " +
		         "gamepad disabled by this control can only be reactivated by it. This may improve performance.")]
		private bool _disableAnyGamepad = false;
		/// <summary>
		/// Checking this before starting the game will cause anyGamepad to not be tracked by Hinput. Any gamepad
		/// disabled by this control can only be reactivated by it. This may improve performance.
		/// </summary>
		public static bool disableAnyGamepad { 
			get { return instance._disableAnyGamepad; } 
			set { instance._disableAnyGamepad = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause the A button ofgamepads to not be tracked by " +
		         "Hinput. This may improve performance.")]
		private bool _disableA = false;
		/// <summary>
		/// Checking this before starting the game will cause the A button of gamepads to not be tracked by Hinput.
		/// This may improve performance.
		/// </summary>
		public static bool disableA { 
			get { return instance._disableA; } 
			set { instance._disableA = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause the B button of gamepads to not be tracked by " +
		         "Hinput. This may improve performance.")]
		private bool _disableB = false;
		/// <summary>
		/// Checking this before starting the game will cause the B button of gamepads to not be tracked by Hinput.
		/// This may improve performance.
		/// </summary>
		public static bool disableB { 
			get { return instance._disableB; } 
			set { instance._disableB = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause the X button of gamepads to not be tracked by " +
		         "Hinput. This may improve performance.")]
		private bool _disableX = false;
		/// <summary>
		/// Checking this before starting the game will cause the X button of gamepads to not be tracked by Hinput.
		/// This may improve performance.
		/// </summary>
		public static bool disableX { 
			get { return instance._disableX; } 
			set { instance._disableX = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause the Y button of gamepads to not be tracked by " +
		         "Hinput. This may improve performance.")]
		private bool _disableY = false;
		/// <summary>
		/// Checking this before starting the game will cause the Y button of gamepads to not be tracked by Hinput.
		/// This may improve performance.
		/// </summary>
		public static bool disableY { 
			get { return instance._disableY; } 
			set { instance._disableY = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause the left bumper of gamepads to not be tracked by " +
		         "Hinput. This may improve performance.")]
		private bool _disableLeftBumper = false;
		/// <summary>
		/// Checking this before starting the game will cause the left bumper of gamepads to not be tracked by Hinput.
		/// This may improve performance.
		/// </summary>
		public static bool disableLeftBumper { 
			get { return instance._disableLeftBumper; } 
			set { instance._disableLeftBumper = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause the right bumper of gamepads to not be tracked " +
		         "by Hinput. This may improve performance.")]
		private bool _disableRightBumper = false;
		/// <summary>
		/// Checking this before starting the game will cause the right bumper of gamepads to not be tracked by Hinput.
		/// This may improve performance.
		/// </summary>
		public static bool disableRightBumper { 
			get { return instance._disableRightBumper; } 
			set { instance._disableRightBumper = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause the left trigger of gamepads to not be tracked " +
		         "by Hinput. This may improve performance.")]
		private bool _disableLeftTrigger = false;
		/// <summary>
		/// Checking this before starting the game will cause the left trigger of gamepads to not be tracked by Hinput.
		/// This may improve performance.
		/// </summary>
		public static bool disableLeftTrigger { 
			get { return instance._disableLeftTrigger; } 
			set { instance._disableLeftTrigger = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause the right trigger of gamepads to not be tracked " +
		         "by Hinput. This may improve performance.")]
		private bool _disableRightTrigger = false;
		/// <summary>
		/// Checking this before starting the game will cause the right trigger of gamepads to not be tracked by
		/// Hinput. This may improve performance.
		/// </summary>
		public static bool disableRightTrigger { 
			get { return instance._disableRightTrigger; } 
			set { instance._disableRightTrigger = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause the back button of gamepads to not be tracked " +
		         "by Hinput. This may improve performance.")]
		private bool _disableBack = false;
		/// <summary>
		/// Checking this before starting the game will cause the back button of gamepads to not be tracked by Hinput.
		/// This may improve performance.
		/// </summary>
		public static bool disableBack { 
			get { return instance._disableBack; } 
			set { instance._disableBack = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause the start button of gamepads to not be tracked " +
		         "by Hinput. This may improve performance.")]
		private bool _disableStart = false;
		/// <summary>
		/// Checking this before starting the game will cause the start button of gamepads to not be tracked by Hinput.
		/// This may improve performance.
		/// </summary>
		public static bool disableStart { 
			get { return instance._disableStart; } 
			set { instance._disableStart = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause the left stick click of gamepads to not be " +
		         "tracked by Hinput. This may improve performance.")]
		private bool _disableLeftStickClick = false;
		/// <summary>
		/// Checking this before starting the game will cause the left stick click of gamepads to not be tracked by
		/// Hinput. This may improve performance.
		/// </summary>
		public static bool disableLeftStickClick { 
			get { return instance._disableLeftStickClick; } 
			set { instance._disableLeftStickClick = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause the right stick click of gamepads to not be " +
		         "tracked by Hinput. This may improve performance.")]
		private bool _disableRightStickClick = false;
		/// <summary>
		/// Checking this before starting the game will cause the right stick click of gamepads to not be tracked by
		/// Hinput. This may improve performance.
		/// </summary>
		public static bool disableRightStickClick { 
			get { return instance._disableRightStickClick; } 
			set { instance._disableRightStickClick = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause the xBox button of gamepads to not be tracked " +
		         "by Hinput. This may improve performance.")]
		private bool _disableXBoxButton = false;
		/// <summary>
		/// Checking this before starting the game will cause the xBox button of gamepads to not be tracked by Hinput.
		/// This may improve performance.
		/// </summary>
		public static bool disableXBoxButton { 
			get { return instance._disableXBoxButton; } 
			set { instance._disableXBoxButton = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause the left stick of gamepads to not be tracked " +
		         "by Hinput. This may improve performance.")]
		private bool _disableLeftStick = false;
		/// <summary>
		/// Checking this before starting the game will cause the left stick of gamepads to not be tracked by Hinput.
		/// This may improve performance.
		/// </summary>
		public static bool disableLeftStick { 
			get { return instance._disableLeftStick; } 
			set { instance._disableLeftStick = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause the right stick of gamepads to not be tracked " +
		         "by Hinput. This may improve performance.")]
		private bool _disableRightStick = false;
		/// <summary>
		/// Checking this before starting the game will cause the right stick of gamepads to not be tracked by Hinput.
		/// This may improve performance.
		/// </summary>
		public static bool disableRightStick { 
			get { return instance._disableRightStick; } 
			set { instance._disableRightStick = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause the D-pad of gamepads to not be tracked by " +
		         "Hinput. This may improve performance.")]
		private bool _disableDPad = false;
		/// <summary>
		/// Checking this before starting the game will cause the D-pad of gamepads to not be tracked by Hinput. This
		/// may improve performance.
		/// </summary>
		public static bool disableDPad { 
			get { return instance._disableDPad; } 
			set { instance._disableDPad = value; }  
		}

		[SerializeField]
		[Tooltip("Checking this before starting the game will cause the anyInput feature of gamepads to not be " +
		         "tracked by Hinput. This may improve performance.")]
		private bool _disableAnyInput = false;
		/// <summary>
		/// Checking this before starting the game will cause the anyInput feature of gamepads to not be tracked by
		/// Hinput. This may improve performance.
		/// </summary>
		public static bool disableAnyInput { 
			get { return instance._disableAnyInput; } 
			set { instance._disableAnyInput = value; }  
		}
    }
}