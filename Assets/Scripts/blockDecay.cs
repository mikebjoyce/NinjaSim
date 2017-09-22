using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockDecay : MonoBehaviour {
	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		body.mass *= (float) (1 - ((1 - 0.95) * Time.deltaTime));	
		if (body.mass < 0.2f)
			Destroy (this.gameObject);
	}
}
