using UnityEngine;

namespace HinputClasses {
    /// <summary>
	/// Hinput abstract class representing anything that can be considered pressed and released. 
	/// It can be an actual button, a stick click, a trigger, a stick direction...<br/>
	/// If no property of the Pressable is used, it will automatically be cast to a boolean with the value pressed. 
	/// For instance, Hinput.gamepad[0].A will return Hinput.gamepad[0].A.pressed.
	/// </summary>
	public abstract class Pressable {
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
		public readonly Gamepad internalGamepad;

		/// <summary>
		/// Returns the gamepad an input is attached to.
		/// </summary>
		/// <remarks>
		/// If this is attached to anyGamepad, returns the gamepad that is currently being pressed.
		/// </remarks>
		public Gamepad gamepad {
			get {
				if (internalGamepad is AnyGamepad) return ((AnyGamepad) internalGamepad).gamepad;
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
		// ENABLED
		// --------------------
		
		/// <summary>
		/// Returns true if a button is being tracked by Hinput. Returns false otherwise.
		/// </summary>
		public bool isEnabled { get; private set; }
		
		/// <summary>
		/// Enable a button so that Hinput starts tracking it.
		/// </summary>
		public void Enable() {
			isEnabled = true;
		}

		/// <summary>
		/// Reset and disable a button so that Hinput stops tracking it. This may improve performances.
		/// </summary>
		public void Disable() {
			Reset();
			isEnabled = false;
		}

		/// <summary>
		/// Reset the position of a button and erase its history.
		/// </summary>
		public void Reset() {
			positionRaw = 0;
			penultimatePressStart = 0f;
			lastPressedFrame = 0;
			lastReleasedFrame = 0;
		}

		
		// --------------------
		// IMPLICIT CONVERSION
		// --------------------

		public static implicit operator bool (Pressable pressable) { return pressable.pressed; }


		// --------------------
		// CONSTRUCTOR
		// --------------------

		protected Pressable(string internalName, Gamepad internalGamepad, string internalFullName) {
			this.internalName = internalName;
			this.internalFullName = internalFullName;
			this.internalGamepad = internalGamepad;
			
			lastPressed = Mathf.NegativeInfinity; // *force wave* this input was never pressed
			
			Enable();
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

		private int lastPressedFrame = 0;

		private int lastReleasedFrame = 0;

		
		// --------------------
		// UPDATE
		// --------------------

		public void Update () {
			if (!isEnabled) return;
			
			UpdatePositionRaw ();

			if (pressed) {
				lastPressed = Time.unscaledTime;
				lastPressedFrame = Time.frameCount;
			} else {
				lastReleased = Time.unscaledTime;
				lastReleasedFrame = Time.frameCount;
			}

			if (justPressed) {
				penultimatePressStart = lastPressStart;
				lastPressStart = Time.unscaledTime;		
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
		public bool justPressed { get { return (pressed && (lastPressedFrame - lastReleasedFrame) == 1); } }

		/// <summary>
		/// Returns true if an input is currently released and was pressed last frame. Returns false otherwise.
		/// </summary>
		public bool justReleased { get { return (released && (lastReleasedFrame - lastPressedFrame) == 1); } }

		/// <summary>
		/// Returns true if the last two presses started a short time apart (including current press if the input is
		/// pressed). Returns false otherwise.
		/// </summary>
		/// <remarks>
		/// The maximum duration of a double press can be changed with the doublePressDuration property of Settings.
		/// </remarks>
		public bool lastPressWasDouble { get { return (lastPressStart - penultimatePressStart) <= Settings.doublePressDuration; } }

		/// <summary>
		/// Returns true if an input is currently pressed and the last two presses started a short time apart. 
		/// Returns false otherwise.
		/// </summary>
		/// <remarks>
		/// The maximum duration of a double press can be changed with the doublePressDuration property of Settings.
		/// </remarks>
		public bool doublePress { get { return pressed && lastPressWasDouble; } }

		/// <summary>
		/// Returns true if an input is currently justPressed and the last two presses started a short time apart. 
		/// Returns false otherwise.
		/// </summary>
		/// <remarks>
		/// The maximum duration of a double press can be changed with the doublePressDuration property of Settings.
		/// </remarks>
		public bool doublePressJustPressed { get { return justPressed && lastPressWasDouble; } }

		/// <summary>
		/// Returns true if an input is currently justReleased and the last two presses started a short time apart. 
		/// Returns false otherwise.
		/// </summary>
		/// <remarks>
		/// The maximum duration of a double press can be changed with the doublePressDuration property of Settings.
		/// </remarks>
		public bool doublePressJustReleased { get { return justReleased && lastPressWasDouble; } }

		/// <summary>
		/// Returns true if the last press was long (including current press if the input is pressed).
		/// Returns false otherwise.
		/// </summary>
		/// <remarks>
		/// The minimum duration of a long press can be changed with the longPressDuration property of Settings.
		/// </remarks>
		public bool lastPressWasLong { get { return (lastPressed - lastPressStart) >= Settings.longPressDuration; }}

		/// <summary>
		/// Returns true if an input is currently pressed and the press was long. 
		/// Returns false otherwise.
		/// </summary>
		/// <remarks>
		/// The minimum duration of a long press can be changed with the longPressDuration property of Settings.
		/// </remarks>
		public bool longPress { get { return pressed && lastPressWasLong; } }

		/// <summary>
		/// Returns true if an input is currently justReleased, and the last press was long. 
		/// Returns false otherwise.
		/// </summary>
		/// <remarks>
		/// The minimum duration of a long press can be changed with the longPressDuration property of Settings.
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
}