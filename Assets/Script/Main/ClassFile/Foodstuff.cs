using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foodstuff : MonoBehaviour//各素材格納クラス
{
    public string Name { get; private set; }//各食材の名前
    public Sprite Material { get; private set; }//料理の画像を格納
    public int Getnum { get; private set; }//該当食材の所持数
    public int[] Money { get; private set; }//該当食材の値段5つ
    public int NowMoney { get; private set; }//今回のゲームで使用する値段



    public Foodstuff(string Name, Sprite Material,int[] Money)//コンストラクタ
    {
        this.Name = Name;
        this.Material = Material;
        Getnum = 0;
        this.Money = Money;

        int MoneyNum = Random.Range(0, Money.Length-1);//該当食材に登録されている金額数を上限にランダムで何番の金額を使用するか決定。
        NowMoney = Money[MoneyNum];//今回のゲームで使用する値段を入れておく
    }

    public void Setup(string Name, Sprite Material, int[] Money)
    {
        this.Name = Name;
        this.Material = Material;
        Getnum = 0;
        this.Money = Money;
    }

    public void GetFood()
    {
        Debug.Log(Name + "ゲット" + Getnum + "個目");
        Getnum++;
    }
}