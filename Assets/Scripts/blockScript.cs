using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockScript : MonoBehaviour {

    public bool settled = false;
    public Rigidbody2D body;
	// Use this for initialization
	
	public float getYVelo(){
		return body.velocity.y;
	}
}
