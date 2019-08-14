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
            BindRecept();
            SqlDataAdapter Adp = new SqlDataAdapter("select VNO from MISBILL_TABLE", con);
            DataTable Dt = new DataTable();
            Adp.Fill(Dt);
            dropbillno.DataSource = Dt;
            dropbillno.DataTextField = "VNO";
            dropbillno.DataValueField = "VNO";
            dropbillno.DataBind();
            dropbillno.Items.Insert(0, "Please Select");
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
    }
    public void BindRecept()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand cmd = new SqlCommand("USP_EMPVENDOR", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            //  cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            ddlRecipent.DataSource = dt;
            ddlRecipent.DataTextField = "NAME";
            ddlRecipent.DataValueField = "ID";
            ddlRecipent.DataBind();
            ddlRecipent.Items.Insert(0, "Please Select");
        }

        con.Close();

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
            SqlCommand cmd = new SqlCommand("insert into ADV_MISBILL_TABLE(DATE,REC,DESCR,PMODE,FYEAR,ADVAMOUNT,VNO,BILLAMT)values(@DATE,@REC,@DESCR,@PMODE,@FYEAR,@ADVAMOUNT,@VNO,@BILLAMT)", con);
            cmd.Parameters.Add("@DATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@REC", SqlDbType.VarChar).Value = ddlRecipent.SelectedValue;
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
        Response.Redirect("~/ACCOUNTS/Advancepaymentmiscbill.aspx");
    }
    protected void dropbillno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand cm = new SqlCommand("select * FROM MISBILL_TABLE WHERE VNO='" + dropbillno.SelectedValue + "'", con);
            dr = cm.ExecuteReader();
            if (dr.Read())
            {
                txtbillamt.Text = dr["AMOUNT"].ToString();

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