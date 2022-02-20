using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSpheres : MonoBehaviour
{
    public GameObject PivotSpheres;
    public GameObject monster;
    public GameObject sphereMud;
    public GameObject sphereBomb;
    public GameObject sphereZombie;
    private bool bombs;
    private bool mudarmor;
    private bool zombie;

    // Start is called before the first frame update
    void Start()
    {
        zombie = monster.GetComponent<EnemyMovement>().hasZombiePower;
        mudarmor = monster.GetComponent<EnemyMovement>().hasMudPower;
        bombs = monster.GetComponent<EnemyMovement>().hasBombPower;

        if (zombie){
            sphereZombie.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            sphereZombie.GetComponent<MeshRenderer>().enabled = false;
        }
        if (mudarmor)
        {
            sphereMud.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            sphereMud.GetComponent<MeshRenderer>().enabled = false;
        }
        if (bombs)
        {
            sphereBomb.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            sphereBomb.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PivotSpheres.transform.Rotate(new Vector3(0, 0, 200*Time.deltaTime)); 
    }
}
