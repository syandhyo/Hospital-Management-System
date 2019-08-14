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

public partial class STOREKEEPER_VenderMaster : System.Web.UI.Page
{
    
    string num1 = "VN000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr, dr1;
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
        string qry1 = "select ID from VENDER_MASTER_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("VN{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        lblAuto.Text = num1;

        dr.Close();
        con.Close();
    }
  
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[8].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
   
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
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        if (!IsPostBack)
        {
            binddata();
            auto();
        }
        //clear_control();
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
        SqlCommand comid = new SqlCommand("select max(ID) as venid from VENDER_MASTER_TABLE ", con);
        dr1 = comid.ExecuteReader();
        if (dr1.Read())
        {
            txtvendorid.Text = dr1["venid"].ToString();
        }
        dr1.Close();
       // SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME1,CITY,STATE,TEL_NO,GST,OPENING,PIN,ADDRESS1 FROM VENDER_MASTER_TABLE WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);

        using (SqlCommand cmd1 = new SqlCommand("SP_VENDOR_PAGING", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT_PAGE";
            cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);
           // GridView1.SelectedIndex = 0;
            GridView1.DataSource = dt;
            GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();
        }
        con.Close();
    }
    

    public void clear_control()
    {
        txtid.Text = "";
        txtcname1.Text = "";
        txtcname2.Text = "";
        txtadd1.Text = "";
        txtadd2.Text = "";
        txtcity.Text = "";
      //  txtcontactno.Text = "";
        txtgstno.Text = "";
        txtopening.Text = "0";
        txtpin.Text = "";
        txtstate.Text = "";
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }

    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
        SqlCommand cm = new SqlCommand("delete from VENDER_MASTER_TABLE where ID='" + slno + "'", con);
        cm.ExecuteNonQuery();
        //SqlCommand cm1 = new SqlCommand("delete from CATEGORYTRAN_TABLE where ID='" + slno + "'", con);
        //cm1.ExecuteNonQuery();
        binddata();
        con.Close();
        Response.Redirect("~/STOREKEEPER/VendorMaster.aspx");
        // Response.Redirect("/USER/Transaction.aspx");
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
       // SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME1,CITY,STATE,TEL_NO,GST,OPENING,PIN,ADDRESS FROM VENDER_MASTER_TABLE WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);

        using (SqlCommand cmd1 = new SqlCommand("SP_VENDOR_PAGING", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT_PAGE";
            cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);
           // GridView1.SelectedIndex = 0;
            GridView1.DataSource = dt;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();
        }
        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtcname1.Text == "")
            {
                string message = "alert('Please!! Enter The Company Name Part1..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtadd1.Text == "")
            {
                string message = "alert('Please!! Enter the Address.. ')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtcity.Text == "")
            {
                string message = "alert('Please!! Enter The City..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtstate.Text == "")
            {
                string message = "alert('Please!! Enter The State..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtpin.Text == "")
            {
                string message = "alert('Please!! Enter The PIN Code..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtmobno.Text == "")
            {
                string message = "alert('Please!! Enter The Mobile Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            //else if (txttelno.Text == "")
            //{
            //    string message = "alert('Please!! Enter The Telephone Number..')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            else if (txtgstno.Text == "")
            {
                string message = "alert('Please!! Enter The GST No..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtstatecode.Text == "")
            {
                string message = "alert('Please!! Enter The Sate Code..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtcperson.Text == "")
            {
                string message = "alert('Please!! Enter The Contact Person..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtcpersonno.Text == "")
            {
                string message = "alert('Please!! Enter The Contact Person Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            //else if (txtemail1.Text == "")
            //{
            //    string message = "alert('Please!! Enter The EmailID..')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            //else if (txtexcise.Text == "")
            //{
            //    string message = "alert('Please!! Enter The Excise Duty Number..')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            else if (txtbankacno.Text == "")
            {
                string message = "alert('Please!! Enter The Bank Account Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtperacno.Text == "")
            {
                string message = "alert('Please!! Enter The Permanent A/c Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            //else if (txtbranch.Text == "")
            //{
            //    string message = "alert('Please!! Enter The Branch Name..')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            else if (txtbankname.Text == "")
            {
                string message = "alert('Please!! Enter The Bank Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtifsc.Text == "")
            {
                string message = "alert('Please!! Enter The IFSC Code..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            //else if (txtdate.Text == "")
            //{
            //    string message = "alert('Please!! Enter The Date..')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            else if (txtopening.Text == "")
            {
                txtopening.Text = "0.00";
            }
            else if (txtactype.SelectedIndex == 0)
            {
                string message = "alert('Please!! Enter Account Type.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();

            using (SqlCommand cmd1 = new SqlCommand("sp_vendor", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblAuto.Text;
                cmd1.Parameters.Add("@NAME1", SqlDbType.VarChar).Value = txtcname1.Text;
                cmd1.Parameters.Add("@NAME2", SqlDbType.VarChar).Value = txtcname2.Text;
                cmd1.Parameters.Add("@ADDRESS1", SqlDbType.VarChar).Value = txtadd1.Text;
                cmd1.Parameters.Add("@ADDRESS2", SqlDbType.VarChar).Value = txtadd2.Text;
                cmd1.Parameters.Add("@PIN", SqlDbType.VarChar).Value = txtpin.Text;
                cmd1.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtcity.Text;
                cmd1.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
                cmd1.Parameters.Add("@TEL_NO", SqlDbType.VarChar).Value = txttelno.Text;
                cmd1.Parameters.Add("@MOB_NO", SqlDbType.VarChar).Value = txtmobno.Text;
                cmd1.Parameters.Add("@FAX_NO", SqlDbType.VarChar).Value = txtfaxno.Text;
                cmd1.Parameters.Add("@CONTACT_PER", SqlDbType.VarChar).Value = txtcperson.Text;
                cmd1.Parameters.Add("@CONTACT_PER_NO", SqlDbType.VarChar).Value = txtcpersonno.Text;
                cmd1.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = txtemail1.Text;
                cmd1.Parameters.Add("@ALT_EMAIL", SqlDbType.VarChar).Value = txtemail2.Text;
                cmd1.Parameters.Add("@EXCISE", SqlDbType.VarChar).Value = txtexcise.Text;
                cmd1.Parameters.Add("@GST", SqlDbType.VarChar).Value = txtgstno.Text;
                cmd1.Parameters.Add("@PERM_ACCT_NO", SqlDbType.VarChar).Value = txtperacno.Text;
                cmd1.Parameters.Add("@BANK_ACCT_NO", SqlDbType.VarChar).Value = txtbankacno.Text;
                cmd1.Parameters.Add("@BANK_NAME", SqlDbType.VarChar).Value = txtbankname.Text;
                cmd1.Parameters.Add("@BRANCH_ADD", SqlDbType.VarChar).Value = txtbranch.Text;
                cmd1.Parameters.Add("@ACC_TYPE", SqlDbType.VarChar).Value = txtactype.Text;
                cmd1.Parameters.Add("@IFSC", SqlDbType.VarChar).Value = txtifsc.Text;
                cmd1.Parameters.Add("@OPENING", SqlDbType.Decimal).Value = txtopening.Text;
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtstatecode.Text;
                cmd1.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");

                cmd1.Parameters.Add("@CREDIT", SqlDbType.Decimal).Value = "0.00";
                cmd1.Parameters.Add("@DEBIT", SqlDbType.Decimal).Value = "0.00";
                cmd1.Parameters.Add("@CLOSING", SqlDbType.Decimal).Value = txtopening.Text;
                cmd1.Parameters.Add("@VENDOR_ID", SqlDbType.VarChar).Value = lblAuto.Text;

                cmd1.ExecuteNonQuery();
                string message = "alert('Successfully Inserted.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            // SqlCommand com = new SqlCommand("select * from VENDER_TABLE where GST='" + txtgstno.Text + "' AND  ORGID='" + lblorgid.Text + "'", con);
            // dr = com.ExecuteReader();
            // if (dr.Read())
            //{
            //   Response.Write("<script> alert('Supplier Already Exist. Try New Name...')</script>");
            //   return;

            // }
            // dr.Close();

            // auto();
            // SqlDataAdapter da = new SqlDataAdapter("insert into VENDER_TABLE values('" + txtid.Text + "','" + txtname.Text + "','" + txtcity.Text + "','" + txtstate.Text + "','" + txtcontactno.Text + "','" + txtgstno.Text + "','0','" + txtpin.Text + "','" + txtaddress.Text + "','" + lblorgid.Text + "','" + txtstatecode.Text + "','" + txtopening.Text + "','" + txtdate.Text + "')", con);
            // DataSet ds = new DataSet();
            // da.Fill(ds);
            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/STOREKEEPER/VendorMaster.aspx");
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand com = new SqlCommand("select * from VENDER_MASTER_TABLE where ID='" + slno + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                lblEditgrd.Text = slno.ToString();
                txtid.Text = dr["ID"].ToString();
                txtcname1.Text = dr["NAME1"].ToString();
                txtcname2.Text = dr["NAME2"].ToString();
                txtadd1.Text = dr["ADDRESS1"].ToString();
                txtadd2.Text = dr["ADDRESS2"].ToString();
                txtpin.Text = dr["PIN"].ToString();
                txtcity.Text = dr["CITY"].ToString();
                txtstate.Text = dr["STATE"].ToString();
                txttelno.Text = dr["TEL_NO"].ToString();
                txtmobno.Text = dr["MOB_NO"].ToString();
                txtfaxno.Text = dr["FAX_NO"].ToString();
                txtcperson.Text = dr["CONTACT_PER"].ToString();
                txtcpersonno.Text = dr["CONTACT_PER_NO"].ToString();
                txtemail1.Text = dr["EMAIL"].ToString();
                txtemail2.Text = dr["ALT_EMAIL"].ToString();
                txtexcise.Text = dr["EXCISE"].ToString();
                txtgstno.Text = dr["GST"].ToString();

                txtperacno.Text = dr["PERM_ACCT_NO"].ToString();
                txtbankacno.Text = dr["BANK_ACCT_NO"].ToString();
                txtbankname.Text = dr["BANK_NAME"].ToString();
                txtbranch.Text = dr["BRANCH_ADD"].ToString();
                //  txtactype.Text = dr["ACC_TYPE"].ToString();
                txtactype.SelectedItem.Text = dr["ACC_TYPE"].ToString();
                txtifsc.Text = dr["IFSC"].ToString();
                txtopening.Text = dr["OPENING"].ToString();
                txtdate.Text = Convert.ToDateTime(dr["DATE"].ToString()).ToString("dd-MM-yyyy");
                txtstatecode.Text = dr["STATECODE"].ToString();

            }
            dr.Close();
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
            if (txtcname1.Text == "")
            {
                string message = "alert('Please!! Enter The Company Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtgstno.Text == "")
            {
                string message = "alert('Please!! Enter GST Number...')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
              
            }
            else if (txtstatecode.Text == "")
            {
                string message = "alert('Please!! Enter State Code.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
              
            }
            else if (txtopening.Text == "")
            {
                string message = "alert('Please!! Enter Opening.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
               
            }
            else if (txtactype.SelectedIndex == 0)
            {
                string message = "alert('Please!! Enter Account Type.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            using (SqlCommand cmd1 = new SqlCommand("sp_vendor", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblEditgrd.Text;
                cmd1.Parameters.Add("@NAME1", SqlDbType.VarChar).Value = txtcname1.Text;
                cmd1.Parameters.Add("@NAME2", SqlDbType.VarChar).Value = txtcname2.Text;
                cmd1.Parameters.Add("@ADDRESS1", SqlDbType.VarChar).Value = txtadd1.Text;
                cmd1.Parameters.Add("@ADDRESS2", SqlDbType.VarChar).Value = txtadd2.Text;
                cmd1.Parameters.Add("@PIN", SqlDbType.VarChar).Value = txtpin.Text;
                cmd1.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtcity.Text;
                cmd1.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
                cmd1.Parameters.Add("@TEL_NO", SqlDbType.VarChar).Value = txttelno.Text;
                cmd1.Parameters.Add("@MOB_NO", SqlDbType.VarChar).Value = txtmobno.Text;
                cmd1.Parameters.Add("@FAX_NO", SqlDbType.VarChar).Value = txtfaxno.Text;
                cmd1.Parameters.Add("@CONTACT_PER", SqlDbType.VarChar).Value = txtcperson.Text;
                cmd1.Parameters.Add("@CONTACT_PER_NO", SqlDbType.VarChar).Value = txtcpersonno.Text;
                cmd1.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = txtemail1.Text;
                cmd1.Parameters.Add("@ALT_EMAIL", SqlDbType.VarChar).Value = txtemail2.Text;
                cmd1.Parameters.Add("@EXCISE", SqlDbType.VarChar).Value = txtexcise.Text;
                cmd1.Parameters.Add("@GST", SqlDbType.VarChar).Value = txtgstno.Text;
                cmd1.Parameters.Add("@PERM_ACCT_NO", SqlDbType.VarChar).Value = txtperacno.Text;
                cmd1.Parameters.Add("@BANK_ACCT_NO", SqlDbType.VarChar).Value = txtbankacno.Text;
                cmd1.Parameters.Add("@BANK_NAME", SqlDbType.VarChar).Value = txtbankname.Text;
                cmd1.Parameters.Add("@BRANCH_ADD", SqlDbType.VarChar).Value = txtbranch.Text;
                cmd1.Parameters.Add("@ACC_TYPE", SqlDbType.VarChar).Value = txtactype.Text;
                cmd1.Parameters.Add("@IFSC", SqlDbType.VarChar).Value = txtifsc.Text;
                cmd1.Parameters.Add("@OPENING", SqlDbType.Decimal).Value = txtopening.Text;
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtstatecode.Text;
                cmd1.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");

                cmd1.Parameters.Add("@CREDIT", SqlDbType.Decimal).Value = "0.00";
                cmd1.Parameters.Add("@DEBIT", SqlDbType.Decimal).Value = "0.00";
                cmd1.Parameters.Add("@CLOSING", SqlDbType.Decimal).Value = txtopening.Text;
                cmd1.Parameters.Add("@VENDOR_ID", SqlDbType.VarChar).Value = lblEditgrd.Text;
                cmd1.ExecuteNonQuery();
                string message = "alert('Successfully Updated.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/STOREKEEPER/VendorMaster.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/STOREKEEPER/VendorMaster.aspx");
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            // SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME,PRICE,QTY FROM MATERIAL_MASTER_TABLE WHERE NAME like'" + txtsearchname.Text + "%' AND  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
            using (SqlCommand cmd1 = new SqlCommand("USP_VENDORSEARCH", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH";
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtsearchname.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
              //  GridView1.SelectedIndex = 0;
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