using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tiles;
    public float zSpawn = 0;
    public float tileLength = 30;
    public int numberOfTiles = 5;
    private List<GameObject> activeTiles = new List<GameObject>();
    public static int destroyCount = 0;
    public int oldTileIndex = 0;

    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);//straight road generation at the beginning
            }
            SpawnTile(Random.Range(0, tiles.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z > zSpawn - (numberOfTiles * tileLength)) 
        {
            SpawnTile(Random.Range(0, tiles.Length));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        if (tileIndex > 3 && oldTileIndex > 3)
        {
            SpawnTile(Random.Range(0, tiles.Length));
        }

        else
        {
            GameObject go = Instantiate(tiles[tileIndex], transform.forward * zSpawn, transform.rotation);
            activeTiles.Add(go);
            zSpawn += tileLength;
            oldTileIndex = tileIndex;
        }
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
        destroyCount++;
    }

}
