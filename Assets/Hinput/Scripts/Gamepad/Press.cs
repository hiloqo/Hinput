using UnityEngine;

namespace HinputClasses {
    /// <summary>
    /// Hinput class representing a specific way of pressing a button, like a regular press, a double press or a long
    /// press.
    /// </summary>
    public class Press {
        // --------------------
        // ID
        // --------------------
        
        /// <summary>
        /// The button a press refers to.
        /// </summary>
        public Pressable pressable;
        
        /// <summary>
        /// The gamepad of the button a press refers to.
        /// </summary>
        public Gamepad gamepad;
        
        
        // --------------------
        // PRIVATE PROPERTIES
        // --------------------
        
        private int lastPressedFrame = -1;
        private int lastReleasedFrame = -1;
        
        
        // --------------------
        // IMPLICIT CONVERSIONS
        // --------------------
        
        public static implicit operator bool (Press press) { return press.justPressed; }
        
        
        // --------------------
        // CONSTRUCTOR
        // --------------------

        public Press(Pressable pressable) {
            this.pressable = pressable;
            
            gamepad = pressable.gamepad;
            lastPressed = Mathf.NegativeInfinity; // *force wave* this input was never pressed
            lastReleased = Mathf.NegativeInfinity;
        }
        
        
        // --------------------
        // UPDATE
        // --------------------

        public void Update(bool isPressed) {
            pressed = isPressed;
            
            if (pressed) {
                lastPressedFrame = Time.frameCount;
                lastPressed = Time.unscaledTime;
            } else {
                lastReleasedFrame = Time.frameCount;
                lastReleased = Time.unscaledTime;
            }
        }
        
        
        // --------------------
        // PUBLIC PROPERTIES
        // --------------------
		
        /// <summary>
        /// Returns true if a press is pressed. Returns false otherwise.
        /// </summary>
        public bool pressed { get; private set; }
		
        /// <summary>
        /// Returns true if a press is released. Returns false otherwise.
        /// </summary>
        public bool released { get { return !pressed; } }
        
        /// <summary>
        /// Returns true if a press has been pressed this frame. Returns false otherwise.
        /// </summary>
        public bool justPressed { get { return (pressed && (lastPressedFrame - lastReleasedFrame) == 1); } }
		
        /// <summary>
        /// Returns true if a press has been released this frame. Returns false otherwise.
        /// </summary>
        public bool justReleased { get { return (released && (lastReleasedFrame - lastPressedFrame) == 1); } }
		
        /// <summary>
        /// The time it was the last time a press was pressed.
        /// </summary>
        public float lastPressed { get; private set; }
		
        /// <summary>
        /// The time it was the last time a press was released.
        /// </summary>
        public float lastReleased { get; private set; }

        /// <summary>
        /// How long a press has been pressed (0 if it is released).
        /// </summary>
        public float pressDuration { get { return Mathf.Clamp(lastPressed - lastReleased, 0, Mathf.Infinity); } }

        /// <summary>
        /// How long a press has been released (0 if it is pressed).
        /// </summary>
        public float releaseDuration { get { return Mathf.Clamp(lastReleased - lastPressed, 0, Mathf.Infinity); } }
    }
}
