using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject ItemPromptText;

    private static UIManager m_Instance = null;
    public static UIManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (UIManager)FindObjectOfType(typeof(UIManager));
                if (m_Instance == null)
                    m_Instance = (new GameObject("UIManager")).AddComponent<UIManager>();
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

    public void ItemPrompt(bool active)
    {
        ItemPromptText.SetActive(active);
    }
}
