using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CharacterItemPickup : MonoBehaviour
{
    private CharacterState characterState;
    public Transform itemHolderTransform;
    [SerializeField]private Vector3 pickUpHeldPosition;

    private void Start()
    {
        characterState = GetComponent<CharacterState>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            other.transform.SetParent(itemHolderTransform);
            other.transform.localPosition = pickUpHeldPosition;
            other.transform.rotation = Quaternion.Euler(Vector3.zero);
            characterState.isHoldingItem = true;
            var balanceItem = other.gameObject.GetComponent<BalanceItem>() ?? null;
            if (balanceItem)
            {
                balanceItem.OnPickup();
            }
        }
    }
}
