using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGo : MonoBehaviour
{
    private float t;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Screen.SetResolution(1920, 1080, false, 60);
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        t += Time.deltaTime;
        if (t > 5)
        {
            if (Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
            {
                Load();
            }
        }
        
    }

    public void ButtonDown()
    {
        Load();
    }

    private void Load()
    {
        //SceneManager.LoadScene("Main");
        audioSource.Play();
        feadSC.fade("Main");
    }
}
