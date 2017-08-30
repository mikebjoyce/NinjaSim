using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {
	public Collider2D groundCheck;
	public Collider2D grabCheck;
	public Collider2D grabCheckFlip;
	public Collider2D crushCheck;

	public Rigidbody2D body;

	public animControl animCnt;

	public float facingDir = -1;

	private bool grounded = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frameanimCnt
	void Update () {
		if (isGrabbing () && !isGrounded() && body.velocity.y < 0) { //the slowing effect of grabbing
			body.AddForce (new Vector2 (0, 450) * Time.deltaTime);
			animCnt.setGrab(true);
		}else{
			animCnt.setGrab(false);
		}
		animCnt.setRunSpeed (runSpeed ());
	}

	void LateUpdate(){
		animCnt.setGrounded (isGrounded());
	}


	public void move(float direction){
		if (isGrounded()) {
			float d = 0;
			if (direction < 0)
				d = -1;
			else
				d = 1;
			Debug.Log (d + " " + facingDir);
			if (d != facingDir)
				flip ();
			body.AddForce (new Vector2 (1200, 0) * Time.deltaTime * facingDir);
			Debug.Log ("here");
			animCnt.setMove (direction);
		}
	}

	public void jump(){
		Debug.Log (isGrounded ());
		if (isGrounded ()) {
			body.AddForce (new Vector2 (0, 100));
			animCnt.setJump ();
		}else if(isGrabbing()){
			body.AddForce (new Vector2 (facingDir * -1 * 20, 100));
			animCnt.setJump ();
		}
	}

	public void flip(){
		Debug.Log ("flip " + facingDir);
		facingDir *= -1;
		animCnt.flip ();
		animCnt.setGrab (false);
	}

	public void die(){

	}
			

	public bool isGrounded(){
		//Debug.Log (LayerMask.LayerToName (8));
		//Debug.Log(groundCheck.IsTouchingLayers (LayerMask.GetMask("Terrain")));
		return groundCheck.IsTouchingLayers (LayerMask.GetMask("Terrain"));
	}

	public bool isGrabbing(){
		bool output = false;
		if(facingDir == -1)
			output = grabCheck.IsTouchingLayers (LayerMask.GetMask("Terrain"));
		else
			output = grabCheckFlip.IsTouchingLayers (LayerMask.GetMask("Terrain"));
		return output;
	}

	public float runSpeed(){
		float temp = Mathf.Abs(body.velocity.x / 15);
		if (temp < 1)
			return 1;
		return temp;
	}

	/*public bool isCrushed(){
		return crushCheck.IsTouchingLayers (0);
	}*/

	void OnTriggerEnter2D (Collider2D other){ //checks for being crushed
		if(crushCheck.IsTouching(other) && other.attachedRigidbody.velocity.y > 0)
			die();
	}
}
