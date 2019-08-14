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

public partial class ACCOUNTS_DailyExpense : System.Web.UI.Page
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
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
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

        SqlDataAdapter Adp = new SqlDataAdapter("SELECT id,ExpType,Amount,Edate from tblDailyExpense order by id desc", con);
        DataTable Dt = new DataTable();
        Adp.Fill(Dt);
        GridView1.DataSource = Dt;
        GridView1.DataKeyNames = new string[] { "id" };
        GridView1.DataBind();
    }

    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        //using (SqlCommand cmd = new SqlCommand("Acct_DailyExpense", con))
        //{
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
        //    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
        //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        //    cmd.Parameters.Add("@ExpType", SqlDbType.VarChar).Value = txtexptype.Text;
        //    cmd.Parameters.Add("@Amount", SqlDbType.VarChar).Value = txtamount.Text;
        //    cmd.Parameters.Add("@EDate", SqlDbType.DateTime).Value = txtdate.Text;
        //    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
        //    cmd.ExecuteNonQuery();

        //    string message = "alert('Deleted Sucessfully.')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //    clearcontrol();
        //}

        string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();
        SqlCommand cm = new SqlCommand("delete from tblDailyExpense where id='" + slno + "'", con);
        cm.ExecuteNonQuery();

        binddata();
        con.Close();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[3].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand("SELECT id,ExpType,Amount,Edate from tblDailyExpense where id='" + slno + "'", con);
        dr = com.ExecuteReader();
        if (dr.Read())
        {
            btnSubmit.Visible = false;
            btnupdate.Visible = true;
            txtid.Text = dr["ID"].ToString();
            txtexptype.Text = dr["ExpType"].ToString();
            txtamount.Text = dr["Amount"].ToString();
        }
        dr.Close();
        con.Close();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("SELECT id,ExpType,Amount,Edate from tblDailyExpense order by id desc", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataKeyNames = new string[] { "id" };
        GridView1.DataBind();
        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            // Validation
            if (txtexptype.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtamount.Text = "0";
                return;
            }
            else if (txtamount.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtamount.Text = "0";
                return;
            }
            else if (Convert.ToDecimal(txtamount.Text) < 0)
            {
                string message = "alert('*Amounts can't be less than 0')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            using (SqlCommand cmd = new SqlCommand("Acct_DailyExpense", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@ExpType", SqlDbType.VarChar).Value = txtexptype.Text;
                cmd.Parameters.Add("@Amount", SqlDbType.VarChar).Value = txtamount.Text;
                cmd.Parameters.Add("@EDate", SqlDbType.DateTime).Value = txtdate.Text;
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
                cmd.ExecuteNonQuery();

                string message = "alert('Created Sucessfully.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                clearcontrol();
            }
            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        
    }
    public void clearcontrol()
    {
        txtexptype.Text = "";
        txtamount.Text = "";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            // Validation
            if (txtexptype.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtamount.Text = "0";
                return;
            }
            else if (txtamount.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtamount.Text = "0";
                return;
            }
            else if (Convert.ToDecimal(txtamount.Text) < 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("Acct_DailyExpense", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@ExpType", SqlDbType.VarChar).Value = txtexptype.Text;
                cmd.Parameters.Add("@Amount", SqlDbType.VarChar).Value = txtamount.Text;
                cmd.Parameters.Add("@EDate", SqlDbType.DateTime).Value = txtdate.Text;
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
                cmd.ExecuteNonQuery();

                string message = "alert('Updated Sucessfully.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                clearcontrol();
            }
            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/Accounts/DailyExpense.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        Response.Redirect("~/Accounts/DailyExpense.aspx");
    }
}