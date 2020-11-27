using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField]
    private GameSettings gameSettings;

    [SerializeField]
    private GameObject prefabToSpawn;

    private float spawnDelay = 0.0f;

    void Start()
    {
        if(prefabToSpawn.tag == "Cube")
        {
            spawnDelay = gameSettings.CubeSpawnDelay;
        } else if(prefabToSpawn.tag == "Sphere")
        {
            spawnDelay = gameSettings.SphereSpawnDelay;
        } else
        {
            spawnDelay = gameSettings.ConeSpawnDelay;
        }

        InvokeRepeating("SpawnPrefab", 0.0f, spawnDelay);
    }

    void SpawnPrefab()
    {
        ObjectPoolScript.Spawn(prefabToSpawn, transform.position, transform.rotation);
    }
}