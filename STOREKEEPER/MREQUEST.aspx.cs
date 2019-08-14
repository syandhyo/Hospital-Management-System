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

public partial class STOREKEEPER_MREQUEST : System.Web.UI.Page
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


        //SqlDataAdapter da = new SqlDataAdapter("SELECT A.INVNO as ID,A.DATE AS DATE,B.DeptName AS DEPARTMENT FROM TBLMR A,tblDepartment B WHERE A.DEPTID=B.id and A.INVNO not in(select MRNO from MIN_TABLE) ORDER BY A.ID DESC", con);
        using (SqlCommand COM = new SqlCommand("SP_MREREQUEST", con))
        {
            COM.CommandType = CommandType.StoredProcedure;
            COM.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT";
            SqlDataAdapter da = new SqlDataAdapter(COM);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //if (dt.Rows.Count > 0 && dt.Rows.Count == null)
            //{
            //  GridView2.SelectedIndex = 0;
            GridView2.DataSource = dt;
            GridView2.DataKeyNames = new string[] { "ID" };
            GridView2.DataBind();
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //con.Open();
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    ImageButton img = (ImageButton)e.Row.FindControl("img_user");
        //    if (e.Row.Cells[4].Text == "Waiting")
        //    {

        //        img.ImageUrl = "~/gimg/Waiting.png";
        //    }
        //    else if (e.Row.Cells[4].Text == "Finished")
        //    {

        //        img.ImageUrl = "~/gimg/Finished.png";
        //    }
        //    else if (e.Row.Cells[4].Text == "Declined")
        //    {

        //        img.ImageUrl = "~/gimg/Declined.png";
        //    }

        //}
        //con.Close();

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
        if(!IsPostBack)
        {
            binddata();

        }        

        con.Close();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
           // SqlDataAdapter da = new SqlDataAdapter("SELECT A.INVNO as ID,A.DATE AS DATE,B.DeptName AS DEPARTMENT FROM TBLMR A,tblDepartment B WHERE A.DEPTID=B.id ORDER BY A.ID DESC", con);


            using (SqlCommand COM = new SqlCommand("SP_MREREQUEST", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT1";
                SqlDataAdapter da = new SqlDataAdapter(COM);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView2.SelectedIndex = 0;
                GridView2.DataSource = dt;
                GridView2.PageIndex = e.NewPageIndex;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
            }
            con.Close();
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
        
        Response.Redirect("~/STOREKEEPER/MIN.aspx");
    }
}