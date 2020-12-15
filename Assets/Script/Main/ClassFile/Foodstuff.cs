using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foodstuff//各素材格納クラス
{
    public string Name { get; private set; }//各食材の名前
    public Sprite Material { get; private set; }//料理の画像を格納
    public int Getnum { get; private set; }//該当食材の所持数


    public Foodstuff(string Name, Sprite Material)//コンストラクタ
    {
        this.Name = Name;
        this.Material = Material;
        Getnum = 0;
    }

    public void GetFood()
    {
        Getnum++;
    }
}