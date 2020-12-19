using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foodstuff : MonoBehaviour//各素材格納クラス
{
    public string Name { get; private set; }//各食材の名前
    public Sprite Material { get; private set; }//料理の画像を格納
    public int Getnum { get; private set; }//該当食材の所持数
    public int MinM { get; private set; }//該当食材の最安値
    public int MaxM { get; private set; }//該当食材の最高値



    public Foodstuff(string Name, Sprite Material,int MinM, int MaxM)//コンストラクタ
    {
        this.Name = Name;
        this.Material = Material;
        Getnum = 0;
        this.MinM = MinM;
        this.MaxM = MaxM;
    }

    public void GetFood()
    {
        Debug.Log(Name + "ゲット" + Getnum + "個目");
        Getnum++;
    }
}