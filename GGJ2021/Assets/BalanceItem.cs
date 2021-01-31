using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceItem : Item
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float tipSpeed = 1f;
    [SerializeField] private float angleWhenItWillFall = 90f;
    [SerializeField] Color initialIndicatorColor = Color.green;
    [SerializeField] private Vector2 moveAxes;
    [SerializeField] private Transform greatGrandparent;

    public override void Start()
    {
        base.Start();
        
    }
    private void FixedUpdate()
    {
        if (isHeld)
        {
            transform.Rotate(greatGrandparent.right, tipSpeed * Time.fixedDeltaTime, Space.World);
            heldIndicator.GetComponent<Renderer>().material.color = Color.Lerp(initialIndicatorColor, Color.red, Mathf.Abs(transform.eulerAngles.x/angleWhenItWillFall));
            if (Mathf.Abs(transform.eulerAngles.x) > angleWhenItWillFall)
            {
               base.DropItem();
            }
        }

        if (moveAxes.y > .1f)
        {
            transform.Rotate(greatGrandparent.right, -(tipSpeed + 0.1f) * Time.fixedDeltaTime,Space.World);
        }
    }

    public override void OnPickup()
    {
        base.OnPickup();
        try
        {
            characterController = GetComponentInParent<CharacterController>();
            characterController.axesDelegate += (vec2, sprinting) => { moveAxes = vec2; };
            greatGrandparent = transform.parent.parent.parent;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
