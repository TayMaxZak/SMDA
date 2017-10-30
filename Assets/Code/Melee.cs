using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
	[SerializeField]
	private float dmg;
	[SerializeField]
	private List<Damageable> targets;
	[SerializeField]
	private AudioClip swingAudio;
	[SerializeField]
	private AudioClip cutAudio; // Played when dealing damage
	[SerializeField]
	private AudioSource swingSource;

	void Start()
	{
		targets = new List<Damageable>();
	}

	public void Enable()
	{
		AudioUtils.PlayClipAt(swingAudio, transform.position, swingSource);
		gameObject.SetActive(true);
	}

	public void Disable()
	{
		targets.Clear();
		gameObject.SetActive(false);
	}
	
	public void SetDmg(float dmg)
	{
		this.dmg = dmg;
	}

	protected virtual void OnTriggerStay2D(Collider2D other)
	{

		// Prevents any interaction with other projectiles
		Damageable target = other.gameObject.GetComponent<Damageable>();
		if (target == null)
			return;

		if (targets.Contains(target))
			return;

		if ((Player_Stats)target != null)
			return;

		target.TakeDamage(dmg);
		targets.Add(target);
		AudioUtils.PlayClipAt(cutAudio, transform.position, swingSource);
	}
}
