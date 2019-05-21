using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hStick : hAbstractStick {
	// --------------------
	// CONSTRUCTOR
	// --------------------
	
	public hStick (int gamepadIndex, int stickIndex, string gamepadFullName, string stickName) {
		this._name = stickName;
		this._gamepadIndex = gamepadIndex;
		this._fullName = gamepadFullName+"_"+stickName;
		this._stickIndex = stickIndex;
	}

	
	// --------------------
	// DIRECTIONS
	// --------------------

	public override hAbstractInput up { 
		get {
			if (_up == null) _up = new hStickDirection (gamepadIndex, stickIndex, name, fullName, "Up", "Vertical", true, 90);
			return _up;
		} 
	}
	public override hAbstractInput down { 
		get {
			if (_down == null) _down = new hStickDirection (gamepadIndex, stickIndex, name, fullName, "Down", "Vertical", false, -90);
			return _down;
		} 
	}
	public override hAbstractInput left { 
		get {
			if (_left == null) _left = new hStickDirection (gamepadIndex, stickIndex, name, fullName, "Left", "Horizontal", true, 180);
			return _left;
		} 
	}
	public override hAbstractInput right { 
		get {
			if (_right == null) _right = new hStickDirection (gamepadIndex, stickIndex, name, fullName, "Right", "Horizontal", false, 0);
			return _right;
		} 
	}
}