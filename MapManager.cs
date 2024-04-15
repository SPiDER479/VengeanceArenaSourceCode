using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tiles;
    void Start()
    {
        for (int x = -38; x <= 38; x += 19)
            for (int y = -20; y <= 20; y += 10)
                spawnTile(x, y, Random.Range(0, tiles.Length));
    }
    private void spawnTile(int x, int y, int i)
    {
        Instantiate(tiles[i], new Vector3(x, y, 0), Quaternion.identity, transform);
    }
}
