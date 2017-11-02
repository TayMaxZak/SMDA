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
	private float maxHealth = 6f; // Maximum health the player can have. 3 hearts, 2 halves each
	[SerializeField]
	private float curHealth; // Actual current health

	[Header("Mana")]
	[SerializeField]
	private float maxMana = 12f; // Maximum mana the player can have. 4 bar segments, 3 points per bar
	[SerializeField]
	private float curMana; // Actual current mana

	public static float MANA_SEGMENT = 3f;
	private static float MARGIN = 0.0001f;

	[SerializeField]
	private Player_Interface ui;

	// Use this for initialization
	void Start ()
	{
		////curHealth = maxHealth;
		// Temp code
		curHealth = maxHealth - 4;
		// Mana should not start full
		//curMana = maxMana;

		UpdateUI();
	}
	
	// Update is called once per frame
	void UpdateUI()
	{
		ui.UpdateHealth(curHealth);
		ui.UpdateMana(curMana);
		Debug.Log("updated!");
	}

	public void TakeDamage(float dmg)
	{
		curHealth -= dmg;
		// TODO: DYING

		UpdateUI();
	}

	// Returns false if failed to add health
	public bool AddHealth(float howMuch)
	{
		float newHealth = curHealth + howMuch;
		if (newHealth >= maxHealth + MARGIN)
		{
			return false;
		}
		else
		{
			////Debug.Log("newHealth = " + newHealth + ", maxHealth + MARGIN = " + (maxHealth + MARGIN));
			curHealth = newHealth;

			UpdateUI();
			return true;
		}
	}

	// MANA //

	// Returns false if failed to add mana
	public bool AddMana(float howMuch)
	{
		float newMana = curMana + howMuch;
		if (newMana >= maxMana + MARGIN)
			return false;
		else
		{
			curMana = newMana;

			UpdateUI();
			return true;
		}
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

			UpdateUI();
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
		if (newMana >= -MARGIN)
		{
			return true;
		}
		else
			return false;
	}


}
