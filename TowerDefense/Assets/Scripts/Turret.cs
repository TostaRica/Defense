using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public enum Type { Fire, Posion, Neutral }

    public float SpeedAttack;

    public float Damage;
    public float RestTimeAttack;
    public float Offset;
    public float BulletSpeed = 4.0f;
    public Type type = Type.Neutral;
    public TowerManager.AimType aimType = TowerManager.AimType.Single;
    public TowerManager.TowerType towerType = TowerManager.TowerType.Basic; 
    public List<EnemyMovement> EnemiesInside = new List<EnemyMovement>();
    protected List<EnemyMovement> EnemisToDelete = new List<EnemyMovement>();

    public BaseTorret RangeZone;
    public GameObject Target;
    public GameObject Bullet;
    public GameObject Punta;
    public GameObject Base;

    public void AddEnemy(GameObject e)
    {
        EnemiesInside.Add(e.GetComponent<EnemyMovement>());
    }
    public void RemoveEnemy(GameObject e)
    {
        if (Target == e) Target = null;
        EnemiesInside.Remove(e.GetComponent<EnemyMovement>());
    }
    public void ShowRangeZone()
    {
        Base.GetComponent<MeshRenderer>().enabled = true;
    }
    public void HideRangeZone()
    {
        Base.GetComponent<MeshRenderer>().enabled = false;
    }
}
