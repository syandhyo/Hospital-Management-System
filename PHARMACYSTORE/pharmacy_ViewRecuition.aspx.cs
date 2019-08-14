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

public partial class PHARMACYSTORE_pharmacy_ViewRecuition : System.Web.UI.Page
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
    DataMathods OBJ_METHOD = new DataMathods();
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
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }

            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("phrmc_RecutionLab", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                grdRecution.DataSource = Ds;
                grdRecution.DataKeyNames = new string[] { "INDENT_NO" };
                grdRecution.DataBind();
            }
            //using (SqlCommand cmd = new SqlCommand("phrmc_RecutionLab", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    // GridView1.SelectedIndex = 0;
            //    grdRecution.DataSource = dt;
            //    grdRecution.DataKeyNames = new string[] { "INDENT_NO" };
            //    grdRecution.DataBind();
            //}

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
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("phrmc_RecutionLab", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                grdRecution.DataSource = Ds;
                grdRecution.PageIndex = e.NewPageIndex;
                grdRecution.DataKeyNames = new string[] { "INDENT_NO" };
                grdRecution.DataBind();
            }
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //using (SqlCommand cmd = new SqlCommand("phrmc_supplMstSeldel", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INDEXCHG";
            //    cmd.Parameters.Add("@Branch_ID", SqlDbType.VarChar).Value = Session["Branch"];
            //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    adp.Fill(dt);

            //    grdRecution.DataSource = dt;
            //    grdRecution.DataKeyNames = new string[] { "INDENT_NO" };
            //    grdRecution.DataBind();
            //}

            //con.Close();
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
        Response.Redirect("~/PHARMACYSTORE/pharmacy_requisition_pharmacy.aspx");
    }
}