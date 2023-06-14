using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json;
using DG.Tweening;
public class Playfab_manager : MonoBehaviour
{
    string password;
    public static Playfab_manager instance;
    [Header("ALERT PANELS")]
    public GameObject bulkCompletePanel;
    public GameObject circleSlider;
    public Ease ease;
    public float timeofdisplay;
    string playfabID;

    public static UnityEvent<string, string> onUserDataRecieved = new UnityEvent<string, string>();

    [HideInInspector]
    public string output;
    string s;

    int index = 0;

    public personalDetails personalDetails;
    string loginData;

    private void Awake()
    {
        instance = this;
        index = 0;
        circleSlider.gameObject.SetActive(false);
    }
    
    #region globalError

    void onerror(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    void onregisterError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        loginPlayer(loginData);
    }
    #endregion

    #region REGISTER
    public void register(string email)
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = email + "@gmail.com",
            Password = "______",
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, onregisterSuccess, onregisterError);
    }

    void onregisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("user registered successfully");
        loginPlayer(loginData);

    }
    #endregion

    #region bulkRegister

    public void bulkregister()
    {
        register( "\"" + CanvasSampleOpenFileText.instance.empID[index]+ "\"" );
        loginData = "\"" + CanvasSampleOpenFileText.instance.empID[index].ToString()+"\"" ;

    }

    #endregion

    #region loginPlayer
    public void loginPlayer(string email)
    {
        var request = new LoginWithEmailAddressRequest()
        {
            Email =  email+ "@gmail.com",
            Password = "______"
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, onloginsuccess, onerror);

    }
    void onloginsuccess(LoginResult result)
    {
        playfabID = result.PlayFabId;
        Debug.Log("logged in!");

        personalDetails.ID = CanvasSampleOpenFileText.instance.empID[index];
        personalDetails.name = CanvasSampleOpenFileText.instance.empName[index];
        personalDetails.emailAddress = CanvasSampleOpenFileText.instance.emailAddress[index];
        personalDetails.mobileNumber = CanvasSampleOpenFileText.instance.mobileNumber[index];
        personalDetails.Orgname = CanvasSampleOpenFileText.instance.OrgName[index];

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
    void ondatasend(UpdateUserDataResult result)
    {
        Debug.Log("data has been sent");
        logout();
    }

    #endregion

    #region logout

    void logout()
    {

        PlayFabClientAPI.ForgetAllCredentials();
        Debug.Log("player has logged out");
        //circular loader
        CanvasSampleOpenFileText.instance.Reg_slider.value = index;
        if (index < CanvasSampleOpenFileText.instance.numberOfNewRegister-1)
        {
            index++;
            Debug.Log(index);
            bulkregister();
        }
        else if(index >= CanvasSampleOpenFileText.instance.numberOfNewRegister-1)
        {
            bulkCompletePanel.gameObject.SetActive(true);
            bulkCompletePanel.gameObject.transform.DOLocalMoveY(-300, timeofdisplay).SetEase(ease).
                OnComplete(() => f1(bulkCompletePanel));
        }

    }

    #endregion
    void f1(GameObject g)
    {
        g.gameObject.transform.DOLocalMoveY(-700, timeofdisplay).SetEase(ease)
            .OnComplete(() => g.SetActive(false))
            .OnComplete(()=>circleSlider.gameObject.SetActive(false));

    }

}

[System.Serializable]
public class personalDetails
{
    public string ID;
    public string name;
    public long mobileNumber;
    public string emailAddress;
    public string Orgname;
}

