﻿using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryData
{
    [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();

    public delegate void OnInventoryChanged(string id, int value);

    public OnInventoryChanged onChanged;

    //Добавляет объекты в инвентарь
    public void Add(string id, int value)
    {
        if (value <= 0) return;

        var itemDefs = DefsFacade.i.items.Get(id);
        if (itemDefs.IsVoid) return;

        var item = GetItem(id);
        if (item == null)
        {
            item = new InventoryItemData(id);
            _inventory.Add(item);
        }

        item.Value += value;
        onChanged?.Invoke(id, Count(id));
    }

    //Удаляет объекты из инвентаря
    public void Remove(string id, int value)
    {
        var itemDefs = DefsFacade.i.items.Get(id);
        if (itemDefs.IsVoid) return;

        var item = GetItem(id);
        if (item == null) return;

        item.Value -= value;

        if (item.Value <= 0)
            _inventory.Remove(item);

        onChanged?.Invoke(id, Count(id));
    }

    //Считает количество объектов по id
    public int Count(string id)
    {
        var count = 0;
        foreach (var item in _inventory)
        {
            if (item.Id == id)
                count += item.Value;
        }

        return count;
    }


    //Возвращает объект по id
    private InventoryItemData GetItem(string id)
    {
        foreach (var itemData in _inventory)
        {
            if (itemData.Id == id)
                return itemData;
        }

        return null;
    }
}

[Serializable]
public class InventoryItemData
{
    [InventoryId] public string Id;
    public int Value;

    public InventoryItemData(string id)
    {
        Id = id;
    }
}