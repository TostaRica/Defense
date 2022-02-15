using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCannon : Turret
{

    // Start is called before the first frame update
    public override TowerManager.TowerType GetTowerType()
    {
        return TowerManager.TowerType.Canon;
    }
    void Start()
    {
        aimType = TowerManager.AimType.Area;
        towerType = TowerManager.TowerType.Canon;
    }

}