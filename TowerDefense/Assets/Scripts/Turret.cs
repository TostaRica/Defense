using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float SpeedAttack;
    public float Damage;
    public float RestTimeAttack;
    public float Offset;
    public float BulletSpeed = 4.0f;
    public TowerManager.Type type = TowerManager.Type.Neutral;
    public TowerManager.AimType aimType = TowerManager.AimType.Single;
    public TowerManager.TowerType towerType = TowerManager.TowerType.Basic; 
    public List<EnemyMovement> EnemiesInside = new List<EnemyMovement>();
    protected List<EnemyMovement> EnemisToDelete = new List<EnemyMovement>();
    public GameObject ShootEffect;
    public AudioSource ShootFX;

    public BaseTorret RangeZone;
    public GameObject Target;
    public GameObject Bullet;
    public GameObject Punta;
    public GameObject Weapon;
    public GameObject Base;

    public bool ShootFail = false;
    public virtual void Update()
    {
        if (Target != null)
        {
            ShootFail = false;
            if (aimType != TowerManager.AimType.Donut)
            {
                Vector3 displacement = Target.transform.position - Weapon.transform.position;
                float targetMoveAngle = Vector3.Angle(-displacement, Target.GetComponent<EnemyMovement>().enemyAgent.velocity) * Mathf.Deg2Rad;
                //if the target is stopping or if it is impossible for the projectile to catch up with the target (Sine Formula)
                if (Target.GetComponent<EnemyMovement>().enemyAgent.velocity.magnitude == 0 ||
                    Target.GetComponent<EnemyMovement>().enemyAgent.velocity.magnitude > BulletSpeed && Mathf.Sin(targetMoveAngle) / BulletSpeed > Mathf.Cos(targetMoveAngle) / Target.GetComponent<EnemyMovement>().enemyAgent.velocity.magnitude)
                {
                    FindNewEnemy();
                    ShootFail = true;
                }
                //also Sine Formula
                float shootAngle = Mathf.Asin(Mathf.Sin(targetMoveAngle) * Target.GetComponent<EnemyMovement>().enemyAgent.velocity.magnitude / BulletSpeed);
                // return targetPosition + targetVelocity * displacement.magnitude / Mathf.Sin(Mathf.PI - targetMoveAngle - shootAngle) * Mathf.Sin(shootAngle) / targetVelocity.magnitude;
                Vector3 aux = Target.transform.position + Target.GetComponent<EnemyMovement>().enemyAgent.velocity * displacement.magnitude / Mathf.Sin(Mathf.PI - targetMoveAngle - shootAngle) * Mathf.Sin(shootAngle) / Target.GetComponent<EnemyMovement>().enemyAgent.velocity.magnitude;

                if (!ShootFail)
                {
                    Weapon.transform.LookAt(aux);
                    if(Base.GetComponent<Base>() != null)Base.GetComponent<Base>().Orientation(transform);
                }
            }
            if (!ShootFail) CheckColdDowns();
        }
        else
        {
            FindNewEnemy();
        }

        CleanEnemyList();
    } 
    public void FindNewEnemy()
    { 
        float menor = 99999999999;
        foreach (EnemyMovement enemy in EnemiesInside)
        {
            if (enemy == null) EnemisToDelete.Add(enemy);
            else if (menor > enemy.castleDistanceRemaining && !enemy.isDead)
            {
                menor = enemy.castleDistanceRemaining;
                Target = enemy.gameObject;
            }
        }
    }
    public virtual void CleanEnemyList()
    {
        foreach (EnemyMovement enemy in EnemisToDelete)
        {
            EnemiesInside.Remove(enemy);
        }
        EnemisToDelete = new List<EnemyMovement>();
    }
    public virtual void CheckColdDowns()
    {
        RestTimeAttack -= Time.deltaTime;
        if (RestTimeAttack <= 0)
        {
            RestTimeAttack = SpeedAttack;
            if (Target != null && !Target.GetComponent<EnemyMovement>().isDead)
            {
                Shoot();
            }
            else
            {
                FindNewEnemy();
            }
        }
    }
    public virtual void Shoot()
    {
        GameObject b;

        if (ShootEffect != null)  Instantiate(ShootEffect, Punta.transform.position, Punta.transform.rotation);
        b = Instantiate(Bullet, Punta.transform.position, Punta.transform.rotation);
        ShootFX.Play();
        b.GetComponent<Bullet>().Target = Target;
        b.GetComponent<Bullet>().Damage = Damage;
        b.GetComponent<Bullet>().Speed = BulletSpeed;
        b.GetComponent<Bullet>().aimType = aimType;
        b.GetComponent<Bullet>().SetBulletType((int)type);
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
    public void ShowRangeZone()
    {
        Base.GetComponent<MeshRenderer>().enabled = true;
    }
    public void HideRangeZone()
    {
        Base.GetComponent<MeshRenderer>().enabled = false;
    }
    public virtual TowerManager.TowerType GetTowerType() {
        return this.towerType;
    }
    public virtual void ShowBase()
    {
        Base.GetComponent<MeshRenderer>().enabled = true;
    }
    public virtual void HideBase()
    {
        Base.GetComponent<MeshRenderer>().enabled = false;
    }

}
