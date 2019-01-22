using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Health : MonoBehaviour {
    [SerializeField] float H_Health; // total health
    [SerializeField] float H_HealthCurrent; // current health
    [SerializeField] float H_PoisonDamageAmount; // amount of damage taken by poison
    [SerializeField] float H_PoisonTimeInitial; // the time it takes before you take poison damage 
    [SerializeField] float H_PoisonTime; // the time it takes before you take poison damage 
    [SerializeField] float H_UIChangePerSecond; // how much we want the Ui to change per second
    [SerializeField] float H_PoisonUIChangePerSecond; // same applies to the poison bar
    [SerializeField] Slider H_PoisonBar; // poison bar object 
    [SerializeField] Slider H_Healthbar; // healthbar object
    private bool H_IsHeal;
    void Start()
    {
        H_HealthCurrent = H_Health;
        H_PoisonTime = H_PoisonTimeInitial;
        H_Healthbar.value = H_CalculateHealthDisplay();
        H_IsHeal = false;
    }

    void Update()
    {
        H_Respawn();
        H_TakePoisonDamage();
        H_UpdateHealthUI();
        H_UpdatePoisonUI();
        UIDebugCommands();


    }

    public void H_TakePoisonDamage()
    {
        // takes off a set amount of damage after a set amount of time
        H_PoisonTime -= Time.deltaTime;
        if (H_PoisonTime <= 0)
        {
            H_DealDamage(H_PoisonDamageAmount);
            H_PoisonTime = H_PoisonTimeInitial;
        }
    }

    public void H_Respawn()
    {
        if (H_HealthCurrent <= 0)
        {
            
            H_HealthCurrent = H_Health;
            H_Healthbar.value = 1;
            // need to decide if we want the scene to reset or just the player reset
        }
    }

    public void H_DealDamage(float damage)
    {

        H_HealthCurrent -= damage;
        H_IsHeal = false;

    }

    public void H_Heal(float amount)
    {
        // heal the player by a set amount
        H_HealthCurrent += amount;
        H_IsHeal = true;
    }

    public void H_UpdateHealthUI()
    {
        // update the health bar
        if (H_IsHeal == false)
        {
            float decresedisplay;
            decresedisplay = H_Healthbar.value - H_UIChangePerSecond * Time.deltaTime;
            if (decresedisplay >= H_CalculateHealthDisplay())
            {
                H_Healthbar.value = decresedisplay;
            }
        }
        if (H_IsHeal == true)
        {
            float decresedisplay;
            decresedisplay = H_Healthbar.value + H_UIChangePerSecond * Time.deltaTime;
            if (decresedisplay <= H_CalculateHealthDisplay())
            {
                H_Healthbar.value = decresedisplay;
            }
        }

    }
    

    public void H_UpdatePoisonUI()
    {
        //update the poison bar
        float display;
        display = H_PoisonBar.value - H_PoisonUIChangePerSecond * Time.deltaTime;
        if (display >= H_CalculatePoisonDisplay())
        {
            H_PoisonBar.value = display;
        }
        if (H_PoisonBar.value <= 0.01)
        {
            H_PoisonBar.value = 1;
        }
        
        
    }

    public float H_WhatHealth()
    {
        // returns our current health
        return H_HealthCurrent;
    }

    public void UIDebugCommands()
    {
        // extra commands used for debugging
        if (Input.GetKeyDown(KeyCode.Q))
        {
            H_DealDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            H_Heal(2);
        }
    }

    

    public float H_CalculateHealthDisplay()
    {
        // getting the percent so we can set the value of the health bar to that
        return H_HealthCurrent / H_Health;
    }

    public float H_CalculatePoisonDisplay()
    {
        return H_PoisonTime / H_PoisonTimeInitial;
    }

}



/*
 *    I   Il
 *    Il  I_
 */
