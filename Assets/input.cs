using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class input : MonoBehaviour {
	public playerScript player;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (Input.GetAxis ("Horizontal"));
		//Debug.Log (Input.GetAxis ("Jump"));

		if(Input.GetAxis ("Horizontal") != 0)
		player.run(Input.GetAxis ("Horizontal"));
		
		if (Input.GetAxis ("Jump") > 0)
			player.jump ();
	}
}
