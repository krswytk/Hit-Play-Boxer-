using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingDisplay : MonoBehaviour
{

    /// <summary>
    /// 表示を行うアクセス関数
    /// </summary>
    public void CDStert(CuisineClass[] CC,int[] t)
    {
        //Debug.Log("デバッグ");
        SetOB(CC,t);
    }


    /// <summary>
    /// 画像を格納するためのImageの取得を行う
    /// </summary>
    private void SetOB(CuisineClass[] CC, int[] t)
    {
        try
        {
            for (int i = 0; i < 3; i++)
            {
                Image cuisine = GameObject.Find("CI" + (i + 1)).gameObject.GetComponent<Image>();//料理欄の料理オブジェクトを右から順に取得する
                cuisine.sprite = CC[t[i]].CuisineMaterial;
            }
        }
        catch
        {
            Debug.LogError("CookingDisplayでエラー");
        }
    }
}
