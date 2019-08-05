using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HouseManager : MonoBehaviour
{
    public int index;
    public int value;

    public Image img;
    public GameObject buyText;
    public GameObject buyText2;
    public TextMeshProUGUI text;
    public TextMeshProUGUI canHoldText;
    // Start is called before the first frame update
    void Start()
    {
        text.text = value.ToString();

        if (PlayerPrefs.GetInt("House", 0) == index)
        {
            MakeValid();
        } else if (PlayerPrefs.GetInt("House", 0) > index)
        {
            MakeInvisible();
        }
    }

    void MakeValid()
    {
        img.color = new Color(1,1,1);
        buyText.SetActive(false);
        buyText2.SetActive(false);
        gameObject.GetComponent<Image>().color = new Color(1,1,0);
        GameObject.Find("House").GetComponent<ChangeHouse>().change(index);
        canHoldText.color = Color.black;
    }

    public void Buy()
    {
        if (PlayerPrefs.GetInt("House", 0) < index) {
            if (value <= PlayerPrefs.GetInt("Money", 0))
            {
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - value);
                PlayerPrefs.SetInt("House", index);
                MakeValid();
            }
        }
    }

    public void MakeInvisible()
    {
        img.color = new Color(1, 1, 1);
        buyText.SetActive(false);
        buyText2.SetActive(false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 0);
        canHoldText.color = Color.black;
    }
}
