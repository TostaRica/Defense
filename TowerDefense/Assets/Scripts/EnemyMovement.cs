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
    public GameObject posionArea;
    public GameObject tombModel;

    //stats
    private float hp = 10.0f;
    private float zombieHp = 10.0f;
    private float speed = 1.0f;
    private float doorDamage = 1.0f;
    private bool jumpSkill = false;
    private bool dead = false;
    private List<Globals.EnemyUpgrade> enemyUpgrades = new List<Globals.EnemyUpgrade>();
    private List<Globals.EnemyState> enemyStates = new List<Globals.EnemyState>();
    //internal
    private int poisonDots = 0;
    private int burnDots = 0;
    private float dotTimer = 0.0f;
    private float deadTimer = 0.0f;
    private float castleDistanceRemaining { get { return enemyAgent.remainingDistance; } }
    // Start is called before the first frame update
    void Start()
    {
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
    public void Init(Globals.EnemyType type = Globals.EnemyType.Standard, bool bombs = false, bool mudArmor = false, bool zombie = false) 
    {
        if (bombs) enemyUpgrades.Add(Globals.EnemyUpgrade.Bomb);
        if (mudArmor) enemyUpgrades.Add(Globals.EnemyUpgrade.MudArmor);
        if (zombie) enemyUpgrades.Add(Globals.EnemyUpgrade.Zombie);

        switch (type) {
            case Globals.EnemyType.Jumper:
                hp = Globals.jumperDefaultHp;
                speed = Globals.jumperDefaultSpeed;
                doorDamage = Globals.jumperDefaultDoorDamage;
                jumpSkill = true;
                break;
            case Globals.EnemyType.Standard:
                hp = Globals.standardDefaultHp;
                speed = Globals.standardDefaultSpeed;
                doorDamage = Globals.standardDefaultDoorDamage;
                break;
            case Globals.EnemyType.Heavy:
                hp = Globals.heavyDefaultHp;
                speed = Globals.heavyDefaultSpeed;
                doorDamage = Globals.standardDefaultDoorDamage;
                break;
        }
        enemyAgent.speed = speed;
    }
    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            DotDamage();
            if (hp <= 0.0f) Die();

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
    public void TakeDamage(float damage)
    {
        hp -= damage;
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
    public void DotDamage()
    {
        if (enemyStates.Count > 0 && dotTimer <= 0.0f)
        {
            if (enemyStates.Contains(Globals.EnemyState.Poison))
            {
                poisonDots--;
                hp -= Globals.poisonDamage;
            }
            if (enemyStates.Contains(Globals.EnemyState.Burn))
            {
                burnDots--;
                hp -= Globals.burnDamage;
            }
            dotTimer = Globals.dotTime;
        }
        else
        {
            dotTimer -= Time.deltaTime;
        }
    }
    public void Resurrect()
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
        enemyAgent.enabled = false;
        enemyModel.SetActive(false);
        if (enemyUpgrades.Contains(Globals.EnemyUpgrade.Zombie)) 
        {
            tombModel.SetActive(true);
            deadTimer = Globals.mudAndDeadTime;
        }
        if (enemyUpgrades.Contains(Globals.EnemyUpgrade.MudArmor))
        {
            mudArea.SetActive(true);
        }
    }
    //TODO: esto lo llamara el projectil
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile")) 
        {
            Debug.Log("pre damage hp: " + hp);
            TakeDamage(100.0f);
            Debug.Log("post damage hp: " + hp);
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("MudArea"))
        {
            ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
            if (!enemyStates.Contains(Globals.EnemyState.Slow))
            {
                enemyAgent.speed = speed * Globals.mudSlowSpeed;
                enemyStates.Add(Globals.EnemyState.Slow);
            }
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
}
