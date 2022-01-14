using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent enemyAgent;
    public Transform castleDoor;
    public GameObject enemyModel;
    public GameObject mudArea;
    public GameObject poisonArea;
    public GameObject bombArea;
    public GameObject tombModel;
    public float castleDistanceRemaining { get { return GetPathRemainingDistance(enemyAgent); } }
    public float doorDamage { get { return attackDamage; } }
    public bool isZombie { get { return enemyStates.Contains(Globals.EnemyState.Zombie); } }
    public bool hasZombieUpgrade { get { return enemyUpgrades.Contains(Globals.EnemyUpgrade.Zombie); } }
    public float spawnWaitTime { get { return spawnTime; } }
    //stats
    private float hp = 10.0f;
    private float zombieHp = 10.0f;
    private float speed = 1.0f;
    private float attackDamage = 1.0f;
    private bool dashSkill = false;
    private bool dead = false;
    private float dashTimer = 0.0f;
    private float dashCooldoown = Globals.jumperDashCooldown;
    private List<Globals.EnemyUpgrade> enemyUpgrades = new List<Globals.EnemyUpgrade>();
    private List<Globals.EnemyState> enemyStates = new List<Globals.EnemyState>();

    //internal
    private int poisonDots = 0;
    private int burnDots = 0;
    private float dotTimer = 0.0f;
    private float deadTimer = 0.0f;
    private float spawnTime = 0.0f;
    void Start()
    {
        if (enemyUpgrades.Contains(Globals.EnemyUpgrade.Bomb))
        {
            speed *= Globals.bombSpeedIncrement;
        }
        if (enemyUpgrades.Contains(Globals.EnemyUpgrade.MudArmor))
        {
            hp += hp * Globals.mudArmor;
        }
        if (enemyUpgrades.Contains(Globals.EnemyUpgrade.Zombie)) 
        {
            hp = hp * Globals.zombieHpFactor;
            zombieHp = hp;
        }
        enemyAgent.SetDestination(castleDoor.position);
        enemyAgent.acceleration = 9999;
        enemyAgent.angularSpeed = 9999;
        enemyAgent.autoBraking = false;
    }
    void Update()
    {

        if (!dead)
        {
            DotDamage();
            if (hp <= 0.0f) Die();
            DashChecker();
        }
        else {
            if (deadTimer <= 0.0f)
            {
                Resurrect();
            }
            else {
                deadTimer -= Time.deltaTime;
            }

        }
    }
    public void Init(Globals.EnemyType type = Globals.EnemyType.Standard, bool bombs = false, bool mudArmor = false, bool zombie = false, Transform doorPosition = null, float waitTime = 0.0f)
    {
        if (bombs) enemyUpgrades.Add(Globals.EnemyUpgrade.Bomb);
        if (mudArmor) enemyUpgrades.Add(Globals.EnemyUpgrade.MudArmor);
        if (zombie) enemyUpgrades.Add(Globals.EnemyUpgrade.Zombie);
        if (doorPosition) castleDoor = doorPosition;
        switch (type)
        {
            case Globals.EnemyType.Jumper:
                hp = Globals.jumperDefaultHp;
                speed = Globals.jumperDefaultSpeed;
                attackDamage = Globals.jumperDefaultDoorDamage;
                dashSkill = true;
                break;
            case Globals.EnemyType.Standard:
                hp = Globals.standardDefaultHp;
                speed = Globals.standardDefaultSpeed;
                attackDamage = Globals.standardDefaultDoorDamage;
                break;
            case Globals.EnemyType.Heavy:
                hp = Globals.heavyDefaultHp;
                speed = Globals.heavyDefaultSpeed;
                attackDamage = Globals.standardDefaultDoorDamage;
                break;
        }
        enemyAgent.speed = speed;
        spawnTime = waitTime;
    }
    public void TakeDamage(float damage)
    {
        if(!enemyStates.Contains(Globals.EnemyState.Dashing)) hp -= damage;
        if (hp <= 0.0f) Die();
    }
    public void AddState(Globals.EnemyState state)
    {
        if (!enemyStates.Contains(state))
        {
            enemyStates.Add(state);
        }
        if (state == Globals.EnemyState.Poison) poisonDots = Globals.poisonDotsNumber;
        if (state == Globals.EnemyState.Burn) burnDots = Globals.burnDotsNumber;
    }
    public void Kill() {
        //Todo: kill animation
        gameObject.GetComponent<Animator>().Play("Dying");
        Globals.currentWaveEnemies.Remove(gameObject);
        Destroy(gameObject);
    }
    private void DotDamage()
    {
        if (enemyStates.Count > 0 && dotTimer <= 0.0f)
        {
            if (poisonDots > 0 && enemyStates.Contains(Globals.EnemyState.Poison))
            {
                poisonDots--;
                TakeDamage(Globals.poisonDamage);
            }
            if (burnDots > 0 && enemyStates.Contains(Globals.EnemyState.Burn))
            {
                burnDots--;
                TakeDamage(Globals.burnDamage);
            }
            dotTimer = Globals.dotTime;
        }
        else
        {
            dotTimer -= Time.deltaTime;
        }
    }
    private void Resurrect()
    {
        if (dead && !enemyStates.Contains(Globals.EnemyState.Zombie) && enemyUpgrades.Contains(Globals.EnemyUpgrade.Zombie)) {
            enemyStates.Add(Globals.EnemyState.Zombie);
            dead = false;
            hp = zombieHp * Globals.zombieHpFactor;
            tombModel.SetActive(false);
            mudArea.SetActive(false);
            enemyModel.SetActive(true);
            enemyAgent.enabled = true;
            enemyAgent.SetDestination(castleDoor.position);
        }
    }
    private void Die() 
    {
        if (!dead) dead = true;
        if (enemyAgent) enemyAgent.enabled = false;
        if (enemyModel) enemyModel.SetActive(false);
        if (tombModel && enemyUpgrades.Contains(Globals.EnemyUpgrade.Zombie)) 
        {
            tombModel.SetActive(true);
            deadTimer = Globals.mudAndDeadTime;
        }
        if (mudArea && enemyUpgrades.Contains(Globals.EnemyUpgrade.MudArmor))
        {
            mudArea.SetActive(true);
        }
        if (poisonArea && enemyStates.Contains(Globals.EnemyState.Poison))
        {
            poisonArea.SetActive(true);
        }
        if (!enemyStates.Contains(Globals.EnemyState.Poison) && !enemyUpgrades.Contains(Globals.EnemyUpgrade.MudArmor) && !enemyUpgrades.Contains(Globals.EnemyUpgrade.Zombie)) Kill();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MudArea"))
        {
            ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
            if (!enemyStates.Contains(Globals.EnemyState.Slow))
            {
                enemyAgent.speed = speed * Globals.mudSlowSpeed;
                enemyStates.Add(Globals.EnemyState.Slow);
            }
        }
        if (other.CompareTag("PoisonArea"))
        {
            ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
            if (!enemyStates.Contains(Globals.EnemyState.Poison))
            {
                enemyStates.Add(Globals.EnemyState.Poison);
            }
            if (poisonDots < Globals.poisonDotsNumber) 
            {
                poisonDots = Globals.poisonDotsNumber;
            }
        }
        if (other.CompareTag("FireArea"))
        {
            ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
            if (!enemyStates.Contains(Globals.EnemyState.Burn))
            {
                enemyStates.Add(Globals.EnemyState.Burn);
            }
            if (burnDots < Globals.burnDotsNumber)
            {
                burnDots = Globals.burnDotsNumber;
            }
        }
        if (other.CompareTag("BombTurretArea"))
        {
            TakeDamage(Globals.bombDamage);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MudArea"))
        {
            ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);
            enemyStates.Remove(Globals.EnemyState.Slow);
            if (!enemyStates.Contains(Globals.EnemyState.Slow))
            {
                enemyAgent.speed = speed;
            }
        }
    }
    private void Dash() 
    {
        enemyAgent.speed = 200;
        gameObject.GetComponent<Animator>().SetFloat("Velocity", enemyAgent.speed);
        enemyStates.Add(Globals.EnemyState.Dashing);
        dashTimer = Globals.jumperDefaultDashTime;
    }
    private void StopDash()
    {
        enemyAgent.speed = speed;
        gameObject.GetComponent<Animator>().SetFloat("Velocity", enemyAgent.speed);
        enemyStates.Remove(Globals.EnemyState.Dashing);
        dashTimer = 0.0f;
        dashCooldoown = Globals.jumperDashCooldown;
    }
    private void DashChecker() {
        if (dashSkill)
        {
            if (enemyStates.Contains(Globals.EnemyState.Dashing))
            {
                if (dashTimer <= 0.0f)
                {
                    StopDash();
                }
                else dashTimer -= Time.deltaTime;
            }
            else
            {
                if (dashCooldoown <= 0.0f)
                {
                    Dash();
                }
                else
                {
                    dashCooldoown -= Time.deltaTime;
                }
            }
        }
    }
    public float GetPathRemainingDistance(NavMeshAgent navMeshAgent)
    {
        if (navMeshAgent.pathPending ||
            navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid ||
            navMeshAgent.path.corners.Length == 0)
            return -1f;

        float distance = 0.0f;
        for (int i = 0; i < navMeshAgent.path.corners.Length - 1; ++i)
        {
            distance += Vector3.Distance(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
        }

        return distance;
    }
}
