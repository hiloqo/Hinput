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
            UpdatePanel(twoGamepads, 1);
            UpdatePanel(fourGamepads, 2);
            UpdatePanel(eightGamepads, 4);
        }

        private void UpdatePanel(GameObject panel, int amountOfGamepads) {
            if (panel == currentPanel) return;
            if (Hinput.gamepad.Count(gamepad => gamepad.isConnected) == 0) return;
            
            if (Hinput.gamepad.Last(gamepad => gamepad.isConnected).index >= amountOfGamepads) {
                currentPanel.SetActive(false);
                panel.SetActive(true);
                currentPanel = panel;
            }
        }
    }
}
