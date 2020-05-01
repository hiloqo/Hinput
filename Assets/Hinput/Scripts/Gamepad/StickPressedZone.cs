using UnityEngine;

namespace HinputClasses {
    /// <summary>
    /// Hinput class representing a stick or D-pad as a button. It is considered pressed if the stick is pushed in any
    /// direction.
    /// </summary>
    public class StickPressedZone : Pressable {
        // --------------------
        // ID
        // --------------------

        /// <summary>
        /// The stick a pressed zone is attached to.
        /// </summary>
        public Stick stick { get; }


        // --------------------
        // CONSTRUCTOR
        // --------------------

        public StickPressedZone(string name, Stick stick) : 
            base(name, stick.gamepad, stick.fullName + "_" + name, true) {
            this.stick = stick;
        }

	    
        // --------------------
        // UPDATE
        // --------------------

        protected override float GetPositionRaw() { return Mathf.Clamp01(stick.distanceRaw/Settings.stickPressedZone); }
        protected override float GetPosition() { return Mathf.Clamp01(stick.distance/Settings.stickPressedZone); }
        protected override bool GetPressed() { return position >= 1f; }
        protected override bool GetInDeadZone() { return stick.inDeadZone; }
    }
}