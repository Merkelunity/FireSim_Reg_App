using System.Xml;
using System.IO;
using UnityEngine;

public class XML : MonoBehaviour
{
    private void Start()
    {
        writetoXML();
        //getfromXML();
    }

    #region set data ---------------------------------------------------------------------------
    void writetoXML()
    {
        XmlDocument xmlDocument = new XmlDocument();

        XmlElement root = xmlDocument.CreateElement("root");
        xmlDocument.AppendChild(root);

        WRITE_TO_XML(xmlDocument , root ,"username", "john");

        /*XmlElement child1 = xmlDocument.CreateElement("child1");
        child1.InnerText = "childNumber=01";
        root.AppendChild(child1);

        XmlElement child2 = xmlDocument.CreateElement("child2");
        child2.InnerText = "childNumber=02";
        root.AppendChild(child2);*/

        xmlDocument.Save(Application.dataPath + "/XMLTest.txt");
        if(!File.Exists(Application.dataPath + "/XMLTest.txt"))
        {
            xmlDocument.Save(Application.dataPath + "/XMLTest.txt");
        }
    }
    #endregion

    #region get data ---------------------------------------------------------------------------

    void getfromXML()
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(Application.dataPath + "/XMLTest.txt");

        XmlNodeList g = xmlDocument.GetElementsByTagName("child2");
        string n = g[0].InnerText;
        Debug.Log(n);
    }

    #endregion

    void WRITE_TO_XML(  XmlDocument xmlDocument , XmlElement root  , string tag , string innerText)
    {

        XmlElement x = xmlDocument.CreateElement(tag);
        x.InnerText = innerText;
        root.AppendChild(x);

    }
}
