using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour {
	Vector2 p;
	// Use this for initialization
	void Start () {
		p = transform.position;
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (p.x, p.y + 0.075f * Time.time);
	}

	void OnTriggerEnter2D (Collider2D other){
		blockScript block = other.GetComponent<blockScript> ();
		if (block != null) {
			other.GetComponent<SpriteRenderer> ().color = Color.black;
			other.GetComponent<Rigidbody2D> ().gravityScale = -0.1f;
			other.GetComponent<Rigidbody2D> ().drag = 0.99f;
		}
	}

}
