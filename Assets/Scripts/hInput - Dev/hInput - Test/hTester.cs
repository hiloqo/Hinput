using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hTester : MonoBehaviour {
	// --------------------
	// SETTINGS
	// --------------------

	[Header("GENERAL")]
	public bool startMessage;
	public bool individualGamepads;
	public bool anyGamepad;

	[Header("STICK DIRECTIONS AS BUTTONS")]
	public bool stickVerticalsAndHorizontals;
	public bool stickDiagonals;

	[Header("BUTTONS")]
	public bool buttonPosition;
	public bool pressedAndReleased;
	public bool justPressedAndReleased;
	public bool lastPressedAndReleased;
	public bool buttonInDeadZone;
	public bool doublePress;
	public bool doublePressJustPressedAndReleased;
	public bool lastPressWasDouble;
	public bool longPress;
	public bool lastPressWasLong;
	public bool pressAndReleaseDuration;

	[Header("STICKS")]
	public bool stickPosition;
	public bool horizontal;
	public bool vertical;
	public bool angle;
	public bool distance;
	public bool stickInDeadZone;
	public bool stickInTriggerZone;
	public bool worldPositionCamera;
	public bool worldPositionCameraRaw;
	public bool worldPositionFlat;
	public bool worldPositionFlatRaw;

	[Header("YOU DON'T NEED TO TOUCH THAT")]
	public Transform redCube;
	public Transform blueSphere;
	public float moveSpeed;

	private hAbstractStick currentStick;
	private hAbstractInput currentButton;


	// --------------------
	// START AND UPDATE
	// --------------------


	void Start () {
		if (startMessage) {
			Debug.Log("OS is : "+hInput.os);
			Debug.Log("hInput gameObject name is : "+hInput.instance.name);
			Debug.Log("camera gameObject name is : "+hInput.worldCamera.name);
		}
	}

	float time = 0f;

	void Update () {
		//Debug.LogError("New Frame");
		TestSticks ();
		TestButtons ();
		
		//Debug.Log(100*(Time.time - time)/hInput.maxDeltaTime);
		//Debug.Log((Time.time - time)+"     --     "+hInput.maxDeltaTime);
		//time = Time.time;

		AddRendomLag();

		hButton A = hInput.gamepad[0].A;
		Debug.Log((A.pressed?"PRESSED":"released")+", Time : "+(int)(Time.time*1000)+",  delta time : "+(int)(hInput.maxDeltaTime*1000)
		+", (released : "+(int)((A.lastReleased)*1000)+", pressed : "+(int)((A.lastPressed)*1000)
		+", press duration : "+(int)((A.lastPressed - A.lastReleased)*1000)+", "+(100*(A.lastPressed - A.lastReleased)/hInput.maxDeltaTime)+"%dt)");
	}

	void AddRendomLag () {
		int nb = 0;
		int ran = (int)Mathf.Pow(10, Random.Range(4, 9));
		for (int i = 0; i<ran; i++) nb *= nb;

	}


	// --------------------
	// TEST BUTTONS
	// --------------------

	private void TestButtons () {
		if (currentButton == null || currentButton.inDeadZone) GetNewCurrentButton ();
		if (currentButton != null) TestCurrentButton ();
	}

	private void GetNewCurrentButton () {
		if (individualGamepads)	
			for (int i=0; i<hInput.maxGamepads; i++) UpdateCurrentButtonFromGamepad(hInput.gamepad[i]);

		if (anyGamepad) UpdateCurrentButtonFromGamepad(hInput.anyGamepad);
	}

	private void UpdateCurrentButtonFromGamepad (hGamepad gamepad) {
		foreach (hAbstractInput button in AllGamepadButtons(gamepad)) 
			if (!button.inDeadZone) {
				currentButton = button;
				break;
			}
	}


	// --------------------
	// GET ALL GAMEPAD BUTTONS
	// --------------------

	private List<hAbstractInput> AllGamepadButtons (hGamepad gamepad) {
		List<hAbstractInput> buttons = new List<hAbstractInput>() {
			gamepad.A, gamepad.B, gamepad.X, gamepad.Y, gamepad.leftBumper, gamepad.rightBumper, 
			gamepad.leftTrigger, gamepad.rightTrigger, gamepad.leftStickClick, gamepad.rightStickClick, 
			gamepad.back, gamepad.start, gamepad.xBoxButton
		};

		if (stickVerticalsAndHorizontals) buttons.AddRange (new List<hAbstractInput>() {
			gamepad.leftStick.up, gamepad.leftStick.down, gamepad.leftStick.left, gamepad.leftStick.right,
			gamepad.rightStick.up, gamepad.rightStick.down, gamepad.rightStick.left, gamepad.rightStick.right,
			gamepad.dPad.up, gamepad.dPad.down, gamepad.dPad.left, gamepad.dPad.right
		});

		if (stickDiagonals) buttons.AddRange (new List<hAbstractInput>() {
			gamepad.leftStick.upLeft, gamepad.leftStick.upRight, gamepad.leftStick.downLeft, gamepad.leftStick.downRight,
			gamepad.rightStick.upLeft, gamepad.rightStick.upRight, gamepad.rightStick.downLeft, gamepad.rightStick.downRight,
			gamepad.dPad.upLeft, gamepad.dPad.upRight, gamepad.dPad.downLeft, gamepad.dPad.downRight
		});

		return buttons;
	}


	// --------------------
	// TEST CURRENT BUTTON
	// --------------------

	private void TestCurrentButton () {
		if (pressedAndReleased) {
			if (currentButton) Debug.Log(currentButton.fullName+" is pressed !");
			else Debug.Log (currentButton.fullName+" is released !");
		}
		if (buttonPosition) Debug.Log (currentButton.fullName+" position : "+currentButton.position+", position raw : "+currentButton.positionRaw);
		if (justPressedAndReleased) {
			if (currentButton.justPressed) Debug.Log (currentButton.fullName+" was just pressed !");
			else if (currentButton.justReleased) Debug.Log (currentButton.fullName+" was just released !");
		}
		if (lastPressedAndReleased) Debug.Log (currentButton.fullName+" last pressed : "+currentButton.lastPressed
			+", last released : "+currentButton.lastReleased+", last press start : "+currentButton.lastPressStart);
		if (buttonInDeadZone) Debug.Log (currentButton.fullName+" is "+(currentButton.inDeadZone?"":"not ")+"in dead zone");
		if (doublePress && currentButton.doublePress) Debug.Log (currentButton.fullName+" is being double pressed !");
		if (doublePressJustPressedAndReleased) {
			if (currentButton.doublePressJustPressed) Debug.Log (currentButton.fullName+" was double pressed !");
			if (currentButton.doublePressJustReleased) Debug.Log (currentButton.fullName+" was released after a double press !");
		}
		if (lastPressWasDouble) Debug.Log (currentButton.fullName+"'s last press was a "+(currentButton.lastPressWasDouble?"double ":"simple ")+"press");
		if (longPress) {
			if (currentButton.longPress) Debug.Log (currentButton.fullName+" is being long pressed");
			if (currentButton.longPressJustReleased) Debug.Log (currentButton.fullName+" has just been released after a long press");
		}
		if (lastPressWasLong) Debug.Log (currentButton.fullName+"'s last press was a "+(currentButton.lastPressWasLong?"long ":"short ")+"press");
		if (pressAndReleaseDuration) {
			if (currentButton) Debug.Log (currentButton.fullName+" has been pressed for "+currentButton.pressDuration);
			else Debug.Log (currentButton.fullName+" has been released for "+currentButton.releaseDuration);
		}
	}


	// --------------------
	// TEST STICKS
	// --------------------

	private void TestSticks () {
		if (currentStick == null || currentStick.inDeadZone) GetNewCurrentStick ();
		if (currentStick != null) TestCurrentStick ();
	}

	private void GetNewCurrentStick () {
		if (individualGamepads)	
			for (int i=0; i<hInput.maxGamepads; i++) UpdateCurrentStickFromGamepad(hInput.gamepad[i]);

		if (anyGamepad) UpdateCurrentStickFromGamepad(hInput.anyGamepad);
	}

	private void UpdateCurrentStickFromGamepad (hGamepad gamepad) {
		if (!gamepad.leftStick.inDeadZone) currentStick = gamepad.leftStick;
		else if (!gamepad.rightStick.inDeadZone) currentStick = gamepad.rightStick;
		else if (!gamepad.dPad.inDeadZone) currentStick = gamepad.dPad;
	}


	// --------------------
	// TEST CURRENT STICK
	// --------------------

	private void TestCurrentStick () {
		if (stickPosition) Debug.Log (currentStick.fullName+" position : "+currentStick.position+", position raw : "+currentStick.positionRaw);
		if (horizontal) Debug.Log (currentStick.fullName+" horizontal : "+currentStick.horizontal+", horizontal raw : "+currentStick.horizontalRaw);
		if (vertical) Debug.Log (currentStick.fullName+" vertical : "+currentStick.vertical+", vertical raw : "+currentStick.verticalRaw);
		if (angle) Debug.Log (currentStick.fullName+" angle : "+currentStick.angle+", angle raw : "+currentStick.angleRaw);
		if (distance) Debug.Log (currentStick.fullName+" distance : "+currentStick.distance+", distance raw : "+currentStick.distanceRaw);
		if (stickInDeadZone) Debug.Log (currentStick.fullName+" is "+(currentStick.inDeadZone?"":"not ")+"in dead zone");
		if (stickInTriggerZone) Debug.Log (currentStick.fullName+" is "+(currentStick.inTriggerZone?"":"not ")+"pushed");
		if (worldPositionCamera) {
			Debug.Log (currentStick.fullName+" is controlling the blue sphere");
			blueSphere.transform.position += currentStick.worldPositionCamera * Time.deltaTime * moveSpeed;
		}
		if (worldPositionCameraRaw) {
			Debug.Log (currentStick.fullName+" is controlling the blue sphere");
			blueSphere.transform.position += currentStick.worldPositionCameraRaw * Time.deltaTime * moveSpeed;
		}
		if (worldPositionFlat) {
			Debug.Log (currentStick.fullName+" is controlling the red cube");
			redCube.transform.position += currentStick.worldPositionFlat * Time.deltaTime * moveSpeed;
		}
		if (worldPositionFlatRaw) {
			Debug.Log (currentStick.fullName+" is controlling the red cube");
			redCube.transform.position += currentStick.worldPositionFlatRaw * Time.deltaTime * moveSpeed;
		}
	}
}