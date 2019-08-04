using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{
    public void press()
    {
        GameObject.Find("ButtonPressSound").GetComponent<AudioSource>().Play();
    }
}
