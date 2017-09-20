using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour {
	Vector2 p;
	float deathForce = 1000f;
	// Use this for initialization
	void Start () {
		p = transform.position;
	}

	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3 (p.x, p.y + 0.033f * Time.time);
	}

	void OnTriggerEnter2D (Collider2D other){
        if (other.CompareTag("Block"))
        { 
            Rigidbody2D body = other.GetComponent<Rigidbody2D> ();
			other.GetComponent<SpriteRenderer> ().color = Color.black;
			body.AddForce (Vector2.up * deathForce);
			body.gravityScale *= -0.5f;
			body.drag = 0.1f;
			body.angularDrag = 0.175f;
			body.freezeRotation = false;
			body.mass *= 4f;
			body.sharedMaterial = Resources.Load<PhysicsMaterial2D> ("Prefabs/Coal");
		}
	}

}
