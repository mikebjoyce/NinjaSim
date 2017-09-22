using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour {
	public Camera cam;
	private Color currentColor;
	private Color nextColor;
	private float colorChangeDuration = 3;
	private float lastColorChange = 0;
    public playerScript player;
    public Transform blockSpawner;
    public Transform leftLimit, rightLimit;
    BlockManager blockManager;
	public killer k;
	public SpawnZone spawn;

	// Use this for initialization
	void Start ()
    {
		currentColor = randomColor();
		nextColor = randomColor();
		blockManager = new BlockManager(leftLimit.position, rightLimit.position, player.transform, blockSpawner, spawn);
		cam.clearFlags = CameraClearFlags.SolidColor;
		cam.backgroundColor = currentColor = randomColor ();
		nextColor = randomColor ();
		lastColorChange = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float dt = Time.deltaTime;
        blockManager.Update(dt);
		changeColor ();
		lastColorChange += dt;
		if (lastColorChange > colorChangeDuration) {
			setNextColors ();
			lastColorChange = 0;
		}
	}

	private void setNextColors(){
		currentColor = cam.backgroundColor;
		nextColor = randomColor ();
	}

	private void changeColor(){
		cam.backgroundColor = Color.LerpUnclamped (currentColor, nextColor, Mathf.PingPong(lastColorChange,colorChangeDuration)/colorChangeDuration);
	}

	private Color randomColor(){
		return new Color (Random.Range (0, 1f), Random.Range (0, 1f), Random.Range (0, 1f), 1);
	}
}
