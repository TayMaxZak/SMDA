using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
	[Header("Stats")]
	[SerializeField]
	private int type = 0; // 0 = health, 1 = mana
	[SerializeField]
	private float amount = 1; // Amount to add to player's stats

	[Header("Movement")]
	[SerializeField]
	private float outerAttractRange = 4; // Starts to move towards player
	[SerializeField]
	private float innerAttractRange = 2; // Range where movement is maximum
	[SerializeField]
	private float attractSpeed = 3; // Max speed towards player
	[SerializeField]
	private float randomSpeedMin = 0.7f;
	[SerializeField]
	private float randomSpeedMax = 1.3f;
	[SerializeField]
	private float fallSpeed = 0.1f;
	private float randomSpeedMult;

	[Header("Effects")]
	[SerializeField]
	private AudioClip clip;

	private Transform player; // Automatically found. Used to if the player is close enough to attract the orb
	private Vector3 playerPos; 
	private Rigidbody2D rigid;

	private bool broken;

	void Start()
	{
		randomSpeedMult = Random.Range(randomSpeedMin, randomSpeedMax);

		player = GameObject.FindGameObjectWithTag("Player").transform;
		
		//playerPos = new Vector3(playerPos.x, playerPos.y + 1, playerPos.z); // TODO: Make this more consistent. You shoulndnt have to say 1
		rigid = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void FixedUpdate()
	{
		playerPos = player.position;
		playerPos = new Vector3(playerPos.x, playerPos.y + 1, playerPos.z); // TODO: Make this more consistent. You shoulndnt have to say 1
		/*** /
		float sqrDist = Vector2.SqrMagnitude(transform.position - player.position);
		if (sqrDist < outerAttractRange * outerAttractRange)
		{
			float ratio = (sqrDist - (innerAttractRange * innerAttractRange)) / ((outerAttractRange * outerAttractRange) - (innerAttractRange * innerAttractRange));
			Debug.Log("TEEEMO ON DUTY! Ratio is: " + ratio);
		}
		/**/
		/*** /
		float dist = Vector2.Distance(transform.position, playerPos);
		if (dist < outerAttractRange)
		{
			float ratio = (dist - innerAttractRange) / (outerAttractRange - innerAttractRange);
			ratio = Mathf.Clamp01(ratio);
			ratio = 1 - ratio;
			////Debug.Log("TEEEMO ON DUTY! Ratio is: " + ratio);
			transform.right = playerPos - transform.position;
			rigid.velocity = transform.right * attractSpeed * ratio;
		}
		/**/
		float sqrDist = Vector3.SqrMagnitude(transform.position - playerPos);
		////Debug.Log("sqrDist is: " + sqrDist + " and outerAttractRange^2 is: " + outerAttractRange * outerAttractRange);
		if (sqrDist < outerAttractRange * outerAttractRange)
		{
			float ratio = (sqrDist - innerAttractRange * innerAttractRange) / (outerAttractRange * outerAttractRange - innerAttractRange * innerAttractRange);
			ratio = Mathf.Clamp01(ratio);
			ratio = 1 - ratio;
			////Debug.Log("Ratio is: " + ratio);
			transform.right = playerPos - transform.position;
			rigid.velocity = transform.right * attractSpeed * ratio * randomSpeedMult;
		}
		else
			rigid.velocity = new Vector3(0, -fallSpeed, 0);
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Player_Stats stats = other.GetComponent<Player_Stats>();

			// Only do this after a quarter of a second to avoid instantly catching what you just threw
			if (type == 0)
			{
				if (stats.AddHealth(amount))
					Break();

			} else if (type == 1)
			{
				if (stats.AddMana(amount))
					Break();
			}
		} //if player
	}

	void Break()
	{
		if (broken)
			return;
		broken = true;

		AudioUtils.PlayClipAt(clip, transform.position, GetComponent<AudioSource>());
		Destroy(gameObject);
	}
}
