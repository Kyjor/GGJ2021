using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    public GameObject playerArea;
    public GameObject personArea;

    public Person nextPerson;

    private bool canRequest;

    private static Desk m_Instance = null;
    public static Desk Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (Desk)FindObjectOfType(typeof(Desk));
                if (m_Instance == null)
                    m_Instance = (new GameObject("Desk")).AddComponent<Desk>();
                DontDestroyOnLoad(m_Instance.gameObject);
            }
            return m_Instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GiveItem(Item item)
    {
        if (nextPerson.GiveItem(item))
        {
            Debug.Log("right item");
            Player.Instance.GetComponent<CharacterState>().isHoldingItem = false;
            Player.Instance.heldItem.GetComponent<Item>().DropItem();
            Player.Instance.heldItem = null;
        } else
        {
            Debug.Log("wrong item");
        }
    }

    public void AskForItem()
    {
        nextPerson.DisplayRequestItem(true);

        ItemGenerator.Instance.GenerateItem(nextPerson.itemRequest);
    }
}
