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

		public enum SD { none, verticalsAndHorizontals, diagonals, pressedZone }
		public enum BF {
			none, position, pressedAndReleased, justPressedAndJustReleased, lastPressedAndLastReleased, inDeadZone,
			doublePress, doublePressJustPressedAndDoublePressedJustReleased, lastPressWasDouble, longPress,
			lastPressWasLong, pressDurationAndReleaseDuration
		}
		[Header("BUTTONS")]
		public SD stickDirections;
		public BF buttonFeature;

		public enum SF { 
			none, position, horizontal, vertical, angle, distance, inDeadZone, worldPositionCamera, worldPositionFlat 
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

		public enum VM { noArgs, duration, intensity, durationAndIntensity, oneCurve, twoCurves, vibrationPreset, advanced }
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
				Debug.Log("camera gameObject name is : "+Settings.worldCamera.name);
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
				
				Gamepad currentGamepad = currentButton.internalGamepad;
				string log = "current gamepad: " +
			             "[isConnected = " + currentGamepad.isConnected +
			             ", type = " + currentGamepad.type +
				          ", index = " + currentGamepad.index +
				          ", internal index = " + currentGamepad.internalIndex +
				          ", name = " + currentGamepad.name +
				          ", internal name = " + currentGamepad.internalName +
				          ", full name = " + currentGamepad.fullName +
				          ", internal full name = " + currentGamepad.internalFullName;

				if (currentGamepad is AnyGamepad) {
					log += ", gamepads = " + ToString(((AnyGamepad)currentGamepad).gamepads.Select(g => g.internalName).ToList()) +
					       ", indices = " + ToString(((AnyGamepad)currentGamepad).indices);
				}

				log += "]";
				Debug.Log(log);
			}

			if (gamepadListsOnLPressed && Input.GetKeyDown(KeyCode.L)) {
				if (currentButton == null) {
					Debug.Log("Current gamepad has not been set");
					return;
				}
				
				Gamepad currentGamepad = currentButton.internalGamepad;
				Debug.Log("current gamepad buttons : " + 
				          ToString(currentGamepad.buttons.Select(b => b.name).ToList()) + 
				          ", current gamepad sticks : " + 
				          ToString(currentGamepad.sticks.Select(s => s.name).ToList()));
			}
			
			if (stickInfoOnPPressed && Input.GetKeyDown(KeyCode.P)) {
				if (currentStick == null) {
					Debug.Log("Current stick has not been set");
					return;
				}

				string log = "[index = " + currentStick.index +
				             ", name = " + currentStick.name +
				             ", full name = " + currentStick.fullName +
				             ", internal full name = " + currentStick.internalFullName +
				             ", gamepad full name = " + currentStick.gamepadFullName +
				             ", gamepad internal full name = " + currentStick.internalGamepadFullName +
				             ", gamepad index = " + currentStick.gamepadIndex +
				             ", gamepad internal index = " + currentStick.internalGamepadIndex;

				if (currentStick is AnyGamepadStick) {
					log = "current anyGamepad stick: " + log +
					      ", pressed sticks = " + ToString(((AnyGamepadStick) currentStick).pressedSticks.Select(s => s.fullName).ToList()) +
					      ", pressed stick = " + ((AnyGamepadStick) currentStick).pressedStick.fullName;
				} else {
					log = "current stick: " + log;
				}

				log += "]";
				Debug.Log(log);
			}
			
			if (buttonInfoOnBPressed && Input.GetKeyDown(KeyCode.B)) {
				if (currentButton == null) {
					Debug.Log("Current button has not been set");
					return;
				}
				
				string log = "name = " + currentButton.name + 
				             ", internal name = " + currentButton.internalName +
				             ", full name = " + currentButton.fullName +
				             ", internal full name = " + currentButton.internalFullName +
				             ", gamepad index = " + currentButton.gamepadIndex +
				             ", gamepad internal index = " + currentButton.internalGamepadIndex +
				             ", gamepad full name = " + currentButton.gamepadFullName +
				             ", gamepad internal full name = " + currentButton.internalGamepadFullName;
				
				if (currentButton is Direction) {
					log = "current direction: [" + log +
					      ", stick index = " + ((Direction)currentButton).stickIndex +
					      ", stick full name = " + ((Direction)currentButton).stickFullName +
					      ", stick internal full name = " + ((Direction)currentButton).internalStickFullName +
					      ", angle = " + ((Direction)currentButton).angle;
				} else if (currentButton is StickPressedZone) {
					log = "current stick pressed zone: [" + log +
					      ", stick index = " + ((StickPressedZone)currentButton).stickIndex +
					      ", stick full name = " + ((StickPressedZone)currentButton).stickFullName +
					      ", stick internal full name = " + ((StickPressedZone)currentButton).internalStickFullName;
				} else if (currentButton is Button) {
					log = "current button: [" + log +
					      ", index = " + ((Button)currentButton).index +
					      ", index = " + ((Button)currentButton).internalIndex;
				} else if (currentButton is Trigger) {
					log = "current trigger: [" + log +
					      ", index = " + ((Trigger)currentButton).index +
					      ", internal index = " + ((Trigger)currentButton).internalIndex;
				} else if (currentButton is AnyInput) {
					log = "current anyInput: [" + log +
					      ", pressed inputs = " + ToString(((AnyInput) currentButton).pressedInputs.Select(i => i.fullName).ToList()) +
					      ", index = " + ((AnyInput)currentButton).index +
					      ", internal index = " + ((AnyInput)currentButton).internalIndex;
				}

				log += "]";
				Debug.Log(log);
			}
		}


		// --------------------
		// TEST BUTTONS
		// --------------------

		private void TestButtons () {
			if (currentButton == null || currentButton.inDeadZone) GetNewCurrentButton ();
			if (currentButton != null) TestCurrentButton ();
		}

		private void GetNewCurrentButton () {
			List<Gamepad> gamepadsToTest = new List<Gamepad>();
			if (gamepadMode == GM.anyGamepad) gamepadsToTest.Add(Hinput.anyGamepad);
			else gamepadsToTest = Hinput.gamepad;
			
			if (inputMode == IM.anyInput) {
				foreach (Gamepad gamepad in gamepadsToTest) {
					if (gamepad.anyInput.inDeadZone) continue;
					currentButton = gamepad.anyInput;
					return;
				}
			} else {
				foreach (Gamepad gamepad in gamepadsToTest) {
					foreach (Pressable button in AllGamepadButtons(gamepad)) {
						if (button.inDeadZone) continue;
						currentButton = button;
						return;
					}
				}
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
			if (buttonFeature == BF.pressedAndReleased) {
				if (currentButton) Debug.Log(currentButton.internalFullName+" is pressed !!!");
				else Debug.Log (currentButton.internalFullName+" is released");
			}
			if (buttonFeature == BF.position) Debug.Log (currentButton.internalFullName+" position : "+currentButton.position+
			                                             ", position raw : "+currentButton.positionRaw);
			if (buttonFeature == BF.justPressedAndJustReleased) {
				if (currentButton.justPressed) Debug.Log (currentButton.internalFullName+" was just pressed !!!");
				else if (currentButton.justReleased) Debug.Log (currentButton.internalFullName+" was just released");
			}
			if (buttonFeature == BF.lastPressedAndLastReleased) 
				Debug.Log (currentButton.internalFullName+" last pressed : "+currentButton.lastPressed+
				           ", last released : "+currentButton.lastReleased+
				           ", last press start : "+currentButton.lastPressStart);
			if (buttonFeature == BF.inDeadZone) {
				if (currentButton.inDeadZone) Debug.Log (currentButton.internalFullName+" is in dead zone");
				else Debug.Log (currentButton.internalFullName+" is not in dead zone !!!");
			}
			if (buttonFeature == BF.doublePress && currentButton.doublePress) 
				Debug.Log (currentButton.internalFullName+" is being double pressed !");
			if (buttonFeature == BF.doublePressJustPressedAndDoublePressedJustReleased) {
				if (currentButton.doublePressJustPressed) 
					Debug.Log (currentButton.internalFullName+" was double pressed !");
				if (currentButton.doublePressJustReleased) 
					Debug.Log (currentButton.internalFullName+" was released after a double press !");
			}
			if (buttonFeature == BF.lastPressWasDouble) {
				if (currentButton.lastPressWasDouble) 
					Debug.Log (currentButton.internalFullName+"'s last press was a double press !!!");
				else Debug.Log (currentButton.internalFullName+"'s last press was a simple press");
			
			}
			if (buttonFeature == BF.longPress) {
				if (currentButton.longPress) Debug.Log (currentButton.internalFullName+" is being long pressed");
				if (currentButton.longPressJustReleased) 
					Debug.Log (currentButton.internalFullName+" has just been released after a long press");
			}
			if (buttonFeature == BF.lastPressWasLong) {
				if (currentButton.lastPressWasLong) 
					Debug.Log (currentButton.internalFullName+"'s last press was a long press !!!");
				else Debug.Log (currentButton.internalFullName+"'s last press was a short press");
			
			}
			if (buttonFeature == BF.pressDurationAndReleaseDuration) {
				if (currentButton) 
					Debug.Log (currentButton.internalFullName+" has been pressed for "+currentButton.pressDuration+" !!!");
				else Debug.Log (currentButton.internalFullName+" has been released for "+currentButton.releaseDuration);
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
			if (gamepadMode == GM.individualGamepads) Hinput.gamepad.ForEach(UpdateCurrentStickFromGamepad);
			else UpdateCurrentStickFromGamepad(Hinput.anyGamepad);
		}

		private void UpdateCurrentStickFromGamepad (Gamepad gamepad) {
			if (!gamepad.leftStick.inDeadZone) currentStick = gamepad.leftStick;
			else if (!gamepad.rightStick.inDeadZone) currentStick = gamepad.rightStick;
			else if (!gamepad.dPad.inDeadZone) currentStick = gamepad.dPad;
		}


		// --------------------
		// TEST CURRENT STICK
		// --------------------

		private void TestCurrentStick () {
			if (stickFeature == SF.none) return;
			if (stickFeature == SF.position) 
				Debug.Log (currentStick.internalFullName+" position : "+currentStick.position+
			                              ", position raw : "+currentStick.positionRaw);
			if (stickFeature == SF.horizontal) Debug.Log (currentStick.internalFullName+" horizontal : "+currentStick.horizontal+
			                                              ", horizontal raw : "+currentStick.horizontalRaw);
			if (stickFeature == SF.vertical) Debug.Log (currentStick.internalFullName+" vertical : "+currentStick.vertical+
			                                            ", vertical raw : "+currentStick.verticalRaw);
			if (stickFeature == SF.angle) Debug.Log (currentStick.internalFullName+" angle : "+currentStick.angle+
			                                         ", angle raw : "+currentStick.angleRaw);
			if (stickFeature == SF.distance) Debug.Log (currentStick.internalFullName+" distance : "+currentStick.distance+
			                                            ", distance raw : "+currentStick.distanceRaw);
			if (stickFeature == SF.inDeadZone) {
				if (currentStick.inDeadZone) Debug.Log (currentStick.internalFullName+" is in dead zone");
				else Debug.Log (currentStick.internalFullName+" is not in dead zone !!!");
			} 
			if (stickFeature == SF.worldPositionCamera) {
				message.gameObject.SetActive(false);
				plane.gameObject.SetActive(true);
				redCube.gameObject.SetActive(false);
				blueSphere.gameObject.SetActive(true);
				Debug.Log (currentStick.internalFullName+" is controlling the blue sphere");
				blueSphere.transform.position += moveSpeed * Time.deltaTime * currentStick.worldPositionCamera;
			} else if (stickFeature == SF.worldPositionFlat) {
				message.gameObject.SetActive(false);
				plane.gameObject.SetActive(true);
				redCube.gameObject.SetActive(true);
				blueSphere.gameObject.SetActive(false);
				Debug.Log (currentStick.internalFullName+" is controlling the red cube");
				redCube.transform.position += moveSpeed * Time.deltaTime * currentStick.worldPositionFlat;
			} else {
				message.gameObject.SetActive(true);
				plane.gameObject.SetActive(false);
				redCube.gameObject.SetActive(false);
				blueSphere.gameObject.SetActive(false);
			}
		}

		// --------------------
		// TEST VIBRATION
		// --------------------

		private void TestVibration () {
			if (gamepadMode == GM.individualGamepads) TestVibrationOnGamepad(Hinput.gamepad[0]);
			else TestVibrationOnGamepad(Hinput.anyGamepad);
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
				if (stopVibrationDuration < 0) gamepad.StopVibration();
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