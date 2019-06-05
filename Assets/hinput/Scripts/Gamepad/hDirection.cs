using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// hinput class representing a given direction of a stick or D-pad, such as the up or down-left directions.
/// </summary>
public class hDirection : hAbstractPressable {
	// --------------------
	// NAME
	// --------------------

	private int _stickIndex;
	/// <summary>
	/// Returns the index of the stick this direction is attached to (0 for a left stick, 1 for a right stick, 2 for a D-pad).
	/// </summary>
	public int stickIndex { get { return _stickIndex; } }

	/// <summary>
	/// Returns the stick this direction is attached to.
	/// </summary>
	public hStick stick { get { return gamepad.sticks[stickIndex]; } }

	private float _angle;
	/// <summary>
	/// Returns the value of the angle that defines this direction (In degrees : left=180, up=90, right=0, down=-90).
	/// </summary>
	public float angle { get { return _angle; } }


	// --------------------
	// CONSTRUCTOR
	// --------------------

	public hDirection (string name, float angle, hStick stick) {
		this._name = name;
		this._stickIndex = stick.index;
		this._fullName = stick.fullName+"_"+name;
		this._gamepadIndex = stick.gamepadIndex;
		this._angle = angle;
	}

	
	// --------------------
	// UPDATE
	// --------------------

	protected override void UpdatePositionRaw() {
		_positionRaw = DotProduct (stick.positionRaw, stick.angleRaw);
	}


	// --------------------
	// PROPERTIES
	// --------------------
	
	/// <summary>
	/// Returns the position of the stick along the direction, between -1 and 1. 
	/// </summary>
	public override float position { get { return DotProduct (stick.position, stick.angle); } }

	/// <summary>
	/// Returns true if (stick) is (inTriggerZone), and within (hinput.directionAngle) degrees of (angle). Returns false otherwise.
	/// </summary>
	public override bool pressed { get { return (stick.inTriggerZone && StickWithinAngle()); } }

	/// <summary>
	/// Returns true if (stick) is (inDeadZone), or beyond (hinput.directionAngle) degrees of (angle). Returns false otherwise.
	/// </summary>
	public override bool inDeadZone { get { return (stick.inDeadZone || ! StickWithinAngle()); } }


	// --------------------
	// USEFUL METHODS
	// --------------------

	// Returns the dot product of a stick position by a unit vector defined by an angle.
	// (i.e. the projected distance to the origin of a stick position on the line defined by the point (0,0) and an angle.)
	private float DotProduct (Vector2 position, float angle) {
		float radStickAngle = angle * Mathf.Deg2Rad;
		float sin = Mathf.Sin(radStickAngle);
		float cos = Mathf.Cos(radStickAngle);
		return Mathf.Clamp01(cos*position.x + sin*position.y);
	}

	// True if the stick is currently within a (hinput.directionAngle) degree cone from this direction
	private bool StickWithinAngle () { 
		float distanceToAngle = Mathf.Abs(Mathf.DeltaAngle(angle, stick.angle));
		float maxDistance = hSettings.directionAngle/2;
		return (distanceToAngle <= maxDistance); 
	}
}