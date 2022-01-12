using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //Dots
    public static float poisonDamage = 5.0f;
    public static int poisonDotsNumber = 5;
    public static float burnDamage = 5.0f;
    public static int burnDotsNumber = 5;
    //Enemy upgrades
    public static float mudArmor = 0.25f; // +%
    public static float dotTime = 1.0f; // Seconds
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
    public static List<GameObject> enemies = new List<GameObject>(); 
    //Door 
    public static float doorDefaultHp = 100.0f;
}
