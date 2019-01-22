using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour {


	public string IT_Name;
	//True: Main Weapon
	//False: Secondary Weapon
	public bool IT_IsMainWeapon;
	//False for weapons, true for items such as keys
	public bool IT_IsItem;

	public int IT_ID;

	//Copy constructor
	public void InventoryItemCopy(InventoryItem ItemToCopy)
	{
		IT_Name = ItemToCopy.IT_Name;
		IT_IsMainWeapon = ItemToCopy.IT_IsMainWeapon;
		IT_IsItem = ItemToCopy.IT_IsItem;
		IT_ID = ItemToCopy.IT_ID;
	}

	//Constructor
	public void InventoryItemInit(string ItemName, bool ItemMain, bool IsItem, int ID)
	{
		IT_Name = ItemName;
		IT_IsMainWeapon = ItemMain;
		IT_IsItem = IsItem;
		IT_ID = ID;

		//Debug.Log(IT_Name + " " + IT_IsMainWeapon);
	}

}
