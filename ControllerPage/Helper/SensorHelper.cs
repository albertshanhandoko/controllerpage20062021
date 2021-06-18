using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;

using ControllerPage.Library;
using System.IO;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel; // CancelEventArgs
using System.Configuration;

namespace ControllerPage.Helper
{
    public static class DashboardHelper
    {

        /*
        public Generate_PDF_Helper(string label, string printdate, string printtime, string average
                , string numberofmeasure, string printedby, List<Data_Measure> datas)
        {
            var dateAndTime = DateTime.Now;
            var date = dateAndTime.Date;
            //string pdfname = DateTime.Now.ToString().Trim();

            string trimmedlabel = String.Concat(label.Where(c => !Char.IsWhiteSpace(c)));
            string UrlPDF = "D:/Sensor_data/Print_Result_Sensor1/" + "sensor1" + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss").Trim() + ".pdf";
            FileStream fs = new FileStream(UrlPDF, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document(PageSize.A4, 10, 10, 30, 30);// left,right,top, bottom
            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            BaseFont Calibri = BaseFont.CreateFont("c:\\windows\\fonts\\Calibri.TTF", BaseFont.WINANSI, BaseFont.EMBEDDED);
            BaseFont Calibri_Bold = BaseFont.CreateFont(@"C:\Windows\Fonts\Calibrib.ttf", "Identity-H", BaseFont.EMBEDDED);
            BaseFont Arial = BaseFont.CreateFont("c:\\windows\\fonts\\Arial.TTF", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            BaseFont Arial_Bold = BaseFont.CreateFont(@"C:\Windows\Fonts\arialbd.ttf", "Identity-H", BaseFont.EMBEDDED);

            //cb.setColorFill(new BaseColor(255, 200, 200));
            //cb.setFontAndSize(bf, tSize);
            document.Open();
            PdfContentByte cb = writer.DirectContent;

            string source_dir = "D:/Job/Bebeb/SensorReader/WpfApp1/Resources/Logo_Chua.jpg";
            System.Windows.Controls.Image finalImage = new System.Windows.Controls.Image();
            finalImage.Source = new BitmapImage(new Uri(source_dir));
            iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(source_dir);

            #region page 1
            // Document Title ---------------------------
            var appcompname = ConfigurationManager.AppSettings;
            string conf_companyname = appcompname["companyname"] ?? "Not Found";
            var appcompaddress = ConfigurationManager.AppSettings;
            string conf_companyaddr = appcompaddress["companyaddress"] ?? "Not Found";
            //string pdf_companyname = "Company Name: " + conf_companyname;
            //string pdf_companyaddress = "Company Address: " + conf_companyaddr;


            png.ScaleAbsolute(70, 70);
            png.SetAbsolutePosition(466, 750);// x,y
            cb.AddImage(png);

            int left_margin = 40;
            int top_margin = 800;
            string sales = "sales@globalinstrumentsg.com";
            string GSTReg_No = "M2 - 8910040 - 7";
            string UEN_No = "199308400D";
            string telepon = "+65 62533538";
            string Fax_no = "+65 62533885";


            //cb.SetLineDash()
            // Draw A line for doc title
            cb.SetLineWidth(0f);
            cb.MoveTo(40, 710);
            cb.LineTo(560, 710);
            cb.Stroke();
            cb.SetColorFill(BaseColor.LIGHT_GRAY);
            //cb.SaveState();
            cb.BeginText();

            writeText_Helper(cb, conf_companyname, left_margin, top_margin, Arial_Bold, 14);
            writeText_Helper(cb, conf_companyaddr, left_margin, top_margin - 30, Calibri, 12);
            writeText_Helper(cb, sales, left_margin, top_margin - 45, Calibri, 12);

            writeText_Helper(cb, GSTReg_No, left_margin, top_margin - 63, Calibri, 12);
            writeText_Helper(cb, UEN_No, left_margin + 100, top_margin - 63, Calibri, 12);

            writeText_Helper(cb, telepon, left_margin, top_margin - 81, Calibri, 12);
            writeText_Helper(cb, Fax_no, left_margin + 100, top_margin - 81, Calibri, 12);

            cb.EndText();
            //cb.RestoreState();



            // Document Header ---------------------------
            string Header_Title1 = "Incoming Grain Analysis Report";
            string Header_Title2 = "(Sensor 1)";

            string pdf_label = "Label: " + label;
            string pdf_date = "Date: " + printdate;
            string pdf_time = "Time: " + printtime;
            string pdf_average = "Average: " + average;
            string pdf_numbermeasure = "No. of Measure: " + numberofmeasure;
            string pdf_printedby = "Prepared By: " + printedby;

            left_margin = 40;
            top_margin = 650;

            cb.SetColorFill(BaseColor.BLACK);
            //cb.SetLineDash(5);
            //cb.SaveState();
            cb.BeginText();

            // Title 
            //private PdfContentByte pdfContentByte;
            //string DescriptionToPrint = "Hii!! I will be underlined.";
            /*
            Int32 AlignmentofDescription = 3;
            float XofDescription = 110;
            float YofDescription = 440;
            float RotationofDescription = 0;
            iTextSharp.text.Font verdana = FontFactory.GetFont("Arial",16,3,BaseColor.BLACK);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT
                , new Phrase(new Chunk(Header_Title1, 
                FontFactory.GetFont(, 8, iTextSharp.text.Font.UNDERLINE)))

                , left_margin, top_margin, RotationofDescription);
            
            ColumnText.ShowTextAligned(pdfContentByte
            , Element.ALIGN_LEFT
                , new Phrase(new Chunk(DescriptionToPrint.ToString()
            , FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.UNDERLINE)))
                , XofDescription, YofDescription, RotationofDescription);
            

            writeText_Helper(cb, Header_Title1, left_margin + 120, top_margin, Calibri_Bold, 16);
            writeText_Helper(cb, Header_Title2, left_margin + 200, top_margin - 30, Calibri_Bold, 16);

            // Label
            writeText_Helper(cb, "Label: ", left_margin, top_margin - 70, Calibri, 12);
            writeText_Helper(cb, label, left_margin + 90, top_margin - 70, Calibri, 12);

            // Date
            writeText_Helper(cb, "Printed Date: ", left_margin, top_margin - 85, Calibri, 12);
            writeText_Helper(cb, printdate, left_margin + 90, top_margin - 85, Calibri, 12);

            // Time
            writeText_Helper(cb, "Printed Time: ", left_margin, top_margin - 100, Calibri, 12);
            writeText_Helper(cb, printtime, left_margin + 90, top_margin - 100, Calibri, 12);

            // Average
            writeText_Helper(cb, "Average: ", left_margin + 300, top_margin - 70, Calibri, 12);
            writeText_Helper(cb, average, left_margin + 400, top_margin - 70, Calibri, 12);

            // Number of Measurement
            writeText_Helper(cb, "No. of Measure: ", left_margin + 300, top_margin - 85, Calibri, 12);
            writeText_Helper(cb, numberofmeasure, left_margin + 400, top_margin - 85, Calibri, 12);

            // Printed By
            writeText_Helper(cb, "Printed By: ", left_margin + 300, top_margin - 100, Calibri, 12);
            writeText_Helper(cb, printedby, left_margin + 400, top_margin - 100, Calibri, 12);


            //writeText_Helper(cb, pdf_companyaddress, left_margin, top_margin - 48, Header, 12);

            cb.EndText();


            // Document Body ---------------------------

            string moisture_content = "Moisture Content (%)";

            // Column Name

            top_margin = 505;

            cb.BeginText();
            writeText_Helper(cb, moisture_content, left_margin, top_margin, Arial_Bold, 18);
            writeText_Helper(cb, "ID", left_margin, top_margin - 30, Arial_Bold, 14);
            writeText_Helper(cb, "Description", left_margin + 70, top_margin - 30, Arial_Bold, 14);
            writeText_Helper(cb, "Created Time", left_margin + 200, top_margin - 30, Arial_Bold, 14);
            cb.EndText();

            // Draw A line for header data_measure
            cb.SetLineWidth(0f);
            cb.MoveTo(40, 470);
            cb.LineTo(560, 470);
            cb.Stroke();


            // Measure_Data
            cb.BeginText();

            left_margin = 40;
            top_margin = 450;

            foreach (Data_Measure data in datas)
            {
                writeText_Helper(cb, data.Id.ToString(), left_margin, top_margin, Calibri, 12);
                writeText_Helper(cb, data.Measures, left_margin + 70, top_margin, Calibri, 12);
                writeText_Helper(cb, data.Created_date.ToString(), left_margin + 200, top_margin, Calibri, 12);
                top_margin -= 20;
            }
            // 465 - 20*10 = 265

            top_margin = 235;
            left_margin = 40;
            string empty_kernels = "Empty Kernels (%)";
            string Grain_Yield = "Grain Yield (%)";

            string val_empty_kernels = "45";
            string val_Grain_Yield = "35";

            writeText_Helper(cb, empty_kernels, left_margin, top_margin, Calibri_Bold, 14);
            writeText_Helper(cb, Grain_Yield, left_margin + 250, top_margin, Calibri_Bold, 14);

            cb.EndText();
            //cb.MoveTo(40, 215);
            // buat rectangle
            //var rect_kernel = new iTextSharp.text.Rectangle(30, 190, 120, 200);
            //rect_kernel.BorderWidth = 2;
            cb.SetLineWidth(1);
            cb.Rectangle(35, 205, 120, 18);
            cb.Stroke();

            cb.SetLineWidth(1);
            cb.Rectangle(280, 205, 120, 18);
            cb.Stroke();
            // Document Footer ---------------------------
            top_margin = 165;

            string qcpersonel = "QC Personnel";
            string approvedby = "Approved By: ";

            string label_name = "Name";
            string label_signature = "Signature";

            cb.BeginText();

            writeText_Helper(cb, qcpersonel, left_margin, top_margin, Calibri_Bold, 12);
            writeText_Helper(cb, approvedby, left_margin, top_margin - 30, Calibri, 12);
            writeText_Helper(cb, label_name, left_margin, top_margin - 100, Calibri, 12);
            writeText_Helper(cb, label_signature, left_margin + 380, top_margin - 100, Calibri, 12);

            cb.EndText();

            // Draw A line for Average data_measure
            cb.SetLineWidth(0f);
            cb.MoveTo(40, 282);
            cb.LineTo(560, 282);
            cb.Stroke();


            // Draw A line for name
            cb.SetLineWidth(0f);
            cb.MoveTo(40, 75);
            cb.LineTo(100, 75);
            cb.Stroke();

            // Draw A line for Signature
            cb.SetLineWidth(0f);
            cb.MoveTo(420, 75);
            cb.LineTo(480, 75);
            cb.Stroke();
            //cb.RestoreState();
            #endregion Page 1
            // Page 2 --------------------------------------------------
            document.NewPage();
            #region Page2
            // Document Title ---------------------------
            //appcompname = ConfigurationManager.AppSettings;
            //string conf_companyname = appcompname["companyname"] ?? "Not Found";
            //var appcompaddress = ConfigurationManager.AppSettings;
            //string conf_companyaddr = appcompaddress["companyaddress"] ?? "Not Found";
            //string pdf_companyname = "Company Name: " + conf_companyname;
            //string pdf_companyaddress = "Company Address: " + conf_companyaddr;


            png.ScaleAbsolute(70, 70);
            png.SetAbsolutePosition(466, 750);// x,y
            cb.AddImage(png);

            left_margin = 40;
            top_margin = 800;
            sales = "sales@globalinstrumentsg.com";
            GSTReg_No = "M2 - 8910040 - 7";
            UEN_No = "199308400D";
            telepon = "+65 62533538";
            Fax_no = "+65 62533885";


            //cb.SetLineDash()
            // Draw A line for doc title
            cb.SetLineWidth(0f);
            cb.MoveTo(40, 710);
            cb.LineTo(560, 710);
            cb.Stroke();
            cb.SetColorFill(BaseColor.LIGHT_GRAY);
            //cb.SaveState();
            cb.BeginText();

            writeText_Helper(cb, conf_companyname, left_margin, top_margin, Arial_Bold, 14);
            writeText_Helper(cb, conf_companyaddr, left_margin, top_margin - 30, Calibri, 12);
            writeText_Helper(cb, sales, left_margin, top_margin - 45, Calibri, 12);

            writeText_Helper(cb, GSTReg_No, left_margin, top_margin - 63, Calibri, 12);
            writeText_Helper(cb, UEN_No, left_margin + 100, top_margin - 63, Calibri, 12);

            writeText_Helper(cb, telepon, left_margin, top_margin - 81, Calibri, 12);
            writeText_Helper(cb, Fax_no, left_margin + 100, top_margin - 81, Calibri, 12);

            cb.EndText();
            //cb.RestoreState();



            // Document Header ---------------------------
            Header_Title1 = "Dari Analisa Biji Padi Kering Panen yang Masuk";
            Header_Title2 = "(Sensor 1)";

            pdf_label = "Nama Supplier: " + label;
            pdf_date = "Tanggal Ukur: " + printdate;
            pdf_time = "Waktu Ukur: " + printtime;
            pdf_average = "Rata-Rata: " + average;
            pdf_numbermeasure = "Jumlah Pengukuran: " + numberofmeasure;
            pdf_printedby = "Diukur Oleh: " + printedby;

            left_margin = 40;
            top_margin = 650;

            cb.SetColorFill(BaseColor.BLACK);
            //cb.SetLineDash(5);
            //cb.SaveState();
            cb.BeginText();

            // Title 
            //private PdfContentByte pdfContentByte;
            //string DescriptionToPrint = "Hii!! I will be underlined.";
            /*
            Int32 AlignmentofDescription = 3;
            float XofDescription = 110;
            float YofDescription = 440;
            float RotationofDescription = 0;
            iTextSharp.text.Font verdana = FontFactory.GetFont("Arial",16,3,BaseColor.BLACK);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT
                , new Phrase(new Chunk(Header_Title1, 
                FontFactory.GetFont(, 8, iTextSharp.text.Font.UNDERLINE)))

                , left_margin, top_margin, RotationofDescription);
            /*
            ColumnText.ShowTextAligned(pdfContentByte
            , Element.ALIGN_LEFT
                , new Phrase(new Chunk(DescriptionToPrint.ToString()
            , FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.UNDERLINE)))
                , XofDescription, YofDescription, RotationofDescription);
            

            writeText_Helper(cb, Header_Title1, left_margin + 120, top_margin, Calibri_Bold, 16);
            writeText_Helper(cb, Header_Title2, left_margin + 200, top_margin - 30, Calibri_Bold, 16);

            // Label
            writeText_Helper(cb, "Label: ", left_margin, top_margin - 70, Calibri, 12);
            writeText_Helper(cb, label, left_margin + 90, top_margin - 70, Calibri, 12);

            // Date
            writeText_Helper(cb, "Printed Date: ", left_margin, top_margin - 85, Calibri, 12);
            writeText_Helper(cb, printdate, left_margin + 90, top_margin - 85, Calibri, 12);

            // Time
            writeText_Helper(cb, "Printed Time: ", left_margin, top_margin - 100, Calibri, 12);
            writeText_Helper(cb, printtime, left_margin + 90, top_margin - 100, Calibri, 12);

            // Average
            writeText_Helper(cb, "Average: ", left_margin + 300, top_margin - 70, Calibri, 12);
            writeText_Helper(cb, average, left_margin + 400, top_margin - 70, Calibri, 12);

            // Number of Measurement
            writeText_Helper(cb, "No. of Measure: ", left_margin + 300, top_margin - 85, Calibri, 12);
            writeText_Helper(cb, numberofmeasure, left_margin + 400, top_margin - 85, Calibri, 12);

            // Printed By
            writeText_Helper(cb, "Printed By: ", left_margin + 300, top_margin - 100, Calibri, 12);
            writeText_Helper(cb, printedby, left_margin + 400, top_margin - 100, Calibri, 12);


            //writeText_Helper(cb, pdf_companyaddress, left_margin, top_margin - 48, Header, 12);

            cb.EndText();


            // Document Body ---------------------------

            moisture_content = "Kadar Air (%)";

            // Column Name

            top_margin = 505;

            cb.BeginText();
            writeText_Helper(cb, moisture_content, left_margin, top_margin, Arial_Bold, 18);
            writeText_Helper(cb, "ID", left_margin, top_margin - 30, Arial_Bold, 14);
            writeText_Helper(cb, "Description", left_margin + 70, top_margin - 30, Arial_Bold, 14);
            writeText_Helper(cb, "Created Time", left_margin + 200, top_margin - 30, Arial_Bold, 14);
            cb.EndText();

            // Draw A line for header data_measure
            cb.SetLineWidth(0f);
            cb.MoveTo(40, 470);
            cb.LineTo(560, 470);
            cb.Stroke();


            // Measure_Data
            cb.BeginText();

            left_margin = 40;
            top_margin = 450;

            foreach (Data_Measure data in datas)
            {
                writeText_Helper(cb, data.Id.ToString(), left_margin, top_margin, Calibri, 12);
                writeText_Helper(cb, data.Measures, left_margin + 70, top_margin, Calibri, 12);
                writeText_Helper(cb, data.Created_date.ToString(), left_margin + 200, top_margin, Calibri, 12);
                top_margin -= 20;
            }
            // 465 - 20*10 = 265

            top_margin = 235;
            left_margin = 40;
            empty_kernels = "Butir Hampa (%)";
            Grain_Yield = "Hasil Biji (%)";

            //string val_empty_kernels = "45";
            //string val_Grain_Yield = "35";

            writeText_Helper(cb, empty_kernels, left_margin, top_margin, Calibri_Bold, 14);
            writeText_Helper(cb, Grain_Yield, left_margin + 250, top_margin, Calibri_Bold, 14);

            cb.EndText();
            //cb.MoveTo(40, 215);
            // buat rectangle
            //var rect_kernel = new iTextSharp.text.Rectangle(30, 190, 120, 200);
            //rect_kernel.BorderWidth = 2;
            cb.SetLineWidth(1);
            cb.Rectangle(35, 205, 120, 18);
            cb.Stroke();

            cb.SetLineWidth(1);
            cb.Rectangle(280, 205, 120, 18);
            cb.Stroke();
            // Document Footer ---------------------------
            top_margin = 165;

            qcpersonel = "QC Pemeriksa";
            approvedby = "Disetujui Oleh: ";

            label_name = "Nama";
            label_signature = "Tanda Tangan";

            cb.BeginText();

            writeText_Helper(cb, qcpersonel, left_margin, top_margin, Calibri_Bold, 12);
            writeText_Helper(cb, approvedby, left_margin, top_margin - 30, Calibri, 12);
            writeText_Helper(cb, label_name, left_margin, top_margin - 100, Calibri, 12);
            writeText_Helper(cb, label_signature, left_margin + 380, top_margin - 100, Calibri, 12);

            cb.EndText();

            // Draw A line for Average data_measure
            cb.SetLineWidth(0f);
            cb.MoveTo(40, 282);
            cb.LineTo(560, 282);
            cb.Stroke();


            // Draw A line for name
            cb.SetLineWidth(0f);
            cb.MoveTo(40, 75);
            cb.LineTo(100, 75);
            cb.Stroke();

            // Draw A line for Signature
            cb.SetLineWidth(0f);
            cb.MoveTo(420, 75);
            cb.LineTo(480, 75);
            cb.Stroke();
            #endregion Page2 end
            // save to text file History ------------------------------------
            string month = DateTime.Now.ToString("yyyyMM");
            string urlHistory_data = "D:/Sensor_data/History_data_Sensor1/" + month.ToString().Trim() + ".txt";

            if (File.Exists(urlHistory_data))
            {
                // write to file
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(urlHistory_data, true))
                {
                    file.WriteLine(Environment.NewLine);
                    file.WriteLine(UrlPDF);
                }
            }
            else
            {
                // Create new file
                System.IO.File.WriteAllText(urlHistory_data, UrlPDF);
            }

            document.Close();
            writer.Close();
            fs.Close();

            return 1;
        }
        */

    }

}
