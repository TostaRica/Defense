using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    IEnumerator Wait() {
        yield return new WaitForSeconds(Globals.mudAndDeadTime);
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Wait());
    }

}
