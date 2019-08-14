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

public partial class STOREKEEPER_PORELEASER : System.Web.UI.Page
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

        using (SqlCommand cm = new SqlCommand("STORE_PO_RELEASER", con))
        {
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            SqlDataAdapter da = new SqlDataAdapter(cm);
            //SqlDataAdapter da = new SqlDataAdapter("SELECT PONO,REFNO,DATEOFISSUE FROM PO_TABLE  ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //if (dt.Rows.Count > 0 && dt.Rows.Count == null)
            //{
            grdporelese.SelectedIndex = 0;
            grdporelese.DataSource = dt;
            //  grdporelese.DataKeyNames = new string[] { "ID" };
            grdporelese.DataBind();
        }
        //  }
        //else
        //{
        //    Response.Write(@"<script language='javascript'>alert('No Record Found !')</script>");
        //    GridView2.DataSource = dt;
        //    // grdDeptgrn.DataKeyNames = new string[] { "ID" };
        //    GridView2.DataBind();
        //    // string message = "alert('*Please select Department.')";
        //    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //}
        // GridView2.Columns[04].Visible = false;
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
            using (SqlCommand cm = new SqlCommand("STORE_PO_RELEASER", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                SqlDataAdapter da = new SqlDataAdapter(cm);
                //SqlDataAdapter da = new SqlDataAdapter("SELECT PONO,REFNO,DATEOFISSUE FROM PO_TABLE ", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                // grdporelese.SelectedIndex = 0;
                grdporelese.DataSource = dt;
                grdporelese.PageIndex = e.NewPageIndex;
                // grdporelese.DataKeyNames = new string[] { "PONO" };
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
        var slno = grdporelese.DataKeys[e.NewSelectedIndex].Values["PONO"].ToString();
        Session["POID"] = slno;

        Response.Redirect("~/STOREKEEPER/ViewPoOrder.aspx");
    }
}