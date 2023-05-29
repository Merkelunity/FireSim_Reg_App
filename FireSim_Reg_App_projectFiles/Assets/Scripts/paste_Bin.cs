using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;

public class paste_Bin : MonoBehaviour
{
    public string url;
    versionControl versionControl;
    //public List<string> files;
    IEnumerator Start()
    {
        UnityWebRequest www = new UnityWebRequest(url);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Success");
            versionControl = JsonConvert.DeserializeObject<versionControl>(www.downloadHandler.text);
            Debug.Log(versionControl.Version);

        }
        //getAllFiles();
    }
    //public void getAllFiles()
    //{
    //    Debug.Log(Directory.GetCurrentDirectory());
    //    DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
    //    DirectoryInfo[] info = dir.GetDirectories();
    //    FileInfo[] fileinfo = dir.GetFiles();
    //    foreach(var i in info)
    //    {
    //        files.Add(i.ToString());
    //    }
    //    foreach(var i in fileinfo)
    //    {
    //        files.Add(i.ToString());
    //    }
    //}
    
}
public class versionControl
{
    public string Version;
}
