﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixing : MonoBehaviour
{
    public string list;
    public bool isLegal = true;

    [Tooltip("Format: 1 45 result ingrediant ingrediant... ; 0 - legal, 1 - ilegal, second number - recipe time")]
    public List<string> recipes;

    private bool isTimed = false;
    private float time = 0;

    private bool canAdd = true;
    private string result;

    private GameObject progressBar;
    private GameObject Cloud;
    public List<GameObject> Drugs;

    private GameObject inside;
    private SpriteRenderer insideColor;
    private bool firstTime = true;

    private void Start()
    {
        progressBar = GameObject.Find("TimerBar");
        progressBar.SetActive(false);
        Cloud = GameObject.Find("ProductionCloud");
        foreach (GameObject obj in Drugs)
        {
            obj.SetActive(false);
        }
        Cloud.SetActive(false);

        inside = GameObject.Find("ChemicalInside");
        insideColor = inside.GetComponent<SpriteRenderer>();
        inside.SetActive(false);
    }

    void Update()
    {
        if (list != "")
        {
            inside.SetActive(true);

            if (firstTime)
            {
                ChangeRandomColor();
                firstTime = false;
            }
        }
        else
        {
            inside.SetActive(false);
            firstTime = true;
        }

        foreach (string recipe in recipes)
        {
            string recip = recipe.Substring(5);
            if (recip.Substring(recip.IndexOf(' ') + 1) == list)
            {
                if (recipe[0].ToString() == "1")
                    isLegal = false;
                else isLegal = true;

                result = recip.Substring(0, recip.IndexOf(' '));
                Timer(int.Parse(recipe[2].ToString() + recipe[3]));
            }
        }
        if (isTimed == true)
        {
            canAdd = false;

            if (time < Time.time)
            {
                isTimed = false;
                GameObject.Find("Inventory").GetComponent<Inventory>().AddItem(result);

                if (!isLegal)
                    isLegal = true;

                Cloud.SetActive(true);
                switch (result)
                {
                    case "Cocaine":
                        Drugs[0].SetActive(true);
                        break;
                    case "Hashish":
                        Drugs[1].SetActive(true);
                        break;
                    case "Amphetamine":
                        Drugs[2].SetActive(true);
                        break;
                    case "Anesthetic":
                        Drugs[3].SetActive(true);
                        break;
                    case "Anticancer":
                        Drugs[4].SetActive(true);
                        break;
                    case "Sedative":
                        Drugs[5].SetActive(true);
                        break;
                }
                GameObject.Find("ProductionCloud").GetComponent<Animator>().SetTrigger("Pop");
                
                ClearList();
                canAdd = true;
            }
        }
    }

    public void AddComponent(string name)
    {
        if (canAdd)
        {
            list += name + " ";
            ChangeRandomColor();
        }
    }

    public void ClearList()
    {
        list = "";
    }

    private void Timer(int sec)
    {
        if (!isTimed)
        {
            progressBar.SetActive(true);
            progressBar.GetComponent<timer>().StartTimer((float)sec/10);

            time = Time.time + (float)sec / 10;
            isTimed = true;

            foreach (GameObject obj in Drugs)
            {
                obj.SetActive(false);
            }
            Cloud.SetActive(false);
        }
    }

    public void TrashOperation()
    {
        isTimed = false;
        ClearList();
        canAdd = true;
        isLegal = true;
    }

    void ChangeRandomColor()
    {
        insideColor.color = new Color(Random.value, Random.value, Random.value);
    }
}