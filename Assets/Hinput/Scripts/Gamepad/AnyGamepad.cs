using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HinputClasses {
    /// <summary>
    /// Hinput class representing every gamepad at once.
    /// </summary>
    public class AnyGamepad : Gamepad {
        // --------------------
        // PUSHED GAMEPADS
        // --------------------

        private List<Gamepad> _gamepads = new List<Gamepad>();
        private int _lastGamepadUpdateFrame = -1;
        /// <summary>
        /// Returns a list of every gamepad that is currently being pressed.
        /// </summary>
        /// <remarks>
        /// If no gamepad is pressed, returns an empty list.
        /// </remarks>
        public List<Gamepad> gamepads {
            get {
                if (_lastGamepadUpdateFrame == Time.frameCount) return _gamepads;
                
                _lastGamepadUpdateFrame = Time.frameCount;
                _gamepads = Hinput.gamepad.Where(g => g.anyInput).ToList();
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
        public Gamepad gamepad {
            get {
                if (gamepads.Count == 0) return this;
                else return gamepads[0];
            }
        }
        
        /// <summary>
        /// Returns a list of the indices of every gamepad that is currently being pressed.
        /// </summary>
        /// <remarks>
        /// If no gamepad is pressed, returns an empty list.
        /// </remarks>
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

        public override string name {
            get {
                if (gamepads.Count == 0) return internalName;
                else return gamepad.name;
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

        public override bool isConnected {
            get {
                return Hinput.gamepad.Any(g => g.isConnected);
            }
        }

        // --------------------
        // CONSTRUCTOR
        // --------------------

        public AnyGamepad() : base(-1) { }
        

        // --------------------
        // PUBLIC VARIABLES
        // --------------------
        
        public override Stick leftStick { 
            get {
                if (_leftStick == null) _leftStick = new AnyGamepadStick ("LeftStick", this, 0);
                return _leftStick; 
            } 
        }

        public override Stick rightStick { 
            get {
                if (_rightStick == null) _rightStick = new AnyGamepadStick ("RightStick", this, 1);
                return _rightStick; 
            }
        }
	    
        public override Stick dPad { 
            get {
                if (_dPad == null) _dPad = new AnyGamepadStick ("DPad", this, 2);
                return _dPad; 
            } 
        }
    }
}