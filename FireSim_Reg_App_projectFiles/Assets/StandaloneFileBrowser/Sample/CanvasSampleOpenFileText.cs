using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;

using TMPro;
using System.IO;


[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileText : MonoBehaviour, IPointerDownHandler {
    //public Text output;
    public static string[] paths;



    public static CanvasSampleOpenFileText instance;
    public Slider Reg_slider;
    public TextMeshProUGUI debug_Text;
    string filename;

    public int numberOfNewRegister;

    public List<string> empID;
    public List<string> empName;
    public List<string> OrgName;
    public List<long> mobileNumber;
    public List<string> emailAddress;
    //public TextMeshProUGUI text;



#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OnPointerDown(PointerEventData eventData) {
        UploadFile(gameObject.name, "OnFileUpload", ".csv", false);
         readCSV();
    }

    // Called from browser
    public void OnFileUpload(string url) {
        StartCoroutine(OutputRoutine(url));
         readCSV();
    }
#else
    //
    // Standalone platforms & editor
    //
    public void OnPointerDown(PointerEventData eventData) { }


    private void Awake()
    {
        instance = this;
      
    }

    void Start() {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick() 
    {
        paths = StandaloneFileBrowser.OpenFilePanel("OpenFile", "", "csv", false);
        if (paths.Length > 0) {
            StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
        }

       
    }
#endif

    public IEnumerator OutputRoutine(string url) 
    {
        var loader = new WWW(url);
        yield return loader;
        //output.text = loader.text;
        //Csv.instance.readCSV(paths);
        readCSV();
    }




    #region readFromCSV ---------------------------------------------------------------------------------------------------------------
    public void readCSV()
    {
        numberOfNewRegister = 0;
        empID.Clear();
        empName.Clear();
        emailAddress.Clear();
        mobileNumber.Clear();
        OrgName.Clear();
        StreamReader strReader = new StreamReader(paths[0]);
        bool endofFile = false;
        while (!endofFile)
        {
            string data = strReader.ReadLine();
            if (data == null)
            {
                endofFile = true;
                break;
            }
            var val = data.Split(',');
            //Debug.Log(val[0] + "," + val[1] + "," + val[2] + "," + val[3] + ","+val[4]);
            //text.text = (val[0] + "," + val[1] + "," + val[2] + "," + val[3]);
            empID.Add(val[0]);//empid
            if(empName.Count>=0)
                empName.Add(val[1]);//empname
            if(mobileNumber.Count>=0)
                mobileNumber.Add(int.Parse(val[2]));//mobileNumber
            if(emailAddress.Count>=0)
                emailAddress.Add(val[3]);//emailaddress
            if (OrgName.Count >= 0) 
                OrgName.Add(val[4]);//orgname


            numberOfNewRegister++;
            //Debug.Log(numberOfNewRegister);
            //Reg_slider.maxValue = numberOfNewRegister-1;

        }

    }
    #endregion




}