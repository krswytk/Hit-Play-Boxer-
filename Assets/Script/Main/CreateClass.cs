using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
/// <summary>
/// 食材、料理生成に必要なクラスの生成を行う
/// 必要データの読み込み→食材クラスの生成→それを元に料理クラスの生成  を行う
/// </summary>

public class CreateClass : MonoBehaviour
{
    Sprite[] ImageDatas;// 画像の中身を入れるリスト; 
    List<string[]> TextDatas = new List<string[]>(); // 料理CSVの中身を入れるリスト;リストの中に配列を格納している   list   a[0]  b[0]  c[0]
    List<string[]> MoneyDatas = new List<string[]>(); // 食材CSVの中身を入れるリスト;リストの中に配列を格納している   list   a[0]  b[0]  c[0]
    

    //食材用のクラス
    public Foodstuff[] FS { get; private set; }//全部の画像の名前とspritを格納する

    //料理用のクラス
    public CuisineClass[] CC { get; private set; }//料理の構成を格納する

    
    //メインのスタートで呼び出す。以下の関数をここから呼び出しクラスの生成を行う。
    public void CreateClassStater()
    {
        ImageReading();//素材をImageDatasに格納
        TextReading();//TextDatasに料理CSVを読み込んだデータを格納
        MoneyReading();//MoneyDatasに食材CSVを読み込んだデータを格納
        //SpriteReading();////SpriteDatasにSpriteを読み込んだデータを格納//使わない方式に変更したため削除
        CreateFSClass();//FSClassの生成
        CreateCClass();//CClassの生成

        //Debug.Log(FS.Length);
    }

    //食材の画像を保持する
    private void ImageReading()//ImageDatasに画像を格納
    {
        ImageDatas = (Resources.LoadAll<Sprite>("Material/"));
    }

    private void TextReading()//TextDatasに二次元配列で料理名と食材を格納
    {

        TextAsset csvFile = Resources.Load("Text/Cooking") as TextAsset; // Resouces下のCSV読み込み
        //Cookingの中身   料理名,食材名,...(MAX5個まで
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            TextDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }

        // csvDatas[行][列]を指定して値を自由に取り出せる
        string D = "";
        for (int i = 0; i < TextDatas.Count(); i++)
        {
            for (int l = 0; l < TextDatas[i].Count(); l++)
            {
                D += TextDatas[i][l] + ",";
            }
            D += "\n\r";
        }
        Debug.Log(D);
    }
    private void MoneyReading()//TextDatasに二次元配列で料理名と食材を格納
    {

        TextAsset csvFile = Resources.Load("Text/Foodstuff") as TextAsset; // Resouces下のCSV読み込み
        //Cookingの中身   料理名,基礎値,-40,-20,0,+20,+40(MAX5個まで
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            MoneyDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }

        // csvDatas[行][列]を指定して値を自由に取り出せる
        string D = "";
        for (int i = 0; i < MoneyDatas.Count(); i++)
        {
            for (int l = 0; l < MoneyDatas[i].Count(); l++)
            {
                D += MoneyDatas[i][l] + ",";
            }
            D += "\n\r";
        }
        Debug.Log(D);
    }

    /*
    private void SpriteReading()//SpriteDatasにImageDateの画像を格納
    {
        SpriteDatas = new Sprite[TextDatas.Count()][];
        for (int i = 0; i < TextDatas.Count(); i++)
        {
            SpriteDatas[i] = new Sprite[TextDatas[i].Count()];
            for (int l = 0; l < TextDatas[i].Count(); l++)
            {
                for (int lp = 0; lp < ImageDatas.Length; lp++)
                {
                    if (ImageDatas[lp].name == TextDatas[i][l])
                    {
                        SpriteDatas[i][l] = ImageDatas[lp];
                    }

                }
            }
        }
    }
    */

    private void CreateFSClass()//食材クラスの生成を行う
    {
        ///食材の数分の配列を用意→配列個数分ループ→名前と金額を入力しクラスを生成
        
        //Debug.Log(MoneyDatas.Count);//正確な個数を出力していることを確認
        //int[] test = { 20, 40, 60, 80, 100 };//仮作成用のテストデータ

        FS = new Foodstuff[MoneyDatas.Count];//MoneyDatas.Count は食材の数。リストの要素数を出力している。リスト内の配列数を出力

        string Fname = "";//食材の名前を格納し画像を探すために保持する
        int[] Money = new int[5];
        for (int i = 0; i < FS.Length; i++)//料理回数分ループ
        {
            Fname = MoneyDatas[i][0];//食材名を取得
            for (int l = 0; l < Money.Length; l++)//金額を格納する//基礎金額を入力し、内部で処理したほうが楽だが、後々の仕様変更を考え、この方式を採用
            {
                Money[l] = int.Parse(MoneyDatas[i][l + 2].ToString());//文字列である金額を整数値に変換し格納
            }

            for (int l = 0; l < ImageDatas.Length; l++)//画像枚数分繰り返す
            {
                //Debug.Log(Fname +" , "+ ImageDatas[i].name);
                if (Fname == ImageDatas[l].name)//料理名とImageDatasの名前が一致する場合
                {
                    //Debug.Log("!!!!!!!!!!!!!!!!!!!!");
                    FS[i] = new Foodstuff(Fname, ImageDatas[l], Money);//各クラスを初期化、同時に素材名とSpritを格納
                    break;
                }
                if (l + 1 == ImageDatas.Length)
                {
                    Debug.LogError("食材クラス生成 '" + Fname + " 'の画像がありません");
                }
            }
        }
        //Debug.Log(MoneyDatas[0][0]);
    }

    private void CreateCClass()
    {
        Sprite[] SpriteDatas; // 画像をTextDatasと連携して入れるリスト配列

        CC = new CuisineClass[TextDatas.Count()];

        for (int i = 0; i < CC.Length; i++)
        {
            SpriteDatas = new Sprite[TextDatas[i].Count()];//料理と食材の画像を格納する。そのための要素数を確保

            for (int l = 0; l < TextDatas[i].Length; l++)//料理→食材の順番で画像から探す。
            {
                for (int lp = 0; lp < ImageDatas.Length; lp++)//全食材回数分ループ
                {
                    //Debug.Log(lp);
                    if (TextDatas[i][l] == ImageDatas[lp].name)//料理、食材名と対応する画像が見つかったら
                    {
                        SpriteDatas[l] = ImageDatas[lp];//SpriteDatas(クラス生成用に画像を保持)に対応画像を代入する。
                        break;//見つかった場合これ以上のループは不要なのでBreakし、抜け出す
                        
                    }
                    if (lp + 1 == ImageDatas.Length)
                    {
                        //Debug.LogError(lp);
                        Debug.LogError("料理クラス生成 '" + TextDatas[i][0]+ "' 内の '" + TextDatas[i][l] + "' の画像がありません");
                    }

                }

            }

            CC[i] = new CuisineClass(TextDatas[i], SpriteDatas);
        }
        //Debug.Log(CC[0].Name[0]);
    }
}




