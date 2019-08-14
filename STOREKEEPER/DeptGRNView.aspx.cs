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

public partial class STOREKEEPER_DeptGRNView : System.Web.UI.Page
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
        if(!IsPostBack)
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

        //SqlDataAdapter da = new SqlDataAdapter("SELECT a.ID,a.MRNDATE,a.RETURN_TO,a.RECBY,b.id,b.DeptName FROM MRN_STORE_TABLE a,tblDepartment b where a.RETURN_TO=b.id and a.AUTOID not in(select MRNO from DEPT_GRNST_TBL) order by a.ID desc", con);
        using (SqlCommand cmd = new SqlCommand("Sp_DeptGrN", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grdDeptgrn.SelectedIndex = 0;
            grdDeptgrn.DataSource = dt;
            grdDeptgrn.DataKeyNames = new string[] { "ID" };
            grdDeptgrn.DataBind();
        }
        //grdDeptgrn.Columns[04].Visible = false;
        con.Close();
    }
    protected void grdDeptgrn_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = grdDeptgrn.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
        Session["ID"] = slno;
        Response.Redirect("~/STOREKEEPER/DeptGRNStock.aspx");
    }
     protected void grdDeptgrn_PageIndexChanging(object sender, GridViewPageEventArgs e)
     {
         try
         {
             SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
             con.Open();
             //SqlDataAdapter da = new SqlDataAdapter("SELECT a.ID,a.MRNDATE,a.RETURN_TO,a.RECBY,b.id,b.DeptName FROM MRN_STORE_TABLE a, tblDepartment b where a.RETURN_TO=b.id and a.AUTOID not in(select MRNO from DEPT_GRNST_TBL) order by a.ID desc", con);

             using (SqlCommand cmd = new SqlCommand("Sp_DeptGrN", con))
             {
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH";
                 SqlDataAdapter da = new SqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 da.Fill(dt);
                 grdDeptgrn.SelectedIndex = 0;
                 grdDeptgrn.DataSource = dt;
                 grdDeptgrn.PageIndex = e.NewPageIndex;
                 grdDeptgrn.DataKeyNames = new string[] { "ID" };
                 grdDeptgrn.DataBind();
             }
             con.Close();
         }
         catch (Exception ex)
         {
             Console.WriteLine("An error occurred: '{0}'", ex);
         }
     }
}