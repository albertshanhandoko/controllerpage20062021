using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using System.Windows.Input;
using ControllerPage.Library;
using System.IO;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
//using System.Windows.Threading;
using System.Diagnostics;
using System.ComponentModel; // CancelEventArgs
using System.Configuration;
using ControllerPage.Helper;
using System.Text.RegularExpressions;
//using Spire.Xls;
//using Spire.Xls.Charts;
using Color = System.Drawing.Color;
using System.Windows.Forms;

namespace ControllerPage.Helper
{
    class SensorHelper_2
    {
        
        public static int test_int(int test)
        {
            if (test > 0)
            {
                return test + 10;
            }
            else
            {
                return -10;
            }
        }
        
        public static List<Data_History> Read_PDF_History_old(string SensorName)
        {
            string month = DateTime.Now.ToString("yyyyMM");
            string urlHistory_data = "D:/Sensor_data/History_data/" + SensorName + month.ToString().Trim() + ".txt";
            List<Data_History> data_histories = new List<Data_History> { };

            if (File.Exists(urlHistory_data))
            {
                string[] lines = File.ReadAllLines(urlHistory_data);
                int i = 1;
                foreach (string line in lines)
                {
                    Data_History data_history = new Data_History(i, "", "", DateTime.Now, "Sensor2_1Jan2020_T130000.pdf");
                    data_histories.Add(data_history);
                    i++;
                }
            }
            return data_histories;
        }

        public static List<Data_Measure> Test_DataMeasure()
        {
            List<Data_Measure> data_Measures = new List<Data_Measure>
            {
                //new Data_Measure {Id = 1, Measures = 241, Created_date = DateTime.Now},
                new Data_Measure (1, "241", DateTime.Now),
                new Data_Measure (2, "242", DateTime.Now),
                new Data_Measure (3, "243", DateTime.Now),
                new Data_Measure (4, "244", DateTime.Now),
                new Data_Measure (5, "245", DateTime.Now),
                new Data_Measure (6, "246", DateTime.Now),
                new Data_Measure (7, "247", DateTime.Now),
                new Data_Measure (8, "248", DateTime.Now),
                new Data_Measure (9, "249", DateTime.Now),
                //new Data_Measure (0, "245", DateTime.Now)
            };
            return data_Measures;
        }

        public static List<data_measure_2> Test_DataMeasure_2()
        {
            List<data_measure_2> data_Measures = new List<data_measure_2>
            {
                //new Data_Measure {Id = 1, Measures = 241, Created_date = DateTime.Now},
                new data_measure_2 (1, "241", (DateTime.Now).ToString()),
                new data_measure_2 (2, "242", (DateTime.Now).ToString()),
                new data_measure_2 (3, "243", (DateTime.Now).ToString()),
                new data_measure_2 (4, "244", (DateTime.Now).ToString()),
                new data_measure_2 (5, "245", (DateTime.Now).ToString()),
                new data_measure_2 (6, "246", (DateTime.Now).ToString()),
                new data_measure_2 (7, "247", (DateTime.Now).ToString()),
                new data_measure_2 (8, "248", (DateTime.Now).ToString()),
                new data_measure_2 (9, "249", (DateTime.Now).ToString()),
                //new Data_Measure (0, "245", DateTime.Now)
            };
            return data_Measures;
        }

        
        

        public static void OpenCon_Port(SerialPort mySerialPort, int BaudRate)
        {
            //SerialPort SerialPort = new SerialPort(PortName);
            mySerialPort.BaudRate = BaudRate;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.RtsEnable = true;
            mySerialPort.ReadBufferSize = 2000000;
            mySerialPort.Encoding = ASCIIEncoding.ASCII;
            //mySerialPort.DataReceived += new SerialDataReceivedEventHandler(ProcessSensorData);
            try
            {
                mySerialPort.Open();
            }
            catch (Exception error)//(Exception e)
            {
                
                MessageBox.Show("Port failed to be opened");
                Console.WriteLine(error.Message);
            }

        }

        public static void ExcelDocViewer(string fileName)
        {
            try
            {
                System.Diagnostics.Process.Start(fileName);
            }
            catch { }
        }


        public string ProcessSensorData(SerialPort mySerialPort,  object sender, SerialDataReceivedEventArgs args)
        {
            try
            {
                Thread.Sleep(3000);// this solves the problem
                byte[] readBuffer = new byte[mySerialPort.ReadBufferSize];
                int readLen = mySerialPort.Read(readBuffer, 0, readBuffer.Length);
                string readStr = string.Empty;

                readStr = readStr.Trim();
                string[] charactersToReplace = new string[] { @"\t", @"\n", @"\r", " ", "<CR>", "<LF>" };
                foreach (string s in charactersToReplace)
                {
                    readStr = readStr.Replace(s, "");
                }
                readStr = Regex.Replace(readStr, "[^0-9.]", "");
                readStr = String.Concat(readStr.Substring(0, readStr.Length - 1)
                    , ".", readStr.Substring(readStr.Length - 1, 1));
                //MyString.Substring(MyString.Length-6);
                return readStr;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Console.WriteLine(ex);
                return "";
            }
            
        }




    }
}
