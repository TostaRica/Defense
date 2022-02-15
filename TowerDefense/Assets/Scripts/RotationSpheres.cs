using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSpheres : MonoBehaviour
{
    public GameObject PivotSpheres;
    public GameObject monster;
    private bool bombs;
    private bool mudarmor;
    private bool zombie;

    // Start is called before the first frame update
    void Start()
    {
        zombie = monster.GetComponent<EnemyMovement>().
    }

    // Update is called once per frame
    void Update()
    {
        PivotSpheres.transform.Rotate(new Vector3(0, 0, 1)); 
    }
}
