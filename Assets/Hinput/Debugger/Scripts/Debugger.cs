using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HinputClasses.Internal {
	// Hinput class allowing you to test every feature of the plugin and log it to the console.
    public class Debugger : MonoBehaviour {
		// --------------------
		// SETTINGS
		// --------------------

		[Header("GENERAL")]
		public bool startMessage;

		public enum SD { none, verticalsAndHorizontals, diagonals, diagonalsInverted, pressedZone }
		public enum BF { none, simplePress, doublePress, longPress, position }
		public enum PF {
			justPressedAndJustReleased, pressedAndReleased, lastPressedAndLastReleased, pressDurationAndReleaseDuration
		}
		[Header("BUTTONS")]
		public SD stickDirections;
		public BF buttonFeature;
		public PF pressFeature;

		public enum SF { 
			none, position, horizontal, vertical, angle, distance, worldPositionCamera, worldPositionFlat 
		}
		[Header("STICKS")]
		public SF stickFeature;

		[Header("VIBRATION")]
		public bool vibrateOnVPressed;

		[Header("TIME")]
		[Space(20)]
		[Header("--------------------")]
		[Space(20)]
		
		[Range(0,3)]
		public float timeScale = 1;
		public bool playInUpdate;
		public bool playInFixedUpdate;

		public enum GM { individualGamepads, anyGamepad }
		public enum IM { individualInputs, anyInput }
		[Header("GAMEPAD AND INPUT MODE")] 
		public GM gamepadMode;
		public IM inputMode;

		[Header("INFO")] 
		public bool gamepadInfoOnGPressed;
		public bool gamepadListsOnLPressed;
		public bool stickInfoOnPPressed;
		public bool buttonInfoOnBPressed;

		[Header("ENABLE/DISABLE")] 
		public bool toggleGamepadOnGPressed;
		public bool toggleStickOnPPressed;
		public bool toggleButtonOnBPressed;

		public enum VM {
			noArgs, duration, intensity, durationAndIntensity, oneCurve, twoCurves, vibrationPreset, advanced
		}
		[Header("VIBRATION")]
		public VM vibrationMode;
		[Range(0,10)]
		public float duration;
		[Range(0,1)]
		public float leftIntensity;
		[Range(0,1)]
		public float rightIntensity;
		public AnimationCurve curve;
		public AnimationCurve leftCurve;
		public AnimationCurve rightCurve;
		public VibrationPreset vibrationPreset;
		public bool multiplyVibrationPreset;
		public bool stopVibrationOnSPressed;
		[Range(-1, 3)]
		public float stopVibrationDuration;
		public bool displayIntensity;

		[Header("REFERENCES")]
		[Space(20)]
		[Header("--------------------")]
		[Space(20)]

		public GameObject message;
		public Transform plane;
		public Transform redCube;
		public Transform blueSphere;
		public float moveSpeed;

		private Stick currentStick;
		private Pressable currentButton;


		// --------------------
		// START AND UPDATE
		// --------------------


		private void Start () {
			if (startMessage) {
				Debug.Log("OS is : "+Utils.os);
				Debug.Log("Hinput gameObject name is : "+Settings.instance.name);
				Debug.Log("Camera gameObject name is : "+Settings.worldCamera.name);
			}
		}

		private void Update () {
			Time.timeScale = timeScale;
			if (playInUpdate) TestEverything();
		}

		private void FixedUpdate () {
			if (playInFixedUpdate) TestEverything();
		}

		private void TestEverything() {
			TestInfo();
			TestEnableDisable();
			TestSticks ();
			TestButtons ();
			TestVibration ();
		}


		// --------------------
		// TEST GAMEPADS
		// --------------------

		private void TestInfo() {
			if (gamepadInfoOnGPressed && Input.GetKeyDown(KeyCode.G)) {
				if (currentButton == null) {
					Debug.Log("Current gamepad has not been set");
					return;
				}
				
				Gamepad currentGamepad = currentButton.gamepad;
				Debug.Log("current gamepad: " +
				          "[isConnected = " + currentGamepad.isConnected +
				          ", isEnabled = " + currentGamepad.isEnabled +
				          ", type = " + currentGamepad.type + 
				          ", index = " + currentGamepad.index + 
				          ", name = " + currentGamepad.name +
				          ", full name = " + currentGamepad.fullName+ "]");
			}

			if (gamepadListsOnLPressed && Input.GetKeyDown(KeyCode.L)) {
				if (currentButton == null) {
					Debug.Log("Current gamepad has not been set");
					return;
				}
				
				Gamepad currentGamepad = currentButton.gamepad;
				Debug.Log("active gamepads = " + 
				          ToString(Hinput.activeGamepads.Select(gamepad => gamepad.fullName).ToList()) + 
				          ", current gamepad active inputs = " + 
				          ToString(currentGamepad.activeInputs.Select(input => input.fullName).ToList()) +
				          ", current gamepad buttons : " + 
				          ToString(currentGamepad.buttons.Select(button => button.name).ToList()) +
				          ", current gamepad sticks : " + 
				          ToString(currentGamepad.sticks.Select(stick => stick.name).ToList()));
			}
			
			if (stickInfoOnPPressed && Input.GetKeyDown(KeyCode.P)) {
				if (currentStick == null) {
					Debug.Log("Current stick has not been set");
					return;
				}

				Debug.Log("current stick: [isEnabled = " + currentStick.isEnabled +
				          ", index = " + currentStick.index +
				          ", name = " + currentStick.name +
				          ", full name = " + currentStick.fullName +
				          ", gamepad = " + currentStick.gamepad.fullName + "]");
			}
			
			if (buttonInfoOnBPressed && Input.GetKeyDown(KeyCode.B)) {
				if (currentButton == null) {
					Debug.Log("Current button has not been set");
					return;
				}
				
				string log = "isEnabled = " + currentButton.isEnabled + 
				             ", name = " + currentButton.name +
				             ", full name = " + currentButton.fullName +
				             ", gamepad = " + currentButton.gamepad.fullName;
				
				if (currentButton is StickDirection) {
					log = "current direction: [" + log +
					      ", stick = " + ((StickDirection)currentButton).stick.fullName +
					      ", angle = " + ((StickDirection)currentButton).angle;
				} else if (currentButton is StickPressedZone) {
					log = "current stick pressed zone: [" + log +
					      ", stick = " + ((StickPressedZone)currentButton).stick.fullName;
				} else if (currentButton is Button) {
					log = "current button: [" + log +
					      ", index = " + ((Button)currentButton).index;
				} else if (currentButton is Trigger) {
					log = "current trigger: [" + log +
					      ", index = " + ((Trigger)currentButton).index;
				} else if (currentButton is AnyInput) {
					log = "current anyInput: [" + log;
				}

				log += "]";
				Debug.Log(log);
			}
		}


		// --------------------
		// TEST ENABLE/DISABLE
		// --------------------

		private void TestEnableDisable() {
			if (toggleGamepadOnGPressed && Input.GetKeyDown(KeyCode.G)) {
				if (currentButton == null) {
					Debug.Log("Current gamepad has not been set");
					return;
				}
				if (currentButton.gamepad.isEnabled) {
					currentButton.gamepad.Disable();
					Debug.Log("Disabling " + currentButton.gamepad.fullName);
				} else {
					currentButton.gamepad.Enable();
					Debug.Log("Enabling "+ currentButton.gamepad.fullName);
				}
			}
			
			if (toggleStickOnPPressed && Input.GetKeyDown(KeyCode.P)) {
				if (currentStick == null) {
					Debug.Log("Current stick has not been set");
					return;
				}
				if (currentStick.isEnabled) {
					currentStick.Disable();
					Debug.Log("Disabling " + currentStick.fullName);
				} else {
					currentStick.Enable();
					Debug.Log("Enabling " + currentStick.fullName);
				}
			}
			
			if (toggleButtonOnBPressed && Input.GetKeyDown(KeyCode.B)) {
				if (currentButton == null) {
					Debug.Log("Current button has not been set");
					return;
				}
				if (currentButton.isEnabled) {
					currentButton.Disable();
					Debug.Log("Disabling " + currentButton.fullName);
				} else {
					currentButton.Enable();
					Debug.Log("Enabling " + currentButton.fullName);
				}
			}
		}


		// --------------------
		// TEST BUTTONS
		// --------------------

		private void TestButtons () {
			if (currentButton == null || currentButton.released) GetNewCurrentButton ();
			if (currentButton != null) TestCurrentButton ();
		}

		private void GetNewCurrentButton () {
			List<Gamepad> gamepadsToTest = new List<Gamepad>();
			if (gamepadMode == GM.anyGamepad) gamepadsToTest.Add(Hinput.anyGamepad);
			if (gamepadMode == GM.individualGamepads) gamepadsToTest = Hinput.gamepad;
			
			if (inputMode == IM.anyInput) {
				gamepadsToTest.ForEach(gamepad => {
					if (gamepad.anyInput.pressed) currentButton = gamepad.anyInput;
				});
			} 
			
			if (inputMode == IM.individualInputs) {
				gamepadsToTest.ForEach(gamepad => {
					AllGamepadButtons(gamepad).ForEach(button => {
						if (button.pressed) currentButton = button;
					});
				});
			}
		}


		// --------------------
		// GET ALL GAMEPAD BUTTONS
		// --------------------

		private List<Pressable> AllGamepadButtons (Gamepad gamepad) {
			List<Pressable> buttons = new List<Pressable>();
			
			buttons.AddRange (new List<Pressable>() {
				gamepad.A, gamepad.B, gamepad.X, gamepad.Y, gamepad.leftBumper, gamepad.rightBumper, 
				gamepad.leftTrigger, gamepad.rightTrigger, gamepad.leftStickClick, gamepad.rightStickClick, 
				gamepad.back, gamepad.start, gamepad.xBoxButton
			});

			if (stickDirections == SD.verticalsAndHorizontals) buttons.AddRange (new List<Pressable> {
				gamepad.leftStick.up, gamepad.leftStick.down, gamepad.leftStick.left, gamepad.leftStick.right,
				gamepad.rightStick.up, gamepad.rightStick.down, gamepad.rightStick.left, gamepad.rightStick.right,
				gamepad.dPad.up, gamepad.dPad.down, gamepad.dPad.left, gamepad.dPad.right
			});

			if (stickDirections == SD.diagonals) buttons.AddRange (new List<Pressable> {
				gamepad.leftStick.upLeft, gamepad.leftStick.upRight, gamepad.leftStick.downLeft, gamepad.leftStick.downRight,
				gamepad.rightStick.upLeft, gamepad.rightStick.upRight, gamepad.rightStick.downLeft, gamepad.rightStick.downRight,
				gamepad.dPad.upLeft, gamepad.dPad.upRight, gamepad.dPad.downLeft, gamepad.dPad.downRight
			});

			if (stickDirections == SD.diagonalsInverted) buttons.AddRange (new List<Pressable> {
				gamepad.leftStick.leftUp, gamepad.leftStick.rightUp, gamepad.leftStick.leftDown, gamepad.leftStick.rightDown,
				gamepad.rightStick.leftUp, gamepad.rightStick.rightUp, gamepad.rightStick.leftDown, gamepad.rightStick.rightDown,
				gamepad.dPad.leftUp, gamepad.dPad.rightUp, gamepad.dPad.leftDown, gamepad.dPad.rightDown
			});

			if (stickDirections == SD.pressedZone) buttons.AddRange (new List<Pressable> {
				gamepad.leftStick.inPressedZone, gamepad.rightStick.inPressedZone, gamepad.dPad.inPressedZone
			});

			return buttons;
		}


		// --------------------
		// TEST CURRENT BUTTON
		// --------------------

		private void TestCurrentButton () {
			if (buttonFeature == BF.none) return;
			if (buttonFeature == BF.position) Debug.Log (currentButton.fullName + 
			                                                           " position : " + currentButton.position);
			
			Press currentPress;
			string adjective;
			if (buttonFeature == BF.simplePress) {
				currentPress = currentButton.simplePress;
				adjective = "";
			} else if (buttonFeature == BF.doublePress) {
				currentPress = currentButton.doublePress;
				adjective = "double ";
			} else if (buttonFeature == BF.longPress) {
				currentPress = currentButton.longPress;
				adjective = "long ";
			} else return;

			if (pressFeature == PF.pressedAndReleased) {
				if (currentPress.pressed) Debug.Log(currentButton.fullName + " is being " + adjective + "pressed!!");
				else Debug.Log(currentButton.fullName + " is not being " + adjective + "pressed");
			}

			if (pressFeature == PF.justPressedAndJustReleased) {
				if (currentPress.justPressed) Debug.Log(currentButton.fullName + " was just " + adjective + "pressed!!");
				if (currentPress.justReleased) Debug.Log(currentButton.fullName + " was just released after a " + 
				                                     adjective + "press");
			}

			if (pressFeature == PF.lastPressedAndLastReleased) {
				Debug.Log (currentButton.fullName+" last " + adjective + "pressed : " + 
				           currentPress.lastPressed + ", last released : " + currentPress.lastReleased);
			}

			if (pressFeature == PF.pressDurationAndReleaseDuration) {
				if (currentPress.pressed) Debug.Log (currentButton.fullName + " has been held (" + adjective + 
				                                     "press) for " + currentPress.pressDuration+" seconds!!!");
				else Debug.Log (currentButton.fullName + " has been released (" + adjective + "press) for " + 
				                currentPress.releaseDuration + " seconds");
			}
		}


		// --------------------
		// TEST STICKS
		// --------------------

		private void TestSticks () {
			if (currentStick == null || !currentStick.inPressedZone) GetNewCurrentStick ();
			if (currentStick != null) TestCurrentStick ();
		}

		private void GetNewCurrentStick () {
			if (gamepadMode == GM.individualGamepads) Hinput.gamepad.ForEach(UpdateCurrentStickFromGamepad);
			else UpdateCurrentStickFromGamepad(Hinput.anyGamepad);
		}

		private void UpdateCurrentStickFromGamepad (Gamepad gamepad) {
			if (gamepad.leftStick.inPressedZone) currentStick = gamepad.leftStick;
			else if (gamepad.rightStick.inPressedZone) currentStick = gamepad.rightStick;
			else if (gamepad.dPad.inPressedZone) currentStick = gamepad.dPad;
		}


		// --------------------
		// TEST CURRENT STICK
		// --------------------

		private void TestCurrentStick () {
			if (stickFeature == SF.worldPositionCamera) {
				message.gameObject.SetActive(false);
				plane.gameObject.SetActive(true);
				redCube.gameObject.SetActive(false);
				blueSphere.gameObject.SetActive(true);
				Debug.Log (currentStick.fullName+" is controlling the blue sphere");
				blueSphere.transform.position += moveSpeed * Time.deltaTime * currentStick.worldPositionCamera;
			} else if (stickFeature == SF.worldPositionFlat) {
				message.gameObject.SetActive(false);
				plane.gameObject.SetActive(true);
				redCube.gameObject.SetActive(true);
				blueSphere.gameObject.SetActive(false);
				Debug.Log (currentStick.fullName+" is controlling the red cube");
				redCube.transform.position += moveSpeed * Time.deltaTime * currentStick.worldPositionFlat;
			} else {
				message.gameObject.SetActive(true);
				plane.gameObject.SetActive(false);
				redCube.gameObject.SetActive(false);
				blueSphere.gameObject.SetActive(false);
			}
			if (stickFeature == SF.none) return;
			if (stickFeature == SF.position) 
				Debug.Log (currentStick.fullName+" position : "+currentStick.position);
			if (stickFeature == SF.horizontal) Debug.Log (currentStick.fullName+
			                                              " horizontal : "+currentStick.horizontal);
			if (stickFeature == SF.vertical) Debug.Log (currentStick.fullName+
			                                            " vertical : "+currentStick.vertical);
			if (stickFeature == SF.angle) Debug.Log (currentStick.fullName+
			                                         " angle : "+currentStick.angle);
			if (stickFeature == SF.distance) Debug.Log (currentStick.fullName+
			                                            " distance : "+currentStick.distance);
		}

		// --------------------
		// TEST VIBRATION
		// --------------------

		private void TestVibration () {
			if (gamepadMode == GM.anyGamepad) TestVibrationOnGamepad(Hinput.anyGamepad);
			if (gamepadMode == GM.individualGamepads && currentButton != null) 
				TestVibrationOnGamepad(currentButton.gamepad);
		}

		private void TestVibrationOnGamepad(Gamepad gamepad) {
			if (vibrateOnVPressed && Input.GetKeyDown(KeyCode.V)) {
				if (vibrationMode == VM.noArgs) gamepad.Vibrate();
				if (vibrationMode == VM.duration) gamepad.Vibrate(duration);
				if (vibrationMode == VM.intensity) gamepad.Vibrate(leftIntensity, rightIntensity);
				if (vibrationMode == VM.durationAndIntensity) gamepad.Vibrate(leftIntensity, rightIntensity, duration);
				if (vibrationMode == VM.oneCurve) gamepad.Vibrate(curve);
				if (vibrationMode == VM.twoCurves) gamepad.Vibrate(leftCurve, rightCurve);
				if (vibrationMode == VM.advanced) gamepad.VibrateAdvanced(leftIntensity, rightIntensity);
				if (vibrationMode == VM.vibrationPreset) {
					if (multiplyVibrationPreset) gamepad.Vibrate(vibrationPreset, leftIntensity, rightIntensity, duration);
					else gamepad.Vibrate(vibrationPreset);
				}
			}

			if (stopVibrationOnSPressed && Input.GetKeyDown(KeyCode.S)) {
				if (stopVibrationDuration.IsEqualTo(0)) gamepad.StopVibration();
				else gamepad.StopVibration(stopVibrationDuration);
			}

			if (displayIntensity) {
				Debug.Log("left vibration : " + gamepad.leftVibration + ", right vibration : "
				          + gamepad.rightVibration);
			}
		}

		// --------------------
		// UTILS
		// --------------------

		private static string ToString<T>(List<T> list) {
			if (list.Count == 0) return "[]";
			string result = "[";
			for (int i = 0; i < list.Count - 1; i++) result += list[i] + ", ";
			return result + list[list.Count - 1] + "]";
		}
	}
}