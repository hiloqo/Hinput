using UnityEngine;

/// <summary>
/// hinput class representing a stick or D-pad as a button. It is considered pressed if the stick is pushed in any direction.
/// </summary>
public class hStickPressedZone : hPressable {
    // --------------------
    // ID
    // --------------------

    /// <summary>
    /// Returns the index of the stick a pressed zone is attached to (0 for a left stick, 1 for a right stick, 2 for a D-pad).
    /// </summary>
    public int stickIndex { get; }
	
    /// <summary>
    /// Returns the real stick a pressed zone is attached to.
    /// </summary>
    /// <remarks>
    /// If this pressed zone is attached to anyGamepad, returns the corresponding stick on anyGamepad.
    /// </remarks>
    public hStick internalStick { get { return internalGamepad.sticks[stickIndex]; } }

    /// <summary>
    /// Returns the stick a pressed zone is attached to.
    /// </summary>
    /// <remarks>
    /// If this pressed zone is attached to anyGamepad, returns the corresponding stick on the gamepad that is
    /// currently being pressed.
    /// </remarks>
    public hStick stick { get { return gamepad.sticks[stickIndex]; } }
	
    /// <summary>
    /// Returns the real full name of the real stick a pressed zone is attached to.
    /// </summary>
    /// <remarks>
    /// If this pressed zone is attached to anyGamepad, returns something like "Linux_AnyGamepad_RightStick".
    /// </remarks>
    public string internalStickFullName { get { return internalStick.internalFullName; } }
    
    /// <summary>
    /// Returns the full name of the stick a pressed zone is attached to.
    /// </summary>
    /// <remarks>
    /// If this presed zone is attached to anyGamepad, returns the name of the corresponding stick on the gamepad that is
    /// currently being pressed.
    /// </remarks>
    public string stickFullName { get { return stick.fullName; } }

    public override string fullName { get { return stick.fullName + "_" + name; } }


    // --------------------
    // CONSTRUCTOR
    // --------------------

    public hStickPressedZone(string name, hStick internalStick) : 
        base(name, internalStick.internalGamepad, internalStick.internalFullName + "_" + name) {
        stickIndex = internalStick.index;
    }

	
    // --------------------
    // UPDATE
    // --------------------

    protected override void UpdatePositionRaw() {
        positionRaw = Mathf.Clamp01(stick.distanceRaw/hSettings.stickPressedZone);
    }


    // --------------------
    // PROPERTIES
    // --------------------
    
    /// <summary>
    /// Returns the relative distance between the current stick's position and the start of its pressed zone (between 0 and 1).
    /// Returns 1 if it is in its pressed zone.
    /// </summary>
    public override float position { get { return Mathf.Clamp01(stick.distance/hSettings.stickPressedZone); } }
    
    /// <summary>
    /// Returns true if the stick is inPressedZone. Returns false otherwise.
    /// </summary>
    public override bool pressed { get { return position >= 1f; } }
    
    /// <summary>
    /// Returns true if the stick is inDeadZone. Returns false otherwise.
    /// </summary>
    public override bool inDeadZone { get { return stick.inDeadZone; } }
}