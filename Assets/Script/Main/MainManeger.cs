using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 基幹部分のソース
/// </summary>

public class MainManeger : MonoBehaviour
{

    CuisineClass[] CC;//料理と食材をまとめて格納するクラス
    Foodstuff[] FS;//食材格納クラス　取得数などを格納
    public int[] t;//ランダムに選ばれた３つの数字を格納しておく
    GameObject[,] ImageBox;//素材表示用のimagegameobject
    int[,] Money_int;//各ボックスに格納されている金額


    CreateClass CrC;//クラスの生成
    RandomCuisine RC;//料理の決定
    InstantiateImageBox IIB;//ボックスの生成
    TransformImageBox TIB;//ボックスの移動
    GetFood GF;//食材の獲得
    PunchDetection PD;//パンチの取得
    MotherHund MH;//妨害の発生


    public int[] Money;//残金　購入できる上限

    private bool SSW;//シーン遷移の管理スイッチ　trueで移動する

    private const int StartMoney = 3000;//最初の所持金

    private float Timer;
    private float MotherHundCoolTime;
    public float MotherHundCoolTimeMax = 3;


    AudioSource _AudioSource;
    public AudioClip _AudioClip;
    private bool sw;

    // Start is called before the first frame update
    void Start()
    {

        _AudioSource = GetComponent<AudioSource>();
        Money = new int[1];
        Money[0] = StartMoney;

        CreateClassStart();//食材クラスと料理クラスの生成
        Debug.Log("クラスの生成完了");

        DecisionCooking();//作る料理の決定
        Debug.Log("作る料理の決定完了");

        CreateImageBox();//動かすImageBoxの生成
        Debug.Log("ImageBoxの生成完了");



        TIB = GetComponent<TransformImageBox>();//食材移動ｓｃｒｉｐｔを取得
        TIB.SetUp(ImageBox, FS);
        Debug.Log("食材移動の準備完了");

        //Money_int = TIB.Money_int;

        GF = GetComponent<GetFood>();
        GF.SetUP(ImageBox, CC, FS, Money, Money_int);//初期設定および変数の参照渡し
        Debug.Log("食材取得の準備完了");

        PD = GetComponent<PunchDetection>();//パンチを取得

        MH = GetComponent<MotherHund>();//パンチを取得
        MH.SetUp(ImageBox);

        SSW = false;
        //Debug.Log(t[1]);
        sw = true;

        Timer = 0;
        MotherHundCoolTime = 5.5f;//ここは絶対に.5であること
    }
    
    void Update()
    {
        //Debug.Log(Money[0]);
        //0-2 RSLでも可
        /*
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //Debug.Log(ImageBox[0, 0].GetComponent<Foodstuff>().Name);

            PD.Punch(0);

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            PD.Punch(1);

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            PD.Punch(2);

        }
        *///PDの方に移植したため削除 場合によってはPDを削除して復活

        Timer += Time.deltaTime;
        //食材強奪の部分
        MotherHundCoolTime -= Time.deltaTime;
        if(MotherHundCoolTime < 0)
        {
            MH.MagicHund(Random.Range(0, 3), Random.Range(0, 2));//ｘ０－２ ｙ０－１
            MotherHundCoolTime = MotherHundCoolTimeMax;
        }



        if (Money[0] <= 0)
        {            //シーン移動の処理はここでのみ行う
            if (sw)
            {
                _AudioSource.PlayOneShot(_AudioClip);
                SceneManager.sceneLoaded += GameSceneLoadedMain;
                feadSC.fade("Risult");
                sw = false;
            }
        }



        TIB.RollmageBox();//ImageBoxを移動させる

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.L))//デバッグ用チートコマンド
        {
            Money[0] = 1;
        }
#endif

    }

    private void CreateClassStart()//食材クラスと料理クラスの生成
    {
        CrC = GetComponent<CreateClass>();//同オブジェクトにアタッチされたCreateClassを取得

        CrC.CreateClassStater();

        CC = CrC.CC;
        FS = CrC.FS;
        //Debug.Log(FS[0].Name);

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
        //Debug.Log(t[0] + "" + t[1] + "" + t[2]);
        t = RC.ReturnT();
        //Debug.Log(t[0] + "" + t[1] + "" + t[2]);
    }

    private void CreateImageBox()//食材を入れておく箱の生成及び反映
    {
        //料理を入れておくimageの作成
        IIB = GetComponent<InstantiateImageBox>();
        IIB.CreateImageBox();//ボックスの生成と初期位置への配置
        ImageBox = IIB.ImageBox;//同時にボックスの生成から初期位置への配置までを行う
        /*//debug
        ImageBox[0, 0].SetActive(false);
        */

    }


    private void GameSceneLoadedMain(Scene next, LoadSceneMode mode)
    {
        //遷移後のリザルトのマネージャーscriptを取得
        RisultManeger RM = GameObject.Find("Main Camera").GetComponent<RisultManeger>();

        RM.StertSetUP(CC,FS,t);


        Debug.Log("シーン遷移完了");
        // イベントから削除
        SceneManager.sceneLoaded -= GameSceneLoadedMain;
    }
}
