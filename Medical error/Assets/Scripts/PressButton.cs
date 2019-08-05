using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{
    public void press()
    {
        GameObject.Find("ButtonPressSound").GetComponent<AudioSource>().Play();
    }

    public void pressDifferent(AudioClip playThis)
    {
        GameObject.Find("ButtonPressSound").GetComponent<AudioSource>().PlayOneShot(playThis);
    }
}
