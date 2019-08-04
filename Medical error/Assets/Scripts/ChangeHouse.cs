using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHouse : MonoBehaviour
{
    public List<Sprite> Images;

    public void Awake()
    {
        gameObject.GetComponent<Image>().enabled = false;
    }

    public void change(int index)
    {
        gameObject.GetComponent<Image>().enabled = true;
        gameObject.GetComponent<Image>().sprite = Images[index-1];
    }
}
