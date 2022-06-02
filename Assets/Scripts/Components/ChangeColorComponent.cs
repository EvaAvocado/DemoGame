using UnityEngine;

public class ChangeColorComponent : MonoBehaviour
{
   [SerializeField] private Color _color;
   
   public void ChangeColor()
   {
      GetComponent<SpriteRenderer>().color = _color;
   }
}
