using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HinputClasses.Internal {
    /// <summary>
    /// Hinput class representing a given type of stick, such as a left stick, a right stick, or a D-Pad, on every
    /// gamepad at once.
    /// </summary>
    public class AnyGamepadStick : Stick {
        // --------------------
        // CONSTRUCTOR
        // --------------------

        public AnyGamepadStick(string name, Gamepad gamepad, int index)
            : base(name, gamepad, index, true) {
            sticks = Hinput.gamepad.Select(g => g.sticks[index]).ToList();
        }


        // --------------------
        // STICKS
        // --------------------

        // Every stick of this type
        private readonly List<Stick> sticks;

        private List<Stick> _activeSticks;
        private int _lastActiveSticksUpdateFrame = -1;
        private List<Stick> activeSticks {
            get {
                if (_lastActiveSticksUpdateFrame == Time.frameCount) return _activeSticks;

                _activeSticks = sticks.Where(s => s.distance.IsNotEqualTo(0)).ToList();
                _lastActiveSticksUpdateFrame = Time.frameCount;
                return _activeSticks;
            }
        }


        // --------------------
        // PUBLIC PROPERTIES
        // --------------------

        private Vector2 _position;
        private int _lastPositionUpdateFrame = -1;
        public override Vector2 position {
            get {
                if (_lastPositionUpdateFrame == Time.frameCount) return _position;

                if (activeSticks.Count == 0) _position = Vector2.zero;
                else _position = new Vector2(
                    activeSticks.Average(stick => stick.horizontal),
                    activeSticks.Average(stick => stick.vertical)
                );
                
                _lastPositionUpdateFrame = Time.frameCount;
                return _position; 
            } 
        }
    }
}