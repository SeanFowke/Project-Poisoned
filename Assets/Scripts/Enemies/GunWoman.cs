using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWoman : Enemy {

	//Attacking parameters
	private bool GW_Shooting;
	private Vector2 GW_BulletPos;
	private GameObject GW_InstaBullet;
	private bool GW_HasShot;
	private float GW_BetweenShotsTimer;
	private float GW_BulletInstantiationTimer;
	private Vector2 GW_BulletForce;
	private Vector2 GW_BulletForceVertical;
	private int GW_BulletDirection;


	//Special parameters
	private int GW_ShotsCount;
	private Vector2 GW_CurrentPos;
	private bool GW_GoneUp;
	private int GW_SpecialCount;
	private float GW_ShootVerticalTimer;
	private int GW_HoizontalSpecialDirection;
	private ParticleSystem GW_ParticleSystem;

	//Reloading parameters
	private float GW_ReloadingTimer;
	private Vector2 GW_ReloadingPos;

	public GameObject GW_Bullet;
	public GameObject GW_Reloading;
	private GameObject GW_InstaReloading;
	private bool GW_HasReloading;

	// Use this for initialization
	void Start () {
		E_Start();
		E_Health = 30;
		E_RaycastBlocked = 0.0f;
		GW_BulletPos = new Vector2(gameObject.transform.position.x - 0.1f, gameObject.transform.position.y);
		GW_HasShot = false;
		GW_Shooting = false;
		GW_BulletInstantiationTimer = 0.4f;
		GW_BetweenShotsTimer = 0.3f;
		GW_BulletForce = new Vector2(350.0f, 0.0f);
		GW_BulletForceVertical = new Vector2(0.0f, 350.0f);
		GW_GoneUp = false;
		GW_ParticleSystem = gameObject.GetComponent<ParticleSystem>();
		//Used to determine when the character will move horizontally
		GW_SpecialCount = 0;
		GW_ShootVerticalTimer = 0.2f;
		GW_ReloadingTimer = 2.0f;
		GW_ReloadingPos = gameObject.transform.position;
		GW_HasReloading = false;



		if (E_Transform.rotation==new Quaternion(0.0f,180.0f,0.0f,0.0f))
		{
			//Start facing right
			GW_BulletDirection = 1;
			GW_HoizontalSpecialDirection = -1;
		}
		else
		{
			//Start facing left
			GW_BulletDirection = -1;
			GW_HoizontalSpecialDirection = 1;
		}


		//Special

		GW_CurrentPos = gameObject.transform.position;
		GW_ShotsCount = 0;
	}

	// Update is called once per frame
	void Update()
	{

		//Debug.Log(GW_BulletDirection);

		E_PlayerPosition = E_FindPlayer.transform.position;
		E_DistanceFromPlayer = new Vector2(E_PlayerPosition.x - this.gameObject.transform.position.x, E_PlayerPosition.y - this.gameObject.transform.position.y);


		//Determine which animation we're on
		E_Animator.SetBool("EGW_Shooting", GW_Shooting);
		E_Animator.SetBool("EGW_HasShot", GW_HasShot);

		if (E_CurrentState != E_State.Staggered)
		{

			if (E_CurrentState == E_State.Reloading)
			{
				GW_ReloadingPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 2.0f);
				if (GW_ReloadingTimer >= 0.0f)
				{
					if (!GW_HasReloading)
					{
						GW_InstaReloading = (GameObject)Instantiate(GW_Reloading, GW_ReloadingPos, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
						GW_HasReloading = true;
					}
					GW_ReloadingTimer -= Time.deltaTime;
				}
				else
				{
					Destroy(GW_InstaReloading);
					GW_ReloadingTimer = 2.0f;
					GW_HasReloading = false;
					E_CurrentState = E_State.Patrolling;
				}
			}


			//Check if the enemy is not doing her special move
			else if (E_CurrentState != E_State.Special)
			{
				GW_ParticleSystem.Stop();

				if (Mathf.Abs(E_DistanceFromPlayer.x) <= 8.0f) //&& Mathf.Abs(E_DistanceFromPlayer.y) <= 5.0f)
				{
					//Player is close enough, start shooting
					E_CurrentState = E_State.Attacking;
				}
				else
				{
					//If we go back to patrolling, reset everything
					E_CurrentState = E_State.Patrolling;
					GW_Shooting = false;
					GW_HasShot = false;
					GW_BulletInstantiationTimer = 0.4f;
					GW_BetweenShotsTimer = 0.3f;
					GW_ShotsCount = 0;
				}

				if (E_CurrentState == E_State.Attacking)
				{
					GW_Shooting = true;

					GW_BulletPos = new Vector2(gameObject.transform.position.x - 0.1f, gameObject.transform.position.y);

					//If we are in attacking state, start shooting while alternating between shooting and between shots animations
					if (GW_Shooting && !GW_HasShot)
					{

						if (GW_BulletInstantiationTimer <= 0.0f)
						{
							GW_InstaBullet = (GameObject)Instantiate(GW_Bullet, GW_BulletPos, transform.rotation);
							GW_InstaBullet.GetComponent<Rigidbody2D>().AddRelativeForce(GW_BulletForce * GW_BulletDirection);
							GW_InstaBullet.transform.localScale = gameObject.transform.localScale;
							GW_ShotsCount++;
							GW_HasShot = true;
							GW_BulletInstantiationTimer = 0.4f;
						}
						else
						{
							GW_BulletInstantiationTimer -= Time.deltaTime;
						}

					}
					//Give 0.3f delay between each shot
					if (GW_HasShot)
					{
						if (GW_BetweenShotsTimer >= 0.0f)
						{
							GW_BetweenShotsTimer -= Time.deltaTime;
						}
						else
						{
							GW_HasShot = false;
							GW_BetweenShotsTimer = 0.3f;
						}
					}
					//If more than three shots have been shot, go into special state.
					if (GW_ShotsCount >= 3)
					{
						E_CurrentState = E_State.Special;
						GW_ShotsCount = 0;
						GW_Shooting = false;
						GW_HasShot = false;
						GW_CurrentPos = gameObject.transform.position;
					}
				}
			}

			//In the special state, go up, then return to patrolling. The only reason we go back to this state is let the system calcualte the distance between
			//the enemy and the player again so that if the player is far away, the enemy will not get stuck in an attacking-special loop
			else
			{
				GW_ParticleSystem.Play();

				if (GW_SpecialCount >= 3 && GW_GoneUp)
				{
					//Add the velocity based on which directions the enemy was facing when the scene was loaded
					E_RigidBody.velocity = new Vector2(-8.0f * GW_HoizontalSpecialDirection, 0.0f);
					//Shoot bullets vertically while moveing to the left

					GW_ShootVertically(GW_GoneUp);

					if (Mathf.Abs(gameObject.transform.position.x - GW_CurrentPos.x) >= 10.0f)
					{
						//Once the enemy has moved 10 units, stop moving and flip.
						E_RigidBody.velocity = new Vector2(0.0f, 0.0f);
						//flip
						Vector3 P_Scale = gameObject.transform.localScale;
						P_Scale.x *= -1;
						gameObject.transform.localScale = P_Scale;
						//flip bullet direction
						GW_BulletDirection *= -1;
						//Start reloading
						E_CurrentState = E_State.Reloading;
						GW_SpecialCount = 0;
					}
				}
				else if (GW_SpecialCount >= 3 && !GW_GoneUp)
				{
					E_RigidBody.velocity = new Vector2(8.0f * GW_HoizontalSpecialDirection, 0.0f);
					GW_ShootVertically(GW_GoneUp);
					if (Mathf.Abs(gameObject.transform.position.x - GW_CurrentPos.x) >= 10.0f)
					{
						E_RigidBody.velocity = new Vector2(0.0f, 0.0f);
						Vector3 P_Scale = gameObject.transform.localScale;
						P_Scale.x *= -1;
						gameObject.transform.localScale = P_Scale;
						GW_BulletDirection *= -1;

						E_CurrentState = E_State.Reloading;
						GW_SpecialCount = 0;
					}
				}

				else if (!GW_GoneUp)
				{
					E_RigidBody.velocity = new Vector2(0.0f, 8.0f);

					if (Mathf.Abs(gameObject.transform.position.y - GW_CurrentPos.y) >= 4.0f)
					{
						E_RigidBody.velocity = new Vector2(0.0f, 0.0f);
						GW_GoneUp = true;
						E_CurrentState = E_State.Patrolling;
						GW_SpecialCount++;
					}
				}
				else
				{
					E_RigidBody.velocity = new Vector2(0.0f, -8.0f);
					if (Mathf.Abs(gameObject.transform.position.y - GW_CurrentPos.y) >= 4.0f)
					{
						E_RigidBody.velocity = new Vector2(0.0f, 0.0f);
						GW_GoneUp = false;
						E_CurrentState = E_State.Patrolling;
						GW_SpecialCount++;
					}
				}
			}

		}
		else
		{
			if(E_StaggeredTimer>0.0f)
			{
				E_StaggeredTimer -= Time.deltaTime;
			}
			else
			{
				E_Animator.enabled = true;
				E_CurrentState = E_PreviousState;
				E_SpriteRenderer.color = Color.white;				
				E_StaggeredTimer = 0.05f;
			}

		}
	}

	private void GW_ShootVertically(bool HasGoneUp)
	{
		//if the enemy is dow, shoot up, and vice versa.
		if(HasGoneUp)
		{
			if (GW_ShootVerticalTimer <= 0.0)
			{
				GW_BulletPos = gameObject.transform.position;
				GW_InstaBullet = (GameObject)Instantiate(GW_Bullet, GW_BulletPos, transform.rotation);
				GW_InstaBullet.GetComponent<Rigidbody2D>().AddForce(GW_BulletForceVertical * -1.0f);
				GW_InstaBullet.transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
				GW_ShootVerticalTimer = 0.2f;
			}
			else
			{
				GW_ShootVerticalTimer -= Time.deltaTime;
			}
		}
		else
		{
			if (GW_ShootVerticalTimer <= 0.0)
			{
				GW_BulletPos = gameObject.transform.position;
				Quaternion GW_Rotation = new Quaternion(0.0f, 0.0f, -90.0f, 0.0f);
				GW_InstaBullet = (GameObject)Instantiate(GW_Bullet, GW_BulletPos, GW_Rotation);
				GW_InstaBullet.GetComponent<Rigidbody2D>().AddForce(GW_BulletForceVertical);
				GW_InstaBullet.transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
				GW_ShootVerticalTimer = 0.2f;
			}
			else
			{
				GW_ShootVerticalTimer -= Time.deltaTime;
			}
		}
	}
}
