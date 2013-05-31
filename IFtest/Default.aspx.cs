using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Helpers;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace IFtest
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtMachineName.Text = "page loaded initial value";

            //try
            //{
            //    txtMachineName.Text = "1. " + Environment.MachineName + "\r\n";
            //    txtMachineName.Text += "| 2. " + System.Environment.GetEnvironmentVariable("COMPUTERNAME") + "\r\n";
            //    txtMachineName.Text += "| 3. " + System.Net.Dns.GetHostName() + "\r\n";
            //    txtMachineName.Text += "| 4. " + System.Windows.Forms.SystemInformation.ComputerName + "\r\n";
            //}
            //catch (Exception ex)
            //{
            //    txtMachineName.Text = ex.Message;
            //}

            //if (string.IsNullOrEmpty(txtMachineName.Text))
            //{
            //    txtMachineName.Text = "something is blowing up but not generating an exception error!";
            //}

            try
            {
                txtConnectionInfo.Text = "";

                //decode then iterate through the VCAP_APPLICTION settings Iron/Cloud Foundry provides to the application at run time
                foreach (var p in Json.Decode(ConfigurationManager.AppSettings["VCAP_APPLICATION"]))
                {
                    
                    string SettingName1 = p.Key.ToString();
                    string Settingvalue = p.Value.ToString();

                    if (SettingName1 == "instance_id")
                    {
                        lblInstance_id.Text = Settingvalue;
                    }
                    else if (SettingName1 == "instance_index")
                    {
                        lblInstanceIndex.Text = Settingvalue;
                    }
                }
                //Also available: HOME (string value, local drive path) VCAP_SERVICES (JSON decode for db strings etc.) and VCAP_APP_HOST (string value, host IP address)
            }
            catch (Exception ex)
            {
                lblInstance_id.Text = ex.Message;
            }


            try
            {
                //ListBox1.Items.Clear();
                
                //decode then iterate through the VCAP_APPLICTION settings Iron/Cloud Foundry provides to the application at run time
                foreach (var p in Json.Decode(ConfigurationManager.AppSettings["VCAP_SERVICES"]))
                {
                    
                    string SettingName1 = p.Key.ToString();
                    string Settingvalue = p.Value.ToString();
                    var whatAmI = p.Value;
                                                           
                    string serviceType = SettingName1;
                    
                    foreach (var VCAP_SERVICES in p.Value)
                    {
                        if (VCAP_SERVICES.Key == "credentials")
                        {

                            foreach (var connectInfo in VCAP_SERVICES.Value)
                            {
                                if (connectInfo.Value != null)
                                {
                                    string keyValue = connectInfo.Key;
                                    string keyLabel = "";

                                    switch (keyValue)
                                    {
                                        case "name":
                                            keyLabel = "db catalog name: ";
                                            break;
                                        case "hostname":
                                            keyLabel = "db host name: ";
                                            break;
                                        case "host":
                                            keyLabel = "db host: ";
                                            break;
                                        case "port":
                                            keyLabel = "db port: ";
                                            break;
                                        case "user":
                                            keyLabel = "db user name: ";
                                            break;
                                        case "username":
                                            keyLabel = "db username: ";
                                            break;
                                        case "password":
                                            keyLabel = "db user password: ";
                                            break;
                                    }

                                    if (!string.IsNullOrEmpty(keyLabel))
                                    {
                                        //ListBox1.Items.Add(keyLabel + connectInfo.Value.ToString());
                                        txtConnectionInfo.Text += keyLabel + connectInfo.Value.ToString() + "\n";
                                    }
                                }
                                else
                                {
                                    //ListBox1.Items.Add("Key = " + connectInfo.Key + " | Value = null");
                                    txtConnectionInfo.Text += "Key = " + connectInfo.Key + " | Value = null" + "\n";
                                }
                            }

                        }

                        else if (VCAP_SERVICES.Value != null)
                        {
                            string keyValue = VCAP_SERVICES.Key;
                            switch (keyValue)
                            {
                                case "name":
                                    //ListBox1.Items.Add("DB CF Service Name: " + VCAP_SERVICES.Value.ToString());
                                    txtConnectionInfo.Text += "DB CF Service Name: " + VCAP_SERVICES.Value.ToString() + "\n";
                                    break;
                                case "label":
                                    //ListBox1.Items.Add("DB CF Service Type: " + VCAP_SERVICES.Value.ToString());
                                    txtConnectionInfo.Text += "DB CF Service Type: " + VCAP_SERVICES.Value.ToString() + "\n";
                                    break;
                                case "vendor":
                                    {
                                        if (VCAP_SERVICES.Value.ToString() != "mssql")
                                        {
                                            Button1.Enabled = false;
                                        }
                                        else
                                        {
                                            Button1.Enabled = true;
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                            
                        }
                        //else
                        //{
                        //    //ListBox1.Items.Add("Key = " + VCAP_SERVICES.Key + " | Value = null");
                        //}

                        //Console.WriteLine("Key = {0}, Value = {1}", VCAP_SERVICES.Key, VCAP_SERVICES.Value.ToString());
                        //string DB_NAME= VCAP_SERVICES["name"];

                        //string DB_USER= VCAP_SERVICES["user"];

                        //string DB_PASSWORD= VCAP_SERVICES["password"];

                        //string DB_HOST= VCAP_SERVICES["hostname"];

                        //string DB_PORT= VCAP_SERVICES["port"];
                    }

                                        

                    //if (SettingName1 == "instance_id")
                    //{
                    //    lblInstance_id.Text = Settingvalue;
                    //}
                    //else if (SettingName1 == "instance_index")
                    //{
                    //    lblInstanceIndex.Text = Settingvalue;
                    //}
                }
                //Also available: HOME (string value, local drive path) VCAP_SERVICES (JSON decode for db strings etc.) and VCAP_APP_HOST (string value, host IP address)
            }
            catch (Exception ex)
            {
                //txtMachineName.Text += "Error in for loop" + ex.Message;
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //get the connection string named "Default" (from the web.config)
            string connString = ConfigurationManager.ConnectionStrings["Default"].ToString();
            txtConnectionString.Text = connString;
            string tableName = "table1";

            try
            {
                //setup the data connection
                using (SqlConnection conn = new SqlConnection(connString))
                {

                    
                    //open the connection
                    conn.Open();
                    try
                    {
                        //create a SQL command object
                        SqlCommand command = conn.CreateCommand();
                        try
                        {
                            //In a try catch set the create statements (or better: write code to check if the creates have already been done and skip if already done).
                            command.CommandText = "Create table " + tableName + " (id int IDENTITY(1,1) NOT NULL, name varchar(50) NULL) ON [PRIMARY]";
                            //Run the create commands
                            command.ExecuteNonQuery();

                            //add any inserts to populate drop downs etc.
                            for (int i = 0; i < 20; i++)
                            {
                                command.CommandText = "insert into " + tableName + " (name) values (\'" + Guid.NewGuid().ToString() + "\')";
                                command.ExecuteNonQuery();
                            }
                        }
                        catch { }

                        //select data from your database and bind it to a control.
                        command.CommandText = "select * from " + tableName;
                        SqlDataReader reader = command.ExecuteReader();
                        GridView1.DataSource = reader;
                        GridView1.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("oops, something went terribly wrong:" + ex.ToString());
                    }

                }

            }
            catch (SqlException ex)
            {
                lblConStr.Text += " SQLException: " + ex.Message;
            }
            catch (Exception ex)
            {
                lblConStr.Text += " Exception: " + ex.Message;
            }

        }
    }
}