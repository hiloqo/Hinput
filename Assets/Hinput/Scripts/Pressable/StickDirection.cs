using HinputClasses.Internal;

namespace HinputClasses {
    /// <summary>
	/// Hinput class representing a given direction of a stick or D-pad, such as the up or down-left directions.
	/// </summary>
	public class StickDirection : StickPressable {
		// --------------------
		// ID
		// --------------------

		/// <summary>
		/// The value of the angle that defines a direction (in degrees : right=0, up=90, left=180, down=-90).
		/// </summary>
		public float angle { get; }


		// --------------------
		// CONSTRUCTOR
		// --------------------

		public StickDirection (string name, float angle, Stick stick) : 
			base(name, stick) {
			this.angle = angle;
		}

		
		// --------------------
		// UPDATE
		// --------------------

		protected override bool GetPressed() {
			return (stick.PushedTowards(angle) && stick.distance > Settings.stickPressedZone);
		}

	}
}