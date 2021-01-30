using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    private Item item;

    public GameObject itemRequest;

    public Person(Item item)
    {
        this.item = item;
    }

    void Start()
    {
        // temp assign here while we don't have person generator
        this.item = itemRequest.GetComponent<Item>();
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
