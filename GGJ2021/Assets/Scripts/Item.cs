using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    protected Rigidbody rb;
    protected BoxCollider bc;
    protected bool isHeld;
    [SerializeField] protected GameObject heldIndicator;
    public bool canBePickedUp;
    [SerializeField] private float pickUpEnabledDelay;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        heldIndicator = transform.GetChild(0).gameObject;
        canBePickedUp = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DropItem();
        }
    }

    public virtual void DropItem()
    {
        print("drop");
        transform.parent = null;
        isHeld = false;
        bc.isTrigger = false;
        rb.isKinematic = false;
        StartCoroutine(EnablePickup());
    }

    private IEnumerator EnablePickup()
    {
        yield return new WaitForSeconds(pickUpEnabledDelay);
        canBePickedUp = true;
    }

    public virtual void OnPickup()
    {
        canBePickedUp = false;
        rb.isKinematic = true;
        bc.isTrigger = true;
        isHeld = true;
    }
    

}
