using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour
{
    public ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ps && ps.isStopped) Destroy(gameObject);
    }
}
