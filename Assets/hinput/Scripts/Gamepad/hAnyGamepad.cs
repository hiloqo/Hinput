using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class hAnyGamepad : hGamepad {
    // --------------------
    // PUSHED GAMEPADS
    // --------------------

    private List<hGamepad> _gamepads = new List<hGamepad>();
    private float _gamepadsDate;

    public List<hGamepad> gamepads {
        get {
            if (Time.unscaledTime.IsEqualTo(_gamepadsDate)) return _gamepads;
            
            _gamepadsDate = Time.unscaledTime;
            _gamepads = hinput.gamepad.Where(g => g.anyInput).ToList();
            return _gamepads;
        }
    }

    public hGamepad gamepad {
        get {
            if (gamepads.Count == 0) return this;
            else return gamepads[0];
        }
    }
    
    public List<int> indices  { get { return gamepads.Select(g => g.index).ToList(); } }
    
    
    // --------------------
    // NAME
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
            if (_leftStick == null) _leftStick = new hAnyStick ("LeftStick", this, 0);
            return _leftStick; 
        } 
    }

    public override hStick rightStick { 
        get {
            if (_rightStick == null) _rightStick = new hAnyStick ("RightStick", this, 1);
            return _rightStick; 
        }
    }
	
    public override hStick dPad { 
        get {
            if (_dPad == null) _dPad = new hAnyStick ("DPad", this, 2);
            return _dPad; 
        } 
    }
}