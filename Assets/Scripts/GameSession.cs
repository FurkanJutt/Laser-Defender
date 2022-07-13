using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    int score = 0;
    int enemyRan = 0;

    private void Awake()
    {
        if (FindObjectsOfType<GameSession>().Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    public int GetScore() { return score; }

    public void AddToScore(int scoreValue){ score += scoreValue; }

    public int GetEnemyRanCount() { return enemyRan; }

    public void AddEnemyRanNumber()
    { 
        enemyRan += 1;
        if (SceneManager.GetActiveScene().name == "ExpertMode")
        {
            if (enemyRan >= 16)
                SceneManager.LoadScene("GameOver");
        }
    }

    public void ResetGame() { Destroy(gameObject); }
}
