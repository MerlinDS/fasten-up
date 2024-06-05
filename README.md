# Fasten Up

### Minimalist UI data binding framework for <a href="https://unity.com/">Unity</a>.

[![stability-experimental](https://img.shields.io/badge/stability-experimental-orange.svg)](https://github.com/emersion/stability-badges#experimental)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Releases](https://img.shields.io/github/v/release/merlinds/fasten-up.svg)](https://github.com/MerlinDS/fasten-up/releases)
[![Unity](https://img.shields.io/badge/Unity-2022+-black.svg)](https://unity3d.com/pt/get-unity/download/archive)

[comment]: <> (add badge for tests: https://github.com/merlinds/fasten-up/workflows/Tests/badge.svg)
[comment]: <> (add badge for openupm: https://openupm.com/packages/com.merlinds.fasten-up/)

[comment]: <> (Finish description)

## 💾 Installation

You can install Fasten Up using any of the following methods:

### Unity Package Manager

```
https://github.com/merlinds/fasten-up.git?path=/Assets/FastenUp#v0.1.0
```

1. In Unity, open **Window** → **Package Manager**.
2. Press the **+** button, choose "**Add package from git URL...**"
3. Enter url above and press **Add**.

### Open Unity Package Manager

```
openupm install com.merlinds.fasten-up
```

### Unity Package

1. Download the .unitypackage from [releases](https://github.com/MerlinDS/fasten-up/releases) page.
2. Import FastenUp.X.X.X.unitypackage

## 📦 Dependencies

- [TextMeshPro](https://docs.unity3d.com/Manual/upm-ui-giturl.html) - for `Text` components binding.

## 🌱 Usage example

Mediator class is a bridge between the game data and the UI components.

Create a mediator class that inherits from `IMediator` interface, like this:

```csharp
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;
using UnityEngine;

public partial class MyMediator : MonoBehaviour, IMediator
{
    public Bindable<string> Text { get; } = new();
    
    public void Awake()
    {
        Text.Value = "Hello World!";
    }
}
```

> **Note:** The `partial` keyword must be used for the sake of the source generator.

Add the `MyMediator` component to the GameObject.

Then add the `TextMeshPro` component with the `TextBinder` component to the child GameObject.

![text_binder_example.png](https://merlinds.github.io/fasten-up/resources/text_binder_example.png)

Now the `TextMeshPro` component will be bound to the `Text` property of the `MyMediator` class,
and the text will be updated when the `Text` property changes.

More examples can be found in the `Samples~` folder.

![smaples screen](https://merlinds.github.io/fasten-up/resources/smaples_screen.png)

![samples installation](https://merlinds.github.io/fasten-up/resources/samples_installation.png)

## 📚 Documentation

- [Manual](https://merlinds.github.io/fasten-up/docs/introduction.html)
- [Scripting API](https://merlinds.github.io/fasten-up/api/FastenUp.Runtime.Bindables.html)

## 📜 License

Fasten Up is distributed under the terms of the MIT License.
A complete version of the license is available in the [LICENSE](LICENSE) file in
this repository. Any contribution made to this project will be licensed under
the MIT License.
