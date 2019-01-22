using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VGUI : MonoBehaviour {

	public bool V_MainWeaponExists;
	public bool V_SecondaryWeaponExists;
	public bool V_ItemExists;
	public string V_MainWeaponName;
	public string V_SecondaryWeaponName;
	public string V_ItemName;
	private Rect V_ItemsRect;
	private Vector2 V_ItemRectSize;
	private Texture2D V_TextureToShow;

	

	// Use this for initialization
	void Start () {

		V_MainWeaponExists = false;
		V_SecondaryWeaponExists = false;
		V_ItemExists = false;
		V_MainWeaponName = "";
		V_SecondaryWeaponName = "";
		V_ItemName = "";
		V_ItemRectSize = new Vector2(50.0f, 50.0f);
		V_ItemsRect = new Rect(Screen.width / 2.2f, Screen.height/1.2f, V_ItemRectSize.x, V_ItemRectSize.y);
	}


	private void OnGUI()
	{
		V_ItemsRect = new Rect(Screen.width / 2.3f, Screen.height / 1.2f, V_ItemRectSize.x, V_ItemRectSize.y);
		GUI.Box(V_ItemsRect,"");

		if(V_MainWeaponExists)
		{
			V_TextureToShow = Resources.Load("Textures/" +V_MainWeaponName, typeof(Texture2D)) as Texture2D;
			GUI.DrawTexture(V_ItemsRect, V_TextureToShow);
		}

		V_ItemsRect = new Rect(Screen.width / 1.95f , Screen.height / 1.2f, V_ItemRectSize.x, V_ItemRectSize.y);
		GUI.Box(V_ItemsRect, "");

		if(V_SecondaryWeaponExists)
		{
			V_TextureToShow = Resources.Load("Textures/" + V_SecondaryWeaponName, typeof(Texture2D)) as Texture2D;
			GUI.DrawTexture(V_ItemsRect, V_TextureToShow);
		}

		V_ItemsRect = new Rect(Screen.width / 1.69f, Screen.height / 1.2f, V_ItemRectSize.x, V_ItemRectSize.y);
		GUI.Box(V_ItemsRect, "");

		if(V_ItemExists)
		{
			V_TextureToShow = Resources.Load("Textures/" + V_ItemName, typeof(Texture2D)) as Texture2D;
			GUI.DrawTexture(V_ItemsRect, V_TextureToShow);
		}

	}
}
