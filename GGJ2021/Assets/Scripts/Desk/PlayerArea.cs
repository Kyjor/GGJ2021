using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player.holdingItem)
            {
                Desk.Instance.GiveItem(player.heldItem);
            } else
            {
                Desk.Instance.AskForItem();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
