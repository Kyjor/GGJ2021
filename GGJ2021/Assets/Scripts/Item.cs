using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    protected Rigidbody rb;
    protected bool isHeld;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void DropItem()
    {
        print("drop");
        transform.parent = null;
        isHeld = false;
        rb.isKinematic = false;
    }
    public virtual void OnPickup()
    {
        rb.isKinematic = true;
        isHeld = true;
    }
    

}
