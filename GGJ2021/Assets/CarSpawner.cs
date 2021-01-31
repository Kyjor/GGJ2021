using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] cars;
    public Transform[] carSpawns;
    float timeBetweenSpawns = 1f;

    private float spawnTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnCar();
    }

    private void SpawnCar()
    {
        GameObject carToSpawn = cars[Random.Range(0, cars.Length - 1)];
        int position = Random.Range(0, 1);
        int rotation = position == 0 ? 0 : -180;
        GameObject newCar = GameObject.Instantiate(carToSpawn, carSpawns[position].position, carSpawns[position].rotation, null) as  GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer < timeBetweenSpawns)
        {
            spawnTimer += Time.deltaTime;
        }
        else if (spawnTimer > timeBetweenSpawns)
        {
            SpawnCar();
            spawnTimer = 0;
        }
    }
}
