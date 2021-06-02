using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManeger : MonoBehaviour
{
    GameObject GameStartText;

    GameObject CookElipse;//献立強調の赤丸
    GameObject GetFoodElipse;//獲得場所強調の赤丸
    GameObject LimitTImeElipse;//制限時間強調の赤丸
    GameObject LimitMoneyElipse;//所持金強調の赤丸

    Text MainText;//献立を確認
    Text NextCount;//献立を確認

    string[] TutorialText = { "献立を確認", "献立に使う食材を予想", "使う食材を殴れ", "制限時間は24時間", "針は0時からスタート", "所持金は3000円だ！", "お金は残した方がいいぞ", "14時と20時から特売", "ゲームスタート！",""};

    int LimitTime, LimitMonty,count;
    float SkipTime;

    // Start is called before the first frame update
    public void TutorialSetUp(int LimitTime,int LimitMonty,float SkipTime)//timeは制限時間 Montyは初期所持金
    {
        GameStartText = GameObject.Find("GameStartText");

        CookElipse = GameObject.Find("CookElipse");
        GetFoodElipse = GameObject.Find("GetFoodElipse");
        LimitTImeElipse = GameObject.Find("LimitTImeElipse");
        LimitMoneyElipse = GameObject.Find("LimitMoneyElipse");
        
        MainText = GameObject.Find("MainText").GetComponent<Text>();
        NextCount = GameObject.Find("NextCount").GetComponent<Text>();

        this.LimitTime = LimitTime;
        this.LimitMonty = LimitMonty;
        this.SkipTime = SkipTime;
        count = 0;

        //TutorialText[3] = "制限時間は" + LimitTime.ToString() + "秒！";
        TutorialText[5] = "所持金は" + LimitMonty.ToString() + "円だ！";

        Close();//オブジェクトを非表示にする。
    }
    
    public bool TutorialNext()
    {
        MainText.text = TutorialText[count];
        //"献立を確認", "献立に使う食材を予想", "使う食材を殴れ", "制限時間は120秒！", 
        //"針が2周するまで", "所持金は3000円だ！", "14時と20時から特売", "ゲームスタート！"
        switch (count)
        {
            case 0://献立を確認
                GameStartText.SetActive(true);//チュートリアル全体表示ON
                CookElipse.SetActive(true);//献立ON
                break;
            case 1://献立に使う食材を予想
                CookElipse.SetActive(false);//献立OFF
                GetFoodElipse.SetActive(true);//食材ON
                break;
            case 2://使う食材を殴れ
                break;
            case 3://制限時間は120秒
                GetFoodElipse.SetActive(false);//食材OFF
                LimitTImeElipse.SetActive(true);//時間ON
                break;
            case 4://針が2周するまで
                break;
            case 5://所持金は3000円だ
                LimitTImeElipse.SetActive(false);//時間OFF
                LimitMoneyElipse.SetActive(true);
                break;
            case 6://お金は残した方がいいぞ
                break;
            case 7://14時と20時から特売
                break;
            case 8://ゲームスタート
                LimitMoneyElipse.SetActive(false);//所持金OFF
                break;
            case 9:
                GameStartText.SetActive(false);//チュートリアル全体表示OFF
                break;
            default: Debug.LogError("チュートリアルネクスト場合分けでエラー"); break;
        }
        count++;
        //Debug.Log(count +"  "+ TutorialText.Length);
        if (count >= TutorialText.Length)
        {
            Debug.Log("チュートリアル終了");
            return true;//チュートリアルテキストを流し終わったら
        }
        return false;
    }

    float Timer;
    public void TutorialSlipCountText(float TimerSkip)
    {
        Timer = SkipTime - TimerSkip;
        NextCount.text = Mathf.Ceil(Timer).ToString();
    }

    public void Close()
    {
        CookElipse.SetActive(false);
        GetFoodElipse.SetActive(false);
        LimitTImeElipse.SetActive(false);
        LimitMoneyElipse.SetActive(false);
        GameStartText.SetActive(false);
    }
}
