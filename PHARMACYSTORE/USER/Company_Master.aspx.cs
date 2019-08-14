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
public partial class ADMIN_PHARMACYSTORE_Company_Master : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr;
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
       // lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        lblid.Text = Session["NAME"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        if (!IsPostBack)
        {
            binddata();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[1].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    public void binddata()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME FROM COMPANY_TABLE WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            using (SqlCommand cmd = new SqlCommand("phrmc_CompnyMstSeldel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                // GridView1.SelectedIndex = 0;
                GridView1.DataSource = dt;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            //---------------------------
            using (SqlCommand COM = new SqlCommand("FINANCIAL_YEAR", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                SqlDataReader dr = COM.ExecuteReader();
                if (dr.Read())
                {
                    lblfyear.Text = dr["FYEAR"].ToString();
                }
                dr.Close();
            }
            con.Close();
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
            if (txtname.Text == "")
            {
                string message = "alert('* Please Enter Party Name')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            SqlCommand com = new SqlCommand("select * from COMPANY_TABLE where NAME='" + txtname.Text.ToUpper() + "' and ORGID='" + lblorgid.Text + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                string message = "alert('" + txtname.Text + " Already Exist.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            dr.Close();

            using (SqlCommand cmd1 = new SqlCommand("phrmc_CompnyMstInsUp", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.ExecuteNonQuery();
            }
            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
            Response.Redirect("~/PHARMACYSTORE/USER/Company_Master.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtname.Text == "")
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('* Fields are mandatory.')</script>");

                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            //SqlCommand com = new SqlCommand("select * from COMPANY_TABLE where NAME='" + txtname.Text.ToUpper() + "' and ORGID='" + lblorgid.Text + "'", con);
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    Response.Write("<script LANGUAGE='JavaScript' >alert('Alredy Exist.')</script>");
            //    return;
            //}
            //dr.Close();

            using (SqlCommand cmd1 = new SqlCommand("phrmc_CompnyMstInsUp", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.ExecuteNonQuery();
            }
            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/PHARMACYSTORE/USER/Company_Master.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        Response.Redirect("~/PHARMACYSTORE/USER/Company_Master.aspx");
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("phrmc_CompnyMstSeldel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = dr["ID"].ToString();
                    txtname.Text = dr["NAME"].ToString();
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
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
            using (SqlCommand cmd = new SqlCommand("phrmc_CompnyMstSeldel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                cmd.ExecuteNonQuery();
            }
            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/PHARMACYSTORE/USER/Company_Master.aspx");
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            using (SqlCommand cmd = new SqlCommand("phrmc_CompnyMstSeldel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INDEXCHG";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                // GridView1.SelectedIndex = 0;
                GridView1.DataSource = dt;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Btnsearch_click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME FROM COMPANY_TABLE WHERE NAME LIKE '" + txtsearchname.Text + "%' AND ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);

            using (SqlCommand cmd = new SqlCommand("phrmc_CompnyMstSeldel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SEARCH";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtsearchname.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                //GridView1.SelectedIndex = 0;
                GridView1.DataSource = dt;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}