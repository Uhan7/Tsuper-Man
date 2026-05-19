# AGENTS.md

## Project Summary
- Unity 6 project (Editor 6000.0.48f1).
- Current flow: Title Screen -> Jeepney Scene. Combat Scene is legacy.
- Target platform: WebGL (for now).

## Where To Work
- Only modify content under `Assets/` by default.
- If changes outside `Assets/` are needed (ProjectSettings, Packages, etc.), ask first.
- Typical edits: `.cs` scripts, occasionally ScriptableObjects and `.unity` scenes.

## Conventions Observed
- C# style: braces on next line, `private` fields with `[SerializeField]`, sections separated by `//` comments.
- Methods default to `private` unless needed; Unity callbacks use `private void`.
- Use `rb` for Rigidbody2D references and `GetComponent<>()` in `Awake()`.

## Scenes
- `Assets/Scenes/Title Screen.unity` (entry)
- `Assets/Scenes/Jeepney Scene.unity` (main gameplay)
- `Assets/Scenes/Combat Scene.unity` (legacy, avoid changes unless asked)

## Build/Run Notes
- WebGL target; avoid APIs unsupported in WebGL (threads, file system writes, etc.).
- Prefer deterministic behavior in `FixedUpdate()` for physics changes.

## How To Change Behavior
- Favor small, isolated script changes over scene rewiring unless requested.
- When a behavior is scene-dependent, update the relevant prefab and verify in the scene.

## Communication
- If a request needs edits outside `Assets/`, ask before proceeding.
- If changing any `.unity` scene, call it out explicitly.
