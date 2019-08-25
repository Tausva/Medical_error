using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlytrapManager : MonoBehaviour
{
    public void Eat()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Eat");
    }
}
