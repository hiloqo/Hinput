using System.Linq;
using UnityEngine;

namespace HinputClasses.Internal {
    // Hinput class handling the Example Scene.
    public class ExampleManager : MonoBehaviour {
        [Header("STATE")]
        public GameObject currentPanel;

        [Header("REFERENCES")]
        public GameObject oneGamepad;
        public GameObject twoGamepads;
        public GameObject fourGamepads;
        public GameObject eightGamepads;

        public void Start() {
            currentPanel = oneGamepad;
        }

        public void Update() {
            if (Hinput.gamepad.Count(gamepad => gamepad.isConnected) < 2) ActivatePanel(oneGamepad);
            else if (Hinput.gamepad.Count(gamepad => gamepad.isConnected) < 3) ActivatePanel(twoGamepads);
            else if (Hinput.gamepad.Count(gamepad => gamepad.isConnected) < 5) ActivatePanel(fourGamepads);
            else ActivatePanel(eightGamepads);
        }

        private void ActivatePanel(GameObject panel) {
            if (panel == currentPanel) return;
            currentPanel.SetActive(false);
            panel.SetActive(true);
            currentPanel = panel;
        }
    }
}
