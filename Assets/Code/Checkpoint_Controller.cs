using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Controller : MonoBehaviour {

	public Sprite Apple; 
	public SpriteRenderer checkpointSpriteRenderer;
	public bool checkpointReached;

	// Use this for initialization
	void Start () {
		checkpointSpriteRenderer = GetComponent<SpriteRenderer>;
	}
	
	// Update is called once per frame
	void Update(){

	}
	void OntriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			checkpointReached = Apple; 
			checkpointReached = true;
		}
	}
}