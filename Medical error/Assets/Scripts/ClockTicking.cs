using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClockTicking : MonoBehaviour
{
    public AudioClip bell;
    private bool hadRand = false;

    public float TimeInSeconds;

    private Transform hoursArrow;
    private Transform minuteArrow;

    private GameObject panel;
    private GameObject pausePanel;
    private GameObject pauseButton;
    private GameObject exitPanel;

    private bool isTicking = true;

    void Start()
    {
        exitPanel = GameObject.Find("ConfirmationPanel");
        exitPanel.SetActive(false);

        TimeInSeconds += Time.time;
        hoursArrow = GameObject.Find("HourArrow").transform;
        minuteArrow = GameObject.Find("MinuteArrow").transform;

        panel = GameObject.Find("EndGame");
        panel.SetActive(false);

        pausePanel = GameObject.Find("PauseMenu");
        pausePanel.SetActive(false);

        pauseButton = GameObject.Find("PauseButton");
    }

    void FixedUpdate()
    {
        if (isTicking)
        {
            minuteArrow.Rotate(0, 0, -1f / (TimeInSeconds / 48f));
            hoursArrow.Rotate(0, 0, -1f / (TimeInSeconds / 4f));
        } else
        {
            TimeInSeconds += Time.deltaTime;
        }

        if (Time.time >= TimeInSeconds)
        {
            PauseGame();
            panel.SetActive(true);
            GameObject.Find("Recycle Bin").GetComponent<Recycle>().RecycleWhatsLeft();
            GameObject.Find("Inventory").GetComponent<Inventory>().ShowStats();

            if (!hadRand)
            {
                hadRand = true;
                GameObject.Find("MusicManager").GetComponent<AudioSource>().Stop();
                GameObject.Find("AudioManager").GetComponent<AudioSource>().PlayOneShot(bell);
            }
        }
    }

    public void PauseGame()
    {
        GameObject.Find("MusicManager").GetComponent<AudioSource>().Pause();
        GameObject.Find("mixingAudio").GetComponent<AudioSource>().Stop();
        isTicking = false;
        GameObject.Find("Marijuana").GetComponent<Drag>().DisableDragging();
        GameObject.Find("Coca leaves").GetComponent<Drag>().DisableDragging();
        GameObject.Find("Zoxide").GetComponent<Drag>().DisableDragging();
        GameObject.Find("Clorid").GetComponent<Drag>().DisableDragging();
        GameObject.Find("Boss").GetComponent<Spawning>().PauseDaBoy();
        pauseButton.SetActive(false);

        GameObject.Find("Chemical Mix Place").GetComponent<Mixing>().PauseMixing();
        if (GameObject.Find("TimerBar") != null)
            GameObject.Find("TimerBar").GetComponent<timer>().PauseTimer();
    }

    public void PausePauseGame()
    {
        PauseGame();
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        GameObject.Find("MusicManager").GetComponent<AudioSource>().UnPause();
        isTicking = true;
        GameObject.Find("Marijuana").GetComponent<Drag>().EnableDragging();
        GameObject.Find("Coca leaves").GetComponent<Drag>().EnableDragging();
        GameObject.Find("Zoxide").GetComponent<Drag>().EnableDragging();
        GameObject.Find("Clorid").GetComponent<Drag>().EnableDragging();
        GameObject.Find("Boss").GetComponent<Spawning>().ResumeDaBoy();
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);

        GameObject.Find("Chemical Mix Place").GetComponent<Mixing>().UnPauseMixing();
        if (GameObject.Find("TimerBar") != null)
            GameObject.Find("TimerBar").GetComponent<timer>().UnPauseTimer();
        exitPanel.SetActive(false);
    }

    public void MainMenu()
    {
        GameObject.Find("Inventory").GetComponent<Inventory>().ConvertMoeny();
        SceneManager.LoadScene("MainMenu");
    }

    public void Continue()
    {
        GameObject.Find("Inventory").GetComponent<Inventory>().ConvertMoeny();
        GameObject.Find("Inventory").GetComponent<Inventory>().ConvertDrugs();
        SceneManager.LoadScene("NightScene");
    }

    public void GameOver()
    {
        GameObject.Find("Inventory").GetComponent<Inventory>().Confiscate();
        PauseGame();
        panel.SetActive(true);
        GameObject.Find("EndTitle").GetComponent<TextMeshProUGUI>().text = "Game Over";
        GameObject.Find("Drugs").GetComponent<TextMeshProUGUI>().text = "You have been caught, drugs you've made has been confiscated. Also no money for a day of work has been paid out :(";
        GameObject.Find("NightButton").SetActive(false);
        GameObject.Find("EarnedTitle").SetActive(false);
        GameObject.Find("Earned amount").SetActive(false);
        GameObject.Find("DollarSign").SetActive(false);
        GameObject.Find("HashText").SetActive(false);
        GameObject.Find("CocaineText").SetActive(false);
        GameObject.Find("AmphetamineText").SetActive(false);
        GameObject.Find("HashAmount").SetActive(false);
        GameObject.Find("CocaineAmount").SetActive(false);
        GameObject.Find("AmphetamineAmount").SetActive(false);
    }

    public void ExitPanel()
    {
        exitPanel.SetActive(true);
    }

    public void BackToGame ()
    {
        exitPanel.SetActive(false);
    }
}