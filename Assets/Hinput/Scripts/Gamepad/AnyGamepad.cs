using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HinputClasses.Internal;

namespace HinputClasses {
    /// <summary>
    /// Hinput class representing every gamepad at once.
    /// </summary>
    public class AnyGamepad : Gamepad {
        // --------------------
        // ID
        // --------------------

        public override string type { get { return "AnyGamepad"; } }
        public override bool isConnected { get { return Input.GetJoystickNames().Any(name => (name != "")); } }
        
        
        // --------------------
        // CONSTRUCTOR
        // --------------------

        public AnyGamepad() {
            index = -1;
            name = "AnyGamepad";
            fullName = Utils.os + "_" + name;
            leftStick = new AnyGamepadStick("LeftStick", this, 0);
            rightStick = new AnyGamepadStick("RightStick", this, 1);
            dPad = new AnyGamepadStick("DPad", this, 2);

            vibration = new Vibration (-1);

            SetUp();
        }
		
		
        // --------------------
        // UPDATE
        // --------------------

        protected override bool DoNotUpdate() {
            return (Settings.disableAnyGamepad || !isEnabled);
        }

		// --------------------
		// VIBRATION
		// --------------------

		public override void Vibrate() {
			Hinput.gamepad.Take(4).ToList()
				.ForEach(gamepad => gamepad.Vibrate());
		}

		public override void Vibrate(float duration) {
			Hinput.gamepad.Take(4).ToList()
				.ForEach(gamepad => gamepad.Vibrate(duration));
		}
		
		public override void Vibrate(float leftIntensity, float rightIntensity) {
			Hinput.gamepad.Take(4).ToList()
				.ForEach(gamepad => gamepad.Vibrate(leftIntensity, rightIntensity));
		}
		
		public override void Vibrate(float leftIntensity, float rightIntensity, float duration) {
			Hinput.gamepad.Take(4).ToList()
				.ForEach(gamepad => gamepad.Vibrate(leftIntensity, rightIntensity, duration));
		}
		
		public override void Vibrate(AnimationCurve curve) {
			Hinput.gamepad.Take(4).ToList()
				.ForEach(gamepad => gamepad.Vibrate(curve));
		}
		
		public override void Vibrate(AnimationCurve leftCurve, AnimationCurve rightCurve) {
			Hinput.gamepad.Take(4).ToList()
				.ForEach(gamepad => gamepad.Vibrate(leftCurve, rightCurve));
		}

		public override void Vibrate(VibrationPreset vibrationPreset) {
			Hinput.gamepad.Take(4).ToList()
				.ForEach(gamepad => gamepad.Vibrate(vibrationPreset));
		}

		public override void Vibrate(VibrationPreset vibrationPreset, float duration) {
			Hinput.gamepad.Take(4).ToList()
				.ForEach(gamepad => gamepad.Vibrate(vibrationPreset, duration));
		}

		public override void Vibrate(VibrationPreset vibrationPreset, float leftIntensity, float rightIntensity) {
			Hinput.gamepad.Take(4).ToList()
				.ForEach(gamepad => gamepad.Vibrate(vibrationPreset, leftIntensity, rightIntensity));
		}

		public override void Vibrate(VibrationPreset vibrationPreset, float leftIntensity, float rightIntensity, 
			float duration) {
			Hinput.gamepad.Take(4).ToList()
				.ForEach(gamepad => gamepad.Vibrate(vibrationPreset, leftIntensity, rightIntensity, duration));
		}
		
		public override void VibrateAdvanced(float leftIntensity, float rightIntensity) {
			Hinput.gamepad.Take(4).ToList()
				.ForEach(gamepad => gamepad.VibrateAdvanced(leftIntensity, rightIntensity));
		}

		public override void StopVibration () {
			Hinput.gamepad.Take(4).ToList()
				.ForEach(gamepad => gamepad.StopVibration());
		}

		public override void StopVibration (float duration) {
			Hinput.gamepad.Take(4).ToList()
				.ForEach(gamepad => gamepad.StopVibration(duration));
		}

		public override float leftVibration { get { return -1; } }

		public override float rightVibration { get { return -1; } }
    }
}