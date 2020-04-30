using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HinputClasses {
    /// <summary>
    /// Hinput class representing every input of a controller at once
    /// </summary>
    public class AnyInput : Pressable {
        // --------------------
        // CONSTRUCTOR
        // --------------------

        public AnyInput(string name, Gamepad gamepad, bool isEnabled) : 
            base(name, gamepad, gamepad.fullName + "_" + name, isEnabled) {
            inputs = new List<Pressable> {
                gamepad.A, gamepad.B, gamepad.X, gamepad.Y,
                gamepad.leftBumper, gamepad.rightBumper, 
                gamepad.leftTrigger, gamepad.rightTrigger,
                gamepad.back, gamepad.start, 
                gamepad.leftStickClick, gamepad.rightStickClick, gamepad.xBoxButton,
                gamepad.leftStick, gamepad.rightStick, gamepad.dPad
            };
        }
        
        
        // --------------------
        // INPUTS
        // --------------------
        
        // Every input on this gamepad
        private readonly List<Pressable> inputs;

        private List<Pressable> _activeInputs;
        private int _lastActiveInputsUpdateFrame = -1;
        /// <summary>
        /// The list of all inputs that are currently being pressed on a gamepad.
        /// </summary>
        public List<Pressable> activeInputs {
            get {
                if (_lastActiveInputsUpdateFrame == Time.frameCount) return _activeInputs;
                
                _activeInputs = inputs.Where(input => input.pressed).ToList();
                _lastActiveInputsUpdateFrame = Time.frameCount;
                return _activeInputs;
            }
        }
        

        // --------------------
        // UPDATE
        // --------------------

        protected override void UpdatePositionRaw() {
            positionRaw = inputs.Select(input => input.positionRaw).Max();
        }


        // --------------------
        // PROPERTIES
        // --------------------
        
        public override float position { get { return inputs.Select(input => input.position).Max(); } }
        public override bool pressed { get { return inputs.Any(input => input.pressed); } }
        public override bool inDeadZone { get { return inputs.All(input => input.inDeadZone); } }
    }
}