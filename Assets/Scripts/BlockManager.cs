using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager  {

    float blockFallRate = 2f;
    float blockFallSpeedIncreasePerSec = .95f;
    float blockCurrentCounter;
    float blockIncreaseRateCounter = 0;
    Vector2 blockSpawnScale = new Vector2(.55f, 2.1f);
    List<blockScript> blocks = new List<blockScript>();
    float spawnHeight = 8; //How high above the highest block a block is spawned


    Vector2 xLimits;

    public BlockManager(Vector2 leftWall, Vector2 rightWall)
    {
        blockCurrentCounter = blockFallRate;
        xLimits = new Vector2(leftWall.x, rightWall.x);
    }

    public void Update(float dt)
    {
        blockCurrentCounter -= Time.deltaTime;
        blockIncreaseRateCounter += Time.deltaTime;
        if(blockIncreaseRateCounter > 1)
        {
            blockIncreaseRateCounter = 0;
            blockFallRate *= blockFallSpeedIncreasePerSec;
        }
       // Debug.Log("counter: " + blockCurrentCounter);
        if (blockCurrentCounter <= 0)
        {
            blockCurrentCounter = blockFallRate;
            GameObject newBlock = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Block")) as GameObject;
            Vector2 blockScale = GetValidScale();
            newBlock.GetComponent<Rigidbody2D>().mass = blockScale.x * blockScale.y; 
            Vector2 spawnSpot = GetValidSpawnSpot(blockScale.x);
            newBlock.transform.position = spawnSpot;
            newBlock.transform.localScale = blockScale;
            blocks.Add(newBlock.GetComponent<blockScript>());
			//newBlock.GetComponent<SpriteRenderer> ().color = new Color (Random.Range (0, 1f), Random.Range (0, 1f), Random.Range (0, 1f));
			newBlock.GetComponent<blockScript>().setBlockType();
        }
    }

    private Vector2 GetValidScale()
    {
        float howRectangly = .2f;
        float size = Random.Range(blockSpawnScale.x, blockSpawnScale.y);
        float xScale = size * Random.Range(1 - howRectangly, 1 + howRectangly);
        float yScale = size * Random.Range(1 - howRectangly, 1 + howRectangly);
        return new Vector2(xScale,yScale);
    }

    private Vector2 GetValidSpawnSpot(float xScale)
    {
        Vector2 spawnLoc = new Vector2();
        spawnLoc.y = HighestYBlock() + spawnHeight;
        spawnLoc.x = Random.Range(xLimits.x + xScale, xLimits.y - xScale);
        return spawnLoc;
    }

    private float HighestYBlock()
    {
        float y = 0;
        foreach (blockScript b in blocks)
        {
            if(b.settled)
                y = Mathf.Max(y, b.transform.position.y);
        }
        Debug.Log("Highest settled block: " + y);
        return y;
    }
}
