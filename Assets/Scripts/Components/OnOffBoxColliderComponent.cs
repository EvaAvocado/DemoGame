using UnityEngine;

public class OnOffBoxColliderComponent : MonoBehaviour
{
    [SerializeField] private int _colliderIndex;
    private BoxCollider2D _collider;

    void Start()
    {
        _collider = GetComponents<BoxCollider2D>()[_colliderIndex];
    }

    public void CheckBoxTrigger(bool status)
    {
        _collider.enabled = status;
    }

}
