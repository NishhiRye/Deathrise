using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriterBehaviour : MonoBehaviour
{
    public float typeWriterDelay = 0.1f;
    string fullText;
    string currentText;
    public GameObject canvas;

    // Update is called once per frame
    void Start()
    {
        fullText = GetComponent<Text>().text;
        StartCoroutine(ShowText());
        StartCoroutine(canvasOff());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(typeWriterDelay);
        }         
    }

    IEnumerator canvasOff()
    {
        yield return new WaitForSeconds(10);

        canvas.SetActive(false);
    }
}
