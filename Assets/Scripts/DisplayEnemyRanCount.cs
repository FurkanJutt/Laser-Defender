using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayEnemyRanCount : MonoBehaviour
{
    TextMeshProUGUI textEnemyRanCount;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        textEnemyRanCount = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "ExpertMode")
            textEnemyRanCount.text = gameSession.GetEnemyRanCount().ToString();
    }
}
