using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Player player;
    public Image healthBar;
    private int maxHealth = 100;
    private int totalGrenades = 0;
    private Weapon weapon;

    void Start()
    {
        maxHealth = player.maxHealth;
        healthBar.fillAmount = 1;
        weapon = player.GetComponent<Weapon>();
        if (weapon != null) {
            totalGrenades = weapon.grenadesQuantity;
            // if totalGrenades > 0, then show all grenades
            if (transform.childCount > 0 && totalGrenades > 0) {
                for (int i = 0; i < totalGrenades; i++) {
                    this.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }

    void Update()
    {
        healthBar.fillAmount =  Mathf.Clamp((float)player.health / maxHealth, 0, 1);
        if (weapon != null &&  weapon.grenadesQuantity < totalGrenades && transform.childCount > 0) {
            totalGrenades = weapon.grenadesQuantity;
            Destroy(this.transform.GetChild(totalGrenades).gameObject);
        }
    }
}
