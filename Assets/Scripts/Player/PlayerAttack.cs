﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	//States
	private enum PA_States
	{
		Idle,
		Main,
		Sub1,
		Sub2,
		Sub3
	};

	private PA_States PA_CurrentState;
	private PA_States PA_NewState;
	private bool PA_ChangeStates;
	private bool PA_ResetState;
	private float PA_StateTimer;
	private float PA_ResetStateTimer;

	//Animation
	private Animator PA_Animator;
	private bool PA_AttackLeft;
	private bool PA_AttackRight;
	private bool PA_AttackUp;
	private bool PA_AttackDown;
	private Vector2 PA_AttackDirection;
	private bool PA_HasAttackedSword;
	
	//Find
	private GameObject PA_FindObject;
	private Inventory PA_Inventory;
	private VGUI PA_VGUI;

	//Inflict Damage
	private int PA_SwordAttackDamage;
	public LayerMask PA_EnemiesToDamageMask;
	private Vector2 PA_AttackBoxCenter;
	private Vector2 PA_AttackBoxSize;
	private bool PA_InflictDamage;
	private int PA_Direction;
	private float PA_InflictDamageTimer;
	private bool PA_HasInflictedDamage;

	
	//The player needs to be aware of all the possible weapons he can use
	public InventoryItem PA_Sword;
	public InventoryItem PA_Axe;
	public InventoryItem PA_Spear;
	public InventoryItem PA_DaggerBleeding;
	public InventoryItem PA_DaggerPoison;
	public InventoryItem PA_Fire;
	public InventoryItem PA_Lightning;
	public InventoryItem PA_Ice;
	public InventoryItem PA_GravityBall;

	private PlayerController PA_PlayerController;
	private float PA_FreezeTimer;
	private bool PA_Frozen;


	// Use this for initialization
	void Start () {

		//Find Objects
		if (PA_VGUI = GameObject.Find("VGUI").GetComponent<VGUI>())
		{
			//Do nothing
		}
		else
		{
			Debug.Log("VGUI not found, PLayer Attack");
		}

		if (PA_FindObject = GameObject.Find("Inventory"))
		{
			PA_Inventory = PA_FindObject.GetComponent<Inventory>();
		}
		else
		{
			Debug.Log("Player Attack could not find Inventory");
		}

		//Animation
		PA_Animator = gameObject.GetComponent<Animator>();
		PA_AttackLeft = false;
		PA_AttackRight = false;
		PA_AttackUp = false;
		PA_AttackDown = false;
		PA_HasAttackedSword = false;

		//States
		PA_CurrentState = PA_States.Idle;
		PA_NewState = PA_States.Idle;
		PA_ChangeStates = false;
		PA_StateTimer = 0.1f;
		PA_ResetStateTimer = 0.2f;


		//Attack direction
		PA_PlayerController = gameObject.GetComponent<PlayerController>();
		PA_AttackDirection = PA_PlayerController.P_PlayerDirection;
		PA_FreezeTimer = 0.1f;
		PA_Frozen = false;


		//Inflict Damage
		PA_SwordAttackDamage = 5;
		PA_AttackBoxSize = new Vector2(1.13f, 0.85f);
		PA_InflictDamage = false;
		PA_InflictDamageTimer = 0.3f;
		PA_HasInflictedDamage = false;

		

	}
	
	// Update is called once per frame
	void Update () {

		if(PA_Frozen)
		{
			if(PA_FreezeTimer>0.0f)
			{
				PA_FreezeTimer -= Time.deltaTime;
			}
			else
			{
				PA_FreezeTimer = 0.1f;
				PA_PlayerController.UnFreeze();
				PA_Frozen = false;
			}
		}

		if (PA_HasAttackedSword)
		{
			PA_Animator.SetBool("Attack_Left", PA_AttackLeft);
			PA_Animator.SetBool("Attack_Right", PA_AttackRight);
			PA_Animator.SetBool("Attack_Up", PA_AttackUp);
			PA_Animator.SetBool("Attack_Down", PA_AttackDown);
			PA_HasAttackedSword = false;
			PA_InflictDamage = true;
		}

		//Timer is needed to make sure the overlap collider is created when the animation is played not before.
		if(PA_InflictDamage)
		{
			if(PA_InflictDamageTimer>0)
			{
				PA_InflictDamageTimer -= Time.deltaTime;
			}
			else
			{
				PA_Attack(PA_Direction);
			}
		}

		//If the player moves in a certain dirction, update the attack direction
		if (PA_PlayerController.P_PlayerDirection.x!=0.0f || PA_PlayerController.P_PlayerDirection.y !=0.0f)
		{
			PA_AttackDirection = PA_PlayerController.P_PlayerDirection;
		}
		
		//Timer for each state, to make sure the attack finishes before the next one starts
		if (PA_ChangeStates)
		{
			if(PA_StateTimer<=0)
			{
				PA_StateTimer = 0.1f;
				PA_ChangeStates = false;
				PA_CurrentState = PA_NewState;
			}
			else
			{
				PA_StateTimer -= Time.deltaTime;
			}

		}
		//Return to Idle state and animation
		if(PA_ResetState)
		{
			if(PA_ResetStateTimer<=0)
			{
				PA_CurrentState = PA_States.Idle;
				PA_ResetStateTimer = 0.2f;
				PA_AttackDown = false;
				PA_AttackLeft = false;
				PA_AttackRight = false;
				PA_AttackUp = false;
				PA_HasAttackedSword = true;
				PA_ResetState = false;
				PA_InflictDamage = false;
				PA_InflictDamageTimer = 0.3f;
				PA_HasInflictedDamage = false;
			}
			else
			{
				PA_ResetStateTimer -= Time.deltaTime;
				
			}
		}

		//Check which direction the player is facing, and based on that create the overlap box to damage enemies
		if (PA_CurrentState == PA_States.Idle)
		{
			if (Input.GetButtonDown("CTRL - X"))
			{
				PA_CurrentState = PA_States.Main;

				if (PA_AttackDirection.x > 0)
				{
					PA_AttackRight = true;
					PA_HasAttackedSword = true;

					if(PA_PlayerController.Freeze())
					{
						
						PA_Frozen = true;
						PA_PlayerController.P_rb.AddRelativeForce(PA_AttackDirection * 150.0f);
						
					}

					PA_Direction = 1;
					PA_HasInflictedDamage = false;

				}
				else if (PA_AttackDirection.x < 0)
				{
					PA_AttackLeft = true;
					PA_HasAttackedSword = true;

					if (PA_PlayerController.Freeze())
					{
						
						PA_Frozen = true;
						PA_PlayerController.P_rb.AddRelativeForce(PA_AttackDirection * 150.0f);
					}

					PA_Direction = 2;
					PA_HasInflictedDamage = false;
				}
				else if (PA_AttackDirection.y > 0)
				{
					PA_AttackUp = true;
					PA_HasAttackedSword = true;

					if (PA_PlayerController.Freeze())
					{

						PA_Frozen = true;
						PA_PlayerController.P_rb.AddRelativeForce(PA_AttackDirection * 150.0f);
					}

					PA_Direction = 3;
					PA_HasInflictedDamage = false;
				}
				else if (PA_AttackDirection.y < 0)
				{
					PA_AttackDown = true;
					PA_HasAttackedSword = true;

					if (PA_PlayerController.Freeze())
					{

						PA_Frozen = true;
						PA_PlayerController.P_rb.AddRelativeForce(PA_AttackDirection * 150.0f);
					}

					PA_Direction = 4;
					PA_HasInflictedDamage = false;
				}
				else
				//The player is spawned facing downwards, so if he attack before moving at all, the direction will be downwards
				{
					PA_AttackDown = true;
					PA_HasAttackedSword = true;

					if (PA_PlayerController.Freeze())
					{

						PA_Frozen = true;
						PA_PlayerController.P_rb.AddRelativeForce(PA_AttackDirection * 150.0f);
					}

					PA_Direction = 4;
					PA_HasInflictedDamage = false;
				}
				

				PA_ResetState = true;
			}
		}
		
	}

	private void OnDrawGizmosSelected()
	{
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(PA_AttackBoxCenter,PA_AttackBoxSize);
		
	}

	//Method used to create an overlap collider and damage all the enemies within those boundaries. Takes an integer to determine direction
	private void PA_Attack(int Direction)
	{
		Collider2D[] EnemiesToDamage;

		//1 Right
		//2 Left
		//3 Up
		//4 Down

		//Boolean used to make sure the enemy is damaged only once
		if (!PA_HasInflictedDamage)
		{
			switch (Direction)
			{
				case 1:

					PA_AttackBoxCenter = new Vector2(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y - 0.2f);
					break;

				case 2:

					PA_AttackBoxCenter = new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y - 0.2f);
					break;
				case 3:

					PA_AttackBoxCenter = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f);
					break;
				case 4:

					PA_AttackBoxCenter = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f);
					break;

				default:
					break;
			}

			EnemiesToDamage = Physics2D.OverlapBoxAll(PA_AttackBoxCenter, PA_AttackBoxSize, 0.0f, PA_EnemiesToDamageMask);
			for (int i = 0; i < EnemiesToDamage.Length && EnemiesToDamage.Length > 0 && PA_InflictDamage; i++)
			{
				EnemiesToDamage[i].GetComponent<Enemy>().E_TakeDamage(PA_SwordAttackDamage);
			}

			PA_InflictDamage = false;
			PA_InflictDamageTimer = 0.3f;
			PA_HasInflictedDamage = true;

		}


	}
}
