using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// hinput class representing every gamepad at once.
/// </summary>
public class hAnyGamepad : hGamepad {
    // --------------------
    // PUSHED GAMEPADS
    // --------------------

    private List<hGamepad> _gamepads = new List<hGamepad>();
    private float _gamepadsDate;

    /// <summary>
    /// Returns a list of every gamepad that is currently being pressed.
    /// </summary>
    public List<hGamepad> gamepads {
        get {
            if (Time.unscaledTime.IsEqualTo(_gamepadsDate)) return _gamepads;
            
            _gamepadsDate = Time.unscaledTime;
            _gamepads = hinput.gamepad.Where(g => g.anyInput).ToList();
            return _gamepads;
        }
    }

    /// <summary>
    /// Returns the gamepad that is currently being pressed.
    /// </summary>
    /// <remarks>
    /// If several gamepads are pressed, returns the one with the smallest index.
    /// If no gamepad is pressed, returns null.
    /// </remarks>
    public hGamepad gamepad {
        get {
            if (gamepads.Count == 0) return this;
            else return gamepads[0];
        }
    }
    
    /// <summary>
    /// Returns a list of the indices of every gamepad that is currently being pressed.
    /// </summary>
    public List<int> indices  { get { return gamepads.Select(g => g.index).ToList(); } }
    
    
    // --------------------
    // ID
    // --------------------

    public override int index {
        get {
            if (gamepads.Count == 0) return internalIndex;
            else return gamepad.index;
        }
    }

    public override string fullName {
        get {
            if (gamepads.Count == 0) return internalFullName;
            else return gamepad.fullName;
        }
    }
	
    public override string type {
        get {
            if (gamepads.Count == 0) return null;
            else return gamepad.type;
        }
    }

    public override string name {
        get {
            if (gamepads.Count == 0) return internalName;
            else return gamepad.name;
        }
    }

    // --------------------
    // CONSTRUCTOR
    // --------------------

    public hAnyGamepad() : base(-1) { }
    

    // --------------------
    // PUBLIC VARIABLES
    // --------------------
    
    public override hStick leftStick { 
        get {
            if (_leftStick == null) _leftStick = new hAnyGamepadStick ("LeftStick", this, 0);
            return _leftStick; 
        } 
    }

    public override hStick rightStick { 
        get {
            if (_rightStick == null) _rightStick = new hAnyGamepadStick ("RightStick", this, 1);
            return _rightStick; 
        }
    }
	
    public override hStick dPad { 
        get {
            if (_dPad == null) _dPad = new hAnyGamepadStick ("DPad", this, 2);
            return _dPad; 
        } 
    }
}