using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public AudioClip bossSound;
    public AudioClip DoorOpenSound;
    public AudioClip DoorCloseSound;
    public AudioClip Angry;
    private new AudioSource audio = new AudioSource();

    public int checkRate;
    public float commingTime = 3f;
    public float watchTime = 2f;

    private int state = 0;
    private float timer;

    private GameObject DoorClosed;
    private GameObject DoorOpen;
    private GameObject Body;
    private GameObject Head;
    private GameObject Happy;
    private SpriteRenderer HeadColor;
    private GameObject ShoutingCloud;


    private int countOfMedicine;
    private int pausedState;
    private bool isAngry = false;

    void Start()
    {
        DoorClosed = GameObject.Find("DoorClosed");
        DoorOpen = GameObject.Find("DoorOpen");
        Body = GameObject.Find("Body");
        Head = GameObject.Find("Head");
        Happy = GameObject.Find("Happy");
        HeadColor = Head.GetComponent<SpriteRenderer>();
        ShoutingCloud = GameObject.Find("shouting-cloud");

        Head.SetActive(false);
        ShoutingCloud.SetActive(false);
        DoorOpen.SetActive(false);
        Body.SetActive(false);
        Happy.SetActive(false);

        audio = GameObject.FindGameObjectWithTag("DayMusic").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (state == -1)
        {
            timer += Time.deltaTime;
        }
        else if (state == 0)
        {
            if (Random.Range(0, checkRate) == 0)
            {
                state = 1;
            }
        }
        else if (state == 1)
        {
            audio.Stop();
            audio.PlayOneShot(bossSound);

            GameObject.Find("Shadow").transform.position = new Vector2(6.11f, 2.6f);
            GameObject.Find("Shadow").GetComponent<Animator>().SetTrigger("Walk");
            state = 2;
            timer = commingTime + Time.time;
        }
        else if (state == 2 && timer <= Time.time)
        {
            state = 3;
        }
        else if (state == 3)
        {
            DoorClosed.SetActive(false);
            DoorOpen.SetActive(true);
            Body.SetActive(true);

            GameObject.Find("AudioManager").GetComponent<AudioSource>().PlayOneShot(DoorOpenSound);

            timer = Time.time + watchTime;
            state = 4;
        }
        else if (state == 4 && timer > Time.time)
        {
            Detect();
        }
        else if (state == 4 && timer <= Time.time)
        {
            DoorClosed.SetActive(true);
            DoorOpen.SetActive(false);
            Body.SetActive(false);

            GameObject.Find("AudioManager").GetComponent<AudioSource>().PlayOneShot(DoorCloseSound);

            audio.Stop();
            audio.Play();

            if (countOfMedicine < 1)
            {
                checkRate -= 20;
                watchTime += 0.2f;
            }
            else if (countOfMedicine > 0)
            {
                checkRate += 20;
                watchTime -= 0.2f;
            }
            countOfMedicine = 0;

            state = 5;
            GameObject.Find("Shadow").transform.position = new Vector2(6.11f, 2.6f);
            GameObject.Find("Shadow").GetComponent<Animator>().SetTrigger("WalkBack");
            timer = Time.time + commingTime;

            Happy.SetActive(false);
        }
        else if (state == 5 && timer <= Time.time)
        {
            state = 0;
        }
        else if (state == 6)
        {
            if (!isAngry)
            {
                GameObject.Find("AudioManager").GetComponent<AudioSource>().PlayOneShot(Angry);
                isAngry = true;
            }

            Head.SetActive(true);
            HeadColor.color = new Color(HeadColor.color.r, HeadColor.color.g - 0.01f, HeadColor.color.b - 0.01f);
            ShoutingCloud.SetActive(true);

            if (HeadColor.color.g < -1)
            {
                state = 7;
            }
        }
        else if (state == 7)
        {
            GameObject.Find("Clock").GetComponent<ClockTicking>().GameOver();
        }
    }

    void Detect()
    {
        if (!GameObject.Find("Chemical Mix Place").GetComponent<Mixing>().isLegal)
            state = 6;
    }

    public void AddOneToMedicineCount()
    {
        if (state == 4)
        {
            countOfMedicine++;
            Happy.SetActive(true);
        }
    }

    public void PauseDaBoy()
    {
        pausedState = state;
        state = -1;
        GameObject.Find("AudioManager").GetComponent<AudioSource>().Pause();
        PauseResumeAnimation(false);
    }

    public void ResumeDaBoy()
    {
        state = pausedState;
        GameObject.Find("AudioManager").GetComponent<AudioSource>().UnPause();
        PauseResumeAnimation(true);
    }

    private void PauseResumeAnimation(bool state)
    {
        GameObject.Find("Shadow").GetComponent<Animator>().enabled = state;
    }
}
