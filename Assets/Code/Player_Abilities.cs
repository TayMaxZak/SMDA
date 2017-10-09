using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Abilities : MonoBehaviour
{
	[SerializeField]
	private float boomerangCooldown = 9; // Time you must wait after using the boomerang
	[SerializeField]
	private bool canBoomerang = true;

	[SerializeField]
	private float swordCooldown = 0.6f; // Time you must wait after using the sword
	[SerializeField]
	private bool canSword = true;

	private Coroutine resetBoomerang;

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

