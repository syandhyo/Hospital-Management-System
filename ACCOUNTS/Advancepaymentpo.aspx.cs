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

public partial class ACCOUNTS_Advancepaymentmiscbill : System.Web.UI.Page
{
    SqlDataReader dr;
    SqlConnection con;
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


        if (!IsPostBack)
        {
            binddata();

            SqlDataAdapter Adp = new SqlDataAdapter("select PONO from PO_TABLE", con);
            DataTable Dt = new DataTable();
            Adp.Fill(Dt);
            dropbillno.DataSource = Dt;
            dropbillno.DataTextField = "PONO";
            dropbillno.DataValueField = "PONO";
            dropbillno.DataBind();
            dropbillno.Items.Insert(0, "Please Select");
            SqlDataAdapter Adp1 = new SqlDataAdapter("select * from VENDER_MASTER_TABLE", con);
            DataTable Dt1 = new DataTable();
            Adp1.Fill(Dt1);
            dropvendor.DataSource = Dt1;
            dropvendor.DataTextField = "NAME1";
            dropvendor.DataValueField = "ID";
            dropvendor.DataBind();
            dropvendor.Items.Insert(0, "Please Select");
        }


        con.Close();
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");

    }
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

        //SqlDataAdapter Adp = new SqlDataAdapter("SELECT id,ExpType,Amount,Edate from tblDailyExpense", con);
        //DataTable Dt = new DataTable();
        //Adp.Fill(Dt);
        //GridView1.DataSource = Dt;
        //GridView1.DataKeyNames = new string[] { "id" };
        //GridView1.DataBind();
    }
    protected void Btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdate.Text == "")
            {
                string message = "alert('* Date is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtdesc.Text == "")
            {
                string message = "alert('* Description is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtamount.Text == "")
            {
                string message = "alert('* Amount is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtamount.Text = "0.00";
                return;
            }
            else if (Convert.ToDecimal(txtamount.Text) < 0)
            {
                string message = "alert('* Amount is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtamount.Text = "0.00";
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into ADV_PO_TABLE(DATE,REC,DESCR,PMODE,FYEAR,ADVAMOUNT,PONO,BILLAMT)values(@DATE,@REC,@DESCR,@PMODE,@FYEAR,@ADVAMOUNT,@VNO,@BILLAMT)", con);
            cmd.Parameters.Add("@DATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@REC", SqlDbType.VarChar).Value = dropvendor.SelectedValue;
            cmd.Parameters.Add("@ADVAMOUNT", SqlDbType.Decimal).Value = txtamount.Text;
            cmd.Parameters.Add("@BILLAMT", SqlDbType.Decimal).Value = txtbillamt.Text;
            cmd.Parameters.Add("@DESCR", SqlDbType.VarChar).Value = txtdesc.Text;
            cmd.Parameters.Add("@PMODE", SqlDbType.VarChar).Value = DropDownList2.Text;
            cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            cmd.Parameters.Add("@VNO", SqlDbType.VarChar).Value = dropbillno.Text;
            cmd.ExecuteNonQuery();
            binddata();
            con.Close();
            string message1 = "alert('Successfully Saved.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/ACCOUNTS/Advancepaymentpo.aspx");
    }
    protected void dropbillno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand cm = new SqlCommand("select * FROM PO_TABLE WHERE PONO='" + dropbillno.SelectedValue + "'", con);
            dr = cm.ExecuteReader();
            if (dr.Read())
            {
                txtbillamt.Text = dr["GRANDTOTAL"].ToString();

            }
            dr.Close();

            con.Close();
        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
}