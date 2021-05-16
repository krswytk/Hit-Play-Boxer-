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
    RectTransform[,] _RectTransform;

    private Animator AnimHund;
    public void SetUp(GameObject[,] ImageBox)
    {
        this.ImageBox = ImageBox;
        _RectTransform = new RectTransform[ImageBox.GetLength(0), ImageBox.GetLength(1)];

        for (int i = 0; i < ImageBox.GetLength(0); i++)//3
        {
            for (int l = 0; l < ImageBox.GetLength(1); l++)//8
            {
                _RectTransform[i,l] = ImageBox[i,l].GetComponent<RectTransform>();
            }
        }

        AnimHund = GameObject.Find("Hund").GetComponent<Animator>();
    }

    /*
    private readonly int a = -20;
    private readonly int b = -280;
    */
    private readonly int a = 110;
    private readonly int b = -150;
    private int ynum = 0;//縦のどこを取得するか格納しておく変数
    private int inum = 0;//iを格納して0.5秒後に食べ物を消せるようにする変数
    private readonly int BoxSize = 130;

    public void MagicHund(int x, int y)//3,2  012 01
    {

        switch (y)
        {
            case 0: ynum = a; break;
            case 1: ynum = b; break;
            default:
                Debug.LogError("ママの手で問題発生_20");
                break;
        }

        //
        Debug.Log(x + "レーン"+ y + "番目の野菜を強奪");
        for (int i = 0; i < ImageBox.GetLength(1); i++)//8
        {
            //Debug.Log(ynum);
            //Debug.Log(BoxSize);
            //Debug.Log(_RectTransform[x, i].localPosition.y);
            if (ynum - BoxSize / 2 < _RectTransform[x, i].localPosition.y && _RectTransform[x, i].localPosition.y < ynum + BoxSize / 2)//円の上座標<食材の中央座標<円の下座標
            {
                if (ImageBox[x, i].activeSelf)//オブジェクトの表示がtrueである
                {
                    inum = i;
                    MagicHundDispley(x, y);//ママの手を表示
                                           //ImageBox[x, i].SetActive(false);
                    StartCoroutine(GetMagicHund(x, y));//0.5秒後に食材を削除
                }
            }
        }
    }

    private IEnumerator GetMagicHund(int x, int y)
    {
        yield return new WaitForSeconds(0.7f);//0.5秒後に                
        if (ImageBox[x, inum].activeSelf)//オブジェクトの表示がtrueである
        {
            ImageBox[x, inum].SetActive(false);
        }
    }


    private void MagicHundDispley(int x, int y)
    {
        switch (x)
        {
            case 0:
                switch (y)
                {
                    case 0:
                        AnimHund.SetBool("OnHundR1", true);
                        break;
                    case 1:
                        AnimHund.SetBool("OnHundR2", true);
                        break;
                    default:
                        Debug.LogError("ママの手で問題発生_21");
                        break;
                }
                break;
            case 1:
                switch (y)
                {
                    case 0:
                        AnimHund.SetBool("OnHundS1", true);
                        break;
                    case 1:
                        AnimHund.SetBool("OnHundS2", true);
                        break;
                    default:
                        Debug.LogError("ママの手で問題発生_22");
                        break;
                }
                break;
            case 2:
                switch (y)
                {
                    case 0:
                        AnimHund.SetBool("OnHundL1", true);
                        break;
                    case 1:
                        AnimHund.SetBool("OnHundL2", true);
                        break;
                    default:
                        Debug.LogError("ママの手で問題発生_23");
                        break;
                }
                break;
            default:
                Debug.LogError("ママの手で問題発生_24");
                break;
        }
    }


}
