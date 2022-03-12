using UnityEngine;

public class OnOffInteractableComponent : MonoBehaviour
{
   public void OnOffComponent(bool status)
   {
      GetComponent<InteractableComponent>().enabled = status;
   }
}
