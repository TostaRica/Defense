using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBallesta : Turret
{

    public GameObject ShootEffect;
    public AudioSource ShootFX;

    public bool ShootFail = false;
    // Start is called before the first frame update
    public override TowerManager.TowerType GetTowerType()
    {
        return TowerManager.TowerType.Ballista;
    }
    void Start()
    {
        aimType = TowerManager.AimType.Single;
        towerType = TowerManager.TowerType.Ballista;
    }

    // Update is called once per frame
    void Update()
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
                  //  Base.GetComponent<Base>().Orientation(transform);
                }
            }
            if (!ShootFail) CheckColdDowns();
        }
        else
        {
            FindNewEnemy();
        }

        CleanEnemyList();

        //For Debug


    }

    void CheckColdDowns()
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

    void FindNewEnemy()
    {
        float menor = 99999999999;
        foreach (EnemyMovement enemy in EnemiesInside)
        {
            if (enemy == null || enemy.isDead) EnemisToDelete.Add(enemy);
            else if (menor > enemy.castleDistanceRemaining)
            {
                menor = enemy.castleDistanceRemaining;
                Target = enemy.gameObject;
            }
        }
    }

    void Shoot()
    {
        GameObject b;

        Instantiate(ShootEffect, Punta.transform.position, Punta.transform.rotation);
        b = Instantiate(Bullet, Punta.transform.position, Punta.transform.rotation);
        ShootFX.Play();
        b.GetComponent<Bullet>().Target = Target;
        b.GetComponent<Bullet>().Damage = Damage;
        b.GetComponent<Bullet>().Speed = BulletSpeed;
        b.GetComponent<Bullet>().SetBulletType((int)type);
    }

    void CleanEnemyList()
    {
        foreach (EnemyMovement enemy in EnemisToDelete)
        {
            EnemiesInside.Remove(enemy);
        }
        EnemisToDelete = new List<EnemyMovement>();
    }
}