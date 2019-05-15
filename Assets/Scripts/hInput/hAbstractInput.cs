using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class hAbstractInput {
	public static implicit operator bool (hAbstractInput hAI) { return hAI.pressed; }

	public abstract float positionRaw { get; }
	public abstract float position { get; }
	public abstract bool pressed { get; }
	public abstract bool inDeadZone { get; }

	public bool released { get { return !pressed; } }

	private float _lastReleased = 0f;
	private float _lastPressed = 0f;
	private float _lastPressStart = 0f;
	private float penultimatePressStart = 0f;

	public float lastReleased { get { return _lastReleased; } }
	public float lastPressed { get { return _lastPressed; } }
	public float lastPressStart { get { return _lastPressStart; } }

	public bool justPressed { get { return (pressed && (_lastPressed - _lastReleased) <= hInput.deltaTime); } }
	public bool justReleased { get { return (released && (_lastReleased - _lastPressed) <= hInput.deltaTime); } }

	private bool lastPressWasDouble { get { return (_lastPressStart - penultimatePressStart) <= hInput.doublePressDuration; } }
	public bool doublePress { get { return pressed && lastPressWasDouble; } }
	public bool doublePressJustPressed { get { return justPressed && lastPressWasDouble; } }
	public bool doublePressJustReleased { get { return justReleased && lastPressWasDouble; } }

	private bool lastPressWasLong { get { return (_lastPressed - _lastPressStart) >= hInput.longPressDuration; }}
	public bool longPress { get { return pressed && lastPressWasLong; } }
	public bool longPressJustReleased { get { return justReleased && lastPressWasLong; } }

	public float pressDuration { get { if (pressed) return (Time.time - _lastPressStart); return 0f; } }
	public float releaseDuration { get { if (released) return (Time.time - _lastPressed); return 0f; } }

	public void Update () {
		float time = Time.time;

		if (pressed) _lastPressed = time;
		else _lastReleased = time;

		if (justPressed) {
			penultimatePressStart = _lastPressStart;
			_lastPressStart = time;			
		}
	}
}