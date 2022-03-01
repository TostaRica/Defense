using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBallesta : Turret
{
    void Awake()
    {
        aimType = TowerManager.AimType.Single;
        towerType = TowerManager.TowerType.Ballista;
    }
}
