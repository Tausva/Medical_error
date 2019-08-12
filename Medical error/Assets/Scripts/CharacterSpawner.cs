using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterSpawner : MonoBehaviour
{
    public List<GameObject> FemaleEyes;
    public List<GameObject> MaleEyes;
    public List<GameObject> FemaleFacial;
    public List<GameObject> MaleFacial;
    public List<GameObject> FemaleHair;
    public List<GameObject> MaleHair;
    public List<GameObject> FemaleTop;
    public List<GameObject> MaleTop;
    public List<GameObject> FemaleBottom;
    public List<GameObject> MaleBottom;
    public GameObject Character;

    private GameObject A;

    private int HashAmount;
    private int CocaineAmount;
    private int AmphetamineAmount;
    private int Money = 0;
    private int amount;

    public int HashPrice;
    public int CocainePrice;
    public int AmphetaminePrice;
    private int AskingPrice = 0;
    private string drug = "Mel";

    private GameObject entry;
    private GameObject exit;
    private TextMeshProUGUI text;
    private TextMeshProUGUI StatText;
    private GameObject panel;

    void Start()
    {
        text = GameObject.Find("PanelText").GetComponent<TextMeshProUGUI>();
        StatText = GameObject.Find("StatText").GetComponent<TextMeshProUGUI>();

        panel = GameObject.Find("Panel");
        panel.SetActive(false);

        entry = GameObject.Find("Entry");
        exit = GameObject.Find("Exit");

        HashAmount = PlayerPrefs.GetInt("Hashish", 0);
        CocaineAmount = PlayerPrefs.GetInt("Cocaine", 0);
        AmphetamineAmount = PlayerPrefs.GetInt("Amphetamine", 0);

        amount = HashAmount + AmphetamineAmount + CocaineAmount;
        int modifier = 0;

        if (amount < 10)
            modifier = amount;

        if (Random.value > 0.25f)
        {
            amount += Random.Range(0, amount);
        }
        else amount -=  Random.Range(0, amount);
        amount += modifier;

        Characters();
    }

    public void Characters()
    {
        ShowStats();

        if (amount > 0 && HashAmount + AmphetamineAmount + CocaineAmount > 0)
        {

            int range = Random.Range(0, HashAmount + AmphetamineAmount + CocaineAmount);
            if (range < HashAmount)
            {
                AskingPrice = HashPrice + HashPrice * Random.Range(-50, 51) / 100;
                drug = "Hashish";
            }
            else if (range < HashAmount + AmphetamineAmount)
            {
                AskingPrice = AmphetaminePrice + AmphetaminePrice * Random.Range(-50, 51) / 100;
                drug = "Amphetamine";
            }
            else
            {
                AskingPrice = CocainePrice + CocainePrice * Random.Range(-50, 51) / 100;
                drug = "Cocaine";
            }

            CreateChar();

            amount--;
        }
    }

    private void CreateChar()
    {
         A = Instantiate(Character);

        int rand = 0;
        if (Random.value >= 0.5f) //gonna be female
        {
            rand = Random.Range(0, FemaleHair.Count);
            InstantiateChild(FemaleHair[rand], A);

            rand = Random.Range(0, FemaleEyes.Count);
            InstantiateChild(FemaleEyes[rand], A);

            rand = Random.Range(0, FemaleFacial.Count + 1);
            if (rand != FemaleFacial.Count)
                InstantiateChild(FemaleFacial[rand], A);

            rand = Random.Range(0, FemaleTop.Count);
            InstantiateChild(FemaleTop[rand], A);

            rand = Random.Range(0, FemaleBottom.Count);
            InstantiateChild(FemaleBottom[rand], A);
        } else
        {
            rand = Random.Range(0, MaleHair.Count + 1);
            if (rand != MaleHair.Count)
                InstantiateChild(MaleHair[rand], A);

            rand = Random.Range(0, MaleEyes.Count);
            InstantiateChild(MaleEyes[rand], A);

            rand = Random.Range(0, MaleFacial.Count + 1);
            if (rand != MaleFacial.Count)
                InstantiateChild(MaleFacial[rand], A);

            rand = Random.Range(0, MaleTop.Count + 1);
            if (rand != MaleTop.Count)
                InstantiateChild(MaleTop[rand], A);

            rand = Random.Range(0, MaleBottom.Count);
            InstantiateChild(MaleBottom[rand], A);
        }
    }

    private void InstantiateChild(GameObject child, GameObject parent)
    {
        GameObject temp = Instantiate(child);
        temp.transform.parent = parent.transform;
        temp.transform.localPosition = temp.transform.position;
    }

    public void AddMoney()
    {
        PlayerPrefs.SetInt("Money", Money + PlayerPrefs.GetInt("Money", 0));
    }

    public void OpenExit()
    {
        exit.SetActive(false);
    }

    public void CloseExit()
    {
        exit.SetActive(true);
    }

    public void openPanel()
    {
        panel.SetActive(true);
        text.text = "The shady person wants to buy " + drug + " for: " + AskingPrice + " $";
    }

    public void closePanel()
    {
        panel.SetActive(false);
    }

    public void SellDrug()
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) + AskingPrice);

        if (drug == "Hashish")
        {
            PlayerPrefs.SetInt("Hashish", --HashAmount);
        } else if (drug == "Amphetamine")
        {
            PlayerPrefs.SetInt("Amphetamine", --AmphetamineAmount);
        } else
        {
            PlayerPrefs.SetInt("Cocaine", --CocaineAmount);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextDay()
    {
        Loader.Load();
    }

    public void Waifus()
    {
        SceneManager.LoadScene("Waifus");
    }

    public void Houses()
    {
        SceneManager.LoadScene("House");
    }

    public void ShowStats()
    {
        StatText.text = "You have: " + HashAmount + " Hashish, " + CocaineAmount + " Cocaine and " + AmphetamineAmount + " Amphetamine";
        if (amount < 1)
            StatText.text += ", But there is no more customers this night";
        if (HashAmount + AmphetamineAmount + CocaineAmount < 1)
            StatText.text = "You have nothing to sell";
    }
}
