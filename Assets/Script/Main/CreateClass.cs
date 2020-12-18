using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class CreateClass : MonoBehaviour
{
    Sprite[] ImageDatas;// 画像の中身を入れるリスト; 
    List<string[]> TextDatas = new List<string[]>(); // CSVの中身を入れるリスト;リストの中に配列を格納している   list   a[0]  b[0]  c[0]
    Sprite[][] SpriteDatas; // 画像をTextDatasと連携して入れるリスト配列 　　        list   a[1]  b[1]  c[1]


    public Foodstuff[] FS { get; private set; }//全部の画像の名前とspritを格納する
    public CuisineClass[] C { get; private set; }//料理の構成を格納する

    

    public void CreateClassStater()
    {
        ImageReading();//素材をImageDatasに格納
        TextReading();//TextDatasにtextを読み込んだデータを格納
        SpriteReading();////SpriteDatasにSpriteを読み込んだデータを格納
        CreateCClass();//CClassの生成
        CreateFSClass();//FSClassの生成
    }

    private void ImageReading()//ImageDatasに画像を格納
    {
        ImageDatas = (Resources.LoadAll<Sprite>("Material/"));
    }

    private void TextReading()//TextDatasに二次元配列で料理名と食材を格納
    {

        TextAsset csvFile = Resources.Load("Text/Text") as TextAsset; // Resouces下のCSV読み込み
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

    private void CreateFSClass()//CClaseを参照し、料理以外の素材を格納
    {
        FS = new Foodstuff[ImageDatas.GetLength(0) - C.GetLength(0)];//ImageDatas - CClase = 食材の数
        
        bool a;//料理の除外に使用
        int lp = 0;

        for (int i = 0; i < ImageDatas.GetLength(0); i++)//料理、食材を含めた全画像枚数分繰り返す
        {
            a = true;
            for (int l = 0; l < C.GetLength(0); l++)//料理クラスの数繰り返す
            {
                if(C[l].CuisineName == ImageDatas[i].name)//料理名とImageDatasの名前が一致する場合
                {
                    a = false;
                }
                    
            }
            if (a)//上記ループで画像名と料理名が一致しなければ食材クラスを生成する
            {
                FS[lp] = new Foodstuff(ImageDatas[i].name, ImageDatas[i]);//各クラスを初期化、同時に素材名とSpritを格納
                lp++;//配列を次に動かす
            }
        }
    }

    private void CreateCClass()
    {
        C = new CuisineClass[TextDatas.Count()];
        for (int i = 0; i < C.Count(); i++)
        {
            C[i] = new CuisineClass(TextDatas[i], SpriteDatas[i]);
        }
    }
}




