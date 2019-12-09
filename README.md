# hinput
**v1.3.1**

hinput is a simple gamepad manager for Unity.

**[install](http://tiny.cc/hinput_install_v1-4-0)** | **[learn](http://tiny.cc/hinput_learn_v1-4-0)** | **[documentation](http://tiny.cc/hinput_doc_v1-4-0)** 

hinput manages all gamepad interactions for you. With this simple multi-OS solution, you will never have to worry again about manually filling gamepad inputs, or handling settings for different operating systems. 

# Featuring

- **Intuitive input system** : No library to import, no gameobject to create : you can start coding right away !

- **Multi-OS** : Seamless integration of Windows, Mac and Linux gamepad drivers. You don't have to do a thing !

- **Reliability** : No string to type manually = no typo. Everything in hinput is a property, which means it will be automatically suggested by VS Code, MonoDevelop, Rider...

- **Up to 8 controllers** : Ideal for multiplayer games !

- **Full button support** : 37 buttons per controller, including triggers, stick clicks, and 8 directions for the sticks and D-pad as virtual buttons. Detection of the button's position, as well as simple, double and long presses on each button .

- **Full stick and D-pad support** : access coordinates, angle and distance to origin, as well as 8 directions as virtual buttons, plus a direct translation of stick and D-pad position to world movement for easy character controllers.

- **Flexibility** : The settings panel exposes many useful properties, such as the duration of double presses, the width of stick directions, or the size of the dead zones.

- **Vibration support** : From a simple rumble to complex balances between your controller's left and right motors, hinput expose all the features of XInput in an intuitive way. 

**Note** : Vibration is provided by the XInputDotNet implementation of Microsoft's XInput API. This means that it is only supported on Windows computers for up to 4 controllers. 

# How to install hinput

- **Download** and **import** the hinput package into your project.

- In your Unity editor, navigate to the **Tools** menu then click **hinput** > **Setup hinput**

- **That’s it** ! hinput is ready to use. Here are a few of the most useful controls :

```csharp
// Get the state of buttons, triggers and stick directions :
hinput.gamepad[0].A.pressed
hinput.gamepad[6].leftTrigger.pressed
hinput.anyGamepad.rightStick.left.pressed

// Get the state of sticks and D-Pads :
hinput.gamepad[4].leftStick.position
hinput.gamepad[1].dPad.position

// Other useful features :
hinput.gamepad[0].X.justPressed
hinput.gamepad[2].rightBumper.doublePress
hinput.anyGamepad.rightStick.vertical
hinput.gamepad[7].Vibrate(0.5);
```

Feel free to check out the **[learn](http://tiny.cc/hinput_learn_v1-4-0)** guide or the **[documentation](http://tiny.cc/hinput_doc_v1-4-0)** for more

#### A few remarks:
- **If you were using XInput** in your project before you imported hinput, do not import the XInput folder from hinput. Unity does not deal well with duplicate packages.


- **If you are building a project for WebGL**, do not import the XInput project from hinput. Everything will work just fine, except for the fact that you won’t be able to use gamepad vibration. I’m still investigating this issue.


- **If you are using Unity 2019**, you have access to the preview of Input System. It should not be enabled by default, however it is not compatible with hinput.
Here is how to check which system you are using : In the Edit menu, click Project Settings, then navigate to Player > Other settings > Configuration, and make sure that Active Input Handling is set to Input Manager.

- **To uninstall hinput**, simply go to Tools > hinput > Uninstall hinput. This will remove hinput’s controls from your InputManager, however it will not delete hinput’s files. You can undo this action at any time by simply clicking Tools > hinput > Setup hinput again.

# License

MIT

# Contact

If you want to report a bug, request a feature of contribute to the project feel free to hit me up at **[couvreurhenri@gmail.com](mailto:couvreurhenri@gmail.com)**!
