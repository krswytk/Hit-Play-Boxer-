using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManeger : MonoBehaviour
{

    CuisineClass[] CC;//料理と食材をまとめて格納するクラス
    Foodstuff[] FS;//食材格納クラス　取得数などを格納
    public int[] t;//ランダムに選ばれた３つの数字を格納しておく
    GameObject[,] ImageBox;//素材表示用のimagegameobject
    int[,] Money_int;//各ボックスに格納されている金額


    CreateClass CrC;
    RandomCuisine RC;
    InstantiateImageBox IIB;
    TransformImageBox TIB;
    GetFood GF;


    public int[] Money;//残金　購入できる上限

    private bool SSW;//シーン遷移の管理スイッチ　trueで移動する

    private const int StartMoney = 1000;

   // Start is called before the first frame update
   void Start()
    {

        Money = new int[1];
        Money[0] = StartMoney;

        CreateClassStart();//食材クラスと料理クラスの生成

        DecisionCooking();//作る料理の決定

        CreateImageBox();//動かすImageBoxの生成



        TIB = GetComponent<TransformImageBox>();
        TIB.SetUp(ImageBox, FS);
        Money_int = TIB.Money_int;

        GF = GetComponent<GetFood>();
        GF.SetUP(ImageBox, CC, FS, Money, Money_int);//初期設定および変数の参照渡し

        SSW = false;

    }
    
    void Update()
    {
        Debug.Log(Money[0]);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //Debug.Log(ImageBox[0, 0].GetComponent<Foodstuff>().Name);

            GF.Get(0);

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GF.Get(1);

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GF.Get(2);

        }
        

        if (Money[0] <= 0)
        {            //シーン移動の処理はここでのみ行う
            SceneManager.sceneLoaded += GameSceneLoaded;
            SceneManager.LoadScene("Risult");
        }



        TIB.RollmageBox();//ImageBoxを移動させる

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.L))
        {
            Money[0] = 1;
        }
#endif

    }

    private void CreateClassStart()//食材クラスと料理クラスの生成
    {
        CrC = GetComponent<CreateClass>();//同オブジェクトにアタッチされたCreateClassを取得

        CrC.CreateClassStater();

        CC = CrC.C;
        FS = CrC.FS;

        /*
        Debug.Log(this.CC[0].CuisineName);
        Debug.Log(this.CC.GetLength(0));
        Debug.Log(this.FS[0].Name);
        Debug.Log(this.FS[2].Name);
        Debug.Log(this.FS[7].Name);
        */
    }

    private void DecisionCooking()//作る料理の決定
    {
        RC = GetComponent<RandomCuisine>();
        RC.RandomCuisineStart(CC, t);//今回殴り作る料理の決定と画面の料理を更新
    }

    private void CreateImageBox()//作る料理の決定
    {
        //料理を入れておくimageの作成
        IIB = GetComponent<InstantiateImageBox>();
        IIB.CreateImageBox();
        ImageBox = IIB.ImageBox;//同時にボックスの生成から初期位置への配置までを行う
        /*//debug
        ImageBox[0, 0].SetActive(false);
        */

    }


    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        //遷移後のリザルトのマネージャーscriptを取得
        RisultManeger RM = GameObject.Find("Main Camera").GetComponent<RisultManeger>();

        RM.StertSetUP(CC,FS,t);


        Debug.Log("シーン遷移完了");
        // イベントから削除
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}
