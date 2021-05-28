using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CoinManeger : MonoBehaviour
{
    Image CoinShadow;

    private int StartMoney;

    private Text Money_Text;
    private Text StartMoney_Text;


    private float per;
    // Start is called before the first frame update
    public void CoinSetUp(int StartMoney)
    {
        CoinShadow = GameObject.Find("CoinShodow").GetComponent<Image>();
        this.StartMoney = StartMoney;
        CoinShadow.fillAmount = 0;
        //Debug.Log("Money =" + this.Money);

        Money_Text = GameObject.Find("Money_Text").GetComponent<Text>();
        StartMoney_Text = GameObject.Find("StartMoney_Text").GetComponent<Text>();

        Money_Text.text = String.Format("{0:#,0}", StartMoney);
        StartMoney_Text.text = String.Format("{0:#,0}", StartMoney);
    }

    // Update is called once per frame
    public void CoinDown(int Money)
    {
        per = Money / (StartMoney * 1.0f);
        CoinShadow.fillAmount = 1 - per;
        //Debug.Log("Money =" + Money);
        Money_Text.text = String.Format("{0:#,0}", Money);
    }
}
