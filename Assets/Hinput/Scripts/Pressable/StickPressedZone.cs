using HinputClasses.Internal;
using UnityEngine;

namespace HinputClasses {
    /// <summary>
    /// Hinput class representing a stick or D-pad as a button. It is considered pressed if the stick is pushed in any
    /// direction.
    /// </summary>
    public class StickPressedZone : StickPressable {
        // --------------------
        // CONSTRUCTOR
        // --------------------

        public StickPressedZone(string name, Stick stick) : 
            base(name, stick) { }

	    
        // --------------------
        // UPDATE
        // --------------------

        protected override float GetPosition() { return stick.distance; }
    }
}