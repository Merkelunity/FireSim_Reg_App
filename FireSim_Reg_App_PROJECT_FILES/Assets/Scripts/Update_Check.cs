using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Update_Check : MonoBehaviour
{
    public string URL;
    public Text update_text;
    public int Set_Version;
    public Text VersionTXT;
    public GameObject Download_Button;

    private int Current_Ver;
    private string PatchURL;

    public myVersionData myVersionData;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Version", Set_Version);
        Current_Ver = PlayerPrefs.GetInt("Version", 10);
        StartCoroutine(downlaodJson());
        VersionTXT.text = "version: " + Current_Ver;
        Download_Button.SetActive(false);

    }
    IEnumerator downlaodJson()
    {
        UnityWebRequest req = UnityWebRequest.Get(URL);
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.ProtocolError || req.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(UnityWebRequest.Result.ConnectionError);
            Debug.Log(UnityWebRequest.Result.ProtocolError);
        }
        else
        {
            var text = req.downloadHandler.text;

            myVersionData = JsonUtility.FromJson<myVersionData>(text);

            if (myVersionData.version > Current_Ver)
            {
                update_text.text = "Update Required!";
                Download_Button.SetActive(true);
                PatchURL = myVersionData.url;
            }

            else
            {
                update_text.text = "You are up to date!";
            }
        }
    }

    public void Download_Button_Method()
    {
        Application.OpenURL(PatchURL);
    }

}
[System.Serializable]
public class myVersionData
{
    public int version;
    public string url;
}