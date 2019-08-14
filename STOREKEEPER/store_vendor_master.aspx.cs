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

public partial class STOREKEEPER_store_vendor_master : System.Web.UI.Page
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
    DataMathods OBJ_METHOD = new DataMathods();

    public void auto()
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
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
            txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            if (!IsPostBack)
            {
                binddata();
                auto();
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
            try
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[1];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

                DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);

            }
            DataSet Ds = OBJ_METHOD.Get_DataSet("select max(ID) as venid from VENDER_MASTER_TABLE where Branch_ID = " + Session["Branch"] + "", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                txtvendorid.Text = Ds.Tables[0].Rows[0]["venid"].ToString();
            }
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SELECT_PAGE");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("SP_VENDOR_PAGING", false, true, SQL_PARAMS1);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds1;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            #region oldcode
            //SqlCommand comid = new SqlCommand("select max(ID) as venid from VENDER_MASTER_TABLE ", con);
            //dr1 = comid.ExecuteReader();
            //if (dr1.Read())
            //{
            //    txtvendorid.Text = dr1["venid"].ToString();
            //}
            //dr1.Close();
            // SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME1,CITY,STATE,TEL_NO,GST,OPENING,PIN,ADDRESS1 FROM VENDER_MASTER_TABLE WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
            
            //using (SqlCommand cmd1 = new SqlCommand("SP_VENDOR_PAGING", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT_PAGE";
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    SqlDataAdapter da = new SqlDataAdapter(cmd1);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    // GridView1.SelectedIndex = 0;
            //    GridView1.DataSource = dt;
            //    GridView1.DataKeyNames = new string[] { "ID" };
            //    GridView1.DataBind();
            //}
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }


    public void clear_control()
    {
        try
        {
            txtmobno.Text = "";
            txtfaxno.Text = "";
            txtstatecode.Text = "";
            txtcperson.Text = "";
            txtcpersonno.Text = "";
            txtexcise.Text = "";
            txtperacno.Text = "";
            txtbankacno.Text = "";
            txtbankname.Text = "";
            txtifsc.Text = "";
            txtactype.SelectedIndex = 0;
            txtid.Text = "";
            txtcname1.Text = "";
            txtcname2.Text = "";
            txtadd1.Text = "";
            txtadd2.Text = "";
            txtcity.Text = "";
            //  txtcontactno.Text = "";
            txtgstno.Text = "";
            txtopening.Text = "0.00";
            txtpin.Text = "";
            txtstate.Text = "";
            btncreate.Visible = true;
            btnupdate.Visible = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

            OBJ_METHOD.ExecuteProceedure("sp_vendor", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();

            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";

            
            //SqlCommand cm = new SqlCommand("delete from VENDER_MASTER_TABLE where ID='" + slno + "'", con);
            //cm.ExecuteNonQuery();
            ////SqlCommand cm1 = new SqlCommand("delete from CATEGORYTRAN_TABLE where ID='" + slno + "'", con);
            ////cm1.ExecuteNonQuery();
            //binddata();
            //con.Close();
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not Deleted.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SELECT_PAGE");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("SP_VENDOR_PAGING", false, true, SQL_PARAMS1);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds1;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
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
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[34];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, lblAuto.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@NAME1", SqlDbType.VarChar, 500, txtcname1.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@NAME2", SqlDbType.VarChar, 500, txtcname2.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ADDRESS1", SqlDbType.VarChar, 500, txtadd1.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@BRANCH_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            SQL_PARAMS[7] = OBJ_METHOD.createParams("@ADDRESS2", SqlDbType.VarChar, 500, txtadd2.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@PIN", SqlDbType.VarChar, 500, txtpin.Text);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@CITY", SqlDbType.VarChar, 500, txtcity.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@STATE", SqlDbType.VarChar, 500, txtstate.Text);

            SQL_PARAMS[11] = OBJ_METHOD.createParams("@TEL_NO", SqlDbType.VarChar, 500, txttelno.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@MOB_NO", SqlDbType.VarChar, 500, txtmobno.Text);
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@FAX_NO", SqlDbType.VarChar, 500, txtfaxno.Text);
            SQL_PARAMS[14] = OBJ_METHOD.createParams("@CONTACT_PER", SqlDbType.VarChar, 500, txtcperson.Text);

            SQL_PARAMS[15] = OBJ_METHOD.createParams("@CONTACT_PER_NO", SqlDbType.VarChar, 500, txtcpersonno.Text);
            SQL_PARAMS[16] = OBJ_METHOD.createParams("@EMAIL", SqlDbType.VarChar, 500, txtemail1.Text);
            SQL_PARAMS[17] = OBJ_METHOD.createParams("@ALT_EMAIL", SqlDbType.VarChar, 500, txtemail2.Text);
            SQL_PARAMS[18] = OBJ_METHOD.createParams("@EXCISE", SqlDbType.VarChar, 500, txtexcise.Text);

            SQL_PARAMS[19] = OBJ_METHOD.createParams("@GST", SqlDbType.VarChar, 500, txtgstno.Text);
            SQL_PARAMS[20] = OBJ_METHOD.createParams("@PERM_ACCT_NO", SqlDbType.VarChar, 500, txtperacno.Text);
            SQL_PARAMS[21] = OBJ_METHOD.createParams("@BANK_ACCT_NO", SqlDbType.VarChar, 500, txtbankacno.Text);
            SQL_PARAMS[22] = OBJ_METHOD.createParams("@BANK_NAME", SqlDbType.VarChar, 500, txtbankname.Text);

            SQL_PARAMS[23] = OBJ_METHOD.createParams("@BRANCH_ADD", SqlDbType.VarChar, 500, txtbranch.Text);
            SQL_PARAMS[24] = OBJ_METHOD.createParams("@ACC_TYPE", SqlDbType.VarChar, 500, txtactype.Text);
            SQL_PARAMS[25] = OBJ_METHOD.createParams("@OPENING", SqlDbType.VarChar, 500, txtopening.Text);
            SQL_PARAMS[26] = OBJ_METHOD.createParams("@IFSC", SqlDbType.VarChar, 500, txtifsc.Text);

            SQL_PARAMS[27] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[28] = OBJ_METHOD.createParams("@STATECODE", SqlDbType.VarChar, 500, txtstatecode.Text);
            SQL_PARAMS[29] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 500, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[30] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.Decimal, 0, "0.00");

            SQL_PARAMS[31] = OBJ_METHOD.createParams("@DEBIT", SqlDbType.Decimal, 0, "0.00");
            SQL_PARAMS[32] = OBJ_METHOD.createParams("@CLOSING", SqlDbType.Decimal, 0, txtopening.Text);
            SQL_PARAMS[33] = OBJ_METHOD.createParams("@VENDOR_ID", SqlDbType.VarChar, 500, lblAuto.Text);

            OBJ_METHOD.ExecuteProceedure("sp_vendor", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clear_control();
            }

            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            #region insert code
            //using (SqlCommand cmd1 = new SqlCommand("sp_vendor", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblAuto.Text;
            //    cmd1.Parameters.Add("@NAME1", SqlDbType.VarChar).Value = txtcname1.Text;
            //    cmd1.Parameters.Add("@NAME2", SqlDbType.VarChar).Value = txtcname2.Text;
            //    cmd1.Parameters.Add("@ADDRESS1", SqlDbType.VarChar).Value = txtadd1.Text;
            //    cmd1.Parameters.Add("@ADDRESS2", SqlDbType.VarChar).Value = txtadd2.Text;
            //    cmd1.Parameters.Add("@PIN", SqlDbType.VarChar).Value = txtpin.Text;
            //    cmd1.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtcity.Text;
            //    cmd1.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
            //    cmd1.Parameters.Add("@TEL_NO", SqlDbType.VarChar).Value = txttelno.Text;
            //    cmd1.Parameters.Add("@MOB_NO", SqlDbType.VarChar).Value = txtmobno.Text;
            //    cmd1.Parameters.Add("@FAX_NO", SqlDbType.VarChar).Value = txtfaxno.Text;
            //    cmd1.Parameters.Add("@CONTACT_PER", SqlDbType.VarChar).Value = txtcperson.Text;

            //    cmd1.Parameters.Add("@CONTACT_PER_NO", SqlDbType.VarChar).Value = txtcpersonno.Text;
            //    cmd1.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = txtemail1.Text;
            //    cmd1.Parameters.Add("@ALT_EMAIL", SqlDbType.VarChar).Value = txtemail2.Text;
            //    cmd1.Parameters.Add("@EXCISE", SqlDbType.VarChar).Value = txtexcise.Text;

            //    cmd1.Parameters.Add("@GST", SqlDbType.VarChar).Value = txtgstno.Text;
            //    cmd1.Parameters.Add("@PERM_ACCT_NO", SqlDbType.VarChar).Value = txtperacno.Text;
            //    cmd1.Parameters.Add("@BANK_ACCT_NO", SqlDbType.VarChar).Value = txtbankacno.Text;
            //    cmd1.Parameters.Add("@BANK_NAME", SqlDbType.VarChar).Value = txtbankname.Text;

            //    cmd1.Parameters.Add("@BRANCH_ADD", SqlDbType.VarChar).Value = txtbranch.Text;
            //    cmd1.Parameters.Add("@ACC_TYPE", SqlDbType.VarChar).Value = txtactype.Text;
            //    cmd1.Parameters.Add("@IFSC", SqlDbType.VarChar).Value = txtifsc.Text;
            //    cmd1.Parameters.Add("@OPENING", SqlDbType.Decimal).Value = txtopening.Text;

            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtstatecode.Text;
            //    cmd1.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");

            //    cmd1.Parameters.Add("@CREDIT", SqlDbType.Decimal).Value = "0.00";
            //    cmd1.Parameters.Add("@DEBIT", SqlDbType.Decimal).Value = "0.00";
            //    cmd1.Parameters.Add("@CLOSING", SqlDbType.Decimal).Value = txtopening.Text;
            //    cmd1.Parameters.Add("@VENDOR_ID", SqlDbType.VarChar).Value = lblAuto.Text;

            //    cmd1.ExecuteNonQuery();
            //    string message = "alert('Successfully Inserted.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //}
            
            //binddata();
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from VENDER_MASTER_TABLE where ID='" + slno + "' and Branch_ID = " + Session["Branch"] + "", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                lblEditgrd.Text = slno.ToString();
                txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                txtcname1.Text = Ds.Tables[0].Rows[0]["NAME1"].ToString();
                txtcname2.Text = Ds.Tables[0].Rows[0]["NAME2"].ToString();
                txtadd1.Text = Ds.Tables[0].Rows[0]["ADDRESS1"].ToString();
                txtadd2.Text = Ds.Tables[0].Rows[0]["ADDRESS2"].ToString();
                txtpin.Text = Ds.Tables[0].Rows[0]["PIN"].ToString();
                txtcity.Text = Ds.Tables[0].Rows[0]["CITY"].ToString();
                txtstate.Text = Ds.Tables[0].Rows[0]["STATE"].ToString();
                txttelno.Text = Ds.Tables[0].Rows[0]["TEL_NO"].ToString();
                txtmobno.Text = Ds.Tables[0].Rows[0]["MOB_NO"].ToString();
                txtfaxno.Text = Ds.Tables[0].Rows[0]["FAX_NO"].ToString();
                txtcperson.Text = Ds.Tables[0].Rows[0]["CONTACT_PER"].ToString();
                txtcpersonno.Text = Ds.Tables[0].Rows[0]["CONTACT_PER_NO"].ToString();
                txtemail1.Text = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                txtemail2.Text = Ds.Tables[0].Rows[0]["ALT_EMAIL"].ToString();
                txtexcise.Text = Ds.Tables[0].Rows[0]["EXCISE"].ToString();
                txtgstno.Text = Ds.Tables[0].Rows[0]["GST"].ToString();

                txtperacno.Text = Ds.Tables[0].Rows[0]["PERM_ACCT_NO"].ToString();
                txtbankacno.Text = Ds.Tables[0].Rows[0]["BANK_ACCT_NO"].ToString();
                txtbankname.Text = Ds.Tables[0].Rows[0]["BANK_NAME"].ToString();
                txtbranch.Text = Ds.Tables[0].Rows[0]["BRANCH_ADD"].ToString();
                //  txtactype.Text = dr["ACC_TYPE"].ToString();
                txtactype.Text = Ds.Tables[0].Rows[0]["ACC_TYPE"].ToString();
                txtifsc.Text = Ds.Tables[0].Rows[0]["IFSC"].ToString();
                txtopening.Text = Ds.Tables[0].Rows[0]["OPENING"].ToString();
                txtdate.Text = Convert.ToDateTime(Ds.Tables[0].Rows[0]["DATE"].ToString()).ToString("dd-MM-yyyy");
                txtstatecode.Text = Ds.Tables[0].Rows[0]["STATECODE"].ToString();
            }
            #region old code
            //SqlCommand com = new SqlCommand("select * from VENDER_MASTER_TABLE where ID='" + slno + "'", con);
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    btncreate.Visible = false;
            //    btnupdate.Visible = true;
            //    lblEditgrd.Text = slno.ToString();
            //    txtid.Text = dr["ID"].ToString();
            //    txtcname1.Text = dr["NAME1"].ToString();
            //    txtcname2.Text = dr["NAME2"].ToString();
            //    txtadd1.Text = dr["ADDRESS1"].ToString();
            //    txtadd2.Text = dr["ADDRESS2"].ToString();
            //    txtpin.Text = dr["PIN"].ToString();
            //    txtcity.Text = dr["CITY"].ToString();
            //    txtstate.Text = dr["STATE"].ToString();
            //    txttelno.Text = dr["TEL_NO"].ToString();
            //    txtmobno.Text = dr["MOB_NO"].ToString();
            //    txtfaxno.Text = dr["FAX_NO"].ToString();
            //    txtcperson.Text = dr["CONTACT_PER"].ToString();
            //    txtcpersonno.Text = dr["CONTACT_PER_NO"].ToString();
            //    txtemail1.Text = dr["EMAIL"].ToString();
            //    txtemail2.Text = dr["ALT_EMAIL"].ToString();
            //    txtexcise.Text = dr["EXCISE"].ToString();
            //    txtgstno.Text = dr["GST"].ToString();

            //    txtperacno.Text = dr["PERM_ACCT_NO"].ToString();
            //    txtbankacno.Text = dr["BANK_ACCT_NO"].ToString();
            //    txtbankname.Text = dr["BANK_NAME"].ToString();
            //    txtbranch.Text = dr["BRANCH_ADD"].ToString();
            //    //  txtactype.Text = dr["ACC_TYPE"].ToString();
            //    txtactype.SelectedItem.Text = dr["ACC_TYPE"].ToString();
            //    txtifsc.Text = dr["IFSC"].ToString();
            //    txtopening.Text = dr["OPENING"].ToString();
            //    txtdate.Text = Convert.ToDateTime(dr["DATE"].ToString()).ToString("dd-MM-yyyy");
            //    txtstatecode.Text = dr["STATECODE"].ToString();

            //}
            //dr.Close();
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
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
            SqlParameter[] SQL_PARAMS = new SqlParameter[32];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, lblEditgrd.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@NAME1", SqlDbType.VarChar, 500, txtcname1.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@NAME2", SqlDbType.VarChar, 500, txtcname2.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ADDRESS1", SqlDbType.VarChar, 500, txtadd1.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

            SQL_PARAMS[5] = OBJ_METHOD.createParams("@ADDRESS2", SqlDbType.VarChar, 500, txtadd2.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@PIN", SqlDbType.VarChar, 500, txtpin.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@CITY", SqlDbType.VarChar, 500, txtcity.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@STATE", SqlDbType.VarChar, 500, txtstate.Text);

            SQL_PARAMS[9] = OBJ_METHOD.createParams("@TEL_NO", SqlDbType.VarChar, 500, txttelno.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@MOB_NO", SqlDbType.VarChar, 500, txtmobno.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@FAX_NO", SqlDbType.VarChar, 500, txtfaxno.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@CONTACT_PER", SqlDbType.VarChar, 500, txtcperson.Text);

            SQL_PARAMS[13] = OBJ_METHOD.createParams("@CONTACT_PER_NO", SqlDbType.VarChar, 500, txtcpersonno.Text);
            SQL_PARAMS[14] = OBJ_METHOD.createParams("@EMAIL", SqlDbType.VarChar, 500, txtemail1.Text);
            SQL_PARAMS[15] = OBJ_METHOD.createParams("@ALT_EMAIL", SqlDbType.VarChar, 500, txtemail2.Text);
            SQL_PARAMS[16] = OBJ_METHOD.createParams("@EXCISE", SqlDbType.VarChar, 500, txtexcise.Text);

            SQL_PARAMS[17] = OBJ_METHOD.createParams("@GST", SqlDbType.VarChar, 500, txtgstno.Text);
            SQL_PARAMS[18] = OBJ_METHOD.createParams("@PERM_ACCT_NO", SqlDbType.VarChar, 500, txtperacno.Text);
            SQL_PARAMS[19] = OBJ_METHOD.createParams("@BANK_ACCT_NO", SqlDbType.VarChar, 500, txtbankacno.Text);
            SQL_PARAMS[20] = OBJ_METHOD.createParams("@BANK_NAME", SqlDbType.VarChar, 500, txtbankname.Text);

            SQL_PARAMS[21] = OBJ_METHOD.createParams("@BRANCH_ADD", SqlDbType.VarChar, 500, txtbranch.Text);
            SQL_PARAMS[22] = OBJ_METHOD.createParams("@ACC_TYPE", SqlDbType.VarChar, 500, txtactype.Text);
            SQL_PARAMS[23] = OBJ_METHOD.createParams("@OPENING", SqlDbType.VarChar, 500, txtopening.Text);
            SQL_PARAMS[24] = OBJ_METHOD.createParams("@IFSC", SqlDbType.VarChar, 500, txtifsc.Text);

            SQL_PARAMS[25] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[26] = OBJ_METHOD.createParams("@STATECODE", SqlDbType.VarChar, 500, txtstatecode.Text);
            SQL_PARAMS[27] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 500, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[28] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.Decimal, 0, "0.00");

            SQL_PARAMS[29] = OBJ_METHOD.createParams("@DEBIT", SqlDbType.Decimal, 0, "0.00");
            SQL_PARAMS[30] = OBJ_METHOD.createParams("@CLOSING", SqlDbType.Decimal, 0, txtopening.Text);
            SQL_PARAMS[31] = OBJ_METHOD.createParams("@VENDOR_ID", SqlDbType.VarChar, 500, lblAuto.Text);

            OBJ_METHOD.ExecuteProceedure("sp_vendor", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clear_control();
            }

            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            #region update code
            //using (SqlCommand cmd1 = new SqlCommand("sp_vendor", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;

            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblEditgrd.Text;
            //    cmd1.Parameters.Add("@NAME1", SqlDbType.VarChar).Value = txtcname1.Text;
            //    cmd1.Parameters.Add("@NAME2", SqlDbType.VarChar).Value = txtcname2.Text;
            //    cmd1.Parameters.Add("@ADDRESS1", SqlDbType.VarChar).Value = txtadd1.Text;
            //    cmd1.Parameters.Add("@ADDRESS2", SqlDbType.VarChar).Value = txtadd2.Text;
            //    cmd1.Parameters.Add("@PIN", SqlDbType.VarChar).Value = txtpin.Text;
            //    cmd1.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtcity.Text;
            //    cmd1.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
            //    cmd1.Parameters.Add("@TEL_NO", SqlDbType.VarChar).Value = txttelno.Text;
            //    cmd1.Parameters.Add("@MOB_NO", SqlDbType.VarChar).Value = txtmobno.Text;
            //    cmd1.Parameters.Add("@FAX_NO", SqlDbType.VarChar).Value = txtfaxno.Text;
            //    cmd1.Parameters.Add("@CONTACT_PER", SqlDbType.VarChar).Value = txtcperson.Text;
            //    cmd1.Parameters.Add("@CONTACT_PER_NO", SqlDbType.VarChar).Value = txtcpersonno.Text;
            //    cmd1.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = txtemail1.Text;
            //    cmd1.Parameters.Add("@ALT_EMAIL", SqlDbType.VarChar).Value = txtemail2.Text;
            //    cmd1.Parameters.Add("@EXCISE", SqlDbType.VarChar).Value = txtexcise.Text;
            //    cmd1.Parameters.Add("@GST", SqlDbType.VarChar).Value = txtgstno.Text;
            //    cmd1.Parameters.Add("@PERM_ACCT_NO", SqlDbType.VarChar).Value = txtperacno.Text;
            //    cmd1.Parameters.Add("@BANK_ACCT_NO", SqlDbType.VarChar).Value = txtbankacno.Text;
            //    cmd1.Parameters.Add("@BANK_NAME", SqlDbType.VarChar).Value = txtbankname.Text;
            //    cmd1.Parameters.Add("@BRANCH_ADD", SqlDbType.VarChar).Value = txtbranch.Text;
            //    cmd1.Parameters.Add("@ACC_TYPE", SqlDbType.VarChar).Value = txtactype.Text;
            //    cmd1.Parameters.Add("@IFSC", SqlDbType.VarChar).Value = txtifsc.Text;
            //    cmd1.Parameters.Add("@OPENING", SqlDbType.Decimal).Value = txtopening.Text;
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtstatecode.Text;
            //    cmd1.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");

            //    cmd1.Parameters.Add("@CREDIT", SqlDbType.Decimal).Value = "0.00";
            //    cmd1.Parameters.Add("@DEBIT", SqlDbType.Decimal).Value = "0.00";
            //    cmd1.Parameters.Add("@CLOSING", SqlDbType.Decimal).Value = txtopening.Text;
            //    cmd1.Parameters.Add("@VENDOR_ID", SqlDbType.VarChar).Value = lblEditgrd.Text;
            //    cmd1.ExecuteNonQuery();
            //    string message = "alert('Successfully Updated.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //}
            //binddata();
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not updated.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        clear_control();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[4];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SEARCH");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtsearchname.Text);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("USP_VENDORSEARCH", false, true, SQL_PARAMS1);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds1;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            //using (SqlCommand cmd1 = new SqlCommand("USP_VENDORSEARCH", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH";
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtsearchname.Text;
            //    SqlDataAdapter da = new SqlDataAdapter(cmd1);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    //  GridView1.SelectedIndex = 0;
            //    GridView1.DataSource = dt;
            //    GridView1.DataKeyNames = new string[] { "ID" };
            //    GridView1.DataBind();
            //}
            //con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}