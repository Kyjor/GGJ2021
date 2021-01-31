using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    public GameObject playerArea;
    public GameObject personArea;

    private Person nextPerson;

    private bool canRequest = true;

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
        nextPerson = WaitingLine.Instance.GetFrontOfLine();
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
            Player.Instance.DropItem();
            
            WaitingLine.Instance.UpdatePositions();
            nextPerson = WaitingLine.Instance.GetFrontOfLine();

            canRequest = true;
        } else
        {
            Debug.Log("wrong item");
        }
    }

    public void AskForItem()
    {
        if (canRequest)
        {
            nextPerson.DisplayRequestItem(true);
            ItemGenerator.Instance.GenerateItem(nextPerson.itemRequest);
            canRequest = false;
        }
    }
}
