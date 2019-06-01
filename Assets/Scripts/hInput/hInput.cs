using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The main class of hInput, from which you can access gamepads. <br/><br/>
/// You can attach it to a GameObject to expose settings. 
/// If you don’t, it will be instantiated at runtime the first time you call it, with default settings.
/// </summary>
public class hInput : MonoBehaviour {
	// --------------------
	// SETTINGS
	// --------------------

	[Header("hINPUT SETTINGS")]
	[Space(10)]

	[SerializeField]
	[Tooltip ("If enabled, hInput will start tracking every control of every gamepad from startup. "
	+"Otherwise, each control will only start being registered the first time you ask for it.")]
	private bool _buildAllOnStartUp = false;

	[SerializeField]
	[Range(0,1)]
	[Tooltip("The distance from the center beyond which stick and trigger inputs start being registered (except for raw inputs).")]
	protected float _deadZone = 0.2f;
	/// <summary>
	/// The distance from the center beyond which stick and trigger inputs start being registered (except for raw inputs).
	/// </summary>
	public static float deadZone { 
		get { return instance._deadZone; } 
		set { instance._deadZone = value; } 
	}

	[SerializeField]
	[Range(0,1)]
	[Tooltip("The distance from the end of the dead zone beyond which stick and trigger inputs are considered pushed or activated.")]
	private float _triggerZone = 0.5f;
	/// <summary>
	/// The distance from the end of the dead zone beyond which stick and trigger inputs are considered pushed or activated.
	/// </summary>
	public static float triggerZone { 
		get { return instance._triggerZone; } 
		set { instance._triggerZone = value; }  
	}

	[SerializeField]
	[Range(45,90)]
	[Tooltip("The size of the angle that defines a stick direction.\n\n"+
	"Note : if it is higher than 45 degrees, directions like (up) and (leftUp) will overlap. " 
	+"Likewise, if it is lower than 90 degrees, there will be a gap between directions like (up) and (left).")]
	private float _directionAngle = 90f;
	/// <summary>
	/// The size of the angle that defines a stick direction. <br/><br/>
	/// Note : if it is higher than 45 degrees, directions like (up) and (leftUp) will overlap.
	/// Likewise, if it is lower than 90 degrees, there will be a gap between directions like (up) and (left).
	/// </summary>
	public static float directionAngle { 
		get { return instance._directionAngle; } 
		set { instance._directionAngle = value; }  
	}

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

	[SerializeField]
	[Tooltip("The camera on which the worldPosition property of hStick and hDPad should be calculated. If not set, hInput will try to find one on the scene.")]
	private Transform _worldCamera = null;
	/// <summary>
	/// The camera on which the worldPosition property of hStick and hDPad should be calculated. If not set, hInput will try to find one on the scene.
	/// </summary>
	public static Transform worldCamera { 
		get { 
			if (instance._worldCamera != null) return instance._worldCamera;
			else if (Camera.main != null) instance._worldCamera = Camera.main.transform;
			else if (GameObject.FindObjectOfType<Camera>() != null) instance._worldCamera = GameObject.FindObjectOfType<Camera>().transform;
			else { Debug.LogError ("hInput error : No camera found !"); return null; }
			return instance._worldCamera;
		} 
		set { instance._worldCamera = value; } 
	}


	// --------------------
	// SINGLETON PATTERN
	// --------------------

	//The instance of hInput. Assigned when first called.
	private static hInput _instance;
	public static hInput instance { 
		get {
			if (_instance == null) {
				GameObject go = new GameObject();
				go.name = "hInput";
				_instance = go.AddComponent<hInput>();
			}
			
			return _instance;
		} 
	}

	private void Awake () {
		if (_instance == null) _instance = this;
		if (_instance != this) Destroy(this);
		DontDestroyOnLoad (this);

		if (_buildAllOnStartUp) {
			anyGamepad.BuildAll();
			for (int i=0; i<hInputUtils.maxGamepads; i++) gamepad[i].BuildAll();
		}
	}


	// --------------------
	// GAMEPADS
	// --------------------

	private hGamepad _anyGamepad;
	/// <summary>
	/// A virtual gamepad that returns the biggest absolute value for each control of all connected gamepads.
	/// </summary>
	public static hGamepad anyGamepad { 
		get { 
			if (instance._anyGamepad == null) {
				instance._anyGamepad = new hGamepad(hInputUtils.os, -1);
			} else {
				instance.UpdateGamepads ();
			}

			return instance._anyGamepad; 
		}
	}

	private List<hGamepad> _gamepad;
	/// <summary>
	/// An array of 8 gamepads, labelled 0 to 7.
	/// </summary>
	public static List<hGamepad> gamepad { 
		get {
			if (instance._gamepad == null) {
				instance._gamepad = new List<hGamepad>();
				for (int i=0; i<hInputUtils.maxGamepads; i++) gamepad.Add(new hGamepad(hInputUtils.os, i));
			} else {
				instance.UpdateGamepads ();
			} 

			return instance._gamepad; 
		} 
	}


	// --------------------
	// UPDATE
	// --------------------
	
	private void Update () {
		UpdateGamepads();
	}

	// If the gamepads have not been updated this frame, update them.
	private void UpdateGamepads () {
		if (!hInputUtils.isUpToDate) {
			hInputUtils.UpdateTime ();
			anyGamepad.Update();
			for (int i=0; i<hInputUtils.maxGamepads; i++) gamepad[i].Update ();
		}
	}
}