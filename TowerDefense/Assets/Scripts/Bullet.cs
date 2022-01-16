using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum Type { Fire, Posion, Neutral }
    public enum AimType { Area, Single, Donut }

    public Type type = Type.Neutral;
    public AimType aimType = AimType.Single;

    public float Speed;
    public float Live;
    public float Damage;

    public GameObject Area;
    public GameObject HitEffect;
    public GameObject GroundHitEffect;
    public GameObject PosionEffect;
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

            if (type == Type.Posion)
            {
                if (aimType == AimType.Area) Instantiate(PosionEffect, transform.position, PosionEffect.transform.rotation);
            }
            else
            {
                Instantiate(HitEffect, transform.position, transform.rotation);
            }
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

    public void SetAimType(int x)
    {
        switch (x)
        {
            case 0:
                aimType = AimType.Area;
                break;
            case 1:
                aimType = AimType.Single;
                break;
            default:
                aimType = AimType.Donut;
                break;
        }
    }
}
