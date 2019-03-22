using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] string A_Dir;
    [SerializeField] float A_Speed;
    [SerializeField] float A_Damage;
    private Rigidbody2D A_Rb;
    void Start()
    {
        A_Rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        A_MoveArrow();
    }
    
    public void A_MoveArrow()
    {
        if (A_Dir == "Right")
        {
            A_Rb.velocity = new Vector2(A_Speed, 0);
        }
        if (A_Dir == "Left")
        {
            A_Rb.velocity = new Vector2(-A_Speed, 0);
        }
        if (A_Dir == "Up")
        {
            A_Rb.velocity = new Vector2(0, -A_Speed);
        }
        if (A_Dir == "Down")
        {
            A_Rb.velocity = new Vector2(0, A_Speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Health>().H_DealDamage(A_Damage);
        }
    }
}
