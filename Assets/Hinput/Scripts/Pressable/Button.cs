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

		protected override bool GetPressed() {
			try { return Utils.GetButton(fullName, (name != "XBoxButton")); } 
			catch { /* Ignore exceptions here */ }
			return false; }
	}
}