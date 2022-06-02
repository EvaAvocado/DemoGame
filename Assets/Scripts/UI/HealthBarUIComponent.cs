using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIComponent : MonoBehaviour
{
    private Image _healthBar;
    private UhuvduUIComponent _uhuvdus;
    private GameSession _session;

    private void Awake()
    {
        _healthBar = GetComponent<Image>();
    }

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        _uhuvdus = FindObjectOfType<UhuvduUIComponent>();
        _healthBar.fillAmount = _session.playerData.currentHealth;
    }

    public void FillAmount(float currentHealth)
    {
        _healthBar.fillAmount = currentHealth;
        if (currentHealth <= 0)
        {
            _uhuvdus.SubUhuvdu(); 
        }
    }

    
}






