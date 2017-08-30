using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockZone : MonoBehaviour {
	public Transform bodyPos;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2(0,bodyPos.position.y + 0.6f);
	}
}
