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

public partial class LABORATORY_lab_registration : System.Web.UI.Page
{
    string num1 = "PK000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da, da1, da2;
    DataTable dt;
    decimal total_amt = 0;
    decimal total_vat_amt = 0;
    int i, no, no1, sl;
    GridViewRow gr;
    string SS;
    decimal amount = 0;
    DataMathods OBJ_METHOD = new DataMathods();
    string succ;
    [WebMethod(EnableSession = true)]
    public static string[] GetOp(string prefix)
    {
        List<string> customers = new List<string>();
        string s = HttpContext.Current.Session["Branch"].ToString();
        using (SqlConnection conn = new SqlConnection())
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select DISTINCT QRCODE from REGISTRATION_TBL where QRCODE like '%'+@SearchText+'%' and Branch_ID=@Branch_ID";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Parameters.AddWithValue("@Branch_ID", Convert.ToInt32(s));
                cmd.Connection = con;

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["QRCODE"]));
                    }
                }
                con.Close();
            }
        }
        return customers.ToArray();
    }
    [WebMethod (EnableSession = true)]
    public static string[] GetIp(string prefix)
    {
        List<string> customers = new List<string>();
        string s = HttpContext.Current.Session["Branch"].ToString();
        using (SqlConnection conn = new SqlConnection())
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select DISTINCT VN from ADMISSION_TABLE where VN like '%'+@SearchText+'%' and Branch_ID=@Branch_ID";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Parameters.AddWithValue("@Branch_ID", Convert.ToInt32(s));
                cmd.Connection = con;

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["VN"]));
                    }
                }
                con.Close();
            }
        }
        return customers.ToArray();
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
        txtbranch.Text = Session["Branch"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        if (!IsPostBack)
        {
            binddata();
            testype();
        }
        con.Close();
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        txttime.Text = DateTime.Now.ToString("HH:mm");
    }
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        //string qry1 = "select max(ID) as ID from TBLRECLAB where ID like '%LB%'";

        //com = new SqlCommand(qry1, con);
        //dr = null;
        //dr = com.ExecuteReader();
        //string str1 = "1";
        //if (dr.Read() && dr["ID"].ToString() != "")
        //{
        //    // num1 = dr["ID"].ToString();
        //    //------------------
        //    num1 = dr["ID"].ToString();
        //    // string str="0";
        //    string str = num1.Substring(0, num1.Length - 0);//delete last 10 record
        //    string d = str.Substring(2);//delete first 3 record
        //    str1 = (Convert.ToInt64(d) + 1).ToString();//add in numbers
        //}
        ////-----------------------------------------------------
        ////num1 = string.Format("INV{0}", (Convert.ToUInt64(num1.Substring(3)) + 1).ToString("D4"));
        //txtid.Text = "LB" + str1;
        string qry1 = "select Max(ID) as ID from TBLRECLAB";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        if (dr.Read())
        {
            num1 = dr["ID"].ToString();
            if (num1 == "")
            {
                num1 = string.Format("LB{0}", (1).ToString("D4"));
            }
            else
            {
                num1 = string.Format("LB{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
            }
            
        }
        
        txtid.Text = num1;

        dr.Close();
        con.Close();
    }
    public void labtory_item()
    {
        try
        {
            int chkedcounter = 0;
            int correctinput = 0;
            OBJ_METHOD = new DataMathods();
            SS = "FALSE";

            foreach (GridViewRow row in grdtestype.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    //  Label typeIt = (row.Cells[0].FindControl("lbltype") as Label);
                    Label nameIt = (row.Cells[0].FindControl("lblname") as Label);
                    Label invIt = (row.Cells[1].FindControl("lblinv") as Label);
                    TextBox priceIt = (row.Cells[2].FindControl("lblprice") as TextBox);
                    CheckBox chkRow = (row.Cells[3].FindControl("chkRow") as CheckBox);

                    if (chkRow.Checked)
                    {
                        chkedcounter++;
                        var INV = Convert.ToInt32(grdtestype.DataKeys[row.RowIndex].Values[0]);
                        SS = "TRUE";
                        SqlParameter[] SQL_PARAMS = new SqlParameter[8];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDINSERT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@INVID", SqlDbType.VarChar, 500, INV.ToString());
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@INV", SqlDbType.VarChar, 500, invIt.Text);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, priceIt.Text);
                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@CHKSEL", SqlDbType.VarChar, 500, chkRow.Checked);


                        OBJ_METHOD.ExecuteProceedure("USP_RECLABTOR", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                        if (OBJ_METHOD._RESULT > 0)
                        {
                            SQL_PARAMS = new SqlParameter[7];

                            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT1");
                            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                            SQL_PARAMS[2] = OBJ_METHOD.createParams("@INV", SqlDbType.VarChar, 500, INV.ToString());
                            SQL_PARAMS[3] = OBJ_METHOD.createParams("@PRICE", SqlDbType.VarChar, 500, priceIt.Text);
                            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                            SQL_PARAMS[6] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtpname.Text);

                            OBJ_METHOD.ExecuteProceedure("RECP_LAB_REC_INSRTUP", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                            if (OBJ_METHOD._RESULT > 0)
                            {
                                correctinput++;

                            }

                        }
                        
                    }
                }
            }
            if (chkedcounter == correctinput && chkedcounter > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                succ = "success";

            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }


    }
    public void binddata()
    {

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[4] { new DataColumn("TESTNAME"), new DataColumn("INV"), new DataColumn("PRICE"), new DataColumn("CHKTYPE") });

        ViewState["ITEM"] = dt;
        this.BindGrid();
        SqlParameter[] SQL_PARAMS5 = new SqlParameter[2];
        SQL_PARAMS5[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SHOW_DOCT");
        SQL_PARAMS5[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

        DataSet DS6 = OBJ_METHOD.Get_DataSet("RECP_REGISTRATION", false, true, SQL_PARAMS5);
        if (DS6.Tables[0].Rows.Count > 0)
        {
            droprefdoc.DataSource = DS6;
            droprefdoc.DataTextField = "Sname";
            droprefdoc.DataValueField = "id";
            droprefdoc.DataBind();
            droprefdoc.Items.Insert(0, new ListItem("Please Select", "0"));
        }
       
    }
    protected void BindGrid()
    {
        try
        {
            grdtestype.DataSource = (DataTable)ViewState["ITEM"];
            grdtestype.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void testype()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("TESTNAME"), new DataColumn("INV"), new DataColumn("PRICE"), new DataColumn("CHKTYPE") });

            ViewState["ITEM"] = dt;
            this.BindGrid();

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_TEST");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 0, lblorgid.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_LAB_REC", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                dropTestype.DataSource = DS1;
                dropTestype.DataTextField = "NAME";
                dropTestype.DataValueField = "ID";
                dropTestype.DataBind();
                dropTestype.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
       
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (checkdoc.Checked)
        {
            txtrefdoc.Visible = true;
            droprefdoc.Visible = false;
        }
        else
        {
            txtrefdoc.Visible = false;
            droprefdoc.Visible = true;
        }
    }
    protected void droptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (droptype.SelectedValue == "InPatient")
        {
            lblip.Visible = true;
            lblop.Visible = false;
            txtipid.Visible = true;
            txtopid.Visible = false;
            txtpname.Text = txtaddress.Text = "";
        }
        else
        {
            lblip.Visible = false;
            lblop.Visible = true;
            txtipid.Visible = false;
            txtopid.Visible = true;
            txtpname.Text = txtaddress.Text = "";
        }
    }
    protected void txtipid_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet(" SELECT * From ADMISSIONVIEW where VN='" + txtipid.Text + "' AND Branch_ID='" + Session["Branch"] + "'", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                txtpname.Text = Ds.Tables[0].Rows[0]["NAME"].ToString();
                txtaddress.Text = Ds.Tables[0].Rows[0]["DEMOGRAPH"].ToString();

            }
            else
            {
                string message = "alert('* No Data Is There.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void txtopid_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet(" SELECT * From REGISTRATIONVIEW where QRCODE='" + txtopid.Text + "' AND Branch_ID='" + Session["Branch"] + "'", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                txtpname.Text = Ds.Tables[0].Rows[0]["NAME"].ToString();
                txtaddress.Text = Ds.Tables[0].Rows[0]["DEMOGRAPH"].ToString();

            }
            else
            {
                string message = "alert('* No Data Is There.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void chkRow_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grdtestype.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[3].FindControl("chkRow") as CheckBox);

                    if (chkRow.Checked)
                    {
                        var AMT = row.FindControl("lblprice") as TextBox;
                        amount = amount + Convert.ToDecimal(AMT.Text);
                    }
                }
            }
            lbltotalprice.Text = amount.ToString();
            // lblbalanceamt.Text = amount.ToString();
            decimal d = (amount / 100) * Convert.ToDecimal(txtdisc.Text);
            //  lbltotalamt.Text = (Convert.ToDouble(lbltotalprice.Text) - Convert.ToDouble(txtpaidamt.Text) - Convert.ToDouble(txtdisc.Text)).ToString();
            lbltotalamt.Text = (Convert.ToDouble(lbltotalprice.Text) - Convert.ToDouble(d)).ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void dropTestype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            //  DataTable dt = new DataTable();
            DataTable dt = (DataTable)ViewState["ITEM"];
            if (Session["mode"] == "true")
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[3];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_TEST_MULTI1");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, dropTestype.SelectedValue.ToString());
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

                DataSet DS = OBJ_METHOD.Get_DataSet("RECP_LAB_REC", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {

                    DS.Tables[0].TableName = "table1";
                    dt = DS.Tables["table1"];
                }
                
            }
            else
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_TEST_MULTI2");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, dropTestype.SelectedValue.ToString());
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
                DataSet DS = OBJ_METHOD.Get_DataSet("RECP_LAB_REC", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {

                    DS.Tables[0].TableName = "table2";
                    dt = DS.Tables["table2"];
                }
                
            }
            
            ViewState["ITEM"] = dt;
            this.BindGrid();
            divinner2.Visible = true;
            
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void txtdisc_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (droptype.SelectedValue == "InPatient")
            {
                if (txtipid.Text == "")
                {
                    string message = "alert('* Please Select IP NO.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtipid.Focus();
                    return;
                }
            }
            if (droptype.SelectedValue == "OutPatient")
            {
                if (txtopid.Text == "")
                {
                    string message = "alert('* Please Select IP NO.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtopid.Focus();
                    return;
                }
            }
            if (txtpname.Text == "")
            {
                string message = "alert('* Please Enter Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpname.Focus();
                return;
            }
            if (txtaddress.Text == "")
            {
                string message = "alert('* Please Enter Demographics Details.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpname.Focus();
                return;
            }



            auto();
            labtory_item();
            OBJ_METHOD = new DataMathods();
            if (SS == "FALSE")
            {
                string message = "alert('*Please select any item to save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            if (succ == "success")
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[15];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                if (droptype.SelectedValue == "InPatient")
                {
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@OPDNO", SqlDbType.VarChar, 500, txtipid.Text);
                }
                else {
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@OPDNO", SqlDbType.VarChar, 500, txtopid.Text);
                }
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtpname.Text);
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                SQL_PARAMS[6] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, "");
                SQL_PARAMS[7] = OBJ_METHOD.createParams("@EMPID", SqlDbType.VarChar, 500, droprefdoc.SelectedValue);
                SQL_PARAMS[8] = OBJ_METHOD.createParams("@PACKAGE", SqlDbType.VarChar, 500, "");
                SQL_PARAMS[9] = OBJ_METHOD.createParams("@TESTYPEPK", SqlDbType.VarChar, 500, dropTestype.SelectedItem.Text);
                SQL_PARAMS[10] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
                SQL_PARAMS[11] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm"));
                SQL_PARAMS[12] = OBJ_METHOD.createParams("@AMOUNT", SqlDbType.Decimal, 0, lbltotalprice.Text);
                SQL_PARAMS[13] = OBJ_METHOD.createParams("@TOTALAMOUNT", SqlDbType.Decimal, 0, lbltotalamt.Text);
                SQL_PARAMS[14] = OBJ_METHOD.createParams("@DISCOUNT", SqlDbType.Decimal, 0, txtdisc.Text);


                OBJ_METHOD.ExecuteProceedure("USP_RECLABTOR", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    var msg = OBJ_METHOD._objOut;
                    SQL_PARAMS = new SqlParameter[23];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                    if (droptype.SelectedValue == "InPatient")
                    {
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, txtipid.Text);
                    }
                    else
                    {
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, txtopid.Text);
                    }
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@PNAME", SqlDbType.VarChar, 500, txtpname.Text);
                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@UNAME", SqlDbType.VarChar, 500, lblid.Text);
                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@UID", SqlDbType.VarChar, 500, "");
                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@TYPE", SqlDbType.VarChar, 500, "");
                    SQL_PARAMS[10] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, lbltotalprice.Text);
                    SQL_PARAMS[11] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm"));
                    SQL_PARAMS[12] = OBJ_METHOD.createParams("@TESTTYPE", SqlDbType.VarChar, 0, dropTestype.SelectedItem.Text.ToString());
                    SQL_PARAMS[13] = OBJ_METHOD.createParams("@TESTINDEX", SqlDbType.VarChar, 500, "");
                    SQL_PARAMS[14] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 500, "OUTPATIENT");
                    SQL_PARAMS[15] = OBJ_METHOD.createParams("@DISCAMT", SqlDbType.Decimal, 0, txtdisc.Text);
                    SQL_PARAMS[16] = OBJ_METHOD.createParams("@PAIDAMT", SqlDbType.Decimal, 0, "0.00");
                    SQL_PARAMS[17] = OBJ_METHOD.createParams("@DUEAMT", SqlDbType.Decimal, 0, "0.00");
                    SQL_PARAMS[18] = OBJ_METHOD.createParams("@DOCID", SqlDbType.Int, 0, droprefdoc.SelectedValue);
                    SQL_PARAMS[19] = OBJ_METHOD.createParams("@OUTDOCKSTS", SqlDbType.Bit, 0, checkdoc.Checked);
                    SQL_PARAMS[20] = OBJ_METHOD.createParams("@OUTDOCNM", SqlDbType.VarChar, 500, txtrefdoc.Text);
                    SQL_PARAMS[21] = OBJ_METHOD.createParams("@PADDRESS", SqlDbType.VarChar, 500, txtaddress.Text);
                    SQL_PARAMS[22] = OBJ_METHOD.createParams("@TIME", SqlDbType.Time, 0, txttime.Text);

                    OBJ_METHOD.ExecuteProceedure("RECP_LAB_REC_INSRTUP", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("commit");

                    }
                    message1 = "alert('" + msg + "')";
                }
            }
            #region old coding
            //using (SqlCommand stock_cmd = new SqlCommand("USP_RECLABTOR", con))
            //{
            //    stock_cmd.CommandType = CommandType.StoredProcedure;
            //    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";

            //    stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    stock_cmd.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtopdno.Text;
            //    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
            //    stock_cmd.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropCorprt.SelectedItem.Text;
            //    stock_cmd.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = txtEmpid.Text;
            //    stock_cmd.Parameters.Add("@PACKAGE", SqlDbType.VarChar).Value = dropPackage.SelectedItem.Text;
            //    stock_cmd.Parameters.Add("@TESTYPEPK", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text;
            //    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //    stock_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
            //    stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = lbltotalprice.Text;
            //    stock_cmd.Parameters.Add("@TOTALAMOUNT", SqlDbType.Decimal).Value = lbltotalamt.Text;
            //    stock_cmd.Parameters.Add("@DISCOUNT", SqlDbType.Decimal).Value = txtdisc.Text;
            //    // stock_cmd.Parameters.Add("@TESTNAME", SqlDbType.VarChar).Value = "0.00";   
            //    stock_cmd.Parameters.Add("@INVID", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = "0.00";
            //    stock_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "0.00";

            //    stock_cmd.ExecuteNonQuery();
            //}
            //-------------------------------------
            //using (SqlCommand cmd = new SqlCommand("RECP_LAB_REC_INSRTUP", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;

            //    cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtopdno.Text;
            //    cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtname.Text;
            //    cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
            //    cmd.Parameters.Add("@UNAME", SqlDbType.VarChar).Value = lblid.Text;
            //    cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
            //    cmd.Parameters.Add("@TESTTYPE", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text.ToString();
            //    cmd.Parameters.Add("@TESTINDEX", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
            //    cmd.Parameters.Add("@DISCAMT", SqlDbType.Decimal).Value = txtdisc.Text;
            //    cmd.Parameters.Add("@PAIDAMT", SqlDbType.Decimal).Value = "0.00";
            //    cmd.Parameters.Add("@DUEAMT", SqlDbType.Decimal).Value = "0.00";
            //    cmd.Parameters.Add("@REFBY", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";

            //    cmd.ExecuteNonQuery();
            //}
            ////---------------------------
            //binddata();
            //con.Close();
            #endregion
            Session["labbill"] = txtid.Text;
            clearcontrol();
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
        Response.Redirect("~/RECEPTION/reception_lab_receipt.aspx");
    }
    public void clearcontrol()
    {
        DataTable dt = (DataTable)ViewState["ITEM"];
        dt.Clear();
        grdtestype.DataSource = dt;
        grdtestype.DataBind();
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        txttime.Text = DateTime.Now.ToString("HH:mm");
       
        divinner2.Visible = false;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {

    }
    protected void droprefdoc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}