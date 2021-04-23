using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// アイテムの移動　アイテムの生成を行うクラス関数
/// </summary>
public class TransformImageBox : MonoBehaviour
{
    RectTransform[,] _RectTransform;
    Image[,] _Image;
    GameObject[,] ImageBox;
    Text[,] _Text;
    public int[,] Money_int;



    Vector2 V2;

    private bool sw = true;
    public float speed = 1;
    private int Foodnum;//食材の数を格納
    

    Foodstuff[] FS;

    public void SetUp(GameObject[,] ImageBox, Foodstuff[] FS)
    {
        if (sw)
        {
            this.FS = FS;
            //Debug.Log(FS[0].Name);
            this.ImageBox = ImageBox;

            _RectTransform = new RectTransform[ImageBox.GetLength(0), ImageBox.GetLength(1)];//配列の初期化
            _Image = new Image[ImageBox.GetLength(0), ImageBox.GetLength(1)];
            _Text = new Text[ImageBox.GetLength(0), ImageBox.GetLength(1)];
            Money_int = new int[ImageBox.GetLength(0), ImageBox.GetLength(1)];

            Foodnum = FS.GetLength(0);//FSの要素数（食材の数）を格納

            for (int i = 0; i < ImageBox.GetLength(0); i++)//3
            {
                for (int l = 0; l < ImageBox.GetLength(1); l++)//8
                {
                    _RectTransform[i, l] = ImageBox[i, l].GetComponent<RectTransform>();//BOXの位置変更用の取得
                    _Image[i, l] = ImageBox[i, l].GetComponent<Image>();//BOXの画像変更用の変更
                    _Text[i, l] = ImageBox[i, l].transform.GetChild(1).gameObject.GetComponent<Text>();//値段設定用のTextを取得
                    Money_int[i, l] = 0;//ImageBoxと連動して金額を格納しておく　初期値０
                    ImageBox[i, l].SetActive(false);//透明にしておく
                }
            }

            sw = false;
        }

        //Debug.Log(Money_int.GetLength(1));
    }


    public void RollmageBox()//アイテムの移動を行う
    {

        for (int i = 0; i < ImageBox.GetLength(0); i++)//3
        {
            for (int l = 0; l < ImageBox.GetLength(1); l++)//8
            {
                V2 = _RectTransform[i, l].localPosition * 1.0f;
                //if (i == 0 && l == 0) Debug.Log(V2);
                if (V2.y <= -605)//下まで行った場合の処理
                {
                    _RectTransform[i, l].localPosition = new Vector2(V2.x, 305);
                    InputBox(i, l);//画像を入れ込む
                    ImageBox[i, l].SetActive(true);//setup、取得で透明にしたものを可視化できるようにする
                }
                else
                {
                    V2.y -= speed;
                    _RectTransform[i, l].localPosition = V2;
                }


            }
        }


    }

    /// <summary>
    /// FS[]に食材の画像が入っている　ランダムに選定　_SpriteRendererに入れる
    /// 同時にFSのコンポーネントをアタッチ
    /// 同コンポーネントにランダムのFS[num]を入れる
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    Foodstuff f;
    void InputBox(int x,int y)//上に移動したBoxにアイテムを入れ込む//recttransform[x, y]にランダムに素材画像を入れる//食材クラスから金額を取得　ランダムに入れ込む
        //すべての食材から表示する食材をランダムで決定する。
    {
        int num = Random.Range(0, FS.Length - 1);//Random.Rangeはintの場合（以上　以下）で値が返る//表示する食材の決定

        //Boxの中身の入れ替え処理
        try{
            ImageBox[x, y].name = FS[num].Name.ToString();//ゲームオブジェクトの名前を食材名に変更
            _Image[x, y].sprite = FS[num].Material;//FSの該当番号の画像を入れる
            _Text[x, y].text = FS[num].NowMoney.ToString("N0");//金額を金額テキストに入れる
        }
        catch
        {
            Debug.Log("食材:" + FS[num-1].Name + "と" + FS[num + 1].Name + " のあいだがエラーです。");
        }
    }

}
