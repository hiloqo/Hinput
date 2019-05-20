using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hGamepad {
	// --------------------
	// PRIVATE VARIABLES
	// --------------------

	private string _name;

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


	// --------------------
	// CONSTRUCTOR
	// --------------------

	public hGamepad (string os, string name) {
		this._name = os+"_"+name;

		_A = new hButton (_name, "A");
		_B = new hButton (_name, "B");
		_X = new hButton (_name, "X");
		_Y = new hButton (_name, "Y");

		_leftBumper = new hButton (_name, "LeftBumper");
		_rightBumper = new hButton (_name, "RightBumper");

		_back = new hButton (_name, "Back");
		_start = new hButton (_name, "Start");

		_leftStickClick = new hButton (_name, "LeftStickClick");
		_rightStickClick = new hButton (_name, "RightStickClick");

		_xBoxButton = new hButton (_name, "XBoxButton");


		_leftTrigger = new hTrigger (_name, "LeftTrigger");
		_rightTrigger = new hTrigger (_name, "RightTrigger");


		_leftStick = new hStick (_name, "LeftStick");
		_rightStick = new hStick (_name, "RightStick");


		_dPad = new hDPad (_name, "DPad");
	}

	
	// --------------------
	// UPDATE
	// --------------------

	public void Update () {
		_A.Update();
		_B.Update();
		_X.Update();
		_Y.Update();

		_leftBumper.Update();
		_rightBumper.Update();

		_back.Update();
		_start.Update();

		_leftStickClick.Update();
		_rightStickClick.Update();

		_xBoxButton.Update();


		_leftTrigger.Update();
		_rightTrigger.Update();


		_leftStick.Update();
		_rightStick.Update();


		_dPad.Update();
	}

	
	// --------------------
	// PUBLIC PROPERTIES
	// --------------------

	public string name { get { return _name; } }

	public hButton A { get { return _A; } }
	public hButton B { get { return _B; } }
	public hButton X { get { return _X; } }
	public hButton Y { get { return _Y; } }

	public hButton leftBumper { get { return _leftBumper; } }
	public hButton rightBumper { get { return _rightBumper; } }

	public hButton back { get { return _back; } }
	public hButton start { get { return _start; } }

	public hButton leftStickClick { get { return _leftStickClick; } }
	public hButton rightStickClick { get { return _rightStickClick; } }

	public hButton xBoxButton { get { return _xBoxButton; } }


	public hTrigger leftTrigger { get { return _leftTrigger; } }
	public hTrigger rightTrigger { get { return _rightTrigger; } }


	public hStick leftStick { get { return _leftStick; } }
	public hStick rightStick { get { return _rightStick; } }
	

	public hDPad dPad { get { return _dPad; } }
}