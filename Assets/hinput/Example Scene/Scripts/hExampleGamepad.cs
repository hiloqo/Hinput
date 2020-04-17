using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class hExampleGamepad : MonoBehaviour {
    [Header("OPTIONS")]
    public int index;
    public float stickDistance;

    [Header("STATE")]
    public Vector3 leftStickStartPosition;
    public Vector3 rightStickStartPosition;

    [Header("REFERENCES")]
    public GameObject leftStick;
    public GameObject rightStick;
    public GameObject xBoxButton;
    public GameObject vibrate;
    public Text text;
    [Space]
    public GameObject AHighlight;
    public GameObject BHighlight;
    public GameObject XHighlight;
    public GameObject YHighlight;
    public GameObject leftTriggerHighlight;
    public GameObject rightTriggerHighlight;
    public GameObject leftBumperHighlight;
    public GameObject rightBumperHighlight;
    public GameObject backHighlight;
    public GameObject startHighlight;
    public GameObject dpadUpHighlight;
    public GameObject dpadDownHighlight;
    public GameObject dpadLeftHighlight;
    public GameObject dpadRightHighlight;
    public GameObject xBoxButtonHighlight;
    public GameObject leftStickClickHighlight;
    public GameObject rightStickClickHighlight;

    private hGamepad gamepad { get { return hinput.gamepad[index]; } }

    private void Start() {
        leftStickStartPosition = leftStick.transform.localPosition;
        rightStickStartPosition = rightStick.transform.localPosition;
    }

    private void Update() {
        text.text = "";
        UpdateAllStickPositions();
        UpdateXBoxButton();
        UpdateAllButtonHighlights();
        UpdateVibrationButton();
        if (!hSetup.hinputIsInstalled() && index == 0)
            text.text = "Don't forget to install hinput in Tools>hinput>Set Up hinput!";
    }

    private void UpdateVibrationButton() {
        if (hUtils.os != "Windows") return;
        if (Time.time < 5) return;
        if (!hSetup.hinputIsInstalled()) return;
        
        if (gamepad.isConnected || index > 3) vibrate.SetActive(true);
        else vibrate.SetActive(false);
    }

    private void UpdateAllStickPositions() {
        leftStick.transform.localPosition = leftStickStartPosition + gamepad.leftStick.worldPositionCamera * stickDistance;
        rightStick.transform.localPosition = rightStickStartPosition + gamepad.rightStick.worldPositionCamera * stickDistance;
        
        if (gamepad.leftStick.inPressedZone) text.text = "hinput.gamepad[" + index + "].leftStick.position";
        if (gamepad.rightStick.inPressedZone) text.text = "hinput.gamepad[" + index + "].rightStick.position";
    }

    private void UpdateXBoxButton() {
        xBoxButton.SetActive(gamepad.isConnected);
    }

    private void UpdateAllButtonHighlights() {
        UpdateButtonHighLight(gamepad.xBoxButton, xBoxButtonHighlight, "xBoxButton");
        UpdateButtonHighLight(gamepad.rightStickClick, rightStickClickHighlight, "rightStickClick");
        UpdateButtonHighLight(gamepad.leftStickClick, leftStickClickHighlight, "leftStickClick");
        UpdateButtonHighLight(gamepad.rightTrigger, rightTriggerHighlight, "rightTrigger");
        UpdateButtonHighLight(gamepad.leftTrigger, leftTriggerHighlight, "leftTrigger");
        UpdateButtonHighLight(gamepad.rightBumper, rightBumperHighlight, "rightBumper");
        UpdateButtonHighLight(gamepad.leftBumper, leftBumperHighlight, "leftBumper");
        UpdateButtonHighLight(gamepad.dPad.right, dpadRightHighlight, "dPad.right");
        UpdateButtonHighLight(gamepad.dPad.left, dpadLeftHighlight, "dPad.left");
        UpdateButtonHighLight(gamepad.dPad.up, dpadUpHighlight, "dPad.up");
        UpdateButtonHighLight(gamepad.dPad.down, dpadDownHighlight, "dPad.down");
        UpdateButtonHighLight(gamepad.start, startHighlight, "start");
        UpdateButtonHighLight(gamepad.back, backHighlight, "back");
        UpdateButtonHighLight(gamepad.Y, YHighlight, "Y");
        UpdateButtonHighLight(gamepad.X, XHighlight, "X");
        UpdateButtonHighLight(gamepad.B, BHighlight, "B");
        UpdateButtonHighLight(gamepad.A, AHighlight, "A");
    }

    private void UpdateButtonHighLight(hPressable button, GameObject go, string input) {
        if (button.pressed) {
            go.SetActive(true);
            text.text = "hinput.gamepad[" + index + "]."+input;
        } else {
            go.SetActive(false);
        }
    }

    public void Vibrate() {
        gamepad.Vibrate();
        EventSystem.current.SetSelectedGameObject(null);
    }
}