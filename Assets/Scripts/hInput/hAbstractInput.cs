﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class hAbstractInput {
	public static implicit operator bool (hAbstractInput hAI) { return hAI.pressed; }

	public abstract float positionRaw { get; }
	public abstract float position { get; }

	public abstract bool pressed { get; }
	public bool released { get { return !pressed; } }
	public bool inDeadZone { get { return positionRaw >= hInput.deadZone; } }

	public float lastPressed = 0f;
	public float lastpressStart = 0f;
	private float penultimatePressStart = 0f;
	public float lastReleased = 0f;

	public bool justPressed { get { return (pressed && (lastPressed - lastReleased) <= Time.deltaTime); } }
	public bool justReleased { get { return (released && (lastReleased - lastPressed) <= Time.deltaTime); } }

	//DoublePress takes a bit more resources than the rest.
	//Could do a cheaper justDoublePressed and justDoubleReleased, but nothing in between.
	public bool doublePress { get { return (lastpressStart - penultimatePressStart) <= hInput.doublePressDuration; } }
	public bool doublePressJustPressed { get { return justPressed && doublePress; } }
	public bool doublePressJustReleased { get { return justReleased && doublePress; } }

	public bool longPress { get { return (lastPressed - lastpressStart) >= hInput.longPressDuration; } }
	public bool longPressJustReleased { get { return (justReleased && (lastReleased - lastPressed) >= hInput.longPressDuration); } }

	public float pressDuration { get { if (pressed) return (Time.time - lastpressStart); return 0f; } }
	public float releaseDuration { get { if (released) return (Time.time - lastPressed); return 0f; } }

	public void Update () {
		float time = Time.time;

		if (pressed) lastPressed = time;
		else lastReleased = time;

		if (justPressed) {
			penultimatePressStart = lastpressStart;
			lastpressStart = time;			
		}
	}
}