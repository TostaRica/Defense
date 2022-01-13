using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torret : MonoBehaviour
{

    enum Type {Fire, Posion, Neutral}
    enum AimType { Area, Single, Donut}

    Type type = Type.Neutral;
    AimType aimType = AimType.Area;

    public ParticleSystem ShootSingleEffect;
    public ParticleSystem ShootAreaEffect;

    private GameObject Target;
    public GameObject Canon;
    public GameObject BulletSingle;
    public GameObject BulletArea;
    public GameObject BulletDonut;
    public GameObject Punta;
    public GameObject Base;
    public BaseTorret RangeZone;

    public AudioSource ShootFX;

    public List<EnemyMovement> EnemiesInside = new List<EnemyMovement>();
    private List<EnemyMovement> EnemisToDelete = new List<EnemyMovement>();
    public float SpeedAttack;
    public float RestTimeAttack;
    public float Offset;
    public float Damage;
    public float BulletSpeed = 4.0f; 
    public float UpgradeNumber = 0;
    public bool ShootFail = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            ShootFail = false;
            if (aimType != AimType.Donut)
            {
                Vector3 displacement = Target.transform.position - transform.position;
                float targetMoveAngle = Vector3.Angle(-displacement, Target.GetComponent<EnemyMovement>().enemyAgent.velocity) * Mathf.Deg2Rad;
                //if the target is stopping or if it is impossible for the projectile to catch up with the target (Sine Formula)
                if (Target.GetComponent<EnemyMovement>().enemyAgent.velocity.magnitude == 0 || 
                    Target.GetComponent<EnemyMovement>().enemyAgent.velocity.magnitude > BulletSpeed && Mathf.Sin(targetMoveAngle) / BulletSpeed > Mathf.Cos(targetMoveAngle) / Target.GetComponent<EnemyMovement>().enemyAgent.velocity.magnitude)
                {
                    Debug.Log("No le doy bro");
                    FindNewEnemy();
                    ShootFail = true;
                }
                //also Sine Formula
                float shootAngle = Mathf.Asin(Mathf.Sin(targetMoveAngle) * Target.GetComponent<EnemyMovement>().enemyAgent.velocity.magnitude / BulletSpeed);
                // return targetPosition + targetVelocity * displacement.magnitude / Mathf.Sin(Mathf.PI - targetMoveAngle - shootAngle) * Mathf.Sin(shootAngle) / targetVelocity.magnitude;
                Vector3 aux = Target.transform.position + Target.GetComponent<EnemyMovement>().enemyAgent.velocity * displacement.magnitude / Mathf.Sin(Mathf.PI - targetMoveAngle - shootAngle) * Mathf.Sin(shootAngle) / Target.GetComponent<EnemyMovement>().enemyAgent.velocity.magnitude;

                if (!ShootFail)
                {
                    gameObject.transform.LookAt(aux);
                    Base.GetComponent<Base>().Orientation(transform);
                }
            }
           if(!ShootFail) CheckColdDowns();
        }
        else
        {
            FindNewEnemy();
        }

        CleanEnemyList();

        //For Debug
        if (Input.GetKeyDown("w")) UpgradeTower();
        if (Input.GetKeyDown("s")) DownGradeTower();

    }

    void CheckColdDowns()
    {
        RestTimeAttack -= Time.deltaTime;
        if (RestTimeAttack <= 0)
        {
            RestTimeAttack = SpeedAttack;
            if (Target != null)
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
            if (enemy == null) EnemisToDelete.Add(enemy);
            else if (menor > enemy.castleDistanceRemaining)
            {
                menor = enemy.castleDistanceRemaining;
                Target = enemy.gameObject;
                Canon.GetComponent<Base>().SetTarget(enemy);
            }
        }
    }

    void Shoot()
    {
        GameObject b;
        switch (aimType)
        {
            case AimType.Area:
                ShootAreaEffect.Play();
                b = Instantiate(BulletArea, Punta.transform.position, Punta.transform.rotation);
                break;
            case AimType.Donut:
                b = Instantiate(BulletDonut, Punta.transform.position, Punta.transform.rotation);
                break;
            default:
                ShootSingleEffect.Play();
                b = Instantiate(BulletSingle, Punta.transform.position, Punta.transform.rotation);
                break;
        }

        ShootFX.Play();
        b.GetComponent<Bullet>().Damage = Damage;
        b.GetComponent<Bullet>().Speed = BulletSpeed;
    }

   void CleanEnemyList()
    {
        foreach (EnemyMovement enemy in EnemisToDelete)
        {
            EnemiesInside.Remove(enemy);
        }
        EnemisToDelete = new List<EnemyMovement>();
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
    //Methods for Buttons & UI

    public void UpgradeTower()
    {
        RangeZone.UpgradeZone();
        UpgradeNumber++;
        SpeedAttack /= 2;
        Damage *= 2;
    }

    public void DownGradeTower()
    {
        RangeZone.DowngradeZone();
        UpgradeNumber--;
        SpeedAttack *= 2;
        Damage /= 2;
    }
}
