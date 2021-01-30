using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject[] spawnLocations;

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
    public void GenerateItem(GameObject item)
    {
        int randomLocation = Random.Range(0, spawnLocations.Length);
        GameObject newItem = Instantiate(item, spawnLocations[randomLocation].transform);
        newItem.transform.localScale = new Vector3(1,1,1);
        //newItem.AddComponent<Item>();
    }
}
