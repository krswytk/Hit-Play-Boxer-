using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTitleGo : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        t = 0;
    }

    private float t;
    // Update is called once per frame
    void Update()
    { t += Time.deltaTime;
        if (t > 5)
        {
            if (Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
            {
                Down();
            }
        }
    }

    public void Down()
    {
        audioSource.Play();
        feadSC.fade("Title");
    }
}
