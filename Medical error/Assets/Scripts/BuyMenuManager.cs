using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BuyMenuManager : MonoBehaviour
{
    private TextMeshProUGUI moneyText;

    // Start is called before the first frame update
    void Start()
    {
        moneyText = GameObject.Find("Money").GetComponent<TextMeshProUGUI>();
        UpdateMoneyValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMoneyValue()
    {
        moneyText.text = "You have " + PlayerPrefs.GetInt("Money") + " $";
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
