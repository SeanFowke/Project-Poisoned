using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWall : MonoBehaviour
{
    [SerializeField] float SW_TimerInitial;
    [SerializeField] float SW_Damage;
    private float SW_Timer;
    private bool SW_IsActive;
    private Animator SW_Anim;
    void Start()
    {
        SW_Timer = SW_TimerInitial;
        SW_Anim = gameObject.GetComponent<Animator>();
        SW_IsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        SW_Move();
    }

    public void SW_Move()
    {
       
        if (SW_IsActive == true)
        {
            SW_Anim.SetBool("Move", true);
            SW_Timer -= Time.deltaTime;
        }
        if (SW_Timer <= 0)
        {
            SW_IsActive = false;
            SW_Timer = SW_TimerInitial;
            SW_Anim.SetBool("Move", false);
        }
    }

    public void SW_SetActive()
    {
        SW_IsActive = true;
       
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Health>().H_DealDamage(SW_Damage);
        }
    }
}
