# ToggleHUD [<img src="https://github.com/JustSleightly/Resources/raw/main/Icons/Discord.png" width="30" height="30">](https://discord.gg/UnuEEQGjn3/ "Quantum's Discord") [<img src="https://github.com/JustSleightly/Resources/raw/main/Icons/JSLogo.png" width="30" height="30">](https://vrc.sleightly.dev/ "JustSleightly") [<img src="https://github.com/JustSleightly/Resources/raw/main/Icons/Discord.png" width="30" height="30">](https://discord.sleightly.dev/ "JustSleightly's Discord") [<img src="https://github.com/JustSleightly/Resources/raw/main/Icons/GitHub.png" width="30" height="30">](https://github.sleightly.dev/ "Github") [<img src="https://github.com/JustSleightly/Resources/raw/main/Icons/Store.png" width="30" height="30">](https://store.sleightly.dev/ "Store")

[![GitHub stars](https://img.shields.io/github/stars/JustSleightly/ToggleHUD)](https://github.com/JustSleightly/ToggleHUD/stargazers) [![GitHub Tags](https://img.shields.io/github/tag/JustSleightly/ToggleHUD)](https://github.com/JustSleightly/ToggleHUD/tags) [![GitHub release (latest by date including pre-releases)](https://img.shields.io/github/v/release/JustSleightly/ToggleHUD?include_prereleases)](https://github.com/JustSleightly/ToggleHUD/releases) [![GitHub issues](https://img.shields.io/github/issues/JustSleightly/ToggleHUD)](https://github.com/JustSleightly/ToggleHUD/issues) [![GitHub last commit](https://img.shields.io/github/last-commit/JustSleightly/ToggleHUD)](https://github.com/JustSleightly/ToggleHUD/commits/main) [![Discord](https://img.shields.io/discord/780192344800362506)](https://discord.sleightly.dev/) ![Twitter Follow](https://img.shields.io/twitter/follow/SleightlyDev?style=social)

![](https://raw.githubusercontent.com/JustSleightly/ToggleHUD/main/Documentation/Images/Material%20Editor.png)

**ToggleHUD** is a ViewSpace UI shader with the ability to independently display and toggle up to 16 icons from a single texture sheet. These toggles are animatable, allowing for usage as a HUD UI indicating the status of other animations/features. Unlike other screenspace shaders, this UI is built for VR and can simulate depth and curve so as to not rest to close to the user's eyes.

### Download the [latest version](https://github.com/JustSleightly/ToggleHUD/releases)!

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

# Installation

Navigate in your project files to *Assets/Shaders/Quantum/ToggleHUD/Samples/Prefabs* and drag in the ToggleHUD.prefab into the scene for Unity base scaling, then onto your desired avatar. Expand the ToggleHUD object to reveal the ProxyRoot.Head object, and move ProxyRoot.Head onto your avatar's Head bone. Reset the transform of ProxyRoot.Head by right-clicking the Transform module in the inspector, and clicking Reset.

######

# Usage

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


![](https://raw.githubusercontent.com/JustSleightly/ToggleHUD/main/Documentation/Gifs/RowsColumns.gif)

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


![](https://raw.githubusercontent.com/JustSleightly/ToggleHUD/main/Documentation/Gifs/Flip%20HorVerOrder.gif)

</details>

<details open>

  <summary> <strong> X/Y Position </strong> </summary>

######

<blockquote>

Define where the UI will render in viewspace

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

---

# Frequently Asked Questions

<details>

  <summary> <strong> How do I export ToggleHUD with my commercial package? </strong> </summary>

######

<blockquote>

Assuming you have a **commercial license** for ToggleHUD, the shader files can be found at *Assets/Shaders/Quantum/ToggleHUD*. The minimum files to export are the ToggleHUD.shader and the Editor folder, then whatever material and texture you used with it.

</details>

---

## Contributions

Shader and Functional Design - Quantum#0846

ShaderGUI Editor and Project Manager - JustSleightly#0001
