using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBallesta : Turret
{


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
}
