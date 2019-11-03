using System.Collections.Generic;
using System.Linq;

/// <summary>
/// hinput class representing every input of a controller at once
/// </summary>
public class hAnyInput : hPressable{
    // --------------------
    // CONSTRUCTOR
    // --------------------

    public hAnyInput(string name, hGamepad gamepad) : 
        base(name, gamepad.fullName+"_"+name, gamepad.index) {
        inputs = new List<hPressable>() {
            gamepad.A, gamepad.B, gamepad.X, gamepad.Y,
            gamepad.leftBumper, gamepad.rightBumper, gamepad.leftTrigger, gamepad.rightTrigger,
            gamepad.back, gamepad.start, gamepad.leftStickClick, gamepad.rightStickClick, gamepad.xBoxButton,
            gamepad.leftStick, gamepad.rightStick, gamepad.dPad
        };
    }
    
    
    // --------------------
    // INPUT LIST
    // --------------------
    
    private readonly List<hPressable> inputs;

	
    // --------------------
    // UPDATE
    // --------------------

    protected override void UpdatePositionRaw() {
        positionRaw = inputs.Max(input => input.positionRaw);
    }


    // --------------------
    // PROPERTIES
    // --------------------
    
	
    /// <summary>
    /// Returns the position of the most pushed gamepad button (between 0 and 1)
    /// </summary>
    public override float position { get { return inputs.Max(input => input.position); } }

    /// <summary>
    /// Returns true if a gamepad button is currently pressed. Returns false otherwise.
    /// </summary>
    public override bool pressed { get { return inputs.Select(input => input.pressed).Contains(true); } }

    /// <summary>
    /// Returns true if all gamepad buttons are currently in dead zone. Returns false otherwise.
    /// </summary>
    public override bool inDeadZone { get { return !inputs.Select(input => input.inDeadZone).Contains(false); } }
}