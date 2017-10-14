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
	private Transform swordSpawn; // Where the sword object will be placed
	[SerializeField]
	private GameObject swordrefab; // The actual sword object

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
	// TODO: FIX AWKWARD BOOMERANG THROWS AS PLAYER IS STARTING TO SPEED UP

	[Header("Dash")]
	[SerializeField]
	private float dashCooldown = 0.4f; // Time you must wait after using the sword
	[SerializeField]
	private bool canDash = true;

	private Coroutine resetBoomerang; // The stored coroutine call that can be cancelled later when the boomerang is caught

	private Rigidbody2D rigid;
	private Player_Controller control;

	// Use this for initialization
	void Start ()
	{
		rigid = GetComponent<Rigidbody2D>();
		control = GetComponent<Player_Controller>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown("Sword"))
		{
			UseSword();
	
		}

		if (Input.GetButtonDown("Boomerang"))
		{
			UseBoomerang();
		}

		// Test code, make sure this never makes it to the final game!
		/**/
		if (Input.GetButtonDown("Fire3"))
		{
			CatchBoomerang();
		}
		/**/
	}

	void UseSword()
	{
		if (!canSword)
			return;

		Debug.Log("Swoosh!");

		StartCoroutine(ResetSword());
		canSword = false;
	}

	// Resets sword after a cooldown period
	IEnumerator ResetSword()
	{
		yield return new WaitForSeconds(swordCooldown);

		Debug.Log("Sword ready!");
		canSword = true;
	}

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
}

