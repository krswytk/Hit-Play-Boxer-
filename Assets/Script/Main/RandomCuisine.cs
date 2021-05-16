using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomCuisine : MonoBehaviour//制作する料理をランダムで決定すると同時に料理を画面に表示
{
    public CuisineClass[] CC;
    int[] t;//選択された数字を格納しておく

    public void RandomCuisineStart(CuisineClass[] CC, int[] t)//ロードされて格納され終わっている料理クラスをメインから受け取る
    {
        this.CC = CC;//呼び出しがめんどいのでメモリコピー
        this.t = t;//呼び出しがめんどいのでメモリコピー
        RandomDecision();//殴り調理する料理をかぶらないように決定
        CookingDisplay();//ゲーム上UIに選択された料理を表示する

    }


    public void RandomDecision()
    {
        int num = CC.GetLength(0);//料理の個数を取得
        t = new int[3];//ランダムに3つの数字を選択
        Debug.Log("保持している料理の個数は : " + num);
        do
        {
            t[0] = Random.Range(0, num);
            t[1] = Random.Range(0, num);
            t[2] = Random.Range(0, num);
            //Debug.Log(t[0]);
            //Debug.Log(t[1]);
           // Debug.Log(t[2]);
        } while (t[0] == t[1] || t[1] == t[2] || t[0] == t[2]);//かぶらないまで繰り返す
        //Debug.Log(t[0] + "" + t[1] + "" + t[2]);

    }

    public void CookingDisplay()
    {
        for (int i = 0; i < 3; i++)
        {
            Image image = GameObject.Find("Cook" + (i + 1)).GetComponent<Image>();//料理欄の料理オブジェクトを右から順に取得する
            image.sprite = CC[t[i]].CuisineMaterial;
        }
        //Debug.Log(t[0] + "" + t[1] + "" + t[2]);
    }

    public int[] ReturnT()
    {
        return t;
    }
}
