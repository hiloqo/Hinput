using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hInput : MonoBehaviour {
	[SerializeField]
	[Range(0,1)]
	[Tooltip("The distance from the center beyond which stick and trigger inputs start being registered.")]
	private float _deadZone = 0.2f;
	public static float deadZone { get { return instance._deadZone; } set { instance._deadZone = value; } }

	[SerializeField]
	[Range(0,1)]
	[Tooltip("The distance from the center beyond which stick and trigger inputs are considered pushed or activated.")]
	private float _triggerZone = 0.5f;
	public static float triggerZone { get { return instance._triggerZone; } }

	[SerializeField]
	[Range(0,4)]
	[Tooltip("The maximum amount of gamepads to prepare. Reduce for better performances.")]
	private int _maxGamepads = 4;
	public static int maxGamepads { get { return instance._maxGamepads; } }

	[SerializeField]
	[Range(45,90)]
	[Tooltip("The size of the angle that defines a stick direction. Note that if it is higher than 45 degrees, directions like (up) and (leftUp) will overlap." 
	+"Likewise, if it is lower than 90 degrees, there will be a gap between directions like (up) and (left).")]
	private float _directionAngle = 90f;
	public static float directionAngle { get { return instance._directionAngle; } }

	[SerializeField]
	[Range(0,2)]
	[Tooltip("The maximum duration between the start of two presses for them to be considered a double press.")]
	private float _doublePressDuration = 0.2f;
	public static float doublePressDuration { get { return instance._doublePressDuration; } }

	[SerializeField]
	[Range(0,2)]
	[Tooltip("The minimum duration of a press for it to be considered a long press.")]
	private float _longPressDuration = 0.1f;
	public static float longPressDuration { get { return instance._longPressDuration; } }

	[SerializeField]
	[Tooltip("The camera on which the worldPosition property of hStick should be calculated. If null, hInput will use the main camera.")]
	private Camera _worldCamera = null;
	public Camera worldCamera { 
		get { 
			if (_worldCamera == null) _worldCamera = Camera.main; 
			return _worldCamera; 
		} 
	}

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
	}
}