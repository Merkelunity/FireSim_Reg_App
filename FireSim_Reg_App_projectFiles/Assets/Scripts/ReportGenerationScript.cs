using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json;
using DG.Tweening;
using SFB;
using UnityEngine.Networking;
public class ReportGenerationScript : MonoBehaviour
{
    public static ReportGenerationScript instance;
    string certificate_login;
    public string trainerString;
    string playfabID;
    string output;
    public bool checkbool;

    [Header("ALERT PANELS")]
    public GameObject loginErrorPanel;
    public GameObject canGenerateReportPanel;
    public GameObject NAreportPanel;
    public GameObject dataFetchPanel;
    public GameObject pathEmptyPanel;

    public Ease ease;
    public float timeofdisplay;


    public TextMeshProUGUI debug_Text;
    public TextMeshProUGUI fileSaveText;
    public TMP_InputField cert_emp_ID;
    public TMP_InputField trainerName;


    [Header("CLASS DEBUG")]
    public details details;
    public IndustrialKitchen IndustrialKitchen;
    public OfficeElectric OfficeElectric;
    public OfficePaper OfficePaper;
    public OfficeSofa OfficeSofa;
    public WarehouseElectric WarehouseElectric;
    public WarehouseOil WarehouseOil;
    public WarehouseWood WarehouseWood;
    public LabMetal LabMetal;
    public canGenerateReport canGenerateReport;

    [Header("REPORT VALUES")]
    public TextMeshProUGUI officeElectricVal;
    public TextMeshProUGUI officeSofaVal;
    public TextMeshProUGUI officePaperVal;
    public TextMeshProUGUI IndustrialKitchenVal;
    public TextMeshProUGUI labFireVal;
    public TextMeshProUGUI WarehouseElectricVal;
    public TextMeshProUGUI WarehouseWoodVal;
    public TextMeshProUGUI WarehouseOilVal;
    public TextMeshProUGUI empIDVal;
    public TextMeshProUGUI empOrgNameVal;
    public TextMeshProUGUI empNameVal;


    public static UnityEvent<string, string> onUserDataRecieved = new UnityEvent<string, string>();
    bool bulk = false;
    bool single = false;
    private string _path;
    [HideInInspector]
    public int index = 0;
    public string[] paths;
    string filepath;
    public void openFolderdir()
    {
        paths = StandaloneFileBrowser.OpenFolderPanel("Select Folder", Application.dataPath, true);
        fileSaveText.text = "Files will be saved to " + paths[0];
        Debug.Log(paths[0].ToString());
        filepath = paths[0];
    }

    private void Awake()
    {
        //paths[0] = "";
        instance = this;

    }

    private void Start()
    {
        //alert the user
        index = 0;
        loginErrorPanel.gameObject.SetActive(false);
    }

    //BTN
    public void loginNextUser()
    {
        bulk = true;
        single = false;
    }

    public void certificate_generate()
    {
        loginNextUser();
        certificate_login = CanvasSampleOpenFileText.instance.empID[index];
        trainerString = CanvasSampleOpenFileText.instance.trainerName[index];
        loginPlayer(certificate_login.ToString());
    }
    //BTN
    public void certificate_generate_1()
    {
        if (System.String.IsNullOrEmpty(filepath))
        {
            Debug.Log("path is empty");
            //alert the user
            pathEmptyPanel.gameObject.SetActive(true);
            pathEmptyPanel.gameObject.transform.DOLocalMoveY(-300, timeofdisplay).SetEase(ease).
                OnComplete(() => f1(pathEmptyPanel));
        }
        else
        {
            bulk = false;
            single = true;
            trainerString = trainerName.text.ToString();
            certificate_login = cert_emp_ID.text;
            loginPlayer(certificate_login.ToString());
        }
    }
    #region ERROR

    void onerror(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        //alert the user
        loginErrorPanel.gameObject.SetActive(true);
        loginErrorPanel.gameObject.transform.DOLocalMoveY(-300, timeofdisplay).SetEase(ease).
            OnComplete(() => f1(loginErrorPanel));
    }

    #endregion

    #region loginPlayer
    public void loginPlayer(string email)
    {
        var request = new LoginWithEmailAddressRequest()
        { 
            Email = "\"" + email + "\"" + "@gmail.com",
            Password = "______"
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, onloginsuccess, onerror);

    }
    void onloginsuccess(LoginResult result)
    {
        cert_emp_ID.text = "";
        playfabID = result.PlayFabId;
        Debug.Log("logged in!");

        //testFunction();

        getData("sendReport");

        if(checkbool == true)
        {
            getData("personalDetails");

            getData("Office_electric");
            getData("Office_Sofa");
            getData("Office_Paper");

            getData("Warehouse_Electric");
            getData("Warehouse_Boxfire");
            getData("Warehouse_Oil_Fire");

            getData("IndustrialKitchen");
            getData("LabFire");
        
        }
    }
    #endregion

    #region get data
    public string getData(string key)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = playfabID,
            Keys = new List<string>() { key }
        }, response =>
        {
            Debug.Log("successful getData");
            if (response.Data.ContainsKey(key))
            {
                output = response.Data[key].Value;
                onUserDataRecieved.Invoke(key, output);
            }
            getMultipleData();
        }, error =>
        {
            Debug.Log("unsuccessful getData");
        });
        return output;
    }

    #region getMultipleData

    public void getMultipleData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), getMultipleDataOnSuccess, onerror);
    }
    void getMultipleDataOnSuccess(GetUserDataResult result)
    {
        if (result.Data.ContainsKey("IndustrialKitchen"))
        {
            IndustrialKitchen = JsonConvert.DeserializeObject<IndustrialKitchen>(result.Data["IndustrialKitchen"].Value);
            IndustrialKitchenVal.text = IndustrialKitchen.totalScoreVal.ToString();
        }
        else if (!result.Data.ContainsKey("IndustrialKitchen"))
        {
            IndustrialKitchenVal.text = "Not Completed";
        }
        //----------------------------------------------------------------------------------------
        if (result.Data.ContainsKey("Office_electric"))
        {
            OfficeElectric = JsonConvert.DeserializeObject<OfficeElectric>(result.Data["Office_electric"].Value);
            officeElectricVal.text = OfficeElectric.totalScoreVal.ToString();
        }
        else if (!result.Data.ContainsKey("Office_electric"))
        {
            officeElectricVal.text = "Not Completed";
        }
        //----------------------------------------------------------------------------------------
        if (result.Data.ContainsKey("Office_Sofa"))
        {
            OfficeSofa = JsonConvert.DeserializeObject<OfficeSofa>(result.Data["Office_Sofa"].Value);
            officeSofaVal.text = OfficeSofa.totalScoreVal.ToString();
        }
        else if (!result.Data.ContainsKey("Office_Sofa"))
        {
            officeSofaVal.text = "Not Completed";
        }
        //----------------------------------------------------------------------------------------
        if (result.Data.ContainsKey("Office_Paper"))
        {
            OfficePaper = JsonConvert.DeserializeObject<OfficePaper>(result.Data["Office_Paper"].Value);
            officePaperVal.text = OfficePaper.totalScoreVal.ToString();
        }
        else if (!result.Data.ContainsKey("Office_Paper"))
        {
            officePaperVal.text = "Not Completed";
        }
        //----------------------------------------------------------------------------------------
        if (result.Data.ContainsKey("LabFire"))
        {
            LabMetal = JsonConvert.DeserializeObject<LabMetal>(result.Data["LabFire"].Value);
            labFireVal.text = LabMetal.totalScoreVal.ToString();
        }
        else if (!result.Data.ContainsKey("LabFire"))
        {
            labFireVal.text = "Not Completed";
        }
        //----------------------------------------------------------------------------------------
        if (result.Data.ContainsKey("Warehouse_Electric"))
        {
            WarehouseElectric = JsonConvert.DeserializeObject<WarehouseElectric>(result.Data["Warehouse_Electric"].Value);
            WarehouseElectricVal.text = WarehouseElectric.totalScoreVal.ToString();
        }
        else if (!result.Data.ContainsKey("Warehouse_Electric"))
        {
            WarehouseElectricVal.text = "Not Completed";
        }
        //----------------------------------------------------------------------------------------
        if (result.Data.ContainsKey("Warehouse_Boxfire"))
        {
            WarehouseWood = JsonConvert.DeserializeObject<WarehouseWood>(result.Data["Warehouse_Boxfire"].Value);
            WarehouseWoodVal.text = WarehouseWood.totalScoreVal.ToString();
        }
        else if (!result.Data.ContainsKey("Warehouse_Boxfire"))
        {
            WarehouseWoodVal.text = "Not Completed";
        }
        //----------------------------------------------------------------------------------------
        if (result.Data.ContainsKey("Warehouse_Oil_Fire"))
        {
            WarehouseOil = JsonConvert.DeserializeObject<WarehouseOil>(result.Data["Warehouse_Oil_Fire"].Value);
            WarehouseOilVal.text = WarehouseOil.totalScoreVal.ToString();
        }
        else if (!result.Data.ContainsKey("Warehouse_Oil_Fire"))
        {
            WarehouseOilVal.text = "Not Completed";
        }
        //----------------------------------------------------------------------------------------
        if (result.Data.ContainsKey("personalDetails"))
        {
            details = JsonConvert.DeserializeObject<details>(result.Data["personalDetails"].Value);
            empIDVal.text = details.ID.ToString();
            empNameVal.text = details.name.ToString();
            empOrgNameVal.text = details.Orgname.ToString();
        }
        //----------------------------------------------------------------------------------------
        

        if(result.Data.ContainsKey("sendReport"))
        {
            //Debug.Log( result.Data["Send_Report"].Value);
            canGenerateReport = JsonConvert.DeserializeObject<canGenerateReport>(result.Data["sendReport"].Value);
            checkbool = canGenerateReport.report;
            if (checkbool == false)
            {
                //alert the user
                canGenerateReportPanel.gameObject.SetActive(true);
                canGenerateReportPanel.gameObject.transform.DOLocalMoveY(-300, timeofdisplay).SetEase(ease).
                    OnComplete(() => f1(canGenerateReportPanel));
            }
        }
        else if (!result.Data.ContainsKey("sendReport"))
        {
            Debug.Log("not available");
            //alert the user
            NAreportPanel.gameObject.SetActive(true);
            NAreportPanel.gameObject.transform.DOLocalMoveY(-300, timeofdisplay).SetEase(ease).
                OnComplete(() => f1(NAreportPanel));
        }
        //----------------------------------------------------------------------------------------

        //alert the user
        if(checkbool == true)
        {
            if (bulk == true && single == false)
            {
                if (index < CanvasSampleOpenFileText.instance.numberOfNewRegister)
                {
                    PrintingManager_Final.instance.GenerateFile(CanvasSampleOpenFileText.instance.empID[index].ToString());
                    logout();

                }
            }
            if (bulk == false && single == true)
            {
                Debug.Log(certificate_login.ToString());
                PrintingManager_Final.instance.GenerateFile(certificate_login.ToString());
                cert_emp_ID.text = "";
                trainerName.text = "";
                logout();
            }
            //debug_Text.text = "data fetched successfully";
            //PrintingManager_Final.instance.GenerateFile();
            //dataFetchPanel.gameObject.SetActive(true);
            //dataFetchPanel.gameObject.transform.DOLocalMoveY(-300, timeofdisplay).SetEase(ease).
            //    OnComplete(() => f2(dataFetchPanel));


        }
    }



    #endregion


    #endregion

    #region logout

    void logout()
    {
        Debug.Log("logged out");
        if (index < CanvasSampleOpenFileText.instance.numberOfNewRegister)
        {
            certificate_generate();
        }
        else if (index >= CanvasSampleOpenFileText.instance.numberOfNewRegister)
        {
            Debug.Log("list over");
            index = 0;
            dataFetchPanel.gameObject.SetActive(true);
            dataFetchPanel.gameObject.transform.DOLocalMoveY(-300, timeofdisplay).SetEase(ease).
                OnComplete(() => f1(dataFetchPanel));
        }
        checkbool = false;
        PlayFabClientAPI.ForgetAllCredentials();

    }

    #endregion

    void f2(GameObject g)
    {
        g.SetActive(false);
    }
    void f1(GameObject g)
    {
        g.gameObject.transform.DOLocalMoveY(-700, timeofdisplay).SetEase(ease)
            .OnComplete(() => f2(g));
    }
}

#region CLASSES

[System.Serializable]
public class details
{
    public string ID;
    public string name;
    public string Orgname;
    public long mobileNumber;
    public string emailAddress;
}

[System.Serializable]
public class ScenarioBase
{
    public int evacuateScoreVal;
    public int alarmScoreVal;
    public int cylinSelectScore;
    public int pullPinScoreVal;
    public int distanceScoreVal;
    public int timeScoreVal;
    public int sweepCntScoreVal;
    public int sweepAngScoreVal;
    public int MatRemainScoreVal;
    public int totalScoreVal;
    public string datetime;
}

[System.Serializable]
public class IndustrialKitchen : ScenarioBase
{
    // Additional properties or methods specific to the IndustrialKitchen scenario
}

[System.Serializable]
public class OfficeElectric : ScenarioBase
{
    // Additional properties or methods specific to the OfficeElectric scenario
}

[System.Serializable]
public class OfficeSofa : ScenarioBase
{
    // Additional properties or methods specific to the OfficeSofa scenario
}

[System.Serializable]
public class OfficePaper : ScenarioBase
{
    // Additional properties or methods specific to the OfficePaper scenario
}

[System.Serializable]
public class LabMetal : ScenarioBase
{
    // Additional properties or methods specific to the LabMetal scenario
}

[System.Serializable]
public class WarehouseElectric : ScenarioBase
{
    // Additional properties or methods specific to the WarehouseElectric scenario
}

[System.Serializable]
public class WarehouseWood : ScenarioBase
{
    // Additional properties or methods specific to the WarehouseWood scenario
}

[System.Serializable]
public class WarehouseOil : ScenarioBase
{
    // Additional properties or methods specific to the WarehouseOil scenario
}


[System.Serializable]
public class canGenerateReport
{
    public bool report;
}

#endregion