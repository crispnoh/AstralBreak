using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    //variables for player stats, can do levels and all that if we have another year lmao
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthBar; 

    //other variables
    GameObject player;
    UIFunctions uiScript;
    PlayerControl playerControl;

    void Start()
    {
        currentHealth = maxHealth;

        player = GameObject.Find("Player");
        uiScript = player.GetComponent<UIFunctions>();
        playerControl = player.GetComponent<PlayerControl>();
    }

    //functions for heal and dmg
    public void TakeDmg(int dmg)
    {
        if (currentHealth - dmg <= 0) 
        { 
            currentHealth = 0;
            playerControl.DisableControls();
            uiScript.die();            
        }
        else { currentHealth -= dmg; }
        healthBar.value = currentHealth;
    }

    public void Heal(int health)
    {
        if(currentHealth + health > maxHealth) { currentHealth = maxHealth; }
        else { currentHealth += health; }
        healthBar.value = currentHealth;
    }
}
