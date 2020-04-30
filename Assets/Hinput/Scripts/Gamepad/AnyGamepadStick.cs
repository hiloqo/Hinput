using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HinputClasses.Internal {
    /// <summary>
    /// Hinput class representing a given stick, such as the left stick, the right stick or the D-Pad, on every gamepad
    /// at once.
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
                if (_lastActiveSticksUpdateFrame != Time.frameCount) return _activeSticks;

                _activeSticks = sticks.Where(s => !s.inDeadZone).ToList();
                _lastActiveSticksUpdateFrame = Time.frameCount;
                return _activeSticks;
            }
        }


        // --------------------
        // PUBLIC PROPERTIES - RAW
        // --------------------

        public override float horizontalRaw {
            get {
                if (activeSticks.Count == 0) return sticks.Select(stick => stick.horizontalRaw).Average();
                else return activeSticks.Select(stick => stick.horizontalRaw).Average();
            }
        }

        public override float verticalRaw {
            get {
                if (activeSticks.Count == 0) return sticks.Select(stick => stick.verticalRaw).Average();
                else return activeSticks.Select(stick => stick.verticalRaw).Average();
            }
        }
    }
}