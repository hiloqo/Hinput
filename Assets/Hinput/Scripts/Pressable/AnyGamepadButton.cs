using System.Collections.Generic;
using System.Linq;

namespace HinputClasses.Internal {
    // --------------------
    // CONSTRUCTOR
    // --------------------
    public class AnyGamepadButton : Button {
        public AnyGamepadButton(string name, Gamepad gamepad, int index, bool isEnabled) :
            base(name, gamepad, index, isEnabled) {
            buttons = Hinput.gamepad.Select(g => g.buttons[index]).ToList();
        }


        // --------------------
        // STICKS
        // --------------------

        // Every stick of this type
        private readonly List<Pressable> buttons;


        // --------------------
        // UPDATE
        // --------------------

        protected override float GetPosition() { return buttons.Select(button => button.position).Max(); }
    }
}
