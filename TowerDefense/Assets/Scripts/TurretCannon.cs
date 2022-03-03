using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCannon : Turret
{
    void Awake()
    {
        aimType = TowerManager.AimType.Area;
        towerType = TowerManager.TowerType.Canon;
    }

}