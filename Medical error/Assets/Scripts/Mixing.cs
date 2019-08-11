using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixing : MonoBehaviour
{
    public AudioClip addgrass;
    public AudioClip addliquid;
    public AudioClip mixingSound;
    public AudioClip trash;

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

    private bool isPaused = false;

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
        if (isPaused)
            time += Time.deltaTime;

        if (isTimed == true)
        {
            canAdd = false;

            if (time < Time.time)
            {
                isTimed = false;
                GameObject.Find("Inventory").GetComponent<Inventory>().AddItem(result);

                GameObject.Find("mixingAudio").GetComponent<AudioSource>().Stop();
                GameObject.Find("AudioManager").GetComponent<AudioSource>().PlayOneShot(mixingSound);

                if (!isLegal)
                    isLegal = true;
                else GameObject.Find("Boss").GetComponent<Spawning>().AddOneToMedicineCount();

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

            if (name == "Coca leaves" || name == "Marijuana")
            {
                GameObject.Find("AudioManager").GetComponent<AudioSource>().PlayOneShot(addgrass);
            } else GameObject.Find("AudioManager").GetComponent<AudioSource>().PlayOneShot(addliquid);
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

            GameObject.Find("mixingAudio").GetComponent<AudioSource>().Play();

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
        GameObject.Find("mixingAudio").GetComponent<AudioSource>().Stop();
    }

    void ChangeRandomColor()
    {
        insideColor.color = new Color(Random.value, Random.value, Random.value);
    }

    public void TrashSound()
    {
        GameObject.Find("AudioManager").GetComponent<AudioSource>().PlayOneShot(trash);
    }

    public void PauseMixing()
    {
        GameObject.Find("mixingAudio").GetComponent<AudioSource>().Pause();
        isPaused = true;
    }

    public void UnPauseMixing()
    {
        GameObject.Find("mixingAudio").GetComponent<AudioSource>().UnPause();
        isPaused = false;
    }
}