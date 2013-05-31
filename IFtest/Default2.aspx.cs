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


namespace IFtest
{
    public partial class Default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connString = WebConfigurationManager.AppSettings["dbConnectionString"];
 
            string curDBCon = "not found";

            try
            {

                //curDBCon = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                curDBCon = WebConfigurationManager.AppSettings["dbConnectionString"];

            }
            catch { }

            lblConStr.Text = curDBCon;


            try
            {
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
            }
            catch (Exception ex)
            {
                lblInstance_id.Text = ex.Message;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connString = WebConfigurationManager.AppSettings["dbConnectionString"];
            string tableName = "test" + Guid.NewGuid().ToString().Replace("-", "");
            try
            {
                SqlConnection conn = new SqlConnection(connString);


                lblConStr.Text += " conn.ConnectionString: " + conn.ConnectionString.ToString();

                conn.Open();
                try
                {
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = "Create table " + tableName + " (id smallint, description varchar(50))";
                    command.ExecuteNonQuery();
                    for (int i = 0; i < 20; i++)
                    {
                        command.CommandText = "insert into " + tableName + " (id, description) values (" + i + ", \'" + Guid.NewGuid().ToString() + "\')";
                        command.ExecuteNonQuery();
                    }

                    command.CommandText = "select * from " + tableName;
                    SqlDataReader reader = command.ExecuteReader();
                    GridView1.DataSource = reader;
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write("oops, something went terribly wrong:" + ex.ToString());
                }
                finally
                {
                    conn.Close();
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