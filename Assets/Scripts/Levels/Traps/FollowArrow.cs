using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowArrow : MonoBehaviour
{
    private Transform FA_Player;
    private Rigidbody2D FA_Rb;
    [SerializeField] float FA_Speed;
    [SerializeField] float FA_FollowTime;
    [SerializeField] float FA_DeleteTime;
    [SerializeField] float FA_Damage;
    private float FA_YDir;
    private bool FA_HasLaunched;
    void Start()
    {
        FA_Rb = GetComponent<Rigidbody2D>();
        FA_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        FA_HasLaunched = false;
    }

    void Update()
    {
        MoveToPlayer();
    }

    public void MoveToPlayer()
    {
        FA_YDir = Random.Range(-2, 2);
        FA_FollowTime -= Time.deltaTime;
        float check = gameObject.transform.position.x - FA_Player.transform.position.x;
        if (FA_FollowTime >= 0)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, FA_Player.transform.position.y , gameObject.transform.position.z);
        }
        else if (FA_FollowTime < 0 && FA_HasLaunched == false)
        {
            FA_HasLaunched = true;
            if (check > 0)
            {
                FA_Rb.velocity = new Vector2(-FA_Speed, FA_YDir);
            }
            else if (check < 0)
            {
                FA_Rb.velocity = new Vector2(FA_Speed, FA_YDir);
            }
            else
            {
                FA_Rb.velocity = new Vector2(-FA_Speed, FA_YDir);
            }
            
        }
        if (FA_HasLaunched == true)
        {
            Destroy(gameObject, FA_DeleteTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Health>().H_DealDamage(FA_Damage);
        }
    }
}
