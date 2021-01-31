using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreField;
    public TextMeshProUGUI timerField;

    public GameObject endGamePanel;
    public TextMeshProUGUI endGameScore;

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

    // Update is called once per frame
    void Update()
    {
        timerField.text = "Time Remaining: " + Math.Floor(GameManager.Instance.GetTimeRemaining()).ToString();
        scoreField.text = "Score: " + GameManager.Instance.GetCurrentScore().ToString();
    }

    public void EndGame()
    {
        endGamePanel.SetActive(true);
        endGameScore.text = "Score: " + GameManager.Instance.GetCurrentScore().ToString();
    }
}
