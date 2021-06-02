using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTitleGo : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip PanchSE;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }
    public void downbutton()
    {
        audioSource.Play();
        feadSC.fade("Title");
    }
}
