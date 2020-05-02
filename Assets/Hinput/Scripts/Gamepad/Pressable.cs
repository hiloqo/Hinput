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
		}

		
		// --------------------
		// IMPLICIT CONVERSIONS
		// --------------------

		public static implicit operator bool(Pressable pressable) { return (bool)(Press) pressable; }

		public static implicit operator Press(Pressable pressable) {
			if (Settings.defaultPressType == Settings.DefaultPressTypes.LongPress) return pressable.longPress;
			if (Settings.defaultPressType == Settings.DefaultPressTypes.DoublePress) return pressable.doublePress;
			return pressable.simplePress;
		}
		
		/// <summary>
		/// Returns true if an input is pressed. Returns false otherwise.
		/// </summary>
		public bool pressed { get { return ((Press)this).pressed; } }
		
		/// <summary>
		/// Returns true if an input is released. Returns false otherwise.
		/// </summary>
		public bool released { get { return ((Press)this).released; } }
		
		/// <summary>
		/// Returns true if an input has been pressed this frame. Returns false otherwise.
		/// </summary>
		public bool justPressed { get { return ((Press)this).justPressed; } }
		
		/// <summary>
		/// Returns true if an input has been released this frame. Returns false otherwise.
		/// </summary>
		public bool justReleased { get { return ((Press)this).justReleased; } }
		
		/// <summary>
		/// The time it was the last time an input was pressed.
		/// </summary>
		public float lastPressed { get { return ((Press)this).lastPressed; } }
		
		/// <summary>
		/// The time it was the last time an input was released.
		/// </summary>
		public float lastReleased { get { return ((Press)this).lastReleased; } }
		
		/// <summary>
		/// How long an input has been pressed (0 if it is released).
		/// </summary>
		public float pressDuration { get { return ((Press)this).pressDuration; } }

		/// <summary>
		/// How long an input has been released (0 if it is pushed down).
		/// </summary>
		public float releaseDuration { get { return ((Press)this).releaseDuration; } }


		// --------------------
		// CONSTRUCTOR
		// --------------------

		protected Pressable(string name, Gamepad gamepad, string fullName, bool isEnabled) {
			this.name = name;
			this.fullName = fullName;
			this.gamepad = gamepad;
			this.isEnabled = isEnabled;
			
			inDeadZone = true;
			
			simplePress = new Press(this);
			longPress = new Press(this);
			doublePress = new Press(this);
		}

		
		// --------------------
		// PRIVATE PROPERTIES
		// --------------------

		private bool isPressed = false;
		private float lastPressStart = 0f;
		private float penultimatePressStart = 0f;

		
		// --------------------
		// UPDATE
		// --------------------

		protected abstract float GetPositionRaw();
		protected abstract float GetPosition();
		protected abstract bool GetPressed();
		protected abstract bool GetInDeadZone();

		public void Update () {
			if (!isEnabled) return;
			
			bool prevPressed = isPressed;
			positionRaw = GetPositionRaw();
			position = GetPosition();
			isPressed = GetPressed();
			inDeadZone = GetInDeadZone();

			if (isPressed && !prevPressed) {
				penultimatePressStart = lastPressStart;
				lastPressStart = Time.unscaledTime;		
			}
			
			simplePress.Update(isPressed);
			longPress.Update(isPressed && Time.unscaledTime - lastPressStart > Settings.longPressDuration);
			doublePress.Update(isPressed && lastPressStart - penultimatePressStart < Settings.doublePressDuration);
		}

		
		// --------------------
		// PUBLIC PROPERTIES
		// --------------------
		
		/// <summary>
		/// The current raw position of an input, i.e. not taking the dead zone into account.
		/// </summary>
		public float positionRaw { get; private set; }

		/// <summary>
		/// The current position of an input.
		/// </summary>
		public float position { get; private set; }

		/// <summary>
		/// Returns true if an input is in its dead zone. Returns false otherwise.
		/// </summary>
		public bool inDeadZone { get; private set; }

		/// <summary>
		/// Considered pressed whenever an input is pressed.
		/// </summary>
		public readonly Press simplePress;

		/// <summary>
		/// Considered pressed when an input has been pressed twice in a row.
		/// </summary>
		public readonly Press doublePress;
		
		/// <summary>
		/// Considered pressed when an input has been pressed for a long time.
		/// </summary>
		public readonly Press longPress;
	}
}