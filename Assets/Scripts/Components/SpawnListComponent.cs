﻿using UnityEngine;
using System;


public class SpawnListComponent : MonoBehaviour
{
    [SerializeField] private SpawnData[] _spawners;

    public void Spawn(string id)
    {
        foreach (var data in _spawners)
        {
            if (data.Id == id)
            {
                data.Component.Spawn();
                break;
            }
        }
    }

    [Serializable]
    public class SpawnData
    {
        public string Id;
        public SpawnComponent Component;
    }
}