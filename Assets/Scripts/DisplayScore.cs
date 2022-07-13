using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    TextMeshProUGUI textScore;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        textScore = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = gameSession.GetScore().ToString();
    }
}
