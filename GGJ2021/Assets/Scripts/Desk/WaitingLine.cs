using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingLine : MonoBehaviour
{
    public GameObject[] spots;
    public GameObject personPrefab;
    public GameObject[] items;

    private float timeForUpdate;
    private float timeBetweenUpdates = 1f;

    private static WaitingLine m_Instance = null;
    public static WaitingLine Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (WaitingLine)FindObjectOfType(typeof(WaitingLine));
                if (m_Instance == null)
                    m_Instance = (new GameObject("WaitingLine")).AddComponent<WaitingLine>();
                DontDestroyOnLoad(m_Instance.gameObject);
            }
            return m_Instance;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        //spots = GetComponentsInChildren<GameObject>();
        PopulateLine();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Time.deltaTime >= timeForUpdate)
        {
            //update position if nothing is in first position
            if (spots[0].GetComponentInChildren<GameObject>())
            {
                //UpdatePositions();
                // not sure if I'm going to have a timer based thing here or not
            }
        }
        timeForUpdate += timeBetweenUpdates;
        */
    }

    public void UpdatePositions()
    {
        // remove first person
        Transform firstInLine = spots[0].GetComponentsInChildren<Transform>()[1].transform;
        firstInLine.position = new Vector3(10000,10000,10000);
        firstInLine.SetParent(null);
        Destroy(firstInLine.gameObject);

        // move all the people forward
        for (int i=1; i < spots.Length; i++)
        {
            GameObject person = spots[i].GetComponentsInChildren<Transform>()[1].gameObject;
            person.transform.SetParent(spots[i-1].transform);
            person.GetComponent<Person>().SetTarget(spots[i-1].transform);
            //person.transform.localPosition = new Vector3(0, person.transform.localPosition.y, 0);
            //Debug.Log("test " + i);
        }

        // new person at the end of the line
        GameObject newPerson = Instantiate(personPrefab, spots[spots.Length-1].transform);
        GameObject newItem = Instantiate(items[Random.Range(0, items.Length)], newPerson.transform);
        newPerson.GetComponent<Person>().SetItem(newItem);
    }

    public Person GetFrontOfLine()
    {
        return spots[0].GetComponentInChildren<Person>();
    }

    private void PopulateLine()
    {
        for (int i=0; i < spots.Length; i++)
        {
            GameObject person = Instantiate(personPrefab, spots[i].transform);
            GameObject newItem = Instantiate(items[Random.Range(0, items.Length)], person.transform);
            person.GetComponent<Person>().SetItem(newItem);
        }
    }
}
