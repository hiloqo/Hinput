using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hDPad : hAbstractStick {
	// --------------------
	// CONSTRUCTOR
	// --------------------
	
	public hDPad (int gamepadIndex, int stickIndex, string gamepadFullName, string dPadName) {
		this._name = dPadName;
		this._gamepadIndex = gamepadIndex;
		this._fullName = gamepadFullName+"_"+dPadName;
		this._stickIndex = stickIndex;
	}

	
	// --------------------
	// DIRECTIONS
	// --------------------

	public override hAbstractInput up { 
		get {
			if (_up == null) _up = new hDPadDirection (gamepadIndex, stickIndex, name, fullName, "Up", "Vertical", true, 90);
			return _up;
		} 
	}
	public override hAbstractInput down { 
		get {
			if (_down == null) _down = new hDPadDirection (gamepadIndex, stickIndex, name, fullName, "Down", "Vertical", false, -90);
			return _down;
		} 
	}
	public override hAbstractInput left { 
		get {
			if (_left == null) _left = new hDPadDirection (gamepadIndex, stickIndex, name, fullName, "Left", "Horizontal", true, 180);
			return _left;
		} 
	}
	public override hAbstractInput right { 
		get {
			if (_right == null) _right = new hDPadDirection (gamepadIndex, stickIndex, name, fullName, "Right", "Horizontal", false, 0);
			return _right;
		} 
	}
}