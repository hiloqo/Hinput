using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// hinput class representing every input of a controller at once
/// </summary>
public class hAnyInput : hPressable{
    // --------------------
    // CONSTRUCTOR
    // --------------------

    public hAnyInput(string name, hGamepad gamepad) : base(name, gamepad.fullName+"_"+name, gamepad.index) {
        buttons = new List<hPressable>() {
            gamepad.A, gamepad.B, gamepad.X, gamepad.Y,
            gamepad.leftBumper, gamepad.rightBumper, gamepad.leftTrigger, gamepad.rightTrigger,
            gamepad.back, gamepad.start, gamepad.leftStickClick, gamepad.rightStickClick, gamepad.xBoxButton,
            gamepad.leftStick, gamepad.rightStick, gamepad.dPad
        };
    }
    
    
    // --------------------
    // BUTTON LIST
    // --------------------
    
    private readonly List<hPressable> buttons;

	
    // --------------------
    // UPDATE
    // --------------------

    protected override void UpdatePositionRaw() {
        positionRaw = buttons.Max(button => button.positionRaw);
    }


    // --------------------
    // PROPERTIES
    // --------------------
    
	
    /// <summary>
    /// Returns the position of the most pushed gamepad button (between 0 and 1)
    /// </summary>
    public override float position { get { return buttons.Max(button => button.position); } }

    /// <summary>
    /// Returns true if a gamepad button is currently beyond hSettings.triggerPressedZone. Returns false otherwise.
    /// </summary>
    public override bool pressed { get { return buttons.Select(button => button.pressed).Contains(true); } }

    /// <summary>
    /// Returns true if all gamepad buttons are currently within hSettings.triggerDeadZone. Returns false otherwise.
    /// </summary>
    public override bool inDeadZone { get { return !buttons.Select(button => button.inDeadZone).Contains(false); } }
}