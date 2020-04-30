using HinputClasses.Internal;

namespace HinputClasses {
    /// <summary>
    /// Hinput class representing the left or right trigger of a controller.
    /// </summary>
    public class Trigger : Pressable {
    	// --------------------
    	// ID
    	// --------------------
    
    	/// <summary>
    	/// The index of a trigger on its gamepad.
    	/// </summary>
    	public readonly int index;
    	
    	
    	// --------------------
    	// CONSTRUCTOR
    	// --------------------
    
    	public Trigger (string name, Gamepad gamepad, int index, bool isEnabled) : 
    		base(name, gamepad, gamepad.fullName + "_" + name, isEnabled) {
    		this.index = index;
    		initialValue = measuredPosition;
    	}
    
    
    	// --------------------
    	// INITIAL VALUE
    	// --------------------
    	
    	private readonly float initialValue;
    	private bool hasBeenMoved = false;
    
    	// The value of the trigger's position, given by the gamepad driver.
    	// In some instances, until an input is recorded triggers will have a non-zero measured resting position.
    	private float measuredPosition { 
    		get {
    			if (Utils.os == "Mac") return (Utils.GetAxis(fullName) + 1)/2;
    			else return Utils.GetAxis(fullName);	
    		}
    	}
    
    	
    	// --------------------
    	// UPDATE
    	// --------------------
    
    	// If no input have been recorded before, make sure the resting position is zero
    	// Else just return the measured position.
    	protected override void UpdatePositionRaw() {
    		float measuredPos = measuredPosition;
    
    		if (hasBeenMoved) positionRaw = measuredPos;
    		else if (measuredPos.IsEqualTo(initialValue)) positionRaw = 0f;
    		else {
	            hasBeenMoved = true;
	            positionRaw = measuredPos;
            }
    	}
    
    
    	// --------------------
    	// PROPERTIES
    	// --------------------
    
    	public override float position { 
    		get { 
	            if (positionRaw < Settings.triggerDeadZone) return 0f;
    			else return ((positionRaw - Settings.triggerDeadZone)/(1 - Settings.triggerDeadZone));
    		} 
    	}
    	public override bool pressed { get { return position >= Settings.triggerPressedZone; } }
    	public override bool inDeadZone { get { return position < Settings.triggerDeadZone; } }
    }
}