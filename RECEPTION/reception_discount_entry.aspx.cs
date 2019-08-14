using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;

public partial class RECEPTION_reception_discount_entry : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    string fr, to;
    string medbill, medcrvbill;
    SqlDataReader rr;
    Double amt = 0;
    DataMathods OBJ_METHOD = new DataMathods();


    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from DISCOUNT_TABLE";
        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("DS{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        lblmid.Text = num1;
        dr.Close();
        con.Close();
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
        lbluid.Text = Session["UID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
      
        if (!IsPostBack)
        {
            binddata();
        }
     
    }
    public void binddata()
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

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DISCOUNT");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@BRANCH_ID", SqlDbType.VarChar, 500, Session["Branch"].ToString());

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_DISCOUNT_ENTRY", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.DataBind();
       
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
              
    }
    public void clear_control()
    {
        
        lblipno.Text = "";
        lblname.Text = "";
        lblbalance.Text = "0";
        lbltotalamt.Text = "0";
        txtamount.Text = "0";
        txtname.Text = "";
    }
    public void clear()
    {
        lblbalance.Text = "0";
        lbltotalamt.Text = "0";
        txtamount.Text = "0";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
       try
        {
            if (droptype.SelectedIndex == 0)
            {
                if (TextBox1.Text == "")
                {
                    string message = "alert('* Please!! Enter The Bed No..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    TextBox1.Focus();
                    return;
                }

                //if (TextBox1.Text != "")
                //{
                //    using (SqlCommand cmd = new SqlCommand("RECP_DISCOUNT_ENTRY", con))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_VALID";
                //        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                //        cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
                //        cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = TextBox1.Text;
                //        //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //        //SqlCommand com = new SqlCommand("select * from BED_TABLE where BEDNO='" + TextBox1.Text + "'", con);
                //        dr = cmd.ExecuteReader();
                //        if (dr.Read())
                //        {
                //            lblipno.Text = dr["VN"].ToString();
                //            lblname.Text = dr["PNAME"].ToString();
                //        }
                //    }
                //}

                SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_2BEDNO");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, TextBox1.Text);

                DataSet DS = OBJ_METHOD.Get_DataSet("RECP_DISCOUNT_ENTRY", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    lblipno.Text = DS.Tables[0].Rows[0]["VN"].ToString();
                    lblname.Text = DS.Tables[0].Rows[0]["PNAME"].ToString();

                }

                SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_VN_MED");
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, TextBox1.Text);

                DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_DISCOUNT_ENTRY", false, true, SQL_PARAMS1);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    medbill = DS1.Tables[0].Rows[0]["BAMT"].ToString();
                    lbltotalamt.Text = DS1.Tables[0].Rows[0]["BAMT"].ToString();
                }


                SqlParameter[] SQL_PARAMS2 = new SqlParameter[2];

                SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_VN_MED_PAY");
                SQL_PARAMS2[1] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblipno.Text);

                DataSet DS2 = OBJ_METHOD.Get_DataSet("RECP_DISCOUNT_ENTRY", false, true, SQL_PARAMS2);
                if (DS2.Tables[0].Rows.Count > 0)
                {           
                   lblbalance.Text = DS2.Tables[0].Rows[0]["AMOUNT"].ToString();                 
                }


                #region oldcode
                //using (SqlCommand cmd = new SqlCommand("RECP_DISCOUNT_ENTRY", con))
                //{
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_2BEDNO";
                //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                //    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
                //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = TextBox1.Text;
                //    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //    //SqlCommand com = new SqlCommand("select * from BED_TABLE where BEDNO='" + TextBox1.Text + "'", con);
                //    dr = cmd.ExecuteReader();
                //    if (dr.Read())
                //    {
                //        lblipno.Text = dr["VN"].ToString();
                //        lblname.Text = dr["PNAME"].ToString();
                //    }
                //}
                //dr.Close();
                //using (SqlCommand cmd1 = new SqlCommand("RECP_DISCOUNT_ENTRY", con))
                //{
                //    cmd1.CommandType = CommandType.StoredProcedure;
                //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_VN_MED";
                //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                //    cmd1.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblipno.Text;
                //    cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                //    //SqlCommand cmd1 = new SqlCommand("SELECT SUM(CHARGES)AS BAMT FROM PA_TRANS WHERE VN='" + lblipno.Text + "' AND DESCRIPTION like 'MEDICINE%'", con);
                //    dr = cmd1.ExecuteReader();
                //    if (dr.Read())
                //    {
                //        medbill = dr["BAMT"].ToString();
                //        lbltotalamt.Text = dr["BAMT"].ToString();

                //    }

                //}
                //dr.Close();

                //using (SqlCommand com3 = new SqlCommand("RECP_DISCOUNT_ENTRY", con))
                //{
                //    com3.CommandType = CommandType.StoredProcedure;
                //    com3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_VN_MED_PAY";
                //    com3.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                //    com3.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblipno.Text;
                //    com3.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                //    //SqlCommand com3 = new SqlCommand("SELECT SUM(-CHARGES) AS AMOUNT FROM PA_TRANS WHERE VN='" + lblipno.Text + "'AND DESCRIPTION='MEDICINE PAYMENT' ", con);
                //    dr = com3.ExecuteReader();
                //    if (dr.Read())
                //    {
                //        lblbalance.Text = dr["AMOUNT"].ToString();
                //    }
                //}
                //dr.Close();
                #endregion
            }
            else
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_2BEDNO");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, TextBox1.Text);

                DataSet DS = OBJ_METHOD.Get_DataSet("RECP_DISCOUNT_ENTRY", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    lblipno.Text = DS.Tables[0].Rows[0]["VN"].ToString();
                    lblname.Text = DS.Tables[0].Rows[0]["PNAME"].ToString();

                }

                SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_PA_MASTER");
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblipno.Text);

                DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_DISCOUNT_ENTRY", false, true, SQL_PARAMS1);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    lbltotalamt.Text = DS1.Tables[0].Rows[0]["CREDIT"].ToString();
                    lblbalance.Text = DS1.Tables[0].Rows[0]["DEBIT"].ToString();
                }
                #region oldcode
                //using (SqlCommand com = new SqlCommand("RECP_DISCOUNT_ENTRY", con))
                //{
                //    com.CommandType = CommandType.StoredProcedure;
                //    com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_2BEDNO";
                //    com.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                //    com.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
                //    com.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = TextBox1.Text;
                //    //SqlCommand com = new SqlCommand("select * from BED_TABLE where BEDNO='" + TextBox1.Text + "'", con);
                //    dr = com.ExecuteReader();
                //    if (dr.Read())
                //    {
                //        lblipno.Text = dr["VN"].ToString();
                //        lblname.Text = dr["PNAME"].ToString();
                //    }
                //}
                //dr.Close();
                //using (SqlCommand com1 = new SqlCommand("RECP_DISCOUNT_ENTRY", con))
                //{
                //    com1.CommandType = CommandType.StoredProcedure;
                //    com1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PA_MASTER";
                //    com1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                //    com1.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblipno.Text;
                //    com1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                //    //SqlCommand com1 = new SqlCommand("select * from PA_MASTER where VN='" + lblipno.Text + "'", con);
                //    dr = com1.ExecuteReader();
                //    if (dr.Read())
                //    {
                //        lbltotalamt.Text = dr["CREDIT"].ToString();
                //        lblbalance.Text = dr["DEBIT"].ToString();
                //    }
                //}
                //dr.Close();
                #endregion
            }
            clear();
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
       

    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_EVENT");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_DISCOUNT_ENTRY", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                btnsave.Visible = false;
                btndelete.Visible = true;
                lblipno.Text = DS1.Tables[0].Rows[0]["PID"].ToString();
                lbldate.Text = DS1.Tables[0].Rows[0]["DATE"].ToString();
                lblmid.Text = DS1.Tables[0].Rows[0]["ID"].ToString();
                TextBox1.Text = DS1.Tables[0].Rows[0]["BEDNO"].ToString();
                lblname.Text = DS1.Tables[0].Rows[0]["NAME"].ToString();
                lblbalance.Text = DS1.Tables[0].Rows[0]["BALANCEAMT"].ToString();
                txtamount.Text =  DS1.Tables[0].Rows[0]["AMOUNT"].ToString();
                lbltotalamt.Text =  DS1.Tables[0].Rows[0]["TOTALAMT"].ToString();
                LBPAIDAMT.Text =  DS1.Tables[0].Rows[0]["AMOUNT"].ToString();
                txtname.Text =  DS1.Tables[0].Rows[0]["AUTHORITY"].ToString();
                droptype.Text =  DS1.Tables[0].Rows[0]["TYPE"].ToString();
                lblmid.Visible = false;

            }
            #region oldcode
            //using (SqlCommand com1 = new SqlCommand("RECP_DISCOUNT_ENTRY", con))
            //{
            //    com1.CommandType = CommandType.StoredProcedure;
            //    com1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT";
            //    com1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    com1.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            //    com1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //    //SqlCommand com = new SqlCommand("SELECT ID,DATE,PID,NAME,BEDNO,TOTALAMT,BALANCEAMT,AMOUNT,TYPE,AUTHORITY FROM DISCOUNT_TABLE WHERE ID='" + slno + "'", con);
            //    dr = com1.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        btnsave.Visible = false;
            //        btndelete.Visible = true;
            //        lblipno.Text = dr["PID"].ToString();
            //        lbldate.Text = dr["DATE"].ToString();
            //        lblmid.Text = dr["ID"].ToString();
            //        TextBox1.Text = dr["BEDNO"].ToString();
            //        lblname.Text = dr["NAME"].ToString();
            //        lblbalance.Text = dr["BALANCEAMT"].ToString();
            //        txtamount.Text = dr["AMOUNT"].ToString();
            //        lbltotalamt.Text = dr["TOTALAMT"].ToString();
            //        LBPAIDAMT.Text = dr["AMOUNT"].ToString();
            //        txtname.Text = dr["AUTHORITY"].ToString();
            //        droptype.Text = dr["TYPE"].ToString();
            //        lblmid.Visible = false;
            //    }
            //}
            //dr.Close();
            #endregion

        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

        SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DISCOUNT");
        SQL_PARAMS1[1] = OBJ_METHOD.createParams("@BRANCH_ID", SqlDbType.VarChar, 500, Session["Branch"].ToString());
        DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_DISCOUNT_ENTRY", false, true, SQL_PARAMS1);
        if (DS1.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = DS1;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();

        }
       
    }
    protected void Btnsave_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (TextBox1.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                TextBox1.Focus();
                return;
            }
            else if (txtname.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (lblipno.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtamount.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtamount.Focus();
                return;
            }
            else if (Convert.ToDecimal(txtamount.Text) <= 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            auto();
            lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");

            SqlParameter[] SQL_PARAMS = new SqlParameter[14];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, lblmid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, lbldate.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, lblname.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblipno.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 200, TextBox1.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@TOTALAMT", SqlDbType.Decimal, 0, lbltotalamt.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@BALANCEAMT", SqlDbType.Decimal, 0, lblbalance.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@AMOUNT", SqlDbType.Decimal, 0, txtamount.Text);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@UID", SqlDbType.VarChar, 500, lblid.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@TYPE", SqlDbType.VarChar, 200, droptype.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@AUTHORITY", SqlDbType.VarChar, 500, txtname.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());

            OBJ_METHOD.ExecuteProceedure("RECP_DISCOUNT_ENTRY_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
            
            if (OBJ_METHOD._RESULT > 0)
            {
                SqlParameter[] SQL_PARAMS1 = new SqlParameter[12];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 0, lbldate.Text);
                SQL_PARAMS1[2] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, lblmid.Text);
                SQL_PARAMS1[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 200, lblipno.Text);
                SQL_PARAMS1[4] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, TextBox1.Text);

                if (droptype.SelectedIndex == 0)
                {
                    SQL_PARAMS1[5] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 200, "DISCOUNT ON MEDICINE CHARGE" + '(' + txtname.Text + ')');
                }
                else
                {
                    SQL_PARAMS1[5] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 200, "DISCOUNT ON OVERALL CHARGE" + '(' + txtname.Text + ')');
                }
                SQL_PARAMS1[6] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 200, '-' + txtamount.Text);
                SQL_PARAMS1[7] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 500, "INPATIENT");
                SQL_PARAMS1[8] = OBJ_METHOD.createParams("@DEBIT", SqlDbType.VarChar, 50, "0");
                SQL_PARAMS1[9] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 200, '-' + txtamount.Text);
                SQL_PARAMS1[10] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 100, lblipno.Text);
                SQL_PARAMS1[11] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 200, "TOTAL DISCOUNT");
               
                OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_CREDIT", "", "", SqlDbType.VarChar, SQL_PARAMS1, true);
                if (OBJ_METHOD._RESULT > 0)
                {
                    //OBJ_METHOD.commitOrRollbackTran("commit");
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Due to some issues, Data not saved.')";
                }
               
            }
            OBJ_METHOD.commitOrRollbackTran("commit");
            message1 = "alert('Discount Details SAVED successfully...')";
            clear_control();
            #region oldcode
            //using (SqlCommand cm = new SqlCommand("RECP_DISCOUNT_ENTRY_INSERT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    //SqlCommand cm = new SqlCommand("insert into DISCOUNT_TABLE(ID,DATE,PID,NAME,BEDNO,TOTALAMT,BALANCEAMT,AMOUNT,UID,TYPE,AUTHORITY) VALUES (@ID,@DATE,@PID,@NAME,@BEDNO,@TOTALAMT,@BALANCEAMT,@AMOUNT,@UID,@TYPE,@AUTHORITY)", con);
            //    cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblmid.Text;
            //    cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = lbldate.Text;
            //    cm.Parameters.Add("@NAME", SqlDbType.VarChar).Value = lblname.Text;
            //    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblipno.Text;
            //    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = TextBox1.Text;
            //    cm.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = lbltotalamt.Text;
            //    cm.Parameters.Add("@BALANCEAMT", SqlDbType.VarChar).Value = lblbalance.Text;
            //    cm.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = txtamount.Text;
            //    cm.Parameters.Add("@UID", SqlDbType.VarChar).Value = lblid.Text;
            //    cm.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = droptype.Text;
            //    cm.Parameters.Add("@AUTHORITY", SqlDbType.VarChar).Value = txtname.Text;
            //    cm.ExecuteNonQuery();
            //}

            //using (SqlCommand cm1 = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            //{
            //    cm1.CommandType = CommandType.StoredProcedure;
            //    cm1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cm1.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = lbldate.Text;
            //    cm1.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = lblmid.Text;
            //    cm1.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblipno.Text;
            //    cm1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = TextBox1.Text;
            //    if (droptype.SelectedIndex == 0)
            //    {
            //        cm1.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "DISCOUNT ON MEDICINE CHARGE" + '(' + txtname.Text + ')';
            //    }
            //    else
            //    {
            //        cm1.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "DISCOUNT ON OVERALL CHARGE" + '(' + txtname.Text + ')';
            //    }
            //    cm1.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = '-' + txtamount.Text;
            //    cm1.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //    cm1.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
            //    cm1.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = '-' + txtamount.Text;
            //    cm1.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblipno.Text;
            //    cm1.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "TOTAL DISCOUNT";
            //    cm1.ExecuteNonQuery();
            //    con.Close();
            //}
            #endregion
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
       
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, lblmid.Text);

             OBJ_METHOD.ExecuteProceedure("RECP_DISCOUNT_ENTRY_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

             if (OBJ_METHOD._RESULT > 0)
             {
                 SqlParameter[] SQL_PARAMS1 = new SqlParameter[4];

                 SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                 SQL_PARAMS1[1] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, lblmid.Text);
                 if (droptype.SelectedIndex == 0)
                 {
                     SQL_PARAMS1[2] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 200, "DISCOUNT ON MEDICINE CHARGE" + '(' + txtname.Text + ')');
                 }
                 else
                 {
                     SQL_PARAMS1[2] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 200, "DISCOUNT ON OVERALL CHARGE" + '(' + txtname.Text + ')');
                 }
                 SQL_PARAMS1[3] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 100, lblipno.Text);

                 OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_CREDIT", "", "", SqlDbType.VarChar, SQL_PARAMS1, true);
                 if (OBJ_METHOD._RESULT > 0)
                 {
                     //OBJ_METHOD.commitOrRollbackTran("commit");
                     btnsave.Visible = true;
                     btndelete.Visible = false;
                 }
                 else
                 {
                     OBJ_METHOD.commitOrRollbackTran("rollback");
                     message1 = "alert('Due to some issues, Data not saved.')";
                 }
                
             }
             OBJ_METHOD.commitOrRollbackTran("commit");
             binddata();
             message1 = "alert('Discount Details Deleted!!!!..')";
            #region old code

            //using (SqlCommand cmd = new SqlCommand("RECP_DISCOUNT_ENTRY", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblmid.Text.ToString();
            //    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter da = new SqlDataAdapter("delete from DISCOUNT_TABLE where ID='" + lblmid.Text.ToString() + "'", con);
            //    ds = new DataSet();
            //    da.Fill(ds);

            //    using (SqlCommand cm1 = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            //    {
            //        cm1.CommandType = CommandType.StoredProcedure;
            //        cm1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //        cm1.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = lbldate.Text;
            //        cm1.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = lblmid.Text;
            //        cm1.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblipno.Text;
            //        cm1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = TextBox1.Text;
            //        if (droptype.SelectedIndex == 0)
            //        {
            //            cm1.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "DISCOUNT ON MEDICINE CHARGE" + '(' + txtname.Text + ')';
            //        }
            //        else
            //        {
            //            cm1.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "DISCOUNT ON OVERALL CHARGE" + '(' + txtname.Text + ')';
            //        }
            //        cm1.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = '-' + LBPAIDAMT.Text;
            //        cm1.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //        cm1.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
            //        cm1.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = '-' + LBPAIDAMT.Text;
            //        cm1.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblipno.Text;
            //        cm1.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "TOTAL DISCOUNT";
            //        cm1.ExecuteNonQuery();
            //    }
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
            clear_control();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
       
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear_control();
    }
    protected void chkRelation_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRelation.Checked == true)
        {
            try
            {
                SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_CHECKED");
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, lblmid.Text.ToString());

                DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_DISCOUNT_ENTRY", false, true, SQL_PARAMS1);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    Double stock = 0;
                    stock = Convert.ToDouble(DS1.Tables[0].Rows[0]["OTH"].ToString());
                    amt = Math.Round(((Convert.ToDouble(lbltotalamt.Text) / 100) * (Convert.ToDouble(stock))));
                    txtamount.Text = amt.ToString();
                }

            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
        }
        else
        {
            txtamount.Text = "";
        }
    }
}