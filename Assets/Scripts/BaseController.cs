using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Simple class that keeps track of the base's health and displays it on a slider
/// </summary>
public class BaseController : MonoBehaviour
{
    public int health;
    private int _maxHealth;

    [Header("UI")]
    public Slider healthSlider;

    private void Awake()
    {
        _maxHealth = health;
    }

    private void Update()
    {
        if(healthSlider != null)
            healthSlider.value = (float)health / (float)_maxHealth;
    }
}
