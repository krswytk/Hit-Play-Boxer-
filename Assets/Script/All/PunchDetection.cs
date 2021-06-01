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
    int power;
    //tirle
    SceneSelect SS;
    //main
    MainManeger MM;
    //risult
    RTitleGo RTG;
    //credit
    CTitleGo CTG;


    // Start is called before the first frame update
    void Start()
    {
        power = 0;
        if(SceneManager.GetActiveScene().name == "Title")
        {
            Scenenum = 0;
            SS = GetComponent<SceneSelect>();

        }
        if (SceneManager.GetActiveScene().name == "main")
        {
            Scenenum = 1;
            MM = GetComponent<MainManeger>();
        }
        if (SceneManager.GetActiveScene().name == "Risult")
        {
            Scenenum = 2;
            RTG = GetComponent<RTitleGo>();

        }
        if (SceneManager.GetActiveScene().name == "credit")
        {
            Scenenum = 3;
            CTG = GetComponent<CTitleGo>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            Punch(0,0);

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Punch(1,0);

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Punch(2,0);

        }
        if (Input.GetKeyDown(KeyCode.A))
        {

            Punch(0,1);

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Punch(1,1);

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Punch(2,1);

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {

            Punch(0,2);

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Punch(1,2);

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Punch(2,2);

        }
    }

    /// <summary>
    /// パンチ時に呼び出す 0－2の番号もしくはＬ，Ｓ，Ｒで識別
    /// </summary>
    /// <param name="num">呼び出し時の引数 これでどれが叩かれたのか識別
    public void Punch(int num)
    {
        power = 0;//初期値として1を入れる
        switch (num)
        {
            case 0: PunchL(); break;
            case 1: PunchS(); break;
            case 2: PunchR(); break;
            default: Debug.LogError("パンチで問題発生_01"); break;
        }
    }
    public void Punch(string num)
    {
        power = 0;//初期値として1を入れる
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

    public void Punch(int num,int power)
    {
        if (power == 0 || power == 1 || power == 2)
        {
            this.power = power;
        }
        else
        {
            Debug.LogError("パンチで問題発生_032");
        }
        switch (num)
        {
            case 0: PunchL(); break;
            case 1: PunchS(); break;
            case 2: PunchR(); break;
            default: Debug.LogError("パンチで問題発生_031"); break;
        }
    }
    public void Punch(string num,int power)
    {
        if (power == 0 || power == 1 || power == 2)
        {
            this.power = power;
        }
        else
        {
            Debug.LogError("パンチで問題発生_042");
        }
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
            default: Debug.LogError("パンチで問題発生_041"); break;
        }
    }

    /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void PunchL()
    {
        switch (Scenenum)
        {
            case 0: //タイトルの場合
                SS.Panch();
                break;

            case 1:  //メインの場合
                MM.Panc(0,power);
                break; 

            case 2:  //リザルトの場合
                RTG.downbutton();
                break;

            case 3:  //クレジットの場合
                CTG.Down();
                break;

            default:
                Debug.LogError("パンチで問題発生_05");
                break;
        }

        //Debug.Log("左パンチ");
    }
    void PunchS()
    {
        switch (Scenenum)
        {
            case 0: //タイトルの場合
                SS.Panch();
                break;

            case 1:  //メインの場合
                MM.Panc(1,power);
                break;

            case 2:  //リザルトの場合
                RTG.downbutton();
                break;

            case 3:  //クレジットの場合
                CTG.Down();
                break;

            default:
                Debug.LogError("パンチで問題発生_05");
                break;
        }
        //Debug.Log("中パンチ");

    }
    void PunchR()
    {
        switch (Scenenum)
        {
            case 0: //タイトルの場合
                SS.Panch();
                break;

            case 1:  //メインの場合
                MM.Panc(2,power);
                break;

            case 2:  //リザルトの場合
                RTG.downbutton();
                break;

            case 3:  //クレジットの場合
                CTG.Down();
                break;

            default:
                Debug.LogError("パンチで問題発生_05");
                break;
        }
       // Debug.Log("右パンチ");

    }
}
