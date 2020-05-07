using HinputClasses.Internal;

namespace HinputClasses {
	/// <summary>
	/// Hinput class representing a physical button of a controller, such as the A button, a bumper or a stick click.
	/// </summary>
	public class Button : GamepadPressable {
		// --------------------
		// CONSTRUCTOR
		// --------------------

		public Button(string name, Gamepad gamepad, int index, bool isEnabled) : 
			base(name, gamepad, index, isEnabled) { }

	
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