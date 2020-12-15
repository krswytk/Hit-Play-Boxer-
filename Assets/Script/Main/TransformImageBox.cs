using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// アイテムの移動　アイテムの生成を行うクラス関数
/// </summary>
public class TransformImageBox : MonoBehaviour
{
    RectTransform[,] recttransform;

    public void SetUp(GameObject[,] ImageBox)
    {
        recttransform = new RectTransform[ImageBox.GetLength(0), ImageBox.GetLength(1)];
        for (int i = 0; i < ImageBox.GetLength(0); i++)//3
        {
            for (int l = 0; l < ImageBox.GetLength(1); l++)//8
            {
                recttransform[i, l] = ImageBox[i, l].GetComponent<RectTransform>();
            }
        }
    }

    public float speed = 1;
    public void RollmageBox(GameObject[,] ImageBox)//アイテムの移動を行う
    {
        for (int i = 0; i < ImageBox.GetLength(0); i++)//3
        {
            for (int l = 0; l < ImageBox.GetLength(1); l++)//8
            {
                Vector2 V2 = recttransform[i, l].localPosition * 1.0f;
                //if (i == 0 && l == 0) Debug.Log(V2);
                if (V2.y <= -640)//下まで行った場合の処理
                {
                    recttransform[i, l].localPosition = new Vector2(V2.x, 399);
                }
                else
                {
                    V2.y -= speed;
                    recttransform[i, l].localPosition = V2;
                }


            }
        }


    }

    void InputBox(int n)//上に移動したBoxにアイテムを入れ込む
    {

    }

}
