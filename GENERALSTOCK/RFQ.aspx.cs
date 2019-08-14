using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;

public partial class GENERALSTOCK_RFQ : System.Web.UI.Page
{
    string num1 = "PR000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
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
           // binddata();
            //BindDepartment();
            BindItem();
            //autoReq();
            BindVendor();
        }
    }
    //public void binddata()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //    con.Open();
    //    SqlDataAdapter da = new SqlDataAdapter("select * from RFQ_TABLE", con);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    GridView1.SelectedIndex = 0;
    //    GridView1.DataSource = dt;
    //    //GridView1.DataKeyNames = new string[] { "ID" };
    //    GridView1.DataBind();
    //    con.Close();
    //}
    public void autoReq()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select max(RFQID) as ID from RFQ_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        if (dr.Read() && dr["ID"].ToString() != "")
        {
            num1 = dr["ID"].ToString();

        }

        num1 = string.Format("RFQ{0}", (Convert.ToUInt64(num1.Substring(3)) + 1).ToString("D4"));
        lblAuto.Text = num1;
        dr.Close();
        con.Close();
    }
    public void BindItem()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlDataAdapter Adp = new SqlDataAdapter("select slno,NAME from MATERIAL_TABLE", con);
        DataTable Dt = new DataTable();
        Adp.Fill(Dt);
        ddItem.DataSource = Dt;
        ddItem.DataTextField = "NAME";
        ddItem.DataValueField = "slno";
        ddItem.DataBind();
        ddItem.Items.Insert(0, "Please Select");
        con.Close();
    }

    public void BindVendor()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlDataAdapter Adp = new SqlDataAdapter("select ID,NAME from VENDER_TABLE", con);
        DataTable Dt = new DataTable();
        Adp.Fill(Dt);
        dropvendor.DataSource = Dt;
        dropvendor.DataTextField = "NAME";
        dropvendor.DataValueField = "ID";
        dropvendor.DataBind();
        dropvendor.Items.Insert(0, "Please Select");
        con.Close();
    }

    protected void btncreate_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        autoReq();
        using (SqlCommand cm = new SqlCommand("USP_RFQ", con))
        {

            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            cm.Parameters.Add("@ID", SqlDbType.Int).Value = "1";
            cm.Parameters.Add("@RFQID", SqlDbType.VarChar).Value = lblAuto.Text;
            cm.Parameters.Add("@MID", SqlDbType.VarChar).Value = ddItem.SelectedValue;
            cm.Parameters.Add("@QTY", SqlDbType.VarChar).Value = txtQuantity.Text;
            cm.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            cm.Parameters.Add("@VENDORID", SqlDbType.VarChar).Value = dropvendor.SelectedValue;

            cm.ExecuteNonQuery();
            string message1 = "alert('Inserted Successfully.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        clear();
      //  binddata();
        con.Close();

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        autoReq();
        using (SqlCommand cm = new SqlCommand("USP_RFQ", con))
        {

            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            cm.Parameters.Add("@ID", SqlDbType.Int).Value = "1";
            cm.Parameters.Add("@RFQID", SqlDbType.VarChar).Value = lblAuto.Text;
            cm.Parameters.Add("@MID", SqlDbType.VarChar).Value = ddItem.SelectedValue;
            cm.Parameters.Add("@QTY", SqlDbType.VarChar).Value = txtQuantity.Text;
            cm.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            cm.Parameters.Add("@VENDORID", SqlDbType.VarChar).Value = dropvendor.SelectedValue;

            cm.ExecuteNonQuery();
            string message1 = "alert('Successfully Updated.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        clear();
     //   binddata();
        con.Close();
    }
    public void clear()
    {
        txtdate.Text = "";
        txtQuantity.Text = "";
        dropvendor.SelectedIndex = 0;
        ddItem.SelectedIndex = 0;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("RFQ.aspx");
    }
}