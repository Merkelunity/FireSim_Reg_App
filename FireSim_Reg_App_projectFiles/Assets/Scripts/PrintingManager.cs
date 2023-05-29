using UnityEngine;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class PrintingManager : MonoBehaviour
{
    public static PrintingManager instance;
    string path = null;

    //public string logoPath1;
    //public string logoPath2;

    public float scaleX;
    public float scaleY;

    public float posX;
    public float posY;

    public float spacingBefore;

    public float spaceAfter;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        path = Application.dataPath + "/FireSim_Report.pdf";
        //GenerateFile();
    }

    public void GenerateFile() 
    {
        if (File.Exists(path))
            File.Delete(path);

        //Add border to page


        Paragraph spacing = new Paragraph("\n");

        //addImageFromThisPath("D:\\Dhinesh\\Firesim_Login_project\\Assets\\UI\\firesimUI.png");
        using (var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
        {
            var document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            var writer = PdfWriter.GetInstance(document, fileStream);

            document.Open();

            document.NewPage();


            #region border

            PdfContentByte content = writer.DirectContent;
            Rectangle rectangle = new Rectangle(document.PageSize);
            rectangle.Left += document.LeftMargin;
            rectangle.Right -= document.RightMargin;
            rectangle.Top -= document.TopMargin;
            rectangle.Bottom += document.BottomMargin;
            content.SetColorStroke(BaseColor.BLACK);
            content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
            content.Stroke();

            #endregion

            #region logos

            //Image jpg = Image.GetInstance("D:\\Dhinesh\\Firesim_Login_project\\Assets\\UI\\firesimUI.png");
            //jpg.ScaleToFit(140, 120);
            //jpg.SetAbsolutePosition(100, 100);
            //jpg.SpacingBefore = 10;
            //jpg.SpacingAfter = 1;
            //jpg.Alignment = Element.ALIGN_LEFT;
            //document.Add(jpg);

            Image jpg1 = Image.GetInstance("D:\\Dhinesh\\Firesim_Login_project\\Assets\\UI\\firesimUI.png");
            jpg1.ScaleToFit(scaleX, scaleY);
            jpg1.SetAbsolutePosition(posX, posY);
            jpg1.SpacingBefore = spacingBefore;
            jpg1.SpacingAfter = spaceAfter;
            jpg1.Alignment = Element.ALIGN_CENTER;
            document.Add(jpg1);

            Image jpg2 = Image.GetInstance("D:\\Dhinesh\\Firesim_Login_project\\Assets\\UI\\firesimUI.png");
            jpg2.ScaleToFit(80 , 80);
            jpg2.SetAbsolutePosition(490, 750);
            jpg2.SpacingBefore = 0;
            jpg2.SpacingAfter = 0;
            //jpg2.Alignment = Element.ALIGN_LEFT;
            document.Add(jpg2);

            #endregion

            #region testZone



            #endregion


            #region header

            //header
            var Fontcolor = FontFactory.GetFont("TIMES_ROMAN", 18, iTextSharp.text.Font.BOLD);
            document.Add(spacing);
            Paragraph p = new Paragraph(string.Format("Fire Safety Training Program \n Performance Report"), Fontcolor);
            p.Alignment = Element.ALIGN_CENTER;
            document.Add(p);

            var Fontcolor1 = FontFactory.GetFont("TIMES_ROMAN", 14, BaseColor.BLACK);
            document.Add(spacing);
            document.Add(spacing);
            Paragraph c = new Paragraph(string.Format("This is to certify that the following information ______________________________________________________________ "), Fontcolor1); 
            c.Alignment = Element.ALIGN_CENTER;
            document.Add(c);

            #endregion

            #region body


            #region personal details

            Paragraph name = new Paragraph(" Name of the participant : " + ReportGenerationScript.instance.empNameVal.text);
            name.Alignment = 0;
            document.Add(name);

            Paragraph id = new Paragraph(" UID : " + ReportGenerationScript.instance.empIDVal.text);
            id.Alignment = Element.ALIGN_LEFT;
            document.Add(id);


            Paragraph orgName = new Paragraph(" Company Name : " + ReportGenerationScript.instance.empOrgNameVal.text);
            orgName.Alignment = Element.ALIGN_LEFT;
            document.Add(orgName);

            #endregion

            Paragraph date = new Paragraph(" Date : " + System.DateTime.UtcNow.ToString("dd-MM-yyyy") );
            date.Alignment = Element.ALIGN_LEFT;
            document.Add(date);

            //Paragraph lines = new Paragraph("------------------------------------------------------------------------------------------------------------------------------------");
            //lines.Alignment = Element.ALIGN_CENTER;
            //document.Add(lines);

            document.Add(spacing);
            PdfPTable table = new PdfPTable(3);

            #region tableContent
            var tableStyling = FontFactory.GetFont("TIMES_ROMAN", 14, iTextSharp.text.Font.NORMAL);

            Paragraph scenario = new Paragraph(string.Format("Scenario"), tableStyling);
            scenario.Alignment = Element.ALIGN_CENTER;

            Paragraph maxScore = new Paragraph(string.Format("Max Score"), tableStyling);
            maxScore.Alignment = Element.ALIGN_CENTER;

            Paragraph obtainedScore = new Paragraph(string.Format("Obtained Score"), tableStyling);
            obtainedScore.Alignment = Element.ALIGN_CENTER;

            #endregion

            table.AddCell(scenario);
            table.AddCell(maxScore);
            table.AddCell(obtainedScore);

            #region score


            if (ReportGenerationScript.instance.officePaperVal.text == "Not Completed") { }
            else if (ReportGenerationScript.instance.officePaperVal != null)
            {
                Paragraph officePaper = new Paragraph(" Class A - Office - Combustible - paper ");
                officePaper.Alignment = Element.ALIGN_JUSTIFIED;

                addThisToTable(officePaper,ReportGenerationScript.instance.officePaperVal.text);

            }

            if (ReportGenerationScript.instance.officeSofaVal.text == "Not Completed") { }
            else if (ReportGenerationScript.instance.officeSofaVal != null)
            {
                Paragraph officeSofa = new Paragraph(" Class A - Office - Combustible - sofa ");
                officeSofa.Alignment = Element.ALIGN_JUSTIFIED;

                addThisToTable(officeSofa, ReportGenerationScript.instance.officeSofaVal.text);
            }

            if (ReportGenerationScript.instance.WarehouseWoodVal.text == "Not Completed") { }
            else if (ReportGenerationScript.instance.WarehouseWoodVal != null)
            {
                Paragraph warehouseWood = new Paragraph(" Class A - Warehouse - Wood " );
                warehouseWood.Alignment = Element.ALIGN_JUSTIFIED;

                addThisToTable(warehouseWood, ReportGenerationScript.instance.WarehouseWoodVal.text);
            }

            if (ReportGenerationScript.instance.WarehouseOilVal.text == "Not Completed") { }
            else if (ReportGenerationScript.instance.WarehouseOilVal != null)
            {
                Paragraph warehouseOil = new Paragraph(" Class B - Warehouse - Oil " );
                warehouseOil.Alignment = Element.ALIGN_JUSTIFIED;

                addThisToTable(warehouseOil, ReportGenerationScript.instance.WarehouseOilVal.text);
            }

            if (ReportGenerationScript.instance.officeElectricVal.text == "Not Completed") { }
            else if (ReportGenerationScript.instance.officeElectricVal != null)
            {
                Paragraph officeElectric = new Paragraph(" Class C - Office - electric " );
                officeElectric.Alignment = Element.ALIGN_JUSTIFIED;

                addThisToTable(officeElectric, ReportGenerationScript.instance.officeElectricVal.text);
            }

            if (ReportGenerationScript.instance.WarehouseElectricVal.text == "Not Completed") { }
            else if (ReportGenerationScript.instance.WarehouseElectricVal != null)
            {
                Paragraph warehouseElectric = new Paragraph(" Class C - Warehouse - Electric ");
                warehouseElectric.Alignment = Element.ALIGN_JUSTIFIED;

                addThisToTable(warehouseElectric, ReportGenerationScript.instance.WarehouseElectricVal.text);
            }

            if (ReportGenerationScript.instance.labFireVal.text == "Not Completed") { }
            else if (ReportGenerationScript.instance.labFireVal != null)
            {
                Paragraph labMetal = new Paragraph(" Class D - Laboratory - Metal " );
                labMetal.Alignment = Element.ALIGN_JUSTIFIED;

                addThisToTable(labMetal, ReportGenerationScript.instance.labFireVal.text);
            }

            if (ReportGenerationScript.instance.IndustrialKitchenVal.text == "Not Completed") { }
            else if (ReportGenerationScript.instance.IndustrialKitchenVal != null)
            {
                Paragraph kitchenOil = new Paragraph(" Class K - Kitchen - Oil " );
                kitchenOil.Alignment = Element.ALIGN_JUSTIFIED;

                addThisToTable(kitchenOil, ReportGenerationScript.instance.IndustrialKitchenVal.text);
            }

            void addThisToTable(Paragraph p , string obtScore) 
            {
                table.AddCell(p);
                table.AddCell(100.ToString());
                table.AddCell(obtScore);
            }

            #endregion

            document.Add(table);

            document.Add(spacing);
            document.Add(spacing);

            #endregion

            #region footer

            //footer
            Paragraph issueDate = new Paragraph(" Issue Date : ");
            issueDate.Alignment = Element.ALIGN_LEFT;
            document.Add(issueDate);

            document.Add(spacing);
             
            Paragraph expiryDate = new Paragraph(" Expiry Date : ");
            expiryDate.Alignment = Element.ALIGN_LEFT;
            document.Add(expiryDate);

            document.Add(spacing);

            Paragraph qrCode= new Paragraph(" QR Code for detailed parameters measured ");
            qrCode.Alignment = Element.ALIGN_LEFT;
            document.Add(qrCode);

            document.Add(spacing);
            document.Add(spacing);
            document.Add(spacing);

            Paragraph signature = new Paragraph(" SIGNATURE");
            signature.Alignment = Element.ALIGN_LEFT;
            document.Add(signature);

            #endregion

            document.Close();
            writer.Close();
        }

        PrintFiles();
    }

    void PrintFiles()
    {
        Debug.Log(path);
        if (path == null)
            return;

        if (File.Exists(path))
        {
            Debug.Log("file found");
        }
        else
        {
            Debug.Log("file not found");
            return;
        }

        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        process.StartInfo.UseShellExecute = true;
        process.StartInfo.FileName = path;
        //process.StartInfo.Verb = "print";

        process.Start();
        //process.WaitForExit();

    }
}
