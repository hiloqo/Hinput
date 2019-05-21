using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hGamepad {
	// --------------------
	// NAME
	// --------------------

	private string _fullName;
	private int _index;

	public int index { get { return _index; } }
	public string fullName { get { return _fullName; } }


	// --------------------
	// PRIVATE VARIABLES
	// --------------------

	private hButton _A;
	private hButton _B;
	private hButton _X;
	private hButton _Y;

	private hButton _leftBumper;
	private hButton _rightBumper;

	private hButton _back;
	private hButton _start;

	private hButton _leftStickClick;
	private hButton _rightStickClick;

	private hButton _xBoxButton;


	private hTrigger _leftTrigger;
	private hTrigger _rightTrigger;


	private hStick _leftStick;
	private hStick _rightStick;


	private hDPad _dPad;


	private List<hAbstractStick> _sticks;


	// --------------------
	// CONSTRUCTOR
	// --------------------

	public hGamepad (string os, int index) {
		this._index = index;
		if (_index >= 0) this._fullName = os+"_Gamepad"+(index+1);
		else this._fullName = os+"_AnyGamepad";
	}

	
	// --------------------
	// UPDATE
	// --------------------

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


		if ((hDPad)_dPad != null) _dPad.Update();
	}

	
	// --------------------
	// BUILD
	// --------------------

	public void BuildAll () {
		int indices = A.gamepadIndex;
		indices = B.gamepadIndex;
		indices = X.gamepadIndex;
		indices = Y.gamepadIndex;
		indices = leftBumper.gamepadIndex;
		indices = rightBumper.gamepadIndex;
		indices = back.gamepadIndex;
		indices = start.gamepadIndex;
		indices = leftStickClick.gamepadIndex;
		indices = rightStickClick.gamepadIndex;
		indices = xBoxButton.gamepadIndex;
		indices = leftTrigger.gamepadIndex;
		indices = rightTrigger.gamepadIndex;
		
		leftStick.BuildAll ();
		rightStick.BuildAll ();
		dPad.BuildAll ();
	}

	
	// --------------------
	// PUBLIC PROPERTIES
	// --------------------

	public hButton A { 
		get {
			if ((hButton)_A == null) _A = new hButton (index, fullName, "A");
			return _A; 
		} 
	}
	public hButton B { 
		get {
			if ((hButton)_B == null) _B = new hButton (index, fullName, "B");
			return _B; 
		} 
	}
	public hButton X { 
		get {
			if ((hButton)_X == null) _X = new hButton (index, fullName, "X");
			return _X; 
		} 
	}
	public hButton Y { 
		get {
			if ((hButton)_Y == null) _Y = new hButton (index, fullName, "Y");
			return _Y; 
		} 
	}

	public hButton leftBumper { 
		get {
			if ((hButton)_leftBumper == null) _leftBumper = new hButton (index, fullName, "LeftBumper");
			return _leftBumper; 
		} 
	}
	public hButton rightBumper { 
		get {
			if ((hButton)_rightBumper == null) _rightBumper = new hButton (index, fullName, "RightBumper");
			return _rightBumper; 
		} 
	}

	public hButton back { 
		get {
			if ((hButton)_back == null) _back = new hButton (index, fullName, "Back");
			return _back; 
		} 
	}
	public hButton start { 
		get {
			if ((hButton)_start == null) _start = new hButton (index, fullName, "Start");
			return _start; 
		} 
	}

	public hButton leftStickClick { 
		get {
			if ((hButton)_leftStickClick == null) _leftStickClick = new hButton (index, fullName, "LeftStickClick");
			return _leftStickClick; 
		} 
	}
	public hButton rightStickClick { 
		get {
			if ((hButton)_rightStickClick == null) _rightStickClick = new hButton (index, fullName, "RightStickClick");
			return _rightStickClick; 
		} 
	}

	public hButton xBoxButton { 
		get {
			if ((hButton)_xBoxButton == null) _xBoxButton = new hButton (index, fullName, "XBoxButton");
			return _xBoxButton; 
		} 
	}


	public hTrigger leftTrigger { 
		get {
			if ((hTrigger)_leftTrigger == null) _leftTrigger = new hTrigger (index, fullName, "LeftTrigger");
			return _leftTrigger; 
		} 
	}
	public hTrigger rightTrigger { 
		get {
			if ((hTrigger)_rightTrigger == null) _rightTrigger = new hTrigger (index, fullName, "RightTrigger");
			return _rightTrigger; 
		} 
	}


	public hStick leftStick { 
		get {
			if ((hStick)_leftStick == null) _leftStick = new hStick (index, 0, fullName, "LeftStick");
			return _leftStick; 
		} 
	}
	public hStick rightStick { 
		get {
			if ((hStick)_rightStick == null) _rightStick = new hStick (index, 1, fullName, "RightStick");
			return _rightStick; 
		} 
	}
	

	public hDPad dPad { 
		get {
			if ((hDPad)_dPad == null) _dPad = new hDPad (index, 2, fullName, "DPad");
			return _dPad; 
		} 
	}


	public List<hAbstractStick> sticks { 
		get {
			if (_sticks == null) _sticks = new List<hAbstractStick>() { leftStick, rightStick, dPad };
			return _sticks;
		}
	}
}