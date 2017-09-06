using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockSettler : MonoBehaviour {

    float alive = 0;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

	void Update ()
    {
        alive += Time.deltaTime;
		if(alive > 2 && rb.velocity.y >= 0)
        {
            GetComponent<blockScript>().settled = true;
            Destroy(this);
        }                    
    }
}
