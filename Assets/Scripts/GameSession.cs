using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int intPlayerLives = 3;
    [SerializeField] int intScore = 0;

    [SerializeField] TextMeshProUGUI livesText;
    
    [SerializeField] TextMeshProUGUI scoreText;


    void Awake()
    {
        int intNumGameSessions = FindObjectsOfType<GameSession>().Length;

        if(intNumGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() 
    {
        livesText.text = intPlayerLives.ToString();
        scoreText.text = intScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (intPlayerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToScore(int intPointsToAdd)
    {
        intScore += intPointsToAdd;
        scoreText.text = intScore.ToString();
    }
    
    void TakeLife()
    {
        intPlayerLives--;
        int intCurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(intCurrentSceneIndex);
        livesText.text = intPlayerLives.ToString();
    }
    

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
