/// <summary>
/// hinput class representing a physical button of the controller, such as the A button, the bumpers or the stick clicks.
/// </summary>
public class hButton : hPressable {
	// --------------------
	// ID
	// --------------------
	
	/// <summary>
	/// Returns the real index of a button on its gamepad.
	/// </summary>
	/// <remarks>
	/// If this button is anyInput, returns -1.
	/// </remarks>
	public readonly int internalIndex;
	
	/// <summary>
	/// Returns the index of the button on its gamepad.
	/// </summary>
	/// <remarks>
	/// If this button is anyInput, returns the index of the input that is currently being pressed.
	/// </remarks>
	public int index { get { return internalIndex; } }
	
	
	// --------------------
	// CONSTRUCTOR
	// --------------------

	public hButton(string name, hGamepad internalGamepad, int internalIndex) : 
		base(name, internalGamepad, internalGamepad.internalFullName + "_" + name) {
		this.internalIndex = internalIndex;
	}

	
	// --------------------
	// UPDATE
	// --------------------

	protected override void UpdatePositionRaw() {
		try {
			if (hUtils.GetButton(internalFullName, (internalName !="XBoxButton"))) positionRaw = 1;
			else positionRaw = 0;
		} catch {
			positionRaw = 0;
		}
	}


	// --------------------
	// PROPERTIES
	// --------------------
	
	/// <summary>
	/// Returns 1 if a button is currently pressed. Returns 0 otherwise.
	/// </summary>
	public override float position { get { return positionRaw; } }

	/// <summary>
	/// Returns true if a button is currently pressed. Returns false otherwise.
	/// </summary>
	public override bool pressed { get { return position.IsEqualTo(1); } }

	/// <summary>
	/// Returns true if a button is currently released. Returns false otherwise.
	/// </summary>
	public override bool inDeadZone { get { return !pressed; } }
}