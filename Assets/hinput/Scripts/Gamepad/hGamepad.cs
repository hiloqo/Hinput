using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// hinput class representing a gamepad.
/// </summary>
public class hGamepad {
	// --------------------
	// NAME
	// --------------------

	private int _index;
	/// <summary>
	/// The index of a gamepad in the (gamepad) array of hinput, like 3 for (hinput.gamepad[3].index). <br/><br/> 
	/// Note : (hinput.anyGamepad.index) will return -1.
	/// </summary>
	public int index { get { return _index; } }

	private string _fullName;
	/// <summary>
	/// The full name of a gamepad, like “Linux_Gamepad4”.<br/><br/>
	/// Note : the number at the end of the gamepad’s name is the one used by Unity, not by hinput. 
	/// It is NOT equal to (index), but to (index)+1.
	/// </summary>
	public string fullName { get { return _fullName; } }


	// --------------------
	// PRIVATE VARIABLES
	// --------------------

	private hButton _A, _B, _X, _Y;
	private hButton _leftBumper, _rightBumper, _back, _start;
	private hButton _leftStickClick, _rightStickClick, _xBoxButton;

	private hTrigger _leftTrigger, _rightTrigger;

	private hStick _leftStick, _rightStick, _dPad;

	private List<hStick> _sticks;


	// --------------------
	// CONSTRUCTOR
	// --------------------

	public hGamepad (string os, int index) {
		this._index = index;
		if (_index >= 0) this._fullName = os+"_Gamepad"+index;
		else this._fullName = os+"_AnyGamepad";
	}

	
	// --------------------
	// BUILD
	// --------------------

	/// <summary>
	/// Please never call that.
	/// </summary>
	public void BuildAll () {
		if (A.gamepadIndex == 0
		|| B.gamepadIndex == 0
		|| X.gamepadIndex == 0
		|| Y.gamepadIndex == 0
		|| leftBumper.gamepadIndex == 0
		|| rightBumper.gamepadIndex == 0
		|| back.gamepadIndex == 0
		|| start.gamepadIndex == 0
		|| leftStickClick.gamepadIndex == 0
		|| rightStickClick.gamepadIndex == 0
		|| xBoxButton.gamepadIndex == 0
		|| leftTrigger.gamepadIndex == 0
		|| rightTrigger.gamepadIndex == 0) {
			// Do nothing, I'm just looking them up so that they are assigned.
		}
		
		leftStick.BuildDirections ();
		rightStick.BuildDirections ();
		dPad.BuildDirections ();
	}

	
	// --------------------
	// UPDATE
	// --------------------

	/// <summary>
	/// Please never call that.
	/// </summary>
	public void Update () {
		if ((hButton)_A != null) _A.Update();
		if ((hButton)_B != null) _B.Update();
		if ((hButton)_X != null) _X.Update();
		if ((hButton)_Y != null) _Y.Update();

		if ((hButton)_leftBumper != null) _leftBumper.Update();
		if ((hButton)_rightBumper != null) _rightBumper.Update();

		if ((hButton)_back != null) _back.Update();
		if ((hButton)_start != null) _start.Update();

		if ((hButton)_leftStickClick != null) _leftStickClick.Update();
		if ((hButton)_rightStickClick != null) _rightStickClick.Update();

		if ((hButton)_xBoxButton != null) _xBoxButton.Update();


		if ((hTrigger)_leftTrigger != null) _leftTrigger.Update();
		if ((hTrigger)_rightTrigger != null) _rightTrigger.Update();


		if ((hStick)_leftStick != null) _leftStick.Update();
		if ((hStick)_rightStick != null) _rightStick.Update();


		if ((hStick)_dPad != null) _dPad.Update();
	}

	
	// --------------------
	// PUBLIC PROPERTIES
	// --------------------

	/// <summary>
	/// The A button of a gamepad.
	/// </summary>
	public hButton A { 
		get {
			if ((hButton)_A == null) _A = new hButton ("A", this);
			return _A; 
		} 
	}

	/// <summary>
	/// The B button of a gamepad.
	/// </summary>
	public hButton B { 
		get {
			if ((hButton)_B == null) _B = new hButton ("B", this);
			return _B; 
		} 
	}

	/// <summary>
	/// The X button of a gamepad.
	/// </summary>
	public hButton X { 
		get {
			if ((hButton)_X == null) _X = new hButton ("X", this);
			return _X; 
		} 
	}

	/// <summary>
	/// The Y button of a gamepad.
	/// </summary>
	public hButton Y { 
		get {
			if ((hButton)_Y == null) _Y = new hButton ("Y", this);
			return _Y; 
		} 
	}


	/// <summary>
	/// The left bumper of a gamepad.
	/// </summary>
	public hButton leftBumper { 
		get {
			if ((hButton)_leftBumper == null) _leftBumper = new hButton ("LeftBumper", this);
			return _leftBumper; 
		} 
	}

	/// <summary>
	/// The right bumper of a gamepad.
	/// </summary>
	public hButton rightBumper { 
		get {
			if ((hButton)_rightBumper == null) _rightBumper = new hButton ("RightBumper", this);
			return _rightBumper; 
		} 
	}

	/// <summary>
	/// The back button of a gamepad.
	/// </summary>
	public hButton back { 
		get {
			if ((hButton)_back == null) _back = new hButton ("Back", this);
			return _back; 
		} 
	}

	/// <summary>
	/// The start button of a gamepad.
	/// </summary>
	public hButton start { 
		get {
			if ((hButton)_start == null) _start = new hButton ("Start", this);
			return _start; 
		} 
	}

	/// <summary>
	/// The left stick click of a gamepad.
	/// </summary>
	public hButton leftStickClick { 
		get {
			if ((hButton)_leftStickClick == null) _leftStickClick = new hButton ("LeftStickClick", this);
			return _leftStickClick; 
		} 
	}

	/// <summary>
	/// The right stick click of a gamepad.
	/// </summary>
	public hButton rightStickClick { 
		get {
			if ((hButton)_rightStickClick == null) _rightStickClick = new hButton ("RightStickClick", this);
			return _rightStickClick; 
		} 
	}

	/// <summary>
	/// The xBox button of a gamepad.<br/>
	/// <br/>Note : Windows and Linux drivers can’t detect the value of this button. 
	/// Therefore it will be considered released at all times on these operating systems.
	/// </summary>
	public hButton xBoxButton { 
		get {
			if ((hButton)_xBoxButton == null) _xBoxButton = new hButton ("XBoxButton", this);
			return _xBoxButton; 
		} 
	}

	/// <summary>
	/// The left trigger of a gamepad.
	/// </summary>
	public hTrigger leftTrigger { 
		get {
			if ((hTrigger)_leftTrigger == null) _leftTrigger = new hTrigger ("LeftTrigger", this);
			return _leftTrigger; 
		} 
	}
	
	/// <summary>
	/// The right trigger of a gamepad.
	/// </summary>
	public hTrigger rightTrigger { 
		get {
			if ((hTrigger)_rightTrigger == null) _rightTrigger = new hTrigger ("RightTrigger", this);
			return _rightTrigger; 
		} 
	}

	/// <summary>
	/// The left stick of a gamepad.
	/// </summary>
	public hStick leftStick { 
		get {
			if ((hStick)_leftStick == null) _leftStick = new hStick ("LeftStick", this, 0);
			return _leftStick; 
		} 
	}

	/// <summary>
	/// The right stick click of a gamepad.
	/// </summary>
	public hStick rightStick { 
		get {
			if ((hStick)_rightStick == null) _rightStick = new hStick ("RightStick", this, 1);
			return _rightStick; 
		} 
	}
	
	/// <summary>
	/// The D-pad of a gamepad.
	/// </summary>
	public hStick dPad { 
		get {
			if ((hStick)_dPad == null) _dPad = new hStick ("DPad", this);
			return _dPad; 
		} 
	}

	/// <summary>
	/// The list containing a gamepad’s sticks, in the following order : { leftStick, rightStick, dPad }
	/// </summary>
	public List<hStick> sticks { 
		get {
			if (_sticks == null) _sticks = new List<hStick>() { leftStick, rightStick, dPad };
			return _sticks;
		}
	}
}