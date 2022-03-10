using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArea : MonoBehaviour
{
    private bool destroyMe;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(Globals.burnAreaTime*Time.deltaTime);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
