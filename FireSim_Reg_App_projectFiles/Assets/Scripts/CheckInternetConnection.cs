using UnityEngine;
using DG.Tweening;

public class CheckInternetConnection : MonoBehaviour
{
    public GameObject panel;
    [Header("ALERT PANELS")]
    public GameObject intconnPanel;
    private const bool allowCarrierDataNetwork = false;
    private const string pingAddress = "8.8.8.8"; // Google Public DNS server
    private const float waitingTime = 2.0f;
    private Ping ping;
    private float pingStartTime;

    public Ease ease;
    public float timeofdisplay;

    private void Awake()
    {
        InvokeRepeating(nameof(function), 2.5f, 2.5f);
    }

    void function()
    {
        bool internetPossiblyAvailable;
        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                internetPossiblyAvailable = true;
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                internetPossiblyAvailable = allowCarrierDataNetwork;
                break;
            default:
                internetPossiblyAvailable = false;
                break;
        }
        if (!internetPossiblyAvailable)
        {
            InternetIsNotAvailable();
            return;
        }
        ping = new Ping(pingAddress);
        pingStartTime = Time.time;

    }

    public void Update()
    {
        if (ping != null)
        {
            bool stopCheck = true;
            if (ping.isDone)
            {
                if (ping.time >= 0)
                    InternetAvailable();
                
                else
                    InternetIsNotAvailable();
            }
            else if (Time.time - pingStartTime < waitingTime)
                stopCheck = false;
            else
                InternetIsNotAvailable();
            if (stopCheck)
                ping = null;
        }
    }

    private void InternetIsNotAvailable()
    {
        Debug.LogWarning("No Internet :(");
        //alert the user
        intconnPanel.gameObject.SetActive(true);
        panel.gameObject.SetActive(false);
    }


    private void InternetAvailable()
    {
        Debug.LogWarning("Internet is available! ;)");
        //alert the user
        intconnPanel.gameObject.SetActive(false);
        panel.gameObject.SetActive(true);
    }
}