using HinputClasses.Internal;

namespace HinputClasses {
	/// <summary>
	/// Hinput class representing a physical button of the controller, such as the A button, the bumpers or the stick
	/// clicks.
	/// </summary>
	public class Button : Pressable {
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

		public Button(string name, Gamepad gamepad, int index, bool isEnabled) : 
			base(name, gamepad, gamepad.fullName + "_" + name, isEnabled) {
			this.index = index;
		}

	
		// --------------------
		// UPDATE
		// --------------------

		protected override void UpdatePositionRaw() {
			try {
				if (Utils.GetButton(fullName, (name !="XBoxButton"))) positionRaw = 1;
				else positionRaw = 0;
			} catch {
				positionRaw = 0;
			}
		}


		// --------------------
		// PROPERTIES
		// --------------------
	
		public override float position { get { return positionRaw; } }
		public override bool pressed { get { return position.IsEqualTo(1); } }
		public override bool inDeadZone { get { return !pressed; } }
	}
}