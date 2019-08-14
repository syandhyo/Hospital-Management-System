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

public partial class PHARMACYSTORE_USER_PatientSearch : System.Web.UI.Page
{
    SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
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
        lbluid.Text = Session["UID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {
            binddata();
            div1.Visible = true;
            div2.Visible = true;
        }
        con.Close();
    }
    public void binddata()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand COM = new SqlCommand("FINANCIAL_YEAR", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                dr = COM.ExecuteReader();
                if (dr.Read())
                {
                    lblfyear.Text = dr["FYEAR"].ToString();
                }
            }
            dr.Close();

            using (SqlCommand cmd = new SqlCommand("phrm_ptnSearhDrop", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                adp.Fill(dt1);

                DropDownList1.DataSource = dt1;
                DropDownList1.DataTextField = "BEDNO";
                DropDownList1.DataValueField = "BEDNO";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "Please Select");              
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();       
        using (SqlCommand cmd = new SqlCommand("Pharmcy_Patientseach", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SHOWID";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtspid.Text;
            cmd.Parameters.Add("@TELPHONE", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if(dt.Rows.Count>0)
            {
                // GridView1.SelectedIndex = 0;
                GridView1.DataSource = dt;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            else
            {
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            txtspid.Focus();
        }
        con.Close();
         }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnshowph_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            
            using (SqlCommand cmd = new SqlCommand("Pharmcy_Patientseach", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SHOWPHON";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value ="";
                cmd.Parameters.Add("@TELPHONE", SqlDbType.VarChar).Value = txtsmobile.Text; ;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";                
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);  
             if(dt.Rows.Count>0)
            {
                GridView1.DataSource = dt;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            else
            {
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            }
            txtsmobile.Focus();
            con.Close();
        }        
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnshowip_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
          
            using (SqlCommand cmd = new SqlCommand("Pharmcy_Patientseach", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SHOWIP";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@TELPHONE", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtip.Text;
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                 if(dt.Rows.Count>0)
            {
                //  GridView2.SelectedIndex = 0;
                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
            else
            {
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            }
            txtip.Focus();
            con.Close();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnshowbed_Click(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList1.SelectedIndex == 0)
            {
                string message = "alert('Please!! Choose The Bed No..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);               
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("Pharmcy_Patientseach", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SHOWBED";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@TELPHONE", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = DropDownList1.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
               // GridView2.SelectedIndex = 0;
                GridView2.DataSource = dt;
                GridView2.DataBind();
               }
                else
                {
                   string message = "alert(' No Record Found')";
                   ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}