using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// hinput abstract class representing anything that can be pressed. 
/// It can be an actual button, a stick click, a trigger, or a stick or D-pad direction.<br/><br/>
/// If no property of the hAbstractPressable is used, it will automatically be cast to a boolean with the value (pressed). 
/// For instance, (hinput.gamepad[0].A) will return (hinput.gamepad[0].A.pressed).
/// </summary>
public abstract class hAbstractPressable {
	// --------------------
	// NAME
	// --------------------

	protected string _name;
	/// <summary>
	/// Returns the name of the input , like “A”, “LeftTrigger” or “DPad_Up”.
	/// </summary>
	public string name { get { return _name; } }

	protected string _fullName;
	/// <summary>
	/// Returns the full name of the input , like “Mac_Gamepad2_RightStickClick”<br/><br/>
	/// Note : the number at the end of the gamepad’s name is the one used by Unity, not by hinput. 
	/// It is NOT equal to (index), but to (index)+1.
	/// </summary>
	public string fullName { get { return _fullName; } }

	protected int _gamepadIndex;
	/// <summary>
	/// Returns the index of the gamepad this input is attached to.
	/// </summary>
	public int gamepadIndex { get { return _gamepadIndex; } }

	/// <summary>
	/// Returns the gamepad this input is attached to.
	/// </summary>
	public hGamepad gamepad { 
		get { 
			if (gamepadIndex >= 0) return hinput.gamepad[gamepadIndex]; 
			else return hinput.anyGamepad;
		} 
	}

	
	// --------------------
	// IMPLICIT CONVERSION
	// --------------------

	public static implicit operator bool (hAbstractPressable hAbstractPressable) { return hAbstractPressable.pressed; }

	
	// --------------------
	// ABSTRACT PROPERTIES
	// --------------------

	/// <summary>
	/// Returns the current position of the input (0 or 1 for a button, 0 to 1 for a trigger, and -1 to 1 for a stick direction).
	/// </summary>
	public abstract float position { get; }

	/// <summary>
	/// Returns true if the input is considered “pressed”. Returns false otherwise.
	/// </summary>
	public abstract bool pressed { get; }

	/// <summary>
	/// For a button, returns (pressed)<br/><br/>
	/// For a trigger, returns true if the trigger’s raw position is higher than (deadZone).<br/><br/>
	/// For a stick direction, returns true if the stick’s raw distance is higher than (deadZone)
	/// </summary>
	public abstract bool inDeadZone { get; }

	
	// --------------------
	// PRESS AND RELEASE TIME
	// --------------------

	private float _lastReleased = 0f;
	private float _lastPressed = 0f;
	private float _lastPressStart = 0f;
	private float penultimatePressStart = 0f;

	
	// --------------------
	// UPDATE
	// --------------------

	/// <summary>
	/// Please never call that.
	/// </summary>
	public void Update () {		
		float time = Time.time;

		UpdatePositionRaw ();

		if (pressed) _lastPressed = time;
		else _lastReleased = time;

		if (justPressed) {
			penultimatePressStart = _lastPressStart;
			_lastPressStart = time;		
		}
	}

	protected abstract void UpdatePositionRaw ();

	
	// --------------------
	// PUBLIC PROPERTIES
	// --------------------

	protected float _positionRaw;
	/// <summary>
	/// Returns the current raw position of the input. Similar to (position) for buttons. 
	/// Triggers and stick directions do not take the dead zone into account.
	/// </summary>
	public float positionRaw { get { return _positionRaw; } }

	/// <summary>
	/// Returns true if the input is not (pressed). Returns false otherwise.
	/// </summary>
	public bool released { get { return !pressed; } }

	/// <summary>
	/// Returns the date the input was last (released) (in seconds from the beginning of the game). 
	/// Returns zero if it hasn't been (pressed).
	/// </summary>
	public float lastReleased { get { return _lastReleased; } }

	/// <summary>
	/// Returns the date the input was last (pressed) (in seconds from the beginning of the game). 
	/// Returns 0 if it hasn't been (pressed).
	/// </summary>
	public float lastPressed { get { return _lastPressed; } }

	/// <summary>
	/// Returns the date the input was last (justPressed) (in seconds from the beginning of the game). 
	/// Returns 0 if it hasn't been (pressed).
	/// </summary>
	public float lastPressStart { get { return _lastPressStart; } }

	/// <summary>
	/// Returns true if the input is currently (pressed) and was (released) last frame. Returns false otherwise.
	/// </summary>
	public bool justPressed { get { return (pressed && (lastPressed - lastReleased) <= hUtils.maxDeltaTime); } }

	/// <summary>
	/// Returns true if the input is currently (released) and was (pressed) last frame. Returns false otherwise.
	/// </summary>
	public bool justReleased { get { return (released && (lastReleased - lastPressed) <= hUtils.maxDeltaTime); } }

	/// <summary>
	/// Returns true if the last two presses started less than (hinput.doublePressDuration) seconds apart 
	/// (including current press if the input is (pressed)). Returns false otherwise.
	/// </summary>
	public bool lastPressWasDouble { get { return (lastPressStart - penultimatePressStart) <= hSettings.doublePressDuration; } }

	/// <summary>
	/// Returns true if the input is currently (pressed), 
	/// and the last two presses started less than (hinput.doublePressDuration) seconds apart. 
	/// Returns false otherwise.
	/// </summary>
	public bool doublePress { get { return pressed && lastPressWasDouble; } }

	/// <summary>
	/// Returns true if the input is currently (justPressed), 
	/// and the last two presses started less than (hinput.doublePressDuration) seconds apart. 
	/// Returns false otherwise.
	/// </summary>
	public bool doublePressJustPressed { get { return justPressed && lastPressWasDouble; } }

	/// <summary>
	/// Returns true if the input is currently (justReleased), 
	/// and the last two presses started less than (hinput.doublePressDuration) seconds apart. 
	/// Returns false otherwise.
	/// </summary>
	public bool doublePressJustReleased { get { return justReleased && lastPressWasDouble; } }

	/// <summary>
	/// Returns true if the last press has lasted longer than (hinput.longPressDuration) seconds 
	/// (including current press if the input is (pressed)). Returns false otherwise.
	/// </summary>
	public bool lastPressWasLong { get { return (lastPressed - lastPressStart) >= hSettings.longPressDuration; }}

	/// <summary>
	/// Returns true if the input is currently (pressed) 
	/// and the press has lasted longer than (hinput.longPressDuration) seconds. 
	/// Returns false otherwise.
	/// </summary>
	public bool longPress { get { return pressed && lastPressWasLong; } }

	/// <summary>
	/// Returns true if the input is currently (justReleased), 
	/// and the last press has lasted longer than (hinput.longPressDuration) seconds. 
	/// Returns false otherwise.
	/// </summary>
	public bool longPressJustReleased { get { return justReleased && lastPressWasLong; } }

	/// <summary>
	/// If the input is (pressed), returns the amount of time that has passed since it is (pressed). 
	/// Returns 0 otherwise.
	/// </summary>
	public float pressDuration { get { if (pressed) return (Time.time - lastPressStart); return 0f; } }

	/// <summary>
	/// If the input is (released), returns the amount of time that has passed since it is (released). 
	/// Returns 0 otherwise.
	/// </summary>
	public float releaseDuration { get { if (released) return (Time.time - lastPressed); return 0f; } }
}