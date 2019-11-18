using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class hAnyStick : hStick {
    public override hGamepad gamepad { get { return ((hAnyGamepad)internalGamepad).gamepad; } }

    public hAnyStick(string name, hGamepad internalGamepad, int index) 
        : base(name, internalGamepad, index, true) { }


    private List<hStick> _pushedSticks;
    private float pushedSticksDate = -1;
    // Get all gamepads with this stick outside of the deadzone, or all gamepads if there are none.
    private List<hStick> pushedSticks {
        get {
            if (pushedSticksDate.IsEqualTo(Time.unscaledTime)) return _pushedSticks;
            
            List<hStick> allSticks = hinput.gamepad.Select(g => g.sticks[index]).ToList();
            List<hStick> allPushedSticks = allSticks.Where(s => !s.inDeadZone).ToList();

            if (allPushedSticks.Count == 0) _pushedSticks = allSticks;
            else _pushedSticks = allPushedSticks;

            pushedSticksDate = Time.unscaledTime;

            return _pushedSticks;
        }
    }

    public override float horizontalRaw { get { return pushedSticks.Select(stick => stick.horizontalRaw).Average(); } }
    public override float verticalRaw { get { return pushedSticks.Select(stick => stick.verticalRaw).Average(); } }
}