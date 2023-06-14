using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
public class googleFormScript : MonoBehaviour
{
    public TMP_InputField inputField;
    //get this url from going to inspect mode and find "formResponse"
    string url = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdtSxcUBTkmfI7k2uHuc9W5jlyfWaGtGnOvNP61BBBdR_jRwA/formResponse";

    void Start()
    {
        inputField.text = "hello world1";
        StartCoroutine(post(inputField.text,5));
    }

    IEnumerator post(string s1,int i)
    {
        WWWForm form = new WWWForm();
        //get this entry point from going to gform and login to your original form and click get pre-filled link and find "entry"
        form.AddField("entry.1404640065", s1);
        form.AddField("entry.356167416", i);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        Debug.Log(www.result);
    }


}
