using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject T_Arrow;
    [SerializeField] float T_DistX;
    [SerializeField] float T_DistYInitial;
    [SerializeField] float T_DistY1;
    private float T_DistY2;
    private float T_DistY3;
    [SerializeField] float T_OffsetY;
    [SerializeField] int T_Shots;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        T_DistY2 = T_DistY1 + T_OffsetY;
        T_DistY3 = T_DistY2 + T_OffsetY;
    }

    public void T_FireArrow()
    {
        if (T_Shots > 0)
        {
            Instantiate(T_Arrow, new Vector3(gameObject.transform.position.x + T_DistX, gameObject.transform.position.y), Quaternion.identity);
            T_Shots--;
            Debug.Log("Fire");
        }
    }

    public void T_MultiShot()
    {
        Instantiate(T_Arrow, new Vector3(gameObject.transform.position.x + T_DistX, gameObject.transform.position.y + T_DistY1), Quaternion.identity);
        Instantiate(T_Arrow, new Vector3(gameObject.transform.position.x + T_DistX, gameObject.transform.position.y + T_DistY2), Quaternion.identity);
        Instantiate(T_Arrow, new Vector3(gameObject.transform.position.x + T_DistX, gameObject.transform.position.y + T_DistY3), Quaternion.identity);
    }


}
