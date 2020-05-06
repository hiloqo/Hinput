using HinputClasses.Internal;
using UnityEngine;

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
        
    	// The value of the trigger's position, measured by the gamepad driver.
    	private float measuredPosition { 
    		get { // Triggers range from -1 to 1 on Mac, and from 0 to 1 on Windows and Linux.
    			if (Utils.os == "Mac") return (Utils.GetAxis(fullName) + 1)/2;
    			else return Utils.GetAxis(fullName);	
    		}
    	}
    
    	
    	// --------------------
    	// UPDATE
    	// --------------------
    
        private readonly float initialValue;
        private bool hasBeenMoved = false;
    
        // In some instances, triggers have a non-zero resting position until an input is recorded.
        protected override float GetPosition() {
	        if (!hasBeenMoved) {
		        if (measuredPosition.IsEqualTo(initialValue)) return 0;
		        else hasBeenMoved = true;
	        }
	        
	        return Mathf.Clamp01((measuredPosition - Settings.triggerDeadZone)/(1 - Settings.triggerDeadZone));
        }

        protected override bool GetPressed() { return position >= Settings.triggerPressedZone; }
    }
}