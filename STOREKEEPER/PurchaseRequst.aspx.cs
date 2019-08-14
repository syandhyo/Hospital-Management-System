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

public partial class STOREKEEPER_PurchaseRequst : System.Web.UI.Page
{
    string num1 = "PR000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
    //decimal total_amt = 0;
    //decimal total_vat_amt = 0;
    //int i, no, no1, sl;
    //GridViewRow gr;

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
            binddata();
            BindDepartment();
            BindItem();
            autoReq();
        }
    }
    public void BindDepartment()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlDataAdapter Adp = new SqlDataAdapter("select id,DeptName from tblDepartment", con);
        DataTable Dt = new DataTable();
        Adp.Fill(Dt);
        ddDeptment.DataSource = Dt;
        ddDeptment.DataTextField = "DeptName";
        ddDeptment.DataValueField = "id";
        ddDeptment.DataBind();
        ddDeptment.Items.Insert(0, "Please Select");
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
    public void autoReq()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select max(PRID) as ID from DEPT_PUR_REQ";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        if (dr.Read() && dr["ID"].ToString() != "")
        {
            num1 = dr["ID"].ToString();
        }

        num1 = string.Format("PR{0}", (Convert.ToUInt64(num1.Substring(3)) + 1).ToString("D4"));
        lblAuto.Text = num1;
        dr.Close();
        con.Close();
    }
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("select dp.ID,dp.MID,dp.DEPTId,dp.DATE,d.DeptName,m.NAME from DEPT_PUR_REQ  dp , tblDepartment d , MATERIAL_TABLE  m where dp.MID=m.slno and dp.DEPTId=d.id  ORDER BY ID DESC", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.SelectedIndex = 0;
        GridView1.DataSource = dt;
        //GridView1.DataKeyNames = new string[] { "ID" };
        GridView1.DataBind();
        con.Close();
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        autoReq();
        using (SqlCommand cm = new SqlCommand("USP_DEPT_PUR_REQ", con))
        {

            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            cm.Parameters.Add("@ID", SqlDbType.Int).Value =1;
            cm.Parameters.Add("@PRID", SqlDbType.VarChar).Value = lblAuto.Text;
            cm.Parameters.Add("@MID", SqlDbType.VarChar).Value = ddItem.SelectedValue;
            cm.Parameters.Add("@DEPTId", SqlDbType.VarChar).Value = ddDeptment.SelectedValue;
            cm.Parameters.Add("@QTY", SqlDbType.VarChar).Value = txtQuantity.Text;
            cm.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            cm.Parameters.Add("@ORGID", SqlDbType.VarChar).Value =lblorgid.Text;
           
            cm.ExecuteNonQuery();
            string message1 = "alert('Successfully Inserted.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        binddata();
        clear();

        con.Close();

    }
    public void clear()
    {
        txtdate.Text = "";
        txtQuantity.Text = "";
        ddDeptment.SelectedIndex = 0;
        ddItem.SelectedIndex = 0;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PurchaseRequst.aspx");
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand("select ID,PRID,MID,DEPTId,QTY,DATE,ORGID from DEPT_PUR_REQ  where ID='" + slno + "'", con);
        dr = com.ExecuteReader();
        if (dr.Read())
        {
            btncreate.Visible = false;
            btnupdate.Visible = true;
            lblUpId.Text = slno.ToString();
            ddDeptment.Text = dr["DEPTId"].ToString();
            ddItem.Text = dr["MID"].ToString();
            txtdate.Text = dr["DATE"].ToString();
            txtQuantity.Text = dr["QTY"].ToString();
           
            //droptesttype.SelectedItem.Text = dr["TESTTYPE"].ToString();
           // droptesttype.SelectedValue = dr["TESTINDEX"].ToString();
            //droptesttype.SelectedItem.Text=
        }
        dr.Close();
        con.Close();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        autoReq();
        using (SqlCommand cm = new SqlCommand("USP_DEPT_PUR_REQ", con))
        {

            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            cm.Parameters.Add("@ID", SqlDbType.Int).Value = lblUpId.Text;
            cm.Parameters.Add("@PRID", SqlDbType.VarChar).Value = lblAuto.Text;
            cm.Parameters.Add("@MID", SqlDbType.VarChar).Value = ddItem.SelectedValue;
            cm.Parameters.Add("@DEPTId", SqlDbType.VarChar).Value = ddDeptment.SelectedValue;
            cm.Parameters.Add("@QTY", SqlDbType.VarChar).Value = txtQuantity.Text;
            cm.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            cm.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;

            cm.ExecuteNonQuery();
            string message1 = "alert('Successfully Updated.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        binddata();
        clear();

        con.Close();
    }
}