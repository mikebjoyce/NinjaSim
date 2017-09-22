using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour {
	public Collider2D spawnArea;
	List<GameObject> blocks = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool isClearToSpawn(){
		return blocks.Count == 0;	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Block")) {
			blocks.Add (other.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.CompareTag ("Block")) {
			blocks.Remove (other.gameObject);
		}
	}
}
