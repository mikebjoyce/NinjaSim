using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager  {

    float blockFallRate = 3;
    float blockCurrentCounter = 3;
    Vector2 blockSpawnScale = new Vector2(.1f, 3);
    List<blockScript> blocks = new List<blockScript>();
    float spawnHeight = 8; //How high above the highest block a block is spawned

    Vector2 xLimits;

    public BlockManager(Vector2 leftWall, Vector2 rightWall)
    {
        xLimits = new Vector2(leftWall.x, rightWall.x);
    }

    public void Update(float dt)
    {
        blockCurrentCounter -= Time.deltaTime;
        Debug.Log("counter: " + blockCurrentCounter);
        if (blockCurrentCounter <= 0)
        {
            Debug.Log("ici");
            blockCurrentCounter = blockFallRate;
            GameObject newBlock = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Block")) as GameObject;
            Vector2 blockScale = new Vector2(Random.Range(blockSpawnScale.x, blockSpawnScale.y), Random.Range(blockSpawnScale.x, blockSpawnScale.y));
            Vector2 spawnSpot = GetValidSpawnSpot(blockScale.x);
            newBlock.transform.position = spawnSpot;
            newBlock.transform.localScale = blockScale;
        }
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
        return y;
    }
}
