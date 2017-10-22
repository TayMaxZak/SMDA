using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Abilities : MonoBehaviour
{

	[Header("Sword")]
	[SerializeField]
	private float swordCooldown = 0.6f; // Time you must wait after using the sword
	[SerializeField]
	private bool canSword = true;
	[SerializeField]
	private Melee swordTrigger; // The actual boomerang projectile
	[SerializeField]
	private float swordDmg = 1f;
	private float swordLife = 0.15f; // How long the melee trigger lasts


	[Header("Boomerang")]
	[SerializeField]
	private float boomerangCooldown = 9; // Time you must wait after using the boomerang
	[SerializeField]
	private bool canBoomerang = true;
	[SerializeField]
	private Transform boomerangSpawn; // Where the boomerang is spawned
	[SerializeField]
	private GameObject boomerangPrefab; // The actual boomerang projectile
	[SerializeField]
	private float boomerangSpeedInher = 0.75f; // How much of the player's speed is applied to the boomerang initially

	private Coroutine resetBoomerang; // The stored coroutine call that can be cancelled later when the boomerang is caught


	[Header("Dash")]
	[SerializeField]
	private float dashCooldown = 0.4f; // Time you must wait after using the dash
	[SerializeField]
	private bool canDash = true;
	[SerializeField]
	private float dashDistance = 1.5f;
	[SerializeField]
	private LayerMask dashThrough;
	// Dash check area
	[SerializeField]
	private float dashCheckHeight = 2.56f; // Height up to check if dash is valid
	[SerializeField]
	private float dashCheckLength = 0.5f; // Length forwards and backwards to check if dash is valid
	// Dash positioning
	[SerializeField]
	private float dashStepX = 1.1f; // If a dash fails, it is tried again up to this far backwards
	private int dashAttemptsX = 4; // If a dash fails, it is tried again this many times at different distances. Must be at least 1
	[SerializeField]
	private float dashStepY = 1.5f; // If a dash fails, it is tried again up to this high up or down
	private int dashAttemptsY = 5; // If a dash fails, it is tried again this many times at different heights. Must be at least 2, but ideally an odd number thats greater
	private float dashBufferY = 0.15f; // Additional buffer added to make the player doesn't clip at all

	[Header("TEST")]
	[SerializeField]
	private GameObject indicator; // TEST



	private Rigidbody2D rigid;
	private Player_Controller control;
	private Player_Stats stats; // How much mana, HP, etc.

	// Use this for initialization
	void Start ()
	{
		rigid = GetComponent<Rigidbody2D>();
		control = GetComponent<Player_Controller>();
		stats = GetComponent<Player_Stats>();

		swordTrigger.SetDmg(swordDmg);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonUp("Sword"))
		{
			UseSword();
	
		}

		if (Input.GetButtonUp("Boomerang"))
		{
			UseBoomerang();
		}

		if (Input.GetButtonDown("Dash"))
		{
			UseDash();
		}
	}


	// SWORD //

	void UseSword()
	{
		if (!canSword)
			return;

		////Debug.Log("Swoosh!");
		canSword = false;
		StartCoroutine(ResetSword());

		swordTrigger.Enable();
		StartCoroutine(DisableSword());
	}

	// Resets sword after a cooldown period
	IEnumerator ResetSword()
	{
		yield return new WaitForSeconds(swordCooldown);

		////Debug.Log("Sword ready!");
		canSword = true;
	}

	// Disables sword trigger after a period
	IEnumerator DisableSword()
	{
		yield return new WaitForSeconds(swordLife);

		swordTrigger.Disable();
	}

	// BOOMERANG //

	void UseBoomerang()
	{
		if (!canBoomerang)
			return;

		////Debug.Log("Boomerang noise!");

		// Spawn boomerang
		GameObject newBoomerang = Instantiate(boomerangPrefab, boomerangSpawn.position, Quaternion.identity);

		Projectile_Boomerang newBoom = newBoomerang.GetComponent<Projectile_Boomerang>();
		int dir = (int)Mathf.Sign(transform.localScale.x);
		newBoom.SetDir(dir);

		// If the player is moving we want to add a bit of initial velocity
		////newBoom.SetAddedVel(new Vector2(rigid.velocity.x * boomerangSpeedInher, 0));
		if (control.IsMoving())
			newBoom.SetAddedVel(new Vector2(dir * control.GetMaxSpeed() * boomerangSpeedInher, 0));


		resetBoomerang = StartCoroutine(ResetBoomerang());
		canBoomerang = false;
	}

	// Resets boomerang after a cooldown period
	IEnumerator ResetBoomerang()
	{
		yield return new WaitForSeconds(boomerangCooldown);

		////Debug.Log("Boomerang ready!");
		canBoomerang = true;
	}

	// Called when a boomerang hits the player
	public void CatchBoomerang()
	{
		////Debug.Log("Gotta catch em all!");
		if (resetBoomerang != null)
			StopCoroutine(resetBoomerang);
		canBoomerang = true;
	}

	// DASH //

	void UseDash()
	{
		if (!canDash)
			return;
		
		// If we have enough mana...
		if (stats.CheckMana())
		{
			if (DashPossible())
			{
				int dir = (int)Mathf.Sign(transform.localScale.x);
				// Start dash checking
				if (DashRecurse(dashDistance, dir))
				{
					stats.UseMana();

					canDash = false;
					StartCoroutine(ResetDash());
				}
				else
				{
					
				}
			}
		} //UseMana()
	}

	bool DashPossible()
	{
		Vector2 dir = new Vector2((int)Mathf.Sign(transform.localScale.x), 0);

		Vector2 loc = new Vector2(transform.position.x, transform.position.y + dashCheckHeight / 2);

		RaycastHit2D[] hit = Physics2D.RaycastAll(loc, dir, dashDistance, ~dashThrough);

		// 1 because of player collider
		if (hit.Length > 1)
		{
			Debug.Log(hit.Length);
			return false;
		}
		else
			return true;
	}

	bool DashRecurse(float distance, int direction)
	{
		if (distance <= dashDistance - dashStepX)
			return false;

		Vector2 newPosition = new Vector2(transform.position.x + distance * direction, transform.position.y + dashBufferY);

		Vector2 posA = new Vector2(newPosition.x - dashCheckLength, newPosition.y);
		Vector2 posB = new Vector2(newPosition.x + dashCheckLength, newPosition.y + dashCheckHeight);

		for (int i = 0; i < dashAttemptsY; i++)
		{
			Vector2 newPosA = posA + new Vector2(0, -dashStepY + 2 * i * (dashStepY / dashAttemptsY));
			Vector2 newPosB = posB + new Vector2(0, -dashStepY + 2 * i * (dashStepY / dashAttemptsY));
			// Try a range of y values
			if (Physics2D.OverlapAreaAll(newPosA, newPosB).Length == 0)
			{
				transform.position = new Vector2(newPosition.x, newPosition.y + dashBufferY);
				return true;
			}

			
		} //for

		// If we got this far, this x value didn't work
		return DashRecurse(distance - dashStepX / dashAttemptsX, direction);
	}

	// Resets dash after a cooldown period
	IEnumerator ResetDash()
	{
		yield return new WaitForSeconds(dashCooldown);
		canDash = true;
	}
}

