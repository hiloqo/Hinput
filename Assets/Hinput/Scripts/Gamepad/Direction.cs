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

		protected override void UpdatePositionRaw() {
			positionRaw = Utils.DotProduct (stick.positionRaw, stick.angleRaw);
		}


		// --------------------
		// PROPERTIES
		// --------------------
		
		public override float position { get { return Utils.DotProduct (stick.position, stick.angle); } }
		public override bool pressed { get { return (stick.inPressedZone && Utils.StickWithinAngle(stick, angle)); } }
		public override bool inDeadZone { get { return (stick.inDeadZone || !Utils.StickWithinAngle(stick, angle)); } }
	}
}