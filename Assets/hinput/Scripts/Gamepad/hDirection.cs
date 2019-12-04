/// <summary>
/// hinput class representing a given direction of a stick or D-pad, such as the up or down-left directions.
/// </summary>
public class hDirection : hPressable {
	// --------------------
	// ID
	// --------------------

	/// <summary>
	/// Returns the index of the stick a direction is attached to (0 for a left stick, 1 for a right stick, 2 for a
	/// D-pad).
	/// </summary>
	public int stickIndex { get; }
	
	/// <summary>
	/// Returns the real stick a direction is attached to.
	/// </summary>
	/// <remarks>
	/// If this direction is attached to anyGamepad, returns the corresponding stick on anyGamepad.
	/// </remarks>
	public hStick internalStick { get { return internalGamepad.sticks[stickIndex]; } }

	/// <summary>
	/// Returns the stick a direction is attached to.
	/// </summary>
	/// <remarks>
	/// If this direction is attached to anyGamepad, returns the corresponding stick on the gamepad that is currently
	/// being pressed.
	/// </remarks>
	public hStick stick { get { return gamepad.sticks[stickIndex]; } }
	
	/// <summary>
	/// Returns the real full name of the real stick a direction is attached to.
	/// </summary>
	/// <remarks>
	/// If this direction is attached to anyGamepad, returns something like "Linux_AnyGamepad_RightStick".
	/// </remarks>
	public string internalStickFullName { get { return internalStick.internalFullName; } }
	
	/// <summary>
	/// Returns the full name of the stick a direction is attached to.
	/// </summary>
	/// <remarks>
	/// If a direction is attached to anyGamepad, returns the name of the appropriate stick on the gamepad that is
	/// currently being pressed.
	/// </remarks>
	public string stickFullName { get { return stick.fullName; } }

	public override string fullName { get { return stick.fullName + "_" + name; } }

	/// <summary>
	/// Returns the value of the angle that defines a direction (In degrees : left=180, up=90, right=0, down=-90).
	/// </summary>
	public float angle { get; }


	// --------------------
	// CONSTRUCTOR
	// --------------------

	public hDirection (string name, float angle, hStick internalStick) : 
		base(name, internalStick.internalGamepad, internalStick.internalFullName + "_" + name) {
		stickIndex = internalStick.index;
		this.angle = angle;
	}

	
	// --------------------
	// UPDATE
	// --------------------

	protected override void UpdatePositionRaw() {
		positionRaw = hUtils.DotProduct (stick.positionRaw, stick.angleRaw);
	}


	// --------------------
	// PROPERTIES
	// --------------------
	
	/// <summary>
	/// Returns the position of the stick along a direction, between -1 and 1. 
	/// </summary>
	public override float position { get { return hUtils.DotProduct (stick.position, stick.angle); } }

	/// <summary>
	/// Returns true if the stick is inPressedZone, and pointing towards angle.
	/// Returns false otherwise.
	/// </summary>
	/// <remarks>
	/// The width of this virtual button can be changed with the directionAngle property of hSettings.
	/// </remarks>
	public override bool pressed { get { return (stick.inPressedZone && hUtils.StickWithinAngle(stick, angle)); } }

	/// <summary>
	/// Returns true if the stick is inDeadZone, or not pointing towards angle.
	/// Returns false otherwise.
	/// </summary>
	/// <remarks>
	/// The width of this virtual button can be changed with the directionAngle property of hSettings.
	/// </remarks>
	public override bool inDeadZone { get { return (stick.inDeadZone || ! hUtils.StickWithinAngle(stick, angle)); } }
}