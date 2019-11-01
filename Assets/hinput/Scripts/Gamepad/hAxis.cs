// hinput class measuring a stick axis, and feeding it to a hStick.
public class hAxis {
	// --------------------
	// NAME
	// --------------------

	private readonly string fullAxisName;
	private readonly string fullPositiveButtonName;
	private readonly string fullNegativeButtonName;


	// --------------------
	// CONSTRUCTORS
	// --------------------

	// D-pad constructor
	public hAxis (string fullAxisName, string fullPositiveButtonName, string fullNegativeButtonName) {
		this.fullAxisName = fullAxisName;
		this.fullPositiveButtonName = fullPositiveButtonName;
		this.fullNegativeButtonName = fullNegativeButtonName;
	}

	// left/right stick constructor
	public hAxis (string fullAxisName) {
		this.fullAxisName = fullAxisName;
		fullPositiveButtonName = "";
		fullNegativeButtonName = "";
	}

	
	// --------------------
	// PROPERTIES
	// --------------------

	// The D-pad will be recorded as two axes or four buttons, depending on the gamepad driver used.
	// Measure both the axes and the buttons, and ignore the one that returns an error.
	private float _positionRaw;
	public float positionRaw { 
		get {
			float axisValue = hUtils.GetAxis(fullAxisName, false);

			float buttonValue = 0f;
			if (fullPositiveButtonName != "" && fullNegativeButtonName != "") {
				if (hUtils.GetButton(fullPositiveButtonName, false)) buttonValue = 1;
				if (hUtils.GetButton(fullNegativeButtonName, false)) buttonValue = -1;
			}

			return (axisValue + buttonValue);
		} 
	}
}