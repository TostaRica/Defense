using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        cameraPos.x = transform.position.x;
        transform.LookAt(2*transform.position - cameraPos);
    }
}
