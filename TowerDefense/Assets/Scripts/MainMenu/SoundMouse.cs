using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMouse : MonoBehaviour
{
    public AudioSource SoundClickEnter;
    public AudioSource SoundClickClick;

    public void soundHover()
    {
        SoundClickEnter.Play();
    }

    public void soundClick() {

        SoundClickClick.Play();
    }
}
