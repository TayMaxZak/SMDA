using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Movement : MonoBehaviour {
	public float enemySpeed;

	Animator enemyAnimator;

	public GameObject enemyGraphics;
	bool canflip = true;
	bool facingRight = false;
	float flipTime = 5f;
	float nextFlipChance = 0f;
	// Attacking 
	public float chargeTime;
	public float startChargeTime;
	bool charging;
	Rigidbody2D enemyRB;

	// Use this for initialization
	void Start () {
		enemyAnimator = GetComponentInChildren<Animator> ();
		enemyRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextFlipChance){
			if (Random.Range (0, 10) >= 5)
				flipFacing ();
				nextFlipChance = Time.time + flipTime;
			}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			if (facingRight && other.transform.position.x < transform.position.x) {
				flipFacing ();
			} else if( facingRight && other.transform.position.x > transform.position.x){
				flipFacing();
				}
				canflip = false;
				charging = true;
				startChargeTime = Time.time + chargeTime;
				}
	}
	void OnTriggerStay2D(Collider2D other){
			if(other.tag == "Player"){
				if(startChargeTime>= Time.time){
				 enemyRB.AddForce(new Vector2(1,0)*enemySpeed);
				enemyAnimator.SetBool("isCharging", charging);
					}
				}
			}
		void OnTriggerExist2D(Collider2D other){
					if(other.tag == "Player"){
			canflip = true;
				charging = false;
			enemyRB.velocity = new Vector2(0f,0f);
						}
								}	
		void flipFacing(){
			if(!canflip) return;
				float facingX = enemyGraphics.transform.localScale.x;
			facingX *= -1f;
		enemyGraphics.transform.localScale = new Vector3(facingX, enemyGraphics.transform.localScale.y, enemyGraphics.transform.localScale.x);
			facingRight = !facingRight;
									
			}
	}
	



	