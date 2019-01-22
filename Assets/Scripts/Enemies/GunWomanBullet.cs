using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWomanBullet : MonoBehaviour {

	public int GWB_Damage;


	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag.Equals("Player"))
		{
			col.gameObject.GetComponent<Health>().H_DealDamage(GWB_Damage);
		}
	}
}
