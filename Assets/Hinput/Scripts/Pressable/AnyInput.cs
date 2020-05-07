using System.Collections.Generic;
using System.Linq;
using HinputClasses.Internal;

namespace HinputClasses {
    /// <summary>
    /// Hinput class representing every input of a controller at once
    /// </summary>
    public class AnyInput : GamepadPressable {
        // --------------------
        // CONSTRUCTOR
        // --------------------

        public AnyInput(string name, Gamepad gamepad, bool isEnabled) : 
            base(name, gamepad, -1, isEnabled) {
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
        

        // --------------------
        // UPDATE
        // --------------------

        protected override float GetPosition() { return inputs.Select(input => input.position).Max(); }
        protected override bool GetPressed() { return inputs.Any(input => input.simplePress.pressed); }
    }
}