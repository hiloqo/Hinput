using UnityEngine;

// hinput’s class responsible for updating gamepads. 
// It is automatically instantiated at runtime, or added to the gameobject with the hinputSettings component if you created one.
// You don’t need to do anything about it.
public class hUpdater : MonoBehaviour {
	// --------------------
	// INTERNAL SETTINGS
	// --------------------

	//The time it was last time the game was updated
	private static float lastUpdated;

	//The duration it took to process the previous frame
	private static float deltaTime;
	
	//The previous frame was processed in less than this duration.
	public static float maxDeltaTime { get { return (deltaTime)*(1 + deltaTimeEpsilon); } }

	//By how much to increase deltaTime (in %) when comparing it, to account for rounding errors.
	private const float deltaTimeEpsilon = 0.1f;
	
	
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
		if (lastUpdated.IsEqualTo(Time.unscaledTime)) return;
		
		float currentTime = Time.unscaledTime;
		deltaTime = currentTime - lastUpdated;
		lastUpdated = currentTime;
		
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
