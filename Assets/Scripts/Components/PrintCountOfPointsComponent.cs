using System;
using UnityEngine;

public class PrintCountOfPointsComponent : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    public void Print()
    {
        print(_hero.points);
    }
}
