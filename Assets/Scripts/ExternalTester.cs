using UnityEngine;

public class ExternalTester : MonoBehaviour {
    private void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            Hinput.anyGamepad.Vibrate(10);
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            Hinput.gamepad[0].StopVibration();
        }
        
        if (Hinput.gamepad[0].A.simplePress) return;

    }
}
