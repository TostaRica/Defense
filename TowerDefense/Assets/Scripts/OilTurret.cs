using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTurret : Turret
{
    public GameObject Fire;
    public float Duration;
    public float RestTimeDuration;
    public GameObject flare;
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
}
