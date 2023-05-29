using System.Collections;
using System.IO;
using System.IO.Compression;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class UWR_Test : MonoBehaviour
{
    public string url = "https://drive.google.com/uc?export=download&id=1LR_qk3o4G-4yLntKzzTKc36cj_IcseVX";
    //public string extractToPath;
    public RawImage rawImage;

    //DownloadHandlerFile file;

    //void Awake()
    //{
    //    string file = Path.Combine(Application.dataPath,"myfile.zip");
    //    if(!File.Exists(file))
    //    {
    //        File.Create(file);
    //        Debug.Log("file created");
    //    }
    //}

    void Start()
    {

        StartCoroutine(downloadImageFromURL(url,rawImage));

    }
    IEnumerator downloadImageFromURL(string URL , RawImage RAWIMAGE)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(URL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(UnityWebRequest.Result.ConnectionError);
            Debug.Log(UnityWebRequest.Result.ProtocolError);
        }
        else
        {
            Texture  texture = DownloadHandlerTexture.GetContent(request);
            RAWIMAGE.texture = texture;
        }
    }
    //public void ExtractZipFile(string zipFilePath, string extractPath)
    //{
    //    // Open the zip file
    //    using (ZipArchive archive = ZipFile.OpenRead(zipFilePath))
    //    {
    //        // Extract each entry in the zip file
    //        foreach (ZipArchiveEntry entry in archive.Entries)
    //        {
    //            string extractFilePath = Path.Combine(extractPath, entry.FullName);
                
    //            // If the entry is a directory, create it
    //            if (entry.FullName.EndsWith("/") || entry.FullName.EndsWith("\\"))
    //            {
    //                Directory.CreateDirectory(extractFilePath);
    //                continue;
    //            }
                
    //            // Extract the entry to the extract path
    //            entry.ExtractToFile(extractFilePath, true);

    //        }
    //        Debug.Log("files extracted to D:\\Dhinesh\\Firesim_Login_project successfully");
    //    }
    //}
}
