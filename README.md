# Hinput
**v1.6.1**

Hinput is a simple gamepad manager for Unity.

Set it up with a single click, learn how to use it in 5 minutes, and start making games for Windows, Mac and Linux!

**[install](http://tiny.cc/hinput_install_v1-6-0)** | **[learn](http://tiny.cc/hinput_learn_v1-6-0)** | **[documentation](http://tiny.cc/hinput_doc_v1-6-0)** | **[contact](mailto:couvreurhenri@gmail.com)**

Hinput is a wrapper around Unity's base input system, with a command to fill up the input manager in a single click, and an intuitive API to expose gamepad controls. It automatically detects the OS you're using, and ajusts to Windows, Mac and Linux at runtime. It also features vibration on Windows for up to 4 gamepads, thanks to the XInput interface. 

## Featuring
- Up to 8 controllers at the same time
- Detection of when a button is pressed, just pressed, just released, long pressed, double pressed...
- Values of stick and D-Pad position, coordinates, angle and distance to the center
- Conversion of analog sticks to 4 or 8 buttons - that have every property of regular buttons!
- Vibration on Windows (with possibility to adjust the strength between left & right motors)
- Detection of gamepad types
- Detection of every gamepad at once
- Customization of the size of dead zones, the duration of long presses, the default vibration parameters... 

## How to install Hinput

- Download and import the Hinput package into your project.

- In your Unity editor, navigate to the **Tools** menu then click **Hinput** > **Setup Hinput**

- On Unity 2019.3 and higher, if a warning message appears in your console click **Assets** > **reimport all**

- That’s it ! Hinput is ready to use. Here are a few of the most useful controls :

```csharp
// Get the state of buttons, triggers and stick directions :
Hinput.gamepad[0].A.pressed
Hinput.gamepad[0].leftTrigger.pressed
Hinput.gamepad[0].rightStick.left.pressed

// Get the state of sticks and D-Pads :
Hinput.gamepad[3].leftStick.position
Hinput.gamepad[1].dPad.position

// Other useful features :
Hinput.gamepad[4].X.justPressed
Hinput.gamepad[2].rightBumper.doublePress
Hinput.anyGamepad.rightStick.vertical
Hinput.gamepad[7].Vibrate();
```

Feel free to check out the **[learn](http://tiny.cc/hinput_learn_v1-6-0)** guide or the **[documentation](http://tiny.cc/hinput_doc_v1-6-0)** for more

#### A few remarks:
- **If you were using XInput** in your project before you imported Hinput, do not import the XInput folder from Hinput. Unity does not deal well with duplicate packages.


- **If you are building a project for WebGL**, do not import the XInput project from Hinput. Everything will work just fine, except for the fact that you won’t be able to use gamepad vibration. I’m still investigating this issue.


- **If you are using Unity 2019**, you have access to the preview of Input System. It should not be enabled by default, however it is not compatible with Hinput. Here is how to check which system you are using : In the Edit menu, click Project Settings, then navigate to Player > Other settings > Configuration, and make sure that Active Input Handling is set to Input Manager.

- **To uninstall Hinput**, simply go to Tools > Hinput > Uninstall Hinput. On Unity 2019.3 and higher, if a warning message appears in your console, click Assets > reimport all. This will remove Hinput’s controls from your InputManager, however it will not delete Hinput’s files. You can undo this action at any time by simply clicking Tools > Hinput > Setup Hinput again.

## License: MIT

I allow everybody to download, edit and use the code in this project and its releases, and distribute or sell projects made with  them. Please check out the license files in Assets/Hinput/Licenses for details.

## Contact

If you want to report a bug, request a feature of contribute to the project feel free to hit me up at **[couvreurhenri@gmail.com](mailto:couvreurhenri@gmail.com)**!
