using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : MonoBehaviour
{
    public GameObject parent = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    IEnumerator Wait() {
        yield return new WaitForSeconds(Globals.mudAndDeadTime);
        gameObject.SetActive(false);
        EnemyMovement enemyScript = null;
        if (parent) {
            enemyScript = parent.GetComponent<EnemyMovement>();
            if (!enemyScript.hasZombieUpgrade || enemyScript.isZombie)
            {
                enemyScript.Kill();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Wait());
    }

}
