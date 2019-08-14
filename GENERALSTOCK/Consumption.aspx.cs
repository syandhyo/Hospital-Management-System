using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Web.Services;

public partial class GENERALSTOCK_Consumption : System.Web.UI.Page
{
    string num1 = "0";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    GridViewRow gr;
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
    public void auto()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            string qry1 = "select ID AS ID from CONSUMPTION_TBL";

            com = new SqlCommand(qry1, con);
            dr = null;

            dr = com.ExecuteReader();

            while (dr.Read())
            {
                num1 = dr["ID"].ToString();
            }
            //num1 = string.Format("PU{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
            txtContrtno.Text = "CM-" + num1 + 1 + "-" + lblfyear.Text;
            dr.Close();
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

            da1 = new SqlDataAdapter("select id,DeptName from tblDepartment ", con);
            DataTable ds1 = new DataTable();
            da1.Fill(ds1);
            ddDepartment.DataSource = ds1;
            ddDepartment.DataTextField = "DeptName";
            ddDepartment.DataValueField = "id";
            ddDepartment.DataBind();
            ddDepartment.Items.Insert(0, "Please Select");

           // SqlDataAdapter da = new SqlDataAdapter("select * from CONSUMPTION_TBL", con);
            using (SqlCommand cmd = new SqlCommand("DTSTOCK_CONSUMPTION", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
               // grdConsumtion.SelectedIndex = 0;
                grdConsumtion.DataSource = dt;
                //grdConsumtion.DataKeyNames = new string[] { "ID" };
                grdConsumtion.DataBind();
            }
            con.Close();
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
            // lblorgid.Text = Session["ORGID"].ToString();

            if (!IsPostBack)
            {
                binddata();
                bindTGrid();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void bindTGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("NAME"), new DataColumn("UNIT"), new DataColumn("QTY") });
            ViewState["ITEM"] = dt;
            this.BindGrid();
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
       // btncreate.Visible = true;
       // btncancel.Visible = true;
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
            Label quantity = (Label)row.FindControl("lbl_Qty");

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
    protected void txtMaterial_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            //SqlCommand com = new SqlCommand("select ID, UNIT,NAME from MATERIAL_MASTER_TABLE where NAME='" + txtMaterial.Text + "'", con);
            using (SqlCommand cmd = new SqlCommand("DTSTOCK_CONSUMPTION", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "NMTEXTCHG";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtUnit.Text = dr["UNIT"].ToString();
                }
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
            if (ddDepartment.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Department..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (grdMaterial.Rows.Count <= 0)
            {

                string message = "alert('*Add Material to Save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand CM_cmd = new SqlCommand("USP_CONSUMPTION", con))
            {
                CM_cmd.CommandType = CommandType.StoredProcedure;
                CM_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";

                CM_cmd.Parameters.Add("@CONSPMNID", SqlDbType.VarChar).Value = txtContrtno.Text;
                CM_cmd.Parameters.Add("@DEPTNAME", SqlDbType.VarChar).Value = ddDepartment.SelectedValue;
                CM_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                CM_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                CM_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "";
                CM_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
                CM_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = 0.00;

                CM_cmd.ExecuteNonQuery();
            }
            ItemCM();
            string message1 = "alert('Inserted Successfully.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/GENERALSTOCK/Consumption.aspx");
    }
    public void ItemCM()
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
                var quantyGr = (gv1.FindControl("lbl_Qty") as Label);

                using (SqlCommand MRitem_cmd = new SqlCommand("USP_CONSUMPTION", con))
                {
                    MRitem_cmd.CommandType = CommandType.StoredProcedure;
                    MRitem_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
                    MRitem_cmd.Parameters.Add("@CONSPMNID", SqlDbType.VarChar).Value = txtContrtno.Text;
                    MRitem_cmd.Parameters.Add("@DEPTNAME", SqlDbType.VarChar).Value = ddDepartment.SelectedValue;
                    MRitem_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                    MRitem_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                    MRitem_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = nameGr.Text;
                    MRitem_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitGr.Text;
                    MRitem_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = quantyGr.Text;
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

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/GENERALSTOCK/Consumption.aspx");
    }
    protected void grdConsumtion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            // SqlDataAdapter Adp = new SqlDataAdapter("select * from CONSUMPTION_TBL", con); 
            using (SqlCommand cmd = new SqlCommand("DTSTOCK_CONSUMPTION", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INDEXING";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                SqlDataAdapter Adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                Adp.Fill(dt);
                grdConsumtion.DataSource = dt;
                grdConsumtion.PageIndex = e.NewPageIndex;
                grdConsumtion.DataKeyNames = new string[] { "ID" };
                grdConsumtion.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}