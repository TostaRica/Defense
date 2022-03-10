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
    private bool loseAnimationOn = false;
    public Turret activeTurret = null;
    public int speedAttackLvl = 0;
    public int damageLvl = 0;
    public TowerType towerType { get { return (activeTurret) ? activeTurret.GetTowerType() : TowerType.Basic; } }
    public Type towerElement { get { return (activeTurret) ? activeTurret.towerElement : Type.Neutral;} }
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
                    newTurret.Damage = CalculateDamage(type);
                    newTurret.SpeedAttack = CalculateSpeed(type);
                    newTurret.type = activeTurret.type;
                    activeTurret = newTurret;
                    Turrets[i].SetActive(true);
                }
            }
        }
        return upgraded;
    }
    float CalculateDamage(TowerType towerType) {
        float damage = 0.0f;
        switch (towerType) {
            case TowerType.Basic:
                damage = Globals.defaultSimpleTowerDamage + (Globals.defaultSimpleTowerDamage * Globals.damageUpgradeRate * damageLvl);
                break;
            case TowerType.Ballista:
                damage = Globals.defaultBallistaTowerDamage  + (Globals.defaultBallistaTowerDamage * Globals.damageUpgradeRate * damageLvl);
                break;
            case TowerType.Canon:
                damage = Globals.defaultBomberTowerDamage + (Globals.defaultBomberTowerDamage * Globals.damageUpgradeRate * damageLvl);
                break;
            case TowerType.Caoldron:
                damage = Globals.defaultCauldronTowerDamage + (Globals.defaultCauldronTowerDamage * Globals.damageUpgradeRate * damageLvl);
                break;
        }
        return damage;
    }
    float CalculateSpeed(TowerType towerType)
    {
        float speed = 0.0f;
        switch (towerType)
        {
            case TowerType.Basic:
                speed = Globals.defaultSimpleTowerAttackSpeed - (Globals.defaultSimpleTowerAttackSpeed * Globals.speedUpgradeRate * speedAttackLvl);
                break;
            case TowerType.Ballista:
                speed = Globals.defaultBallistaTowerAttackSpeed - (Globals.defaultBallistaTowerAttackSpeed * Globals.speedUpgradeRate * speedAttackLvl);
                break;
            case TowerType.Canon:
                speed = Globals.defaultBomberTowerAttackSpeed - (Globals.defaultBomberTowerAttackSpeed * Globals.speedUpgradeRate * speedAttackLvl);
                break;
            case TowerType.Caoldron:
                speed = Globals.defaultCauldronTowerAttackSpeed - (Globals.defaultCauldronTowerAttackSpeed * Globals.speedUpgradeRate * speedAttackLvl);
                break;
        }
        return speed;
    }
    void Start()
    {
        if (Turrets[activeTower]) {
            activeTurret = Turrets[activeTower].GetComponent<Turret>();
            activeTurret.Damage = Globals.defaultSimpleTowerDamage + (Globals.defaultSimpleTowerDamage * Globals.damageUpgradeRate * damageLvl);
            activeTurret.SpeedAttack = Globals.defaultSimpleTowerAttackSpeed - (Globals.defaultSimpleTowerAttackSpeed * Globals.speedUpgradeRate * speedAttackLvl);
        }
       // DestoyTower();
    }
    private void Update()
    {
        if (Globals.doorCurrentHp <= 0.0f && !loseAnimationOn) DestroyTower();
    }
    public void DestroyTower()
    {
        loseAnimationOn = true;
        GameOver.Play();
        Rigidbody[] rigidbodiesOfAllChild = this.gameObject.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rigidbodiesOfAllChild.Length; i++)
        {
            rigidbodiesOfAllChild[i].isKinematic = false;
        }
    }

   
}
