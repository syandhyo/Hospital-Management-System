using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;

public partial class RECEPTION_Medicine_Bill : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    string fr, to;
    string medbill, medcrvbill;
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from MEDPAYMENT_TABLE";
        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("PY{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        lblmid.Text = num1;
        dr.Close();
        con.Close();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this patient info ?');";
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
        lbluid.Text = Session["UID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm");
        binddata();
       // dr.Close();
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
        using (SqlCommand cmd = new SqlCommand("RECP_MEDICINE_BILL", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_MED_PAY_PAGE";
            cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID,DATE,PID,NAME,BEDNO,TOATALAMT,BALANCEAMT,AMOUNT FROM MEDPAYMENT_TABLE ORDER BY ID DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.SelectedIndex = 0;
            GridView1.DataSource = dt;
            GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();
        }
        //droppackage.Items.Insert(0, "-----Select------");
        con.Close();
    }
    public void clear_control()
    {
        lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        lblipno.Text = "";
        lblname.Text = "";
        lblbalance.Text = "0";
        lbltotalamt.Text = "0";
        txtamount.Text = "0";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        clear_control();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            if (TextBox1.Text == "")
            {
                string message = "alert('*Please!! Enter The Bed No..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            using (SqlCommand cmd = new SqlCommand("RECP_MEDICINE_BILL", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BEDNO";
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = TextBox1.Text;
                cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlCommand com = new SqlCommand("select * from BED_TABLE where BEDNO='" + TextBox1.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lblipno.Text = dr["VN"].ToString();
                    lblname.Text = dr["PNAME"].ToString();
                }
                else
                {
                    string message = "alert('*No Data Is There..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
            }
            dr.Close();
            using (SqlCommand com1 = new SqlCommand("RECP_MEDICINE_BILL", con))
            {
                com1.CommandType = CommandType.StoredProcedure;
                com1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_MED_VN";
                com1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                com1.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblipno.Text;
                //SqlCommand com1 = new SqlCommand("SELECT SUM(CHARGES)AS BAMT FROM PA_TRANS WHERE VN='" + lblipno.Text + "' AND DESCRIPTION like 'MEDICINE%'", con);
                dr = com1.ExecuteReader();
                if (dr.Read())
                {
                    medbill = dr["BAMT"].ToString();
                    lbltotalamt.Text = dr["BAMT"].ToString();

                }
            }
            dr.Close();

            using (SqlCommand com3 = new SqlCommand("RECP_MEDICINE_BILL", con))
            {
                com3.CommandType = CommandType.StoredProcedure;
                com3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_MEDPAY_VN";
                com3.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                com3.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblipno.Text;
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlCommand com3 = new SqlCommand("SELECT SUM(-CHARGES) AS AMOUNT FROM PA_TRANS WHERE VN='" + lblipno.Text + "'AND DESCRIPTION='MEDICINE PAYMENT' ", con);
                dr = com3.ExecuteReader();
                if (dr.Read())
                {
                    lblbalance.Text = dr["AMOUNT"].ToString();
                }
            }
           
            dr.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        con.Close();
    }
   
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand com3 = new SqlCommand("RECP_MEDICINE_BILL", con))
            {
                com3.CommandType = CommandType.StoredProcedure;
                com3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT";
                com3.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                com3.Parameters.Add("@VN", SqlDbType.VarChar).Value = slno;
                //SqlCommand com = new SqlCommand("SELECT ID,DATE,PID,NAME,BEDNO,TOATALAMT,BALANCEAMT,AMOUNT FROM MEDPAYMENT_TABLE WHERE ID='" + slno + "'", con);
                dr = com3.ExecuteReader();
                if (dr.Read())
                {
                    btnsave.Visible = false;
                    btndelete.Visible = true;
                    lblipno.Text = dr["PID"].ToString();
                    lbldate.Text = dr["DATE"].ToString();
                    lblmid.Text = dr["ID"].ToString();
                    TextBox1.Text = dr["BEDNO"].ToString();
                    lblname.Text = dr["NAME"].ToString();
                    lblbalance.Text = dr["BALANCEAMT"].ToString();
                    txtamount.Text = dr["AMOUNT"].ToString();
                    lbltotalamt.Text = dr["TOATALAMT"].ToString();
                    LBPAIDAMT.Text = dr["AMOUNT"].ToString();
                    lblmid.Visible = false;
                }
            }
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand("RECP_MEDICINE_BILL", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_MED_PAY_PAGE";
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlDataAdapter da = new SqlDataAdapter("SELECT ID,DATE,PID,NAME,BEDNO,TOATALAMT,BALANCEAMT,AMOUNT FROM MEDPAYMENT_TABLE ORDER BY ID DESC", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = dt;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (TextBox1.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (lblipno.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtamount.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (Convert.ToDecimal(txtamount.Text) <= 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (Convert.ToDecimal(lblbalance.Text) >= Convert.ToDecimal(lbltotalamt.Text))
            {
                string message = "alert('* Balance amount should not be greater than taotalamount.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand cm = new SqlCommand("RECP_MEDICINE_BILL_INSERT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                //SqlCommand cm = new SqlCommand("insert into MEDPAYMENT_TABLE(ID,DATE,PID,NAME,BEDNO,TOATALAMT,BALANCEAMT,AMOUNT,UID) VALUES (@ID,@DATE,@PID,@NAME,@BEDNO,@TOATALAMT,@BALANCEAMT,@AMOUNT,@UID)", con);
                cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblmid.Text;
                cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = lbldate.Text;
                cm.Parameters.Add("@NAME", SqlDbType.VarChar).Value = lblname.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblipno.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = TextBox1.Text;
                cm.Parameters.Add("@TOATALAMT", SqlDbType.VarChar).Value = lbltotalamt.Text;
                cm.Parameters.Add("@BALANCEAMT", SqlDbType.VarChar).Value = lblbalance.Text;
                cm.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = txtamount.Text;
                cm.Parameters.Add("@UID", SqlDbType.VarChar).Value = lblid.Text;
                cm.ExecuteNonQuery();
            }

            using (SqlCommand cm1 = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
            {
                cm1.CommandType = CommandType.StoredProcedure;
                cm1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cm1.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = lbldate.Text;
                cm1.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = lblmid.Text;
                cm1.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblipno.Text;
                cm1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = TextBox1.Text;
                cm1.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "MEDICINE PAYMENT";
                cm1.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = '-' + txtamount.Text;
                cm1.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
                cm1.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = '-' + txtamount.Text;
                cm1.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                cm1.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblipno.Text;
                cm1.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "MEDICINE CHARGES";
                cm1.ExecuteNonQuery();
            }
            using (SqlCommand cmd = new SqlCommand("usp_UserCollection", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = Session["UID"].ToString();
                cmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Now.ToString();
                cmd.Parameters.Add("@Collection", SqlDbType.Decimal).Value = Convert.ToDecimal(txtamount.Text).ToString() ;
                cmd.Parameters.Add("@Paid", SqlDbType.Decimal).Value = 0.00;
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblipno.Text;
                cmd.ExecuteNonQuery();
            }
            Session["mid"] = lblmid.Text;
            con.Close();
        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
            Response.Redirect("~/RECEPTION/MEDADbill.aspx");
          
        
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Session["HU"] = lblipno.Text;
        Response.Redirect("~/RECEPTION/medbill.aspx");
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            using (SqlCommand cmd = new SqlCommand("RECP_MEDICINE_BILL", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblmid.Text.ToString();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlDataAdapter da = new SqlDataAdapter("delete from MEDPAYMENT_TABLE where ID='" + lblmid.Text.ToString() + "'", con);
                ds = new DataSet();
                da.Fill(ds);
            }

            using (SqlCommand cm1 = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
            {
                cm1.CommandType = CommandType.StoredProcedure;
                cm1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                cm1.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = lbldate.Text;
                cm1.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = lblmid.Text;
                cm1.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblipno.Text;
                cm1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = TextBox1.Text;
                cm1.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "MEDICINE PAYMENT";
                cm1.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = '-' + LBPAIDAMT.Text;
                cm1.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
                cm1.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = '-' + LBPAIDAMT.Text;
                cm1.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                cm1.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblipno.Text;
                cm1.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "MEDICINE CHARGES";
                cm1.ExecuteNonQuery();
            }
            binddata();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        con.Close();
        Response.Redirect("~/RECEPTION/Medicine_Bill.aspx");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RECEPTION/Medicine_Bill.aspx");
    }
}