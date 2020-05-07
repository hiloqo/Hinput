using System.Collections.Generic;
using System.Linq;

namespace HinputClasses.Internal {
    public class AnyGamepadButton : Button {
        // --------------------
        // CONSTRUCTOR
        // --------------------
        
        public AnyGamepadButton(string name, Gamepad gamepad, int index, bool isEnabled) :
            base(name, gamepad, index, isEnabled) {
            buttons = Hinput.gamepad.Select(g => g.buttons[index]).ToList();
        }


        // --------------------
        // BUTTONS
        // --------------------

        // Every stick of this type
        private readonly List<Pressable> buttons;


        // --------------------
        // UPDATE
        // --------------------

        protected override bool GetPressed() { return buttons.Any(button => button.simplePress.pressed); }
    }
}
