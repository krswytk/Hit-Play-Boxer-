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
    Clock C;//時計の表示の発生
    SlotManeger SM;//スロット系の管理
    TutorialManeger TM;//スロット系の管理
    private float TutorialTimer; 
    private const float TutorialTimerMax = 3;

    public int Money;//残金　購入できる上限

    private const int StartMoney = 3000;//最初の所持金


    private float LimitTime = 60f;
    private float Timer;

    private float MotherHundCoolTime;
    public float MotherHundCoolTimeMax = 3;


    AudioSource _AudioSource;
    public AudioClip _AudioClip;

    private bool sw;
    

    private enum Stage
    {
        Lot = 1,//ルーレットフェーズ
        Tutorial = 2,//説明フェーズ
        Main = 3//食材獲得フェーズ
    }

    Stage _Stage;

    // Start is called before the first frame update
    void Start()
    {

        _AudioSource = GetComponent<AudioSource>();
        Money = StartMoney;

        _Stage = Stage.Lot;

        CreateClassStart();//食材クラスと料理クラスの生成
        Debug.Log("クラスの生成完了");

        RC = GetComponent<RandomCuisine>();
        //DecisionCooking();//作る料理の決定
        //Debug.Log("作る料理の決定完了");

        CreateImageBox();//動かすImageBoxの生成
        Debug.Log("ImageBoxの生成完了");


        GF = GetComponent<GetFood>();

        TIB = GetComponent<TransformImageBox>();//食材移動ｓｃｒｉｐｔを取得
        TIB.SetUp(ImageBox, FS);
        Debug.Log("食材移動の準備完了");

        //Money_int = TIB.Money_int;


        PD = GetComponent<PunchDetection>();//パンチを取得

        MH = GetComponent<MotherHund>();//主婦の手を取得
        MH.SetUp(ImageBox);

        C = GetComponent<Clock>();//時計の表示スクリプトを取得
        C.TimerSetUp(LimitTime);

        SM = GetComponent<SlotManeger>();//時計の表示スクリプトを取得
        SM.SlotSetUp(CC);

        TM = GetComponent<TutorialManeger>();//チュートリアル表示管理スクリプトを取得

        //Debug.Log(t[1]);
        sw = true;

        Timer = 0;
        MotherHundCoolTime = 5.5f;//ここは絶対に.5であること
        TutorialTimer = 0;
    }


    private void Update()//即時に判断が必要なものはこっちに
    {

        Timer += Time.deltaTime;
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.B))//デバッグ用チートコマンド
        {
            _Stage = Stage.Lot;
        }
        if (Input.GetKeyDown(KeyCode.N))//デバッグ用チートコマンド
        {
            _Stage = Stage.Tutorial;
        }
        if (Input.GetKeyDown(KeyCode.M))//デバッグ用チートコマンド
        {
            _Stage = Stage.Main;
        }
#endif

        switch (_Stage)
        {
            ///////////////////////////////////////////////////////////////////////////////////////////
            case Stage.Lot:

                break;

            ///////////////////////////////////////////////////////////////////////////////////////////
            case Stage.Tutorial:
                TutorialTimer += Time.deltaTime;
                if (TutorialTimer > TutorialTimerMax)
                {
                    TM.TutorialNext();
                    TutorialTimer = 0;
                }
                break;

            ///////////////////////////////////////////////////////////////////////////////////////////
            case Stage.Main:

                //食材強奪の部分
                MotherHundCoolTime -= Time.deltaTime;
                if (MotherHundCoolTime < 0)
                {
                    MH.MagicHund(Random.Range(0, 3), Random.Range(0, 2));//ｘ０－２ ｙ０－１
                    MotherHundCoolTime = MotherHundCoolTimeMax;
                }

                C.ClockMove(Timer);//時計を動かす

                if (LimitTime <= Timer)//制限時間を超えたら
                {
                    Risult();
                }

                //Debug.Log("Money = " + Money);

                if (Money <= 0)
                {            //シーン移動の処理はここでのみ行う
                    Risult();
                }
                break;
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            default: Debug.LogError("メインのUpdateでエラー"); break;

        }

    }

    private void FixedUpdate()//落下などの一定の処理速度を要求するものはこっちに
    {
        #region PuncTest
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
        #endregion



        switch (_Stage)
        {
            ///////////////////////////////////////////////////////////////////////////////////////////
            case Stage.Lot:

                SM.SlotOn();//ルーレットの回転
                break;

            ///////////////////////////////////////////////////////////////////////////////////////////
            case Stage.Tutorial:
                
                break;

            ///////////////////////////////////////////////////////////////////////////////////////////
            case Stage.Main:
                TIB.RollmageBox();//ImageBoxを移動させる
                break;
            ///////////////////////////////////////////////////////////////////////////////////////////
            default:Debug.LogError("メインのFixdでエラー"); break;
        }
        
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

    public void Risult()//タイプアップの処理
    {
        if (sw)
        {
            Debug.Log("リザルトに遷移");
            _AudioSource.PlayOneShot(_AudioClip);
            SceneManager.sceneLoaded += GameSceneLoadedMain;
            feadSC.fade("Risult");
            sw = false;
        }
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

    public void Panc(int x ,int power)
    {
        switch (_Stage)
        {
            ///////////////////////////////////////////////////////////////////////////////////////////
            case Stage.Lot:
                t = SM.Select();
                _Stage = Stage.Tutorial;
                GF.SetUP(ImageBox, CC, FS, Money, Money_int, StartMoney);//初期設定および変数の参照渡し
                Debug.Log("食材取得の準備完了");
                RC.CookingDisplay(CC, t);
                break;

            ///////////////////////////////////////////////////////////////////////////////////////////
            case Stage.Tutorial:
                TM.TutorialNext();
                _Stage = Stage.Main;
                break;

            ///////////////////////////////////////////////////////////////////////////////////////////
            case Stage.Main:
                switch (x){
                    case 0:
                        GF.Get(0, power);
                        break;
                    case 1:
                        GF.Get(1, power);
                        break;
                    case 2:
                        GF.Get(2, power);
                        break;
                    default: Debug.LogError("メインのパンチ取得でエラー"); break;
                }
                break;
            ///////////////////////////////////////////////////////////////////////////////////////////
            default: Debug.LogError("パンチステージでエラー"); break;
        }

    }
}
