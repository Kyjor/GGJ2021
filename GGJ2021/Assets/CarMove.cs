using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    private float carSpeed;
    private Rigidbody rb;
    private int dir;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        carSpeed = Random.Range(50f, 70f);
        dir = transform.eulerAngles.y == 0 ? 1 : -1;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0f, 0f, carSpeed * dir);
    }
}
