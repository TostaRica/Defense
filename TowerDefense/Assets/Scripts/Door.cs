using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Door : MonoBehaviour
{
    public Text castleHpLabel;
    public Text goldLabel;
    // Start is called before the first frame update
    void Start()
    {
        Globals.doorCurrentHp = Globals.doorDefaultHp;
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
        Globals.doorCurrentHp -= damage;
        if (castleHpLabel) castleHpLabel.text = (Globals.doorCurrentHp / Globals.doorDefaultHp * 100).ToString() + "%"; 
    }
}
