using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockScript : MonoBehaviour {
    public bool settled = false;
	public Rigidbody2D body;
	public SpriteRenderer sprite;
	// Use this for initialization

	public PhysicsMaterial2D iron;
	public PhysicsMaterial2D ice;
	public PhysicsMaterial2D cloud;
	public PhysicsMaterial2D slime;
	public PhysicsMaterial2D coal;



	public float getYVelo(){
		return body.velocity.y;
	}

	public void setBlockType(){
		float scaleMult = transform.localScale.x * transform.localScale.y;
		int type = Random.Range (0, 4);
		switch (type) {
		case 0: 
			this.name = "Iron Block";
			sprite.color = Color.gray;
			body.mass = 1 * scaleMult;
			body.sharedMaterial = iron;
			break;
		case 1:
			this.name = "Ice Block";
			sprite.color = Color.blue;
			body.mass = 0.75f * scaleMult;
			body.drag = 0f;
			body.sharedMaterial = ice;
			break;
		case 2:
			this.name = "Cloud Block";
			sprite.color = Color.white;
			body.mass = 0.2f * scaleMult;
			body.gravityScale = 0.01f;
			//body.freezeRotation = false;
			body.sharedMaterial = cloud;
			break;
		case 3:
			this.name = "Slime Block";
			sprite.color = Color.green;
			body.mass = 0.5f * scaleMult;
			body.drag = 0.2f;
			body.sharedMaterial = slime;
			break;
		case 4:
			this.name = "Coal Block";
			sprite.color = Color.black;
			body.mass = 1f * scaleMult;
			body.drag = 0.2f;
			body.sharedMaterial = coal;
			break;
		default:
			this.name = "Default";
			sprite.color = Color.white;
			body.mass = 0.2f * scaleMult;
			body.gravityScale = 0.05f;
			body.freezeRotation = false;
			body.sharedMaterial = cloud;
			break;
		}
	}
}
