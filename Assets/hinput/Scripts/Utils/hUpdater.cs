using UnityEngine;

// hinput’s class responsible for updating gamepads. 
// It is automatically instantiated at runtime, or added to the gameobject with the hinputSettings component if you created one.
// You don’t need to do anything about it.
public class hUpdater : MonoBehaviour {
	// --------------------
	// INTERNAL SETTINGS
	// --------------------

	// The last frame hinput was updated
	private static int lastUpdatedFrame = -1;
	
	
	// --------------------
	// SINGLETON PATTERN
	// --------------------

	//The instance of hinputSettings. Assigned when first called.
	private static hUpdater _instance;
	public static hUpdater instance { 
		get {
			CheckInstance();
			return _instance;
		} 
	}

	public static void CheckInstance() {
		if (_instance != null) return;
		
		_instance = hSettings.instance.gameObject.AddComponent<hUpdater>();
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
		
		hinput.anyGamepad.Update();
		for (int i=0; i<hUtils.maxGamepads; i++) hinput.gamepad[i].Update ();
	}


	// --------------------
	// ON APPLICATION QUIT
	// --------------------

	public void OnApplicationQuit() {
		foreach (hGamepad gamepad in hinput.gamepad) {
			gamepad.StopVibration();
		}
		
		hinput.anyGamepad.StopVibration();
	}
}
