﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

	//Colliders
	public Collider2D groundCheck;
	public Collider2D grabCheck;
	public Collider2D grabCheckFlip;
	public Collider2D crushCheck;

	//Unity Objects
	public Rigidbody2D body;
	public animControl animCnt;

	//Jump Limiters
	private float grabJump = 0;
	private float grabJumpDelay = 0.75f;
	private float groundJump = 0;
	private float groundJumpDelay = 0.2f;
	private float punchTime = 0;
	private float punchTimeDelay = 2.5f;

	//internal Vars
	private float facingDir = -1;

	//gameplay vars
	private float horizontalJumpRatio = 0.5f;
	private float verticalJumpRatio = 1;
	private float jumpForce = 375;
	private float runForce = 450;
	private float flyForce = 350;
	private float punchForce = 150;

	public float healthPoints = 100f;
	private bool dmgPerTick = false;
	public Color origionalColor;

	// Use this for initialization
	void Start () {
		Color playerColor = new Color (Random.Range (0, 1f), Random.Range (0, 1f), Random.Range (0, 1f));
		foreach (SpriteRenderer sR in GetComponentsInChildren<SpriteRenderer>()) {
			sR.color = playerColor;
		}
	}

	// Update is called once per frameanimCnt
	void Update () {
		if (isGrabbing () && !isGrounded() && body.velocity.y < 0.5f) { //the slowing effect of grabbing
			body.AddForce (new Vector2 (0, 450) * Time.deltaTime);
			animCnt.setGrab (true);
		}else if (isGrabbing () && !isGrounded() && body.velocity.y < -0.5f) { //the slowing effect of grabbing
			body.AddForce (new Vector2 (0, 1000) * Time.deltaTime);
			animCnt.setGrab (true);
		}
		else if(isBackGrabbing() && !isGrounded() && body.velocity.y < 0 && punchTime + (punchTimeDelay/2) <= Time.time){
			flip ();
		}
		else{
			animCnt.setGrab(false);
		}
		animCnt.setRunSpeed (runSpeed ());
		if (Mathf.Abs (body.velocity.x) > 0.1 && isGrounded ())
			animCnt.setMove (1);
		else
			animCnt.setMove (0);
//		Debug.Log (Time.time + " grab: " + grabJump + " reg: " + groundJump);
	}

	void LateUpdate(){
		animCnt.setGrounded (isGrounded());
		dmgPerTick = false;
	}


	public void move(Vector2 dir){
		float direction = dir.x;
		if (isGrounded ()) {
			float d = Mathf.Sign(dir.x);
			if (d != facingDir && d != 0)
				flip ();
			body.AddForce (new Vector2 (runForce, 0) * Time.deltaTime * facingDir);
			//Debug.Log ("here");
			animCnt.setMove (direction);
		} else if (isGrabbing ()) {
			if (grabJump + grabJumpDelay < Time.time) {
				jump (dir);
				grabJump = Time.time;
			}
		} else {
			body.AddForce (new Vector2 (flyForce*dir.normalized.x, flyForce * dir.normalized.y * .2f) * Time.deltaTime);
		}
	}

	public void punch(Vector2 dir){
		if (punchTime + punchTimeDelay <= Time.time) {
			punchTime = Time.time;
			float d = Mathf.Sign(dir.x);
			Debug.Log ("d"  + d + (d != facingDir && d != 0));
			if (d != facingDir && d != 0)
				flip ();
			body.AddForce (new Vector2 (punchForce * dir.x, punchForce * dir.y));
			animCnt.setPunchAxis (dir);
		}
	}

	/* 
	public void jump(){
		if (isGrounded ()) {
			body.AddForce (new Vector2 (0, 100));
			animCnt.setJump ();
		}else if(isGrabbing()){
			if (grabJump + grabJumpDelay < Time.time) {
				body.AddForce (new Vector2 (facingDir * -1 * 20, 100));
				animCnt.setJump ();
			}
		}
	} */

	public void jump(Vector2 dir){
		/* float d = 0;
		if (dir.x < 0)
			d = -1;
		else
			d = 1;
		if (d != facingDir && d != 0)
			flip ();
			 */
		Vector2 normalDir = dir.normalized;
		if (isGrounded ()) {
			if (groundJump + groundJumpDelay < Time.time) {
				body.AddForce (new Vector2 (normalDir.x * jumpForce * horizontalJumpRatio, normalDir.y * jumpForce * verticalJumpRatio));
				animCnt.setJump ();
				animCnt.setJumpAxis (normalDir);
				groundJump = grabJump = Time.time;
			}
		} else if (isGrabbing ()) {
			if (grabJump + grabJumpDelay < Time.time) {
				float xForce = 0;
				float yForce = 0.2f;
				if (normalDir.y <= 0.95)
					yForce = 1;
				if (!isForwardDir (dir.x)) {
					xForce = 1;
				} else {
					yForce = 0.2f;
				}
				body.AddForce (new Vector2 (xForce * normalDir.x * jumpForce * horizontalJumpRatio, normalDir.y * jumpForce * verticalJumpRatio * yForce));
				animCnt.setJump ();
				animCnt.setJumpAxis (new Vector2(normalDir.x * xForce, normalDir.y));
				grabJump = Time.time;
				//Debug.Log ("grab @ " + grabJump);
			}
		}
	}

	public void flip(){
		//Debug.Log ("flip " + facingDir);
		facingDir *= -1;
		animCnt.flip ();
		//animCnt.setGrab (false);
		Debug.Log("flipped");
	}

	public bool isForwardDir(float x){
		if (x > 0)
			return facingDir == 1;
		else
			return facingDir == -1;
	}

	public void jumpFlip(){

	}

	public void die(){
		Debug.Log ("Herererreererre!!");
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

	public bool isBackGrabbing(){
		bool output = false;
		if(facingDir == 1)
			output = grabCheck.IsTouchingLayers (LayerMask.GetMask("Terrain"));
		else
			output = grabCheckFlip.IsTouchingLayers (LayerMask.GetMask("Terrain"));
		return output;
	}

	public float runSpeed(){
		float temp = Mathf.Abs(body.velocity.x / 4);
		if (temp < 1)
			return 1;
		return temp;
	}

	/*public bool isCrushed(){
		return crushCheck.IsTouchingLayers (0);
	}*/

	/*void OnTriggerEnter2D (Collider2D other){ //checks for being crushed
		//if(crushCheck.IsTouching(other) && other.attachedRigidbody == null)
		//	die();
	}*/

	public void loseHealth(float dmg){
		if (!dmgPerTick) {
			dmgPerTick = true;
			healthPoints -= dmg;
		}
	}
}
