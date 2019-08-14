using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;

public partial class RECEPTION_Ambulance_receipt : System.Web.UI.Page
{
    SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            lblid.Text = Session["NAME"].ToString();
            lblorgid.Text = Session["ORGID"].ToString();
            lbluid.Text = Session["UID"].ToString();
            string c_id = Session["idAmb"].ToString();
            try
            {

                using (SqlCommand COM = new SqlCommand("FINANCIAL_YEAR", con))
                {
                    COM.CommandType = CommandType.StoredProcedure;
                    COM.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    dr = COM.ExecuteReader();
                    if (dr.Read())
                    {
                        lblfyear.Text = dr["FYEAR"].ToString();
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
            }
            using (SqlCommand cmd = new SqlCommand("RECP_AMBULANCE_RECEIPT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = Session["id"].ToString();
                //SqlCommand COM1 = new SqlCommand("SELECT * FROM tblambulance WHERE ID='" + Session["id"].ToString() + "'", con);

                //SqlCommand COM1 = new SqlCommand("SELECT * FROM tblambulance WHERE ID='" + c_id + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Label1.Text = dr["Adate"].ToString();
                    Label2.Text = dr["from"].ToString();
                    Label3.Text = dr["PatientName"].ToString();
                    Label4.Text = dr["to"].ToString();
                    Label5.Text = dr["AttendentName"].ToString();
                    Label6.Text = dr["Approxkm"].ToString();
                    Label7.Text = dr["contactno"].ToString();
                    Label8.Text = dr["fee"].ToString();
                }
                dr.Close();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
   
}