using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player_Interface : MonoBehaviour
{
	[SerializeField]
	private Text health;
	[SerializeField]
	private Text mana;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	public void UpdateHealth(float value)
	{
		health.text = "HEALTH - " + (int)value;
	}

	public void UpdateMana(float value)
	{
		mana.text = "MANA - " + (int)value;
	}
}
