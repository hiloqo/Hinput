using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// hinput class representing the left or right trigger of a controller.
/// </summary>
public class hTrigger : hAbstractPressable {
	// --------------------
	// CONSTRUCTOR
	// --------------------

	public hTrigger (string name, hGamepad gamepad) {
		this._name = name;
		this._gamepadIndex = gamepad.index;
		this._fullName = gamepad.fullName+"_"+name;

		initialValue = measuredPosition;
	}


	// --------------------
	// INITIAL VALUE
	// --------------------
	
	private float initialValue;
	private bool hasBeenMoved;

	// The value of the trigger's position, given by the gamepad driver.
	// In some instances, until an input is recorded triggers will have a non-zero measured resting position.
	private float measuredPosition { 
		get {
			if (hUtils.os == "Windows") return hUtils.GetAxis(fullName);
			return (hUtils.GetAxis(fullName) + 1)/2;	
		}
	}

	
	// --------------------
	// UPDATE
	// --------------------

	// If no input have been recorded before, make sure the resting position is zero
	// Else just return the measured position.
	protected override void UpdatePositionRaw() {
		float mesPos = measuredPosition;

		if (hasBeenMoved) {
			_positionRaw = mesPos;
		} else if (mesPos != initialValue) {
			hasBeenMoved = true;
			_positionRaw = mesPos;
		}
		else _positionRaw = 0f;
	}


	// --------------------
	// PROPERTIES
	// --------------------

	/// <summary>
	/// Returns the position of the trigger, between 0 and 1.
	/// </summary>
	public override float position { 
		get { 
			float posRaw = positionRaw;

			if (posRaw < hSettings.deadZone) return 0f;
			else return ((posRaw - hSettings.deadZone)/(1 - hSettings.deadZone));
		} 
	}

	/// <summary>
	/// Returns true if the position of the trigger is beyond (hinput.triggerZone). Returns false otherwise.
	/// </summary>
	public override bool pressed { get { return position >= hSettings.triggerZone; } }

	/// <summary>
	/// Returns true if if the position of the trigger is within (hinput.deadZone). Returns false otherwise.
	/// </summary>
	public override bool inDeadZone { get { return position < hSettings.deadZone; } }
}