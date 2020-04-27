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
		
		/// <summary>
		/// Returns true if a gamepad is currently connected. Returns false otherwise.
		/// </summary>
		/// <remarks>
		/// If this is anyGamepad, returns true if a gamepad is connected. Returns false otherwise.
		/// </remarks>
		public virtual bool isConnected { get { return type != ""; } }


		// --------------------
		// CONSTRUCTOR
		// --------------------

		public Gamepad (int index) {
			internalIndex = index;
			vibration = new Vibration (index);

			if (index == -1) internalName = "AnyGamepad";
			else internalName = "Gamepad" + index;
			
			internalFullName = Utils.os + "_" + internalName;
		}

		
		// --------------------
		// UPDATE
		// --------------------

		public void Update () {
			vibration.Update ();

			if (_A != null) _A.Update();
			if (_B != null) _B.Update();
			if (_X != null) _X.Update();
			if (_Y != null) _Y.Update();

			if (_leftBumper != null) _leftBumper.Update();
			if (_rightBumper != null) _rightBumper.Update();

			if (_back != null) _back.Update();
			if (_start != null) _start.Update();

			if (_leftStickClick != null) _leftStickClick.Update();
			if (_rightStickClick != null) _rightStickClick.Update();

			if (_xBoxButton != null) _xBoxButton.Update();


			if (_leftTrigger != null) _leftTrigger.Update();
			if (_rightTrigger != null) _rightTrigger.Update();


			if (_leftStick != null) _leftStick.Update();
			if (_rightStick != null) _rightStick.Update();


			if (_dPad != null) _dPad.Update();
			
			anyInput.Update();
		}

		
		// --------------------
		// PUBLIC PROPERTIES
		// --------------------

		private Button _A;
		/// <summary>
		/// The A button of a gamepad.
		/// </summary>
		public Button A { 
			get {
				if (_A == null) _A = new Button ("A", this, 0);
				return _A; 
			} 
		}

		private Button _B;
		/// <summary>
		/// The B button of a gamepad.
		/// </summary>
		public Button B { 
			get {
				if (_B == null) _B = new Button ("B", this, 1);
				return _B; 
			} 
		}

		private Button _X;
		/// <summary>
		/// The X button of a gamepad.
		/// </summary>
		public Button X { 
			get {
				if (_X == null) _X = new Button ("X", this, 2);
				return _X; 
			} 
		}

		private Button _Y;
		/// <summary>
		/// The Y button of a gamepad.
		/// </summary>
		public Button Y { 
			get {
				if (_Y == null) _Y = new Button ("Y", this, 3);
				return _Y; 
			} 
		}


		private Button _leftBumper;
		/// <summary>
		/// The left bumper of a gamepad.
		/// </summary>
		public Button leftBumper { 
			get {
				if (_leftBumper == null) _leftBumper = new Button ("LeftBumper", this, 4);
				return _leftBumper; 
			} 
		}

		private Button _rightBumper;
		/// <summary>
		/// The right bumper of a gamepad.
		/// </summary>
		public Button rightBumper { 
			get {
				if (_rightBumper == null) _rightBumper = new Button ("RightBumper", this, 5);
				return _rightBumper; 
			} 
		}

		private Button _back;
		/// <summary>
		/// The back button of a gamepad.
		/// </summary>
		public Button back { 
			get {
				if (_back == null) _back = new Button ("Back", this, 8);
				return _back; 
			} 
		}

		private Button _start;
		/// <summary>
		/// The start button of a gamepad.
		/// </summary>
		public Button start { 
			get {
				if (_start == null) _start = new Button ("Start", this, 9);
				return _start; 
			} 
		}

		private Button _leftStickClick;
		/// <summary>
		/// The left stick click of a gamepad.
		/// </summary>
		public Button leftStickClick { 
			get {
				if (_leftStickClick == null) _leftStickClick = new Button ("LeftStickClick", this, 10);
				return _leftStickClick; 
			} 
		}

		private Button _rightStickClick;
		/// <summary>
		/// The right stick click of a gamepad.
		/// </summary>
		public Button rightStickClick { 
			get {
				if (_rightStickClick == null) _rightStickClick = new Button ("RightStickClick", this, 11);
				return _rightStickClick; 
			} 
		}

		private Button _xBoxButton;
		/// <summary>
		/// The xBox button of a gamepad.<br/>
		/// Windows and Linux drivers can’t detect the value of this button. 
		/// Therefore it will be considered released at all times on these operating systems.
		/// </summary>
		public Button xBoxButton { 
			get {
				if (_xBoxButton == null) _xBoxButton = new Button ("XBoxButton", this, 12);
				return _xBoxButton; 
			} 
		}

		private Trigger _leftTrigger;
		/// <summary>
		/// The left trigger of a gamepad.
		/// </summary>
		public Trigger leftTrigger { 
			get {
				if (_leftTrigger == null) _leftTrigger = new Trigger ("LeftTrigger", this, 6);
				return _leftTrigger; 
			} 
		}
		
		private Trigger _rightTrigger;
		/// <summary>
		/// The right trigger of a gamepad.
		/// </summary>
		public Trigger rightTrigger { 
			get {
				if (_rightTrigger == null) _rightTrigger = new Trigger ("RightTrigger", this, 7);
				return _rightTrigger; 
			} 
		}

		protected Stick _leftStick;
		/// <summary>
		/// The left stick of a gamepad.
		/// </summary>
		public virtual Stick leftStick { 
			get {
				if (_leftStick == null) _leftStick = new Stick ("LeftStick", this, 0);
				return _leftStick; 
			} 
		}

		protected Stick _rightStick;
		/// <summary>
		/// The right stick click of a gamepad.
		/// </summary>
		public virtual Stick rightStick { 
			get {
				if (_rightStick == null) _rightStick = new Stick ("RightStick", this, 1);
				return _rightStick; 
			} 
		}
		
		protected Stick _dPad;
		/// <summary>
		/// The D-pad of a gamepad.
		/// </summary>
		public virtual Stick dPad { 
			get {
				if (_dPad == null) _dPad = new Stick ("DPad", this, 2);
				return _dPad; 
			} 
		}

		private List<Stick> _sticks;
		/// <summary>
		/// The list containing a gamepad’s sticks, in the following order : { leftStick, rightStick, dPad }
		/// </summary>
		public List<Stick> sticks { 
			get {
				if (_sticks == null) _sticks = new List<Stick> { leftStick, rightStick, dPad };
				return _sticks;
			}
		}

		private List<Pressable> _buttons;
		/// <summary>
		/// The list containing a gamepad’s buttons, in the following order : { A, B, X, Y, left bumper, right bumper, left
		/// trigger, right trigger, back, start, left stick click, right stick click, XBox button }
		/// </summary>
		public List<Pressable> buttons { 
			get {
				if (_buttons == null) _buttons = new List<Pressable> {
					A, B, X, Y,
					leftBumper, rightBumper, leftTrigger, rightTrigger,
					back, start, leftStickClick, rightStickClick, xBoxButton
				};
				return _buttons;
			}
		}

		private AnyInput _anyInput;
		/// <summary>
		/// A virtual button that returns every input of a gamepad at once.
		/// It has the name and full name of the input that is currently being pushed (except if you use "internal"
		/// properties).
		/// </summary>
		public AnyInput anyInput {
			get {
				if (_anyInput == null) _anyInput = new AnyInput("AnyInput", this);
				return _anyInput;
			}
		}

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