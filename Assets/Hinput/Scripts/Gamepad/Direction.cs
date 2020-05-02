using HinputClasses.Internal;

namespace HinputClasses {
    /// <summary>
	/// Hinput class representing a given direction of a stick or D-pad, such as the up or down-left directions.
	/// </summary>
	public class Direction : Pressable {
		// --------------------
		// ID
		// --------------------

		/// <summary>
		/// The stick a direction is attached to.
		/// </summary>
		public Stick stick { get; }

		/// <summary>
		/// The value of the angle that defines a direction (In degrees : left=180, up=90, right=0, down=-90).
		/// </summary>
		public float angle { get; }


		// --------------------
		// CONSTRUCTOR
		// --------------------

		public Direction (string name, float angle, Stick stick) : 
			base(name, stick.gamepad, stick.fullName + "_" + name, true) {
			this.stick = stick;
			this.angle = angle;
		}

		
		// --------------------
		// UPDATE
		// --------------------

		protected override float GetPositionRaw() { return Utils.DotProduct (stick.positionRaw, stick.angleRaw); }
		protected override float GetPosition() { return Utils.DotProduct (stick.position, stick.angle); }

		protected override bool GetPressed() {
			return (stick.inPressedZone.simplePress.pressed && Utils.StickWithinAngle(stick, angle));
		}
		
		protected override bool GetInDeadZone() { return (stick.inDeadZone || !Utils.StickWithinAngle(stick, angle)); }
	}
}