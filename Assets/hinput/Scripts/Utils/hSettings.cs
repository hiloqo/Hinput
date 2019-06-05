using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// hinput class responsible for handling settings, as well as gamepad updates.<br/><br/>
/// You can attach it to a GameObject to expose settings. 
/// If you don’t, it will automatically be instantiated at runtime the first time you call hinput, with default settings.
/// </summary>
public class hSettings : MonoBehaviour {
	// --------------------
	// SINGLETON PATTERN
	// --------------------

	//The instance of hinputSettings. Assigned when first called.
	private static hSettings _instance;
	public static hSettings instance { 
		get {
			if (_instance == null) {
				GameObject go = new GameObject();
				go.name = "hinput";
				_instance = go.AddComponent<hSettings>();
			}
			
			return _instance;
		} 
	}

	private void Awake () {
		if (_instance == null) _instance = this;
		if (_instance != this) Destroy(this);
		DontDestroyOnLoad (this);

		if (_buildAllOnStartUp) {
			hinput.anyGamepad.BuildAll();
			for (int i=0; i<hUtils.maxGamepads; i++) hinput.gamepad[i].BuildAll();
		}
	}


	// --------------------
	// SETTINGS
	// --------------------

	[Header("hinput settings")]
	[Space(10)]

	[SerializeField]
	[Tooltip ("If enabled, hinput will start tracking every control of every gamepad from startup. "
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
	[Tooltip("The camera on which the worldPosition property of hStick and hDPad should be calculated. If not set, hinput will try to find one on the scene.")]
	private Transform _worldCamera = null;
	/// <summary>
	/// The camera on which the worldPosition property of hStick and hDPad should be calculated. If not set, hinput will try to find one on the scene.
	/// </summary>
	public static Transform worldCamera { 
		get { 
			if (instance._worldCamera != null) return instance._worldCamera;
			else if (Camera.main != null) instance._worldCamera = Camera.main.transform;
			else if (GameObject.FindObjectOfType<Camera>() != null) instance._worldCamera = GameObject.FindObjectOfType<Camera>().transform;
			else { Debug.LogError ("hinput error : No camera found !"); return null; }
			return instance._worldCamera;
		} 
		set { instance._worldCamera = value; } 
	}
}