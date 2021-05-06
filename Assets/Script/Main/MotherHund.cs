using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 主婦のおてての表示と処理を行う。mainからランダムな状況で値をもらい、実行する。
/// setＵＰをあらかじめメインのスタート内で実行し、配列へのアクセスを取得する。
/// 
/// </summary>


public class MotherHund : MonoBehaviour
{
    GameObject[,] ImageBox;
    public void SetUp(GameObject[,] ImageBox)
    {
        this.ImageBox = ImageBox;
    }

    private readonly int a = -20;
    private readonly int b = -280;
    // Update is called once per frame
    /*
    public void MagicHund(int x,int y)//3,2
    {
        ImageBox[x, y].SetActive(false);
        Debug.Log(x + "レーンの野菜を取得");
        for (int i = 0; i < ImageBox.GetLength(1); i++)//8
        {
            //上の判定
            if (ynum - BoxSize / 2 < _RectTransform[x, i].localPosition.y && _RectTransform[x, i].localPosition.y < ynum + BoxSize / 2)//円の上座標<食材の中央座標<円の下座標
            {
                if (ImageBox[x, i].activeSelf)//オブジェクトの表示がtrueである
                {
                    Debug.Log(int.Parse(ImageBox[x, i].name));
                    num = int.Parse(ImageBox[x, i].name);
                    FS[num].GetFood();//食材を取得したことにして数を増やす
                    Money[0] -= FS[num].NowMoney;//所持金から値段をマイナスする
                    ImageBox[x, i].SetActive(false);//取得したオブジェクトを非表示に
                }
            }
        }
        */
}
