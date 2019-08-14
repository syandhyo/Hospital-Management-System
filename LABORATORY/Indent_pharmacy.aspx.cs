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

public partial class LABORATORY_Indent_pharmacy : System.Web.UI.Page
{
    string num1 = "SJ000";
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
            using (SqlCommand cmd = new SqlCommand("LAB_INDENT_PHARMACY", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_STOCKTBL";
                cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = prefix;
                cmd.Parameters.Add("@INDENT_NO", SqlDbType.VarChar).Value = "NULL";
                //cmd.Parameters.AddWithValue("@SearchText", prefix);
                //cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                //cmd.CommandText = "select DISTINCT NAME from STOCK_TABLE where NAME like @SearchText+'%'";
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
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from INDENT_TO_PHARMACY";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }        
        txtindentno.Text = "IND-" + num1 + "-" + lblfyear.Text;
        dr.Close();
        con.Close();
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
        using (SqlCommand cmd = new SqlCommand("LAB_INDENT_PHARMACY", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DEPT";
            cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@INDENT_NO", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter Adp2 = new SqlDataAdapter(cmd);
            //SqlDataAdapter Adp2 = new SqlDataAdapter("select * from tblDepartment", con);
            DataTable Dt2 = new DataTable();
            Adp2.Fill(Dt2);
            dropdept.DataSource = Dt2;
            dropdept.DataTextField = "DeptName";
            dropdept.DataValueField = "DeptName";
            dropdept.DataBind();
            dropdept.Items.Insert(0, "---Please Select---");
        }
        using (SqlCommand cmd1 = new SqlCommand("LAB_INDENT_PHARMACY", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_INDT_PHRMA_PAGE";
            cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@INDENT_NO", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
            //SqlDataAdapter Adp = new SqlDataAdapter("select * from INDENT_TO_PHARMACY Order by ID desc", con);
            DataTable Dt = new DataTable();
            Adp.Fill(Dt);
            GridView1.DataSource = Dt;
            GridView1.DataBind();
        }
        con.Close();
    }
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

        if (!IsPostBack)
        {
            binddata();
            bindTGrid();            
        }
        con.Close();
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
    }
    public void bindTGrid()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[3] { new DataColumn("NAME"), new DataColumn("UNIT"), new DataColumn("QTY") });
        ViewState["ITEM"] = dt;
        this.BindGrid();
    }
    protected void BindGrid()
    {
        try
        {
            grdMaterial.DataSource = (DataTable)ViewState["ITEM"];
            grdMaterial.DataBind();

        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            if (dropdept.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtipno.Text == "")
            {
                string message = "alert('Please!! Enter The IP Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtipno.Text != "")
            {
                using (SqlCommand cmd1 = new SqlCommand("SP_PATIENT_SEARCH", con))
                {
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISCHARGE";
                    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtipno.Text;
                    cmd1.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = "NULL";
                    cmd1.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = "NULL";
                    cmd1.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
                    cmd1.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtipno.Text;
                    cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.Read())
                    {
                        string message = "alert('* This Pateint Is Discharged..')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                        return;
                    }
                    else
                    {
                        dr1.Close();
                        using (SqlCommand cmd = new SqlCommand("SP_PATIENT_SEARCH", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ADMISSION_VN";
                            cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                            cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = "NULL";
                            cmd.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = "NULL";
                            cmd.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
                            cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtipno.Text;
                            cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                            dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {

                            }
                            else
                            {
                                string message = "alert('Ip Number Is not Valid..')";
                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                                return;
                            }
                        }

                    }
                }
            }
            else if (txtenterby.Text == "")
            {
                string message = "alert('Enter By Field Is Mandatory..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (grdMaterial.Rows.Count <= 0)
            {

                string message = "alert('*Add items To save..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            
            auto();
            using (SqlCommand cmd = new SqlCommand("USP_INDENT_PHARMACY", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                //cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd.Parameters.Add("@INDENT_NO", SqlDbType.VarChar).Value = txtindentno.Text;
                cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = dropdept.SelectedItem.Text;
                cmd.Parameters.Add("@ENTER_BY", SqlDbType.VarChar).Value = txtenterby.Text;
                cmd.Parameters.Add("@AUTHORISED_BY", SqlDbType.VarChar).Value = txtauthorised.Text;
                cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = txtipno.Text.ToUpper();
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
                cmd.ExecuteNonQuery();
            }

            ItemCM();
            con.Close();
            string message1 = "alert('Successfully Created.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/LABORATORY/Indent_pharmacy.aspx");
    }
    public void ItemCM()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        foreach (GridViewRow gv1 in grdMaterial.Rows)
        {
            //var lbname = (gv1.FindControl("chkRow") as CheckBox);
            var nameGr = (gv1.FindControl("lbl_Name") as Label);
            var unitGr = (gv1.FindControl("lbl_Unit") as Label);
            var quantyGr = (gv1.FindControl("lbl_Qty") as Label);

            using (SqlCommand cmd1 = new SqlCommand("USP_INDENT_PHARMACY", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
               // cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd1.Parameters.Add("@INDENT_NO", SqlDbType.VarChar).Value = txtindentno.Text;
                cmd1.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                cmd1.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = dropdept.SelectedItem.Text;
                cmd1.Parameters.Add("@ENTER_BY", SqlDbType.VarChar).Value = txtenterby.Text;
                cmd1.Parameters.Add("@AUTHORISED_BY", SqlDbType.VarChar).Value = txtauthorised.Text;
                cmd1.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = txtipno.Text;
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = nameGr.Text;                
                cmd1.Parameters.Add("@QTY", SqlDbType.Decimal).Value = quantyGr.Text;
                cmd1.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitGr.Text;

                cmd1.ExecuteNonQuery();
            }
        }
        con.Close();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            // Validation
            if (dropdept.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtauthorised.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtenterby.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (grdMaterial.Rows.Count <= 0)
            {

                string message = "alert('*Add item.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            using (SqlCommand cmd = new SqlCommand("USP_INDENT_PHARMACY", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                //cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd.Parameters.Add("@INDENT_NO", SqlDbType.VarChar).Value = lblEditgrd.Text;
                cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = dropdept.SelectedItem.Text;
                cmd.Parameters.Add("@ENTER_BY", SqlDbType.VarChar).Value = txtenterby.Text;
                cmd.Parameters.Add("@AUTHORISED_BY", SqlDbType.VarChar).Value = txtauthorised.Text;
                cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = txtipno.Text;

                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
                cmd.ExecuteNonQuery();
            }
            binddata();
            con.Close();
            //clearcontrol();
            string message1 = "alert('Successfully Updated.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/LABORATORY/Indent_pharmacy.aspx");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/LABORATORY/Indent_pharmacy.aspx");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('*Add item.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtqty.Text == "")
            {
                string message = "alert('*Add Qty.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            DataTable dt = (DataTable)ViewState["ITEM"];
            dt.Rows.Add(txtname.Text.Trim(), txtUnit.Text.Trim(), txtqty.Text.Trim());
            ViewState["ITEM"] = dt;
            this.BindGrid();

            con.Close();
            txtname.Text = "";
            txtqty.Text = "";
            txtUnit.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

        //btncreate.Visible = true;
        //btncancel.Visible = true;
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            string slno = GridView1.DataKeys[e.RowIndex].Values["INDENT_NO"].ToString();
            using (SqlCommand cm = new SqlCommand("LAB_INDENT_PHARMACY", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                cm.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "NULL";
                cm.Parameters.Add("@INDENT_NO", SqlDbType.VarChar).Value = slno;
                //SqlCommand cm = new SqlCommand("delete from INDENT_TO_PHARMACY where INDENT_NO='" + slno + "'", con);
                cm.ExecuteNonQuery();
            }

            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["INDENT_NO"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cm = new SqlCommand("LAB_INDENT_PHARMACY", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT";
                cm.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "NULL";
                cm.Parameters.Add("@INDENT_NO", SqlDbType.VarChar).Value = slno;
                //SqlCommand com = new SqlCommand("SELECT * from INDENT_TO_PHARMACY where INDENT_NO='" + slno + "'", con);
                dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    lblEditgrd.Text = slno.ToString();
                    txtid.Text = dr["INDENT_NO"].ToString();
                    txtdate.Text = dr["DATE"].ToString();
                    dropdept.SelectedValue = dr["DEPARTMENT"].ToString();
                    txtenterby.Text = dr["ENTER_BY"].ToString();
                    txtauthorised.Text = dr["AUTHORISED_BY"].ToString();
                    txtipno.Text = dr["IPNO"].ToString();
                }
                dr.Close();
                using (SqlCommand cmd = new SqlCommand("LAB_INDENT_PHARMACY", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT_OR";
                    cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "NULL";
                    cmd.Parameters.Add("@INDENT_NO", SqlDbType.VarChar).Value = slno;
                    da = new SqlDataAdapter(cmd);
                    //da = new SqlDataAdapter("select NAME,UNIT,QTY FROM INDENT_TO_PHARMACY_ITEM where INDENT_NO='" + slno + "'", con);
                    DataSet ds2 = new DataSet();
                    da.Fill(ds2);
                    grdMaterial.DataSource = ds2.Tables["Table"];
                    grdMaterial.DataBind();
                }
                txtipno.Enabled = false;
            }
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
            using (SqlCommand cmd1 = new SqlCommand("LAB_INDENT_PHARMACY", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_INDT_PHRMA_PAGE";
                cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "NULL";
                cmd1.Parameters.Add("@INDENT_NO", SqlDbType.VarChar).Value = "NULL";
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                //SqlDataAdapter da = new SqlDataAdapter("SELECT * from INDENT_TO_PHARMACY", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "INDENT_NO" };
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