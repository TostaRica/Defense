using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public EnemyMovement Target;

    public float Offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null) return;
        Vector3 lookPos = Target.gameObject.transform.position - this.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100);
    }

    public void SetTarget(EnemyMovement e)
    {
        Target = e;
    }
}
