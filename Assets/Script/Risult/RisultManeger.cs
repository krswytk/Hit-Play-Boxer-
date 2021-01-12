using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisultManeger : MonoBehaviour
{
    private CuisineClass[] CC;//料理と食材をまとめて格納するクラス
    private Foodstuff[] FS;//食材格納クラス　取得数などを格納
    private int[] t;//ランダムに選ばれた３つの数字を格納しておく

    private CookingDisplay CD;//料理の表示を行う関数群を格納した関数
    private FoodNum FN;//料理の表示を行う関数群を格納した関数

    // Start is called before the first frame update
    void Start()
    {

        DecisionCooking();//制作する料理の表示を行う
        GetFoodNum();//入手した食材の量を表示する

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// メインからリザルトに必要なクラス、値を持ってくる
    /// </summary>
    /// <param name="CC"></param>
    /// <param name="FS"></param>
    /// <param name="t"></param>
    public void StertSetUP(CuisineClass[] CC,Foodstuff[] FS,int[] t)
    {
        this.CC = CC;
        this.FS = FS;
        this.t = t;


        string StringCC = ""; 
        string StringFS = "";
        string Stringt = "";
        for (int i = 0; i< CC.Length; i++)
        {
            StringCC += CC[i].CuisineName + ", ";
        }
        for (int i = 0; i < FS.Length; i++)
        {
            StringFS += FS[i].Name + ", ";
        }
        for (int i = 0; i < t.Length; i++)
        {
            Stringt += t[i].ToString() + ", ";
        }
        Debug.Log(StringCC);
        Debug.Log(StringFS);
        Debug.Log(Stringt);
    }


    private void DecisionCooking()//作る料理の決定
    {
        CD = GetComponent<CookingDisplay>();
        CD.CDStert(CC,t);
    }

    private void GetFoodNum()//入手した食材の量を表示
    {
        FN = GetComponent<FoodNum>();
        FN.CNStart(CC, FS, t);
    }
}
