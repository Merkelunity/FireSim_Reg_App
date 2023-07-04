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
using UnityEngine.UI.TableUI;

[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileText : MonoBehaviour, IPointerDownHandler {
    //public Text output;
    public static string[] paths;



    public static CanvasSampleOpenFileText instance;
    public Slider Reg_slider;
    public TextMeshProUGUI debug_Text;
    //string filename;

    public int numberOfNewRegister;

    public List<string> empID;
    public List<string> empName;
    public List<string> OrgName;
    public List<long> mobileNumber;
    public List<string> emailAddress;
    public List<string> trainerName;
    //public TextMeshProUGUI text;

    public TableUI table;
    int colCount;



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
        //table
        table.Rows = 1;
        table.Columns = 6;
        //table.GetCell(0, 0).text = "Emp ID";
        //table.GetCell(0, 1).text = "Name";
        //table.GetCell(0, 2).text = "Mobile Number";
        //table.GetCell(0, 3).text = "Email Address";
        //table.GetCell(0, 4).text = "Company Name";
        //table.GetCell(0, 5).text = "Trainer Name";
        string[] row1 =
            {
                "Emp ID",
                "Name",
                "Mobile Number",
                "Email Address",
                "Company Name",
                "Trainer Name"
            };
        fillRow(0,0, row1);
    }

    public void fillRow(int row,int col , string[] data)
    {
        for (int i = 0; i < table.Columns; i++)
        {
            table.GetCell(col, i).text = data[i];
        }
    }

    void Start() {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        colCount = 0;
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
        trainerName.Clear();
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
            if (trainerName.Count >= 0)
                trainerName.Add(val[5]);


            numberOfNewRegister++;
            //table
            debug_Text.text = numberOfNewRegister.ToString();
            //Reg_slider.maxValue = numberOfNewRegister-1;

        }
        //table
        table.Rows = numberOfNewRegister+1;
        for(int i = 0; i < numberOfNewRegister; i++)
        {
            colCount++;
            table.GetCell(colCount, 0).text = empID[colCount-1];
            table.GetCell(colCount, 1).text = empName[colCount-1];
            table.GetCell(colCount, 2).text = mobileNumber[colCount-1].ToString();
            table.GetCell(colCount, 3).text = emailAddress[colCount-1];
            table.GetCell(colCount, 4).text = OrgName[colCount-1];
            table.GetCell(colCount, 5).text = trainerName[colCount-1];
        }


    }
    #endregion




}