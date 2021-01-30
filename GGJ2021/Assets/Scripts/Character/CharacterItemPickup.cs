using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CharacterItemPickup : MonoBehaviour
{
    private CharacterState characterState;
    [SerializeField]private Vector3 pickUpHeldPosition;

    private void Start()
    {
        characterState = GetComponent<CharacterState>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            other.transform.SetParent(Player.Instance.itemHolder.transform);
            other.transform.localPosition = pickUpHeldPosition;
            other.transform.rotation = Quaternion.Euler(Vector3.zero);
            Player.Instance.heldItem = other.gameObject.GetComponent<Item>();
            characterState.isHoldingItem = true;
            var item = other.gameObject.GetComponent<BalanceItem>() ?? other.gameObject.GetComponent<Item>();
            if (item)
            {
                item.OnPickup();
            }
        }
    }
}
