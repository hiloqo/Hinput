using UnityEngine;

namespace HinputClasses.Internal {
    // Hinput class responsible for updating gamepads. 
    // It is automatically instantiated at runtime, or added to the gameobject with the HinputSettings component if you created one.
    // You don’t need to do anything about it.
    public class Updater : MonoBehaviour {
    	// --------------------
    	// INTERNAL SETTINGS
    	// --------------------
    
    	// The last frame Hinput was updated
    	private static int lastUpdatedFrame = -1;
    	
    	
    	// --------------------
    	// SINGLETON PATTERN
    	// --------------------
    
    	//The instance of HinputSettings. Assigned when first called.
    	private static Updater _instance;
    	public static Updater instance { 
    		get {
    			CheckInstance();
    			return _instance;
    		} 
    	}
    
    	public static void CheckInstance() {
    		if (_instance != null) return;
    		
    		_instance = Settings.instance.gameObject.AddComponent<Updater>();
    	}
    
    	private void Awake () {
    		if (_instance == null) _instance = this;
    		if (_instance != this) Destroy(this);
    		DontDestroyOnLoad (this);
    	}
    
    
    	// --------------------
    	// UPDATE
    	// --------------------
    	
    	private void Update () {
    		UpdateGamepads();
    	}
    
    	// If the gamepads have not been updated this frame, update them.
    	public static void UpdateGamepads () {
    		if (lastUpdatedFrame == Time.frameCount) return;
    		
    		lastUpdatedFrame = Time.frameCount;
    		
    		Hinput.anyGamepad.Update();
    		Hinput.gamepad.ForEach(gamepad => gamepad.Update());
    	}
    
    
    	// --------------------
    	// ON APPLICATION QUIT
    	// --------------------
    
    	public void OnApplicationQuit() {
    		Hinput.anyGamepad.StopVibration();
    		Hinput.gamepad.ForEach(gamepad => gamepad.StopVibration());
    	}
    }
}
