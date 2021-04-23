using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// パンチされたことを検知する関数。Ｌ（左）Ｓ（中）Ｒ（右）を呼び出す。
/// </summary>
public class PunchDetection : MonoBehaviour
{
    int Scenenum;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Title")
        {
            Scenenum = 0;

        }
        if (SceneManager.GetActiveScene().name == "main")
        {
            Scenenum = 1;

        }
        if (SceneManager.GetActiveScene().name == "Risult")
        {
            Scenenum = 2;

        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// パンチ時に呼び出す 0－2の番号もしくはＬ，Ｓ，Ｒで識別
    /// </summary>
    /// <param name="num">呼び出し時の引数 これでどれが叩かれたのか識別
    void Punch(int num)
    {
        switch (num)
        {
            case 0: PunchL(); break;
            case 1: PunchS(); break;
            case 2: PunchR(); break;
            default: Debug.LogError("パンチで問題発生_01"); break;
        }
    }
    void Punch(string num)
    {
        switch (num[0])
        {
            //大文字
            case 'L': PunchL(); break;
            case 'S': PunchS(); break;
            case 'R': PunchR(); break;
            //小文字
            case 'l': PunchL(); break;
            case 's': PunchS(); break;
            case 'r': PunchR(); break;
            default: Debug.LogError("パンチで問題発生_02"); break;
        }
    }

    void PunchL()
    {
        switch (Scenenum)
        {
            case 0: //タイトルの場合
                break;

            case 1:  //メインの場合
                break;

            case 2:  //リザルトの場合
                break;

            default:
                Debug.LogError("パンチで問題発生_03");
                break;
        }
    }
    void PunchS()
    {

    }
    void PunchR()
    {

    }
}
