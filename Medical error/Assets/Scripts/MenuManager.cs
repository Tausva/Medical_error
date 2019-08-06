using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private bool isFirstGame = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("First", 0) != 5)
        {
            isFirstGame = true;
            PlayerPrefs.SetInt("First", 5);
        }
        if (isFirstGame)
        {
            PlayerPrefs.SetInt("Money", 0);
            PlayerPrefs.SetInt("Hashish", 0);
            PlayerPrefs.SetInt("Cocaine", 0);
            PlayerPrefs.SetInt("Amphetamine", 0);
        }
        GameObject.Find("MoneyAmount").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Money", 0).ToString() + " $";
    }

    public void PlayGame()
    {
        Loader.Load();
        //SceneManager.LoadScene("DayScene");
    }

    public void Waifus()
    {
        SceneManager.LoadScene("Waifus");
    }

    public void House()
    {
        SceneManager.LoadScene("House");
    }

    public void Credits ()
    {
        SceneManager.LoadScene("CreditsScene");
    }
}
