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
            other.gameObject.GetComponent<Enemy>().GetHit(Damage);
            Destroy(this.gameObject);
            if(aimType == AimType.Area) Instantiate(Area, transform.position, transform.rotation);
        }
    }
}
