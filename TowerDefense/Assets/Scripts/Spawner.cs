using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject jumperPrefab;
    public GameObject standardPrefab;
    public GameObject heavyPrefab;
    public Transform door; 
    // Start is called before the first frame update
    void Start()
    {
       InitWaves();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Add wait time between waves
        if (Globals.currentWaveWaitingEnemies.Count <= 0 && Globals.currentWaveEnemies.Count <= 0) {
            if (Globals.waves.Count > 0) Globals.updateMoney(300);
            StartNextWave();
        }
    }
    private void StartNextWave() {
        if(Globals.waves.Count > 0) {
            WaveInstantiate(Globals.waves.Dequeue());
            StartCoroutine(ActivateEnemies());
        }
    }
    private void InitWaves() {
        Globals.waves = new Queue<Wave>();
        Wave wave1 = new Wave();
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false);
        wave1.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 3.0f);
        wave1.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 3.0f);
        Globals.waves.Enqueue(wave1);
        Wave wave2 = new Wave();
        wave2.AddEnemy(Globals.EnemyType.Jumper, false, false, false);
        wave2.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 3.0f);
        wave2.AddEnemy(Globals.EnemyType.Standard, false, false, false);
        wave2.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.1f);

        
        Globals.waves.Enqueue(wave2);
    }
    IEnumerator ActivateEnemies()
    {
        if (Globals.currentWaveWaitingEnemies.Count > 0) {
            float spawnTime = Globals.currentWaveWaitingEnemies.Peek().GetComponent<EnemyMovement>().spawnWaitTime;
            yield return new WaitForSeconds(spawnTime);
            GameObject enemyGO = Globals.currentWaveWaitingEnemies.Dequeue();
            if (enemyGO)
            {
                enemyGO.SetActive(true);
                Globals.currentWaveEnemies.Add(enemyGO);
                StartCoroutine(ActivateEnemies());
            }
        }
    }
    private void WaveInstantiate(Wave wave)
    {
        Globals.currentWave = wave;
        for (int i = 0; i < wave.enemiesNumber; ++i) 
        {
            InstantiateEnemy(wave.GetNextEnemy());
        }
    }
    private void InstantiateEnemy(Wave.Enemy enemy) 
    {
        EnemyMovement enemyScript = null;
        GameObject enemyGO = null;
        switch (enemy.enemyType) 
        {
            case Globals.EnemyType.Jumper:
                if (jumperPrefab) enemyGO = jumperPrefab;
                break;
            case Globals.EnemyType.Standard:
                if (standardPrefab) enemyGO = standardPrefab;
                break;
            case Globals.EnemyType.Heavy:
                if (heavyPrefab) enemyGO = heavyPrefab;
                break;
        }
        GameObject enemyInstance = Instantiate(enemyGO, transform.position, Quaternion.identity);
        enemyInstance.transform.position = transform.position;
        enemyInstance.SetActive(false);
        enemyScript = enemyInstance.GetComponent<EnemyMovement>();
        enemyScript.Init(enemy.enemyType, enemy.bombUpgrade, enemy.mudArmorUpgrade, enemy.zombieUpgrade, door,enemy.waitTime);

        Globals.currentWaveWaitingEnemies.Enqueue(enemyInstance);
    }
}
