using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    private int money = 0;
    private List<int> drugs = new List<int>();

    public int AnesteticPrice, AntiCancerPrice, SedativePrice;

    void Start()
    {
        for (int i=0; i<3; i++)
            drugs.Add(0);
    }

    public void AddItem(string item)
    {
        switch (item)
        {
            case "Cocaine":
                drugs[1]++;
                break;
            case "Hashish":
                drugs[0]++;
                break;
            case "Amphetamine":
                drugs[2]++;
                break;
            case "Anesthetic":
                money += AnesteticPrice;
                break;
            case "Anticancer":
                money += AntiCancerPrice;
                break;
            case "Sedative":
                money += SedativePrice;
                break;
        }
    }

    public void ConvertMoeny()
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + money);
    }

    public void ConvertDrugs()
    {
        PlayerPrefs.SetInt("Hashish", PlayerPrefs.GetInt("Hashish") + drugs[0]);
        PlayerPrefs.SetInt("Cocaine", PlayerPrefs.GetInt("Cocaine") + drugs[1]);
        PlayerPrefs.SetInt("Amphetamine", PlayerPrefs.GetInt("Amphetamine") + drugs[2]);
    }

    public void Confiscate ()
    {
        money = 0;
        drugs[0] = 0;
        drugs[1] = 0;
        drugs[2] = 0;
    }

    public void ShowStats()
    {
        GameObject.Find("Earned amount").GetComponent<TextMeshProUGUI>().text = money.ToString();
        GameObject.Find("HashAmount").GetComponent<TextMeshProUGUI>().text = drugs[0].ToString();
        GameObject.Find("CocaineAmount").GetComponent<TextMeshProUGUI>().text = drugs[1].ToString();
        GameObject.Find("AmphetamineAmount").GetComponent<TextMeshProUGUI>().text = drugs[2].ToString();
    }
}
