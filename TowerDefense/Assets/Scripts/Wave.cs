using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public struct Enemy {
        public Globals.EnemyType enemyType;
        public bool bombUpgrade;
        public bool mudArmorUpgrade;
        public bool zombieUpgrade;
        public float waitTime;
        public Enemy (Globals.EnemyType _enemyType, bool _bombUpgrade, bool _mudArmorUpgrade, bool _zombieUpgrade , float _waitTime)
        {
            enemyType = _enemyType;
            bombUpgrade = _bombUpgrade;
            mudArmorUpgrade = _mudArmorUpgrade;
            zombieUpgrade = _zombieUpgrade;
            waitTime = _waitTime;
        }
    }
    public float waitTime = 0.0f;
    private int enemiesCount = 0;
    public int enemiesNumber { get { return enemiesCount; } }

    Queue<Enemy> enemies = new Queue<Enemy>();

    public void AddEnemy(Globals.EnemyType _enemyType = Globals.EnemyType.Standard, bool _bombUpgrade = false, bool _mudArmorUpgrade = false, bool _zombieUpgrade = false, float _waitTime = 0.0f) 
    {
        enemies.Enqueue(new Enemy(_enemyType, _bombUpgrade, _mudArmorUpgrade, _zombieUpgrade, _waitTime));
        ++enemiesCount;
    }
    public Enemy GetNextEnemy()
    {
        return enemies.Dequeue();
    }
}
