namespace HinputClasses.Internal {
    // A pressable from a gamepad, such as a button, a trigger or AnyInput.
    public abstract class GamepadPressable : Pressable {
        // --------------------
        // ID
        // --------------------
	
        /// <summary>
        /// The index of a button on its gamepad.
        /// </summary>
        public readonly int index;
	
	
        // --------------------
        // CONSTRUCTOR
        // --------------------
        protected GamepadPressable(string name, Gamepad gamepad, int index, bool isEnabled) :
            base(name, gamepad, gamepad.fullName + "_" + name, isEnabled) {
            this.index = index;
        }
    }
}
