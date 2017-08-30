using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animControl : MonoBehaviour {
	public Animator playerAnim;
	private bool hasJumped = false;
	private float timeAtJump = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setMove(float f){
		playerAnim.SetFloat("horizontal",f);
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

	public void setBool(bool b){
		playerAnim.SetBool ("grab", b);
	}

	public void setRunSpeed(float s){
		playerAnim.SetFloat ("runSpeed", s);
	}

	public void flip(){
		Vector3 temp = transform.localScale;
		//transform.lossyScale.Set (temp.x * -1, temp.y, temp.z);
		transform.localScale = new Vector3 (temp.x * -1, temp.y, temp.z);
	}
}
