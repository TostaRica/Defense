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
    private float speed = 1.0f;
    private int damage = 1;
    private Stack<Globals.EnemyUpgrade> EnemyUpgrades = new Stack<Globals.EnemyUpgrade>();
    private Stack<Globals.EnemyState> EnemyStates = new Stack<Globals.EnemyState>();
    private bool dead = false;
    //internal
    private int poisonDots = 0;
    private int burnDots = 0;
    private float dotTimer = 0.0f;
    private float deadTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        EnemyUpgrades.Push(Globals.EnemyUpgrade.MudArmor);
        EnemyUpgrades.Push(Globals.EnemyUpgrade.Zombie);
        if (EnemyUpgrades.Contains(Globals.EnemyUpgrade.MudArmor))
        {
            hp += hp * Globals.mudArmor;
        }
        if (EnemyUpgrades.Contains(Globals.EnemyUpgrade.Zombie)) 
        {
            hp = hp * Globals.zombieHpFactor;
        }
        enemyAgent.SetDestination(castleDoor.position);
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
            // //TODO: esto lo llamara el script del mud
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
    }
    public void AddState(Globals.EnemyState state)
    {
        if (!EnemyStates.Contains(state))
        {
            EnemyStates.Push(state);
        }
        if (state == Globals.EnemyState.Poison) poisonDots = Globals.poisonDotsNumber;
        if (state == Globals.EnemyState.Burn) burnDots = Globals.burnDotsNumber;
    }
    public void DotDamage()
    {
        if (EnemyStates.Count > 0 && dotTimer <= 0.0f)
        {
            if (EnemyStates.Contains(Globals.EnemyState.Poison))
            {
                poisonDots--;
                hp -= Globals.poisonDamage;
            }
            if (EnemyStates.Contains(Globals.EnemyState.Burn))
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
        if (dead && !EnemyStates.Contains(Globals.EnemyState.Zombie)) {
            EnemyStates.Push(Globals.EnemyState.Zombie);
            dead = false;
            // //TODO:la vida se seteara desde un script global con los valores por defecto
            hp = 10 * Globals.zombieHpFactor;
            tombModel.SetActive(false);
            mudArea.SetActive(false);
            enemyModel.SetActive(true);
            enemyAgent.isStopped = false;
        }
    }
    private void Die() 
    {
        if (!dead) dead = true;
        enemyAgent.isStopped = true;
        enemyModel.SetActive(false);
        if (EnemyUpgrades.Contains(Globals.EnemyUpgrade.Zombie)) 
        {
            tombModel.SetActive(true);
            deadTimer = 3.0f;
        }
        if (EnemyUpgrades.Contains(Globals.EnemyUpgrade.MudArmor))
        {
            mudArea.SetActive(true);
        }
    }
    //TODO: esto lo llamara el projectil
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile")) 
        {
            TakeDamage(5.0f);
            other.gameObject.SetActive(false);
            AddState(Globals.EnemyState.Poison);
        }
    }

}
