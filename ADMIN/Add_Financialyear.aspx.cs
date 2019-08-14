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
public partial class ADMIN_Add_Financialyear : System.Web.UI.Page
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
    string id, id1, ph, val1, val2, p, q, des1, name, k, PIN;
    public string id_hist;
    public double amt = 0, qty = 0, amt1 = 0, t_amt = 0, qt = 0, c_rs = 0, avg = 0;
    decimal a, b, c, d, e, f, g, h, j;
    DateTime DT;
    double totalamt1, totamt, totalgstamt, dis, dism;
    GridViewRow gr;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

   
    public void binddata()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand COM = new SqlCommand("FINANCIAL_YEAR", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                SqlDataReader dr = COM.ExecuteReader();
                if (dr.Read())
                {
                    lblfyear.Text = dr["FYEAR"].ToString();
                    Label1.Text = dr["FYEAR"].ToString();
                }
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }


     
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("ADMIN_Add_Financialyear_uspFinancialYearAdd", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                // cmd.Parameters.Add("@ID", SqlDbType.Int).Value = txtid.Text;
                cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = DropDownList1.Text;
                cmd.ExecuteNonQuery();
            }
            binddata();
            con.Close();
            
             string message = "alert('Year Saved Successfully.')";
             ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
             return;
         
            //SqlDataAdapter da1 = new SqlDataAdapter("delete from FYEAR1_TABLE  WHERE ORGID='" + lblorgid.Text + "'", con);
            //DataSet ds1 = new DataSet();
            //da1.Fill(ds1);

            //SqlDataAdapter da = new SqlDataAdapter("insert into FYEAR1_TABLE values('" + lblorgid.Text + "','" + DropDownList1.Text + "')", con);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("ADMIN_Add_Financialyear_uspFinancialYearAdd", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                // cmd.Parameters.Add("@ID", SqlDbType.Int).Value = txtid.Text;
                cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = DropDownList1.Text;
                cmd.ExecuteNonQuery();
            }
            binddata();
            con.Close();
           
                string message = "alert('Year Changed Successfully.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            
            //SqlDataAdapter da1 = new SqlDataAdapter("delete from FYEAR1_TABLE  WHERE ORGID='" + lblorgid.Text + "'", con);
            //DataSet ds1 = new DataSet();
            //da1.Fill(ds1);

            //SqlDataAdapter da = new SqlDataAdapter("insert into FYEAR1_TABLE values('" + lblorgid.Text + "','" + DropDownList1.Text + "')", con);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
  
    
}