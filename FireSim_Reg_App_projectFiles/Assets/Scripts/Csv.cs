using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;

public class Csv : MonoBehaviour
{
    public static Csv instance;
    string filename;
    [HideInInspector]
    public int numberOfNewRegister;
    public List<string> empID;
    public List<string> empName;
    public List<long> mobileNumber;
    public List<string> emailAddress;
    public TextMeshProUGUI text;



    private void Awake()
    {
        instance = this;
        filename = Application.dataPath + "/test.csv";
    }
   

    #region readFromCSV ---------------------------------------------------------------------------------------------------------------
    public void readCSV(string[] path)
    {
        StreamReader strReader = new StreamReader(path[0]);
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
            Debug.Log(val[0]+","+val[1]+","+val[2]+","+val[3]);
            text.text= (val[0] + "," + val[1] + "," + val[2] + "," + val[3]);
            empID.Add(val[0]);//empid
            empName.Add(val[1]);//empname
            mobileNumber.Add(long.Parse( val[2]));//mobileNumber
            emailAddress.Add(val[3]);//emailaddress
           

            numberOfNewRegister++;
            Debug.Log(numberOfNewRegister);
        }
        
    }
    #endregion

    #region writeToCSV ---------------------------------------------------------------------------------------------------------------
    public void writeToCSV(string name , int id,int w)
    {
        TextWriter tw = new StreamWriter(filename, false);
        tw.WriteLine("name,id,w");
        tw.Close();

        tw = new StreamWriter(filename, true);
        tw.WriteLine(name + "," + id + "," + w);
        tw.Close();
        

    }
    #endregion
}
