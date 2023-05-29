using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;
using TMPro;

[RequireComponent(typeof(Button))]
public class CanvasSampleSaveFileText : MonoBehaviour, IPointerDownHandler {

    public static CanvasSampleSaveFileText instance;

    public TextMeshProUGUI output;
    [Header("ALERT PANELS")]
    public GameObject savePanel;

    // Sample text data
    public string _data ;


#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void DownloadFile(string gameObjectName, string methodName, string filename, byte[] byteArray, int byteArraySize);

    // Broser plugin should be called in OnPointerDown.
    public void OnPointerDown(PointerEventData eventData) {
        var bytes = Encoding.UTF8.GetBytes(_data);
        DownloadFile(gameObject.name, "OnFileDownload", "sample.txt", bytes, bytes.Length);
    }

    // Called from browser
    public void OnFileDownload() {
        output.text = "File Successfully Downloaded";
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

    // Listen OnClick event in standlone builds
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick() 
    {
        var path = StandaloneFileBrowser.SaveFilePanel("Title", "", "sample", "txt");
        Debug.Log(path);
        if (!string.IsNullOrEmpty(path)) 
        {
            returnData();
            File.WriteAllText(path, _data);
        }

        savePanel.gameObject.SetActive(true);
    }

    string returnData()
    {

        _data = "Name : " +                                     ReportGenerationScript.instance.empNameVal.text             +"\n\n"+
                "ID : " +                                       ReportGenerationScript.instance.empIDVal.text               + "\n\n" +
                "OrgName : " +                                  ReportGenerationScript.instance.empOrgNameVal.text          + "\n\n" +
                " Class A - Office - Combustible - sofa : " +   ReportGenerationScript.instance.officeSofaVal.text          + "\n" +
                " Class A - Office - Combustible - paper : " +  ReportGenerationScript.instance.officePaperVal.text         + "\n"+
                " Class A - Warehouse - Wood : "+               ReportGenerationScript.instance.WarehouseWoodVal.text       +"\n"+
                " Class B - Warehouse - Oil : "+                ReportGenerationScript.instance.WarehouseOilVal.text        + "\n"+
                " Class C - Office - electric : " +             ReportGenerationScript.instance.officeElectricVal.text      + "\n" +
                " Class C - Warehouse - Electric : "+           ReportGenerationScript.instance.WarehouseElectricVal.text   +"\n"+
                " Class D - Laboratory - Metal : "+             ReportGenerationScript.instance.labFireVal.text             +"\n"+
                " Class K - Kitchen - Oil : " +                 ReportGenerationScript.instance.IndustrialKitchenVal.text   + "\n"
                +"\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "Signature";
                        
        return _data;
    }
#endif
}