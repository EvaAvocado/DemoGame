using UnityEngine;

public class ChangeLayerComponent : MonoBehaviour
{
    public void ChangeLayer(string _layer)
    {
        gameObject.layer = LayerMask.NameToLayer(_layer);
    }
}