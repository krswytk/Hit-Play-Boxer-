﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    private RectTransform Long;
    private RectTransform Shot;


    private float LimitTime;
    private float ShotStartRotation = -27.69f;

    private float per;
    private float LongTimeper;
    private float ShotTimeper;

    private float MinitPer;

    private Text TimeText;
    public GameObject TimeBaseAnim;

    // Start is called before the first frame update
    public void TimerSetUp(float LimitTime)
    {
        Long = GameObject.Find("Long").GetComponent<RectTransform>();
        //Shot = GameObject.Find("Shot").GetComponent<RectTransform>();
        this.LimitTime = LimitTime;

        per = 0;
        LongTimeper = 0;
        ShotTimeper = 0;

        MinitPer = Mathf.Floor(LimitTime / 60);

        /*//正確に取得できていないため上で直接入力
        ShotStartRotation = Shot.rotation.z;//短針の初期回転度を取得
        Debug.Log("ShotStartRotation =" + ShotStartRotation + " \n\r Shot.rotation.z =" + Shot.rotation.z);
        */
        TimeText = GameObject.Find("TimeText").GetComponent<Text>();
        //TimeBaseAnim = GameObject.Find("AnimBase").GetComponent<GameObject>();
        //var t = TimeBaseAnim.GetComponent<RectTransform>();t.position = new Vector2(500,500);
        TimeBaseAnim.SetActive(false);
    }

    // Update is called once per frame
    public void ClockMove(float Timer)
    {
        per = Timer / (LimitTime/ MinitPer);//時間の割合を求める
        TimeText.text = Mathf.Floor(24 * (per / 2)).ToString();
        if(24 * (per / 2) > 18) TimeBaseAnim.SetActive(true);

        //長針
        LongTimeper = 360 * per;
        //Debug.Log("Timeper =" + LongTimeper + "Timer =" + Timer + "per =" + per + "LimitTime =" + LimitTime);
        Long.rotation = Quaternion.Euler(0, 0, -LongTimeper);

        /*
        //短針
        ShotTimeper = 30 * per;
        //Debug.Log("Timeper =" + ShotTimeper + "Timer =" + Timer + "per =" + per + "LimitTime =" + LimitTime);
        Shot.rotation = Quaternion.Euler(0, 0, (ShotStartRotation + - ShotTimeper));
        */
    }
}
