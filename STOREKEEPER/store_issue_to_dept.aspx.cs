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

public partial class STOREKEEPER_store_issue_to_dept : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    DataMathods OBJ_METHOD = new DataMathods();
    GridViewRow gr;
    public void binddata()
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }
            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("SP_MREREQUEST", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = Ds;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        //SqlDataAdapter da = new SqlDataAdapter("SELECT A.INVNO as ID,A.DATE AS DATE,B.DeptName AS DEPARTMENT FROM TBLMR A,tblDepartment B WHERE A.DEPTID=B.id and A.INVNO not in(select MRNO from MIN_TABLE) ORDER BY A.ID DESC", con);
        //using (SqlCommand COM = new SqlCommand("SP_MREREQUEST", con))
        //{
        //    COM.CommandType = CommandType.StoredProcedure;
        //    COM.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT";
        //    SqlDataAdapter da = new SqlDataAdapter(COM);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    //if (dt.Rows.Count > 0 && dt.Rows.Count == null)
        //    //{
        //    //  GridView2.SelectedIndex = 0;
        //    GridView2.DataSource = dt;
        //    GridView2.DataKeyNames = new string[] { "ID" };
        //    GridView2.DataBind();
        //}
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        binddata();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       

    }
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
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("SP_MREREQUEST", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = Ds;
                GridView2.PageIndex = e.NewPageIndex;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
            }
            //using (SqlCommand COM = new SqlCommand("SP_MREREQUEST", con))
            //{
            //    COM.CommandType = CommandType.StoredProcedure;
            //    COM.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT1";
            //    SqlDataAdapter da = new SqlDataAdapter(COM);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    GridView2.SelectedIndex = 0;
            //    GridView2.DataSource = dt;
            //    GridView2.PageIndex = e.NewPageIndex;
            //    GridView2.DataKeyNames = new string[] { "ID" };
            //    GridView2.DataBind();
            //}
            //con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
        Session["MRINDID"] = slno;

        Response.Redirect("~/STOREKEEPER/store_MIN.aspx");
    }
}