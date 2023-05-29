using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using DG.Tweening;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.Events;

public class AdminLoginScript : MonoBehaviour
{
    public static AdminLoginScript instance;
    public GameObject bestBefore;
    string certificate_login;
    string playfabID;
    string output;
    public bool checkbool;

    public expDate expDate;

    [Header("inputFields")]
    public TMP_InputField adminLogin;
    public TMP_InputField adminPassword;

    public GameObject admin_login_panel;
    public GameObject bulk_single_cert_panel;
    [Header("ALERT PANELS")]
    public GameObject adminSuccessPanel;
    public GameObject adminErrorPanel;

    public Ease ease;
    public float timeofdisplay;

    public static UnityEvent<string, string> onUserDataRecieved = new UnityEvent<string, string>();

    private void Awake()
    {
        instance = this;
    }

    #region globalError

    void onerror(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        //alert the user
        adminErrorPanel.gameObject.SetActive(true);
        adminErrorPanel.gameObject.transform.DOLocalMoveY(-300, timeofdisplay).SetEase(ease).
            OnComplete(() => f1(adminErrorPanel));
            
    }

    #endregion

    void f1(GameObject g)
    {
        g.gameObject.transform.DOLocalMoveY(-700, timeofdisplay).SetEase(ease)
            .OnComplete(()=>g.SetActive(false));

    }
    void f2()
    {
        adminSuccessPanel.gameObject.transform.DOLocalMoveY(-700, timeofdisplay).SetEase(ease)
            .OnComplete(() => adminSuccessPanel.SetActive(false));
        admin_login_panel.gameObject.SetActive(false);
        bulk_single_cert_panel.SetActive(true);
    }

    #region loginPlayer
    public void loginPlayer(string email,string password)
    {
        var request = new LoginWithEmailAddressRequest()
        {
            Email = "\"" + email +"\"" + "@gmail.com",
            Password = password
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, onloginsuccess, onerror);

    }
    void onloginsuccess(LoginResult result)
    {
        Debug.Log("logged in!");
        //alert the user
        bestBefore.gameObject.SetActive(true);
        getData("expDate");
        adminSuccessPanel.SetActive(true);
        adminSuccessPanel.gameObject.transform.DOLocalMoveY(-300, timeofdisplay).SetEase(ease).
            OnComplete(() => f2());
        //logout();
    }
    #endregion

    #region get data
    public string getData(string key)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            Keys = new List<string>() { key }
        }, response =>
        {
            Debug.Log("successful getData");
            if (response.Data.ContainsKey(key))
            {
                output = response.Data[key].Value;
                onUserDataRecieved.Invoke(key, output);
                Debug.Log(output);
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
        if (result.Data.ContainsKey("expDate"))
        {
            expDate = JsonConvert.DeserializeObject<expDate>(result.Data["expDate"].Value);
            BestBefore.instance.Year = expDate.Year;
            BestBefore.instance.Day = expDate.Day;
            BestBefore.instance.Hour = expDate.Hour;
            BestBefore.instance.Minute = expDate.Minute;
            BestBefore.instance.Month = (BestBefore.MonthsOfYear)expDate.Month;


            //Debug.Log(expDate.Day);
            //Debug.Log(expDate.Minute);
            //Debug.Log(expDate.Month);
            //Debug.Log(expDate.Second);
            //Debug.Log(expDate.Hour);
            
            logout();
        }
        else if (!result.Data.ContainsKey("expDate"))
        {

        }

    }


    #endregion

    #endregion

    #region logout

    void logout()
    {
        Debug.Log("logged out");
        PlayFabClientAPI.ForgetAllCredentials();
    }

    #endregion


    public void loginButton()
    {
        loginPlayer(adminLogin.text, adminPassword.text);
    }
}

[System.Serializable]
public class expDate
{
    public enum MonthsOfYear
    {
        January = 1,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

    [Header("Expiration Date")]
    [Range(1, 9999)]
    public int Year = 2018;
    public MonthsOfYear Month = MonthsOfYear.January;
    [Range(1, 31)]
    public int Day = 1;
    [Range(0, 23)]
    public int Hour = 0;
    [Range(0, 59)]
    public int Minute = 0;
}


