# hinput
**v1.5.1**

hinput is a simple gamepad manager for Unity.

Set it up with a single click, learn how to use it in 5 minutes, and start making games for Windows, Mac and Linux!

**[install](http://tiny.cc/hinput_install_v1-5-0)** | **[learn](http://tiny.cc/hinput_learn_v1-5-0)** | **[documentation](http://tiny.cc/hinput_doc_v1-5-0)** | **[contact](mailto:couvreurhenri@gmail.com)**

hinput is a wrapper around Unity's base input system, with a command to fill up the input manager in a single click, and an intuitive API to expose gamepad controls. It automatically detects the OS you're using, and ajusts to Windows, Mac and Linux at runtime. It also features vibration on Windows for up to 4 gamepads, thanks to the XInput interface. 

# Featuring
- Up to 8 controllers at the same time
- Detection of when a button is pressed, just pressed, just released, long pressed, double pressed...
- Values of stick and D-Pad position, coordinates, angle and distance to the center
- Conversion of analog sticks to 4 or 8 buttons - that have every property of regular buttons!
- Vibration on Windows (with possibility to adjust the strength between left & right motors)
- Detection of gamepad types
- Detection of every gamepad at once
- Customization of the size of dead zones, the duration of long presses, the default vibration parameters... 

# How to install hinput

- Download and import the hinput package into your project.

- In your Unity editor, navigate to the **Tools** menu then click **hinput** > **Setup hinput**

- On Unity 2019.3 and higher, if a warning message appears in your console click **Assets** > **reimport all**

- That’s it ! hinput is ready to use. Here are a few of the most useful controls :

```csharp
// Get the state of buttons, triggers and stick directions :
hinput.gamepad[0].A.pressed
hinput.gamepad[0].leftTrigger.pressed
hinput.gamepad[0].rightStick.left.pressed

// Get the state of sticks and D-Pads :
hinput.gamepad[3].leftStick.position
hinput.gamepad[1].dPad.position

// Other useful features :
hinput.gamepad[4].X.justPressed
hinput.gamepad[2].rightBumper.doublePress
hinput.anyGamepad.rightStick.vertical
hinput.gamepad[7].Vibrate(0.5);
```

Feel free to check out the **[learn](http://tiny.cc/hinput_learn_v1-5-0)** guide or the **[documentation](http://tiny.cc/hinput_doc_v1-5-0)** for more

#### A few remarks:
- **If you were using XInput** in your project before you imported hinput, do not import the XInput folder from hinput. Unity does not deal well with duplicate packages.


- **If you are building a project for WebGL**, do not import the XInput project from hinput. Everything will work just fine, except for the fact that you won’t be able to use gamepad vibration. I’m still investigating this issue.


- **If you are using Unity 2019**, you have access to the preview of Input System. It should not be enabled by default, however it is not compatible with hinput. Here is how to check which system you are using : In the Edit menu, click Project Settings, then navigate to Player > Other settings > Configuration, and make sure that Active Input Handling is set to Input Manager.

- **To uninstall hinput**, simply go to Tools > hinput > Uninstall hinput. On Unity 2019.3 and higher, if a warning message appears in your console, click Assets > reimport all. This will remove hinput’s controls from your InputManager, however it will not delete hinput’s files. You can undo this action at any time by simply clicking Tools > hinput > Setup hinput again.

# License

MIT

# Contact

If you want to report a bug, request a feature of contribute to the project feel free to hit me up at **[couvreurhenri@gmail.com](mailto:couvreurhenri@gmail.com)**!
