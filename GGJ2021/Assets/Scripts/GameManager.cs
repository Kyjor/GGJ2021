using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private float timeRemaining;
    private float gameEndTime;
    private float gameLength = 300f; // in seconds

    private static GameManager m_Instance = null;
    public static GameManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (GameManager)FindObjectOfType(typeof(GameManager));
                if (m_Instance == null)
                    m_Instance = (new GameObject("GameManager")).AddComponent<GameManager>();
                DontDestroyOnLoad(m_Instance.gameObject);
            }
            return m_Instance;
        }
    }

    void Start()
    {
        gameEndTime = Time.time + gameLength;
    }

    void Update()
    {
        timeRemaining = gameEndTime - Time.time;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0f;
            Time.timeScale = 0;
            UIManager.Instance.EndGame();
        }
    }

    // will load in a single scene, closing the current scene
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    public int GetCurrentScore()
    {
        return score;
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }
}
