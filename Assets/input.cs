using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class input : MonoBehaviour {
	public playerScript player;
    float jumpActivationTolerance = .3f; //Pressing +/- Y from horzonital to cause jump
    float xDifferenceActivation = .1f; //pressing less than that x away wont trigger movement to sides

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log (Input.GetAxis ("Horizontal"));
        //Debug.Log (Input.GetAxis ("Jump"));

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pressedLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 pressedLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (pressedLoc.y - transform.position.y > jumpActivationTolerance)
            {
                player.jump();
            }
            if (Mathf.Abs(pressedLoc.x - transform.position.x) > xDifferenceActivation)
            {
                player.run(Mathf.Sign(pressedLoc.x - transform.position.x));
            }
        }

		if(Input.GetAxis ("Horizontal") != 0)
		player.run(Input.GetAxis ("Horizontal"));
		
		if (Input.GetAxis ("Jump") > 0)
			player.jump ();
	}
}
