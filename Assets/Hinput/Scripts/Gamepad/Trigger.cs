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
    
    	// If no input has been recorded before, make sure the resting position is zero
    	// Else just return the measured position.
    	protected override float GetPositionRaw() {
	        if (hasBeenMoved) return measuredPosition;
    		if (measuredPosition.IsEqualTo(initialValue)) return 0f;
    
    		hasBeenMoved = true;
	        return measuredPosition;
    	}
    
        protected override float GetPosition() {
	        if (positionRaw < Settings.triggerDeadZone) return 0f;
	        else return ((positionRaw - Settings.triggerDeadZone)/(1 - Settings.triggerDeadZone));
        }

        protected override bool GetPressed() { return position >= Settings.triggerPressedZone; }
        protected override bool GetInDeadZone() { return position < Settings.triggerDeadZone; }
    }
}