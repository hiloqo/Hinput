namespace HinputClasses.Internal {
	// Hinput class measuring a stick axis, and feeding it to a Stick.
    public class Axis {
     	// --------------------
     	// ID
     	// --------------------
 
     	private readonly string fullAxisName;
     	private readonly string fullPositiveButtonName;
     	private readonly string fullNegativeButtonName;
 
 
     	// --------------------
     	// CONSTRUCTORS
     	// --------------------
 
     	// D-pad constructor
     	public Axis (string fullAxisName, string fullPositiveButtonName, string fullNegativeButtonName) {
     		this.fullAxisName = fullAxisName;
     		this.fullPositiveButtonName = fullPositiveButtonName;
     		this.fullNegativeButtonName = fullNegativeButtonName;

            if (Utils.os == "Windows") AxisType = AT.Axis;
            if (Utils.os == "Mac") AxisType = AT.Buttons;
        }
 
     	// left/right stick constructor
     	public Axis (string fullAxisName) {
     		this.fullAxisName = fullAxisName;
     		fullPositiveButtonName = "";
     		fullNegativeButtonName = "";
            
            AxisType = AT.Axis;
        }
 
     	
     	// --------------------
     	// PROPERTIES
     	// --------------------
 
        private enum AT { None, Axis, Buttons }
        
     	// The D-pad will be recorded as two axes or four buttons, depending on the gamepad driver used.
     	// Measure both the axes and the buttons, and ignore the one that returns an error.
        private AT AxisType = AT.None;
     	private float _positionRaw;
     	public float positionRaw { 
     		get {
	            if (AxisType == AT.Axis) return GetAxisValue();
	            if (AxisType == AT.Buttons) return GetButtonValue();

	            float value = GetAxisValue();
	            if (value.IsNotEqualTo(0))AxisType = AT.Axis;
	            else {
		            value = GetButtonValue();
		            if (value.IsNotEqualTo(0)) AxisType = AT.Buttons;
	            }

	            return value;
            } 
     	}

        private float GetAxisValue() {
	        return Utils.GetAxis(fullAxisName, false);
        }

        private float GetButtonValue() {
	        float buttonValue = 0f;
	        if (Utils.GetButton(fullPositiveButtonName, false)) buttonValue += 1;
	        if (Utils.GetButton(fullNegativeButtonName, false)) buttonValue -= 1;
	        return buttonValue;
        }
    }
}