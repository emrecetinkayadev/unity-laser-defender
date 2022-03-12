using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemy_prefab;
    [SerializeField] GameObject enemy_path;
    [SerializeField] float time_between_spawns = 0.5f;
    [SerializeField] float spawn_time_random_factor = 0.3f;
    [SerializeField] int number_of_enemies = 5;
    [SerializeField] float enemy_movementspeed = 2f; 

    public GameObject GetEnemyPrefab() { return enemy_prefab; }
    public List<Transform> GetEnemyPath() 
    {
        List<Transform> enemy_way_follow = new List<Transform>();
        foreach  (Transform child in enemy_path.transform)
        {
            enemy_way_follow.Add(child);
        }

        return enemy_way_follow; 
    }
    public float GetTimeBetweenSpawns() { return time_between_spawns; }
    public float GetSpawnTimeRandom() { return spawn_time_random_factor; }
    public int GetNumberofEnemies() { return number_of_enemies; }
    public float GetEnemyMovementSpeed() { return enemy_movementspeed; }



}
