# hinput
hinput is a simple gamepad manager for Unity.

**[How to install](https://bit.ly/2JLXf4S)** | **[Getting started](https://bit.ly/2MZCdlo)** | **[full documentation](https://bit.ly/2Wa4Qkd)** 

hinput manages all gamepad interactions for you. With this simple multi-OS solution, you will never have to worry again about manually filling gamepad inputs, or handling settings for different operating systems. 

# Featuring :

- **Intuitive input system** : No library to import, no gameobject to create : you can start coding right away !

- **Multi-OS** : Seamless integration of Windows, Mac and Linux gamepad drivers. You don't have to do a thing !

- **Reliability** : No string to type manually = no typo. Everything in hinput is a property, which means it will be automatically suggested by VS Code, MonoDevelop, Rider...

- **Up to 8 controllers** : Ideal for multiplayer games !

- **Full button support** : 37 buttons per controller, including triggers, stick clicks, and 8 directions for the sticks and D-pad as virtual buttons. Detection of the button's position, as well as simple, double and long presses on each button .

- **Full stick and D-pad support** : access coordinates, angle and distance to origin, as well as 8 directions as virtual buttons, plus a direct translation of stick and D-pad position to world movement for easy character controllers.

- **Flexibility** : The settings panel exposes many useful properties, such as the duration of double presses, the width of stick directions, or the size of the dead zones.

# How to install hinput

- **Download** and **import** the hinput package into your project.

- In your Unity editor, navigate to the **Tools** menu then click **hinput** > **Setup hinput**

- **That’s it** ! hinput is ready to use. Here are a few examples of how to get the state of a controller :

```csharp
// For buttons, triggers and stick directions :
hinput.gamepad[0].A.pressed
hinput.gamepad[6].leftTrigger.pressed
hinput.anyGamepad.rightStick.left.pressed

// For sticks and D-Pads :
hinput.gamepad[4].leftStick.position
hinput.gamepad[1].dPad.position

// Some other useful features :
hinput.gamepad[0].X.justPressed
hinput.gamepad[2].rightBumper.doublePress
hinput.anyGamepad.rightStick.vertical
hinput.gamepad[7].dPad.angle
```

Feel free to check out the **[Getting started](https://bit.ly/2MZCdlo)** guide or the **[full documentation](https://bit.ly/2Wa4Qkd)** for more


**Note** : If you are using Unity 2019, you have access to the preview of Input System. It is not enabled by default, however it is not compatible with hinput.

Here is how to check which system you are using : In the **Edit** menu, click **Project Settings**, then navigate to **Player** > **Other settings** > **Configuration**, and make sure that **Active Input Handling** is set to **Input Manager**.

# License :

- You are allowed to download hinput for free, to use it, to edit it and to distribute it in free projects.

- Purchasing **[hinput pro](https://henriforshort.itch.io/hinput)** gives you or your company the right to sell stuff containing the plugin.

# Contact :

If you want to report a bug, request a feature of contribute to the project feel free to hit me up at **[hiloqo.games@gmail.com](mailto:hiloqo.games@gmail.com)**!
