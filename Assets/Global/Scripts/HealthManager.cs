using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Player player;
    public Image healthBar;
    private int maxHealth = 100;

    void Start()
    {
        maxHealth = player.maxHealth;
        healthBar.fillAmount = 1;
    }

    void Update()
    {
        healthBar.fillAmount =  Mathf.Clamp((float)player.health / maxHealth, 0, 1);
    }
}
