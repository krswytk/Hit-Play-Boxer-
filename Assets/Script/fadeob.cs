using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fadeob : MonoBehaviour
{
    public Image C;
    public float timer = 2.0f;

    private void Start()
    {
        C = GetComponent<Image>();
    }

    public void ONfade(string name)
    {
        C.color = new Color(0, 0, 0, 0);//徐々に1に近づける        
        for (int i = 0; i <= 255; i++)
        {
            StartCoroutine(start(Map(i, 0, 255, 0, timer), i, name, () =>//徐々に値を増やす　最後がtimerの値になる
            {

            }));

        }
    }

    public void OFFfade()
    {
        C.color = new Color(0, 0, 0, 1);//徐々に0に近づける        
        for(int i = 0; i <= 255; i++)
        {
            StartCoroutine(end(Map(i, 0, 255, 0, timer), 255-i, () =>//徐々に値を増やす　最後がtimerの値になる
            {

            }));

        }
    }

    private IEnumerator start(float waitTime, int i, string name, Action action)
    {
        yield return new WaitForSeconds(waitTime);

        C.color = new Color(0, 0, 0, i / 255.0f);
        action();
        Debug.Log(i);
        if (i >= 255)
        {
            SceneManager.LoadScene(name);
        }
    }
    private IEnumerator end(float waitTime, int i, Action action)
    {
        yield return new WaitForSeconds(waitTime);

        C.color = new Color(0, 0, 0, i / 255.0f);
        action();
        Debug.Log(i);
        if (i == 0)
        {
            Destroy(this.gameObject);
        }
    }
    float Map(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }

}
