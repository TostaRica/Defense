using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public enum TowerType
    {
        Basic = 0, Ballista = 1, Canon = 2, Caoldron = 3
    }
    public enum AimType { Area, Single, Donut }
    public enum Type { Fire, Poison, Neutral }
    public GameObject[] Turrets;
    public GameObject RedFlag;
    public GameObject GreenFlag;

    public ParticleSystem ChangeTowerEffect;
    public ParticleSystem GameOver;

    private int activeTower = 0;
    public Turret activeTurret = null;
    public int speedAttackLvl = 0;
    public int damageLvl = 0;
    public Turret turretScript { get { return Turrets[activeTower].GetComponent<Turret>(); } }
    public bool UpgradeTowerDamage()
    {
        bool upgraded = false;
        if (activeTurret) { 
            if (damageLvl < 3)
            {
                upgraded = true;
                damageLvl++;
                float defaultDamage = 0.0f;
                switch (activeTurret.towerType)
                {
                    case TowerManager.TowerType.Basic:
                        defaultDamage = Globals.defaultSimpleTowerDamage;
                        break;
                    case TowerManager.TowerType.Canon:
                        defaultDamage = Globals.defaultBomberTowerDamage;
                        break;
                    case TowerManager.TowerType.Ballista:
                        defaultDamage = Globals.defaultBallistaTowerDamage;
                        break;
                    case TowerManager.TowerType.Caoldron:
                        defaultDamage = Globals.defaultCauldronTowerDamage;
                        break;

                }
                activeTurret.Damage = defaultDamage + (defaultDamage * Globals.damageUpgradeRate * damageLvl);
            }
        }
        return upgraded;
    }
    public bool UpgradeTowerSpeed()
    {
        bool upgraded = false;
        if (activeTurret)
        {
            if (speedAttackLvl < 3)
            {
                upgraded = true;
                speedAttackLvl++;
                float defaultSpeed = 0.0f;
                switch (activeTurret.towerType)
                {
                    case TowerManager.TowerType.Basic:
                        defaultSpeed = Globals.defaultSimpleTowerAttackSpeed;
                        break;
                    case TowerManager.TowerType.Canon:
                        defaultSpeed = Globals.defaultBomberTowerAttackSpeed;
                        break;
                    case TowerManager.TowerType.Ballista:
                        defaultSpeed = Globals.defaultBallistaTowerAttackSpeed;
                        break;
                    case TowerManager.TowerType.Caoldron:
                        defaultSpeed = Globals.defaultCauldronTowerAttackSpeed;
                        break;

                }
                activeTurret.SpeedAttack = defaultSpeed - (defaultSpeed * Globals.speedUpgradeRate * speedAttackLvl);
            }
        }
        return upgraded;
    }
    public bool SetElement(Type element)
    {
        bool upgraded = false;
        if (activeTurret.type != element) {
            upgraded = true;
            activeTurret.type = element;
            if(element == Type.Fire)
            {
                RedFlag.SetActive(true);
                GreenFlag.SetActive(false);
            }else if(element == Type.Poison){
                RedFlag.SetActive(false);
                GreenFlag.SetActive(true);
            }
            else
            {
                RedFlag.SetActive(false);
                GreenFlag.SetActive(false);
            }
        }
        return upgraded;
    }
    public bool ChangeTower(TowerType type)
    {
        bool upgraded = false;
        if(activeTurret.GetTowerType() != type) {
            upgraded = true;
            ChangeTowerEffect.Play();
            for (int i = 0; i < Turrets.Length; i++)
            {
                if (i != (int)type)
                {
                    Turrets[i].SetActive(false);
                }
                else
                {
                    activeTower = i;
                    Turret newTurret = Turrets[activeTower].GetComponent<Turret>();
                    newTurret.Damage = activeTurret.Damage;
                    newTurret.SpeedAttack = activeTurret.SpeedAttack;
                    newTurret.type = activeTurret.type;
                    activeTurret = newTurret;
                    Turrets[i].SetActive(true);
                }
            }
        }
        return upgraded;
    }
    void Start()
    {
        if (Turrets[activeTower]) {
            activeTurret = Turrets[activeTower].GetComponent<Turret>();
            //Turrets[0].SetActive(true);
        }
       // DestoyTower();
    }
    public void DestoyTower()
    {
        GameOver.Play();
        Rigidbody[] rigidbodiesOfAllChild = this.gameObject.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rigidbodiesOfAllChild.Length; i++)
        {
            rigidbodiesOfAllChild[i].isKinematic = false;
        }
    }

   
}
