# Hinput
**v2.0**

Hinput is a simple gamepad manager for Unity.

Set it up with a single click, learn how to use it in 5 minutes, and start making games for Windows, Mac and Linux!

**[install](http://tiny.cc/hinput_install_v3-0)** | **[learn](http://tiny.cc/hinput_learn_v3-0)** | **[documentation](http://tiny.cc/hinput_doc_v3-0)** | **[contact](mailto:hello@hinput.co)**

Hinput is the ultimate tool for artists and designers looking for a straightforward way of accessing gamepad inputs, as well as experienced developers in need of a lightweight versatile interface.

🎮 **Write it as you think it**
The Hinput interface is the most intuitive gamepad API on the Asset store. Its simple wording allows you to call it quickly and easily, without having to constantly look up the documentation.

🎮 **Plug and play**
Hinput works right out of the box: you do not need to set up any key maps or control bindings before using it. It does all the heavy lifting of assigning the proper controls to each player, so that you can focus on creating your game right away.

🎮 **Multi-platform support**
The plugin features built-in cross-platform support. Hinput automatically detects the player's OS, and adapts at runtime to Windows, Mac and Linux, as well as WebGL and Xbox builds. You don't need to do anything!

🎮 **Rumble**
Hinput gives you access to vibration on Windows for up to 4 gamepads. You can adjust the duration and intensity of the rumble, and access a selection of fine-tuned vibration presets.

🎮 **Easy to learn**
Two simple step-by-step guides will teach you how to install and use Hinput in a matter of minutes. If you prefer a more hands-on approach, Hinput also features an interactive example scene with visual feedback on how to use the API, as well as a debug scene that will allow you to test every single feature of the plugin and log them to the console.

🎮 **In-depth documentation**
The plugin features a detailed API reference, describing the behavior of each class and how to use them best. This documentation is reflected directly into the code, so that you don't even have to open your web browser. Every single class, property and method is commented!

🎮 **Open source initiative**
Hinput is a free open tool, created for the Unity community. Its source code is public, and you are welcome to tweak it to your own needs, and to contribute to the project on Github.

🎮 **Reliable developer support**
The plugin is contantly updated with new features, API improvements and bug fixes. If you have questions, remarks or requests you can email **hello@hinput.co** to receive immediate support from the developer.

**Featuring**
- Up to 8 controllers at the same time
- Detection of when a button is pressed, just pressed, just released, long pressed, double pressed...
- Values of stick and D-Pad position, coordinates, angle and distance to the center
- Conversion of analog sticks to 4 or 8 buttons - that have every property of regular buttons!
- Vibration on Windows, including balance between high & low-frequency motors, and fine-tuned vibration presets
- Detection of gamepad types and whether gamepads are connected
- Detection of every gamepad at once
- Calibration of stick and trigger dead zones, duration of long presses and double presses, default vibration parameters...

## How to install Hinput

- Download and import the Hinput package into your project.

- Click **Tools** > **Hinput** > **Set Up Hinput**

- If a warning message appears in your console click **Assets** > **Reimport all**

- That’s it! Hinput is ready to use. 

Here are some of the most useful controls :

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

Feel free to have a look at the **[learn](http://tiny.cc/hinput_learn_v3-0)** guide, and open the **Hinput Example Scene** to experiment with the controls for yourself.

## License: MIT

You are allowed to download, edit and use the code in this project and its releases, and distribute or sell projects made with  them. Please refer yourself to the license files in Assets/Hinput/Licenses for details.

## Contact

If you want to report a bug, request a feature of contribute to the project, feel free to email **hello@hinput.co**.
