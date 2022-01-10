using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torret : MonoBehaviour
{

    enum Type {Fire, Posion, Neutral}
    enum AimType { Area, Single, Donut}

    Type type = Type.Neutral;
    AimType aimType = AimType.Single;


    private GameObject Target;
    public GameObject BulletSingle;
    public GameObject BulletArea;
    public GameObject BulletDonut;

    public List<Enemy> EnemiesInside = new List<Enemy>();
    private List<Enemy> EnemisToDelete = new List<Enemy>();
    public float SpeedAttack;
    public float RestTimeAttack;
    public float Offset;
    public float Damage;

    public float UpgradeNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            if (aimType != AimType.Donut) gameObject.transform.LookAt(Target.transform.position + Target.transform.forward * Offset);
            CheckColdDowns();
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
        float menor = 999;
        foreach (Enemy enemy in EnemiesInside)
        {
            if (enemy == null) EnemisToDelete.Add(enemy);
            else if (menor > enemy.EnemyPosition)
            {
                menor = enemy.EnemyPosition;
                Target = enemy.gameObject;
            }
        }
    }

    void Shoot()
    {
        GameObject b;
        switch (aimType)
        {
            case AimType.Area:
                b = Instantiate(BulletArea, transform.position, transform.rotation);
                break;
            case AimType.Donut:
                b = Instantiate(BulletDonut, transform.position, transform.rotation);
                break;
            default:
                b = Instantiate(BulletSingle, transform.position, transform.rotation);
                break;
        }
        b.GetComponent<Bullet>().Damage = Damage;
    }

   void CleanEnemyList()
    {
        foreach (Enemy enemy in EnemisToDelete)
        {
            EnemiesInside.Remove(enemy);
        }
        EnemisToDelete = new List<Enemy>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemiesInside.Remove(other.gameObject.GetComponent<Enemy>());
            if(Target == other.gameObject) FindNewEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemiesInside.Add(other.gameObject.GetComponent<Enemy>());    
        }
    }
    
    //Methods for Buttons & UI

    public void UpgradeTower()
    {
        UpgradeNumber++;
        SpeedAttack /= 2;
        Damage *= 2;
    }

    public void DownGradeTower()
    {
        UpgradeNumber--;
        SpeedAttack *= 2;
        Damage /= 2;
    }
}
