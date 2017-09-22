using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager  {

    public Transform player;
    public Transform blockSpawner;
    float blockSpawnDistance = 9;                       //The min Y distance the block spawner is from the player
    float blockSummonThreshold = 1;                     //At 1 point, a block is spawned
    float blockFallRate = .3f;                          //How many points are adding every 1 second, once reaches "blockSummonThreshold" it summons a block (so .1f is once every 10 seconds)
    float blockCurrentCounter;                          //Stores the current fall rate score accumalated
    Vector2 blockSpawnScale = new Vector2(.55f, 2.1f);  //Scale the blocks will spawn
    Vector2 xLimits;
    float blockFallRateIncreasePerDistance = .005f;      //For every meter up the spawner is, the blockFallRate is increased by that much
	private SpawnZone spawnZone;

	public BlockManager(Vector2 leftWall, Vector2 rightWall, Transform _player, Transform _blockSpawner, SpawnZone sz)
    {
        blockCurrentCounter = blockFallRate;
        xLimits = new Vector2(leftWall.x, rightWall.x);
        player = _player;
        blockSpawner = _blockSpawner;
		spawnZone = sz;
    }

    public void Update(float dt)
    {
        blockCurrentCounter += Time.deltaTime * (blockFallRate + (blockFallRateIncreasePerDistance*blockSpawner.position.y));
		if(blockSpawner.position.y - player.position.y < blockSpawnDistance)
			blockSpawner.position = new Vector2(blockSpawner.position.x,player.position.y + blockSpawnDistance);
		if(blockCurrentCounter >= blockSummonThreshold && spawnZone.isClearToSpawn())
        {
            Debug.Log("Spawn rate: " + (blockFallRate + (blockFallRateIncreasePerDistance * blockSpawner.position.y)));
            blockCurrentCounter -= blockSummonThreshold;
            GameObject newBlock = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Block")) as GameObject;
            Vector2 blockScale = GetValidScale();
            newBlock.GetComponent<Rigidbody2D>().mass = blockScale.x * blockScale.y; 
            Vector2 spawnSpot = GetValidSpawnSpot(blockScale.x);
            newBlock.transform.position = spawnSpot;
            newBlock.transform.localScale = blockScale;
			//newBlock.GetComponent<SpriteRenderer> ().color = new Color (Random.Range (0, 1f), Random.Range (0, 1f), Random.Range (0, 1f));
			newBlock.GetComponent<blockScript>().setBlockType();
            MonoBehaviour.Destroy(newBlock.GetComponent<blockScript>());
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

	private bool HasValidSpawnSpot(float xScale){
		Vector2 spawnLoc = new Vector2();
		spawnLoc.y = blockSpawner.position.y;
		spawnLoc.x = Random.Range(xLimits.x + xScale, xLimits.y - xScale);
		return false;
	}

    private Vector2 GetValidSpawnSpot(float xScale)
    {
        Vector2 spawnLoc = new Vector2();
        spawnLoc.y = blockSpawner.position.y;
        spawnLoc.x = Random.Range(xLimits.x + xScale, xLimits.y - xScale);
        return spawnLoc;
    }
}
