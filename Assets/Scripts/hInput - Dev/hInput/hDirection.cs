using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hDirection : hAbstractPressable {
	// --------------------
	// NAME
	// --------------------

	private int _stickIndex;
	public int stickIndex { get { return _stickIndex; } }

	public hStick stick { get { return gamepad.sticks[stickIndex]; } }

	private float _angle;
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
		_positionRaw = ProjectedDistance (stick.positionRaw, stick.angleRaw);
	}


	// --------------------
	// PROPERTIES
	// --------------------
	
	public override float position { get { return ProjectedDistance (stick.position, stick.angle); } }
	public override bool pressed { get { return (stick.inTriggerZone && StickWithinAngle()); } }
	public override bool inDeadZone { get { return (stick.inDeadZone || ! StickWithinAngle()); } }


	// --------------------
	// USEFUL METHODS
	// --------------------

	// Returns the projected distance of a stick position on the line defined by the point (0, 0) and an angle
	private float ProjectedDistance (Vector2 position, float angle) {
		float radStickAngle = angle * Mathf.Deg2Rad;
		float sin = Mathf.Sin(radStickAngle);
		float cos = Mathf.Cos(radStickAngle);
		return Mathf.Clamp01(cos*position.x + sin*position.y);
	}

	// True if the stick is currently within a (hInput.directionAngle) degree cone from this direction
	private bool StickWithinAngle () { return (Mathf.Abs(Mathf.DeltaAngle(angle, stick.angle)) <= hInput.directionAngle/2); }
}