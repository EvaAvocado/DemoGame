using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] private InventoryData _inventory;
    public InventoryData inventory => _inventory;
    
    public Vector3 playerPositionAfterLoadingScene;
    public int maxHealth;
    public int currentHealth;
    public int uhuwdu;
    public Weapon.Gem currentGem = Weapon.Gem.Nothing;
    public bool menuStatus;
}