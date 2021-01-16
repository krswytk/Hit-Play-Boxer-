using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrezitGo : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ButtonDown()
    {
        Load();
    }

    private void Load()
    {
        //SceneManager.LoadScene("Main");
        audioSource.Play();
        feadSC.fade("credit");
    }
}
