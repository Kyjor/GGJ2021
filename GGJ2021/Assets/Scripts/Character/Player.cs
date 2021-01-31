using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Item heldItem;
    public GameObject itemHolder;

    private static Player m_Instance = null;
    public static Player Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (Player)FindObjectOfType(typeof(Player));
                if (m_Instance == null)
                    m_Instance = (new GameObject("Player")).AddComponent<Player>();
                DontDestroyOnLoad(m_Instance.gameObject);
            }
            return m_Instance;
        }
    }

    public void DropItem()
    {
        Destroy(heldItem.gameObject);
        GetComponent<CharacterState>().isHoldingItem = false;

        /*
        GetComponent<CharacterState>().isHoldingItem = false;
        GetComponent<Item>().DropItem();
        GetComponent<Item>().GetComponent<Collider>().isTrigger = false;
        */
        heldItem = null;
    }
}
