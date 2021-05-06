using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using System.IO.Ports;
using UnityEngine;
using UniRx;

public class Serial : MonoBehaviour
{
    public string portName;
    public string portName2;
    public string portName3;
    public int baurate;

    SerialPort serial;
    SerialPort serial2;
    SerialPort serial3;
    bool isLoop = true;

    float accY = 0.0f;
    float OldaccY = 0.0f;
    float accY2 = 0.0f;
    float OldaccY2 = 0.0f;
    float accY3 = 0.0f;
    float OldaccY3 = 0.0f;

    GetFood GetFood;
    // Start is called before the first frame update
    void Start()
    {
        this.serial = new SerialPort(portName,
            baurate, Parity.None, 8, StopBits.One);
        this.serial2 = new SerialPort(portName2,
            baurate, Parity.None, 8, StopBits.One);
        this.serial3 = new SerialPort(portName3,
            baurate, Parity.None, 8, StopBits.One);
        try
        {
            this.serial.Open();
            Scheduler.ThreadPool.Schedule(() => ReadDataA()).AddTo(this);
            this.serial2.Open();
            Scheduler.ThreadPool.Schedule(() => ReadDataB()).AddTo(this);
            this.serial3.Open();
            Scheduler.ThreadPool.Schedule(() => ReadDataC()).AddTo(this);
        }
        catch (Exception e)
        {
            Debug.Log("can not open serial port");
        }
        GetFood = GameObject.Find("Main Camera").GetComponent<GetFood>();
    }
    public void ReadDataA()
    {//データ受信時
        while (this.isLoop)
        {
            string message = this.serial.ReadLine();

            try
            {
                string msg = message;

                accY = float.Parse(msg);

            }
            catch (Exception e) { }
        }
    }
    public void ReadDataB()
    {//データ受信時
        while (this.isLoop)
        {
            string message2 = this.serial2.ReadLine();

            try
            {
                string msg2 = message2;


                accY2 = float.Parse(msg2);



            }
            catch (Exception e) { }
        }
    }
    public void ReadDataC()
    {//データ受信時
        while (this.isLoop)
        {
            string message3 = this.serial3.ReadLine();

            try
            {
                string msg3 = message3;


                accY3 = float.Parse(msg3);


            }
            catch (Exception e) { }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (accY == 1 && OldaccY == 0)
        {
            GetFood.Get(2,0);
        }
        if (accY2 == 1 && OldaccY2 == 0)
        {
            GetFood.Get(1,0);
        }
        if (accY3 == 1 && OldaccY3 == 0)
        {
            GetFood.Get(0,0);
        }
        OldaccY = accY;
        OldaccY2 = accY2;
        OldaccY3 = accY3;
    }
    void OnDestroy()
    {//終了時
        this.isLoop = false;
        this.serial.Close();
    }
}
