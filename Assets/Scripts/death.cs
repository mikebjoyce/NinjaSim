using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour {
	Vector2 p;
	float deathForce = 500f;
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
			//other.gameObject.AddComponent<
			body.gravityScale *= -0.5f;
			body.drag = 0.1f;
			body.angularDrag = 0.175f;
			body.freezeRotation = false;
			body.mass *= 4f;
			body.sharedMaterial = Resources.Load<PhysicsMaterial2D> ("Prefabs/Coal");
			other.tag = "BlackBlock";
			other.gameObject.AddComponent<blockDecay> ();
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.CompareTag("Block"))
		{ 
			//other.tag = "BlackBlock";
			//other.gameObject.AddComponent<blockDecay> ();
			//Rigidbody2D body = other.GetComponent<Rigidbody2D> ();
			//body.AddForce (Vector2.up * deathForce * Time.deltaTime);
			//double temp = body.mass * (1 - ((1 - 0.95) * Time.deltaTime));
			//body.mass = temp;
			//body.mass *= (float) (1 - ((1 - 0.95) * Time.deltaTime));

		}else if(other.CompareTag("Player")){
			List<SpriteRenderer> sr = new List<SpriteRenderer>(other.GetComponentsInChildren<SpriteRenderer> ());
			playerScript ps = other.GetComponent<playerScript> ();
			if (ps.healthPoints == 100) {
				ps.origionalColor = sr [0].color;
			} else if (ps.healthPoints <= 0) {
				ps.die ();
			}
			ps.loseHealth(10 * Time.deltaTime);
			foreach (SpriteRenderer s in sr) {
				s.color = Color.LerpUnclamped (ps.origionalColor, Color.black, (100 - ps.healthPoints) / 100);
			}
		}
	}

}
