using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceItem : Item
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float tipSpeed = 10f;
    [SerializeField] private float angleWhenItWillFall = 90f;
   
    

    private void FixedUpdate()
    {
        if (isHeld)
        {
            transform.Rotate(Vector3.right, tipSpeed * Time.fixedDeltaTime);
            
            if (Mathf.Abs(transform.eulerAngles.x) > angleWhenItWillFall)
            {
               base.DropItem();
            }
        } 
    }

    public override void OnPickup()
    {
        base.OnPickup();
        try
        {
            characterController = GetComponentInParent<CharacterController>();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
