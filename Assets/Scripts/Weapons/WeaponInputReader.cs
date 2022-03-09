using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponInputReader : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    public void OnAttack(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<float>();
        _weapon.SetPhase(direction);
    }
}