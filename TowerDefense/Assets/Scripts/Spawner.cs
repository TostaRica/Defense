using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject jumperPrefab;
    public GameObject standardPrefab;
    public GameObject heavyPrefab;

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(Wave1());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Wave1() 
    {
        Spawn(Globals.EnemyType.Standard, false, false, false);
        yield return new WaitForSeconds(0.5f);
        //Spawn(Globals.EnemyType.Standard, false, true, false);
        yield return new WaitForSeconds(1.5f);
        Spawn(Globals.EnemyType.Standard, false, false, false);

    }
    private void Spawn(Globals.EnemyType type = Globals.EnemyType.Standard, bool bomb = false, bool mudArmor = false, bool zombie = false) 
    {
        EnemyMovement enemyScript = null;
        GameObject enemy = null;
        switch (type) 
        {
            case Globals.EnemyType.Jumper:
                if (jumperPrefab) enemy = jumperPrefab;
                break;
            case Globals.EnemyType.Standard:
                if (standardPrefab) enemy = standardPrefab;
                break;
            case Globals.EnemyType.Heavy:
                if (heavyPrefab) enemy = heavyPrefab;
                break;
        }

        if (enemy) {
            enemy.transform.position = transform.position;
            GameObject enemyInstance = Instantiate(enemy, transform.position, Quaternion.identity);
            Globals.enemies.Add(enemyInstance);
            enemyScript = enemyInstance.GetComponent<EnemyMovement>();
            enemyScript.Init(type, bomb, mudArmor, zombie);
            enemyInstance.SetActive(true);
        }

    }
}
