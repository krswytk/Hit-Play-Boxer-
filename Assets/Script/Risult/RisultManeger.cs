using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisultManeger : MonoBehaviour
{
    private CuisineClass[] CC;//料理と食材をまとめて格納するクラス
    private Foodstuff[] FS;//食材格納クラス　取得数などを格納
    private int[] t;//ランダムに選ばれた３つの数字を格納しておく

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


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
}
