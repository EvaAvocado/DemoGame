﻿using UnityEngine;

public class InventoryAddComponent : MonoBehaviour
{
    [InventoryId] [SerializeField] private string _id;
    [SerializeField] private int _count;

    public void Add(GameObject go)
    {
        var hero = go.GetComponent<Hero>();

        if (hero != null)
        {
            hero.AddInventory(_id, _count);
        }
    }
}