﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Block"))
			Destroy (other.gameObject);
	}
}
