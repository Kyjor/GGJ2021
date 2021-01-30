using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject[] spawnLocations;
    public GameObject[] standardItems;
    public GameObject[] balanceItems;

    private static ItemGenerator m_Instance = null;
    public static ItemGenerator Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (ItemGenerator)FindObjectOfType(typeof(ItemGenerator));
                if (m_Instance == null)
                    m_Instance = (new GameObject("ItemGenerator")).AddComponent<ItemGenerator>();
                DontDestroyOnLoad(m_Instance.gameObject);
            }
            return m_Instance;
        }
    }

    // type 0 is standard, type 1 is balance
    public void GenerateItem(int itemType)
    {
        int randomLocation = Random.Range(0, spawnLocations.Length);
        GameObject randomItem = itemType == 0 ? standardItems[Random.Range(0, standardItems.Length)] : 
            balanceItems[Random.Range(0, balanceItems.Length)];
        GameObject.Instantiate(randomItem, spawnLocations[randomLocation].transform);
    }
}
