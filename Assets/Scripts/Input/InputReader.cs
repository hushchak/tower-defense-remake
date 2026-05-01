using System;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputReader
{
    private static InputSystem_Actions actions;

    public static event Action OnBackPerformed, OnStartPerformed;
    public static event Action<Vector2> OnLeftButtonPerformed;
    public static Vector2 PointerPosition => actions.Player.PointerPosition.ReadValue<Vector2>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        actions = new InputSystem_Actions();
        actions.Enable();

        actions.Player.LeftButton.performed += LeftButton;
        actions.Player.Back.performed += BackPerfomed;
        actions.Player.Start.performed += StartPerformed;
    }

    private static void LeftButton(InputAction.CallbackContext context)
    {
        OnLeftButtonPerformed?.Invoke(PointerPosition);
    }

    private static void BackPerfomed(InputAction.CallbackContext context)
    {
        OnBackPerformed?.Invoke();
    }

    private static void StartPerformed(InputAction.CallbackContext context)
    {
        OnStartPerformed?.Invoke();
    }
}
