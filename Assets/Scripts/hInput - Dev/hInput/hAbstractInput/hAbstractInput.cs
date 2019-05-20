using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class hAbstractInput {
	// --------------------
	// NAME
	// --------------------

	protected string _name;
	public string name { get { return _name; } }

	protected string _fullName;
	public string fullName { get { return _fullName; } }

	
	// --------------------
	// IMPLICIT CONVERSION
	// --------------------

	public static implicit operator bool (hAbstractInput hAbstractInput) { return hAbstractInput.pressed; }

	
	// --------------------
	// ABSTRACT PROPERTIES
	// --------------------

	public abstract float positionRaw { get; }
	public abstract float position { get; }
	public abstract bool pressed { get; }
	public abstract bool inDeadZone { get; }

	
	// --------------------
	// PRESS AND RELEASE TIME
	// --------------------

	private float _lastReleased = 0f;
	private float _lastPressed = 0f;
	private float _lastPressStart = 0f;
	private float penultimatePressStart = 0f;

	public void Update () {
		float time = Time.time;

		if (pressed) _lastPressed = time;
		else _lastReleased = time;

		if (justPressed) {
			penultimatePressStart = _lastPressStart;
			_lastPressStart = time;			
		}
	}

	
	// --------------------
	// PROPERTIES
	// --------------------

	public bool released { get { return !pressed; } }

	public float lastReleased { get { return _lastReleased; } }
	public float lastPressed { get { return _lastPressed; } }
	public float lastPressStart { get { return _lastPressStart; } }

	public bool justPressed { get { return (pressed && (lastPressed - lastReleased) <= hInput.maxDeltaTime); } }
	public bool justReleased { get { return (released && (lastReleased - lastPressed) <= hInput.maxDeltaTime); } }

	public bool lastPressWasDouble { get { return (lastPressStart - penultimatePressStart) <= hInput.doublePressDuration; } }
	public bool doublePress { get { return pressed && lastPressWasDouble; } }
	public bool doublePressJustPressed { get { return justPressed && lastPressWasDouble; } }
	public bool doublePressJustReleased { get { return justReleased && lastPressWasDouble; } }

	public bool lastPressWasLong { get { return (lastPressed - lastPressStart) >= hInput.longPressDuration; }}
	public bool longPress { get { return pressed && lastPressWasLong; } }
	public bool longPressJustReleased { get { return justReleased && lastPressWasLong; } }

	public float pressDuration { get { if (pressed) return (Time.time - lastPressStart); return 0f; } }
	public float releaseDuration { get { if (released) return (Time.time - lastPressed); return 0f; } }
}