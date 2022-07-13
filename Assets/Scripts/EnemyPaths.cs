using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyPaths : MonoBehaviour
{
    // Configuration Parameters
    WaveConfig waveConfig;
    float moveSpeed;
    [SerializeField] int waypointIndex = 0;

    List<Transform> waypoints;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = waveConfig.GetMoveSpeed();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    // Update is called once per frame
    void Update()
    {
        MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movePerFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movePerFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
            if (SceneManager.GetActiveScene().name == "ExpertMode")
                FindObjectOfType<GameSession>().AddEnemyRanNumber();
        }
    }
}
