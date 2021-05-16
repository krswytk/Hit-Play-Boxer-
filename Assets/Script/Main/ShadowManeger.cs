using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShadowManeger : MonoBehaviour
{
    private Image[] ShadowImage;//影格納スクリプト
    CuisineClass[] CC;//料理と食材をまとめて格納するクラス
    private Foodstuff[] FS;
    private int[] t;//仕様しているCCの配列番号を格納している メインから呼び出す


    // Start is called before the first frame update
    public void SetUP(CuisineClass[] CC, Foodstuff[] FS)
    {
        this.CC = CC;
        this.FS = FS;

        t = GetComponent<MainManeger>().t;        try
        {
            ShadowImage = new Image[t.Length];//tは使用しているCCを格納している つまり表示されているCCの数がt

            for (int i = 0; i < t.Length; i++)
            {
                //Debug.LogError(i);
                ShadowImage[i] = GameObject.Find("Shadow" + (i+1)).GetComponent<Image>();
            }
        }
        catch(NullReferenceException e)
        {
            Debug.LogError(e);
            Debug.LogError("影の取得または料理の配列取得に失敗しました。");
            return;
        }
    }

    int lp;
    public void GetCuisineCheck(int num)//入手した食材が必要食材かチェック
    {
        /*ShadowImage[0].fillAmount = 0f;
        ShadowImage[1].fillAmount = 0f;
        ShadowImage[2].fillAmount = 0f;*/
        //ShadowImage[0].color = new Color(1f,0f,0f,1f);//色変わった
        //Debug.LogError("GetCuisineCheckは正常に呼び出されています。");
        for (int i = 0; i < t.GetLength(0); i++)//3回転
        {
            lp = 0;
            for (int l = 0; l < CC[t[i]].Name.Length; l++)//5回転//料理に必要な食材の数分ループ
            {
                //判定
                if (CC[t[i]].FoodCheck[l] == false && CC[t[i]].Name[l] == FS[num].Name)//食材がまだ未獲得 かつ 必要食材名と入手食材名が一緒なら
                {
                    CC[t[i]].GetFood(l);
                }

                if (CC[t[i]].FoodCheck[l]) lp++;//入手済みなら数を増やす
            }
            //Debug.LogError(lp);
            //Debug.LogError(1f / CC[t[i]].Name.Length * lp);
            ShadowImage[i].fillAmount = 1f - 1f / CC[t[i]].Name.Length * lp;//こいつのせい 1f 
        }
    }
}
