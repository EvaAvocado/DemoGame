using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
    [SerializeField] private Hero _hero;

    public void OnHorizontal(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<float>();
        _hero.SetDirectionHorizontal(direction);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _hero.Jump();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            _hero.JumpBroke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        _hero.Interact();
    }

    public void OnVertical(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<float>();
        _hero.SetDirectionVertical(direction);
    }
}