using UnityEngine;

namespace HinputClasses {
    /// <summary>
	/// Hinput abstract class representing anything that can be considered pressed and released. 
	/// It can be an actual button, a stick click, a trigger, a stick direction...<br/><br/>
	/// If no property of the Pressable is used, it will automatically be cast to a boolean with the value pressed. 
	/// For instance, Hinput.gamepad[0].A will return Hinput.gamepad[0].A.pressed.
	/// </summary>
	public abstract class Pressable {
		// --------------------
		// ID
		// --------------------

		/// <summary>
		/// The name of an input, like “A”, “LeftTrigger” or “AnyInput”.
		/// </summary>
		public readonly string name;
		
		/// <summary>
		/// The full name of an input, like “Mac_Gamepad0_A”, "Windows_AnyGamepad_LeftTrigger", or
		/// "Linux_Gamepad2_AnyInput".
		/// </summary>
		public readonly string fullName;
		
		/// <summary>
		/// The gamepad an input is attached to.
		/// </summary>
		public readonly Gamepad gamepad;
		
		
		// --------------------
		// ENABLED
		// --------------------
		
		/// <summary>
		/// Returns true if an input is being tracked by Hinput. Returns false otherwise.<br/><br/>
		/// On AnyInput, returns true if AnyInput is enabled (this does NOT give any information on regular inputs).
		/// Returns false otherwise.
		/// </summary>
		public bool isEnabled { get; private set; }
		
		/// <summary>
		/// Enable an input so that Hinput starts tracking it.<br/><br/>
		/// Calling this method on AnyInput only enables AnyInput.
		/// </summary>
		public void Enable() {
			isEnabled = true;
		}

		/// <summary>
		/// Reset and disable an input so that Hinput stops tracking it. <br/><br/>
		/// This may improve performance. Calling this method on AnyInput only disables AnyInput.
		/// </summary>
		public void Disable() {
			Reset();
			isEnabled = false;
		}

		/// <summary>
		/// Reset the position of an input and erase its history.<br/><br/>
		/// Calling this method on AnyInput only resets AnyInput.
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

		protected Pressable(string name, Gamepad gamepad, string fullName, bool isEnabled) {
			this.name = name;
			this.fullName = fullName;
			this.gamepad = gamepad;
			this.isEnabled = isEnabled;
			
			lastPressed = Mathf.NegativeInfinity; // *force wave* this input was never pressed
		}

		
		// --------------------
		// ABSTRACT PROPERTIES
		// --------------------

		/// <summary>
		/// The current position of an input.
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
		/// The current raw position of an input, i.e. not taking the dead zone into account.
		/// </summary>
		public float positionRaw { get; protected set; }

		/// <summary>
		/// Returns true if an input is not pressed. Returns false otherwise.
		/// </summary>
		public bool released { get { return !pressed; } }

		/// <summary>
		/// The date an input was last released (in seconds from the beginning of the game). 
		/// Returns 0 if it hasn't been pressed.
		/// </summary>
		public float lastReleased { get; private set; }

		/// <summary>
		/// The date an input was last pressed (in seconds from the beginning of the game). 
		/// Returns 0 if it hasn't been pressed.
		/// </summary>
		public float lastPressed { get; private set; }

		/// <summary>
		/// The date an input was last justPressed (in seconds from the beginning of the game). 
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
		public bool lastPressWasDouble { get { return (lastPressStart - penultimatePressStart) <= Settings.doublePressDuration; } }

		/// <summary>
		/// Returns true if an input is currently pressed and the last two presses started a short time apart. 
		/// Returns false otherwise.
		/// </summary>
		public bool doublePress { get { return pressed && lastPressWasDouble; } }

		/// <summary>
		/// Returns true if an input is currently justPressed and the last two presses started a short time apart. 
		/// Returns false otherwise.
		/// </summary>
		public bool doublePressJustPressed { get { return justPressed && lastPressWasDouble; } }

		/// <summary>
		/// Returns true if an input is currently justReleased and the last two presses started a short time apart. 
		/// Returns false otherwise.
		/// </summary>
		public bool doublePressJustReleased { get { return justReleased && lastPressWasDouble; } }

		/// <summary>
		/// Returns true if the last press was long (including current press if the input is pressed).
		/// Returns false otherwise.
		/// </summary>
		public bool lastPressWasLong { get { return (lastPressed - lastPressStart) >= Settings.longPressDuration; }}

		/// <summary>
		/// Returns true if an input is currently pressed and the press was long. 
		/// Returns false otherwise.
		/// </summary>
		public bool longPress { get { return pressed && lastPressWasLong; } }

		/// <summary>
		/// Returns true if an input is currently justReleased, and the last press was long. 
		/// Returns false otherwise.
		/// </summary>
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