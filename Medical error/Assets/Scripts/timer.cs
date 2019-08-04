using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{
    private float progressTime;
    private bool isActive = false;

    private float space;
    private float passedTime;

    private Transform bar;
    private float barXposition;

    private void Awake()
    {
        bar = GameObject.Find("Bar").transform;
        barXposition = bar.position.x - 1.3f;
    }

    void Update()
    {
        if (isActive)
        {
            passedTime -= Time.deltaTime;

            if (passedTime < 0)
            {
                isActive = false;
                gameObject.SetActive(false);
            }
            space = 1 - passedTime / progressTime;
            bar.localScale = new Vector2(space, bar.localScale.y);
            bar.localPosition = new Vector2(-1f + space , 0);
        }
    }

    public void StartTimer(float time)
    {
        progressTime = time;
        passedTime = time;

        isActive = true;
    }
}