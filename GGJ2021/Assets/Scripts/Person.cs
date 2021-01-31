using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public GameObject itemRequest;

    private Item item;
    private Transform endMarker;
    private float speed = 1.5F;
    private bool moving;

    void Start()
    {
        endMarker = transform;
    }

    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.Lerp(transform.position, 
                new Vector3(endMarker.position.x, 1.68f, endMarker.position.z), 
                speed * Time.deltaTime);
        }

        if (transform.position == endMarker.position)
        {
            moving = false;
        }
    }
    
    public void SetTarget(Transform target)
    {
        endMarker = target;
        moving = true;
    }

    public void SetItem(GameObject item)
    {
        this.itemRequest = item;
        this.item = item.GetComponent<Item>();
        DisplayRequestItem(false);
    }

    public void DisplayRequestItem(bool isDisplaying)
    {
        itemRequest.SetActive(isDisplaying);
    }

    public bool GiveItem(Item item)
    {
        if (item.itemName == this.item.itemName)
        {
            DisplayRequestItem(false);
            return true;
        }
        return false;
    }
}
