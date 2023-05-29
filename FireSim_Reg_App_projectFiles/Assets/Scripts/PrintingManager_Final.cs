using System;
using UnityEngine;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;

public class PrintingManager_Final : MonoBehaviour
{
    public static PrintingManager_Final instance;
    string path = null;

    public bool generateAtStart;

    //[HideInInspector]
    //public int pdfName;

    int check = 0;
    int j;

    private void Awake()
    {
        instance = this;
        //pdfName = 0;
    }

    void Start()
    {
        //pdfName = 0;
        //ReportGenerationScript.instance.paths[0] = "";
        if (generateAtStart)
            GenerateFile("");
    }


    public void GenerateFile(string fileName)
    {
        //if(ReportGenerationScript.instance.paths[0] == null)
        //{
        //    path = Application.persistentDataPath + fileName + ".pdf";
        //}
        path = ReportGenerationScript.instance.paths[0]+"\\" + fileName + ".pdf";

        if (File.Exists(path))
            File.Delete(path);

        Paragraph spacing = new Paragraph("\n");

        using (var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
        {
            var document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            var writer = PdfWriter.GetInstance(document, fileStream);

            document.Open();

            document.NewPage();

            #region Page1

            document.Add(spacing);
            document.Add(spacing);
            document.Add(spacing);
            document.Add(spacing);

            var Fontcolor = FontFactory.GetFont("TIMES_ROMAN", 18, iTextSharp.text.Font.BOLD, BaseColor.RED.Darker());
            Paragraph p = new Paragraph(string.Format("BASIC FIRE SAFETY TRAINING"), Fontcolor);
            p.Alignment = Element.ALIGN_CENTER;
            document.Add(p);

            var Fontcolor1 = FontFactory.GetFont("TIMES_ROMAN", 16, BaseColor.BLACK.Brighter());
            Paragraph pq = new Paragraph(string.Format("This Certificate acknowledges that"), Fontcolor1);
            pq.Alignment = Element.ALIGN_CENTER;
            document.Add(pq);

            document.Add(spacing);

            var bold = FontFactory.GetFont("TIMES_ROMAN", 22, iTextSharp.text.Font.BOLD, BaseColor.BLACK.Darker());
            Paragraph name = new Paragraph(ReportGenerationScript.instance.empNameVal.text, bold);
            name.Alignment = Element.ALIGN_CENTER;
            document.Add(name);

            var bold1 = FontFactory.GetFont("TIMES_ROMAN", 20, BaseColor.BLACK.Darker());
            Paragraph id = new Paragraph(ReportGenerationScript.instance.empIDVal.text + "," +ReportGenerationScript.instance.empOrgNameVal.text, bold1);
            id.Alignment = Element.ALIGN_CENTER;
            document.Add(id);

            document.Add(spacing);

            Paragraph w = new Paragraph("has successfully fulfilled the requirement for", Fontcolor1);
            w.Alignment = Element.ALIGN_CENTER;
            document.Add(w);

            Paragraph e = new Paragraph("the BASIC FIRE SAFETY TRAINING ", Fontcolor1);
            e.Alignment = Element.ALIGN_CENTER;
            document.Add(e);

            document.Add(spacing);
            document.Add(spacing);

            var datefont = FontFactory.GetFont("TIMES_ROMAN", 18, BaseColor.BLACK.Brighter());
            Paragraph date = new Paragraph(" Date : " + System.DateTime.UtcNow.ToString("dd-MM-yyyy") + "         ", datefont);
            date.Alignment = Element.ALIGN_RIGHT;
            document.Add(date);

            document.Add(spacing);
            document.Add(spacing);
            document.Add(spacing);

            Paragraph r = new Paragraph("TRAINER : " + ReportGenerationScript.instance.trainerString.ToString());
            r.Alignment = Element.ALIGN_RIGHT;
            document.Add(r);

            document.Add(spacing);

            Paragraph t = new Paragraph("ORGANISATION NAME" + "           ");
            t.Alignment = Element.ALIGN_RIGHT;
            document.Add(t);

            document.Add(spacing);

            Paragraph y = new Paragraph("____________________________________________________________________");
            y.Alignment = Element.ALIGN_CENTER;
            document.Add(y);


            #endregion

            document.NewPage();

            #region PAGE2

            Paragraph u = new Paragraph("Detailed Report");
            u.Alignment = Element.ALIGN_BASELINE;
            document.Add(u);

            Paragraph i = new Paragraph("____________________________________________________________________");
            i.Alignment = Element.ALIGN_LEFT;
            document.Add(i);

            #region scenario 1

            if (ReportGenerationScript.instance.IndustrialKitchenVal.text == "Not Completed") { }
            else if (ReportGenerationScript.instance.IndustrialKitchenVal != null)
            {
                Type type = Type.GetType(string.Format("IndustrialKitchen"));
                object t1 = Activator.CreateInstance(type);

                IndustrialKitchen testClass = t1 as IndustrialKitchen;

                Type type1 = Type.GetType(string.Format("ClassA"));
                object t11 = Activator.CreateInstance(type1);

                ClassA testClass1 = t11 as ClassA;



                int[] score =
                    {
                        testClass1.alarmScoreVal,
                        testClass1.evacuateScoreVal,
                        testClass1.cylinSelectScore,
                        testClass1.pullPinScoreVal,
                        testClass1.sweepCntScoreVal,
                        testClass1.sweepAngScoreVal,
                        testClass1.MatRemainScoreVal,
                        testClass1.distanceScoreVal,
                        testClass1.timeScoreVal,
                    };
                AddScenarioDetails(document, "Scenario - Industrial kitchen", "Class - K", "Max Score - 100", testClass1.totalScoreVal);

                document.Add(spacing);

                makeTable(document, score);

                check++;
                addNewPage(document);
            }
            #endregion

            #region scenario 2

            if (ReportGenerationScript.instance.labFireVal.text == "Not Completed") { }
            else if (ReportGenerationScript.instance.labFireVal != null)
            {
                Type type = Type.GetType(string.Format("LabMetal"));
                object t1 = Activator.CreateInstance(type);

                LabMetal testClass = t1 as LabMetal;

                Type type1 = Type.GetType(string.Format("ClassB"));
                object t11 = Activator.CreateInstance(type1);

                ClassB testClass1 = t11 as ClassB;

                int[] score =
                    {
                        testClass1.alarmScoreVal,
                        testClass1.evacuateScoreVal,
                        testClass1.cylinSelectScore,
                        testClass1.pullPinScoreVal,
                        testClass1.sweepCntScoreVal,
                        testClass1.sweepAngScoreVal,
                        testClass1.MatRemainScoreVal,
                        testClass1.distanceScoreVal,
                        testClass1.timeScoreVal,
                    };

                AddScenarioDetails(document, "Scenario - Lab", "Class - D", "Max Score - 100", testClass1.totalScoreVal);

                document.Add(spacing);

                makeTable(document, score);

                check++;
                addNewPage(document);
            }

            #endregion

            #region scenario 3

            if (ReportGenerationScript.instance.officePaperVal.text == "Not Completed") { }
            else if (ReportGenerationScript.instance.officePaperVal != null)
            {
                Type type = Type.GetType(string.Format("OfficePaper"));
                object t1 = Activator.CreateInstance(type);

                OfficePaper testClass = t1 as OfficePaper;


                Type type1 = Type.GetType(string.Format("ClassC"));
                object t11 = Activator.CreateInstance(type1);

                ClassC testClass1 = t11 as ClassC;

                int[] score =
                    {
                        testClass1.alarmScoreVal,
                        testClass1.evacuateScoreVal,
                        testClass1.cylinSelectScore,
                        testClass1.pullPinScoreVal,
                        testClass1.sweepCntScoreVal,
                        testClass1.sweepAngScoreVal,
                        testClass1.MatRemainScoreVal,
                        testClass1.distanceScoreVal,
                        testClass1.timeScoreVal,
                    };

                AddScenarioDetails(document, "Scenario - Office", "Class - A", "Max Score - 100", testClass1.totalScoreVal);

                document.Add(spacing);

                makeTable(document, score);

                check++;
                addNewPage(document);

            }
            #endregion

            #region scenario 4

            if (ReportGenerationScript.instance.officeElectricVal.text == "Not Completed") { }
            else if (ReportGenerationScript.instance.officeElectricVal != null)
            {

                Type type = Type.GetType(string.Format("OfficeElectric"));
                object t1 = Activator.CreateInstance(type);

                OfficeElectric testClass = t1 as OfficeElectric;

                Type type1 = Type.GetType(string.Format("ClassD"));
                object t11 = Activator.CreateInstance(type1);

                ClassD testClass1 = t11 as ClassD;

                

                int[] score =
                    {
                        testClass1.alarmScoreVal,
                        testClass1.evacuateScoreVal,
                        testClass1.cylinSelectScore,
                        testClass1.pullPinScoreVal,
                        testClass1.sweepCntScoreVal,
                        testClass1.sweepAngScoreVal,
                        testClass1.MatRemainScoreVal,
                        testClass1.distanceScoreVal,
                        testClass1.timeScoreVal,
                    };


                AddScenarioDetails(document, "Scenario - Office", "Class - C", "Max Score - 100", testClass1.totalScoreVal);

                document.Add(spacing);

                makeTable(document, score);

                check++;
                addNewPage(document);

            }
            #endregion

            #region scenario 5

            if (ReportGenerationScript.instance.officeSofaVal.text == "Not Completed") { }
            else if (ReportGenerationScript.instance.officeSofaVal != null)
            {

                Type type = Type.GetType(string.Format("OfficeSofa"));
                object t1 = Activator.CreateInstance(type);

                OfficeSofa testClass = t1 as OfficeSofa;

                Type type1 = Type.GetType(string.Format("ClassE"));
                object t11 = Activator.CreateInstance(type1);

                ClassE testClass1 = t11 as ClassE;

                int[] score =
                    {
                        testClass1.alarmScoreVal,
                        testClass1.evacuateScoreVal,
                        testClass1.cylinSelectScore,
                        testClass1.pullPinScoreVal,
                        testClass1.sweepCntScoreVal,
                        testClass1.sweepAngScoreVal,
                        testClass1.MatRemainScoreVal,
                        testClass1.distanceScoreVal,
                        testClass1.timeScoreVal,
                    };


                AddScenarioDetails(document, "Scenario - Office", "Class - A", "Max Score - 100", testClass1.totalScoreVal);

                makeTable(document, score);

                check++;
                addNewPage(document);

            }
            #endregion

            #region scenario 6

            if (ReportGenerationScript.instance.WarehouseElectricVal.text == "Not Completed") { }
            else if (ReportGenerationScript.instance.WarehouseElectricVal != null)
            {
                Type type = Type.GetType(string.Format("WarehouseElectric"));
                object t1 = Activator.CreateInstance(type);

                WarehouseElectric testClass = t1 as WarehouseElectric;

                Type type1 = Type.GetType(string.Format("ClassF"));
                object t11 = Activator.CreateInstance(type1);

                ClassF testClass1 = t11 as ClassF;

                int[] score =
                    {
                        testClass1.alarmScoreVal,
                        testClass1.evacuateScoreVal,
                        testClass1.cylinSelectScore,
                        testClass1.pullPinScoreVal,
                        testClass1.sweepCntScoreVal,
                        testClass1.sweepAngScoreVal,
                        testClass1.MatRemainScoreVal,
                        testClass1.distanceScoreVal,
                        testClass1.timeScoreVal,
                    };


                AddScenarioDetails(document, "Scenario - WareHouse", "Class - C", "Max Score - 100", testClass1.totalScoreVal);

                document.Add(spacing);

                makeTable(document, score);

                check++;
                addNewPage(document);

            }
            #endregion

            #region scenario 7

            if (ReportGenerationScript.instance.WarehouseWoodVal.text == "Not Completed") { }
            else if (ReportGenerationScript.instance.WarehouseWoodVal != null)
            {

                Type type = Type.GetType(string.Format("WarehouseWood"));
                object t1 = Activator.CreateInstance(type);

                WarehouseWood testClass = t1 as WarehouseWood;

                Type type1 = Type.GetType(string.Format("ClassG"));
                object t11 = Activator.CreateInstance(type1);

                ClassG testClass1 = t11 as ClassG;

                int[] score =
                    {
                        testClass1.alarmScoreVal,
                        testClass1.evacuateScoreVal,
                        testClass1.cylinSelectScore,
                        testClass1.pullPinScoreVal,
                        testClass1.sweepCntScoreVal,
                        testClass1.sweepAngScoreVal,
                        testClass1.MatRemainScoreVal,
                        testClass1.distanceScoreVal,
                        testClass1.timeScoreVal,
                    };



                AddScenarioDetails(document, "Scenario - WareHouse", "Class - C", "Max Score - 100", testClass1.totalScoreVal);

                document.Add(spacing);

                makeTable(document, score);

                check++;
                addNewPage(document);

            }
            #endregion

            #region scenario 8

            if (ReportGenerationScript.instance.WarehouseOilVal.text == "Not Completed") { }
            else if (ReportGenerationScript.instance.WarehouseOilVal != null)
            {

                Type type = Type.GetType(string.Format("WarehouseOil"));
                object t1 = Activator.CreateInstance(type);

                WarehouseOil testClass = t1 as WarehouseOil;

                Type type1 = Type.GetType(string.Format("ClassH"));
                object t11 = Activator.CreateInstance(type1);

                ClassH testClass1 = t11 as ClassH;

                int[] score =
                    {
                        testClass1.alarmScoreVal,
                        testClass1.evacuateScoreVal,
                        testClass1.cylinSelectScore,
                        testClass1.pullPinScoreVal,
                        testClass1.sweepCntScoreVal,
                        testClass1.sweepAngScoreVal,
                        testClass1.MatRemainScoreVal,
                        testClass1.distanceScoreVal,
                        testClass1.timeScoreVal,
                    };




                AddScenarioDetails(document, "Scenario - WareHouse", "Class - B", "Max Score - 100", testClass1.totalScoreVal);

                document.Add(spacing);

                makeTable(document, score);

                check++;
                addNewPage(document);

            }
            #endregion

            #endregion

            document.Close();
            writer.Close();

            StartCoroutine( PrintFiles() );

        void AddScenarioDetails(Document document, string scenario, string className, string maxScore, int obtainedScore)
    {
        document.Add(new Paragraph(scenario));
        document.Add(new Paragraph(className));
        document.Add(new Paragraph(maxScore));
        document.Add(new Paragraph("Obtained Score - " + obtainedScore));
    }

        void makeTable(Document document,int[] i)
        {
            PdfPTable table = new PdfPTable(3);

            for (int num = 0; num <= 11; num++)
            {
                switch (num)
                {
                    case 0:
                        tableHeaders(table);
                        break;
                    case 1:
                        Addthis(num.ToString(), "Alarm sounded", i[num-1].ToString(),table);
                        break;
                    case 2:
                        Addthis(num.ToString(), "Evacuate", i[num-1].ToString(),table);
                        break;
                    case 3:
                        Addthis(num.ToString(), "Cylinder selection", i[num-1].ToString(),table);
                        break;
                    case 4:
                        Addthis(num.ToString(), "Pull pin", i[num-1].ToString(),table);
                        break;
                    case 5:
                        Addthis(num.ToString(), "Sweep count", i[num-1].ToString(),table);
                        break;
                    case 6:
                        Addthis(num.ToString(), "Sweep Angle", i[num-1].ToString(),table);
                        break;
                    case 7:
                        Addthis(num.ToString(), "Material Remaining", i[num-1].ToString(),table);
                        break;
                    case 8:
                        Addthis(num.ToString(), "Distance from fire", i[num-1].ToString(),table);
                        break;
                    case 9:
                        Addthis(num.ToString(), "Time taken to put off the fire", i[num-1].ToString(),table);
                        break;
                    default:
                        break;
                }
            }

            document.Add(table);
        }

        void Addthis(string v1, string v2, string v3, PdfPTable table)
        {
            string[] s = { v1, v2, v3 };
            for(int i = 0; i < 3; i++)
            {
                PdfPCell k = new PdfPCell(new Phrase(s[i]));
                k.FixedHeight = 20;
                k.HorizontalAlignment = Element.ALIGN_CENTER;
                k.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(k);
            }
        }

        void tableHeaders(PdfPTable table)
        {
            string[] l = { "S.NO", "Metrics Measured", "Performance"};
            for (int j = 0; j < 3; j++)
            {
                PdfPCell k = new PdfPCell(new Phrase(l[j]));
                k.FixedHeight = 50;
                k.HorizontalAlignment = Element.ALIGN_CENTER;
                k.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(k);
            }
        }

        void addNewPage(Document document)
        {
            if (check > 1)
            {
                document.NewPage();
                check = 0;
            }
        }

        }
    }
    IEnumerator  PrintFiles()
    {
        Debug.Log(path);
        if (path == null)
            yield return null;

        if (File.Exists(path))
        {
            Debug.Log("file found");
        }
        else
        {
            Debug.Log("file not found");
            yield return null;
        }

        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        process.StartInfo.UseShellExecute = true;
        process.StartInfo.FileName = path;
        ReportGenerationScript.instance.index++;
        //process.StartInfo.Verb = "print";


        //process.Start();
        //process.WaitForExit();
       
    }

}
