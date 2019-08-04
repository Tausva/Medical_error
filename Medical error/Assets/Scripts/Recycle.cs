using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recycle : MonoBehaviour
{
    void OnMouseDown()
    {
        GameObject.Find("Chemical Mix Place").GetComponent<Mixing>().ClearList();
        GameObject.Find("Chemical Mix Place").GetComponent<Mixing>().TrashSound();

        if (GameObject.Find("TimerBar") != null)
        {
            GameObject.Find("TimerBar").SetActive(false);
        }

        GameObject.Find("Chemical Mix Place").GetComponent<Mixing>().TrashOperation();
    }

    public void RecycleWhatsLeft()
    {
        GameObject.Find("Chemical Mix Place").GetComponent<Mixing>().ClearList();

        if (GameObject.Find("TimerBar") != null)
        {
            GameObject.Find("TimerBar").SetActive(false);
        }

        GameObject.Find("Chemical Mix Place").GetComponent<Mixing>().TrashOperation();
    }
}
