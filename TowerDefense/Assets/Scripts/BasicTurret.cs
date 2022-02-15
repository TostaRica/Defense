using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : Turret
{
    //public ParticleSystem ShootEffect;
    public override TowerManager.TowerType GetTowerType()
    {
        return TowerManager.TowerType.Basic;
    }
    // Start is called before the first frame update
    void Start()
    {
        towerType = TowerManager.TowerType.Basic;
        type = TowerManager.Type.Neutral;
        aimType = TowerManager.AimType.Single;
    }
    // Update is called once per frame
}
