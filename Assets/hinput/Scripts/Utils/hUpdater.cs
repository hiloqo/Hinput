using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// hinput’s class responsible for updating gamepads. 
// It is automatically instantiated at runtime, or added to the gameobject with the hinputSettings component if you created one.
// You don’t need to do anything about it.
public class hUpdater : MonoBehaviour {
	// --------------------
	// SINGLETON PATTERN
	// --------------------

	//The instance of hinputSettings. Assigned when first called.
	private static hUpdater _instance;
	public static hUpdater instance { 
		get {
			if (_instance == null) {
				// Add hinputUpdater component to hinputSettings
				// If it didnt exist, it will be created
				_instance = hSettings.instance.gameObject.AddComponent<hUpdater>();
			}

			return _instance;
		} 
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
	public void UpdateGamepads () {
		if (!hUtils.isUpToDate) {
			hUtils.UpdateTime ();
			hinput.anyGamepad.Update();
			for (int i=0; i<hUtils.maxGamepads; i++) hinput.gamepad[i].Update ();
		}
	}
}
