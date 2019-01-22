using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public static Inventory Instance = null;
	private GameObject DummyObject;
	private InventoryItem Item;
	private InventoryItem ItemToAdd;
	//The list which contains what items the player actually has at the moment
	//private List<InventoryItem> IN_PlayerItems = new List<InventoryItem>();

	public InventoryItem IN_PlayerMainWeapon;
	public InventoryItem IN_PlayerSecondaryWeapon;
	public InventoryItem IN_PlayerItem;

	//Create a dictionary of InventoryItem objects which can be indexed using a string.
	//The idea is: we create a database of InventoryItem objects and we index this database using their names

	private Dictionary<string,InventoryItem> IN_InventoryDictionary = new Dictionary<string,InventoryItem>();

	private VGUI IN_VGUI;


	private void Awake()
	{
		//Only one inventory game object should exist
		if(Instance==null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	//Create a new preset of the item and store it in the database
	private void IN_SetPreset(string Name, InventoryItem Preset)
	{
		IN_InventoryDictionary.Add(Name, Preset);
		
	}

	//Adds item to the player's inventory
	public void IN_AddNewItemToPlayer(string Name)
	{
		//Debug.Log(Name + " Added to inventory");
		//Get the preset information.
		//Preset information is hardcoded in the Start function
		//It's like getting data from a database which already has all of its data written.

		DummyObject = new GameObject();
		DummyObject.AddComponent<InventoryItem>();
		ItemToAdd = DummyObject.GetComponent<InventoryItem>();

		//Copy constructor
		ItemToAdd.InventoryItemCopy(IN_InventoryDictionary[Name]);

		if(ItemToAdd.IT_IsMainWeapon)
		{
			IN_PlayerMainWeapon = ItemToAdd;
			//Change GUI
			IN_VGUI.V_MainWeaponExists = true;
			IN_VGUI.V_MainWeaponName = IN_PlayerMainWeapon.IT_Name;
		}
		else if( ItemToAdd.IT_IsItem)
		{
			IN_PlayerItem = ItemToAdd;
			//Change GUI
			IN_VGUI.V_ItemExists = true;
			IN_VGUI.V_ItemName = IN_PlayerItem.IT_Name;
		}
		else
		{
			IN_PlayerSecondaryWeapon = ItemToAdd;
			//Change GUI
			IN_VGUI.V_SecondaryWeaponExists = true;
			IN_VGUI.V_SecondaryWeaponName = IN_PlayerSecondaryWeapon.IT_Name;
		}
		//Debug.Log("Hey from the inventory " + ItemToAdd.IT_IsMainWeapon);

		Destroy(DummyObject);
	}

	//On start, create the database
	private void Start()
	{ 

		if (IN_VGUI = GameObject.Find("VGUI").GetComponent<VGUI>())
		{
			//Do nothing
		}
		else
		{
			Debug.Log("VGUI not found");
		}


		//Create a dummy object
		//Attach a script to it
		//Item = this script
		//Insert this object into the dictionary
		//Destroy this item.
		//The reason we need to do this is because MonoBehaviour does not like constructors so we cannot simply do Item = new InventoryItem...etc


		DummyObject = new GameObject();
		DummyObject.AddComponent<InventoryItem>();
		Item = DummyObject.GetComponent<InventoryItem>();


		//InventoryItem(ItemName, IsMainWeapon, IsItem)

		//Main Weapons

		Item.InventoryItemInit("Sword", true, false,1);
		IN_SetPreset("Sword", Item);

		Destroy(DummyObject);

		DummyObject = new GameObject();
		DummyObject.AddComponent<InventoryItem>();
		Item = DummyObject.GetComponent<InventoryItem>();
	

		Item.InventoryItemInit("Axe", true, false,2);
		IN_SetPreset("Axe", Item);

		Destroy(DummyObject);

		DummyObject = new GameObject();
		DummyObject.AddComponent<InventoryItem>();
		Item = DummyObject.GetComponent<InventoryItem>();


		Item.InventoryItemInit("Spear", true, false,3);
		IN_SetPreset("Spear", Item);

		Destroy(DummyObject);

		DummyObject = new GameObject();
		DummyObject.AddComponent<InventoryItem>();
		Item = DummyObject.GetComponent<InventoryItem>();


		//Secondary Weapons

		Item.InventoryItemInit("DaggerPoison", false, false,1);
		IN_SetPreset("DaggerPoison", Item);

		Destroy(DummyObject);

		DummyObject = new GameObject();
		DummyObject.AddComponent<InventoryItem>();
		Item = DummyObject.GetComponent<InventoryItem>();


		Item.InventoryItemInit("DaggerBleeding", false, false,2);
		IN_SetPreset("DaggerBleeding", Item);

		Destroy(DummyObject);

		DummyObject = new GameObject();
		DummyObject.AddComponent<InventoryItem>();
		Item = DummyObject.GetComponent<InventoryItem>();


		Item.InventoryItemInit("Lightning", false, false,3);
		IN_SetPreset("Lightning", Item);

		Destroy(DummyObject);

		DummyObject = new GameObject();
		DummyObject.AddComponent<InventoryItem>();
		Item = DummyObject.GetComponent<InventoryItem>();


		Item.InventoryItemInit("Fire", false, false,4);
		IN_SetPreset("Fire", Item);

		Destroy(DummyObject);

		DummyObject = new GameObject();
		DummyObject.AddComponent<InventoryItem>();
		Item = DummyObject.GetComponent<InventoryItem>();


		Item.InventoryItemInit("Ice", false, false,5);
		IN_SetPreset("Ice", Item);

		Destroy(DummyObject);

		DummyObject = new GameObject();
		DummyObject.AddComponent<InventoryItem>();
		Item = DummyObject.GetComponent<InventoryItem>();


		Item.InventoryItemInit("GravityBall", false, false,6);
		IN_SetPreset("GravityBall", Item);

		Destroy(DummyObject);

		DummyObject = new GameObject();
		DummyObject.AddComponent<InventoryItem>();
		Item = DummyObject.GetComponent<InventoryItem>();
	


		//Items

		Item.InventoryItemInit("Key", false, true,1);
		IN_SetPreset("Key", Item);

		Destroy(DummyObject);


	}



}

