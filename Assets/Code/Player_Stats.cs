using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damageable
{
	void TakeDamage(float dmg); // Subtracts dmg from the object on which this is called
}

public class Player_Stats : MonoBehaviour, Damageable
{
	[Header("Health")]
	[SerializeField]
	private float maxHealth = 3f; // Maximum health the player can have
	[SerializeField]
	private float curHealth; // Actual current health

	[Header("Mana")]
	[SerializeField]
	private float maxMana = 12f; // Maximum mana the player can have. 4 bar segments, 3 points per bar
	[SerializeField]
	private float curMana; // Actual current mana

	public static float MANA_SEGMENT = 3f;

	// Use this for initialization
	void Start ()
	{
		curHealth = maxHealth;
		curMana = maxMana;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void TakeDamage(float dmg)
	{
		curHealth -= dmg;
	}

	public bool UseMana()
	{
		return UseMana(MANA_SEGMENT);
	}

	public bool UseMana(float howMuch)
	{
		bool hasEnough = CheckMana(howMuch);
		if (hasEnough)
		{
			curMana = curMana - howMuch;
		}
		return hasEnough;
	}

	public bool CheckMana()
	{
		return CheckMana(MANA_SEGMENT);
	}

	public bool CheckMana(float howMuch)
	{
		float newMana = curMana - howMuch;
		if (newMana >= -0.01)
		{
			return true;
		}
		else
			return false;
	}
}
