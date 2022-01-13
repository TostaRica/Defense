using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumenSet : MonoBehaviour
{
    public Slider audiolistenerer;

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = this.gameObject.GetComponent<Slider>().value;
    }
}
