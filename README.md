# ToggleHUD [<img src="https://github.com/JustSleightly/Resources/raw/main/Icons/Discord.png" width="30" height="30">](https://discord.gg/UnuEEQGjn3/ "Quantum's Discord") [<img src="https://github.com/JustSleightly/Resources/raw/main/Icons/JSLogo.png" width="30" height="30">](https://vrc.sleightly.dev/ "JustSleightly") [<img src="https://github.com/JustSleightly/Resources/raw/main/Icons/Discord.png" width="30" height="30">](https://discord.sleightly.dev/ "JustSleightly's Discord") [<img src="https://github.com/JustSleightly/Resources/raw/main/Icons/GitHub.png" width="30" height="30">](https://github.sleightly.dev/ "Github") [<img src="https://github.com/JustSleightly/Resources/raw/main/Icons/Store.png" width="30" height="30">](https://store.sleightly.dev/ "Store")

[![GitHub stars](https://img.shields.io/github/stars/JustSleightly/ToggleHUD)](https://github.com/JustSleightly/ToggleHUD/stargazers) [![GitHub Tags](https://img.shields.io/github/tag/JustSleightly/ToggleHUD)](https://github.com/JustSleightly/ToggleHUD/tags) [![GitHub release (latest by date including pre-releases)](https://img.shields.io/github/v/release/JustSleightly/ToggleHUD?include_prereleases)](https://github.com/JustSleightly/ToggleHUD/releases) [![GitHub issues](https://img.shields.io/github/issues/JustSleightly/ToggleHUD)](https://github.com/JustSleightly/ToggleHUD/issues) [![GitHub last commit](https://img.shields.io/github/last-commit/JustSleightly/ToggleHUD)](https://github.com/JustSleightly/ToggleHUD/commits/main) [![Discord](https://img.shields.io/discord/780192344800362506)](https://discord.sleightly.dev/) ![Twitter Follow](https://img.shields.io/twitter/follow/SleightlyDev?style=social)

[<img src="https://github.com/JustSleightly/ToggleHUD/blob/main/Documentation/Gifs/In-Game%20Demo.gif" height="450">](https://github.com/JustSleightly/ToggleHUD/blob/main/Documentation/Gifs/In-Game%20Demo.gif/ "ToggleHUD In-Game Demo")[<img src="https://github.com/JustSleightly/ToggleHUD/blob/main/Documentation/Images/Material%20Editor.png" height="450">](https://github.com/JustSleightly/ToggleHUD/blob/main/Documentation/Images/Material%20Editor.png "ToggleHUD Material Editor")

**ToggleHUD** is a ViewSpace UI shader with the ability to independently display and toggle up to 16 icons from a single texture sheet. These toggles are animatable, allowing for usage as a HUD UI indicating the status of other animations/features. Unlike other screenspace shaders, this UI is built for VR and can simulate depth and curve so as to not rest to close to the user's eyes.

### Download the [latest version](https://github.com/JustSleightly/ToggleHUD/releases) for free!

### Commercial Usage Licenses available now at [store.sleightly.dev](https://store.sleightly.dev/)!

---

### Features

* Up to 16 icons displayed in viewspace from a single texture/material
* Configurable rows/columns to rearrange the icon display
* Option to flip the horizontal/vertical order of the icons
* Adjustable width/height scaling of the whole UI
* Adjustable X/Y positioning in viewspace (with rotation/curve simulation)
* Adjustable distance for simulated depth in VR view
* Independently toggleable and animatable icons

######

# Disclaimer/Usage

Please have enough relevant experience in Unity and your VR Platform before using this, including knowledge of Unity's animation/animator controller system, and third-party systems such as VRChat's 3.0 system. Sample animation clips and VRC 3.0 resources are available for reference, but should not be used directly and are intended to be implemented in your own use cases.

ToggleHUD usage is intended for personal use only, and cannot be use commercially for any paid projects or redistributable packages. For any additional questions regarding licensing/usage, please reach out to **JustSleightly#0001**.

| Usage  | Personal | Commercial |
| ------------- | ------------- | ------------- |
| Personal Private Uploads | :white_check_mark: | :white_check_mark: |
| Public Uploads | :white_square_button: | :white_check_mark: |
| For Sale Uploads | :white_square_button: | :white_check_mark: |
| Commissioned Uploads | :white_square_button: | :white_check_mark: |
| Free Packages | :white_square_button: | :white_check_mark: |
| For Sale Packages | :white_square_button: | :white_check_mark: |
| Commissioned Packages | :white_square_button: | :white_check_mark: |
| Commercial Media | :white_square_button: | :white_check_mark: |

######

# Installation

The below instructions are just one example method of utilizing ToggleHUD with the included sample prefab. Advanced users do not necessarily have to use this mesh and setup.

Navigate in your project files to *Assets/Shaders/Quantum/ToggleHUD/Samples/Prefabs* and drag in the ToggleHUD.prefab into the scene for Unity base scaling, then onto your desired avatar. Expand the ToggleHUD object to reveal the ProxyRoot.Head object, and move ProxyRoot.Head onto your avatar's Head bone. Reset the transform of ProxyRoot.Head by right-clicking the Transform module in the inspector, and clicking Reset.

######

# Configuration

### Preparing the Texture

Prepare your icons in any 4 x 4 grid of your choice. A sample template can be found [here](https://raw.githubusercontent.com/JustSleightly/ToggleHUD/main/Sample/Textures/UI%20Grid%20Blank.png). The order the icons are displayed in the shader are as follows:

[<img src="https://raw.githubusercontent.com/JustSleightly/ToggleHUD/main/Sample/Textures/UI%20Grid%20Numbered.png" width="300" height="300">](https://github.com/JustSleightly/ToggleHUD/tree/main/Sample/Textures/ "Sample UI Textures")

### Material Settings

<details open>

  <summary> <strong> UI Color </strong> </summary>

######

<blockquote>

Select an HDR color to multiply with the UI. The alpha should not be maxed to prevent OLED burn-in

</details>

<details open>

  <summary> <strong> UI Texture </strong> </summary>

######

<blockquote>

Insert your prepared texture into this texture field, and leave the Tiling at 1,1 with an Offset of 0,0

</details>

<details open>

  <summary> <strong> Rows/Columns </strong> </summary>

######

<blockquote>

Define how many rows and columns of icons you want displayed. These rows and columns may not display more than 16 icons


![ToggleHUD RowsColumns](https://raw.githubusercontent.com/JustSleightly/ToggleHUD/main/Documentation/Gifs/RowsColumns.gif)

</details>

<details open>

  <summary> <strong> Width/Height </strong> </summary>

######

<blockquote>

Define the 2D scale of the UI

</details>

<details open>

  <summary> <strong> Flip Horizontal/Vertical/Ordering </strong> </summary>

######

<blockquote>

Change the directions in which the icons are displayed


![ToggleHUD FlipHorVerOrder](https://raw.githubusercontent.com/JustSleightly/ToggleHUD/main/Documentation/Gifs/Flip%20HorVerOrder.gif)

</details>

<details open>

  <summary> <strong> X/Y Position </strong> </summary>

######

<blockquote>

Define where the UI will render in viewspace. Some sample values can be found in the [FAQ](https://github.com/JustSleightly/ToggleHUD#frequently-asked-questions).

</details>

<details open>

  <summary> <strong> Distance </strong> </summary>

######

<blockquote>

Define the simulated distance from the view

</details>

<details open>

  <summary> <strong> Toggle UI Elements </strong> </summary>

######

<blockquote>

Toggle the display of each of the UI icons using the respective checkboxes

</details>

######

# Animating the UI

It is recommended to implement animation properties from ToggleHUD into your pre-existing animation clips so that they supplement your animator logic.

<ins> If using for multiplayer platforms such as **VRChat**,</ins> please animate the HUD to enable only **locally** using a parameter such as **[IsLocal](https://docs.vrchat.com/docs/animator-parameters)**.

Sample animation clips are included for you to copy/paste animation properties over to your own. They should not need to be re-pathed if you use the included prefab unless you change the names of the prefab objects in the hierarchy.

Sample animator controller and VRC 3.0 Menu/Parameters files are included for your reference, but you should be integrating into your own implementations.

######

---

# Frequently Asked Questions

<details>

  <summary> <strong> How should I position my HUD?  </strong> </summary>

######

<blockquote>

This may end up being a lot of guess and check depending on your preferred rows/columns and width/height.

Personally, I often use ToggleHUD in a 3 Row 5 Column layout with a Width/Height of 6 and Distance of 1, and I've found the following values to be the best for me:

| HUD Position  | X Value | Y Value | Flip Hor | Flip Ver |
| ------------- | ------------- | ------------- | ------------- | ------------- |
| Top Left | -11 | -8 | :white_square_button: | :white_square_button: |
| Top Right | +15 | -8 | :white_square_button: | :white_check_mark: |
| Bottom Left | -6 | +16 | :white_check_mark: | :white_check_mark: |
| Bottom Right | +15 | +16 | :white_check_mark: | :white_check_mark: |

</details>

<details>

  <summary> <strong> How do I export ToggleHUD with my commercial package? </strong> </summary>

######

<blockquote>

Assuming you have a **commercial license** for ToggleHUD, the shader files can be found at *Assets/Shaders/Quantum/ToggleHUD*. 

The minimum files to export are the ToggleHUD.shader and the Editor folder, then whatever material and texture you used with it.

If you used the sample prefab, be sure to also include the sample mesh/material if used found at *Assets/Shaders/Quantum/ToggleHUD/Sample*.

</details>

---

## Contributions

Shader and Functional Design - Quantum#0846

ShaderGUI Editor and Project Manager - JustSleightly#0001
