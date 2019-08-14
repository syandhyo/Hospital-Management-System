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
using System.Web.Services;
public partial class RECEPTION_FinalPayment : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9, cmd10, cmd11, cmd12, cmd13, cmd14, cmd15, cmd16;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    int i, no, no1, sl, F;
    int flag = 0, stock = 0, stock_tran = 0, trans = 0, stock1 = 0, S, CLOSEING, slno, CLOSE;
    string id, id1, ph, val1, AMOUNT, p, q, des1, name, k, PIN,DATE;
    public string id_hist;
    public double amt = 0, qty = 0, amt1 = 0, t_amt = 0, qt = 0, c_rs = 0, avg = 0;
    decimal a, b, c, d, e, f, g, h, j;
    DateTime DT;
    double totalamt1, totamt, totalgstamt, dis, dism;
    GridViewRow gr;
    [WebMethod]
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from DISCHARGE_PAYMENT";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("DI{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        TXTID.Text = num1;

        dr.Close();
        con.Close();
    }
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand COM = new SqlCommand("SELECT FYEAR FROM FYEAR1_TABLE WHERE  ORGID='" + lblorgid.Text + "'", con);
        dr = COM.ExecuteReader();
        if (dr.Read())
        {
            //Label1.Text = dr["FYEAR"].ToString();
            lblfyear.Text = dr["FYEAR"].ToString();
        }
        dr.Close();
        SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,CONVERT(VARCHAR(10),DATE,105) AS DATE,PID AS PID,NAME AS NAME,BEDNO AS BEDNO FROM DISCHARGE_PAYMENT ORDER BY ID DESC", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.SelectedIndex = 0;
        GridView1.DataSource = dt;
        GridView1.DataKeyNames = new string[] { "ID" };
        GridView1.DataBind();
        con.Close();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
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
        lbluid.Text = Session["UID"].ToString();
        DATE = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        if (!IsPostBack)
        {
            binddata();
            da1 = new SqlDataAdapter("select BEDNO FROM BED_TABLE where ORGID='" + lblorgid.Text + "'", con);
            DataTable ds1 = new DataTable();
            da1.Fill(ds1);
            dropbedno.DataSource = ds1;
            dropbedno.DataTextField = "BEDNO";
            dropbedno.DataValueField = "BEDNO";
            dropbedno.DataBind();
            dropbedno.Items.Insert(0, "-----Select-----");
        }
        con.Close();
    }
   
    protected void dropbedno_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand cm = new SqlCommand("select * FROM BED_TABLE WHERE BEDNO='" + dropbedno.SelectedItem.Text + "'", con);
        dr = cm.ExecuteReader();
        if (dr.Read())
        {
            lblpid.Text = dr["VN"].ToString();
            lblpname.Text = dr["PNAME"].ToString();
            lblbedno.Text = dr["BEDNO"].ToString();
        }
        dr.Close();
        SqlCommand cm1 = new SqlCommand("SELECT  DEBIT,CREDIT FROM PA_MASTER WHERE VN='"+lblpid.Text+"'", con);
        dr = cm1.ExecuteReader();
        if (dr.Read())
        {
            lbltotalamt.Text = dr["CREDIT"].ToString();
            lblpaidamt.Text = dr["DEBIT"].ToString();
            lblremainamt.Text = (Convert.ToDouble(lbltotalamt.Text) - Convert.ToDouble(lblpaidamt.Text)).ToString();
            txtamount.Text = lblremainamt.Text;
        }
        dr.Close();
         if (Convert.ToDouble(txtamount.Text) < 0)
        {

            txtamount.Text = "0";
        }
        con.Close();

    }
    protected void txtdisc_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtamount.Text = (Convert.ToDouble(txtamount.Text) - Convert.ToDouble(txtdisc.Text)).ToString();
        }
        catch
        {
            Response.Write("<script LANGUAGE='JavaScript' >alert('* INCORRECT DISCOUNT FORMAT.')</script>");
            return;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (lblpid.Text == "")
        {

            string message = "alert('* Fields are mandatory.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        else if (Convert.ToDouble( txtamount.Text)<0)
        {

           txtamount.Text="0";
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        auto();
        try
        {
            SqlCommand cmd1 = new SqlCommand("insert into DISCHARGE_PAYMENT (ID,PID,NAME,BEDNO,DATE,TOTAL,PAID,REMAIN,INSURANCE,INSNO,DISCAMT,AMOUNT,ORGID,USERNAME,USERID)values(@ID,@PID,@NAME,@BEDNO,@DATE,@TOTAL,@PAID,@REMAIN,@INSURANCE,@INSNO,@DISCAMT,@AMOUNT,@ORGID,@USERNAME,@USERID)", con);
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblpid.Text;
            cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = lblpname.Text.ToUpper();
            cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbedno.Text;
            cmd1.Parameters.Add("@DATE", SqlDbType.DateTime).Value = DATE;
            cmd1.Parameters.Add("@TOTAL", SqlDbType.VarChar).Value = lbltotalamt.Text;
            cmd1.Parameters.Add("@PAID", SqlDbType.VarChar).Value = lblpaidamt.Text;
            cmd1.Parameters.Add("@REMAIN", SqlDbType.VarChar).Value = lblremainamt.Text;
            cmd1.Parameters.Add("@INSURANCE", SqlDbType.VarChar).Value = dropinsurance.Text;
            cmd1.Parameters.Add("@INSNO", SqlDbType.VarChar).Value = Txtinsuranceno.Text;
            cmd1.Parameters.Add("@DISCAMT", SqlDbType.VarChar).Value = txtdisc.Text;
            cmd1.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = txtamount.Text;
            cmd1.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = lblid.Text;
            cmd1.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lbluid.Text;
            cmd1.ExecuteNonQuery();
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = DATE;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblpid.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbedno.Text;
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "DISCHARGE PAYMENT";
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtamount.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = txtamount;
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblpid.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "ADVANCE DETAILS";
                cm.ExecuteNonQuery();
            }

            binddata();
            Session["PAYMENT"] = lblpid.Text;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/RECEPTION/Finalbill_reciept.aspx");
        con.Close();
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand("select * from DISCHARGE_PAYMENT where ID='" + slno + "'", con);
        dr = com.ExecuteReader();
        if (dr.Read())
        {
            TXTID.Text = dr["ID"].ToString();
            btncreate.Visible = false;
            btnupdate.Visible = true;
            lblpid.Text = dr["PID"].ToString();
            lblpname.Text = dr["NAME"].ToString();
            lblbedno.Text = dr["BEDNO"].ToString();
            lbltotalamt.Text = dr["TOTAL"].ToString();
            lblpaidamt.Text = dr["PAID"].ToString();
            lblremainamt.Text = dr["REMAIN"].ToString();
            Txtinsuranceno.Text = dr["INSNO"].ToString();
            txtdisc.Text = dr["DISCAMT"].ToString();
            txtamount.Text = dr["AMOUNT"].ToString();
            AMOUNT = dr["AMOUNT"].ToString();
            dropinsurance.Text = dr["INSURANCE"].ToString();
        }
        dr.Close();
        con.Close();
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
        SqlDataAdapter da1 = new SqlDataAdapter("delete from DISCHARGE_PAYMENT where ID='" + slno + "'", con);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        try
        {
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = DATE;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = slno;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblpid.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbedno.Text;
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PAYMENT";
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtamount.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = txtamount.Text;
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblpid.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "ADVANCE DETAILS";
                cm.ExecuteNonQuery();
            }
            binddata();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        con.Close();
        Response.Redirect("~/RECEPTION/FinalPayment.aspx");
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,CONVERT(VARCHAR(10),DATE,105) AS DATE,PID AS PID,NAME AS NAME,BEDNO AS BEDNO FROM DISCHARGE_PAYMENT ORDER BY ID DESC", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.SelectedIndex = 0;
        GridView1.DataSource = dt;
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataKeyNames = new string[] { "ID" };
        GridView1.DataBind();
        con.Close();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (lblpid.Text == "")
        {

            string message = "alert('* Fields are mandatory.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        else if (Convert.ToDouble(txtamount.Text) < 0)
        {

            txtamount.Text = "0";
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            SqlCommand cmd1 = new SqlCommand("UPDATE DISCHARGE_PAYMENT SET USERNAME=@USERNAME,USERID=@USERID,PID=@PID,NAME=@NAME,BEDNO=@BEDNO,DATE=@DATE,TOTAL=@TOTAL,PAID=@PAID,REMAIN=@REMAIN,INSURANCE=@INSURANCE,INSNO=@INSNO,DISCAMT=@DISCAMT,AMOUNT=@AMOUNT WHERE ORGID=@ORGID AND ID=@ID", con);
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblpid.Text;
            cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = lblpname.Text.ToUpper();
            cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbedno.Text;
            cmd1.Parameters.Add("@DATE", SqlDbType.DateTime).Value = DATE;
            cmd1.Parameters.Add("@TOTAL", SqlDbType.VarChar).Value = lbltotalamt.Text;
            cmd1.Parameters.Add("@PAID", SqlDbType.VarChar).Value = lblpaidamt.Text;
            cmd1.Parameters.Add("@REMAIN", SqlDbType.VarChar).Value = lblremainamt.Text;
            cmd1.Parameters.Add("@INSURANCE", SqlDbType.VarChar).Value = dropinsurance.Text;
            cmd1.Parameters.Add("@INSNO", SqlDbType.VarChar).Value = Txtinsuranceno.Text;
            cmd1.Parameters.Add("@DISCAMT", SqlDbType.VarChar).Value = txtdisc.Text;
            cmd1.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = txtamount.Text;
            cmd1.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = lblid.Text;
            cmd1.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lbluid.Text;
            cmd1.ExecuteNonQuery();
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE1";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = DATE;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblpid.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbedno.Text;
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "DISCHARGE PAYMENT";
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = AMOUNT;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = AMOUNT;
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblpid.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "ADVANCE DETAILS";
                cm.ExecuteNonQuery();
            }
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = DATE;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblpid.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbedno.Text;
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "DISCHARGE PAYMENT";
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtamount.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = txtamount;
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblpid.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "ADVANCE DETAILS";
                cm.ExecuteNonQuery();
            }
            binddata();
            Session["PAYMENT"] = lblpid.Text;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/RECEPTION/Finalbill_reciept.aspx");
        con.Close();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RECEPTION/FinalPayment.aspx");
    }
}