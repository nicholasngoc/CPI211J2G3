using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Simple class that keeps track of the base's health and displays it on a slider
/// </summary>
public class BaseController : MonoBehaviour
{
    public int health;
    private int _maxHealth;

    private void Awake()
    {
        _maxHealth = health;
        GameObject.Find("PlayerUI").GetComponent<UIControl>().maxHealthBase = _maxHealth;
        GameObject.Find("PlayerUI").GetComponent<UIControl>().currentHealthBase = health;
    }

    private void Update()
    {
        if (health <= 0.0)
            {
                SceneManager.LoadScene("GameOver");
            }
        GameObject.FindGameObjectWithTag("BaseHealth").GetComponent<Slider>().value = health;
    }
}
