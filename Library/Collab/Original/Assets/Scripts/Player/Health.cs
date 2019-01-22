using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] int H_Health;
    [SerializeField] int H_HealthCurrent;
    [SerializeField] int H_PoisonDamageAmount; // amount of damage taken by poison
    [SerializeField] float H_PoisonTimeInitial; // the time it takes before you take poison damage 
    [SerializeField] float H_PoisonTime; // the time it takes before you take poison damage 
    [SerializeField] Text H_HealthText;
    [SerializeField] public HealthBar H_Healthbar;
    
    // next step is to add a life counter and check to see if the poison damage needs to occur in fixed or regular update
    void Start ()
    {
        //set our current health and time to its initial value
        H_HealthCurrent = H_Health;
        H_PoisonTime = H_PoisonTimeInitial;
        
	}
	
	void Update ()
    {
        //H_Respawn();
        H_TakePoisonDamage();
        H_UpdateHealthUI();
        UIDegubCommands();

    }

    public void H_TakePoisonDamage()
    {
        H_PoisonTime -= Time.deltaTime;
        if (H_PoisonTime <= 0)
        {
            H_HealthCurrent -= H_PoisonDamageAmount;
            H_PoisonTime = H_PoisonTimeInitial;
        }
    }

    public void H_Respawn()
    {
        if (H_HealthCurrent < 0)
        {
            //this will be filled later when we determine weather death entails resetting the room or resetting the players position
        }
    }

    public void H_TakeDamage(int damage)
    {
        int display;

        H_Healthbar.HB_TakeHealth(damage);
    }

    public void H_Heal(int amount)
    {
        H_HealthCurrent += amount;
    }

    public void H_UpdateHealthUI()
    {
        H_HealthText.text = "Health: " + H_HealthCurrent;
    }

    public int H_WhatHealth()
    {
        return H_HealthCurrent;
    }

    public void UIDegubCommands()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            H_Healthbar.HB_TakeHealth(0.5f);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            H_TakeDamage(1);
        }
    }

}
