using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanchSE : MonoBehaviour
{
    AudioSource audioSource;
    RisultManeger RM;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        RM = Camera.main.gameObject.GetComponent<RisultManeger>();
    }

    public void PanchSEPlay()
    {
        //Debug.Log("パンチＳＥを鳴らしました");
        audioSource.Play();
    }
    public void PanchSEEnd()
    {
        //Debug.Log("アニメーションが終了しました");
        RM.AnimEnd();
    }
}
