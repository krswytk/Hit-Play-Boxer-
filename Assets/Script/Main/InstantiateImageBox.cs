using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateImageBox : MonoBehaviour
{
    GameObject ImageBoxPrefab;//生成するプレハブを格納
    public GameObject[,] ImageBox { get; private set; }
    GameObject Canvas;

    public int x = 3;
    public int y = 7;


    public void CreateImageBox()
    {
        InstantiatePlefab();//
        TransformStartPosition();

    }

    void InstantiatePlefab()//imageboxのprefabを生成する
    {
        ImageBoxPrefab = Resources.Load("Prefab/P") as GameObject; // Resouces下のCSV読み込み
        ImageBox = new GameObject[x, y];
        Canvas = GameObject.Find("Canvas");
        for (int i = 0; i < ImageBox.GetLength(0); i++)//3
        {
            for (int l = 0; l < ImageBox.GetLength(1); l++)
            {//8
                ImageBox[i, l] = Instantiate(ImageBoxPrefab, new Vector2(0, 0), Quaternion.identity, Canvas.transform);//Canvasの子にする
                ImageBox[i, l].name = i + "" + l;
            }
        }
    }

    RectTransform recttransform;
    void TransformStartPosition()//imageboxを初期位置に配置する
    {
        int num = Screen.width / 6;
        int x = 0;
        int y = 0;
        for (int i = 0; i < ImageBox.GetLength(0); i++)//3
        {
            for (int l = 0; l < ImageBox.GetLength(1); l++)//8
            {
                recttransform = ImageBox[i, l].GetComponent<RectTransform>();//これ重い　無駄な処理になっているのでメインで統合したほうが良い

                switch (i)
                {
                    case 0: x = -1 * num * 2;  break;
                    case 1: x =  0;            break;
                    case 2: x =  1 * num * 2;  break;
                    default: Debug.LogError("ImageBox配置で想定外のエラー発生"); break;
                }
                y = -605 + 130 * l;

                recttransform.localPosition = new Vector2(x, y);
            }
        }
    }
}
