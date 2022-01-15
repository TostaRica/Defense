using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject jumperPrefab;
    public GameObject standardPrefab;
    public GameObject heavyPrefab;
    public Transform door;
    bool waveInProgress = false;
    bool waitingWave = false;
    void Start()
    {
       InitWaves();
       Globals.totalNumberOfWaves = Globals.waves.Count;
    }
    void Update()
    {
        if (!waveInProgress)
        {
            if (!waitingWave && Globals.waves.Count > 0)
            {
                StartCoroutine(WaitNextWave());
            }
        }
        else
        {
            waveInProgress = !(Globals.currentWaveWaitingEnemies.Count <= 0 && Globals.currentWaveEnemies.Count <= 0);
        }
    }
    IEnumerator WaitNextWave()
    {
        if (Globals.waves.Count > 0)
        {
            Wave wave = Globals.waves.Peek();
            waitingWave = true;
            Globals.updateMoney(wave.moneyReward);
            yield return new WaitForSeconds(wave.waitTime);
            waitingWave = false;
            StartNextWave();
        }
    }
    private void StartNextWave() {
        if(Globals.waves.Count > 0) {
            Globals.currentWaveNumber++;
            WaveInstantiate(Globals.waves.Dequeue());
            StartCoroutine(ActivateEnemies());
            waveInProgress = true;
        }
    }
    private void InitWaves() {
        Globals.waves = new Queue<Wave>();
        Wave wave1 = new Wave(0.0f, 45000.0f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.5f);
        wave1.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 3.0f);
        wave1.AddEnemy(Globals.EnemyType.Jumper, false, false, false);
        Globals.waves.Enqueue(wave1);
        Wave wave2 = new Wave(15.0f, 300.0f);
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
