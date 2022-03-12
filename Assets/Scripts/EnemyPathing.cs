using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    List<Transform> waypoints;
    WaveConfig waveConfig;
    int waypoint_index = 0;

    private void Start()
    {
        waypoints = waveConfig.GetEnemyPath();
        transform.position = waypoints[waypoint_index].transform.position;
    }

    private void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (waypoint_index <= waypoints.Count - 1)
        {
            Vector3 current_position = transform.position;
            Vector3 next_position = waypoints[waypoint_index].transform.position;
            float movement_speed_by_time = waveConfig.GetEnemyMovementSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(current_position, next_position, movement_speed_by_time);
            if (current_position == next_position)
            {
                waypoint_index++;
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
