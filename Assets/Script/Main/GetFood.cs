using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 殴って取得するのを実装するscript　
/// 現在は簡易的にキーでの取得を行うものとする
/// キーでの取得はメインのほうに実装するものとし、ここに記述されるのは取得し、画像をfalseにするscriptと、取得したもののFSを＋するものである。
/// 012レーンで判断　入力された場所の該当ポイントの
/// </summary>
/// 
public class GetFood : MonoBehaviour
{
    private bool GFsw = true;
    RectTransform[,] _RectTransform;
    private CuisineClass[] CC;
    private Foodstuff[] FS;
    GameObject[,] ImageBox;
    int Money;//所持金額
    int StartMoney;//所持金額
    int[,] Money_int;//食材格納金額

    CoinManeger CM;//小銭袋残金の表示管理

    //Text Money_Text;
    AudioSource audioSource;
    public AudioClip n;



    ShadowManeger SM;//食材の影管理
    MainManeger MM;


    public void SetUP(GameObject[,] ImageBox , CuisineClass[] CC, Foodstuff[] FS,int Money, int[,] Money_int,int StartMoney)
    {
        this.Money = Money;//所持金を取得
        this.StartMoney = StartMoney;//メモリーを取得
        this.Money_int = Money_int;//メモリーを取得

        audioSource = GetComponent<AudioSource>();
        //Debug.Log(Money.Length);
        MM = GetComponent<MainManeger>();//影の管理スクリプト

        SM = GetComponent<ShadowManeger>();//影の管理スクリプト
        SM.SetUP(CC,FS);

        if (GFsw)
        {
            this.ImageBox = ImageBox;
            this.CC = CC;
            this.FS = FS;
            GFsw = false;

            _RectTransform = new RectTransform[ImageBox.GetLength(0), ImageBox.GetLength(1)];//配列の初期化

            for (int i = 0; i < ImageBox.GetLength(0); i++)//3
            {
                for (int l = 0; l < ImageBox.GetLength(1); l++)//8
                {
                    _RectTransform[i, l] = ImageBox[i, l].GetComponent<RectTransform>();//BOXの位置参照用の取得
                }
            }
        }

        /*
        Money_Text = GameObject.Find("Money_Text").GetComponent<Text>();
        //Debug.Log(Money_int);
        Money_Text.text = (Money.ToString("N0"));
        */

        CM = GetComponent<CoinManeger>();//小銭袋のスクリプトを取得
        CM.CoinSetUp(StartMoney);
    }
    

    private readonly int a = 110;
    private readonly int b = -150;
    private readonly int c = -410;
    private readonly int BoxSize = 130;

    private int num = 0;//名前を数値に変換した際に代入しておく変数
    private int ynum = 0;//縦のどこを取得するか格納しておく変数

    //野菜を取得した際に呼び出す関数
    public void Get(int x,int y)//xは横の位置 yはパンチの強さ つまり高さ
    {
        switch (y)
        {
            case 0: ynum = a; break;
            case 1: ynum = b; break;
            case 2: ynum = c; break;

            default:
                Debug.LogError("食材取得で問題発生_10");
                break;
        }

        if (Money > 0)
        {
            //Debug.Log(x + "レーンの野菜を取得");
            for (int i = 0; i < ImageBox.GetLength(1); i++)//8
            {
                //上の判定
                if (ynum - BoxSize / 2 < _RectTransform[x, i].localPosition.y && _RectTransform[x, i].localPosition.y < ynum + BoxSize / 2)//円の上座標<食材の中央座標<円の下座標
                {
                    if (ImageBox[x, i].activeSelf)//オブジェクトの表示がtrueである
                    {
                        //Debug.Log(int.Parse(ImageBox[x, i].name));
                        num = int.Parse(ImageBox[x, i].name);
                        FS[num].GetFood();//食材を取得したことにして数を増やす
                        Money -= FS[num].NowMoney;//所持金から値段をマイナスする
                        SM.GetCuisineCheck(num);
                        ImageBox[x, i].SetActive(false);//取得したオブジェクトを非表示に
                    }
                }
                /*
                //上の判定
                if (a - BoxSize / 2 < _RectTransform[x, y].localPosition.y && _RectTransform[x, y].localPosition.y < a + BoxSize / 2)//円の上座標<食材の中央座標<円の下座標
                {
                    if (ImageBox[x, y].activeSelf)//オブジェクトの表示がtrueである
                    {
                        Debug.Log(int.Parse(ImageBox[x, y].name));
                        num = int.Parse(ImageBox[x, y].name);
                        FS[num].GetFood();//食材を取得したことにして数を増やす
                        Money[0] -= FS[num].NowMoney;//所持金から値段をマイナスする
                        ImageBox[x, y].SetActive(false);//取得したオブジェクトを非表示に
                    }
                }
                //中の判定
                if (b - BoxSize / 2 < _RectTransform[x, y].localPosition.y && _RectTransform[x, y].localPosition.y < b + BoxSize / 2)//円の上座標<食材の中央座標<円の下座標
                {
                    if (ImageBox[x, y].activeSelf)//オブジェクトの表示がtrueである
                    {
                        num = int.Parse(ImageBox[x, y].name);
                        FS[num].GetFood();//食材を取得したことにして数を増やす
                        Money[0] -= FS[num].NowMoney;//所持金から値段をマイナスする
                        ImageBox[x, y].SetActive(false);//取得したオブジェクトを非表示に
                    }
                }
                //下の判定
                if (c - BoxSize / 2 < _RectTransform[x, y].localPosition.y && _RectTransform[x, y].localPosition.y < c + BoxSize / 2)//円の上座標<食材の中央座標<円の下座標
                {
                    if (ImageBox[x, y].activeSelf)//オブジェクトの表示がtrueである
                    {
                        num = int.Parse(ImageBox[x, y].name);
                        FS[num].GetFood();//食材を取得したことにして数を増やす
                        Money[0] -= FS[num].NowMoney;//所持金から値段をマイナスする
                        ImageBox[x, y].SetActive(false);//取得したオブジェクトを非表示に
                    }
                }
                */
            }
        }

        audioSource.PlayOneShot(n);

        /*
        if (Money > 0)
        {
            Money_Text.text = (Money.ToString("N0"));
        }
        else
        {
            Money_Text.text = (0.ToString("N0"));
        }
        */
        CM.CoinDown(Money);
        //Debug.Log("Money = " + Money);
        if (Money <= 0)
        {
            Debug.Log("リザルト画面の呼び出し");
            MM.Risult();
        }
    }





}
