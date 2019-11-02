using UnityEngine;

/// <summary>
/// hinput class representing a given direction of a stick or D-pad, such as the up or down-left directions.
/// </summary>
public class hDirection : hPressable {
	// --------------------
	// NAME
	// --------------------

	/// <summary>
	/// Returns the index of the stick this direction is attached to (0 for a left stick, 1 for a right stick, 2 for a D-pad).
	/// </summary>
	public int stickIndex { get; }

	/// <summary>
	/// Returns the stick this direction is attached to.
	/// </summary>
	public hStick stick { get { return gamepad.sticks[stickIndex]; } }

	/// <summary>
	/// Returns the value of the angle that defines this direction (In degrees : left=180, up=90, right=0, down=-90).
	/// </summary>
	public float angle { get; }


	// --------------------
	// CONSTRUCTOR
	// --------------------

	public hDirection (string name, float angle, hStick stick) : 
		base(name, stick.fullName+"_"+name, stick.gamepadIndex) {
		stickIndex = stick.index;
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
	/// Returns the position of the stick along the direction, between -1 and 1. 
	/// </summary>
	public override float position { get { return hUtils.DotProduct (stick.position, stick.angle); } }

	/// <summary>
	/// Returns true if the stick is inPressedZone, and within hSettings.directionAngle degrees of angle.
	/// Returns false otherwise.
	/// </summary>
	public override bool pressed { get { return (stick.inPressedZone && hUtils.StickWithinAngle(stick, angle)); } }

	/// <summary>
	/// Returns true if the stick is inDeadZone, or beyond hSettings.directionAngle degrees of angle.
	/// Returns false otherwise.
	/// </summary>
	public override bool inDeadZone { get { return (stick.inDeadZone || ! hUtils.StickWithinAngle(stick, angle)); } }
}