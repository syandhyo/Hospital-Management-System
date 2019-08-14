using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class RADIOLOGY_Stockrec2 : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    SqlCommand com, cmd, cmd1, cmd2, cmd3, cmd4;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
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
        //lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        if (IsPostBack != true)
        {
            //if (Ddldept.Items.Count == 0)
            //{


               
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "select distinct * from dbo.tblDepartment where DeptName='Radiology'";
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.Read() == true)
                {
                    Session["id"] = dr["ID"].ToString();
                }
                dr.Close();
                SqlDataAdapter Adp2 = new SqlDataAdapter("select SLNO as id,ITEMNAME from TBL_DEPT_MAT_STOCK ", con);
                DataTable Dt2 = new DataTable();
                Adp2.Fill(Dt2);
                Ddlitem.DataSource = Dt2;
                Ddlitem.DataTextField = "ITEMNAME";
                Ddlitem.DataValueField = "id";
                Ddlitem.DataBind();
                dr.Close();
                con.Close();

            }
        }


    protected void Ddlitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
          con.Open();
        cmd = con.CreateCommand();
        cmd.CommandText = "select distinct STOCK from TBL_DEPT_MAT_STOCK where ITEMNAME='" + Ddlitem.SelectedItem.Text + "'";
        dr = cmd.ExecuteReader();
        if (dr.Read() == true)
        {
            lblquant.Text = dr["STOCK"].ToString();
        }
        dr.Close();
        con.Close();
}
}