using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// hinput class representing every input of a controller at once
/// </summary>
public class hAnyInput : hPressable{
    // --------------------
    // PRESSED INPUTS
    // --------------------

    private List<hPressable> _pressedInputs;
    private float pressedInputsDate;
    public List<hPressable> pressedInputs {
        get {
            if (pressedInputsDate.IsEqualTo(Time.unscaledTime)) return _pressedInputs;
            
            _pressedInputs = inputs.Where(i => i.pressed).ToList();
            pressedInputsDate = Time.unscaledTime;
            return _pressedInputs;
        }
    }

    public hPressable pressedInput {
        get {
            if (pressedInputs.Count == 0) return null;
            
            hPressable mostPushedInput = pressedInputs[0];
            for (int i = 1; i < pressedInputs.Count; i++) {
                if (mostPushedInput.positionRaw.IsEqualTo(1)) break;
                
                hPressable currentInput = pressedInputs[i];
                if (currentInput.positionRaw > mostPushedInput.positionRaw) mostPushedInput = currentInput;
            }

            return mostPushedInput;
        }
    }


    // --------------------
    // NAME
    // --------------------

    public override string name {
        get {
            if (pressedInput == null) return internalName;
            return pressedInput.name;
        }
    }

    public readonly int internalIndex = -1;

    public int index {
        get {
            if (pressedInput == null) return internalIndex;
            if (pressedInput is hButton) return ((hButton) pressedInput).index;
            if (pressedInput is hTrigger) return ((hTrigger) pressedInput).index;

            return internalIndex;
        }
    }


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
        if (pressedInput == null) positionRaw = 0;
        else positionRaw = pressedInput.positionRaw;
    }


    // --------------------
    // PROPERTIES
    // --------------------
    
	
    /// <summary>
    /// Returns the position of the most pushed gamepad button (between 0 and 1)
    /// </summary>
    public override float position {
        get {
            if (pressedInput == null) return 0;
            else return pressedInput.position;
        }
    }

    /// <summary>
    /// Returns true if a gamepad button is currently pressed. Returns false otherwise.
    /// </summary>
    public override bool pressed {
        get {
            if (pressedInput == null) return false;
            else return pressedInput.pressed;
        }
    }

    /// <summary>
    /// Returns true if all gamepad buttons are currently in dead zone. Returns false otherwise.
    /// </summary>
    public override bool inDeadZone {
        get {
            if (pressedInput == null) return true;
            else return pressedInput.inDeadZone;
        }
    }
}