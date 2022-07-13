using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorPath : MonoBehaviour
{
    // Configuration Parameters
    MeteorWave meteorWaveConfig;
    float moveSpeed;
    [SerializeField] int waypointIndex = 0;

    List<Transform> waypoints;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = meteorWaveConfig.GetMoveSpeed();
        waypoints = meteorWaveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    public void SetWaveConfig(MeteorWave waveConfig)
    {
        this.meteorWaveConfig = waveConfig;
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
        }
    }
}
