using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
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
}
