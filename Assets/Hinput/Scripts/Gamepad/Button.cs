using HinputClasses.Internal;

namespace HinputClasses {
	/// <summary>
	/// Hinput class representing a physical button of a controller, such as the A button, a bumper or a stick click.
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

		protected override float GetPosition() {
			try { if (Utils.GetButton(fullName, (name !="XBoxButton"))) return 1; } 
			catch { /* Ignore exceptions here */ }
			return 0;
		}

		protected override bool GetPressed() { return position.IsEqualTo(1); }
	}
}