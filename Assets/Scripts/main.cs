using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour {

    public Transform leftLimit, rightLimit;
    BlockManager blockManager;

	// Use this for initialization
	void Start ()
    {
        blockManager = new BlockManager(leftLimit.position, rightLimit.position);
    }
	
	// Update is called once per frame
	void Update ()
    {
        float dt = Time.deltaTime;
        blockManager.Update(dt);
	}
}
