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
public partial class PHARMACYSTORE_USER_IndentRequest : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9, cmd10, cmd11, cmd12, cmd13, cmd14, cmd15, cmd16;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
 
    GridViewRow gr;
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand COM = new SqlCommand("SELECT FYEAR FROM FYEAR1_TABLE WHERE  ORGID='" + lblorgid.Text + "'", con);
        dr = COM.ExecuteReader();
        if (dr.Read())
        {
            lblfyear.Text = dr["FYEAR"].ToString();
        }
        dr.Close();
        SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,WARD,DATE AS DATE,PID,BEDNO,STATUS FROM MEDICINE_STATUS_TABLE WHERE  ORGID='" + lblorgid.Text + "'ORDER BY slno DESC", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView2.SelectedIndex = 0;
        GridView2.DataSource = dt;
        GridView2.DataKeyNames = new string[] { "ID" };
        GridView2.DataBind();
       // GridView2.Columns[04].Visible = false;
        con.Close();
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        binddata();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton img = (ImageButton)e.Row.FindControl("img_user");
            if (e.Row.Cells[4].Text == "Waiting")
            {

                img.ImageUrl = "~/gimg/Waiting.png";
            }
            else if (e.Row.Cells[4].Text == "Finished")
            {

                img.ImageUrl = "~/gimg/Finished.png";
            }
            else if (e.Row.Cells[4].Text == "Declined")
            {

                img.ImageUrl = "~/gimg/Declined.png";
            }

        }
        con.Close();

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
        binddata();
       
        con.Close();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,WARD,DATE AS DATE,PID,BEDNO,STATUS FROM MEDICINE_STATUS_TABLE WHERE  ORGID='" + lblorgid.Text + "'ORDER BY slno DESC", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView2.SelectedIndex = 0;
        GridView2.DataSource = dt;
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataKeyNames = new string[] { "ID" };
        GridView2.DataBind();

    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
        Session["INDID"] = slno;
        Response.Redirect("~/PHARMACYSTORE/USER/PharmacyIndent_View.aspx");
      
    }
}