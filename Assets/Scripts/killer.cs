using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killer : MonoBehaviour {
	public BlockManager bm;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		blockScript block = other.GetComponent<blockScript> ();
		if (block != null) {
			bm.blocks.Remove (block);
			Destroy (block.gameObject);
			Destroy (block.GetComponent<Rigidbody2D>());
			Destroy (block.GetComponent<SpriteRenderer>());
		}
	}
}
