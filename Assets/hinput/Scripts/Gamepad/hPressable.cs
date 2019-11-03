using UnityEngine;

/// <summary>
/// hinput abstract class representing anything that can be considered pressed and released. 
/// It can be an actual button, a stick click, a trigger, a stick direction...<br/>
/// If no property of the hPressable is used, it will automatically be cast to a boolean with the value pressed. 
/// For instance, hinput.gamepad[0].A will return hinput.gamepad[0].A.pressed.
/// </summary>
public abstract class hPressable {
	// --------------------
	// NAME
	// --------------------

	/// <summary>
	/// Returns the name of the input , like “A”, “LeftTrigger” or “DPad_Up”.
	/// </summary>
	public readonly string name;

	/// <summary>
	/// Returns the full name of the input , like “Mac_Gamepad2_RightStickClick”
	/// </summary>
	public readonly string fullName;

	/// <summary>
	/// Returns the index of the gamepad this input is attached to.
	/// </summary>
	public readonly int gamepadIndex;

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

	public static implicit operator bool (hPressable hPressable) { return hPressable.pressed; }


	// --------------------
	// CONSTRUCTOR
	// --------------------

	protected hPressable(string name, string fullName, int gamepadIndex) {
		this.name = name;
		this.fullName = fullName;
		this.gamepadIndex = gamepadIndex;
	}

	
	// --------------------
	// ABSTRACT PROPERTIES
	// --------------------

	/// <summary>
	/// Returns the current position of the input.
	/// </summary>
	public abstract float position { get; }

	/// <summary>
	/// Returns true if the input is pressed. Returns false otherwise.
	/// </summary>
	public abstract bool pressed { get; }

	/// <summary>
	/// Returns true if the input is in its dead zone. Returns false otherwise.
	/// </summary>
	public abstract bool inDeadZone { get; }

	
	// --------------------
	// PRESS AND RELEASE TIME
	// --------------------

	private float penultimatePressStart = 0f;

	
	// --------------------
	// UPDATE
	// --------------------

	public void Update () {		
		float time = Time.unscaledTime;

		UpdatePositionRaw ();

		if (pressed) lastPressed = time;
		else lastReleased = time;

		if (justPressed) {
			penultimatePressStart = lastPressStart;
			lastPressStart = time;		
		}
	}

	protected abstract void UpdatePositionRaw ();

	
	// --------------------
	// PUBLIC PROPERTIES
	// --------------------
	
	/// <summary>
	/// Returns the current raw position of the input, i.e. not taking the dead zone into account.
	/// </summary>
	public float positionRaw { get; protected set; }

	/// <summary>
	/// Returns true if the input is not pressed. Returns false otherwise.
	/// </summary>
	public bool released { get { return !pressed; } }

	/// <summary>
	/// Returns the date the input was last released (in seconds from the beginning of the game). 
	/// Returns zero if it hasn't been pressed.
	/// </summary>
	public float lastReleased { get; private set; }

	/// <summary>
	/// Returns the date the input was last pressed (in seconds from the beginning of the game). 
	/// Returns 0 if it hasn't been pressed.
	/// </summary>
	public float lastPressed { get; private set; }

	/// <summary>
	/// Returns the date the input was last justPressed (in seconds from the beginning of the game). 
	/// Returns 0 if it hasn't been pressed.
	/// </summary>
	public float lastPressStart { get; private set; }

	/// <summary>
	/// Returns true if the input is currently pressed and was released last frame. Returns false otherwise.
	/// </summary>
	public bool justPressed { get { return (pressed && (lastPressed - lastReleased) <= hUpdater.maxDeltaTime); } }

	/// <summary>
	/// Returns true if the input is currently released and was pressed last frame. Returns false otherwise.
	/// </summary>
	public bool justReleased { get { return (released && (lastReleased - lastPressed) <= hUpdater.maxDeltaTime); } }

	/// <summary>
	/// Returns true if the last two presses started less than hSettings.doublePressDuration seconds apart 
	/// (including current press if the input is pressed). Returns false otherwise.
	/// </summary>
	public bool lastPressWasDouble { get { return (lastPressStart - penultimatePressStart) <= hSettings.doublePressDuration; } }

	/// <summary>
	/// Returns true if the input is currently pressed, 
	/// and the last two presses started less than hSettings.doublePressDuration seconds apart. 
	/// Returns false otherwise.
	/// </summary>
	public bool doublePress { get { return pressed && lastPressWasDouble; } }

	/// <summary>
	/// Returns true if the input is currently justPressed, 
	/// and the last two presses started less than hSettings.doublePressDuration seconds apart. 
	/// Returns false otherwise.
	/// </summary>
	public bool doublePressJustPressed { get { return justPressed && lastPressWasDouble; } }

	/// <summary>
	/// Returns true if the input is currently justReleased, 
	/// and the last two presses started less than hSettings.doublePressDuration seconds apart. 
	/// Returns false otherwise.
	/// </summary>
	public bool doublePressJustReleased { get { return justReleased && lastPressWasDouble; } }

	/// <summary>
	/// Returns true if the last press has lasted longer than hSettings.longPressDuration seconds 
	/// (including current press if the input is pressed). Returns false otherwise.
	/// </summary>
	public bool lastPressWasLong { get { return (lastPressed - lastPressStart) >= hSettings.longPressDuration; }}

	/// <summary>
	/// Returns true if the input is currently pressed 
	/// and the press has lasted longer than hSettings.longPressDuration seconds. 
	/// Returns false otherwise.
	/// </summary>
	public bool longPress { get { return pressed && lastPressWasLong; } }

	/// <summary>
	/// Returns true if the input is currently justReleased, 
	/// and the last press has lasted longer than hSettings.longPressDuration seconds. 
	/// Returns false otherwise.
	/// </summary>
	public bool longPressJustReleased { get { return justReleased && lastPressWasLong; } }

	/// <summary>
	/// If the input is pressed, returns the amount of time that has passed since it is pressed. 
	/// Returns 0 otherwise.
	/// </summary>
	public float pressDuration { get { if (pressed) return (Time.unscaledTime - lastPressStart); return 0f; } }

	/// <summary>
	/// If the input is released, returns the amount of time that has passed since it is released. 
	/// Returns 0 otherwise.
	/// </summary>
	public float releaseDuration { get { if (released) return (Time.unscaledTime - lastPressed); return 0f; } }
}