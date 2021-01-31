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
            if (Player.Instance.GetComponent<CharacterState>().isHoldingItem)
            {
                Desk.Instance.GiveItem(Player.Instance.heldItem);
            } else
            {
                //Desk.Instance.AskForItem();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
