using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;

public partial class PHARMACYSTORE_USER_finditem : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
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
        lblorgid.Text = Session["ORGID"].ToString();
       
        if (!IsPostBack)
        {
            binddata();
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
                SqlDataReader dr = COM.ExecuteReader();
                if (dr.Read())
                {
                    lblfyear.Text = dr["FYEAR"].ToString();
                }
                dr.Close();
            }
          
            using (SqlCommand cmd = new SqlCommand("phrm_finditmDrop", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                adp.Fill(ds1);
                DropDownList1.DataSource = ds1;
                DropDownList1.DataTextField = "NAME";
                DropDownList1.DataValueField = "NAME";
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList1.SelectedIndex == 0)
            {
                string message = "alert('Please!! Choose The Item Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //da1 = new SqlDataAdapter("select distinct A.NAME AS NAME,A.SELF AS SHELF, A.RACK AS RACK,SUM(B.QTY) AS QUANTITY FROM ITEM_TABLE A,STOCK_TABLE B  WHERE A.NAME='" + DropDownList1.Text + "' AND A.NAME=B.NAME  AND a.ORGID='" + lblorgid.Text + "' GROUP BY B.NAME,A.NAME,A.SELF,A.RACK ", con);
            using (SqlCommand cmd = new SqlCommand("Pharmcy_finditem", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SHOWID";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = DropDownList1.Text;

                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds1 = new DataTable();
                adp.Fill(ds1);
                GridView1.DataSource = ds1;
                GridView1.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
       
    }
}