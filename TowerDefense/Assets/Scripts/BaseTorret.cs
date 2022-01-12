using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTorret : MonoBehaviour
{
    public GameObject Torret;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Torret.GetComponent<Torret>().RemoveEnemy(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Torret.GetComponent<Torret>().AddEnemy(other.gameObject);
        }
    }
}
