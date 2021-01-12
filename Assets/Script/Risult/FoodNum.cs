using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodNum : MonoBehaviour
{
    private GameObject[,] Food;
    private Image[,] FoodImage;
    private Text[,] FoodText;

    private const int x = 3;
    private const int y = 5;
    // Start is called before the first frame update
    public void CNStart(CuisineClass[] CC, Foodstuff[] FS, int[] t)
    {
        GetOB();//入れるためのゲームオブジェクトを取得
        ChangeImage(CC,FS,t);//
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


    public void ChangeImage(CuisineClass[] CC, Foodstuff[] FS, int[] t)
    {
        
        for (int i = 0; i < Food.GetLength(0); i++)
        {

            for (int l = 0; l < Food.GetLength(1); l++)
            {
                //食材分回す
                for (int k = 0; k < FS.GetLength(0); k++)
                {
                    //料理の食材分回す(6)
                    for (int ip = 0; ip < t.GetLength(0); ip++)
                    {
                        for (int lp = 0; lp < CC[t[ip]].Name.GetLength(0); lp++)
                        {
                            //料理の食材名と食材名が一致した場合は
                            if (CC[t[ip]].Name[lp] == FS[k].Name)
                            {
                                FoodImage[i, l].sprite = FS[k].Material;

                            }
                        }
                    }
                }

            }
        }
    }

    public void ChangeText()
    {

    }

    public void PT()
    {

    }

}
