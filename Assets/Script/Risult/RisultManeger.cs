using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RisultManeger : MonoBehaviour
{
    private CuisineClass[] CC;//料理と食材をまとめて格納するクラス
    private Foodstuff[] FS;//食材格納クラス　取得数などを格納
    private int[] t;//ランダムに選ばれた３つの数字を格納しておく

    private CookingDisplay CD;//料理の表示を行う関数群を格納した関数
    private FoodNum FN;//料理の表示を行う関数群を格納した関数


    [SerializeField] GameObject[] ScoreTexts;
    [SerializeField] Text[] ScoreName;
    [SerializeField] Text[] ScorePer; 
    [SerializeField] Text EvaluationText;
    [SerializeField] Outline EvaluationOutline;

    [SerializeField] Animation ScoreTextAnim;

    [SerializeField] GameObject TitleGoText;


    AudioSource audioSource;

    private float MoneyPer;//残金率
    private float TimePer;//残り時間率

    private bool AnimSw;//アニメーションを行っているかどうかの判定

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            DecisionCooking();//制作する料理の表示を行う
                              //Debug.Log(FS.GetLength(0));
            GetFoodNum();//入手した食材の量を表示する
            ClearScoreText();
        }
        catch { }
        AnimSw = true;
        audioSource = GetComponent<AudioSource>();
        //Debug.Log(audioSource);
    }

    /// <summary>
    /// メインからリザルトに必要なクラス、値を持ってくる
    /// メインから呼び出す関数 メインで取得したデータをもらう
    /// </summary>
    /// <param name="CC"></param>
    /// <param name="FS"></param>
    /// <param name="t"></param>
    public void StertSetUP(CuisineClass[] CC,Foodstuff[] FS,int[] t,float MoneyPer,float TimePer)
    {
        this.CC = CC;
        this.FS = FS;
        this.t = t;
        this.MoneyPer = MoneyPer;
        this.TimePer = TimePer;


        string StringCC = ""; 
        string StringFS = "";
        string Stringt = "";
        for (int i = 0; i< CC.Length; i++)
        {
            StringCC += CC[i].CuisineName + ", ";
        }
        for (int i = 0; i < FS.Length; i++)
        {
            StringFS += FS[i].Name + ", ";
        }
        for (int i = 0; i < t.Length; i++)
        {
            Stringt += t[i].ToString() + ", ";
        }
        /*Debug.Log(StringCC);
        Debug.Log(StringFS);
        Debug.Log(Stringt);
        */

        Debug.Log("MoneyPer = " + MoneyPer);
        Debug.Log("TimePer = " + TimePer);
    }


    private void DecisionCooking()//作る料理の表示
    {
        CD = GetComponent<CookingDisplay>();
        CD.CDStert(CC,t);
    }

    private void GetFoodNum()//入手した食材を色を付けて表示
    {
        FN = GetComponent<FoodNum>();
        FN.CNStart(CC, FS, t);
    }

    private void ClearScoreText()
    {
        float Total = 0;
        for (int i = 0; i < 3; i++){
            ScoreName[i].text = CC[t[i]].CuisineName;//スコア 料理名表示
            ScorePer[i].text = CC[t[i]].GetPer().ToString() + "%";//スコアパーセントを表示
            Total += CC[t[i]].GetPer();
        }
        ScorePer[3].text = Mathf.Ceil(MoneyPer).ToString() + "%";//残金率//切り上げ
        Total += Mathf.Ceil(MoneyPer);
        ScorePer[4].text = (100 - Mathf.Ceil(TimePer)).ToString() + "%";//残り時間率//切り上げ
        Total += (100 - Mathf.Ceil(TimePer));
        ScorePer[5].text = Mathf.Ceil(Total).ToString() + "%";
        #region ColorSetting
        if (Total > 0)
        {
            EvaluationText.text = "E";
            EvaluationText.color = new Color(252 / 255f, 223 / 255f, 255 / 255f, 1);
            EvaluationOutline.effectColor = new Color(170 / 255f, 0, 1, 1);
        }
        if (Total > 100)
        {
            EvaluationText.text = "D";
            EvaluationText.color = new Color(223 / 255f, 240 / 255f, 255 / 255f, 1);
            EvaluationOutline.effectColor = new Color(0, 115 / 255f, 1, 1);
        }
        if (Total > 149)
        {
            EvaluationText.text = "C";
            EvaluationText.color = new Color(230 / 255f, 255 / 255f, 223 / 255f, 1);
            EvaluationOutline.effectColor = new Color(24 / 255f, 255 / 255f, 0, 1);
        }
        if (Total > 200)
        {
            EvaluationText.text = "B";
            EvaluationText.color = new Color(255 / 255f, 223 / 255f, 239 / 255f, 1);
            EvaluationOutline.effectColor = new Color(255 / 255f, 0, 87 / 255f,  1);
        }
        if (Total > 249)
        {
            EvaluationText.text = "A";
            EvaluationText.color = new Color(255 / 255f, 224 / 255f, 186 / 255f, 1);
            EvaluationOutline.effectColor = new Color(255 / 255f, 81 / 255f, 0, 1);
        }
        if (Total > 299)
        {
            EvaluationText.text = "S";
            EvaluationText.color = new Color(248 / 255f, 255 / 255f, 150 / 255f, 1);
            EvaluationOutline.effectColor = new Color(252 / 255f, 205 / 255f, 0, 1);
        }
        if (Total > 399)
        {
            EvaluationText.text = "SS";
            EvaluationText.color = new Color(252 / 255f, 255 / 255f, 194 / 255f, 1);
            EvaluationOutline.effectColor = new Color(252 / 255f, 205 / 255f, 0, 1);
        }
        #endregion
    }
    private void Update()
    {
        #region DebugKey
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EvaluationText.text = "E";
            EvaluationText.color = new Color(252 / 255f, 223 / 255f, 255 / 255f, 1);
            EvaluationOutline.effectColor = new Color(170 / 255f, 0, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EvaluationText.text = "D";
            EvaluationText.color = new Color(223 / 255f, 240 / 255f, 255 / 255f, 1);
            EvaluationOutline.effectColor = new Color(0, 115 / 255f, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EvaluationText.text = "C";
            EvaluationText.color = new Color(230 / 255f, 255 / 255f, 223 / 255f, 1);
            EvaluationOutline.effectColor = new Color(24 / 255f, 255 / 255f, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EvaluationText.text = "B";
            EvaluationText.color = new Color(255 / 255f, 223 / 255f, 239 / 255f, 1);
            EvaluationOutline.effectColor = new Color(255 / 255f, 0, 87 / 255f, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            EvaluationText.text = "A";
            EvaluationText.color = new Color(255 / 255f, 224 / 255f, 186 / 255f, 1);
            EvaluationOutline.effectColor = new Color(255 / 255f, 81 / 255f, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            EvaluationText.text = "S";
            EvaluationText.color = new Color(248 / 255f, 255 / 255f, 150 / 255f, 1);
            EvaluationOutline.effectColor = new Color(252 / 255f, 205 / 255f, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            EvaluationText.text = "SS";
            EvaluationText.color = new Color(252 / 255f, 255 / 255f, 194 / 255f, 1);
            EvaluationOutline.effectColor = new Color(252 / 255f, 205 / 255f, 0, 1);
        }
        #endregion
    }

    public void PanchRisult()
    {
        if (AnimSw)//もしアニメーション再生中なら
        {
            for (int i = 0; i < ScoreTexts.Length; i++)
            {
                ScoreTexts[i].SetActive(true);
            }
            Destroy(ScoreTextAnim);
            audioSource.Play();
            TitleGoText.SetActive(true);
            AnimSw = false;
        }
        else
        {
            audioSource.Play();
            feadSC.fade("Title");
        }
    }
    public void AnimEnd()
    {
        TitleGoText.SetActive(true);
        AnimSw = false;
    }
}

