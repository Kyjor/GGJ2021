using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceItem : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float tipSpeed = 10f;
    [SerializeField] private float angleWhenItWillFall = 90f;
    private Rigidbody rb;
    private bool isHeld;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isHeld)
        {
            transform.Rotate(Vector3.right, tipSpeed * Time.fixedDeltaTime);
            
            if (Mathf.Abs(transform.eulerAngles.x) > angleWhenItWillFall)
            {
                print("drop");
                transform.parent = null;
                isHeld = false;
                rb.isKinematic = false;
            }
        } 
    }

    public void OnPickup()
    {
        try
        {
            characterController = GetComponentInParent<CharacterController>();
            rb.isKinematic = true;
            isHeld = true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
