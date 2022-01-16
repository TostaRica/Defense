using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    enum Type { Fire, Posion, Neutral }
    enum AimType { Area, Single, Donut }

    Type type = Type.Neutral;
    AimType aimType = AimType.Single;

    public float Speed;
    public float Live;
    public float Damage;

    public GameObject Area;
    public GameObject HitEffect;
    public GameObject GroundHitEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * Speed;

        Live -= Time.deltaTime;
        if (Live <= 0) Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
           other.gameObject.GetComponent<EnemyMovement>().TakeDamage(Damage);
           if (type == Type.Posion) other.gameObject.GetComponent<EnemyMovement>().AddState(Globals.EnemyState.Poison);
           if (type == Type.Fire) other.gameObject.GetComponent<EnemyMovement>().AddState(Globals.EnemyState.Burn);
            if (aimType == AimType.Area)
            {
                GameObject areaGo = Instantiate(Area, transform.position, transform.rotation);
                areaGo.GetComponent<AreaExplosion>().type = (TowerManager.Type)(int)type;
                areaGo.GetComponent<AreaExplosion>().Damage = Damage;
            }
           Instantiate(HitEffect, transform.position, transform.rotation);
           Destroy(this.gameObject);
        }
        else
        {
           // Instantiate(GroundHitEffect, transform.position, transform.rotation);
        }
      
    }


    public void SetBulletType(int x)
    {
        switch (x)
        {
            case 0:
                type = Type.Fire;
                break;
            case 1:
                type = Type.Posion;
                break;
            default:
                type = Type.Neutral;
                break;
        }
    }
}
