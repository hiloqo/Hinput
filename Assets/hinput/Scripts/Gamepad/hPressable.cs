using UnityEngine;

/// <summary>
/// hinput abstract class representing anything that can be considered pressed and released. 
/// It can be an actual button, a stick click, a trigger, a stick direction...<br/>
/// If no property of the hPressable is used, it will automatically be cast to a boolean with the value pressed. 
/// For instance, hinput.gamepad[0].A will return hinput.gamepad[0].A.pressed.
/// </summary>
public abstract class hPressable {
	// --------------------
	// ID
	// --------------------

	/// <summary>
	/// Returns the real name of an input, like “A”, “LeftTrigger” or “AnyInput”.
	/// </summary>
	/// <remarks>
	/// If this is anyInput, returns "AnyInput".
	/// </remarks>
	public readonly string internalName;

	/// <summary>
	/// Returns the name of the an input, like “A”, “LeftTrigger” or “DPad_Up”.
	/// </summary>
	/// <remarks>
	/// If this is anyInput, returns the name of the input that is currently being pressed.
	/// </remarks>
	public virtual string name { get { return internalName; } }

	/// <summary>
	/// Returns the real full name of an input, like “Mac_Gamepad2_A”
	/// </summary>
	/// <remarks>
	/// If this is anyInput, returns something like "Mac_Gamepad2_AnyInput".
	/// If this is attached to anyGamepad, returns something like "Mac_AnyGamepad_A".
	/// </remarks>
	public readonly string internalFullName;

	/// <summary>
	/// Returns the full name of an input, like “Mac_Gamepad2_A”
	/// </summary>
	/// <remarks>
	/// If this is anyInput, returns the full name of the input that is currently being pressed on the
	/// gamepad this input is attached to.
	/// If this is attached to anyGamepad, returns the full name of the corresponding button on the gamepad that is
	/// currently being pressed.
	/// </remarks>
	public virtual string fullName { get { return gamepad.fullName + "_" + name; } }

	/// <summary>
	/// Returns the real gamepad an input is attached to.
	/// </summary>
	/// <remarks>
	/// If this is attached to anyGamepad, returns anyGamepad.
	/// </remarks>
	public readonly hGamepad internalGamepad;

	/// <summary>
	/// Returns the gamepad an input is attached to.
	/// </summary>
	/// <remarks>
	/// If this is attached to anyGamepad, returns the gamepad that is currently being pressed.
	/// </remarks>
	public hGamepad gamepad {
		get {
			if (internalGamepad is hAnyGamepad) return ((hAnyGamepad) internalGamepad).gamepad;
			else return internalGamepad;
		}
	}
	
	/// <summary>
	/// Returns the real full name of the real gamepad an input is attached to.
	/// </summary>
	/// <remarks>
	/// If this is attached to anyGamepad, returns something like "Mac_AnyGamepad".
	/// </remarks>
	public string internalGamepadFullName { get { return internalGamepad.internalFullName; } }
	
	/// <summary>
	/// Returns the full name of the gamepad an input is attached to.
	/// </summary>
	/// <remarks>
	/// If this is attached to anyGamepad, returns the full name of the gamepad that is currently being pressed.
	/// </remarks>
	public string gamepadFullName { get { return gamepad.fullName; } }
	
	/// <summary>
	/// Returns the real index of the real gamepad an input is attached to.
	/// </summary>
	/// <remarks>
	/// If this is attached to anyGamepad, returns -1.
	/// </remarks>
	public int internalGamepadIndex { get { return internalGamepad.internalIndex; } }

	/// <summary>
	/// Returns the index of the gamepad an input is attached to.
	/// </summary>
	/// <remarks>
	/// If this is attached to anyGamepad, returns the index of the gamepad that is currently being pressed.
	/// </remarks>
	public int gamepadIndex { get { return gamepad.index; } }

	
	// --------------------
	// IMPLICIT CONVERSION
	// --------------------

	public static implicit operator bool (hPressable hPressable) { return hPressable.pressed; }


	// --------------------
	// CONSTRUCTOR
	// --------------------

	protected hPressable(string internalName, hGamepad internalGamepad, string internalFullName) {
		this.internalName = internalName;
		this.internalFullName = internalFullName;
		this.internalGamepad = internalGamepad;
		
		lastPressed = Mathf.NegativeInfinity; // *force wave* this input was never pressed
	}

	
	// --------------------
	// ABSTRACT PROPERTIES
	// --------------------

	/// <summary>
	/// Returns the current position of an input.
	/// </summary>
	public abstract float position { get; }

	/// <summary>
	/// Returns true if an input is pressed. Returns false otherwise.
	/// </summary>
	public abstract bool pressed { get; }

	/// <summary>
	/// Returns true if an input is in its dead zone. Returns false otherwise.
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
	/// Returns the current raw position of an input, i.e. not taking the dead zone into account.
	/// </summary>
	public float positionRaw { get; protected set; }

	/// <summary>
	/// Returns true if an input is not pressed. Returns false otherwise.
	/// </summary>
	public bool released { get { return !pressed; } }

	/// <summary>
	/// Returns the date an input was last released (in seconds from the beginning of the game). 
	/// Returns 0 if it hasn't been pressed.
	/// </summary>
	public float lastReleased { get; private set; }

	/// <summary>
	/// Returns the date an input was last pressed (in seconds from the beginning of the game). 
	/// Returns 0 if it hasn't been pressed.
	/// </summary>
	public float lastPressed { get; private set; }

	/// <summary>
	/// Returns the date an input was last justPressed (in seconds from the beginning of the game). 
	/// Returns 0 if it hasn't been pressed.
	/// </summary>
	public float lastPressStart { get; private set; }

	/// <summary>
	/// Returns true if an input is currently pressed and was released last frame. Returns false otherwise.
	/// </summary>
	public bool justPressed { get { return (pressed && (lastPressed - lastReleased) <= hUpdater.maxDeltaTime); } }

	/// <summary>
	/// Returns true if an input is currently released and was pressed last frame. Returns false otherwise.
	/// </summary>
	public bool justReleased { get { return (released && (lastReleased - lastPressed) <= hUpdater.maxDeltaTime); } }

	/// <summary>
	/// Returns true if the last two presses started a short time apart (including current press if the input is
	/// pressed). Returns false otherwise.
	/// </summary>
	/// <remarks>
	/// The maximum duration of a double press can be changed with the doublePressDuration property of hSettings.
	/// </remarks>
	public bool lastPressWasDouble { get { return (lastPressStart - penultimatePressStart) <= hSettings.doublePressDuration; } }

	/// <summary>
	/// Returns true if an input is currently pressed and the last two presses started a short time apart. 
	/// Returns false otherwise.
	/// </summary>
	/// <remarks>
	/// The maximum duration of a double press can be changed with the doublePressDuration property of hSettings.
	/// </remarks>
	public bool doublePress { get { return pressed && lastPressWasDouble; } }

	/// <summary>
	/// Returns true if an input is currently justPressed and the last two presses started a short time apart. 
	/// Returns false otherwise.
	/// </summary>
	/// <remarks>
	/// The maximum duration of a double press can be changed with the doublePressDuration property of hSettings.
	/// </remarks>
	public bool doublePressJustPressed { get { return justPressed && lastPressWasDouble; } }

	/// <summary>
	/// Returns true if an input is currently justReleased and the last two presses started a short time apart. 
	/// Returns false otherwise.
	/// </summary>
	/// <remarks>
	/// The maximum duration of a double press can be changed with the doublePressDuration property of hSettings.
	/// </remarks>
	public bool doublePressJustReleased { get { return justReleased && lastPressWasDouble; } }

	/// <summary>
	/// Returns true if the last press was long (including current press if the input is pressed).
	/// Returns false otherwise.
	/// </summary>
	/// <remarks>
	/// The minimum duration of a long press can be changed with the longPressDuration property of hSettings.
	/// </remarks>
	public bool lastPressWasLong { get { return (lastPressed - lastPressStart) >= hSettings.longPressDuration; }}

	/// <summary>
	/// Returns true if an input is currently pressed and the press was long. 
	/// Returns false otherwise.
	/// </summary>
	/// <remarks>
	/// The minimum duration of a long press can be changed with the longPressDuration property of hSettings.
	/// </remarks>
	public bool longPress { get { return pressed && lastPressWasLong; } }

	/// <summary>
	/// Returns true if an input is currently justReleased, and the last press was long. 
	/// Returns false otherwise.
	/// </summary>
	/// <remarks>
	/// The minimum duration of a long press can be changed with the longPressDuration property of hSettings.
	/// </remarks>
	public bool longPressJustReleased { get { return justReleased && lastPressWasLong; } }

	/// <summary>
	/// If an input is pressed, returns the amount of time that has passed since it is pressed. 
	/// Returns 0 otherwise.
	/// </summary>
	public float pressDuration { get { if (pressed) return (Time.unscaledTime - lastPressStart); return 0f; } }

	/// <summary>
	/// If an input is released, returns the amount of time that has passed since it is released. 
	/// Returns 0 otherwise.
	/// </summary>
	public float releaseDuration { get { if (released) return (Time.unscaledTime - lastPressed); return 0f; } }
}