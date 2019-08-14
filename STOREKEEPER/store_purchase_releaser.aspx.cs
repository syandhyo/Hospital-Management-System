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
using System.Web.Services;

public partial class STOREKEEPER_store_purchase_releaser : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;

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
        lblorgid.Text = Session["ORGID"].ToString();
        if (!IsPostBack)
        {
            binddata();

        }

        con.Close();
    }
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
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
        using (SqlCommand COM1 = new SqlCommand("STORE_PURCHASE_RELEASER", con))
        {
            COM1.CommandType = CommandType.StoredProcedure;
            COM1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
            SqlDataAdapter da = new SqlDataAdapter(COM1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grdporelese.SelectedIndex = 0;
            grdporelese.DataSource = dt;
            //  grdporelese.DataKeyNames = new string[] { "ID" };
            grdporelese.DataBind();
        }
        con.Close();
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        binddata();
    }
    protected void grdporelese_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand COM1 = new SqlCommand("STORE_PURCHASE_RELEASER", con))
            {
                COM1.CommandType = CommandType.StoredProcedure;
                COM1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
                SqlDataAdapter da = new SqlDataAdapter(COM1);
                //SqlDataAdapter da = new SqlDataAdapter("SELECT GRNNO,CONVERT(VARCHAR(10),GRNDATE,105) AS GRNDATE,PONO,CONVERT(VARCHAR(10),PODATE,105) AS PODATE   FROM GRN_TABLE  ", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grdporelese.SelectedIndex = 0;
                grdporelese.DataSource = dt;
                grdporelese.PageIndex = e.NewPageIndex;
                grdporelese.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grdporelese_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = grdporelese.DataKeys[e.NewSelectedIndex].Values["GRNNO"].ToString();
        Session["GRNRID"] = slno;
        Response.Redirect("~/STOREKEEPER/store_ViewPurchase.aspx");
    }
}