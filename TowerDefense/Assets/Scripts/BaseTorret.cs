using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTorret : MonoBehaviour
{
    public bool AreaDamage;
    public GameObject Torret;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeZone() {
        transform.localScale *= 2;
    }

    public void DowngradeZone()
    {
        transform.localScale /= 2;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Torret.GetComponent<Turret>().RemoveEnemy(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Torret.GetComponent<Turret>().AddEnemy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (AreaDamage)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyMovement>().TakeDamage(Torret.GetComponent<Turret>().Damage);
                if (Torret.GetComponent<Turret>().type == TowerManager.Type.Fire) other.gameObject.GetComponent<EnemyMovement>().AddState(Globals.EnemyState.Burn);
                if (Torret.GetComponent<Turret>().type == TowerManager.Type.Poison) other.gameObject.GetComponent<EnemyMovement>().AddState(Globals.EnemyState.Poison);
            }
        }
    }
}
