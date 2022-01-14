using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Door : MonoBehaviour
{
    public Text castleHpLabel;
    public Text goldLabel;
    public float hp = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        hp = Globals.doorDefaultHp;
        Globals.goldLabel = goldLabel;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            TakeDamage(other.GetComponent<EnemyMovement>().doorDamage);
            other.GetComponent<EnemyMovement>().Kill();
        }
    }
    private void TakeDamage(float damage)
    {
        hp -= damage;
        if (castleHpLabel) castleHpLabel.text = (hp / Globals.doorDefaultHp * 100).ToString() + "%"; 
        if (hp <= 0.0f) GameOver();
    }
    private void GameOver()
    {

    }
    private void Win() 
    {
        
    }
}
