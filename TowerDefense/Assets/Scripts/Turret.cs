using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public enum Type { Fire, Posion, Neutral }
    public enum  AimType { Area, Single, Donut }

    public float SpeedAttack;
    public float RestTimeAttack;
    public float Offset;
    public float Damage;
    public float BulletSpeed = 4.0f;
    public float UpgradeNumber = 0;

    public List<EnemyMovement> EnemiesInside = new List<EnemyMovement>();
    protected List<EnemyMovement> EnemisToDelete = new List<EnemyMovement>();


    public BaseTorret RangeZone;
    public GameObject Target;
    public GameObject Bullet;
    public GameObject Punta;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void AddEnemy(GameObject e)
    {
        EnemiesInside.Add(e.GetComponent<EnemyMovement>());
    }

    public void RemoveEnemy(GameObject e)
    {
        if (Target == e) Target = null;
        EnemiesInside.Remove(e.GetComponent<EnemyMovement>());
    }

   

}
