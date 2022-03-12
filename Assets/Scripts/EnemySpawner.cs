using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int starting_wave = 0;
    [SerializeField] Boolean LoopActive = true;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (LoopActive);
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int wave_index = starting_wave; wave_index <= waveConfigs.Count-1; wave_index++)
        {
            WaveConfig current_wave = waveConfigs[wave_index];
            yield return StartCoroutine(SpawnAllEnemyInWave(current_wave));
        }
    }

    private IEnumerator SpawnAllEnemyInWave(WaveConfig waveConfig)
    {
        for(int enemy_count = 0; enemy_count < waveConfig.GetNumberofEnemies(); enemy_count++)
        {
            GameObject new_enemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetEnemyPath()[0].transform.position, Quaternion.identity);
            new_enemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());           
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
