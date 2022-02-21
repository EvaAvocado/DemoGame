using UnityEngine;

public class AddToHeroComponent : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    [SerializeField] private int _count;
    public void Add()
    {
        _hero.points += _count;
    }
}

