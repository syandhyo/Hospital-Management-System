using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

public partial class ACCOUNTS_Bouncedchk : System.Web.UI.Page
{
    string num1 = "0";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    GridViewRow gr;
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
        // lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {
            binddata();
            // bindTGrid();
        }
        con.Close();
    }
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand COM = new SqlCommand("SELECT FYEAR FROM FYEAR1_TABLE", con);
        dr = COM.ExecuteReader();
        if (dr.Read())
        {
            //Label1.Text = dr["FYEAR"].ToString();
            lblfyear.Text = dr["FYEAR"].ToString();
        }
        dr.Close();

        //da1 = new SqlDataAdapter("select EMPID,Sname from tblStaff", con);
        //DataTable ds1 = new DataTable();
        //da1.Fill(ds1);
        //ddEmpl.DataSource = ds1;
        //ddEmpl.DataTextField = "EMPID";
        //ddEmpl.DataValueField = "EMPID";
        //ddEmpl.DataBind();
        //ddEmpl.Items.Insert(0, "Please Select");
        //con.Close();
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            // auto();
            using (SqlCommand CM_cmd = new SqlCommand("USP_BOUNCEDCHK", con))
            {
                CM_cmd.CommandType = CommandType.StoredProcedure;
                CM_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";

                CM_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                CM_cmd.Parameters.Add("@RECEVFRM", SqlDbType.VarChar).Value = txtRecivefr.Text;

                CM_cmd.Parameters.Add("@CHKNO", SqlDbType.VarChar).Value = txtChkNo.Text;
                CM_cmd.Parameters.Add("@BANKNAME", SqlDbType.VarChar).Value = txtBaName.Text;
                CM_cmd.Parameters.Add("@AMT", SqlDbType.Decimal).Value = txtAmt.Text;
                CM_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                CM_cmd.ExecuteNonQuery();
            }
            con.Close();
            clear();
            string message = "alert('*Inserted  Successfully .')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        //  Response.Redirect("Salryovertime.aspx");
    }
    public void clear()
    {
        txtdate.Text = "";
        txtRecivefr.Text = "";
        txtChkNo.Text = "";
        txtBaName.Text = "";
        txtAmt.Text = "";
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ACCOUNTS/Bouncedchk.aspx");
    }
}