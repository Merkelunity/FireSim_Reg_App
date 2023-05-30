
using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
public class Remote_Config_Test : MonoBehaviour
{
    //public struct userAttr { }
    //public struct appAttr { }

    //void Start()
    //{
    //    ConfigManager.FetchCompleted += getDataFromRC;
    //    ConfigManager.FetchConfigs<userAttr, appAttr>(new userAttr(), new appAttr());
    //}

    //void getDataFromRC(ConfigResponse response)
    //{
    //    bool b = ConfigManager.appConfig.GetBool("TestBool");
    //    Debug.Log(b);
    //    //Debug.Log(response.body);
    //    //Debug.Log(response.headers);
    //    //Debug.Log(response.requestOrigin);
    //    //Debug.Log(response.status);

    //}

    //private void OnDestroy()
    //{
    //    ConfigManager.FetchCompleted -= getDataFromRC;
    //    Debug.Log("destroyed");
    //}

    public struct userAttributes { }
    public struct appAttributes { }

    async Task InitializeRemoteConfigAsync()
    {
        // initialize handlers for unity game services
        await UnityServices.InitializeAsync();

        // remote config requires authentication for managing environment information
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    async Task Start()
    {
        // initialize Unity's authentication and core services, however check for internet connection
        // in order to fail gracefully without throwing exception if connection does not exist
        if (Utilities.CheckForInternetConnection())
        {
            await InitializeRemoteConfigAsync();
        }

        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
    }

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        Debug.Log("RemoteConfigService.Instance.appConfig fetched: " + RemoteConfigService.Instance.appConfig.config.ToString());
        bool b = RemoteConfigService.Instance.appConfig.GetBool("TestBool");
        float s = RemoteConfigService.Instance.appConfig.GetFloat("FLOAT");
        Debug.Log(b);
        ReportGenerationScript.instance.fileSaveText.text = s.ToString();
    }
}

