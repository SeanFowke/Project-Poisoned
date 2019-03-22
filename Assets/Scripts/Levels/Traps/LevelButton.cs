using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] bool LB_IsTurretButton;
    [SerializeField] bool LB_IsSpikeWallButton;
    [SerializeField] bool LB_IsSpikepit;
    [SerializeField] bool LB_IsMultiShot;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (LB_IsTurretButton == true && (col.CompareTag("Player")) && LB_IsMultiShot == false)
        {
            LB_FireOneShot();
        }
        else if (LB_IsTurretButton == true && (col.CompareTag("Player")) && LB_IsMultiShot == true)
        {
            LB_FireMultiShot();
        }
        else if (LB_IsSpikeWallButton == true && (col.CompareTag("Player")))
        {
            gameObject.BroadcastMessage("SW_SetActive");
        }
        else if (LB_IsSpikepit == true && (col.CompareTag("Player")))
        {
            gameObject.SendMessageUpwards("SP_Activate");
        }
        


    }

    public void LB_FireOneShot()
    {
        gameObject.SendMessageUpwards("T_FireArrow", "Right");
    }

    public void LB_FireMultiShot()
    {
        gameObject.SendMessageUpwards("T_MultiShot");
    }
}
