using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManeger : MonoBehaviour
{
    CuisineClass[] CC;//料理と食材をまとめて格納するクラス

    GameObject Canvas;//ルーレットが乗っているキャンバス

    GameObject SlotBer;//生成するプレハブオブジェクトバー

    GameObject Bers;//枠Berを格納しておく空のＯＢ
    GameObject[] Ber;//ルーレットの枠
    RectTransform[] RectTransformBer;
    
    GameObject CCImagePrent;//料理画像の親 多分そこまで必要ない。階層管理用に必要 料理画像はberの下に表示したい
    GameObject[] CCImage;//画像を格納しているオブジェクト
    RectTransform[] CCImage_RectTransform;//上のトランス
    Image[] CCImage_Image;//上のトランス
    private const int CCImageSize = 300;//生成する画像サイズ
    private const int CCImageRadLong = 370;//円配置したものを伸ばす長さ
    private const float RotateAcc = 3;//ルーレットの加速度


    private int CCNum;//料理数
    private float RotateNum;//料理1つに対して使用できる角度

    float rad;
    float MainRotate;
    // Start is called before the first frame update
    public void SlotSetUp(CuisineClass[] CC)
    {
        this.CC = CC;
        CCNum = CC.Length;//料理数
        RotateNum = 360 / CCNum;//料理1つに対して使用できる角度
        MainRotate = 0;

        GetMaterial();//リソースからプレハブの取得 親オブジェクトを取得
        BerSet();//枠区切りを設置
        CCImageSet();//料理画像を配置
    }


    private void GetMaterial()//image格納用のプレハブだがBerが必要要件を満たしているためBerを変形させて流用
    {

        SlotBer = Resources.Load("Prefab/SlotBer") as GameObject;
        Bers = GameObject.Find("BeasBer");//これを回転されると枠もそろって回転する ただし料理は別で回さないと上下左右がおかしくなる。
        CCImagePrent = GameObject.Find("CCImagePrent");//これを回転されると枠もそろって回転する ただし料理は別で回さないと上下左右がおかしくなる。
        Canvas  = GameObject.Find("LotCanvas");//ルーレットが乗っているキャンバス
    }


    private void BerSet()//枠区切りを設置
    { 
        Ber = new GameObject[CCNum];
        RectTransformBer = new RectTransform[CCNum];

        

        for (int i = 0; i < CCNum; i++)
        {
            Ber[i] = Instantiate(SlotBer, Vector3.zero, Quaternion.identity, Bers.transform);
            RectTransformBer[i] = Ber[i].GetComponent<RectTransform>();
            RectTransformBer[i].anchoredPosition = new Vector3(0f, 0f, 0f);
            //RectTransformBer[i].Rotate(0, 0, r * i * 1.0f);
            RectTransformBer[i].rotation = Quaternion.Euler(0f, 0f, RotateNum * i * 1.0f);//0度から始まる
        }
    }

    private void CCImageSet()//image格納用のプレハブだがBerが必要要件を満たしているためBerを変形させて流用
    {
        //初期化//要素数確保
        CCImage = new GameObject[CCNum];
        CCImage_RectTransform = new RectTransform[CCNum];
        CCImage_Image = new Image[CCNum];

        rad = 0;//必要ないけど一応初期化

        for (int i = 0; i < CCNum; i++)
        {
            //画像表示用のオブジェクト生成
            CCImage[i] = Instantiate(SlotBer, Vector3.zero, Quaternion.identity, CCImagePrent.transform);
            //それぞれのコンポーネントを取得
            CCImage_RectTransform[i] = CCImage[i].GetComponent<RectTransform>();
            CCImage_Image[i] = CCImage[i].GetComponent<Image>();

            CCImage_RectTransform[i].pivot = new Vector2(0.5f, 0.5f);//pivotを初期位置に
            CCImage_RectTransform[i].sizeDelta  = new Vector2(CCImageSize, CCImageSize);//画像サイズを適正サイズに
            CCImage_RectTransform[i].anchoredPosition = new Vector3(0f, 0f, 0f);//位置を初期位置に
            CCImage_Image[i].sprite = CC[i].CuisineMaterial;//料理の画像を格納
            CCImage_Image[i].color = new Color(1,1,1,1);//色を白に変更

            rad = ((RotateNum * i) + (RotateNum / 2)) * Mathf.Deg2Rad;//対応したラジアンを求める
            //Debug.Log("角度 = " + ((RotateNum * i) + (RotateNum / 2)) + " \nx =" + Mathf.Cos(rad) + " \ny =" + Mathf.Sin(rad));
            CCImage_RectTransform[i].anchoredPosition = new Vector3(Mathf.Cos(rad) * CCImageRadLong, Mathf.Sin(rad) * CCImageRadLong, 0f);//円状に画像位置を変更
        }
    }

    //fixdupdateで呼び出す ルーレットが回転する
    public void SlotOn()
    {
        MainRotate += RotateAcc;//毎回角度を＋１していく
        if (MainRotate > 360) MainRotate = 0;//続いてもそこまで問題ないけど一応360度ごとに元に戻す

        for (int i = 0; i < CCNum; i++)
        {
            RectTransformBer[i].rotation = Quaternion.Euler(0f, 0f, RotateNum * i * 1.0f + MainRotate);//0度から始まる
            rad = ((RotateNum * i) + (RotateNum / 2) + MainRotate) * Mathf.Deg2Rad;//対応したラジアンを求める
            //Debug.Log("角度 = " + ((RotateNum * i) + (RotateNum / 2)) + " \nx =" + Mathf.Cos(rad) + " \ny =" + Mathf.Sin(rad));
            CCImage_RectTransform[i].anchoredPosition = new Vector3(Mathf.Cos(rad) * CCImageRadLong, Mathf.Sin(rad) * CCImageRadLong, 0f);//円状に画像位置を変更
        }
    }

    int[] t; 
    public int[] Select()//メインからパンチで決定したときに呼び出される
    {
        t = new int[3];
        //止まった場所の食材を割り出す90,210,330度の場所にある食材
        for (int i = 0; i < CCNum; i++)
        {
            float rot = ((RotateNum * i) + (RotateNum / 2) + MainRotate);
            if (rot > 360) rot = rot - 360;

            //.Log("i  = " + i + " 角度は"+ ((RotateNum * i) + (RotateNum / 2) + MainRotate));
            if ((90 - (RotateNum / 2)) <rot && rot < (90 + (RotateNum / 2)))
            {
                //Debug.Log("t0 = "+rot);
                t[0] = i;
            }
            if ((210 - (RotateNum / 2)) < rot && rot < (210 + (RotateNum / 2)))
            {
                //Debug.Log("t1 = " + rot);
                t[1] = i;
            }
            if ((330 - (RotateNum / 2)) < rot && rot < (330 + (RotateNum / 2)))
            {
                //Debug.Log("t2 = " + rot);
                t[2] = i;
            }
        }
        //Debug.Log("t[0] = " + t[0]+ "  t[1] = " + t[1]+ "  t[2] = " + t[2]);
        Canvas.SetActive(false);
        return t;
    }

}
