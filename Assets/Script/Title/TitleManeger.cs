using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManeger : MonoBehaviour
{
    PunchDetection PD;
    // Start is called before the first frame update
    void Start()
    {
        PD = GetComponent<PunchDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Money[0]);
        //0-2 RSLでも可
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //Debug.Log(ImageBox[0, 0].GetComponent<Foodstuff>().Name);

            PD.Punch(0);

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            PD.Punch(1);

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            PD.Punch(2);

        }
    }
}
