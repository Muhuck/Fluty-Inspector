# Unity3D-fluty-Inspector
this project I created for handle custom view of inspector in Unity 3D game engine.

# Fluty Inspector

Fluty Inspector is a **lightweight, reflection-based custom Inspector framework for Unity**, inspired by Odin Inspector.  
It enhances the Unity Inspector using attributes while keeping the workflow simple, familiar, and fully compatible with standard `MonoBehaviour`.

Fluty Inspector is designed to be:
- Easy to integrate
- Easy to extend
- Editor-only
- Free from runtime dependencies

---

## Features

- Attribute-driven Inspector customization
- Folder groups with unlimited nesting
- Expandable / collapsible sub-groups
- Inspector buttons with parameters
- Conditional visibility (ShowIf)
- Conditional disabling (DisableIf)
- Read-only fields
- Group-level conditional visibility
- Works with standard `MonoBehaviour`
- No runtime overhead
- No custom base class required

---

## Requirements

- Unity 2020.3 or newer
- IMGUI-based Inspector
- Editor-only usage

---

## Installation

1. Download or clone this repository
2. Copy the `FlutyInspector` folder into your Unity project


> All editor scripts must remain inside an `Editor` folder.

No additional setup is required.

---

## Getting Started

Fluty Inspector works automatically through a global custom editor.  
You do **not** need to inherit from a special base class.

Example:
```csharp
using UnityEngine;
using Fluty.Inspector;

public class Example : MonoBehaviour
{
    public bool showAdvanced;

    [Fluty_FoldoutGroup("Basic")]
    public int health = 100;

    [Fluty_FoldoutGroup("Advanced")]
    [Fluty_ShowIf(nameof(showAdvanced))]
    public float speed = 5f;
}
```

## Attributes Overview
### Foldout Group
Organize fields and buttons into folders.
```csharp
[Fluty_FoldoutGroup("Settings")]
public float volume;
```
All members with the same group name are placed in the same folder.

### Sub Groups
Use / to define sub-groups.
```csharp
[Fluty_FoldoutGroup("Settings/Audio")]
public float musicVolume;

[Fluty_FoldoutGroup("Settings/Graphics")]
public int resolutionIndex;
```
Each group and subgroup has:
Its own foldout arrow
Independent expand / collapse state

### Button
Expose methods as Inspector buttons.
```csharp
[Fluty_Button]
void ResetStats()
{
    Debug.Log("Stats reset");
}
```
#### Custom label:
```csharp
[Fluty_Button("Reset Player")]
void ResetPlayer() { }
```

#### Button Parameters
Buttons may define parameters that are editable in the Inspector.
```csharp
[Fluty_Button]
void Teleport(Vector3 position, float speed)
{
    transform.position = position;
}
```
Parameters are displayed above the button when Expanded = true.

### ReadOnly
Displays a field but prevents editing.
```csharp
[Fluty_ReadOnly]
public int runtimeValue;
```
### ShowIf
Conditionally show a field or button.
```csharp
public bool showExtra;

[Fluty_ShowIf(nameof(showExtra))]
public int extraValue;
```

#### Inverse condition:
```csharp
[Fluty_ShowIf(nameof(showExtra), inverse: true)]
public string hiddenWhenTrue;
```
Supported conditions:
  - Boolean fields
  - Boolean properties
  - Boolean methods

### DisableIf
Disable a field or button without hiding it.
```csharp
public bool locked;

[Fluty_DisableIf(nameof(locked))]
public float editableValue;
```

### Conditional Folder Groups
Folder groups can be conditionally shown by applying ShowIf to any member inside the group.
```csharp
public bool showDebug;

[Fluty_ShowIf(nameof(showDebug))]
[Fluty_FoldoutGroup("Debug/Buttons")]
[Fluty_Button]
void DebugAction() { }
```
When the condition is false:
  - The entire folder is hidden
  - All sub-groups are hidden
  - Buttons and fields inside are not drawn

### Supported Parameter Types
Fluty Inspector v1.0 supports the following button parameter types:
# Unity3D-fluty-Inspector
this project I created for handle custom view of inspector in Unity 3D game engine.

# Fluty Inspector

Fluty Inspector is a **lightweight, reflection-based custom Inspector framework for Unity**, inspired by Odin Inspector.  
It enhances the Unity Inspector using attributes while keeping the workflow simple, familiar, and fully compatible with standard `MonoBehaviour`.

Fluty Inspector is designed to be:
- Easy to integrate
- Easy to extend
- Editor-only
- Free from runtime dependencies

---

## Features

- Attribute-driven Inspector customization
- Folder groups with unlimited nesting
- Expandable / collapsible sub-groups
- Inspector buttons with parameters
- Conditional visibility (ShowIf)
- Conditional disabling (DisableIf)
- Read-only fields
- Group-level conditional visibility
- Works with standard `MonoBehaviour`
- No runtime overhead
- No custom base class required

---

## Requirements

- Unity 2020.3 or newer
- IMGUI-based Inspector
- Editor-only usage

---

## Installation

1. Download or clone this repository
2. Copy the `FlutyInspector` folder into your Unity project


> All editor scripts must remain inside an `Editor` folder.

No additional setup is required.

---

## Getting Started

Fluty Inspector works automatically through a global custom editor.  
You do **not** need to inherit from a special base class.

Example:
```csharp
using UnityEngine;
using Fluty.Inspector;

public class Example : MonoBehaviour
{
    public bool showAdvanced;

    [Fluty_FoldoutGroup("Basic")]
    public int health = 100;

    [Fluty_FoldoutGroup("Advanced")]
    [Fluty_ShowIf(nameof(showAdvanced))]
    public float speed = 5f;
}
```

## Attributes Overview
### Foldout Group
Organize fields and buttons into folders.
```csharp
[Fluty_FoldoutGroup("Settings")]
public float volume;
```
All members with the same group name are placed in the same folder.

### Sub Groups
Use / to define sub-groups.
```csharp
[Fluty_FoldoutGroup("Settings/Audio")]
public float musicVolume;

[Fluty_FoldoutGroup("Settings/Graphics")]
public int resolutionIndex;
```
Each group and subgroup has:
Its own foldout arrow
Independent expand / collapse state

### Button
Expose methods as Inspector buttons.
```csharp
[Fluty_Button]
void ResetStats()
{
    Debug.Log("Stats reset");
}
```
#### Custom label:
```csharp
[Fluty_Button("Reset Player")]
void ResetPlayer() { }
```

#### Button Parameters
Buttons may define parameters that are editable in the Inspector.
```csharp
[Fluty_Button]
void Teleport(Vector3 position, float speed)
{
    transform.position = position;
}
```
Parameters are displayed above the button when Expanded = true.

### ReadOnly
Displays a field but prevents editing.
```csharp
[Fluty_ReadOnly]
public int runtimeValue;
```
### ShowIf
Conditionally show a field or button.
```csharp
public bool showExtra;

[Fluty_ShowIf(nameof(showExtra))]
public int extraValue;
```

#### Inverse condition:
```csharp
[Fluty_ShowIf(nameof(showExtra), inverse: true)]
public string hiddenWhenTrue;
```
Supported conditions:
  - Boolean fields
  - Boolean properties
  - Boolean methods

### DisableIf
Disable a field or button without hiding it.
```csharp
public bool locked;

[Fluty_DisableIf(nameof(locked))]
public float editableValue;
```

### Conditional Folder Groups
Folder groups can be conditionally shown by applying ShowIf to any member inside the group.
```csharp
public bool showDebug;

[Fluty_ShowIf(nameof(showDebug))]
[Fluty_FoldoutGroup("Debug/Buttons")]
[Fluty_Button]
void DebugAction() { }
```
When the condition is false:
  - The entire folder is hidden
  - All sub-groups are hidden
  - Buttons and fields inside are not drawn

### Supported Parameter Types
Fluty Inspector v1.0 supports the following button parameter types:
  - bool
  - int
  - float
  - string
  - enum
  - Vector2
  - Vector3
  - Color
  - UnityEngine.Object
Unsupported types will display a warning message in the Inspector.

### Global Editor Integration
Fluty Inspector uses a global custom editor:
```csharp
[CustomEditor(typeof(MonoBehaviour), true)]
public class FlutyMonoEditor : FlutyEditor<MonoBehaviour> { }
```

This allows Fluty attributes to work on:
  - Any MonoBehaviour
  - Without inheritance
  - Without modifying existing scripts
If Fluty is disabled for a target, it automatically falls back to Unity's default Inspector.

## How It Works
  - Uses reflection to collect fields and methods
  - Builds a hierarchical group tree from folder paths
  - Evaluates conditions every OnInspectorGUI
  - Maintains foldout states per group path
  - Renders everything using Unity IMGUI
All logic runs Editor-only.

## Version
Fluty Inspector v1.0
Initial public release.

## License
MIT License
Free for personal and commercial use.
