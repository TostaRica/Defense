using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObject[] Turrets;

    public ParticleSystem ChangeTowerEffect;
    // Start is called before the first frame update
    void Start()
    {
        ChangeTower(Random.Range(0,3));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a")) ChangeTower(0);
        if (Input.GetKeyDown("s")) ChangeTower(1);
        if (Input.GetKeyDown("d")) ChangeTower(2);
    }

    public void ChangeTower(int x)
    {
        ChangeTowerEffect.Play();
        for (int i = 0; i < Turrets.Length; i++)
        {
            if (i != x)
            {
                Turrets[i].SetActive(false);
            }
            else
            {
                Turrets[i].SetActive(true);
            }
        }
    }
}
