using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;

using System.IO;

using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel; // CancelEventArgs
using System.Configuration;
using ControllerPage.Constant;
using System.Reflection;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
//using MySql.data.Entity;
using System.Data;
using System.Net;
using System.Net.Sockets;

namespace ControllerPage.Helper
{
    public class Sensor_input_Helper
    {

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        public static void Command_Check(SerialPort mySerialPort)
        {
            //mySerialPort.Write("123");
            //public void Write (byte[] buffer, int offset, int count);
            string data = "00191\r";
            byte[] hexstring = Encoding.ASCII.GetBytes(data);

            foreach (byte hexval in hexstring)
            {
                byte[] _hexval = new byte[] { hexval };     // need to convert byte // to byte[] to write
                mySerialPort.Write(_hexval, 0, 1);
                //Thread.Sleep(10);
            }
        }
        public static void Command_CheckData(SerialPort mySerialPort)
        {
            //mySerialPort.Write("123");
            //public void Write (byte[] buffer, int offset, int count);
            string data = "9119B\r";
            byte[] hexstring = Encoding.ASCII.GetBytes(data);
            foreach (byte hexval in hexstring)
            {
                byte[] _hexval = new byte[] { hexval };     // need to convert byte 
                                                            // to byte[] to write
                mySerialPort.Write(_hexval, 0, 1);
                //Thread.Sleep(10);
            }
        }
        public static void Command_Stop(SerialPort mySerialPort)
        {
            //mySerialPort.Write("123");
            //public void Write (byte[] buffer, int offset, int count);
            string data = "\r";
            byte[] hexstring = Encoding.ASCII.GetBytes(data);
            foreach (byte hexval in hexstring)
            {
                byte[] _hexval = new byte[] { hexval };     // need to convert byte 
                                                            // to byte[] to write
                mySerialPort.Write(_hexval, 0, 1);
                //Thread.Sleep(10);
            }
        }

        public static void Command_CheckTemp(SerialPort mySerialPort)
        {
            //mySerialPort.Write("123");
            //public void Write (byte[] buffer, int offset, int count);
            string data = "9239E\r";
            byte[] hexstring = Encoding.ASCII.GetBytes(data);
            foreach (byte hexval in hexstring)
            {
                byte[] _hexval = new byte[] { hexval };     // need to convert byte 
                                                            // to byte[] to write
                mySerialPort.Write(_hexval, 0, 1);
                //Thread.Sleep(10);
            }
        }


        public static void Command_MoistureMeasure(SerialPort mySerialPort, string input)
        {
            string data = input;
            byte[] hexstring = Encoding.ASCII.GetBytes(data);
            foreach (byte hexval in hexstring)
            {
                byte[] _hexval = new byte[] { hexval };     // need to convert byte 
                                                            // to byte[] to write
                mySerialPort.Write(_hexval, 0, 1);
                //Thread.Sleep(10);
            }
        }
        public static void Command_NumberofGrain(SerialPort mySerialPort, string input)
        {

            string data = input;
            byte[] hexstring = Encoding.ASCII.GetBytes(data);
            foreach (byte hexval in hexstring)
            {
                byte[] _hexval = new byte[] { hexval };     // need to convert byte 
                                                            // to byte[] to write
                mySerialPort.Write(_hexval, 0, 1);
                //Thread.Sleep(10);
            }
        }

        public static void Command_Write(SerialPort mySerialPort, string input)
        {
            string data = input;
            byte[] hexstring = Encoding.ASCII.GetBytes(data);
            foreach (byte hexval in hexstring)
            {
                byte[] _hexval = new byte[] { hexval };     // need to convert byte 
                                                            // to byte[] to write
                mySerialPort.Write(_hexval, 0, 1);
                //Thread.Sleep(10);
            }

        }
        public static void Command_MoisturAggregate(SerialPort mySerialPort)
        {
            //mySerialPort.Write("123");
            //public void Write (byte[] buffer, int offset, int count);
            string data = "9129C\r";
            byte[] hexstring = Encoding.ASCII.GetBytes(data);
            foreach (byte hexval in hexstring)
            {
                byte[] _hexval = new byte[] { hexval };     // need to convert byte 
                                                            // to byte[] to write
                mySerialPort.Write(_hexval, 0, 1);
                //Thread.Sleep(10);
            }
        }
        public static List<string> Get_List_Time_Interval()
        {
            var attributes = typeof(Time_Interval).GetMembers()
                .SelectMany(member => member.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>())
                .ToList();

            var result = attributes.Select(x => x.Description);
            List<string> asList = attributes.Select(x => x.Description).ToList();

            return asList;

        }
        public static List<string> Get_List_Number_Grain()
        {
            var attributes = typeof(number_grain).GetMembers()
                .SelectMany(member => member.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>())
                .ToList();

            var result = attributes.Select(x => x.Description);
            List<string> asList = attributes.Select(x => x.Description).ToList();

            return asList;

        }
        public static string GetDescription(Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
        public static T GetEnumValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException();
            FieldInfo[] fields = type.GetFields();
            var field = fields
                            .SelectMany(f => f.GetCustomAttributes(
                                typeof(DescriptionAttribute), false), (
                                    f, a) => new { Field = f, Att = a })
                            .Where(a => ((DescriptionAttribute)a.Att)
                                .Description == description).SingleOrDefault();
            return field == null ? default(T) : (T)field.Field.GetRawConstantValue();
        }

        public static string GetDescriptionFromEnumValue(Enum value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
        
        public void SQL_ConnectDatabase()
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=DESKTOP-4MMBVA4\SQLEXPRESS;Initial Catalog=Sensor_Result;User ID=sa_admin;Password=P@ssw0rd";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            //MessageBox.Show("Connection Open!");
            //cnn.Close();
        }
        //string myConnectionString = "server=localhost;database=testDB;uid=root;pwd=abc123;";
        public static void MySQL_ConnectDatabase()
        {
            //string server = "Localhost_test";

            //string server = "103.224.212.219";
            string server = "192.168.0.4";

            //string server = "192.168.0.6";
            //string server = "raspberrypi";

            //string server = "Localhost via UNIX socket";
            MySqlConnection connection;
            //string server = "localhost";
            string database = "sensor_database";
            string user = "root";
            //string password = "admin";
            string password = "raspberry";
            string port = "3306";
            string sslM = "none";
            string connectionString;
            
            connectionString = String.Format("server={0};port={1};user id={2}; password={3}" +
                "; database={4}; SslMode={5}", server, port, user, password, database, sslM);
            
            /*
            connectionString = String.Format("server={0};port={1};user id={2};password={3}" +
                "; database={4}", server, port, user, password, database);
            */

            connection = new MySqlConnection(connectionString);
            
            try
            {
                connection.Open();
                //MessageBox.Show("successful connection");
                connection.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message + connectionString);
            }
        }

        public static void MySQL_ConnectDatabase_test(string connection_string_input)
        {
            //string server = "Localhost_test";

            //string server = "103.224.212.219";
            //string server = "192.168.0.6";
            //string server = "raspberrypi";
            
            //string server = "Localhost via UNIX socket";
            MySqlConnection connection;
            string server = "localhost";
            string database = "sensor_database";
            string user = "root";
            //string password = "admin";
            string password = "admin";
            string port = "3306";
            string sslM = "none";
            string connectionString;

            connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);

            /*
            connectionString = String.Format("server={0};port={1};user id={2};password={3}" +
                "; database={4}", server, port, user, password, database);
            */

            connection = new MySqlConnection(connection_string_input);

            try
            {
                connection.Open();
                MessageBox.Show("successful connection");
                connection.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message + connectionString);
            }
        }


        public static int MySql_Insert_Batch( string IPAddress_varinput,string product_varinput
            , int total_interval_varinput, string time_interval_varinput, int number_perinterval_varinput, string Temperature_Var_varinput)
        {
            //MySqlConnection connection;
            //string server = "localhost";
            //string server = "localhost";
            string server = "192.168.0.4";
            //string server = "192.168.0.6";

            string database = "sensor_database";
            string user = "root";
            //string password = "admin";
            string password = "raspberry";
            string port = "3306";
            string sslM = "none";
            string connectionString;

            //connectionString = String.Format("server={0};port={1};user id={2}; password={3}" +
            //    "; database={4}; SslMode={5}", server, port, user, password, database, sslM);


            connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);
            
            using (var connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand("Insert_Batch", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("IPAddress_var", IPAddress_varinput));
                command.Parameters.Add(new MySqlParameter("Product_var", product_varinput));
                command.Parameters.Add(new MySqlParameter("Total_Interval_var", total_interval_varinput));
                command.Parameters.Add(new MySqlParameter("Time_Interval_var", time_interval_varinput));
                command.Parameters.Add(new MySqlParameter("Number_Per_Interval_var", number_perinterval_varinput));
                command.Parameters.Add(new MySqlParameter("Temperature_Var", Temperature_Var_varinput));
                // Add parameters
                command.Parameters.Add(new MySqlParameter("?Out_Result_Batch_ID", MySqlDbType.VarChar));
                command.Parameters["?Out_Result_Batch_ID"].Direction = ParameterDirection.Output;


                command.Connection.Open();
                var result = command.ExecuteNonQuery();

                //cmd.ExecuteNonQuery();
                //int lastInsertID = Convert.ToInt32(command.Parameters["Result_Batch_ID"].Value);
                //string str_batch = (string)command.Parameters["?Out_Result_Batch_ID"].Value;
                
                //int PG = (int)command.Parameters["?Out_Result_Batch_ID"].Value;

                //int lastInsertID = (int)command.Parameters["?Out_Result_Batch_ID"].Value;
                //int lastInsertID = (int)command.Parameters["?Out_Result_Batch_ID"].Value;

                //int lastInsertID = 0;
                int lastInsertID  = Int32.Parse( (string)command.Parameters["?Out_Result_Batch_ID"].Value);
                //int lastInsertID = Convert.ToInt32(cmd.Parameters["@fileid"].Value);
                command.Connection.Close();

                return lastInsertID;
            }

        }
        public static void MySql_Insert_Measure(int Batch_ID_varinput, int PerBatch_ID_varinput, float measure_result_varinput, DateTime Created_On_varinput, int IsAverage_varinput)
        {
            //string server = "localhost";
            //MySqlConnection connection;
            //string server = "localhost";
            string server = "192.168.0.4";

            //string server = "192.168.0.6";
            
            string database = "sensor_database";
            string user = "root";
            //string password = "admin";
            string password = "raspberry";
            string port = "3306";
            string sslM = "none";
            
            string connectionString;

            connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);


            using (var connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand("Insert_Measure", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("Batch_ID_var", Batch_ID_varinput));
                command.Parameters.Add(new MySqlParameter("PerBatchID_var", PerBatch_ID_varinput));
                command.Parameters.Add(new MySqlParameter("measure_result_var", measure_result_varinput));
                command.Parameters.Add(new MySqlParameter("Created_On_var", Created_On_varinput));
                command.Parameters.Add(new MySqlParameter("IsAverage_var", IsAverage_varinput));


                command.Connection.Open();
                var result = command.ExecuteNonQuery();
                command.Connection.Close();
            }

        }

        public static int MySQL_Get_Parameter(string IPAddress_varinput, string product_varinput
            , int total_interval_varinput, string time_interval_varinput, int number_perinterval_varinput, string Temperature_Var_varinput)
        {
            //MySqlConnection connection;
            //string server = "localhost";
            //string server = "localhost";
            string server = "192.168.0.4";
            //string server = "192.168.0.6";

            string database = "sensor_database";
            string user = "root";
            //string password = "admin";
            string password = "raspberry";
            string port = "3306";
            string sslM = "none";
            string connectionString;

            //connectionString = String.Format("server={0};port={1};user id={2}; password={3}" +
            //    "; database={4}; SslMode={5}", server, port, user, password, database, sslM);


            connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);

            using (var connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand("Insert_Batch", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("IPAddress_var", IPAddress_varinput));
                command.Parameters.Add(new MySqlParameter("Product_var", product_varinput));
                command.Parameters.Add(new MySqlParameter("Total_Interval_var", total_interval_varinput));
                command.Parameters.Add(new MySqlParameter("Time_Interval_var", time_interval_varinput));
                command.Parameters.Add(new MySqlParameter("Number_Per_Interval_var", number_perinterval_varinput));
                command.Parameters.Add(new MySqlParameter("Temperature_Var", Temperature_Var_varinput));
                // Add parameters
                command.Parameters.Add(new MySqlParameter("?Out_Result_Batch_ID", MySqlDbType.VarChar));
                command.Parameters["?Out_Result_Batch_ID"].Direction = ParameterDirection.Output;


                command.Connection.Open();
                var result = command.ExecuteNonQuery();

                //cmd.ExecuteNonQuery();
                //int lastInsertID = Convert.ToInt32(command.Parameters["Result_Batch_ID"].Value);
                //string str_batch = (string)command.Parameters["?Out_Result_Batch_ID"].Value;

                //int PG = (int)command.Parameters["?Out_Result_Batch_ID"].Value;

                //int lastInsertID = (int)command.Parameters["?Out_Result_Batch_ID"].Value;
                //int lastInsertID = (int)command.Parameters["?Out_Result_Batch_ID"].Value;

                //int lastInsertID = 0;
                int lastInsertID = Int32.Parse((string)command.Parameters["?Out_Result_Batch_ID"].Value);
                //int lastInsertID = Convert.ToInt32(cmd.Parameters["@fileid"].Value);
                command.Connection.Close();

                return lastInsertID;
            }

        }


        public static void MySQL_Update_Parameter(string IPAddress_varinput, string product_varinput
            , int total_interval_varinput, string time_interval_varinput, int number_perinterval_varinput, string Temperature_Var_varinput)
        {
            //MySqlConnection connection;
            //string server = "localhost";
            //string server = "localhost";
            string server = "192.168.0.4";
            //string server = "192.168.0.6";

            string database = "sensor_database";
            string user = "root";
            //string password = "admin";
            string password = "raspberry";
            string port = "3306";
            string sslM = "none";
            string connectionString;

            //connectionString = String.Format("server={0};port={1};user id={2}; password={3}" +
            //    "; database={4}; SslMode={5}", server, port, user, password, database, sslM);


            connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);

            using (var connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand("Insert_Batch", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new MySqlParameter("IPAddress_var", IPAddress_varinput));
                command.Parameters.Add(new MySqlParameter("Product_var", product_varinput));
                command.Parameters.Add(new MySqlParameter("Total_Interval_var", total_interval_varinput));
                command.Parameters.Add(new MySqlParameter("Time_Interval_var", time_interval_varinput));
                command.Parameters.Add(new MySqlParameter("Number_Per_Interval_var", number_perinterval_varinput));
                command.Parameters.Add(new MySqlParameter("Temperature_Var", Temperature_Var_varinput));
                // Add parameters
                command.Parameters.Add(new MySqlParameter("?Out_Result_Batch_ID", MySqlDbType.VarChar));
                command.Parameters["?Out_Result_Batch_ID"].Direction = ParameterDirection.Output;


                command.Connection.Open();
                var result = command.ExecuteNonQuery();

                //cmd.ExecuteNonQuery();
                //int lastInsertID = Convert.ToInt32(command.Parameters["Result_Batch_ID"].Value);
                //string str_batch = (string)command.Parameters["?Out_Result_Batch_ID"].Value;

                //int PG = (int)command.Parameters["?Out_Result_Batch_ID"].Value;

                //int lastInsertID = (int)command.Parameters["?Out_Result_Batch_ID"].Value;
                //int lastInsertID = (int)command.Parameters["?Out_Result_Batch_ID"].Value;

                //int lastInsertID = 0;
                int lastInsertID = Int32.Parse((string)command.Parameters["?Out_Result_Batch_ID"].Value);
                //int lastInsertID = Convert.ToInt32(cmd.Parameters["@fileid"].Value);
                command.Connection.Close();

            }

        }


        public void InsertSensorResult()
        {


        }


    }
}
