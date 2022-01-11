using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float hp = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        hp = Globals.doorDefaultHp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            TakeDamage(other.GetComponent<EnemyMovement>().doorDamage);
            other.GetComponent<EnemyMovement>().Kill();
        }
    }
    private void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0.0f) GameOver();
    }
    private void GameOver()
    {

    }
    private void Win() 
    {
        
    }
}
