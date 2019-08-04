using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyWifu : MonoBehaviour
{
    // Start is called before the first frame update
    public int index;
    public int price;
    public GameObject text;
    private bool isBought = false;
    private int space;

    void Awake()
    {
        //PlayerPrefs.SetInt("Wifu" + index, 0);
        space = PlayerPrefs.GetInt("House", 0) + PlayerPrefs.GetInt("House", 0) - 1;

        text.GetComponent<TextMeshProUGUI>().text = price + "$";

        if (PlayerPrefs.GetInt("Wifu" + index, 0) > 0)
        {
            Unlocked();
        }

        if (isBought)
        {
            GameObject.Find("Wifu").GetComponent<BuyWifu>().AddMeIn();
        }
    }

    void Start()
    {
        GameObject.Find("Wifu").GetComponent<BuyWifu>().CalculateSpace();
    }

    void Unlocked()
    {
        isBought = true; 
        gameObject.GetComponent<Image>().color = new Color(1,1,1);
        text.SetActive(false);
    }

    public void Buy()
    {
        if (!isBought)
        {
            if (PlayerPrefs.GetInt("Money", 0) >= price && GameObject.Find("Wifu").GetComponent<BuyWifu>().GetSpace() > 0)
            {
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - price);
                PlayerPrefs.SetInt("Wifu" + index, 1);
                Unlocked();
                GameObject.Find("Wifu").GetComponent<BuyWifu>().AddMeIn();
                GameObject.Find("Wifu").GetComponent<BuyWifu>().CalculateSpace();
            }
        } else
        {
            PlayerPrefs.SetInt("Wifu" + index, PlayerPrefs.GetInt("Wifu" + index) + 1);
        }
    }

    public void AddMeIn()
    {
        space--;
    }

    public int GetSpace()
    {
        return space;
    }

    public void CalculateSpace()
    {
        if (space == 0)
            GameObject.Find("Space").GetComponent<TextMeshProUGUI>().text = "Full :(";
        else GameObject.Find("Space").GetComponent<TextMeshProUGUI>().text = space + " empty spaces";
        Debug.Log(gameObject.name);
    }
}
