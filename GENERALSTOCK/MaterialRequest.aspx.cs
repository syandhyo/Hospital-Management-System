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
public partial class GENERALSTOCK_MaterialRequest : System.Web.UI.Page
{
    string num1 = "0";
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
                binddata();
                BindDepartment();
                binddYear();
                autoReq();
                GridShwAll();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    public void GridShwAll()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("DTSTOCK_MATREQUEST", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                //grdShowAll.SelectedIndex = 0;
                grdShowAll.DataSource = dt;
                //GridView1.DataKeyNames = new string[] { "ID" };
                grdShowAll.DataBind();
            }
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
    public void binddata()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[3] { new DataColumn("NAME"), new DataColumn("PUNIT"), new DataColumn("QTY") });
        ViewState["ITEM"] = dt;
        this.BindGrid();
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
    public void autoReq()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            string qry1 = "select max(ID) as ID from TBLMR";

            com = new SqlCommand(qry1, con);
            dr = null;

            dr = com.ExecuteReader();

            if (dr.Read() && dr["ID"].ToString() != "")
            {
                num1 = dr["ID"].ToString();
            }

            //num1 = string.Format("INV{0}", (Convert.ToUInt64(num1.Substring(3)) + 1).ToString("D4"));
            lblAuto.Text = "MR-" + num1 + 1 + "-" + lblfyear.Text;

            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void BindGrid()
    {
        try
        {
            grdMaterial.DataSource = (DataTable)ViewState["ITEM"];
            grdMaterial.DataBind();

        }
        catch
        {
        }
    }
   
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            DataTable dt = (DataTable)ViewState["ITEM"];
            dt.Rows.Add(txtMaterial.Text.Trim(), txtUnit.Text.Trim(), txtquantity.Text.Trim());
            ViewState["ITEM"] = dt;
            this.BindGrid();

            con.Close();
            txtMaterial.Text = "";
            txtquantity.Text = "";
            txtUnit.Text = "";            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
   
    protected void grdMaterial_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = (DataTable)ViewState["ITEM"];
            GridViewRow row = (GridViewRow)grdMaterial.Rows[e.RowIndex];

            Label name = (Label)row.FindControl("lbl_Name");
            Label unit = (Label)row.FindControl("lbl_Unit");
            Label quantity = (Label)row.FindControl("lbl_Quantity");

            string name1 = name.Text.ToString();
            string unit1 = unit.Text.ToString();
            string quantity1 = quantity.Text.ToString();

            dt.Rows[index].Delete();
            ViewState["ITEM"] = dt;
            //  lblgrandtotal.Text = (Convert.ToDecimal(lblgrandtotal.Text) - Convert.ToDecimal(TOTALAMT)).ToString();

            this.BindGrid();
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
                return;
            }          
            else if (grdMaterial.Rows.Count <= 0)
            {
                string message = "alert('*Add Item to Save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            autoReq();
            using (SqlCommand MR_cmd = new SqlCommand("USP_TBLMR", con))
            {
                MR_cmd.CommandType = CommandType.StoredProcedure;
                MR_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                MR_cmd.Parameters.Add("@ID", SqlDbType.Int).Value = 1;
                MR_cmd.Parameters.Add("@INVNO", SqlDbType.VarChar).Value = lblAuto.Text;
                MR_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                MR_cmd.Parameters.Add("@DEPTID", SqlDbType.VarChar).Value = ddDeptment.SelectedValue;
                MR_cmd.Parameters.Add("@FINANCEYR", SqlDbType.VarChar).Value = lblfyear.Text;
                MR_cmd.ExecuteNonQuery();

            }
            ItemMr();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        //string message = "alert('*Please select any test to save.')";
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        Response.Redirect("~/GENERALSTOCK/MaterialRequest.aspx");
    }
    public void ItemMr()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            foreach (GridViewRow gv1 in grdMaterial.Rows)
            {
                //var lbname = (gv1.FindControl("chkRow") as CheckBox);
                var nameGr = (gv1.FindControl("lbl_Name") as Label);
                var unitGr = (gv1.FindControl("lbl_Unit") as Label);
                var quantyGr = (gv1.FindControl("lbl_Quantity") as Label);
                using (SqlCommand MRitem_cmd = new SqlCommand("USP_TBLMRITEM", con))
                {
                    MRitem_cmd.CommandType = CommandType.StoredProcedure;
                    MRitem_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    MRitem_cmd.Parameters.Add("@ID", SqlDbType.Int).Value = 1;
                    MRitem_cmd.Parameters.Add("@DEPARTMENTID", SqlDbType.VarChar).Value = ddDeptment.SelectedValue;
                    MRitem_cmd.Parameters.Add("@INVNO", SqlDbType.VarChar).Value = lblAuto.Text;
                    MRitem_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = nameGr.Text.ToUpper();
                    MRitem_cmd.Parameters.Add("@QUANITY", SqlDbType.VarChar).Value = quantyGr.Text;
                    MRitem_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitGr.Text;


                    MRitem_cmd.ExecuteNonQuery();
                }
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void txtMaterial_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            //SqlCommand com = new SqlCommand("select * from TBL_DEPT_MAT_MST where ITEMNAME='" + txtMaterial.Text + "'", con);
            using (SqlCommand com = new SqlCommand("DTSTOCK_MATREQUEST", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "NMTEXTCHG";
                com.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtMaterial.Text;
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    txtUnit.Text = dr["UNIT"].ToString();
                    dr.Close();
                    txtUnit.Enabled = false;
                }
                else
                {
                    string message = "alert('*No Record Found ')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtUnit.Enabled = false;
                }
            }
            txtquantity.Text = "";
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/GENERALSTOCK/MaterialRequest.aspx");
    }
    protected void grdShowAll_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("DTSTOCK_MATREQUEST", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
               // grdShowAll.SelectedIndex = 0;
                grdShowAll.DataSource = dt;
                grdShowAll.PageIndex = e.NewPageIndex;
                //grdShowAll.DataKeyNames = new string[] { "ID" };
                grdShowAll.DataBind();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}