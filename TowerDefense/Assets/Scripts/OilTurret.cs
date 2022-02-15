using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTurret : Turret
{
    public GameObject Fire;
    public float Duration;
    public float RestTimeDuration;
    public override TowerManager.TowerType GetTowerType()
    {
        return TowerManager.TowerType.Caoldron;
    }
    // Start is called before the first frame update
    void Start()
    {
        towerType = TowerManager.TowerType.Caoldron;
    }

    // Update is called once per frame
    public override void Update()
    {
        CheckColdDowns();
        if (RestTimeDuration > 0)
        {
            RestTimeDuration -= Time.deltaTime;
        }
        else
        {
            DisableFire();
        }
        CleanEnemyList();
    }

    public override void CheckColdDowns()
    {
        RestTimeAttack -= Time.deltaTime;
        if (RestTimeAttack <= 0)
        {
            RestTimeAttack = SpeedAttack;
            Shoot();
        }
    }

    void ActiveFire()
    {
        Fire.SetActive(true);
    }
    void DisableFire()
    {
        Fire.SetActive(false);
    }

    public override void Shoot()
    {
        ActiveFire();
        foreach (EnemyMovement enemy in EnemiesInside)
        {
            if (enemy == null || enemy.isDead) EnemisToDelete.Add(enemy);
            enemy.GetComponent<EnemyMovement>().TakeDamage(Damage);
        }

        ParticleSystem.MainModule settings;
        ParticleSystem[] rigidbodiesOfAllChild = this.gameObject.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < rigidbodiesOfAllChild.Length; i++)
        {
            if (type == TowerManager.Type.Poison)
            {
                settings = rigidbodiesOfAllChild[i].main;
                settings.startColor = Color.green;
            }
            else
            {
                settings = rigidbodiesOfAllChild[i].main;
                settings.startColor = Color.white;
            }
           rigidbodiesOfAllChild[i].Play();
        }
    }

    public override void CleanEnemyList()
    {
        foreach (EnemyMovement enemy in EnemiesInside)
        {
            if (enemy == null || enemy.isDead) EnemisToDelete.Add(enemy);
        }

        foreach (EnemyMovement enemy in EnemisToDelete)
        {
            EnemiesInside.Remove(enemy);
        }
        EnemisToDelete = new List<EnemyMovement>();
    }
}
