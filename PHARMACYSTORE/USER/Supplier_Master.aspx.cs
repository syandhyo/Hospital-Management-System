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

public partial class ADMIN_PHARMACYSTORE_Supplier_Master : System.Web.UI.Page
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
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from SUPPLIER_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("SU{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;

        dr.Close();
        con.Close();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try{
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[8].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                }
                dr.Close();
            }
            using (SqlCommand cmd = new SqlCommand("phrmc_supplGrShow", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    public void clear_control()
    {
        txtid.Text = "";
        txtname.Text = "";
        txtaddress.Text = "";
        txtcity.Text = "";
        txtcontactno.Text = "";
        txtgstno.Text = "";
        txtopening.Text = "0";
        txtpin.Text = "";
        txtstate.Text = "";
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
            using (SqlCommand cmd = new SqlCommand("phrmc_supplMstSeldel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
                cmd.ExecuteNonQuery();
            }
            binddata();
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("phrmc_supplMstSeldel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INDEXCHG";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

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
            else if (txtgstno.Text == "")
            {
                string message = "alert('* *Please Enter GST No')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtstatecode.Text == "")
            {
                string message = "alert('* Please Enter State Code')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand com = new SqlCommand("select * from SUPPLIER_TABLE where GST='" + txtgstno.Text + "' AND  ORGID='" + lblorgid.Text + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                string message = "alert('* Party Name " + txtgstno.Text + " Already Exist.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            dr.Close();

            auto();
            using (SqlCommand cmd1 = new SqlCommand("phrmc_supplMstInsUp", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
                cmd1.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtcity.Text;
                cmd1.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
                cmd1.Parameters.Add("@CONTACT", SqlDbType.VarChar).Value = txtcontactno.Text;
                cmd1.Parameters.Add("@GST", SqlDbType.VarChar).Value = txtgstno.Text;
                cmd1.Parameters.Add("@OPENING", SqlDbType.Decimal).Value ="0";
                cmd1.Parameters.Add("@PIN", SqlDbType.VarChar).Value = txtpin.Text;
                cmd1.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = txtaddress.Text;

                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtstatecode.Text;
               
                cmd1.ExecuteNonQuery();
            }
            string message1 = "alert('Created Successfully.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            binddata();
            con.Close();           
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/PHARMACYSTORE/USER/Supplier_Master.aspx");
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("phrmc_supplMstSeldel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = dr["ID"].ToString();
                    txtname.Text = dr["NAME"].ToString();
                    txtcity.Text = dr["CITY"].ToString();
                    txtstate.Text = dr["STATE"].ToString();
                    txtcontactno.Text = dr["CONTACT"].ToString();
                    txtgstno.Text = dr["GST"].ToString();
                    txtpin.Text = dr["PIN"].ToString();
                    txtaddress.Text = dr["ADDRESS"].ToString();
                    txtstatecode.Text = dr["STATECODE"].ToString();
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('* Please Enter Party Name')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtgstno.Text == "")
            {
                string message = "alert('* *Please Enter GST No')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtstatecode.Text == "")
            {
                string message = "alert('* Please Enter State Code')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            using (SqlCommand cmd1 = new SqlCommand("phrmc_supplMstInsUp", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
                cmd1.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtcity.Text;
                cmd1.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
                cmd1.Parameters.Add("@CONTACT", SqlDbType.VarChar).Value = txtcontactno.Text;
                cmd1.Parameters.Add("@GST", SqlDbType.VarChar).Value = txtgstno.Text;
                cmd1.Parameters.Add("@OPENING", SqlDbType.Decimal).Value = "0";
                cmd1.Parameters.Add("@PIN", SqlDbType.VarChar).Value = txtpin.Text;
                cmd1.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = txtaddress.Text;
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtstatecode.Text;
                cmd1.ExecuteNonQuery();
            }
            binddata();
            con.Close();
            //  Response.Redirect("~/ADMIN/PHARMACYSTORE/Supplier_Master.aspx");
            //Response.Redirect("~/LMS/PartyMaster.aspx");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/PHARMACYSTORE/USER/Supplier_Master.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/ADMIN/PHARMACYSTORE/Supplier_Master.aspx");
        //Response.Redirect("~/LMS/PartyMaster.aspx");
        Response.Redirect("~/PHARMACYSTORE/USER/Supplier_Master.aspx");
    }
}