using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
