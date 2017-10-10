using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_movemen : MonoBehaviour {
	public float enemySpeed;

	Animator EnemyAnimator;

	public GameObject EnemyGraphics;
	bool canflip = true;
	bool facingRight = false;
	float FlipTime = 5f;
	float NextFlipChance = 0f;
	// Attacking 
	public float chargeTime;
	public float StartChargeTime;
	bool charging;
	Rigidbody2D enemyRB;

	// Use this for initialization
	void Start () {
		EnemyAnimator = GetComponentInChildren<Animator> ();
		enemyRB = GetComponents<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > NextFlipChance){
			if (Random.Range (0, 10) >= 5)
				flipFacing ();
				NextFlipChance = Time.time + FlipTime;
			}
	}

	void OntriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			if (facingRight && other.transform.position.x < transform.position.x) {
				flipFacing ();
			} else if( facingRight && other.transform.position.x > transform.position.x){
				flipFacing();
				}
				canflip = false;
				charging = true;
				StartChargeTime = Time.time + chargeTime;
				}
	}
	void OntriggerStay2D(Collider2D other){
			if(other.tag == "Player"){
				if(StartChargeTime>= Time.time){
				 enemyRB.AddForce(new Vector2(1,0)*enemySpeed);
				EnemyAnimator.SetBool("isCharging", charging);
					}
				}
			}
		void OntriggerExist2D(Collider2D other){
					if(other.tag == "Player"){
			canflip = true;
				charging = false;
			enemyRB.velocity = new Vector2(0f,0f);
						}
								}	
		void flipFacing(){
			if(!canflip) return;
				float facingX = EnemyGraphics.transform.localScale.x;
			facingX *= -1f;
		EnemyGraphics.transform.localScale = new Vector3(facingX, EnemyGraphics.transform.localScale.y, EnemyGraphics.transform.localScale.x);
			facingRight = !facingRight;
									
			}
							}
	



	