using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class ACCOUNTS_account_referral_settlement : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr, dr1;
    SqlDataAdapter da;
    DataTable dt;
    decimal total_amt = 0;
    decimal total_vat_amt = 0;
    int i, no, no1, sl;
    GridViewRow gr;

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
        }
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
    }
    public void auto()
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con1.Open();
        string qry1 = "select ID AS slno  from REFFEAL_SATELMENT_TBL order by slno DESC";
        com = new SqlCommand(qry1, con1);
        dr = null;
        dr = com.ExecuteReader();
        string str1 = "1";
        if (dr.Read())
        {
            num1 = dr["slno"].ToString();
            //string str = num1.Substring(0, num1.Length - 0);//delete last 10 record
            string d = num1.Substring(3);//delete first 3 record
            str1 = (Convert.ToInt64(d) + 1).ToString();//add in numbers
        }
        txtid.Text = "RSM" + str1;
        dr.Close();
        con1.Close();
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

        da = new SqlDataAdapter("select ID,NAME FROM Broker_Table", con);
        DataTable ds = new DataTable();
        da.Fill(ds);
        droprefname.DataSource = ds;
        droprefname.DataTextField = "NAME";
        droprefname.DataValueField = "ID";
        droprefname.DataBind();
        droprefname.Items.Insert(0, "Please Select");

        SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM  REFFEAL_SATELMENT_TBL ORDER BY ID DESC", con);
        DataTable dt = new DataTable();
        da1.Fill(dt);

        grdReffsatel.DataSource = dt;
        grdReffsatel.DataKeyNames = new string[] { "ID" };
        grdReffsatel.DataBind();

        con.Close();
    }
    protected void droprefname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select a.NAME,CAST(((b.PCHARGE)/100)*b.PP AS INT) as pchgDsc,CAST(((b.LCHARGE)/100)*b.LP AS INT) as lpchgDsc,CAST(((b.BCHARGE)/100)*b.BP AS INT) as bpchgDsc,CAST(((b.RCHARGE)/100)*b.RP AS INT) as RpchgDsc,CAST(((b.MISCHARGE)/100)*b.MP AS INT) as MpchgDsc,CAST((((b.PCHARGE/100)*b.PP)+((b.LCHARGE/100)*b.LP)+((b.BCHARGE/100)*b.BP)+((b.MISCHARGE/100)*b.MP))+((b.RCHARGE/100)*b.RP)AS INT) as totamt from  Broker_Table a,REF_TRAN b where a.ID=convert(int,b.REF) and a.NAME='" + droprefname.SelectedItem.Text + "'", con);
            da.Fill(dt);
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                GridView1.DataBind();
                divtotamt.Visible = true;
                decimal sum = 0;
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    Label totamt = (gr.Cells[5].FindControl("lbltotamt") as Label);
                    // string totamt = GridView1.Rows[gr.RowIndex].Cells[5].Text;
                    sum += Convert.ToDecimal(totamt.Text);
                }
                lbltotamt.Text = Convert.ToString(sum);
            }
            else
            {
                string message = "alert('* No Record Found !')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (droprefname.SelectedIndex == 0)
            {
                string message = "alert('* Refferal Name is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtdate.Text == "")
            {
                string message = "alert('* Date is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            auto();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cm = new SqlCommand("REFFRAL_SATELMENT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cm.Parameters.Add("@REFFNAME", SqlDbType.VarChar).Value = droprefname.SelectedValue;
                cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                cm.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = lbltotamt.Text;
                cm.Parameters.Add("@SATELAMOUNT", SqlDbType.Decimal).Value = txtsatelment.Text;
                cm.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                cm.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@PCHARGE", SqlDbType.Decimal).Value = "0";
                cm.Parameters.Add("@LCHARGE", SqlDbType.Decimal).Value = "0";
                cm.Parameters.Add("@BCHARGE", SqlDbType.Decimal).Value = "0";
                cm.Parameters.Add("@TOTAMTITEM", SqlDbType.Decimal).Value = "0";
                cm.ExecuteNonQuery();
            }
            con.Close();
            refferalItem();
            string message1 = "alert('* Saved Successfully.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/Accounts/account_referral_settlement.aspx");
    }
    //----------------------------------
    public void refferalItem()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        foreach (GridViewRow row in GridView1.Rows)
        {
            // CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
            Label nameIt = (row.Cells[1].FindControl("lblname") as Label);
            Label pchgIt = (row.Cells[2].FindControl("lblpchg") as Label);
            Label ichgIt = (row.Cells[3].FindControl("lblichg") as Label);
            Label bchgIt = (row.Cells[4].FindControl("lblbchg") as Label);
            Label totamtIt = (row.Cells[5].FindControl("lbltotamt") as Label);

            using (SqlCommand cm1 = new SqlCommand("REFFRAL_SATELMENT", con))
            {
                cm1.CommandType = CommandType.StoredProcedure;
                cm1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
                cm1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cm1.Parameters.Add("@REFFNAME", SqlDbType.VarChar).Value = "";
                cm1.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                cm1.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = "0";
                cm1.Parameters.Add("@SATELAMOUNT", SqlDbType.Decimal).Value = "0";
                cm1.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                cm1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = nameIt.Text;
                cm1.Parameters.Add("@PCHARGE", SqlDbType.Decimal).Value = pchgIt.Text;
                cm1.Parameters.Add("@LCHARGE", SqlDbType.Decimal).Value = ichgIt.Text;
                cm1.Parameters.Add("@BCHARGE", SqlDbType.Decimal).Value = bchgIt.Text;
                cm1.Parameters.Add("@TOTAMTITEM", SqlDbType.Decimal).Value = totamtIt.Text;
                cm1.ExecuteNonQuery();
            }
        }
        con.Close();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounts/account_referral_settlement.aspx");
    }
}