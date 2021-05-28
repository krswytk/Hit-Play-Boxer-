using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSelect : MonoBehaviour
{
    private Patrn patrm;
    private Color Choice, NoChoice;
    private Image[] PanchImage;
    private Animator PnchAnimOb;
    // Start is called before the first frame update
    void Start()
    {
        /*
        patrm = Patrn.Sentr;

        Choice = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        NoChoice = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        */

        PanchImage = new Image[3];
        PanchImage[0] = GameObject.Find("Green").GetComponent<Image>();
        PanchImage[1] = GameObject.Find("Red").GetComponent<Image>();
        PanchImage[2] = GameObject.Find("Yellow").GetComponent<Image>();

        PnchAnimOb = GameObject.Find("pnch").GetComponent<Animator>();
        colorChange();
    }
    public enum Patrn
    {
        Leit = 0,
        Sentr = 1,
        Right = 2
    }


    public void SelectR()
    {
        Destroy(PnchAnimOb);
        PanchImage[0].color = new Color(0, 0, 0, 0);
        PanchImage[1].color = new Color(0, 0, 0, 0);
        PanchImage[2].color = new Color(1, 1, 1, 1);
        feadSC.fade("credit");

        /////////////////////////////////////////////////
        /*
        switch (patrm)
        {
            case Patrn.Leit://左 -> 中
                patrm = Patrn.Sentr;
                break;
            case Patrn.Sentr://中 -> 右
                patrm = Patrn.Right;
                break;
            case Patrn.Right://右 -> 左
                patrm = Patrn.Leit;
                break;
        }
        colorChange();
        */
    }
    public void SelectL()
    {

        Destroy(PnchAnimOb);
        PanchImage[0].color = new Color(1, 1, 1, 1);
        PanchImage[1].color = new Color(0, 0, 0, 0);
        PanchImage[2].color = new Color(0, 0, 0, 0);
        ////////////////////////////////////////////
        /*
        switch (patrm)
        {
            case Patrn.Leit://左 -> 右
                patrm = Patrn.Right;
                break;
            case Patrn.Sentr://中 -> 左
                patrm = Patrn.Leit;
                break;
            case Patrn.Right://右 -> 中
                patrm = Patrn.Sentr;
                break;
        }
        colorChange();
        */

    }
    public void SelectS()
    {
        Destroy(PnchAnimOb);
        PanchImage[0].color = new Color(0, 0, 0, 0);
        PanchImage[1].color = new Color(1, 1, 1, 1);
        PanchImage[2].color = new Color(0, 0, 0, 0);
        feadSC.fade("Main");



        ///////////////////////////////////////
        /*
        switch (patrm)
        {
            case Patrn.Leit://左 -> 中
                patrm = Patrn.Sentr;
                Debug.Log("ルール説明画面に移行");
                break;
            case Patrn.Sentr://中 -> メイン画面に
                feadSC.fade("Main");
                break;
            case Patrn.Right://右 -> 左
                patrm = Patrn.Leit;
                feadSC.fade("credit");
                break;
        }
        */

    }

    private void colorChange()
    {
        switch (patrm)
        {
            case Patrn.Leit:
                PanchImage[0].color = Choice;
                PanchImage[1].color = NoChoice;
                PanchImage[2].color = NoChoice;
                break;
            case Patrn.Sentr:
                PanchImage[0].color = NoChoice;
                PanchImage[1].color = Choice;
                PanchImage[2].color = NoChoice;
                break;
            case Patrn.Right:
                PanchImage[0].color = NoChoice;
                PanchImage[1].color = NoChoice;
                PanchImage[2].color = Choice;
                break;
        }
    }
}
