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
public partial class ADMIN_PHARMACYSTORE_Store_Master : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
    decimal total_amt = 0;
    decimal total_vat_amt = 0;
    int i, no, no1, sl;
    GridViewRow gr;
    public void auto()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            string qry1 = "select ID from PHARMACY_STORE_MASTER";
            com = new SqlCommand(qry1, con);
            dr = null;

            dr = com.ExecuteReader();
            while (dr.Read())
            {
                num1 = dr["ID"].ToString();
            }
            num1 = string.Format("ST{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
            txtid.Text = num1;
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["out"] == "INACTIVE")
        {
            Response.Redirect("~/index.aspx");
        }
        Response.Buffer = true;

        Response.CacheControl = "no-cache";
        if (Session["NAME"] == null)
        {
            Response.Redirect("~/index.aspx");
        }
      
        lblid.Text = Session["NAME"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        if (!IsPostBack)
        {
            binddata();
        }
        if (txtname.Text == "")
        {
            btncreate.Visible = true;
            btnupdate.Visible = false;
        }
        else
        {
            btncreate.Visible = false;
            btnupdate.Visible = true;
        }
    }
    public void binddata()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("phrmc_storeShow", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                txtid.Text = ds.Tables[0].Rows[0]["ID"].ToString();
                txtname.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                txtphone.Text = ds.Tables[0].Rows[0]["PHONE"].ToString();
                txtstatecode.Text = ds.Tables[0].Rows[0]["STATECODE"].ToString();
                txtgst.Text = ds.Tables[0].Rows[0]["GSTNO"].ToString();
                txtaddress.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
            }

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
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtname.Text == "")
            {
                //Response.Write("<script LANGUAGE='JavaScript' >alert('* Fields are mandatory.')</script>");
                string message = "alert('Name Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtstatecode.Text == "")
            {
               // Response.Write("<script LANGUAGE='JavaScript' >alert('Enter Hsncode')</script>");
                string message = "alert('State Code are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtgst.Text == "")
            {
                //Response.Write("<script LANGUAGE='JavaScript' >alert('Enter Opening Balance')</script>");
                string message = "alert('GST Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtaddress.Text == "")
            {
                //Response.Write("<script LANGUAGE='JavaScript' >alert('Enter Purchase Price')</script>");
                string message = "alert('Address Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtphone.Text == "")
            {
                //Response.Write("<script LANGUAGE='JavaScript' >alert('Enter Sale Price')</script>");
                string message = "alert('Phone Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand cmd1 = new SqlCommand("phrmc_storeInsUp", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
                cmd1.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = txtphone.Text;
                cmd1.Parameters.Add("@GSTNO", SqlDbType.VarChar).Value = txtgst.Text;
                cmd1.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtstatecode.Text;
                cmd1.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = txtaddress.Text;
                cmd1.ExecuteNonQuery();
            }
            string message1 = "alert('Created Successfully.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            con.Close();
            //Response.Write("<script LANGUAGE='JavaScript' >alert('Created Successfully.')</script>");          
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtname.Text == "")
            {
                //Response.Write("<script LANGUAGE='JavaScript' >alert('* Fields are mandatory.')</script>");
                string message = "alert('Name Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtstatecode.Text == "")
            {
                string message = "alert('State Code are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtgst.Text == "")
            {
                string message = "alert('GST Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;               
            }
            else if (txtaddress.Text == "")
            {
                string message = "alert('Address Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;             
            }
            else if (txtphone.Text == "")
            {
                string message = "alert('Phone Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("phrmc_storeInsUp", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
                cmd1.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = txtphone.Text;
                cmd1.Parameters.Add("@GSTNO", SqlDbType.VarChar).Value = txtgst.Text;
                cmd1.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtstatecode.Text;
                cmd1.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = txtaddress.Text;
                cmd1.ExecuteNonQuery();
            }
            con.Close();
            string message1 = "alert('Updated Successfully.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
           // Response.Write("<script LANGUAGE='JavaScript' >alert('Created Successfully.')</script>");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
}