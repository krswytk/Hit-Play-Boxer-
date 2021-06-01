using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManeger : MonoBehaviour
{
    GameObject GameStartText;

    GameObject CookElipse;//献立強調の赤丸
    GameObject LimitTImeElipse;//献立強調の赤丸
    GameObject LimitMoneyElipse;//献立強調の赤丸

    Text MainText;//献立を確認
    Text NextCount;//献立を確認

    string[] TutorialText = { "献立を確認", "使う食材を殴れ", "制限時間は120秒！", "所持金は3000円だ！", "では！", "ゲームスタート！"};

    int LimitTime, LimitMonty,count;

    // Start is called before the first frame update
    void TutorialSetUp(int LimitTime,int LimitMonty)//timeは制限時間 Montyは初期所持金
    {
        GameStartText = GameObject.Find("GameStartText");

        CookElipse = GameObject.Find("CookElipse");
        LimitTImeElipse = GameObject.Find("LimitTImeElipse");
        LimitMoneyElipse = GameObject.Find("LimitMoneyElipse");
        
        MainText = GameObject.Find("GameStartText").GetComponent<Text>();
        NextCount = GameObject.Find("NextCount").GetComponent<Text>();

        this.LimitTime = LimitTime;
        this.LimitMonty = LimitMonty;
        count = 0;

        TutorialText[2] = "制限時間は" + LimitTime.ToString() + "秒！";
        TutorialText[3] = "制限時間は" + LimitTime.ToString() + "秒！";

        Close();//オブジェクトを非表示にする。
    }
    
    public void TutorialNext()
    {
        MainText.text = TutorialText[count];
        count++;

    }

    public void Close()
    {
        CookElipse.SetActive(false);
        LimitTImeElipse.SetActive(false);
        LimitMoneyElipse.SetActive(false);
        GameStartText.SetActive(false);
    }
}
