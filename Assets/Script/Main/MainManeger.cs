using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MainManeger : MonoBehaviour
{

    CuisineClass[] CC;//料理と食材をまとめて格納するクラス
    Foodstuff[] FS;//食材格納クラス　取得数などを格納
    public int[] t;//ランダムに選ばれた３つの数字を格納しておく
    GameObject[,] ImageBox;//素材表示用のimagegameobject
    int[,] Money_int;


    CreateClass CrC;
    RandomCuisine RC;
    InstantiateImageBox IIB;
    TransformImageBox TIB;
    GetFood GF;


    public int Money;//残金　購入できる上限

    // Start is called before the first frame update
    void Start()
    {
        Money = 1000;

        CreateClassStart();//食材クラスと料理クラスの生成

        DecisionCooking();//作る料理の決定

        CreateImageBox();//動かすImageBoxの生成



        TIB = GetComponent<TransformImageBox>();
        TIB.SetUp(ImageBox, FS);
        Money_int = TIB.Money_int;

        GF = GetComponent<GetFood>();
        GF.SetUP(ImageBox,FS,Money, Money_int);//初期設定および変数の参照渡し

    }
    
    void Update()
    {

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

        TIB.RollmageBox();//ImageBoxを移動させる


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


}
