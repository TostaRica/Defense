using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTurret : Turret
{
    public GameObject Fire;
    public float Duration;
    public float RestTimeDuration;
    public GameObject flare;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
    }

    void CheckColdDowns()
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
        flare.SetActive(true);
    }
    void DisableFire()
    {
        Fire.SetActive(false);
        flare.SetActive(false);
    }

    void Shoot()
    {
        ActiveFire();
        RestTimeDuration = Duration;
        ParticleSystem[] rigidbodiesOfAllChild = this.gameObject.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < rigidbodiesOfAllChild.Length; i++)
        {
            rigidbodiesOfAllChild[i].Play();
        }
    }
}
