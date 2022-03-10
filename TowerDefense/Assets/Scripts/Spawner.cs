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
        Globals.currentWave = null;
        Globals.waves = new Queue<Wave>();
        Globals.currentWaveWaitingEnemies = new Queue<GameObject>();
        Globals.currentWaveEnemies = new List<GameObject>();
        InitWaves();
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
            if (Globals.currentWave != null) Globals.UpdateMoney(Globals.currentWave.moneyReward);
            Wave wave = Globals.waves.Peek();
            waitingWave = true;
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
        Globals.currentWaveWaitingEnemies = new Queue<GameObject>();
        Globals.currentWaveEnemies = new List<GameObject>();
        /// bomb, mud,  zombie
        Globals.waves = new Queue<Wave>();
        Wave wave1 = new Wave(0.0f, 400.0f);
        //test bomb +mud + zombie por separado
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 1.0f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 1.0f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 1.0f);
        wave1.AddEnemy(Globals.EnemyType.Standard, false, false, false, 1.0f);
        wave1.UpdateEnemiesGoldReward();
        // wave1.AddEnemy(Globals.EnemyType.Standard, false, false, true, 10.0f);

        Globals.waves.Enqueue(wave1);
        Wave wave2 = new Wave(4.0f, 500.0f);
        wave2.AddEnemy(Globals.EnemyType.Standard, false, true, false, 1.0f);
        wave2.AddEnemy(Globals.EnemyType.Standard, false, false, false, 1.0f);
        wave2.AddEnemy(Globals.EnemyType.Standard, false, false, false, 1.0f);
        wave2.AddEnemy(Globals.EnemyType.Standard, false, false, false, 1.0f);
        wave2.AddEnemy(Globals.EnemyType.Standard, false, false, false, 1.0f);
        wave2.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 1.0f);
        wave2.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 1.0f);
        wave2.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 1.0f);
        wave2.UpdateEnemiesGoldReward();

        Globals.waves.Enqueue(wave2);
        Wave wave3 = new Wave(4.0f, 525.0f);
        wave3.AddEnemy(Globals.EnemyType.Standard, true, false, false, 1.0f);
        wave3.AddEnemy(Globals.EnemyType.Standard, false, false, false, 1.0f);
        wave3.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 1.0f);
        wave3.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 1.0f);
        wave3.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 1.0f);
        wave3.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 1.0f);
        wave3.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 1.0f);
        wave3.AddEnemy(Globals.EnemyType.Heavy, false, false, false, 1.5f);
        wave3.AddEnemy(Globals.EnemyType.Standard, false, false, false, 1.0f);
        wave3.AddEnemy(Globals.EnemyType.Standard, false, false, false, 1.0f);
        wave3.AddEnemy(Globals.EnemyType.Standard, false, false, false, 1.0f);
        wave3.AddEnemy(Globals.EnemyType.Standard, false, false, false, 1.0f);
        wave3.AddEnemy(Globals.EnemyType.Standard, false, false, false, 1.0f);
        wave3.AddEnemy(Globals.EnemyType.Standard, false, false, false, 1.0f);
        wave3.UpdateEnemiesGoldReward();
        Globals.waves.Enqueue(wave3);

        Wave wave4 = new Wave(4.0f, 550.0f);

        wave4.AddEnemy(Globals.EnemyType.Standard, true, false, false, 1.0f);
        wave4.AddEnemy(Globals.EnemyType.Standard, true, false, false, 1.0f);
        wave4.AddEnemy(Globals.EnemyType.Standard, true, false, false, 1.0f);
        wave4.AddEnemy(Globals.EnemyType.Standard, false, true, false, 1.0f);
        wave4.AddEnemy(Globals.EnemyType.Standard, false, true, false, 1.0f);
        wave4.AddEnemy(Globals.EnemyType.Standard, false, true, false, 1.0f);
        wave4.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 1.0f);
        wave4.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 1.0f);
        wave4.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 1.0f);
        wave4.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 1.0f);
        wave4.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 1.0f);
        wave4.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 1.0f);
        wave4.UpdateEnemiesGoldReward();
        Globals.waves.Enqueue(wave4);

        Wave wave5 = new Wave(4.0f, 575.0f);

        wave5.AddEnemy(Globals.EnemyType.Standard, true, false, false, 1.0f);
        wave5.AddEnemy(Globals.EnemyType.Standard, true, false, false, 1.0f);
        wave5.AddEnemy(Globals.EnemyType.Standard, true, false, false, 1.0f);
        wave5.AddEnemy(Globals.EnemyType.Standard, true, false, false, 1.0f);
        wave5.AddEnemy(Globals.EnemyType.Standard, true, false, false, 1.0f);
        wave5.AddEnemy(Globals.EnemyType.Standard, true, false, false, 1.0f);
        wave5.AddEnemy(Globals.EnemyType.Standard, true, false, false, 1.0f);
        wave5.AddEnemy(Globals.EnemyType.Standard, true, false, false, 1.0f);
        wave5.AddEnemy(Globals.EnemyType.Standard, true, false, false, 1.0f);
        wave5.AddEnemy(Globals.EnemyType.Standard, false, true, false, 1.0f);
        wave5.AddEnemy(Globals.EnemyType.Standard, false, true, false, 1.0f);
        wave5.AddEnemy(Globals.EnemyType.Heavy, false, false, true, 1.0f);
        wave5.AddEnemy(Globals.EnemyType.Heavy, false, false, true, 1.0f);
        wave5.UpdateEnemiesGoldReward();
        Globals.waves.Enqueue(wave5);

        Wave wave6 = new Wave(5.0f, 625.0f);

        wave6.AddEnemy(Globals.EnemyType.Jumper, true, false, false, 1.0f);
        wave6.AddEnemy(Globals.EnemyType.Jumper, true, false, false, 1.0f);
        wave6.AddEnemy(Globals.EnemyType.Jumper, true, false, false, 1.0f);
        wave6.AddEnemy(Globals.EnemyType.Standard, true, false, true, 1.0f);
        wave6.AddEnemy(Globals.EnemyType.Standard, true, false, true, 1.0f);
        wave6.AddEnemy(Globals.EnemyType.Standard, true, false, true, 1.0f);
        wave6.AddEnemy(Globals.EnemyType.Standard, true, false, false, 1.0f);
        wave6.AddEnemy(Globals.EnemyType.Standard, true, false, false, 1.0f);
        wave6.AddEnemy(Globals.EnemyType.Standard, true, false, false, 1.0f);
        wave6.AddEnemy(Globals.EnemyType.Standard, true, false, false, 1.0f);
        wave6.AddEnemy(Globals.EnemyType.Standard, false, false, false, 1.0f);
        wave6.AddEnemy(Globals.EnemyType.Standard, false, false, false, 1.0f);
        wave6.AddEnemy(Globals.EnemyType.Heavy, false, false, false, 1.0f);
        wave6.AddEnemy(Globals.EnemyType.Heavy, false, false, false, 1.0f);
        wave6.UpdateEnemiesGoldReward();
        Globals.waves.Enqueue(wave6);
        Globals.totalNumberOfWaves = Globals.waves.Count;

        Wave wave7 = new Wave(5.0f, 650.0f);

        wave7.AddEnemy(Globals.EnemyType.Heavy, false, true, false, 1.0f);
        wave7.AddEnemy(Globals.EnemyType.Heavy, false, true, false, 2.5f);
        wave7.AddEnemy(Globals.EnemyType.Heavy, false, true, false, 2.5f);
        wave7.AddEnemy(Globals.EnemyType.Heavy, false, false, true, 1.0f);
        wave7.AddEnemy(Globals.EnemyType.Jumper, false, false, true, 1.0f);
        wave7.AddEnemy(Globals.EnemyType.Jumper, false, false, true, 1.0f);
        wave7.AddEnemy(Globals.EnemyType.Jumper, false, false, true, 1.0f);
        wave7.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 1.0f);
        wave7.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 1.0f);
        wave7.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 1.0f);

        wave7.AddEnemy(Globals.EnemyType.Heavy, true, false, false, 1.0f);
        wave7.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.7f);
        wave7.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.7f);
        wave7.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.7f);
        wave7.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.7f);
        wave7.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.7f);
        wave7.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.7f);
        wave7.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.7f);
        wave7.AddEnemy(Globals.EnemyType.Standard, true, false, true, 0.7f);
        wave7.AddEnemy(Globals.EnemyType.Standard, true, false, true, 0.7f);
        wave7.AddEnemy(Globals.EnemyType.Standard, true, false, true, 0.7f);

        wave7.AddEnemy(Globals.EnemyType.Heavy, true, false, true, 1.0f);

        wave7.UpdateEnemiesGoldReward();
        Globals.waves.Enqueue(wave7);
        Globals.totalNumberOfWaves = Globals.waves.Count;

        Wave wave8 = new Wave(5.0f, 700.0f);


        wave8.AddEnemy(Globals.EnemyType.Standard, true, false, true, 0.7f);
        wave8.AddEnemy(Globals.EnemyType.Standard, true, false, true, 0.01f);
        wave8.AddEnemy(Globals.EnemyType.Standard, true, false, true, 0.01f);
        wave8.AddEnemy(Globals.EnemyType.Standard, true, false, true, 0.01f);
        wave8.AddEnemy(Globals.EnemyType.Standard, true, false, true, 0.01f);
        wave8.AddEnemy(Globals.EnemyType.Standard, true, false, true, 0.01f);
        wave8.AddEnemy(Globals.EnemyType.Standard, true, false, true, 0.01f);
        wave8.AddEnemy(Globals.EnemyType.Heavy, false, true, false, 0.5f);
        wave8.AddEnemy(Globals.EnemyType.Heavy, true, false, true, 0.5f);
        wave8.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.7f);
        wave8.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.7f);
        wave8.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.7f);
        wave8.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.7f);
        wave8.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.7f);
        wave8.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.7f);
        wave8.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.7f);
        wave8.AddEnemy(Globals.EnemyType.Standard, true, false, true, 0.7f);
        wave8.AddEnemy(Globals.EnemyType.Standard, true, false, true, 0.7f);
        wave8.AddEnemy(Globals.EnemyType.Standard, true, false, true, 0.7f);
        wave8.AddEnemy(Globals.EnemyType.Jumper, true, false, true, 0.5f);
        wave8.AddEnemy(Globals.EnemyType.Jumper, true, false, true, 0.5f);
        wave8.AddEnemy(Globals.EnemyType.Jumper, true, false, true, 0.5f);
        wave8.AddEnemy(Globals.EnemyType.Jumper, true, false, true, 0.5f);
        wave8.AddEnemy(Globals.EnemyType.Jumper, true, false, true, 0.5f);
        wave8.AddEnemy(Globals.EnemyType.Jumper, true, false, true, 0.5f);
        wave8.AddEnemy(Globals.EnemyType.Jumper, true, false, true, 0.5f);
        wave8.AddEnemy(Globals.EnemyType.Jumper, true, false, true, 0.5f);

        wave8.UpdateEnemiesGoldReward();
        Globals.waves.Enqueue(wave8);


        Wave wave9 = new Wave(5.0f, 750.0f);


        wave9.AddEnemy(Globals.EnemyType.Heavy, false, true, true, 0.5f);
        wave9.AddEnemy(Globals.EnemyType.Heavy, true, false, false, 0.5f);
        wave9.AddEnemy(Globals.EnemyType.Heavy, true, false, false, 0.5f);
        wave9.AddEnemy(Globals.EnemyType.Heavy, true, false, false, 0.5f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Heavy, true, false, true, 1.2f);
        wave9.AddEnemy(Globals.EnemyType.Heavy, true, true, false, 0.5f);
        wave9.AddEnemy(Globals.EnemyType.Heavy, true, false, false, 0.5f);
        wave9.AddEnemy(Globals.EnemyType.Heavy, true, false, true, 0.5f);


        wave9.UpdateEnemiesGoldReward();
        Globals.waves.Enqueue(wave9);


        Wave wave10 = new Wave(5.0f, 800.0f);

        wave10.AddEnemy(Globals.EnemyType.Jumper, true, true, false, 0.5f);
        wave10.AddEnemy(Globals.EnemyType.Jumper, true, false, true, 0.5f);
        wave10.AddEnemy(Globals.EnemyType.Jumper, false, true, true, 0.5f);
        wave10.AddEnemy(Globals.EnemyType.Jumper, true, true, false, 0.5f);
        wave10.AddEnemy(Globals.EnemyType.Jumper, true, false, true, 0.5f);
        wave10.AddEnemy(Globals.EnemyType.Jumper, false, true, true, 0.5f);
        wave10.AddEnemy(Globals.EnemyType.Jumper, true, true, false, 0.5f);
        wave10.AddEnemy(Globals.EnemyType.Jumper, true, false, true, 0.5f);
        wave10.AddEnemy(Globals.EnemyType.Jumper, false, true, true, 0.5f);
        wave10.AddEnemy(Globals.EnemyType.Standard, false, true, false, 0.3f);
        wave10.AddEnemy(Globals.EnemyType.Standard, false, true, false, 0.3f);
        wave10.AddEnemy(Globals.EnemyType.Standard, false, true, false, 0.3f);
        wave10.AddEnemy(Globals.EnemyType.Standard, false, true, false, 0.3f);
        wave10.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 2f);
        wave10.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 2f);
        wave10.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 2f);
        wave10.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 2f);
        wave10.AddEnemy(Globals.EnemyType.Jumper, true, true, false, 0.5f);
        wave10.AddEnemy(Globals.EnemyType.Jumper, true, false, true, 0.5f);
        wave10.AddEnemy(Globals.EnemyType.Jumper, false, true, true, 0.5f);
        wave10.AddEnemy(Globals.EnemyType.Jumper, true, true, false, 0.5f);
        wave10.AddEnemy(Globals.EnemyType.Jumper, true, false, true, 0.5f);
        wave10.AddEnemy(Globals.EnemyType.Jumper, false, true, true, 0.5f);
        wave10.AddEnemy(Globals.EnemyType.Jumper, true, true, false, 0.5f);
        wave10.AddEnemy(Globals.EnemyType.Jumper, true, false, true, 0.5f);
        wave10.AddEnemy(Globals.EnemyType.Jumper, false, true, true, 0.5f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave9.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave10.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 2f);
        wave10.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 2f);
        wave10.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 2f);
        wave10.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 2f);
        wave10.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 2f);
        wave10.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 2f);
        wave10.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 2f);
        wave10.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 2f);


        wave10.UpdateEnemiesGoldReward();
        Globals.waves.Enqueue(wave10);

        Wave wave11 = new Wave(5.0f, 850.0f);

        wave11.AddEnemy(Globals.EnemyType.Jumper, false, false, false, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, false, false, false, 0.8f);

        wave11.AddEnemy(Globals.EnemyType.Jumper, true, false, false, 2f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, false, false, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, false, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, false, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, false, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, false, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, true, false, false, 0.8f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, true, false, false, 0.8f);

        wave11.AddEnemy(Globals.EnemyType.Jumper, false, true, false, 2f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, false, true, false, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, false, true, false, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, false, true, false, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, false, true, false, 0.8f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, false, true, false, 0.8f);

        wave11.AddEnemy(Globals.EnemyType.Jumper, false, false, true, 2f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, false, false, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, false, false, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, false, false, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, false, false, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, false, false, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, false, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, false, false, true, 0.8f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, false, false, true, 0.8f);

        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, false, 2f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, false, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, false, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, false, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, false, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, false, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, false, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, false, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, false, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, true, true, false, 0.8f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, true, true, false, 0.8f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, true, true, false, 0.8f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, true, true, false, 0.8f);


        wave11.AddEnemy(Globals.EnemyType.Jumper, false, true, true, 2f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, false, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, false, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, false, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, false, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, false, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, false, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, false, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, false, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, false, true, true, 0.8f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, false, true, true, 0.8f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, false, true, true, 0.8f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, false, true, true, 0.8f);

        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, true, 2f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Jumper, true, true, true, 0.5f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Standard, true, true, true, 0.3f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 0.8f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 0.8f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 0.8f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 0.8f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 0.8f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 0.8f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 0.8f);
        wave11.AddEnemy(Globals.EnemyType.Heavy, true, true, true, 0.8f);





        wave11.UpdateEnemiesGoldReward();
        Globals.waves.Enqueue(wave11);
        Globals.totalNumberOfWaves = Globals.waves.Count;
    }
    IEnumerator ActivateEnemies()
    {
        while (Globals.currentWaveWaitingEnemies.Count > 0) {
            EnemyMovement eMovement = Globals.currentWaveWaitingEnemies.Peek().GetComponent<EnemyMovement>();
            float spawnTime = 0.0f;
            if (eMovement) spawnTime = eMovement.spawnWaitTime;
            yield return new WaitForSeconds(spawnTime);
            GameObject enemyGO = Globals.currentWaveWaitingEnemies.Dequeue();
            if (enemyGO)
            {
                enemyGO.SetActive(true);
                Globals.currentWaveEnemies.Add(enemyGO);
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
        enemyScript.Init(enemy.enemyType, enemy.bombUpgrade, enemy.mudArmorUpgrade, enemy.zombieUpgrade, door,enemy.waitTime, enemy.goldReward);

        Globals.currentWaveWaitingEnemies.Enqueue(enemyInstance);
    }
}
