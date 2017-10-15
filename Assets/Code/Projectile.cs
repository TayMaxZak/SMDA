using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField]
	private int dir = 0; // 1 = positive x, -1 = negative x
	[SerializeField]
	private Vector2 vel;
	[SerializeField]
	private Vector2 addedVel;
	[SerializeField]
	private Vector2 accel;
	//[SerializeField]
	private Rigidbody2D rigid;
	[SerializeField]
	protected AudioClip hitAudio;
	[SerializeField]
	protected AudioSource hitSource;
	[SerializeField]
	protected GameObject hitEffect;

	protected bool broken;

	// Use this for initialization
	// Note: this MUST be awake
	void Awake ()
	{
		// Cache rigidbody
		rigid = gameObject.GetComponent<Rigidbody2D>();
		//hitSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		// Update velocity (due to acceleration)
		vel = new Vector2(vel.x + accel.x * Time.fixedDeltaTime, vel.y + accel.y * Time.fixedDeltaTime);


		rigid.velocity = vel;
	}

	public void SetDir(int dir)
	{
		this.dir = dir;
		// Factor direction into acceleration and velocity
		vel = new Vector2(vel.x * dir, vel.y);
		accel = new Vector2(accel.x * dir, accel.y);
	}

	public void SetAddedVel(Vector2 addedVel)
	{
		vel += addedVel;
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		Debug.LogError("YP");
		// Prevents any interaction with other projectiles
		if (other.tag != "Projectile")
		{
			if (!broken)
				AudioUtils.PlayClipAt(hitAudio, transform.position, hitSource);
			broken = true;
			Instantiate(hitEffect, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
