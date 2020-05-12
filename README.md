# Hinput
**v2.0**

Hinput is a simple gamepad manager for Unity.

Set it up with a single click, learn how to use it in 5 minutes, and start making games for Windows, Mac and Linux!

**[install](http://tiny.cc/hinput_install_v2-0)** | **[learn](http://tiny.cc/hinput_learn_v2-0)** | **[documentation](http://tiny.cc/hinput_doc_v2-0)** | **[contact](mailto:hello@hinput.co)**

Hinput is a wrapper around Unity's base input system, with a command to fill up the input manager in a single click, and an intuitive API to expose gamepad controls. It automatically detects the OS you're using, and ajusts to Windows, Mac and Linux at runtime. It also features vibration on Windows for up to 4 gamepads, thanks to the XInput interface. 

## Featuring
- Up to 8 controllers at the same time
- Detection of when a button is pressed, just pressed, just released, long pressed, double pressed...
- Values of stick and D-Pad position, coordinates, angle and distance to the center
- Conversion of analog sticks to 4 or 8 buttons - that have every property of regular buttons!
- Vibration on Windows (with possibility to adjust the strength between left & right motors, and ready-made vibration presets)
- Detection of gamepad types and whether gamepads are connected
- Detection of every gamepad at once
- Customization of the size of dead zones, the duration of long presses, the default vibration parameters... 
- Thorough documentation and guides, comments on every class, property and method, and support from developer at **hello@hinput.co**.

## How to install Hinput

- Download and import the Hinput package into your project.

- In your Unity editor, click **Tools** > **Hinput** > **Set Up Hinput**

- If a warning message appears in your console click **Assets** > **Reimport all**

- That’s it ! Hinput is ready to use. Here are a few of the most useful controls :

```csharp
// Get the state of buttons, triggers and stick directions :
Hinput.gamepad[0].A
Hinput.gamepad[0].leftTrigger
Hinput.gamepad[0].rightStick.left

// Get the state of sticks and D-Pads :
Hinput.gamepad[0].leftStick
Hinput.gamepad[0].dPad

// Other useful features :
Hinput.gamepad[0].X.justPressed
Hinput.gamepad[0].rightBumper.doublePress
Hinput.gamepad[0].rightStick.vertical
Hinput.gamepad[0].Vibrate();
```

Feel free to check out the **[learn](http://tiny.cc/hinput_learn_v2-0)** guide or the **[documentation](http://tiny.cc/hinput_doc_v2-0)** for more

## License: MIT

I allow anyone to download, edit and use the code in this project and its releases, and distribute or sell projects made with  them. Please check out the license files in Assets/Hinput/Licenses for details.

## Contact

If you want to report a bug, request a feature of contribute to the project feel free to hit me up at **hello@hinput.co**!
