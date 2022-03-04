using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaExplosion : MonoBehaviour
{
    public float Live;
    public float Damage;
    public TowerManager.Type type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Live -= Time.deltaTime;
        if (Live <= 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyMovement>().TakeDamage(Damage);
            if (type == TowerManager.Type.Fire) other.gameObject.GetComponent<EnemyMovement>().AddState(Globals.EnemyState.Burn);
            if (type == TowerManager.Type.Poison) other.gameObject.GetComponent<EnemyMovement>().AddState(Globals.EnemyState.Poison);
            Destroy(gameObject);
        }
    }
}
