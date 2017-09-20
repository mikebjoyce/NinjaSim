using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animControl : MonoBehaviour {
	public Animator playerAnim;
	private bool hasJumped = false;
	private float timeAtJump = 0;
	private float timeAtGrounded = 0;
	private bool isGrounded = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate(){
		
	}

	public void setMove(float f){
		playerAnim.SetFloat("horizontal",Mathf.Abs(f));
	}
		

	public void setJump(){
		if (!hasJumped) {
			playerAnim.SetTrigger ("jump");
			hasJumped = true;
			timeAtJump = Time.time;
		}
		else {
			if (timeAtJump + 1 < Time.time) {
				hasJumped = false;
				setJump ();
			}
		}
	}

	public void setPunchAxis(Vector2 dir){
		playerAnim.SetTrigger ("punch");
		playerAnim.SetFloat ("punchX", Mathf.Abs(dir.normalized.x));
		playerAnim.SetFloat ("punchY", dir.normalized.y);
	}

	public void setJumpAxis(Vector2 dir){
		//Debug.Log ("JumpAxis set with: " + dir);
		playerAnim.SetFloat ("jumpX", Mathf.Abs(dir.normalized.x));
		playerAnim.SetFloat ("jumpY", dir.normalized.y);
	}

	public void setGrab(bool b){
		playerAnim.SetBool ("grab", b);
	}

	public void setRunSpeed(float s){
		playerAnim.SetFloat ("runSpeed", Mathf.Abs(s));
	}

	public void flip(){
		Vector3 temp = transform.localScale;
		//transform.lossyScale.Set (temp.x * -1, temp.y, temp.z);
		transform.localScale = new Vector3 (temp.x * -1, temp.y, temp.z);

	}

	public void setGrounded(bool b){
		if (!isGrounded) {
			if (timeAtGrounded + 0.5f < Time.time) {
				isGrounded = false;
				playerAnim.SetBool ("isGrounded", b);
			}
		}else{
			playerAnim.SetBool ("isGrounded", b);
			isGrounded = true;
			timeAtGrounded = Time.time;
		}
	}
}
