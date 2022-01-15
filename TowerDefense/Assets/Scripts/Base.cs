using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{

    public float Offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void Orientation(Transform lookPos)
    {
        Vector3 newRotation = new Vector3(0, lookPos.eulerAngles.y, 0);
        transform.eulerAngles = newRotation;
    }

}
