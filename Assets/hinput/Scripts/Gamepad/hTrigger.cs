/// <summary>
/// hinput class representing the left or right trigger of a controller.
/// </summary>
public class hTrigger : hPressable {
	// --------------------
	// NAME
	// --------------------
	
	/// <summary>
	/// Returns the index of the trigger on its gamepad.
	/// </summary>
	public int index { get; }
	
	
	// --------------------
	// CONSTRUCTOR
	// --------------------

	public hTrigger (string name, hGamepad gamepad, int index) : 
		base(name, gamepad.fullName+"_"+name, gamepad.index) {
		this.index = index;
		initialValue = measuredPosition;
	}


	// --------------------
	// INITIAL VALUE
	// --------------------
	
	private readonly float initialValue;
	private bool hasBeenMoved;

	// The value of the trigger's position, given by the gamepad driver.
	// In some instances, until an input is recorded triggers will have a non-zero measured resting position.
	private float measuredPosition { 
		get {
			if (hUtils.os == "Windows") return hUtils.GetAxis(fullName);
			return (hUtils.GetAxis(fullName) + 1)/2;	
		}
	}

	
	// --------------------
	// UPDATE
	// --------------------

	// If no input have been recorded before, make sure the resting position is zero
	// Else just return the measured position.
	protected override void UpdatePositionRaw() {
		float measuredPos = measuredPosition;

		if (hasBeenMoved) {
			positionRaw = measuredPos;
		} else if (measuredPos.IsNotEqualTo(initialValue)) {
			hasBeenMoved = true;
			positionRaw = measuredPos;
		}
		else positionRaw = 0f;
	}


	// --------------------
	// PROPERTIES
	// --------------------

	/// <summary>
	/// Returns the position of the trigger, between 0 and 1.
	/// </summary>
	public override float position { 
		get { 
			float posRaw = positionRaw;

			if (posRaw < hSettings.triggerDeadZone) return 0f;
			else return ((posRaw - hSettings.triggerDeadZone)/(1 - hSettings.triggerDeadZone));
		} 
	}

	/// <summary>
	/// Returns true if the position of the trigger is beyond hSettings.triggerPressedZone. Returns false otherwise.
	/// </summary>
	public override bool pressed { get { return position >= hSettings.triggerPressedZone; } }

	/// <summary>
	/// Returns true if if the position of the trigger is within hSettings.triggerDeadZone. Returns false otherwise.
	/// </summary>
	public override bool inDeadZone { get { return position < hSettings.triggerDeadZone; } }
}