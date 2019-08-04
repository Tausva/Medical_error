using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BackToMenu : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.GetInt("UltimateWifu", 0) == 1)
        {
            GameObject.Find("EasterEgg").GetComponent<TextMeshProUGUI>().text = "Art / Ultimate wifu:";
        }
        else GameObject.Find("Egg").SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
