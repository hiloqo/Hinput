namespace HinputClasses.Internal {
    // A pressable from a stick, such as a stick direction or a stick pressed zone.
    public abstract class StickPressable : Pressable {
        // --------------------
        // ID
        // --------------------

        /// <summary>
        /// The stick a button is attached to.
        /// </summary>
        public readonly Stick stick;
	
	
        // --------------------
        // CONSTRUCTOR
        // --------------------
        protected StickPressable(string name, Stick stick) :
            base(name, stick.gamepad, stick.fullName + "_" + name, true) {
            this.stick = stick;
        }

		
        // --------------------
        // UPDATE
        // --------------------
        
        protected override bool GetPressed() { return position >= Settings.stickPressedZone; }
    }
}
