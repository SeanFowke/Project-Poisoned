using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikepit : MonoBehaviour
{
    private bool SP_IsActive;
    private bool SP_Hurt;
    [SerializeField] float SP_ActiveTimerInitial;
    [SerializeField] float SP_InActiveTimerInitial;
    private SpriteRenderer SP_Sr;
    private float SP_ActiveTimer;
    private float SP_InActiveTimer;
    void Start()
    {
        SP_ActiveTimer = SP_ActiveTimerInitial;
        SP_InActiveTimer = SP_InActiveTimerInitial;
        SP_Sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SP_EngageSpikePit();
        Debug.Log(SP_Hurt);
    }

    public void SP_EngageSpikePit()
    {
        SP_InActiveTimer -= Time.deltaTime;
        if(SP_InActiveTimer <=0)
        {
            SP_Hurt = true;
            SP_ActiveTimer -= Time.deltaTime;
            SP_Sr.color = new Color(255.0f, 0.0f, 0.0f);
            if (SP_ActiveTimer <= 0)
            {
                SP_Hurt = false;
                SP_ActiveTimer = SP_ActiveTimerInitial;
                SP_IsActive = false;
                SP_Sr.color = new Color(255.0f, 255.0f, 255.0f);
                SP_InActiveTimer = SP_InActiveTimerInitial;
            }
        }
            
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && SP_Hurt == true)
        {
            col.GetComponent<Health>().H_DealDamage(1);
        }

    }
}
