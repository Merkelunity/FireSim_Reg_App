using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json;
using DG.Tweening;

public class SingleplayfabRegister : MonoBehaviour
{
    public static SingleplayfabRegister instance;

    //string playfabID;
    string loginData;

    [Header("ALERT PANELS")]
    public GameObject registerErrorPanel;
    public GameObject onsuccessPanel;


    public Ease ease;
    public float timeofdisplay;

    [Header("DEBUGGING")]
    public TextMeshProUGUI debug_Text;
    public singlepersonalDetails personalDetails;

    [Header("INPUT FIELDS")]
    public TMP_InputField emp_email;
    public TMP_InputField emp_name;
    public TMP_InputField emp_mobileNumber;
    public TMP_InputField emp_Orgname;
    public TMP_InputField emp_ID;
    public TMP_InputField cert_emp_ID;

    [Header("ADMIN")]
    public TMP_InputField admin;
    public TMP_InputField admin_pwd;


    public static UnityEvent<string, string> onUserDataRecieved = new UnityEvent<string, string>();


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        registerErrorPanel.gameObject.SetActive(false);
        onsuccessPanel.gameObject.SetActive(false);
    }

    #region QUIT

    public void app_quit()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }

    #endregion 

    #region initialization

    void personalDetails_init()
    {
        //emp_email.text = "dhin18124";
        //emp_name.text = "dhin";
        //emp_mobileNumber.text = "123456789";
        //emp_Orgname.text = "MH";
        //emp_ID.text = "qwe";

        personalDetails.emailAddress = emp_email.text;
        personalDetails.name = emp_name.text;
        personalDetails.mobileNumber = long.Parse(emp_mobileNumber.text);
        personalDetails.Orgname = emp_Orgname.text;
        personalDetails.ID = emp_ID.text;
    }
    #endregion

    #region clearAll

    public void clearAll()
    {
        debug_Text.text = "";
        emp_email.text = "";
        emp_ID.text = "";
        emp_mobileNumber.text = "";
        emp_name.text = "";
        emp_Orgname.text = "";
        cert_emp_ID.text = "";
        admin.text = "";
        admin_pwd.text = "";
    }

    #endregion


    #region globalError

    public void onerror(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        debug_Text.text = error.GenerateErrorReport();
    }

    public void onregisterError(PlayFabError error)
    {
        
        Debug.Log(error.GenerateErrorReport());
        debug_Text.text = error.GenerateErrorReport();
        //alert the user
        registerErrorPanel.gameObject.SetActive(true);
        registerErrorPanel.gameObject.transform.DOLocalMoveY(-300, timeofdisplay).SetEase(ease).
            OnComplete(() => f1(registerErrorPanel));
        emp_ID.text = "";


    }
    void f1(GameObject g)
    {
        g.gameObject.transform.DOLocalMoveY(-700, timeofdisplay).SetEase(ease)
            .OnComplete(() => g.SetActive(false));
    }
    #endregion

    #region REGISTER


    public void singleregister()
    {
        personalDetails_init();
        var request = new RegisterPlayFabUserRequest
        {
            Email = "\"" + personalDetails.ID + "\"" + "@gmail.com",
            Password = "______",
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, onregisterSuccess, onregisterError);
        
    }
    void onregisterSuccess(RegisterPlayFabUserResult result)
    {
        debug_Text.text = "user registered successfully";
        loginData = personalDetails.ID.ToString();
        loginPlayer(loginData);

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
        //playfabID = result.PlayFabId;
        debug_Text.text = "logged in";
        loginData = personalDetails.ID.ToString();
        updateUserName(loginData);
    }
    #endregion

    #region updateName

    public void updateUserName(string displayname)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName ="USHA - " + displayname.ToString()
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, ondisplayName, onerror);
    }

    void ondisplayName(UpdateUserTitleDisplayNameResult result)
    {
        setMultipleData("personalDetails");
        Debug.Log(result.DisplayName);
    }



    #endregion

    #region send data
    //public void sendUserData(string string1key, string string2Val)
    //{
    //    var request = new UpdateUserDataRequest
    //    {
    //        Data = new Dictionary<string, string>
    //        {
    //            { string1key.ToString() , string2Val.ToString() },
    //        }
    //    };
    //    PlayFabClientAPI.UpdateUserData(request, ondatasend, onerror);
    //}
    void ondatasend(UpdateUserDataResult result)
    {
        Debug.Log("data has been sent");
        debug_Text.text = "data has been sent";
        logout();
    }
    #endregion

    #region setMultipleData

    void setMultipleData(string key)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { key,JsonConvert.SerializeObject(personalDetails) }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, ondatasend, onerror);
    }

    #endregion

    #region logout

    void logout()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        debug_Text.text = "player has logged out";
        Debug.Log("player has logged out");
        //alert the user
        onsuccessPanel.gameObject.SetActive(true);
        onsuccessPanel.gameObject.transform.DOLocalMoveY(-300, timeofdisplay).SetEase(ease).
            OnComplete(() => f1(onsuccessPanel));


    }

    #endregion

}

//--------------------------------------------------------------------

//--------------------------------------------------------------------

//--------------------------------------------------------------------

#region  _CLASSES

[System.Serializable]
public class singlepersonalDetails
{
    public string ID;
    public string name;
    public long mobileNumber;
    public string emailAddress;
    public string Orgname;
  
}

#endregion