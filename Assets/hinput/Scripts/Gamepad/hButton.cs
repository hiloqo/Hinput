/// <summary>
/// hinput class representing a physical button of the controller, such as the A button, the bumpers or the stick clicks.
/// </summary>
public class hButton : hPressable {
	// --------------------
	// NAME
	// --------------------
	
	/// <summary>
	/// Returns the index of the button on its gamepad.
	/// </summary>
	public int index { get; }
	
	
	// --------------------
	// CONSTRUCTOR
	// --------------------

	public hButton(string name, hGamepad gamepad, int index) : 
		base(name, gamepad.fullName + "_" + name, gamepad.index) {
		this.index = index;
	}

	
	// --------------------
	// UPDATE
	// --------------------

	protected override void UpdatePositionRaw() {
		try {
			if (hUtils.GetButton(fullName, (name !="XBoxButton"))) positionRaw = 1;
			else positionRaw = 0;
		} catch {
			positionRaw = 0;
		}
	}


	// --------------------
	// PROPERTIES
	// --------------------
	
	/// <summary>
	/// Returns 1 if the button is currently pressed. Returns 0 otherwise.
	/// </summary>
	public override float position { get { return positionRaw; } }

	/// <summary>
	/// Returns true if the button is currently pressed. Returns false otherwise.
	/// </summary>
	public override bool pressed { get { return position.IsEqualTo(1); } }

	/// <summary>
	/// Returns true if the button is released. Returns false otherwise.
	/// </summary>
	public override bool inDeadZone { get { return !pressed; } }
}