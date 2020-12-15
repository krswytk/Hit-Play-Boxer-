using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuisineClass//料理本体と必要素材を格納するクラス
{
    public string CuisineName { get; private set; }//料理の名前
    public string[] Name { get; private set; }//食材の名前を配列で格納
    public Sprite CuisineMaterial { get; private set; }//料理の画像を格納
    public Sprite[] Material { get; private set; }//食材の画像を格納


    public CuisineClass(string[] Name, Sprite[] Material)//コンストラクタ
    {
        this.CuisineName = Name[0];
        this.CuisineMaterial = Material[0];

        if (Name.GetLength(0) == Material.GetLength(0))
        {
            this.Name = new string[Name.GetLength(0) - 1];
            this.Material = new Sprite[Material.GetLength(0) - 1];
            //Debug.Log(Name.GetLength(0));
            for (int i = 0; i < Name.GetLength(0) - 1; i++)
            {
                //Debug.Log(i);
                //Debug.Log(Name[i + 1] +" "+Material[i + 1]);
                this.Name[i] = Name[i + 1];
                this.Material[i] = Material[i + 1];
            }
        }
    }
}

