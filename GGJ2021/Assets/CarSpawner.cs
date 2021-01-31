using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] cars;
    public Transform[] carSpawns;
    float timeBetweenSpawns = 5f;

    private float spawnTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnCar();
    }

    private void SpawnCar()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
