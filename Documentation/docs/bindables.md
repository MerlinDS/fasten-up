# Bindables

The main way to communicate between the `Mediator` and the UI elements is to use the `Bindables`.
`Bindable` is a data wrapper that binds data to the `Binder` components in the UI.

### Requirements

`Bindable` must be declared as properties and must be initialized during the declaration.

There are several types of `Bindables`.

```csharp
private Bindable<int> Health { get; } = new(100);
```

## Bindable

The `Bindable<T>` is a property wrapper that binds property between a `Mediator` and UI elements.
It is used to bind simple properties like `int`, `float`, `string`, `Color`, `Sprite` etc.

Key features:

- Binds property between a `Mediator` and UI elements by name.
- One `Bindable` can be bound to multiple `Binders`.
- Provides a `OnValueChanged` event that can be used to subscribe to value changes from the UI elements.
- The value will be automatically assigned to the UI elements when the `Binder` is bound to the `Bindable`.
- Provides cross notification between `Binders` - when the value is changed in one `Binder` it will be automatically
  updated in all other `Binders` that are bound to the same `Bindable`.

See [Binders](binders.md) for more information about available binder types.

#### Example

```csharp
public partial class MyMediator : MonoBehaviour, IMediator
{
    private Bindable<string> Text { get; } = new("Hello World!");
}
```

#### Property with value change event

```csharp
public partial class MyMediator : MonoBehaviour, IMediator
{
    private Bindable<string> Text { get; } = new();

    private void Awake()
    {
        Text.OnValueChanged += OnTextChanged;
    }
    
    private void OnTextChanged(string text)
    {
        Debug.Log($"Text changed to: {text}");
    }
    
    private void OnDestroy()
    {
        Text.OnValueChanged -= OnTextChanged;
    }
}
```

## BindableSetup

The `BindableSetup<T>` used to setup the UI components in runtime. 
For example, you can use it to setup the slider or provides a list of options for the dropdown.

```csharp
public partial class MyMediator : MonoBehaviour, IMediator
{
    private Bindable<Vector2Int> SliderSetup { get; } = new(new Vector2Int(0, 100));
}
```

## BindableEvent

## BindableAction

## BindableRef

## BindableCollection