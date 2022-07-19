using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    //variables for player stats, can do levels and all that if we have another year lmao
    public int maxHealth = 100;
    public int maxMana = 100;

    public int currentHealth;
    public int currentMana;

    public Slider healthBar;
    public Slider manaBar;

    //other variables
    GameObject player;
    UIFunctions uiScript;
    PlayerControl playerControl;

    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;

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

    public void TakeHeal(int health)
    {
        if(currentHealth + health > maxHealth) { currentHealth = maxHealth; }
        else { currentHealth += health; }
        healthBar.value = currentHealth;
    }

    //functions for mana
    public void UseMana(int mana)
    {
        if(currentMana - mana >= 0) { currentMana-= mana; }
        manaBar.value = currentMana;
    }

    public void GainMana(int mana)
    {
        if (currentMana + mana > maxMana) { currentMana = maxMana; }
        else { currentMana += mana; }
        manaBar.value = currentMana;
    }
}
