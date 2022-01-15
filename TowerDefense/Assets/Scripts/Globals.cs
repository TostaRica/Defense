using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public static class Globals
{
    public enum EnemyState
    {
        Poison, Burn, Slow, Zombie, Dashing
    };
    public enum EnemyUpgrade
    {
        Bomb, MudArmor, Zombie
    };
    public enum EnemyType
    {
        Jumper, Standard, Heavy
    };
    //Money
    private static float money = 0.0f;
    //Pause
    public static bool isGamePaused = false;
    //Towers
    public static float defaultSimpleTowerDamage = 5.0f;
    public static float defaultSimpleTowerAttackSpeed = 1.0f;
    public static float defaultBallistaTowerDamage = 1.0f;
    public static float defaultBallistaTowerAttackSpeed = 1.0f;
    public static float defaultBomberTowerDamage = 1.0f;
    public static float defaultBomberTowerAttackSpeed = 1.0f;
    public static float defaultCauldronTowerDamage = 1.0f;
    public static float defaultCauldronTowerAttackSpeed = 1.0f;
    public static float damageUpgradeRate = 0.2f;
    public static float speedUpgradeRate = 0.2f;
    public static int numberOfTowers = 0;
    private static int defaultTowerCost = 200;
    private static int defaultTowerAddCost = 50;
    public static int towerCost { get { return defaultTowerCost + (numberOfTowers * defaultTowerAddCost); } }
    //Dots
    public static float dotTime = 1.0f; // Seconds
    public static float poisonDamage = 5.0f;
    public static int poisonDotsNumber = 5;
    public static float burnDamage = 5.0f;
    public static int burnDotsNumber = 5;
    //Enemy upgrades
    public static float bombSpeedIncrement = 0.25f;
    public static float bombDamage = 3.0f;
    public static float mudArmor = 0.25f; // +%
    public static float zombieHpFactor = 0.6f; // %
    //Enemy jumper
    public static float jumperDefaultSpeed = 20.0f;
    public static float jumperDefaultHp = 10.0f;
    public static float jumperDefaultDoorDamage = 1.0f;
    public static float jumperDefaultDashSpeed = 300.0f;
    public static float jumperDefaultDashTime = 0.5f;
    public static float jumperDashCooldown = 5.0f;
    //Enemy standard
    public static float standardDefaultSpeed = 20.0f;
    public static float standardDefaultHp = 10.0f;
    public static float standardDefaultDoorDamage = 1.0f;
    //Enemy heavy
    public static float heavyDefaultSpeed = 20.0f;
    public static float heavyDefaultHp = 10.0f;
    public static float heavyDefaultDoorDamage = 1.0f;
    //Areas
    public static float mudAndDeadTime = 2.0f;
    public static float mudSlowSpeed = 0.25f;
    public static float poisonTime = 0.5f;
    //Waves
    public static Queue<Wave> waves = new Queue<Wave>();
    public static Wave currentWave = null;
    public static Queue<GameObject> currentWaveWaitingEnemies = new Queue<GameObject>();
    public static List<GameObject> currentWaveEnemies = new List<GameObject>();
    //Door 
    public static float doorDefaultHp = 100.0f;
    public static Text goldLabel;
    public static void updateMoney(float amount) {
        money += amount;
        if(goldLabel) goldLabel.text = money.ToString();
    }
    public static float getMoney()
    {
        return money;
    }
    public static bool IsPointOverUIObject() 
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;

    }
}
