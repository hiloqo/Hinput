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
		[Space(10)]
		[Header("Hinput settings")]

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
		[Tooltip("The default intensity of the left motor when controllers vibrate.")]
		private float _vibrationDefaultLeftIntensity = 1f;
		/// <summary>
		/// The default intensity of the left motor when controllers vibrate.
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
		[Tooltip("The default intensity of the right motor when controllers vibrate.")]
		private float _vibrationDefaultRightIntensity = 1f;
		/// <summary>
		/// The default intensity of the right motor when controllers vibrate.
		/// </summary>
		/// <remarks>
		/// The right motor is a high-frequency rumble motor.
		/// </remarks>
		public static float vibrationDefaultRightIntensity { 
			get { return instance._vibrationDefaultRightIntensity; } 
			set { instance._vibrationDefaultRightIntensity = value; }  
		}
	}
}