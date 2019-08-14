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


public partial class NURSE_nurse_radiology_requisition : System.Web.UI.Page
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
    string SS, succ; 
    decimal amount = 0;
    DataMathods OBJ_METHOD = new DataMathods();

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
                cmd.CommandText = "select distinct VN FROM BED_TABLE where VN like '%'+@SearchText+'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
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
            //  testype();
            binddata();
            //  auto();
        }
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
    }
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID as ID from TBL_RADIOLGYREQ";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("RL{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;

        dr.Close();
        con.Close();
    }

    public void binddata()
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELCT_RAD");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_RAD_REC", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropPackage.DataSource = Ds;
                dropPackage.DataTextField = "RADIOLOGYNAME";
                dropPackage.DataValueField = "ID";
                dropPackage.DataBind();
                dropPackage.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            //----------------------


            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_RADREQ_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RECP_RAD_REC", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                grdpackge.DataSource = Ds1;
                grdpackge.DataBind();
            }

            //--------for FINANCIAL YEAR------------
        
            SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }


            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_RAD_CATA");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("RECP_RAD_REC", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                dropTestype.DataSource = Ds2;
                dropTestype.DataTextField = "NAME";
                dropTestype.DataValueField = "ID";
                dropTestype.DataBind();
                dropTestype.Items.Insert(0, new ListItem("Please Select", "0"));
            }

            txtdisc.Text = "0";
            lbltotalamt.Text = "0";
            lbltotalprice.Text = "0";
            
        }
       
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
    }
    protected void txtipdno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select slno,VN,PNAME,UHID,BEDNO from  BED_TABLE where VN='" + txtipdno.Text + "'", false, false);

            if (Ds1.Tables[0].Rows.Count > 0)
            {
                txtname.Text = Ds1.Tables[0].Rows[0]["PNAME"].ToString();
                LBLUHID.Text = Ds1.Tables[0].Rows[0]["UHID"].ToString();
                lblbed.Text = Ds1.Tables[0].Rows[0]["BEDNO"].ToString();
            }
            else
            {
                string message = "alert('* No record found.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
        //SqlCommand com = new SqlCommand("select slno,VN,PNAME,UHID,BEDNO from  BED_TABLE where VN='" + txtipdno.Text + "'", con);
        //dr = com.ExecuteReader();
        //if (dr.Read())
        //{
        //    txtname.Text = dr["PNAME"].ToString();
        //    LBLUHID.Text = dr["UHID"].ToString();
        //    lblbed.Text = dr["BEDNO"].ToString();
        //    //droptesttype.Text = dr["SELECTYPE"].ToString();
        //}
        //dr.Close();
        //con.Close();
    }
    protected void chkTestType_CheckedChanged(object sender, EventArgs e)
    {
        if (chkTestType.Checked)
        {
            dropTestype.Visible = true;
            dropPackage.Visible = false;
            chkPackage.Checked = false;
            divinner2.Visible = false;
            dropPackage.SelectedIndex = 0;
            // divinner1.Visible = false;
        }
        else
        {

            dropTestype.Visible = false;
            divinner2.Visible = false;
            chkPackage.Enabled = true;
        }
    }
    protected void dropTestype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_CAT_COMP");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, dropTestype.SelectedItem.Text.ToString());

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("RECP_RAD_REC", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                grdtestype.DataSource = Ds2;
                grdtestype.DataBind();
            }
            divinner2.Visible = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void chkPackage_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPackage.Checked)
        {
            dropPackage.Visible = true;
            dropTestype.Visible = false;
            chkTestType.Checked = false;
            divinner2.Visible = false;
            dropTestype.SelectedIndex = 0;
            // divinner1.Visible = false;
        }
        else
        {
            dropPackage.Visible = false;
            divinner2.Visible = false;
            chkPackage.Enabled = true;
        }
    }
    protected void dropPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_MULTI");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@RADIOLOGYNAME", SqlDbType.VarChar, 500, dropTestype.SelectedItem.Text.ToString());

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("RECP_RAD_REC", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                grdtestype.DataSource = dt;
                grdtestype.DataBind();
                divinner2.Visible = true;
            }
            else
            {
                divinner2.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {

            if (txtipdno.Text == "")
            {
                string message = "alert('* Please Enter IPD NO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            if (txtname.Text == "")
            {
                string message = "alert('* Please Enter Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            auto();
            radiolgy_item();
            if (SS == "FALSE")
            {
                string message = "alert('*Please select any item to save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "REDUPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@RCHARGE", SqlDbType.Decimal, 0, lbltotalamt.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@UHID", SqlDbType.VarChar, 500, LBLUHID.Text);

            OBJ_METHOD.ExecuteProceedure("per_tran", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                SQL_PARAMS = new SqlParameter[13];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 0, txtdate.Text);//invIt.Text
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, txtid.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, txtipdno.Text);
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@Bedno", SqlDbType.VarChar, 500, lblbed.Text);
                SQL_PARAMS[5] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "RADIOLOGY BILL");
                SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                SQL_PARAMS[8] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, lbltotalamt.Text);
                SQL_PARAMS[9] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 500, "INPATIENT");
                SQL_PARAMS[10] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 500, lbltotalamt.Text);
                SQL_PARAMS[11] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, txtipdno.Text);
                SQL_PARAMS[12] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, "RADIOLOGY CHARGES");

                OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_CREDIT", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    SQL_PARAMS = new SqlParameter[14];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@OPDNO", SqlDbType.VarChar, 500, txtipdno.Text.ToUpper());
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 0, txtname.Text);

                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@PACKAGE", SqlDbType.VarChar, 500, dropPackage.SelectedItem.Text);
                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@TESTYPE", SqlDbType.VarChar, 500, dropTestype.SelectedItem.Text);
                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@PRICE", SqlDbType.VarChar, 500, lbltotalprice.Text);

                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@TOTDISC", SqlDbType.VarChar, 500, txtdisc.Text);
                    SQL_PARAMS[10] = OBJ_METHOD.createParams("@TOTAMT", SqlDbType.VarChar, 500, lbltotalamt.Text);
                    SQL_PARAMS[11] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, txtdate.Text);
                    SQL_PARAMS[12] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
                    SQL_PARAMS[13] = OBJ_METHOD.createParams("@USERID", SqlDbType.VarChar, 500, lblid.Text);

                    OBJ_METHOD.ExecuteProceedure("USP_RADIOLOGYRQ", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("commit");

                    }
                    message1 = "alert('" + OBJ_METHOD._objOut + "')";
                }
            }
            #region oldcode
            //using (SqlCommand stock_cmd = new SqlCommand("USP_RADIOLOGYRQ", con))
            //{
            //    stock_cmd.CommandType = CommandType.StoredProcedure;
            //    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";

            //    stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    stock_cmd.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtipdno.Text.ToUpper();
            //    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;

            //    stock_cmd.Parameters.Add("@PACKAGE", SqlDbType.VarChar).Value = dropPackage.SelectedItem.Text;
            //    stock_cmd.Parameters.Add("@TESTYPE", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text;
            //    stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
            //    stock_cmd.Parameters.Add("@TOTDISC", SqlDbType.VarChar).Value = txtdisc.Text;
            //    stock_cmd.Parameters.Add("@TOTAMT", SqlDbType.VarChar).Value = lbltotalamt.Text;

            //    stock_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
            //    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //    stock_cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lblid.Text;

            //    stock_cmd.Parameters.Add("@INVID", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@PRICEIT", SqlDbType.Decimal).Value = "0.00";
            //    stock_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.ExecuteNonQuery();
            //}
            //using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd");
            //    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
            //    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtipdno.Text;
            //    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
            //    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "RADIOLOGY BILL ";
            //    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = lbltotalamt.Text;
            //    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
            //    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = lbltotalamt.Text;
            //    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtipdno.Text;
            //    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "RADIOLOGY CHARGES";
            //    cm.ExecuteNonQuery();
            //}
            //using (SqlCommand cm = new SqlCommand("per_tran", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "REDUPDATE";
            //    cm.Parameters.Add("@REF", SqlDbType.VarChar).Value = "";
            //    cm.Parameters.Add("@UHID", SqlDbType.VarChar).Value = LBLUHID.Text;
            //    cm.Parameters.Add("@PCHARGE", SqlDbType.Decimal).Value = "0.00";
            //    cm.Parameters.Add("@PP", SqlDbType.Decimal).Value = "0.00";
            //    cm.Parameters.Add("@LCHARGE", SqlDbType.Decimal).Value = "0.00";
            //    cm.Parameters.Add("@LP", SqlDbType.Decimal).Value = "0.00";
            //    cm.Parameters.Add("@BCHARGE", SqlDbType.Decimal).Value = "0.00";
            //    cm.Parameters.Add("@BP", SqlDbType.Decimal).Value = "0.00";
            //    cm.Parameters.Add("@MISCHARGE", SqlDbType.Decimal).Value = "0.00";
            //    cm.Parameters.Add("@MP", SqlDbType.Decimal).Value = "0.00";
            //    cm.Parameters.Add("@RCHARGE", SqlDbType.Decimal).Value = lbltotalamt.Text;
            //    cm.Parameters.Add("@RP", SqlDbType.Decimal).Value = "0.00";
            //    cm.ExecuteNonQuery();
            //}
            //binddata();
            //con.Close();
            #endregion
            Session["RADID"] = txtid.Text;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/NURSE/nurse_RadioNurseBill.aspx");
    }
    public void radiolgy_item()
    {
        try
        {
            int chkedcounter = 0;
            int correctinput = 0;


            SS = "FALSE";

            foreach (GridViewRow row in grdtestype.Rows)
            {
                OBJ_METHOD = new DataMathods();
                if (row.RowType == DataControlRowType.DataRow)
                {

                    //  Label typeIt = (row.Cells[0].FindControl("lbltype") as Label);
                    Label invIt = (row.Cells[0].FindControl("lblinv") as Label);
                    TextBox priceIt = (row.Cells[1].FindControl("lblprice") as TextBox);
                    CheckBox chkRow = (row.Cells[2].FindControl("chkRow") as CheckBox);

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
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@PRICEIT", SqlDbType.Decimal, 0, priceIt.Text);
                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@CHKSEL", SqlDbType.VarChar, 500, chkRow.Checked);


                        OBJ_METHOD.ExecuteProceedure("USP_RADIOLOGYRQ", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                        if (OBJ_METHOD._RESULT > 0)
                        {
                            correctinput++;
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
    protected void chkRow_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdtestype.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[2].FindControl("chkRow") as CheckBox);

                if (chkRow.Checked)
                {
                    //var value = row.FindControl("lblname") as Label;
                    // var inv = row.FindControl("lblinv") as Label;
                    var AMT = row.FindControl("lblprice") as TextBox;
                    //var re = row.FindControl("txtref") as TextBox;
                    //var unit = row.FindControl("txtunit") as TextBox;
                    //var AMT = row.FindControl("lblprice") as TextBox;                   
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
    protected void grdpackge_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //var slno = grdpackge.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //con.Open();
        //SqlCommand com = new SqlCommand("select a.OPDNO,a.NAME,a.ID,a.TOTAMT,a.DATE,a.PRICE,a.TOTAMT,a.TOTDISC,a.PACKAGE,a.TESTYPE,b.INV,b.PRICEIT,b.CHKSEL from TBL_RADIOLGYREQ a,TBL_RADIOLGYRE_ITEM b  where a.ID=b.ID and a.ID='" + slno + "'", con);
        //dr = com.ExecuteReader();
        //if (dr.Read())
        //{
        //    btncreate.Visible = false;
        //    btnupdate.Visible = true;
        //    txtid.Text = slno.ToString();
        //    txtipdno.Text = dr["OPDNO"].ToString();
        //    txtname.Text = dr["NAME"].ToString();
        //    lbltotalprice.Text = dr["PRICE"].ToString();
        //    txtdisc.Text = dr["TOTDISC"].ToString();
        //    lbltotalamt.Text = dr["TOTAMT"].ToString();
        //    LBPAIDAMT.Text = dr["TOTAMT"].ToString();

        //    dropPackage.SelectedItem.Text = dr["PACKAGE"].ToString();
        //    dropTestype.SelectedItem.Text = dr["TESTYPE"].ToString();
        //    dropPackage.Visible = true;
        //    dropTestype.Visible = true;
        //    dr.Close();

        //    SqlDataAdapter da = new SqlDataAdapter(com);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    grdtestype.SelectedIndex = 0;
        //    grdtestype.DataSource = dt;
        //    grdtestype.DataKeyNames = new string[] { "ID" };
        //    grdtestype.DataBind();
        //    divinner2.Visible = true;
        //    //  chkPackage.Checked = true;
        //    //   chkPackage.Checked = true;
        //}

        //con.Close();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtipdno.Text == "")
            {
                string message = "alert('* Please Enter OPDNO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            if (txtname.Text == "")
            {
                string message = "alert('* Please Enter Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();


            radiolgy_itemUpdat();
            //if (SS == "FALSE")
            //{
            //    string message = "alert('*Please select any item to save.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //    return;
            //}
            using (SqlCommand stock_cmd = new SqlCommand("USP_RADIOLOGYRQ", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";

                stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                stock_cmd.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtipdno.Text.ToUpper();
                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;

                stock_cmd.Parameters.Add("@PACKAGE", SqlDbType.VarChar).Value = dropPackage.SelectedItem.Text;
                stock_cmd.Parameters.Add("@TESTYPE", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text;
                stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
                stock_cmd.Parameters.Add("@TOTDISC", SqlDbType.VarChar).Value = txtdisc.Text;
                stock_cmd.Parameters.Add("@TOTAMT", SqlDbType.VarChar).Value = lbltotalamt.Text;

                stock_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lblid.Text;

                stock_cmd.Parameters.Add("@INVID", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@PRICEIT", SqlDbType.Decimal).Value = "0.00";
                stock_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.ExecuteNonQuery();
            }
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE1";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd");
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtipdno.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "RADIOLOGY BILL ";
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = LBPAIDAMT.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = LBPAIDAMT.Text;
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtipdno.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "RADIOLOGY CHARGES";
                cm.ExecuteNonQuery();
            }
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd");
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtipdno.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "RADIOLOGY BILL ";
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = lbltotalamt.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = lbltotalamt.Text;
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtipdno.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "RADIOLOGY CHARGES";
                cm.ExecuteNonQuery();
            }
            binddata();
            con.Close();
            Session["RADID"] = txtid.Text;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/NURSE/nurse_RadioNurseBill.aspx");
    }
    public void radiolgy_itemUpdat()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        //  SS = "FALSE";
        foreach (GridViewRow row in grdtestype.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                //  Label typeIt = (row.Cells[0].FindControl("lbltype") as Label);
                //Label nameIt = (row.Cells[0].FindControl("lblname") as Label);
                Label invIt = (row.Cells[0].FindControl("lblinv") as Label);
                TextBox priceIt = (row.Cells[1].FindControl("lblprice") as TextBox);
                CheckBox chkRow = (row.Cells[2].FindControl("chkRow") as CheckBox);

                if (chkRow.Checked)
                {
                    var INV = Convert.ToInt32(grdtestype.DataKeys[row.RowIndex].Values[0]);
                    //  SS = "TRUE";
                    using (SqlCommand stock_cmd = new SqlCommand("USP_RADIOLOGYRQ", con))
                    {
                        stock_cmd.CommandType = CommandType.StoredProcedure;
                        stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDUPDATE";

                        stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                        stock_cmd.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtipdno.Text;
                        stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
                        stock_cmd.Parameters.Add("@PACKAGE", SqlDbType.VarChar).Value = dropPackage.SelectedItem.Text;
                        stock_cmd.Parameters.Add("@TESTYPE", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text;
                        stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
                        stock_cmd.Parameters.Add("@TOTDISC", SqlDbType.VarChar).Value = txtdisc.Text;
                        stock_cmd.Parameters.Add("@TOTAMT", SqlDbType.VarChar).Value = lbltotalamt.Text;
                        stock_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
                        stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                        stock_cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lblid.Text;
                        // stock_cmd.Parameters.Add("@TESTNAME", SqlDbType.VarChar).Value = nameIt.Text;  
                        stock_cmd.Parameters.Add("@INVID", SqlDbType.VarChar).Value = INV.ToString();
                        stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = invIt.Text;
                        stock_cmd.Parameters.Add("@PRICEIT", SqlDbType.Decimal).Value = priceIt.Text;
                        stock_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = chkRow.Checked;

                        stock_cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        //}
        //catch (Exception ex)
        //{

        //}
        con.Close();
    }
    //using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
    //        {
    //            cm.CommandType = CommandType.StoredProcedure;
    //            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
    //            cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
    //            cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
    //            cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
    //            cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
    //            cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LAB BILL " + droptesttype.SelectedItem.Text.ToString();
    //            cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = LBPAIDAMT.Text.ToString();
    //            cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.Text;
    //            cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
    //            cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = LBPAIDAMT.Text.ToString();
    //            cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtIPNO.Text;
    //            cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "LAB CHARGES";
    //            cm.ExecuteNonQuery();
    //        }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
        Session["RADID"] = gr.Cells[1].Text;
        Response.Redirect("~/NURSE/nurse_RadioNurseBill.aspx");
        con.Close();
    }
    protected void txtdisc_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdtestype.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[2].FindControl("chkRow") as CheckBox);

                if (chkRow.Checked)
                {
                    //var value = row.FindControl("lblname") as Label;
                    // var inv = row.FindControl("lblinv") as Label;
                    var AMT = row.FindControl("lblprice") as TextBox;
                    //var re = row.FindControl("txtref") as TextBox;
                    //var unit = row.FindControl("txtunit") as TextBox;
                    //var AMT = row.FindControl("lblprice") as TextBox;                   
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
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/NURSE/nurse_radiology_requisition.aspx");
    }
    protected void grdpackge_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_RADREQ_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RECP_RAD_REC", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                grdpackge.DataSource = Ds1;
                grdpackge.PageIndex = e.NewPageIndex;
                grdpackge.DataKeyNames = new string[] { "ID" };
                grdpackge.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void grdtestype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}