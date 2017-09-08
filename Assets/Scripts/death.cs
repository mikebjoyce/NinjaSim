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
	void Update ()
    {
        transform.position = new Vector3 (p.x, p.y + 0.033f * Time.time);
	}

	void OnTriggerEnter2D (Collider2D other){
        if (other.CompareTag("Block"))
        { 
            Rigidbody2D body = other.GetComponent<Rigidbody2D> ();
			other.GetComponent<SpriteRenderer> ().color = Color.black;
			body.gravityScale = -0.2f;
			body.drag = 1f;
			body.freezeRotation = false;
			body.mass *= 1f;
			body.sharedMaterial = Resources.Load<PhysicsMaterial2D> ("Prefabs/Coal");
		}
	}

}
