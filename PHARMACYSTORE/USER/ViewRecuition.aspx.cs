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

public partial class PHARMACYSTORE_USER_ViewRecuition : System.Web.UI.Page
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
        try
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
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void binddata()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("phrmc_RecutionLab", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                // GridView1.SelectedIndex = 0;
                grdRecution.DataSource = dt;
                grdRecution.DataKeyNames = new string[] { "INDENT_NO" };
                grdRecution.DataBind();
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
    protected void grdRecution_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("phrmc_supplMstSeldel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INDEXCHG";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                grdRecution.DataSource = dt;
                grdRecution.DataKeyNames = new string[] { "INDENT_NO" };
                grdRecution.DataBind();
            }

            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grdRecution_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = grdRecution.DataKeys[e.NewSelectedIndex].Values["INDENT_NO"].ToString();
        Session["RECTIONID"] = slno;
        Response.Redirect("~/PHARMACYSTORE/USER/SaleRecution.aspx");
    }
}