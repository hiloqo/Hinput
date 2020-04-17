using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class hExampleManager : MonoBehaviour {
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
        if (hinput.gamepad.Count(gamepad => gamepad.isConnected) == 0) return;
            
        if (hinput.gamepad.Last(gamepad => gamepad.isConnected).index >= amountOfGamepads) {
            currentPanel.SetActive(false);
            panel.SetActive(true);
            currentPanel = panel;
        }
    }
}
