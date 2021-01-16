using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodNum : MonoBehaviour
{
    //生成した素材prefab
    private GameObject[,] Food;
    //の子であるイメージ
    private Image[,] FoodImage;
    //の子であるテキスト
    private Text[,] FoodText;

    private const int x = 3;
    private const int y = 5;

    private int miss;
    public Sprite[] Irast;
    public GameObject Back;
    // Start is called before the first frame update
    public void CNStart(CuisineClass[] CC, Foodstuff[] FS, int[] t)
    {
        GetOB();//入れるためのゲームオブジェクトを取得
        miss = 0;
        ChangeImage(CC,FS,t);//

        //Irast = new Sprite[3];


        ChangeIrast();//

    }

    public void GetOB()
    {
        Food = new GameObject[x, y];
        FoodImage = new Image[x, y];
        FoodText = new Text[x, y];
        for (int i = 1; i < Food.GetLength(0) + 1; i++)
        {

            for (int l = 1; l < Food.GetLength(1) + 1; l++)
            {
                Food[i - 1, l - 1] = GameObject.Find("Food" + i + "" + l);
                //Debug.Log(Food[i - 1, l - 1]);
                FoodImage[i - 1, l - 1] = Food[i - 1, l - 1].transform.Find("Image").gameObject.GetComponent<Image>();
                //GameObject childGameObject = Food[i - 1, l - 1].transform.Find("Image").gameObject;
                //Debug.Log(FoodImage[i - 1, l - 1]);
                //GameObject childGameObject = Food[i - 1, l - 1].transform.Find("num").gameObject;
                //Debug.Log(childGameObject.GetComponent<Text>());
                FoodText[i - 1, l - 1] = Food[i - 1, l - 1].transform.Find("num").gameObject.GetComponent<Text>();
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

            //y列分回す 0-4
            for (int l = 0; l < Food.GetLength(1); l++)
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
                        FoodText[i, l].text = FS[k].Getnum.ToString();

                        if(FS[k].Getnum == 0)
                        {
                            miss++;
                        }
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

            }
        }
    }

    /*
    public void ChangeText()
    {

    }
    */

    public void ChangeIrast()
    {
        if(miss < 2)
        {
            Back.GetComponent<Image>().sprite = Irast[0];
        }
        if (miss < 4)
        {
            Back.GetComponent<Image>().sprite = Irast[1];

        }
        else
        {
            Back.GetComponent<Image>().sprite = Irast[2];

        }
    }

}
