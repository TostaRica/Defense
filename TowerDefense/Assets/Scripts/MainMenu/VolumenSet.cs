using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumenSet : MonoBehaviour
{
    public Slider audiolistenerer;
    private void Start()
    {
        this.gameObject.GetComponent<Slider>().value = AudioListener.volume; 
    }
    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = this.gameObject.GetComponent<Slider>().value;
    }
}
