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

public partial class GENERALSTOCK_deptstock_dept_material_master : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;

    [WebMethod]
    public static string[] GetCustomers(string prefix)
    {
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select DISTINCT NAME from MATERIAL_MASTER_TABLE where NAME like @SearchText+'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = con;

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["NAME"]));
                    }
                }
                con.Close();
            }
        }
        return customers.ToArray();

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
                auto();
                binddata();
                BindDepartment();
                binddYear();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void auto()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            string qry1 = "select ID from TBL_DEPT_MAT_MST";

            com = new SqlCommand(qry1, con);
            dr = null;

            dr = com.ExecuteReader();

            while (dr.Read())
            {
                num1 = dr["ID"].ToString();
            }
            num1 = string.Format("DP{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
            lblAuto.Text = num1;

            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void binddYear()
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
           
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void BindDepartment()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlDataAdapter Adp = new SqlDataAdapter("select id,DeptName from tblDepartment", con);
            DataTable Dt = new DataTable();
            Adp.Fill(Dt);
            ddDeptment.DataSource = Dt;
            ddDeptment.DataTextField = "DeptName";
            ddDeptment.DataValueField = "id";
            ddDeptment.DataBind();
            ddDeptment.Items.Insert(0, "Please Select");
            con.Close();
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
            
            using (SqlCommand cmd = new SqlCommand("DTSTOCK_MATMASTER", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                
                grdMaterial.DataSource = dt;
                grdMaterial.DataBind();
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
            if (ddDeptment.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Department..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                ddDeptment.Focus();
                return;
            }
            else if (txtName.Text == "")
            {
                string message = "alert('Please!! Enter the Item Name.. ')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtName.Focus();
                return;
            }
            if (txtQuantity.Text == "")
            {
                string message = "alert('Please!! Enter The Quantity..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtQuantity.Focus();
                return;
            }
            else if (txtUnit.Text == "")
            {
                string message = "alert('Please!! Enter the Unit.. ')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtUnit.Focus();
                return;
            }
            if (DDType.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Type..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                DDType.Focus();
                return;
            }
            else if (txtdate.Text == "")
            {
                string message = "alert('Please!! Enter the Date.. ')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdate.Focus();
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand dept_cmd = new SqlCommand("USP_DEPT_MAT_MST", con))
            {
                dept_cmd.CommandType = CommandType.StoredProcedure;
                dept_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                dept_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblAuto.Text;
                // dept_cmd.Parameters.Add("@DEPTNO", SqlDbType.VarChar).Value = ;
                dept_cmd.Parameters.Add("@DEPTNAME", SqlDbType.VarChar).Value = ddDeptment.SelectedValue;
                dept_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = txtName.Text;
                dept_cmd.Parameters.Add("@QUANTITY", SqlDbType.VarChar).Value = txtQuantity.Text;
                dept_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = txtUnit.Text.ToUpper();
                dept_cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = DDType.SelectedItem.Text;
                dept_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                dept_cmd.Parameters.Add("@FINANCEYR", SqlDbType.VarChar).Value = lblfyear.Text;

                dept_cmd.Parameters.Add("@STOCK", SqlDbType.VarChar).Value = txtQuantity.Text;
                dept_cmd.Parameters.Add("@OPENING", SqlDbType.Decimal).Value = txtQuantity.Text;
                dept_cmd.Parameters.Add("@GBST", SqlDbType.Decimal).Value = "0.00";
                dept_cmd.Parameters.Add("@GRST", SqlDbType.Decimal).Value = "0.00";
                dept_cmd.Parameters.Add("@ISSBDT", SqlDbType.Decimal).Value = "0.00";
                dept_cmd.Parameters.Add("@ISSRDT", SqlDbType.Decimal).Value = "0.00";
                dept_cmd.Parameters.Add("@CLOSING", SqlDbType.Decimal).Value = txtQuantity.Text;

                dept_cmd.ExecuteNonQuery();

            }
            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/GENERALSTOCK/DeptMaterialMST.aspx");
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/GENERALSTOCK/DeptMaterialMST.aspx");
    }
    protected void grdMaterial_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void txtName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            // SqlCommand com = new SqlCommand("select * from MATERIAL_MASTER_TABLE where NAME='" + txtName.Text + "'", con);
            // dr = com.ExecuteReader();
            using (SqlCommand com = new SqlCommand("DTSTOCK_MATMASTER", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "NMTEXTCHG";
                com.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtName.Text;
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    txtUnit.Text = dr["UNIT"].ToString();
                    dr.Close();
                }
                else
                {
                    string message = "alert('*No Record Found ')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    //txtUnit.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grdMaterial_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("DTSTOCK_MATMASTER", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                // grdMaterial.SelectedIndex = 0;
                grdMaterial.DataSource = dt;
                grdMaterial.PageIndex = e.NewPageIndex;
                //grdMaterial.DataKeyNames = new string[] { "ID" };
                grdMaterial.DataBind();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}