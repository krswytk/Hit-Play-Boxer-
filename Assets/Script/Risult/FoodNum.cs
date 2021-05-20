using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 画像ボックスを取得
/// 対応画像を入れ込み
/// 未入手食材を黒くする
/// </summary>
public class FoodNum : MonoBehaviour
{
    //生成した素材prefab
    private GameObject[,] Food;
    //の子であるイメージ
    private Image[,] FoodImage;//3,6

    private const int x = 3;
    private const int y = 6;

    private int miss;
    public Sprite[] Irast;
    private GameObject Back;

    // Start is called before the first frame update
    public void CNStart(CuisineClass[] CC, Foodstuff[] FS, int[] t)
    {
        miss = 0;
        Back = GameObject.Find("Background");

        //動かないからとりあえず首
        //LoadIrast();
        Debug.Log("イラストロード");

        GetOB();//入れるためのゲームオブジェクトを取得
        Debug.Log("ゲームオブジェクトを取得");

        ChangeImage(CC,FS,t);//料理に必要な食材画像に入れ替え  同時に入手数から色を変更
        Debug.Log("料理に必要な食材画像に入れ替え  同時に入手数から色を変更");


        ChangeIrast();//背景を入手数に応じて変更
        //Back.GetComponent<Image>().sprite = Irast[1];
        Debug.Log("背景を入手数に応じて変更");

    }

    public void GetOB()
    {
        Food = new GameObject[x, y];
        FoodImage = new Image[x, y];

        for (int i = 1; i < Food.GetLength(0) + 1; i++)
        {

            for (int l = 1; l < Food.GetLength(1) + 1; l++)
            {
                Food[i - 1, l - 1] = GameObject.Find("F" + i + "" + l);
                //Debug.Log(Food[i - 1, l - 1]);
                FoodImage[i - 1, l - 1] = Food[i - 1, l - 1].gameObject.GetComponent<Image>();
                //GameObject childGameObject = Food[i - 1, l - 1].transform.Find("Image").gameObject;
                //Debug.Log(FoodImage[i - 1, l - 1]);
                //GameObject childGameObject = Food[i - 1, l - 1].transform.Find("num").gameObject;
                //Debug.Log(childGameObject.GetComponent<Text>());
                //FoodText[i - 1, l - 1] = Food[i - 1, l - 1].transform.Find("num").gameObject.GetComponent<Text>();
                //Debug.Log(FoodText[i - 1, l - 1]);
                //Debug.Log("Food" + i + "" + l);
            }

        }

        //Debug.Log(Food[0,0]);
    }

    //食材の名前と料理の名前が一致する
    public void ChangeImage(CuisineClass[] CC, Foodstuff[] FS, int[] t)
    {
        Debug.Log("Food.GetLength(0) =" + Food.GetLength(0));
        Debug.Log("Food.GetLength(1) =" + Food.GetLength(1));
        Debug.Log("FS.GetLength(0) =" + FS.GetLength(0));
        Debug.Log("t =" + t.GetLength(0));
        //x列分回す 0,1,2
        for (int i = 0; i < Food.GetLength(0); i++)
        {

            //y列分回す 0-5
            for (int l = 0; l < CC[t[i]].FoodCheck.Length; l++)//料理の食材数回す
            {
                //食材分回す
                for (int k = 0; k < FS.GetLength(0); k++)
                {
                    //Debug.Log(CC[t[i]].Name[l]);
                    //Debug.Log(FS[k].Name);
                    //料理の食材名と食材名が一致した場合は
                    if (CC[t[i]].Name[l] == FS[k].Name)
                    {
                        FoodImage[i, l].sprite = FS[k].Material;
                        //FoodText[i, l].text = FS[k].Getnum.ToString();
                        /*
                        if(FS[k].Getnum == 0)
                        {
                            miss++;
                        }
                        */
                    }


                    /*
                    for (int lp = 0; lp < CC[t[2]].Name.GetLength(0); lp++)
                    {
                        //料理の食材名と食材名が一致した場合は
                        if (CC[t[2]].Name[lp] == FS[k].Name)
                        {
                            FoodImage[i, l].sprite = FS[k].Material;

                        }
                    }
                    for (int lp = 0; lp < CC[t[3]].Name.GetLength(0); lp++)
                    {
                        //料理の食材名と食材名が一致した場合は
                        if (CC[t[3]].Name[lp] == FS[k].Name)
                        {
                            FoodImage[i, l].sprite = FS[k].Material;

                        }
                    }
                    */
                }

                //入手していない食材なら黒に染める
                if (CC[t[i]].FoodCheck[l] == false)
                {
                    FoodImage[i, l].color = new Color(0,0,0,1);
                    miss++;//食材を入手していなければ入手数を加算
                }

            }
        }
    }
    
    private void LoadIrast()
    {
        Irast = new Sprite[3];
        Irast[0] = Resources.Load("ex/R0") as Sprite;
        Irast[1] = Resources.Load("ex/R1") as Sprite;
        Irast[2] = Resources.Load("ex/R2") as Sprite;
        Debug.Log(Irast[0]);

    }

    //入手した食材数に応じて数を変更
    public void ChangeIrast()
    {
        Back.GetComponent<Image>().sprite = Irast[2];

        if (miss >= 5)
        {
            Back.GetComponent<Image>().sprite = Irast[1];
        }

        if (miss >= 10)
        {
            Back.GetComponent<Image>().sprite = Irast[0];
        }
    }

}
