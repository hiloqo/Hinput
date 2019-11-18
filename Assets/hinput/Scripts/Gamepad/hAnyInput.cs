using System.Collections.Generic;
using System.Linq;

/// <summary>
/// hinput class representing every input of a controller at once
/// </summary>
public class hAnyInput : hPressable{
    // --------------------
    // CONSTRUCTOR
    // --------------------

    public hAnyInput(string name, hGamepad internalGamepad) : 
        base(name, internalGamepad, internalGamepad.internalFullName + "_" + name) {
        inputs = new List<hPressable> {
            internalGamepad.A, internalGamepad.B, internalGamepad.X, internalGamepad.Y,
            internalGamepad.leftBumper, internalGamepad.rightBumper, 
            internalGamepad.leftTrigger, internalGamepad.rightTrigger,
            internalGamepad.back, internalGamepad.start, 
            internalGamepad.leftStickClick, internalGamepad.rightStickClick, internalGamepad.xBoxButton,
            internalGamepad.leftStick, internalGamepad.rightStick, internalGamepad.dPad
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