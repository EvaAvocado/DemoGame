using UnityEngine;
using UnityEngine.InputSystem;

public class TargetInput : MonoBehaviour
{
    [SerializeField] private Target _target;
    
    public void OnHorizontal(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<float>();
        _target.SetDirectionHorizontal(direction);
    }
    
    public void OnVertical(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<float>();
        _target.SetDirectionVertical(direction);
    }
}
