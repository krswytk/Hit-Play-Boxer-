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
    private Foodstuff[] FS;
    GameObject[,] ImageBox;
    int Money;
    int[,] Money_int;

    Text Money_Text;

    public void SetUP(GameObject[,] ImageBox , Foodstuff[] FS,ref int Money, int[,] Money_int)
    {
        this.Money = Money;
        this.Money_int = Money_int;
        if (GFsw)
        {
            this.ImageBox = ImageBox;
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

        Money_Text = GameObject.Find("Money_Text").GetComponent<Text>();
        //Debug.Log(Money_int);
        Money_Text.text = (Money.ToString("N0"));
    }
    

    private readonly int a = 110;
    private readonly int b = -150;
    private readonly int c = -410;
    private readonly int BoxSize = 130;

    public void Get(int x)
    {
        Debug.Log(x + "レーンの野菜を取得");
        for(int y = 0; y < ImageBox.GetLength(1); y++)
        {
            if (_RectTransform[x, y].localPosition.y < a + BoxSize / 2 && _RectTransform[x, y].localPosition.y > a - BoxSize / 2)
            {
                if (ImageBox[x, y].activeSelf)//オブジェクトの表示がtrueである
                {
                    FS[int.Parse(ImageBox[x, y].name)].GetFood();//食材を取得したことにして数を増やす
                    Money -= Money_int[x, y];//所持金から値段をマイナスする
                    ImageBox[x, y].SetActive(false);//取得したオブジェクトを非表示に
                }
            }
            if (_RectTransform[x, y].localPosition.y < b + BoxSize / 2 && _RectTransform[x, y].localPosition.y > b - BoxSize / 2)
            {
                if (ImageBox[x, y].activeSelf)//オブジェクトの表示がtrueである
                {
                    FS[int.Parse(ImageBox[x, y].name)].GetFood();//食材を取得したことにして数を増やす
                    Money -= Money_int[x, y];//所持金から値段をマイナスする
                    ImageBox[x, y].SetActive(false);//取得したオブジェクトを非表示に
                }
            }
            if (_RectTransform[x, y].localPosition.y < c + BoxSize / 2 && _RectTransform[x, y].localPosition.y > c - BoxSize / 2)
            {
                if (ImageBox[x, y].activeSelf)//オブジェクトの表示がtrueである
                {
                    FS[int.Parse(ImageBox[x, y].name)].GetFood();//食材を取得したことにして数を増やす
                    Money -= Money_int[x, y];//所持金から値段をマイナスする
                    ImageBox[x, y].SetActive(false);//取得したオブジェクトを非表示に
                }
            }
        }

        if (Money <= 0)
        {
            SceneManager.LoadScene("Risult");
        }


        Money_Text.text = (Money.ToString("N0"));

    }

    private void Scene()
    {

    }
    
}
