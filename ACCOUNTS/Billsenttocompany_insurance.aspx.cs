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

public partial class ACCOUNTS_Billsenttocompany_insurance : System.Web.UI.Page
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


        binddata();


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
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand com = new SqlCommand("select A.CREDIT,A.VN,B.NAME AS NAME,B.INSURANCENO,B.INSURANCENAME from PA_MASTER A,ADMISSION_TABLE B where A.VN=B.VN AND A.VN='" + Txtno.Text + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                Txtno.Text = dr["VN"].ToString();
                Txtname.Text = dr["NAME"].ToString();
                txtins.Text = dr["INSURANCENAME"].ToString();
                txtamount.Text = dr["CREDIT"].ToString();
                txtinsno.Text = dr["INSURANCENO"].ToString();
            }
            else
            {
                string message1 = "alert('Invalid UHCNO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
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
            if (txtdate.Text == "")
            {
                string message = "alert('* Date is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (Txtno.Text == "")
            {
                string message = "alert('* UHCNO is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (Txtname.Text == "")
            {
                string message = "alert('* UHCNO is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtinsno.Text == "")
            {
                string message = "alert('* UHCNO is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtins.Text == "")
            {
                string message = "alert('* COMPANY is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtinsno.Text == "")
            {
                string message = "alert('* INSURANCENO is mandatory.')";
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
            else if (Convert.ToDecimal(txtamount.Text) <= 0)
            {
                string message = "alert('* Amount is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtamount.Text = "0.00";
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into INSBILL_TABLE(DATE,UHCNO,NAME,FYEAR,AMOUNT,INS,INSNO)values(@DATE,@UHCNO,@NAME,@FYEAR,@AMOUNT,@INS,@INSNO)", con);
            cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@UHCNO", SqlDbType.VarChar).Value = Txtno.Text;
            cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = txtamount.Text;
            cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = Txtname.Text;
            cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            cmd.Parameters.Add("@INS", SqlDbType.VarChar).Value = txtins.Text;
            cmd.Parameters.Add("@INSNO", SqlDbType.VarChar).Value = txtinsno.Text;
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
        Response.Redirect("~/ACCOUNTS/Billsenttocompany_insurance.aspx");
    }
}