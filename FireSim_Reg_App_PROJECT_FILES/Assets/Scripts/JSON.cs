using UnityEngine;
using System.IO;
using TMPro;
public class JSON : MonoBehaviour
{
    Userdata userdata;
    string path;
    public TextMeshProUGUI dispText;
    private void Start()
    {
        path = Application.persistentDataPath + "/jsontest.txt";
        Debug.Log(path);
        //FromToJSON();
        readFromJson();
        //write once to jsontest.txt
    }
    void FromToJSON()
    {
        //populate json initally

        userdata = new Userdata();
        userdata.h = Random.Range(0, 10);
        userdata.l = Random.Range(0, 10);
        userdata.k = new Vector3(userdata.h, userdata.h, userdata.h);
        userdata.name = "linda";



        //write to json
        string json = JsonUtility.ToJson(userdata);
        File.WriteAllText(Application.persistentDataPath+ "/jsontest.txt", json);
    }

    void readFromJson()
    { 

        string h = File.ReadAllText(Application.persistentDataPath + "/jsontest.txt");
        Userdata g = JsonUtility.FromJson<Userdata>(h);

        dispText.text = g.name;
        
        /*Debug.Log(g.k);
        Debug.Log(g.test);
        Debug.Log(g.name);*/
    }
}
 
[System.Serializable]
public class Userdata
{
    public int l;
    public float h;
    public Vector3 k;
    public bool test;
    public string name;
}
