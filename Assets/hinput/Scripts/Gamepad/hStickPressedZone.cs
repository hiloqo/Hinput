using UnityEngine;

/// <summary>
/// hinput class representing a stick as a button. It is considered pressed if the stick is pushed in any direction.
/// </summary>
public class hStickPressedZone : hPressable {
    // --------------------
    // NAME
    // --------------------

    /// <summary>
    /// Returns the index of the stick this button is attached to (0 for a left stick, 1 for a right stick, 2 for a D-pad).
    /// </summary>
    public int stickIndex { get; }

    /// <summary>
    /// Returns the stick this button is attached to.
    /// </summary>
    public hStick stick { get { return gamepad.sticks[stickIndex]; } }


    // --------------------
    // CONSTRUCTOR
    // --------------------

    public hStickPressedZone (string name, hStick stick) : base(name, stick.fullName + "_" + name, stick.gamepadIndex) {
        stickIndex = stick.index;
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
    /// Returns the distance between the current stick's position and the end of its dead zone.
    /// </summary>
    public override float position { get { return Mathf.Clamp01(stick.distance/hSettings.stickPressedZone); } }
    
    /// <summary>
    /// Returns true if the current position of the stick is beyond a distance of hSettings.stickPressedZone of its origin. 
    /// Returns false otherwise.
    /// </summary>
    public override bool pressed { get { return position >= 1f; } }
    
    /// <summary>
    /// Returns true if the current position of the stick is within a distance of hSettings.stickDeadZone of its origin. 
    /// Returns false otherwise.
    /// </summary>
    public override bool inDeadZone { get { return stick.inDeadZone; } }
}