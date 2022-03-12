using UnityEngine;

public class OnOffBoxColliderComponent : MonoBehaviour
{
    private BoxCollider2D _collider;
    
    void Start()
    {
        _collider = GetComponents<BoxCollider2D>()[0];
    }

    public void CheckBoxTrigger(bool status)
    {
        _collider.enabled = status;
    }

}
