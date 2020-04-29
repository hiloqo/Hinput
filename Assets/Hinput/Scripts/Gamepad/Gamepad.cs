using System.Collections.Generic;
using UnityEngine;
using HinputClasses.Internal;

namespace HinputClasses {
	/// <summary>
	/// Hinput class representing a gamepad.
	/// </summary>
	public class Gamepad {
		// --------------------
		// ID
		// --------------------

		/// <summary>
		/// Returns the real index of a gamepad in the gamepad list of Hinput.
		/// </summary>
		/// <remarks>
		/// If this is anyGamepad, returns -1.
		/// </remarks>
		public readonly int internalIndex;

		/// <summary>
		/// Returns the index of a gamepad in the gamepad list of Hinput.
		/// </summary>
		/// <remarks>
		/// If this is anyGamepad, returns the index of the gamepad that is currently being pressed.
		/// </remarks>
		public virtual int index { get { return internalIndex; } }

		/// <summary>
		/// Returns the real name of a gamepad, like "Gamepad1".
		/// </summary>
		/// <remarks>
		/// If this is anyGamepad, returns "AnyGamepad".
		/// </remarks>
		public readonly string internalName;
		
		/// <summary>
		/// Returns the name of a gamepad, like "Gamepad1".
		/// </summary>
		/// <remarks>
		/// If this is anyGamepad, returns the name of the gamepad that is currently being pressed.
		/// </remarks>
		public virtual string name { get { return internalName; } }

		/// <summary>
		/// Returns the real full name of a gamepad, like "Windows_Gamepad4".
		/// </summary>
		/// <remarks>
		/// If this is anyGamepad, returns the full name of anyGamepad, like "Windows_AnyGamepad".
		/// </remarks>
		public readonly string internalFullName;

		/// <summary>
		/// Returns the full name of a gamepad, like "Windows_Gamepad4".
		/// </summary>
		/// <remarks>
		/// If this is anyGamepad, returns the full name of the gamepad that is currently being pressed.
		/// </remarks>
		public virtual string fullName { get { return internalFullName; } }
		
		/// <summary>
		/// Returns the type of a gamepad, like "Xbox One For Windows".
		/// </summary>
		/// <remarks>
		/// If this is anyGamepad, returns the type of the gamepad that is currently being pressed.
		/// </remarks>
		public virtual string type {
			get {
				if (Input.GetJoystickNames().Length <= index) return "";
				return Input.GetJoystickNames()[index];
			}
		}
		
		private bool hasBeenConnected = false;
		/// <summary>
		/// Returns true if a gamepad is currently connected. Returns false otherwise.
		/// </summary>
		/// <remarks>
		/// If this is anyGamepad, returns true if a gamepad is connected. Returns false otherwise.
		/// </remarks>
		public virtual bool isConnected { get { return type != ""; } }
		
		
		// --------------------
		// ENABLED
		// --------------------

		/// <summary>
		/// Returns true if a gamepad is being tracked by Hinput. Returns false otherwise.
		/// </summary>
		/// <remarks>
		/// If this is anyGamepad, returns true if anyGamepad is being tracked by Hinput. Returns false otherwise.
		/// </remarks>
		public bool internalIsEnabled { get; private set; }
		
		/// <summary>
		/// Returns true if a gamepad is being tracked by Hinput. Returns false otherwise.
		/// </summary>
		/// <remarks>
		/// If this is anyGamepad, returns true if the gamepad that is currently being pressed is being tracked by
		/// Hinput. Returns false otherwise.
		/// </remarks>
		public virtual bool isEnabled { get { return internalIsEnabled; } }
		
		/// <summary>
		/// Enable a gamepad so that Hinput starts tracking it. This method is called automatically on a gamepad the
		/// first time it is connected.
		/// </summary>
		public void Enable() {
			internalIsEnabled = true;
		}

		/// <summary>
		/// Reset and disable a gamepad so that Hinput stops tracking it. This may improve performances.
		/// </summary>
		public void Disable() {
			Reset();
			internalIsEnabled = false;
		}
		
		/// <summary>
		/// Reset the position of a gamepad and erase its history.
		/// </summary>
		public void Reset() {
			buttons.ForEach(button => button.Reset());
			sticks.ForEach(stick => stick.Reset());
			anyInput.Reset();
			StopVibration();
		}


		// --------------------
		// CONSTRUCTOR
		// --------------------

		public Gamepad (int index) {
			internalIndex = index;

			if (index == -1) internalName = "AnyGamepad";
			else internalName = "Gamepad" + index;
			
			internalFullName = Utils.os + "_" + internalName;
			internalIsEnabled = false;
			
			A = new Button ("A", this, 0, !Settings.disableA); 
			B = new Button ("B", this, 1, !Settings.disableB);
			X = new Button ("X", this, 2, !Settings.disableX);
			Y = new Button ("Y", this, 3, !Settings.disableY);
			
			leftBumper = new Button ("LeftBumper", this, 4, !Settings.disableLeftBumper);
			rightBumper = new Button ("RightBumper", this, 5, !Settings.disableRightBumper);
			leftTrigger = new Trigger ("LeftTrigger", this, 6, !Settings.disableLeftTrigger);
			rightTrigger = new Trigger ("RightTrigger", this, 7, !Settings.disableRightTrigger);
			
			back = new Button ("Back", this, 8, !Settings.disableBack);
			start = new Button ("Start", this, 9, !Settings.disableStart);
			leftStickClick = new Button ("LeftStickClick", this, 10, !Settings.disableLeftStickClick);
			rightStickClick = new Button ("RightStickClick", this, 11, !Settings.disableRightStickClick);
			xBoxButton = new Button ("XBoxButton", this, 12, !Settings.disableXBoxButton);

			if (index == -1) {
				leftStick = new AnyGamepadStick ("LeftStick", this, 0);
				rightStick = new AnyGamepadStick ("RightStick", this, 1);
				dPad = new AnyGamepadStick ("DPad", this, 2);
			} else {
				leftStick = new Stick ("LeftStick", this, 0, !Settings.disableLeftStick);
				rightStick = new Stick ("RightStick", this, 1, !Settings.disableRightStick);
				dPad = new Stick ("DPad", this, 2, !Settings.disableDPad);
			}
			
			sticks = new List<Stick> { leftStick, rightStick, dPad };
			buttons = new List<Pressable> {
				A, B, X, Y,
				leftBumper, rightBumper, leftTrigger, rightTrigger,
				back, start, leftStickClick, rightStickClick, xBoxButton
			};
			
			anyInput = new AnyInput("AnyInput", this, !Settings.disableAnyInput);
			
			vibration = new Vibration (index);
		}

		
		// --------------------
		// UPDATE
		// --------------------

		public void Update () {
			if (internalIndex >= Settings.amountOfGamepads) return;
			if (internalIndex == -1 && Settings.disableAnyGamepad) return;

			//Do not update if this gamepad has never been connected.
			if (!internalIsEnabled) {
				if (isConnected && !hasBeenConnected) {
					hasBeenConnected = true;
					Enable();
				}
				else return;
			}

			buttons.ForEach(button => button.Update());
			sticks.ForEach(stick => stick.Update());
			anyInput.Update();
			vibration.Update();
		}

		
		// --------------------
		// PUBLIC PROPERTIES
		// --------------------

		/// <summary>
		/// The A button of a gamepad.
		/// </summary>
		public readonly Button A;

		/// <summary>
		/// The B button of a gamepad.
		/// </summary>
		public readonly Button B;

		/// <summary>
		/// The X button of a gamepad.
		/// </summary>
		public readonly Button X;

		/// <summary>
		/// The Y button of a gamepad.
		/// </summary>
		public readonly Button Y;

		/// <summary>
		/// The left bumper of a gamepad.
		/// </summary>
		public readonly Button leftBumper;

		/// <summary>
		/// The right bumper of a gamepad.
		/// </summary>
		public readonly Button rightBumper;

		/// <summary>
		/// The back button of a gamepad.
		/// </summary>
		public readonly Button back;

		/// <summary>
		/// The start button of a gamepad.
		/// </summary>
		public readonly Button start;

		/// <summary>
		/// The left stick click of a gamepad.
		/// </summary>
		public readonly Button leftStickClick;

		/// <summary>
		/// The right stick click of a gamepad.
		/// </summary>
		public readonly Button rightStickClick;

		/// <summary>
		/// The xBox button of a gamepad.<br/>
		/// Windows and Linux drivers can’t detect the value of this button. 
		/// Therefore it will be considered released at all times on these operating systems.
		/// </summary>
		public readonly Button xBoxButton;

		/// <summary>
		/// The left trigger of a gamepad.
		/// </summary>
		public readonly Trigger leftTrigger;
		
		/// <summary>
		/// The right trigger of a gamepad.
		/// </summary>
		public readonly Trigger rightTrigger;

		/// <summary>
		/// The left stick of a gamepad.
		/// </summary>
		public readonly Stick leftStick;

		/// <summary>
		/// The right stick click of a gamepad.
		/// </summary>
		public readonly Stick rightStick;
		
		/// <summary>
		/// The D-pad of a gamepad.
		/// </summary>
		public readonly Stick dPad;

		/// <summary>
		/// The list containing a gamepad’s sticks, in the following order : { leftStick, rightStick, dPad }
		/// </summary>
		public readonly List<Stick> sticks;

		/// <summary>
		/// The list containing a gamepad’s buttons, in the following order : { A, B, X, Y, left bumper, right bumper, left
		/// trigger, right trigger, back, start, left stick click, right stick click, XBox button }
		/// </summary>
		public readonly List<Pressable> buttons;

		/// <summary>
		/// A virtual button that returns every input of a gamepad at once.
		/// It has the name and full name of the input that is currently being pushed (except if you use "internal"
		/// properties).
		/// </summary>
		public readonly AnyInput anyInput;

		// --------------------
		// VIBRATION
		// --------------------
		
		private readonly Vibration vibration;
		
		/// <summary>
		/// Vibrate a gamepad. Default duration and intensity can be tweaked in settings.
		/// </summary>
		public void Vibrate() {
			vibration.Vibrate(
				Settings.vibrationDefaultLeftIntensity, 
				Settings.vibrationDefaultRightIntensity, 
				Settings.vibrationDefaultDuration);
		}

		/// <summary>
		/// Vibrate a gamepad for duration seconds. Default intensity can be tweaked in settings. 
		/// </summary>
		public void Vibrate(float duration) {
			vibration.Vibrate(
				Settings.vibrationDefaultLeftIntensity, 
				Settings.vibrationDefaultRightIntensity, 
				duration);
		}
		
		/// <summary>
		/// Vibrate a gamepad with an instensity of leftIntensity on the left motor, and an intensity of rightIntensity on
		/// the right motor. Default duration can be tweaked in settings.
		/// </summary>
		public void Vibrate(float leftIntensity, float rightIntensity) {
			vibration.Vibrate(
				leftIntensity, 
				rightIntensity, 
				Settings.vibrationDefaultDuration);
		}
		
		/// <summary>
		/// Vibrate a gamepad for duration seconds with an instensity of leftIntensity on the left motor, and an intensity
		/// of rightIntensity on the right motor.
		/// </summary>
		public void Vibrate(float leftIntensity, float rightIntensity, float duration) {
			vibration.Vibrate(
				leftIntensity, 
				rightIntensity, 
				duration);
		}
		
		/// <summary>
		/// Vibrate a gamepad with an intensity over time based on an animation curve.
		/// </summary>
		public void Vibrate(AnimationCurve curve) {
			vibration.Vibrate(curve, curve);
		}
		
		/// <summary>
		/// Vibrate a gamepad with an intensity over time based on two animation curves, one for the left side and one for
		/// the right side.
		/// </summary>
		public void Vibrate(AnimationCurve leftCurve, AnimationCurve rightCurve) {
			vibration.Vibrate(leftCurve, rightCurve);
		}

		/// <summary>
		/// Vibrate a gamepad with an intensity and a duration based on a vibration preset.
		/// </summary>
		public void Vibrate(VibrationPreset vibrationPreset) {
			vibration.Vibrate(vibrationPreset, 1, 1, 1);
		}

		/// <summary>
		/// Vibrate a gamepad with an intensity and a duration based on a vibration preset.
		/// The duration of the preset is multiplied by duration.
		/// </summary>
		public void Vibrate(VibrationPreset vibrationPreset, float duration) {
			vibration.Vibrate(vibrationPreset, 1, 1, duration);
		}

		/// <summary>
		/// Vibrate a gamepad with an intensity and a duration based on a vibration preset.
		/// The left intensity of the preset is multiplied by leftIntensity, and its right intensity is multiplied by
		/// rightIntensity.
		/// </summary>
		public void Vibrate(VibrationPreset vibrationPreset, float leftIntensity, float rightIntensity) {
			vibration.Vibrate(vibrationPreset, leftIntensity, rightIntensity, 1);
		}

		/// <summary>
		/// Vibrate a gamepad with an intensity and a duration based on a vibration preset.
		/// The left intensity of the preset is multiplied by leftIntensity, its right intensity is multiplied by
		/// rightIntensity, and its duration is multiplied by duration.
		/// </summary>
		public void Vibrate(VibrationPreset vibrationPreset, float leftIntensity, float rightIntensity, float duration) {
			vibration.Vibrate(vibrationPreset, leftIntensity, rightIntensity, duration);
		}
		
		/// <summary>
		/// Vibrate a gamepad with an instensity of leftIntensity on the left motor, and an intensity of rightIntensity on
		/// the right motor, FOREVER. Don't forget to call StopVibration !
		/// </summary>
		public void VibrateAdvanced(float leftIntensity, float rightIntensity) {
			vibration.VibrateAdvanced(leftIntensity, rightIntensity);
		}

		/// <summary>
		/// Stop all vibrations on a gamepad immediately.
		/// </summary>
		public void StopVibration () {
			vibration.StopVibration(0);
		}

		/// <summary>
		/// Stop all vibrations on a gamepad progressively over duration seconds.
		/// </summary>
		public void StopVibration (float duration) {
			vibration.StopVibration(duration);
		}

		/// <summary>
		/// The intensity at which the left motor of a gamepad is currently vibrating.
		/// </summary>
		public float leftVibration { get { return vibration.currentLeft; } }

		/// <summary>
		/// The intensity at which the right motor of a gamepad is currently vibrating.
		/// </summary>
		public float rightVibration { get { return vibration.currentRight; } }
	}
}