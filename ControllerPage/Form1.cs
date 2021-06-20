using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControllerPage.Constant;
using ControllerPage.Helper;
using ControllerPage.Library;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net;
using System.Timers;

namespace ControllerPage
{
    public partial class Form1 : Form
    {
        //static string application_name = ConfigurationManager.AppSettings["application_name"] ?? "Not Found";

        // Parameter General Apliaksi
        SerialPort mySerialPort;
        //SerialPort mySerialPort = new SerialPort("COM1");
        static string application_name = "GX_MX_001_Controller";


        // Parameter Input
        int delay;
        int TotalInterval;
        int counter_data = 0;
        string ResultGrain;
        string ResultMeasure;
        bool temp_cond;
        //System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();
        System.Timers.Timer MyTimer = new System.Timers.Timer();

        int blink_timer;

        // Parameter Looping Sensor
        int current_interval;
        bool start_next_cond;
        bool aggregate_cond;
        bool stat_continue;
        List<data_measure_2> Data_Measure_Result = new List<data_measure_2> { };
        List<data_measure_2> Data_Avg_Result = new List<data_measure_2> { };
        data_measure_2 Data_Measure_Current;
        data_measure_2 Data_Avg_Current;
        int timer_counter = 0;
        float total_current_Average;
        float total_average;
        int finish_measurement = 0;

        //database parameter
        int batch_id;


        public Form1()
        {
            InitializeComponent();
            data_initiation_input();


            /*
            // Open Port

            string comport = Combobox_ComPort.Text;
            int BaudRate = 1200;
            mySerialPort = new SerialPort("COM1");
            mySerialPort.Close();
            SensorHelper_2.OpenCon_Port(mySerialPort, BaudRate);
            Thread.Sleep(30);
            */


        }
        public void button2_Click_2(object sender, EventArgs e)
        {
            // FormProductselection F2 = new FormProductselection();
            //  F2.ShowDialog();
            using (var form = new FormProductselection())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string val = form.Productselection;            //values preserved after close
                    //Do something here with these values
                    string display_val = val.Replace("_", " ");
                    ButtonProduct.Text = display_val;

                }
            }
        }
        public void button3_Click_1(object sender, EventArgs e)
        {

            using (var form = new FormNumberinterval())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ButtonNumInterval.Text = FormNumberinterval.combobox_selectedItem_number_Interval;
                }
            }
        }

        public void button4_Click(object sender, EventArgs e)
        {
            //FormNumberpcsinterval F2 = new FormNumberpcsinterval();
            //F2.ShowDialog();
            using (var form = new FormNumberpcsinterval())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ButtonNumPcs.Text = FormNumberpcsinterval.combobox_selectedItem_number_PerPCS;
                }
            }
        }

        public void button5_Click(object sender, EventArgs e)
        {
            //FormWaitinginterval F2 = new FormWaitinginterval();
            //F2.ShowDialog();
            using (var form = new FormWaitinginterval())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ButtonWaitingTime.Text = FormWaitinginterval.combobox_selectedItem_WaitingTime;

                }
            }
        }

        public void button6_Click(object sender, EventArgs e)
        {
            Form2 F2 = new Form2();
            F2.ShowDialog();
        }
        //

        private void data_cleansing()
        {
            // Parameter Di Layar
            ButtonProduct.Text = "";
            ButtonNumInterval.Text = "";
            ButtonNumPcs.Text = "";
            ButtonWaitingTime.Text = "";
            Curr_Interval_TextBox.Text = "";
            Curr_Kernel_TextBox.Text = "";
            Current_Avg_TextBox.Text = "";
            Curr_Measure_TextBox.Text = "";
            //Temp_TextBox.Text = "";

            // Parameter Input
            delay = 0;
            TotalInterval = 0;
            counter_data = 0;
            ResultGrain = null;
            ResultMeasure = null;
            temp_cond = false;
            //System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();
            System.Timers.Timer MyTimer = new System.Timers.Timer();

            blink_timer = 0;

            // Parameter Looping Sensor
            current_interval = 0;
            start_next_cond = false;
            aggregate_cond = false;
            stat_continue = false;
            Data_Measure_Result = new List<data_measure_2> { };
            Data_Avg_Result = new List<data_measure_2> { };
            Data_Measure_Current = null;
            Data_Avg_Current = null;
            timer_counter = 0;
            total_current_Average = 0;
            total_average = 0;
            finish_measurement = 0;

            //database parameter

        }
        private void data_initiation_input()
        {

            comboBox_IPAddress.Items.Clear();
            comboBox_IPAddress.Items.Add(Sensor_input_Helper.GetLocalIPAddress());

            Combobox_Mode.Items.Clear();
            Combobox_Mode.Items.Add("Interval");

            Combobox_ComPort.Items.Clear();
            Combobox_ComPort.Items.Add("RS-485");



        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {

            //MessageBox.Show("The form will now be closed.", "Time Elapsed");

            Random rd_timer = new Random();
            int rand_num_timer = rd_timer.Next(1, 9);
            if (blink_timer % 2 == 0)
            {
                Curr_Measure_TextBox.Invoke((Action)delegate
                {
                    Curr_Measure_TextBox.Text = "." + " ";
                });
            }
            else
            {
                Curr_Measure_TextBox.Invoke((Action)delegate
                {
                    Curr_Measure_TextBox.Text = "";
                });
            }
            blink_timer++;


        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private List<string> GetWords(string text)
        {
            Regex reg = new Regex("[a-zA-Z0-9]");
            string Word = "";
            char[] ca = text.ToCharArray();
            List<string> characters = new List<string>();
            for (int i = 0; i < ca.Length; i++)
            {
                char c = ca[i];
                if (c > 65535)
                {
                    continue;
                }
                if (char.IsHighSurrogate(c))
                {
                    i++;
                    characters.Add(new string(new[] { c, ca[i] }));
                }
                else
                {
                    if (reg.Match(c.ToString()).Success || c.ToString() == "/")
                    {
                        Word = Word + c.ToString();
                        //characters.Add(new string(new[] { c }));
                    }
                    else if (c.ToString() == " ")
                    {
                        if (Word.Length > 0)
                            characters.Add(Word);
                        Word = "";
                    }
                    else
                    {
                        if (Word.Length > 0)
                            characters.Add(Word);
                        Word = "";
                    }

                }

            }
            return characters;
        }



        // Start_Button = button3_click
        private void button3_Click(object sender, EventArgs e)
        {
            if (ButtonProduct.Text == "")
            {
                MessageBox.Show("PLease Enter Product", application_name);
            }
            else if (ButtonNumInterval.Text == "")
            {
                MessageBox.Show("PLease Enter Number Interval", application_name);
            }
            else if (ButtonNumPcs.Text == "")
            {
                MessageBox.Show("PLease Enter Number of Pieces", application_name);
            }
            else if (ButtonWaitingTime.Text == "")
            {
                MessageBox.Show("PLease Enter Waiting Time", application_name);
            }
            else
            {

                // get data from ComboBox
                // 0.5 min -> this is description. need to get value

                //string combox_timeinteval = Combobox_timeinterval.SelectedItem.ToString();
                var result = Sensor_input_Helper.GetEnumValueFromDescription<Time_Interval>(ButtonWaitingTime.Text);
                delay = ((int)(result)) * 60;

                // 1 -> use directly
                //TotalInterval = int.Parse(Combobox_NumInterval.SelectedItem.ToString());

                TotalInterval = int.Parse(ButtonNumInterval.Text.ToString());


                // 10 => this is int vlaue.get the description

                //string combox_numbergrain = Combobox_NumberGrain.SelectedItem.ToString();
                number_grain enum_numgrain = (number_grain)Enum.Parse(typeof(number_grain), ButtonNumPcs.Text);
                ResultGrain = Sensor_input_Helper.GetDescription(enum_numgrain);


                // Short paddy => this is value string. get the desc


                string combox_typemeasure = ButtonProduct.Text;
                combox_typemeasure = combox_typemeasure.Replace(" ", "_");
                TypeOfMeasure enum_typemeasure = (TypeOfMeasure)Enum.Parse(typeof(TypeOfMeasure), combox_typemeasure);

                ResultMeasure = Sensor_input_Helper.GetDescription(enum_typemeasure);

                //MyTimer.Start();
                if (Temp_TextBox.Text == "" || String.IsNullOrEmpty(Temp_TextBox.Text))
                {
                    Sensor_input_Helper.Command_CheckTemp(mySerialPort);
                    //string result_temp = "29";
                    string result_temp = CheckTemp();


                    while (Temp_TextBox.Text == "")
                    {
                        Console.WriteLine("");
                    }
                }
                else
                {
                    Console.WriteLine("textbox alread filled");
                }
                Thread.Sleep(500);
                timer_counter = 1;
                batch_id = Sensor_input_Helper.MySql_Insert_Batch(Sensor_input_Helper.GetLocalIPAddress()
                    , combox_typemeasure
                    , TotalInterval
                    , delay.ToString()
                    , Int32.Parse(ButtonNumPcs.Text)
                    , Temp_TextBox.Text) ;


                // Run Sensor
                Console.WriteLine(ResultGrain);
                Console.WriteLine(ResultMeasure);
                stat_continue = true;
                Sensor_input_Helper.Command_Stop(mySerialPort);
                Thread.Sleep(2500);
                Console.WriteLine("Stop");

                Sensor_input_Helper.Command_Write(mySerialPort, ResultGrain);
                Thread.Sleep(1000);
                Sensor_input_Helper.Command_Write(mySerialPort, ResultMeasure);

                Console.WriteLine("Start Sequence");
                current_interval = 0;
                Curr_Interval_TextBox.Text = (current_interval + 1).ToString();
                //MyTimer.Start();
                stat_continue = true;
                Thread readThread = new Thread(Read);
                readThread.Start();


            }
        }


        public void Read()
        {
            while (stat_continue)
            {
                try
                {
                    Thread.Sleep(1000);// this solves the problem
                    byte[] readBuffer = new byte[mySerialPort.ReadBufferSize];
                    int readLen = mySerialPort.Read(readBuffer, 0, readBuffer.Length);
                    string readStr = string.Empty;

                    readStr = Encoding.UTF8.GetString(readBuffer, 0, readLen);
                    readStr = readStr.Trim();
                    Console.WriteLine("ReadStr adalah: " + readStr);

                    char[] delimiter_r = { '\r' };
                    string[] Measures_With_U = readStr.Split(delimiter_r);
                    List<string> Measure_Results = new List<string>();
                    List<string> AllText = new List<string>();

                    foreach (var Measure in Measures_With_U)
                    {
                        string Result_Parsing = GetWords(Measure).FirstOrDefault();
                        string[] charactersToReplace = new string[] { @"\t", @"\n", @"\r", " ", "<CR>", "<LF>" };


                        // Data Cleansing
                        if (Result_Parsing != "" && Result_Parsing != null)
                        {
                            foreach (string s in charactersToReplace)
                            {
                                Result_Parsing = Result_Parsing.Replace(s, "");
                                Console.WriteLine(Result_Parsing, "test");


                            }
                        }
                        //Curr_Measure_TextBox.Text = Result_Parsing;
                        // Decide what to do with data
                        if (Result_Parsing != "" && Result_Parsing != null && !Result_Parsing.Trim().ToLower().Contains("r"))
                        {
                            if (timer_counter == 1)
                            {
                                MyTimer.Elapsed += new ElapsedEventHandler(MyTimer_Tick);
                                MyTimer.Interval = (1000); // 45 mins
                                MyTimer.Enabled = true;
                                MyTimer.Start();

                                timer_counter = 0;
                            }
                            aggregate_cond = true;
                            start_next_cond = true;
                            //countersleep = 0;
                            //start_next_init = 0;
                            //Console.WriteLine("Nilai Measure adalah: " + Result_Parsing);
                            Result_Parsing = String.Concat(Result_Parsing.Substring(0, Result_Parsing.Length - 1)
                                    , ".", Result_Parsing.Substring(Result_Parsing.Length - 1, 1));
                            counter_data = Data_Measure_Result.Count;
                            //data_measure_2 data_final_update = new data_measure_2(counter_data + 1, Result_Parsing, (DateTime.Now).ToString());
                            Data_Measure_Current = new data_measure_2(counter_data + 1, Result_Parsing, (DateTime.Now).ToString());
                            Data_Measure_Result.Add(Data_Measure_Current);

                            /*
                            Curr_Measure_TextBox.Invoke((Action)delegate
                            {
                                Curr_Measure_TextBox.Text = Result_Parsing;
                            });
                            */
                            Curr_Kernel_TextBox.Invoke((Action)delegate
                            {
                                Curr_Kernel_TextBox.Text = (counter_data + 1).ToString();
                            });

                            // insert to database
                            // Perbatchid = current_interval.
                            // isaverage = 0 -> in case not average
                            float Result_Parsing_input = float.Parse(Result_Parsing);
                            Sensor_input_Helper.MySql_Insert_Measure(batch_id, counter_data + 1, Result_Parsing_input, DateTime.Now, 0);


                        }

                        else if (Data_Measure_Result.Count % 10 == 0
                            && Data_Measure_Result.Count > 0
                            && aggregate_cond == true
                            && start_next_cond == true
                            )
                        {
                            Console.WriteLine("Start SendData with Data received");

                            #region Get Aggregate value

                            //start_next_init = 0;
                            //OpenCon_Port_local(mySerialPort, BaudRate);
                            while (aggregate_cond)
                            {

                                Sensor_input_Helper.Command_MoisturAggregate(mySerialPort);
                                Thread.Sleep(2000);// this solves the problem
                                Console.WriteLine("Next Iter");
                                Console.WriteLine("counter init is: ");
                                readBuffer = new byte[mySerialPort.ReadBufferSize];
                                readLen = mySerialPort.Read(readBuffer, 0, readBuffer.Length);
                                readStr = string.Empty;
                                readStr = Encoding.UTF8.GetString(readBuffer, 0, readLen);
                                readStr = readStr.Trim();

                                Console.WriteLine("ReadStr adalah: " + readStr);
                                charactersToReplace = new string[] { @"\t", @"\n", @"\r", " ", "<CR>", "<LF>", "R" };
                                foreach (string s in charactersToReplace)
                                {
                                    Result_Parsing = readStr.Replace(s, "");
                                }

                                //Result_Parsing = GetWords(Result_Parsing).FirstOrDefault();
                                if (Result_Parsing != null)
                                {
                                    if (Result_Parsing.Contains("-") || (Result_Parsing.Length) > 4)
                                    {
                                        MyTimer.Stop();

                                        AllText = GetWords(Result_Parsing);
                                        Result_Parsing = AllText[1].Substring(5, 3);
                                        Result_Parsing = String.Concat(Result_Parsing.Substring(0, Result_Parsing.Length - 1)
                                            , ".", Result_Parsing.Substring(Result_Parsing.Length - 1, 1));
                                        Data_Avg_Result.Add(new data_measure_2(100, Result_Parsing, (DateTime.Now).ToString()));
                                        aggregate_cond = false;


                                        Curr_Measure_TextBox.Invoke((Action)delegate
                                        {
                                            Curr_Measure_TextBox.Text = Result_Parsing;
                                            // Latest Average
                                        });
                                        total_average = 0;
                                        Current_Avg_TextBox.Invoke((Action)delegate
                                        {
                                            foreach (data_measure_2 average_val in Data_Avg_Result)
                                            {
                                                total_average = total_average + float.Parse(average_val.Measures);
                                            }

                                            total_current_Average = total_average / Data_Avg_Result.Count();
                                            Current_Avg_TextBox.Text = total_current_Average.ToString("0.0") + "%";
                                            //Final Average
                                        });
                                        //loat Result_Parsing_input = float.Parse(Result_Parsing);
                                        Sensor_input_Helper.MySql_Insert_Measure(batch_id, 1000 + current_interval + 1, float.Parse(Result_Parsing), DateTime.Now, 1);

                                        Console.WriteLine("Finish Aggregate");
                                    }
                                }
                                //start_next_init++;
                            }

                            #endregion Finish get aggregate value
                            Console.WriteLine("Finish aggregate region");

                            #region Finish All Measure and close port

                            Console.WriteLine("data_average count adalah: ", Data_Avg_Result.Count().ToString());
                            //Console.WriteLine("data_average count adalah: ", current_interval.ToString());

                            if (Data_Avg_Result.Count() == TotalInterval)
                            {
                                Console.WriteLine("Start ");
                                stat_continue = false;
                                mySerialPort.DiscardInBuffer();
                                mySerialPort.DiscardOutBuffer();
                                //Sensor_input_Helper.Command_Stop(mySerialPort);
                                //mySerialPort.Close();

                                stat_continue = false;
                                start_next_cond = false;
                                aggregate_cond = false;
                                Console.WriteLine("break finish");
                                //current_interval++;
                                /*
                                Curr_Interval_TextBox.Invoke((Action)delegate
                                {
                                    Curr_Interval_TextBox.Text = (current_interval + 1).ToString();
                                });
                                */

                                finish_measurement = 1;
                                //MessageBox.Show("Measurement Finish");
                                //break;
                                //continue;

                            }


                            #endregion

                            if (start_next_cond == true)
                            {
                                #region Delay start
                                Console.WriteLine("start delay", "start delay");
                                //mySerialPort.Close();
                                Thread.Sleep(delay);
                                Console.WriteLine("Finish delay", "Finish delay");
                                #endregion
                            }
                            #region Start Next sequence

                            while (start_next_cond)
                            {
                                Console.WriteLine("Next Iter");
                                Console.WriteLine("counter init is: ");
                                Sensor_input_Helper.Command_Write(mySerialPort, ResultGrain);
                                Thread.Sleep(1000);
                                Sensor_input_Helper.Command_Write(mySerialPort, ResultMeasure);
                                current_interval++;
                                Curr_Interval_TextBox.Invoke((Action)delegate
                                {
                                    Curr_Interval_TextBox.Text = (current_interval + 1).ToString();
                                });

                                start_next_cond = false;
                                Console.WriteLine("Finish SendData with Data received");
                                blink_timer = 1;
                                timer_counter = 1;
                            }

                            #endregion
                            Console.WriteLine("Finish start next sequence region");

                        }
                        else
                        {
                            Console.WriteLine("Nilai Else adalah: " + Result_Parsing);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                    Console.WriteLine(ex);
                    //return "";
                }

            }

            //MessageBox.Show("measurement finsih");
            Console.WriteLine("Measurement Finish");
        }


        // This is check temp Button
        private void button2_Click(object sender, EventArgs e)
        {
            Sensor_input_Helper.Command_CheckTemp(mySerialPort);
            string result_temp = CheckTemp();


        }

        private string CheckTemp()
        {
            temp_cond = true;
            string Result_Parsing = "";
            while (temp_cond)
            {
                try
                {
                    Thread.Sleep(1500);// this solves the problem
                    byte[] readBuffer = new byte[mySerialPort.ReadBufferSize];
                    int readLen = mySerialPort.Read(readBuffer, 0, readBuffer.Length);
                    string readStr = string.Empty;

                    readStr = Encoding.UTF8.GetString(readBuffer, 0, readLen);
                    readStr = readStr.Trim();
                    Console.WriteLine("ReadStr adalah: " + readStr);

                    char[] delimiter_r = { '\r' };
                    string[] Measures_With_U = readStr.Split(delimiter_r);

                    Result_Parsing = Measures_With_U.Last();


                    if (Result_Parsing == "1000")
                    {
                        Temp_TextBox.Invoke((Action)delegate
                        {
                            Temp_TextBox.Text = "<-20C";
                        });
                        Sensor_input_Helper.Command_Stop(mySerialPort);
                        temp_cond = false;
                    }
                    else if (Result_Parsing == "1200")
                    {
                        Temp_TextBox.Invoke((Action)delegate
                        {
                            Temp_TextBox.Text = "-20C - 0C";
                        });
                        Sensor_input_Helper.Command_Stop(mySerialPort);
                        temp_cond = false;
                    }
                    else if (Result_Parsing == "1400")
                    {
                        Temp_TextBox.Invoke((Action)delegate
                        {
                            Temp_TextBox.Text = "50C - 70C";
                        });
                        Sensor_input_Helper.Command_Stop(mySerialPort);
                        temp_cond = false;
                    }

                    else
                    {
                        Result_Parsing = Result_Parsing.Substring(Result_Parsing.Length - 3);
                        Result_Parsing = String.Concat(Result_Parsing.Substring(0, Result_Parsing.Length - 1)
                                    , ".", Result_Parsing.Substring(Result_Parsing.Length - 1, 1));

                        Temp_TextBox.Invoke((Action)delegate
                        {
                            Temp_TextBox.Text = Result_Parsing;
                        });
                        Sensor_input_Helper.Command_Stop(mySerialPort);
                        temp_cond = false;
                    }

                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                    Console.WriteLine(ex);
                    //return "";
                }
            }

            Console.WriteLine("Finsih_Check_Temp");
            return Result_Parsing;
            //Sensor_input_Helper.Command_Stop(mySerialPort);
        }
        // This is Stop Button
        private void Btn_Stop_Click(object sender, EventArgs e)
        {
            Sensor_input_Helper.Command_Stop(mySerialPort);

        }

        private void Temp_Tex_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void Btn_Check_Click(object sender, EventArgs e)
        {

            // Open Port

            string comport = Combobox_ComPort.Text;
            int BaudRate = 1200;
            mySerialPort = new SerialPort("/dev/ttyS0");
            mySerialPort.Close();
            try
            {
                SensorHelper_2.OpenCon_Port(mySerialPort, BaudRate);
                Thread.Sleep(30);
                Sensor_input_Helper.Command_Check(mySerialPort);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Port is not connected");
                Console.WriteLine(ex.Message);
                //  Block of code to handle errors
            }

            try
            {
                //Sensor_input_Helper.MySQL_ConnectDatabase_test("192.168.0.4");
                Sensor_input_Helper.MySQL_ConnectDatabase();
                MessageBox.Show("connection succeed", application_name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database is not connected");
                Console.WriteLine(ex.Message);
                //  Block of code to handle errors
            }

            data_cleansing();
        }

        private void textBox_Password_TextChanged(object sender, EventArgs e)
        {

        }

        private void Constring_textBox_TextChanged(object sender, EventArgs e)
        {

        }


        private void Curr_Measure_TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sensor_input_Helper.Command_CheckData(mySerialPort);

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //option. Open new page. 
        private void button2_Click_1(object sender, EventArgs e)
        {
            Form2 F2 = new Form2();
            F2.ShowDialog();
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void Combobox_NumInterval_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void Combobox_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Console.WriteLine("test");
            this.Close();
        }

        private void button2_Click_3(object sender, EventArgs e)
        {
            //Sensor_input_Helper.Command_Write(mySerialPort, "10192\r");
            //Thread.Sleep(1000);
            //Sensor_input_Helper.Command_Write(mySerialPort, "22094\r");
            this.Invalidate();
            this.Refresh();
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            this.Controls.Clear();// 'removes all the controls on the form
            InitializeComponent();// 'load all the controls again
            Form1_Load(e, e);// 'Load everything in your form, load event again
            //Sensor_input_Helper.Command_Write(mySerialPort, "22094\r");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Sensor_input_Helper.Command_Write(mySerialPort, "10192\r");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Sensor_input_Helper.Command_Write(mySerialPort, "22094\r");
        }
    }
}
