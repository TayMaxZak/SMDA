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

	[Header("Dash")]
	[SerializeField]
	private float dashCooldown = 0.4f; // Time you must wait after using the sword
	[SerializeField]
	private bool canDash = true;

	private Coroutine resetBoomerang; // The stored coroutine call that can be cancelled later when the boomerang is caught

	// Use this for initialization
	void Start ()
	{
		
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

		Debug.Log("Boomerang noise!");

		resetBoomerang = StartCoroutine(ResetBoomerang());
		canBoomerang = false;
	}

	// Resets boomerang after a cooldown period
	IEnumerator ResetBoomerang()
	{
		yield return new WaitForSeconds(boomerangCooldown);

		Debug.Log("Boomerang ready!");
		canBoomerang = true;
	}

	// Called when a boomerang hits the player
	public void CatchBoomerang()
	{
		Debug.Log("Gotta catch em all!");
		if (resetBoomerang != null)
			StopCoroutine(resetBoomerang);
		canBoomerang = true;
	}
}

