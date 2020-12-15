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


    CreateClass CrC;
    RandomCuisine RC;
    InstantiateImageBox IIB;
    TransformImageBox TIM;


    // Start is called before the first frame update
    void Start()
    {
        CreateClassStart();

        DecisionCooking();



        //料理を入れておくimageの作成
        IIB = GetComponent<InstantiateImageBox>();
        IIB.CreateImageBox();
        ImageBox = IIB.GetImageBox();//同時にボックスの生成から初期位置への配置までを行う
        /*//debug
        ImageBox[0, 0].SetActive(false);
        */


        TIM = GetComponent<TransformImageBox>();
    }

    private bool SetUp = true;//updateで一回だけ呼び出す
    void Update()
    {
        if (SetUp)//updateで一回のみ呼び出したいループ類似処理
        {
            TIM.SetUp(ImageBox);

        }
        TIM.RollmageBox(ImageBox);//アイテムを移動させる


    }

    private void CreateClassStart()
    {
        CrC = GetComponent<CreateClass>();//同オブジェクトにアタッチされたCreateClassを取得

        CrC.CreateClassStater();

        CC = CrC.C;
        FS = CrC.FS;

        Debug.Log(this.CC[0].CuisineName);
        Debug.Log(this.CC.GetLength(0));
        Debug.Log(this.FS[0].Name);
        Debug.Log(this.FS[2].Name);
        Debug.Log(this.FS[7].Name);
    }

    private void DecisionCooking()
    {
        RC = GetComponent<RandomCuisine>();
        RC.RandomCuisineStart(CC, t);//今回殴り作る料理の決定
    }

    }
