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

public partial class STOREKEEPER_store_vendor_contract : System.Web.UI.Page
{
    string num1 = "0";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    double totalamt1, totamt, totalgstamt, dis, dism;
    decimal a, b, c, d, e, f, g, h, j, amount, gstamount;
    GridViewRow gr;
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
                cmd.CommandText = "select DISTINCT QUTIONNO from QUOTATION_TBL where QUTIONNO like '%'+@SearchText+'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = con;

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["QUTIONNO"]));
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
        string qry1 = "select ID AS ID from VENDOR_COTRACT_TBL";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        //num1 = string.Format("PU{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtContrtno.Text = "CN-" + num1 + 1 + "-" + lblfyear.Text;
        dr.Close();
        con.Close();
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

            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("SP_Vendor_contract", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                grdVendContrct.DataSource = Ds;
                grdVendContrct.DataKeyNames = new string[] { "CONTRACTID" };
                grdVendContrct.DataBind();
            }
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select NAME1,ID FROM VENDER_MASTER_TABLE where Branch_ID=" + Session["Branch"] + "", false, false);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                ddVendor.DataSource = Ds1;
                ddVendor.DataTextField = "NAME1";
                ddVendor.DataValueField = "ID";
                ddVendor.DataBind();
                ddVendor.Items.Insert(0,new ListItem("Please Select","0"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        #region oldcode
        //// SqlDataAdapter da = new SqlDataAdapter("select a.CONTRACTID,CONVERT(varchar,a.DATE, 105) DATE,a.QUTIONNO,b.NAME1 from  VENDOR_COTRACT_TBL a,VENDER_MASTER_TABLE b where a.VENDERNAME=b.ID  ORDER BY a.CONTRACTID DESC", con);
        //using (SqlCommand GRN_cmd = new SqlCommand("SP_Vendor_contract", con))
        //{
        //    GRN_cmd.CommandType = CommandType.StoredProcedure;
        //    GRN_cmd.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT";
        //    SqlDataAdapter da = new SqlDataAdapter(GRN_cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    grdVendContrct.DataSource = dt;
        //    // grdVendContrct.DataKeyNames = new string[] { "ID" };
        //    grdVendContrct.DataBind();
        //}
        ////----------------------------
        //da1 = new SqlDataAdapter("select NAME1,ID FROM VENDER_MASTER_TABLE ", con);
        //DataTable ds1 = new DataTable();
        //da1.Fill(ds1);
        //ddVendor.DataSource = ds1;
        //ddVendor.DataTextField = "NAME1";
        //ddVendor.DataValueField = "ID";
        //ddVendor.DataBind();
        //ddVendor.Items.Insert(0, "Please Select");
        #endregion
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
        lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {
            binddata();
            // bindTGrid();
        }
        con.Close();
    }
    public void bindTGrid()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[5] { new DataColumn("NAME"), new DataColumn("VUNIT"), new DataColumn("QTY"), new DataColumn("PRICE"), new DataColumn("TOTAMT") });
        ViewState["ITEM"] = dt;
        // this.BindGrid();
    }
  
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtdate.Text == "")
            {
                string message = "alert('* Enter the date.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdate.Focus();
                return;
            }
            else if (txtInvoice.Text == "")
            {
                string message = "alert('* Enter the Contract No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtContrtno.Focus();
                return;
            }
            else if (ddVendor.SelectedIndex == 0)
            {
                string message = "alert('* Select Vendor.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                ddVendor.Focus();
                return;
            }
            else if (grdVendor.Rows.Count<=0)
            {
                string message = "alert('* Enter a valid Quotation No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtQuotaion.Focus();
                return;
            }
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[11];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@CONTRACTID", SqlDbType.VarChar, 500, txtContrtno.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@VENDERNAME", SqlDbType.VarChar, 500, ddVendor.SelectedValue);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);

            SQL_PARAMS[7] = OBJ_METHOD.createParams("@QUTIONNO", SqlDbType.VarChar, 500, txtQuotaion.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@TOTPRICE", SqlDbType.Decimal, 0, lbltotalprice.Text);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@TOTGSTAMT", SqlDbType.Decimal, 0, lblgstamt.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@GRANDTOT", SqlDbType.Decimal, 0, lblgrandtotal.Text);
            OBJ_METHOD.ExecuteProceedure("USP_VENDOR_COTRACT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
            if (OBJ_METHOD._RESULT > 0)
            {
                int chkedcounter = 0;
                int correctinput = 0;
                if (OBJ_METHOD._RESULT > 0)
                {
                    foreach (GridViewRow row in grdVendor.Rows)
                    {
                        CheckBox chkRow = (row.Cells[1].FindControl("chkQuot") as CheckBox);
                        Label hsncodIt = (row.Cells[2].FindControl("lbl_hsncode") as Label);
                        Label nameIt = (row.Cells[3].FindControl("lbl_name") as Label);
                        Label unitIt = (row.Cells[4].FindControl("lbl_unit") as Label);
                        TextBox quantyIt = (row.Cells[5].FindControl("txt_qty") as TextBox);
                        TextBox priceIt = (row.Cells[6].FindControl("txt_price") as TextBox);
                        Label amtIt = (row.Cells[7].FindControl("lbl_Amt") as Label);
                        Label cgstIt = (row.Cells[8].FindControl("lbl_cgst") as Label);
                        Label sgstIt = (row.Cells[9].FindControl("lbl_sgst") as Label);
                        Label igstIt = (row.Cells[10].FindControl("lbl_igst") as Label);
                        Label gstamtIt = (row.Cells[11].FindControl("lbl_gstamt") as Label);
                        Label totamtIt = (row.Cells[12].FindControl("lbl_totamt") as Label);
                        chkedcounter++;
                        SQL_PARAMS = new SqlParameter[15];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDINSERT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@CONTRACTID", SqlDbType.VarChar, 500, txtContrtno.Text);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@ITEM", SqlDbType.VarChar, 500, nameIt.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, unitIt.Text);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@QTY", SqlDbType.Decimal, 0, quantyIt.Text);
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, priceIt.Text);
                        SQL_PARAMS[8] = OBJ_METHOD.createParams("@TOTAMT", SqlDbType.Decimal, 0, totamtIt.Text);
                        SQL_PARAMS[9] = OBJ_METHOD.createParams("@AMOUNT", SqlDbType.Decimal, 0, amtIt.Text);
                        SQL_PARAMS[10] = OBJ_METHOD.createParams("@CGST", SqlDbType.Decimal, 0, cgstIt.Text);
                        SQL_PARAMS[11] = OBJ_METHOD.createParams("@SGST", SqlDbType.Decimal, 0, sgstIt.Text);
                        SQL_PARAMS[12] = OBJ_METHOD.createParams("@GSTAMT", SqlDbType.Decimal, 0, gstamtIt.Text);
                        SQL_PARAMS[13] = OBJ_METHOD.createParams("@CHKSEL", SqlDbType.VarChar, 500, chkRow.Checked);
                        SQL_PARAMS[14] = OBJ_METHOD.createParams("@HSNCODE", SqlDbType.Decimal, 0, hsncodIt.Text);
                        OBJ_METHOD.ExecuteProceedure("USP_VENDOR_COTRACT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
                        if (OBJ_METHOD._RESULT > 0)
                        {
                            correctinput++;
                        }
                    }
                    if (chkedcounter == correctinput && chkedcounter > 0)
                    {

                        OBJ_METHOD.commitOrRollbackTran("commit");
                        message1 = "alert('" + OBJ_METHOD._objOut + "')";
                        binddata();
                        clearfield();
                    }
                    else
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        message1 = "alert('Error occurred while processing data... Rolling back...')";
                    }
                }
            }
            #region insert code
            //using (SqlCommand VN_cmd = new SqlCommand("USP_VENDOR_COTRACT", con))
            //{
            //    VN_cmd.CommandType = CommandType.StoredProcedure;
            //    VN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    VN_cmd.Parameters.Add("@CONTRACTID", SqlDbType.VarChar).Value = txtContrtno.Text;
            //    VN_cmd.Parameters.Add("@VENDERNAME", SqlDbType.VarChar).Value = ddVendor.SelectedValue;
            //    VN_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //    VN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

            //    VN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = txtQuotaion.Text;
            //    VN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
            //    VN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = lblgstamt.Text;
            //    VN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = lblgrandtotal.Text;

            //    VN_cmd.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = "";
            //    VN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
            //    VN_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = 0.00;

            //    VN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "";
            //    VN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "";

            //    VN_cmd.ExecuteNonQuery();

            //}
            //foreach (GridViewRow row in grdVendor.Rows)
            //{
            //    CheckBox chkRow = (row.Cells[1].FindControl("chkQuot") as CheckBox);
            //    Label hsncodIt = (row.Cells[2].FindControl("lbl_hsncode") as Label);
            //    Label nameIt = (row.Cells[3].FindControl("lbl_name") as Label);
            //    Label unitIt = (row.Cells[4].FindControl("lbl_unit") as Label);
            //    TextBox quantyIt = (row.Cells[5].FindControl("txt_qty") as TextBox);
            //    TextBox priceIt = (row.Cells[6].FindControl("txt_price") as TextBox);
            //    Label amtIt = (row.Cells[7].FindControl("lbl_Amt") as Label);
            //    Label cgstIt = (row.Cells[8].FindControl("lbl_cgst") as Label);
            //    Label sgstIt = (row.Cells[9].FindControl("lbl_sgst") as Label);
            //    Label igstIt = (row.Cells[10].FindControl("lbl_igst") as Label);
            //    Label gstamtIt = (row.Cells[11].FindControl("lbl_gstamt") as Label);
            //    Label totamtIt = (row.Cells[12].FindControl("lbl_totamt") as Label);


            //    using (SqlCommand MRitem_cmd = new SqlCommand("USP_VENDOR_COTRACT", con))
            //    {
            //        MRitem_cmd.CommandType = CommandType.StoredProcedure;
            //        MRitem_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
            //        MRitem_cmd.Parameters.Add("@CONTRACTID", SqlDbType.VarChar).Value = txtContrtno.Text;
            //        MRitem_cmd.Parameters.Add("@VENDERNAME", SqlDbType.VarChar).Value = ddVendor.SelectedValue;
            //        MRitem_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //        MRitem_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

            //        MRitem_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = "";
            //        MRitem_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = 0.00;
            //        MRitem_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = 0.00;
            //        MRitem_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = 0.00;


            //        MRitem_cmd.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = nameIt.Text;
            //        MRitem_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitIt.Text;
            //        MRitem_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = quantyIt.Text;
            //        MRitem_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = priceIt.Text;
            //        MRitem_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = totamtIt.Text;

            //        MRitem_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = amtIt.Text;
            //        MRitem_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = cgstIt.Text;
            //        MRitem_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = sgstIt.Text;
            //        MRitem_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = igstIt.Text;
            //        MRitem_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = gstamtIt.Text;
            //        MRitem_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = chkRow.Checked;
            //        MRitem_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = hsncodIt.Text;


            //        MRitem_cmd.ExecuteNonQuery();
            //    }
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
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        binddata();
        clearfield();
    }
    public void clearfield()
    {
        txtContrtno.Text = "";
        txtdate.Text = "";
        txtQuotaion.Text = "";
        txtInvoice.Text = "";
        ddVendor.SelectedIndex = 0;
        lblgrandtotal.Text = "0";
        lblgstamt.Text = "0";
        lbltotalprice.Text = "0";
        btncreate.Visible = true;
        btndelete.Visible = false;
        btnupdate.Visible = false;
        grdVendor.DataSource = null;
        grdVendor.DataBind();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("SELECT a.QUTIONID,a.QUTIONNO,b.QUTIONID,b.NAME,b.UNIT,b.QUANTITY,b.PRICE,b.AMOUNT,b.CGST,b.SGST,b.IGST,b.GSTAMT,b.TOTAMT,b.CHKSEL,b.HSNCODE,a.TOTPRICE,a.TOTGSTAMT,a.GRANDTOT FROM QUOTATION_TBL a,QUOTATION_ITEM_TBL b WHERE a.QUTIONID=b.QUTIONID and  a.QUTIONNO='" + txtQuotaion.Text + "' where Branch_ID=" + Session["Branch"] + "", false, false);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                grdVendor.DataSource = Ds1;
                grdVendor.DataBind();
                lbltotalprice.Text = Ds1.Tables[0].Rows[0]["TOTPRICE"].ToString();
                lblgstamt.Text = Ds1.Tables[0].Rows[0]["TOTGSTAMT"].ToString();
                lblgrandtotal.Text = Ds1.Tables[0].Rows[0]["GRANDTOT"].ToString();
            }
            //SqlCommand COM = new SqlCommand("SELECT a.QUTIONID,a.QUTIONNO,b.QUTIONID,b.NAME,b.UNIT,b.QUANTITY,b.PRICE,b.AMOUNT,b.CGST,b.SGST,b.IGST,b.GSTAMT,b.TOTAMT,b.CHKSEL,b.HSNCODE,a.TOTPRICE,a.TOTGSTAMT,a.GRANDTOT FROM QUOTATION_TBL a,QUOTATION_ITEM_TBL b WHERE a.QUTIONID=b.QUTIONID and  a.QUTIONNO='" + txtQuotaion.Text + "'", con);
            //SqlDataAdapter da = new SqlDataAdapter(COM);
            //DataTable dt1 = new DataTable();
            //da.Fill(dt1);
            //grdVendor.DataSource = dt1;
            //grdVendor.DataBind();
            //dr = COM.ExecuteReader();
            //if (dr.Read())
            //{
            //    lbltotalprice.Text = dr["TOTPRICE"].ToString();
            //    lblgstamt.Text = dr["TOTGSTAMT"].ToString();
            //    lblgrandtotal.Text = dr["GRANDTOT"].ToString();
            //}
            //dr.Close();
            //con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void chkQuot_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grdVendor.Rows)
            {
                // CheckBox chkRow = (row.Cells[1].FindControl("chkQuot") as CheckBox);
                Label hsncodIt = (row.Cells[2].FindControl("lbl_hsncode") as Label);
                Label nameIt = (row.Cells[3].FindControl("lbl_name") as Label);
                Label unitIt = (row.Cells[4].FindControl("lbl_unit") as Label);
                TextBox quantyIt = (row.Cells[5].FindControl("txt_qty") as TextBox);
                TextBox priceIt = (row.Cells[6].FindControl("txt_price") as TextBox);
                Label amtIt = (row.Cells[7].FindControl("lbl_Amt") as Label);
                Label cgstIt = (row.Cells[8].FindControl("lbl_cgst") as Label);
                Label sgstIt = (row.Cells[9].FindControl("lbl_sgst") as Label);
                Label igstIt = (row.Cells[10].FindControl("lbl_igst") as Label);
                Label gstamtIt = (row.Cells[11].FindControl("lbl_gstamt") as Label);
                Label totamtIt = (row.Cells[12].FindControl("lbl_totamt") as Label);

                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[1].FindControl("chkQuot") as CheckBox);

                    if (chkRow.Checked)
                    {
                        var price = row.FindControl("txt_price") as TextBox;
                        var gstamt = row.FindControl("lbl_gstamt") as Label;
                        var totalamt = row.FindControl("lbl_totamt") as Label;
                        amount = amount + Convert.ToDecimal(totalamt.Text);
                        gstamount = gstamount + Convert.ToDecimal(gstamt.Text);

                    }
                }
            }
            lblgrandtotal.Text = amount.ToString();
            lblgstamt.Text = gstamount.ToString();
            lbltotalprice.Text = (Convert.ToDouble(lblgrandtotal.Text) - Convert.ToDouble(lblgstamt.Text)).ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grdVendContrct_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = grdVendContrct.DataKeys[e.NewSelectedIndex].Values["CONTRACTID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SEARCH_ByINDEX");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@CONTRACTID", SqlDbType.VarChar, 500, slno);

            DataSet Ds = OBJ_METHOD.Get_DataSet("SP_VendorContract", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                btncancel.Visible = true;
                btndelete.Visible = true;
                lblEditgrd.Text = slno.ToString();
                txtdate.Text = Ds.Tables[0].Rows[0]["DATE"].ToString();
                txtInvoice.Text = Ds.Tables[0].Rows[0]["CONTRACTID"].ToString();
                ddVendor.Text = Ds.Tables[0].Rows[0]["VENDERNAME"].ToString();
                txtQuotaion.Text = Ds.Tables[0].Rows[0]["QUTIONNO"].ToString();
                lbltotalprice.Text = Ds.Tables[0].Rows[0]["TOTPRICE"].ToString();
                lblgstamt.Text = Ds.Tables[0].Rows[0]["TOTGSTAMT"].ToString();
                lblgrandtotal.Text = Ds.Tables[0].Rows[0]["GRANDTOT"].ToString();
                SQL_PARAMS = new SqlParameter[3];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SEARCH_BYid");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@CONTRACTID", SqlDbType.VarChar, 500, slno);

                DataSet Ds1 = OBJ_METHOD.Get_DataSet("SP_VendorContract", false, true, SQL_PARAMS);
                if (Ds1.Tables[0].Rows.Count > 0)
                {
                    grdVendor.DataSource = Ds1;
                    grdVendor.DataBind();
                    ViewState["ITEM"] = Ds1;
                }
            }
            #region oldcode
            //using (SqlCommand COM = new SqlCommand("SP_VendorContract", con))
            //{
            //    COM.CommandType = CommandType.StoredProcedure;
            //    COM.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH_ByINDEX";
            //    COM.Parameters.Add("@CONTRACTID", SqlDbType.VarChar).Value = slno;
            //    dr = COM.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        btncreate.Visible = false;
            //        btnupdate.Visible = true;
            //        btncancel.Visible = true;
            //        btndelete.Visible = true;
            //        lblEditgrd.Text = slno.ToString();
            //        txtdate.Text = dr["DATE"].ToString();
            //        txtInvoice.Text = dr["CONTRACTID"].ToString();
            //        ddVendor.Text = dr["VENDERNAME"].ToString();
            //        txtQuotaion.Text = dr["QUTIONNO"].ToString();
            //        lbltotalprice.Text = dr["TOTPRICE"].ToString();
            //        lblgstamt.Text = dr["TOTGSTAMT"].ToString();
            //        lblgrandtotal.Text = dr["GRANDTOT"].ToString();
            //        dr.Close();
            //        DataTable dt = new DataTable();
            //        //SqlDataAdapter da = new SqlDataAdapter("select CHKSEL,HSNCODE,ITEM as NAME,UNIT,QTY as QUANTITY,PRICE,AMOUNT,CGST,SGST,IGST,GSTAMT,TOTAMT  from VENDOR_COTRACT_ITEM_TBL where CONTRACTID='" + slno + "'", con);

            //        using (SqlCommand COM1 = new SqlCommand("SP_QUTO_page_display", con))
            //        {
            //            COM1.CommandType = CommandType.StoredProcedure;
            //            COM1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH_BYid";
            //            COM1.Parameters.Add("@CONTRACTID", SqlDbType.VarChar).Value = slno;
            //            SqlDataAdapter da = new SqlDataAdapter(COM1);
            //            da.Fill(dt);
            //            grdVendor.DataSource = dt;
            //            // grdrfq.DataKeyNames = new string[] { "ID" };
            //            grdVendor.DataBind();
            //            // DataTable dt = ds2.Tables["Table"];
            //            ViewState["ITEM"] = dt;
            //        }
            //    }
            //}
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grdVendContrct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("SP_Vendor_contract", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                grdVendContrct.DataSource = Ds;
                grdVendContrct.PageIndex = e.NewPageIndex;
                grdVendContrct.DataKeyNames = new string[] { "CONTRACTID" };
                grdVendContrct.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtdate.Text == "")
            {
                string message = "alert('* Enter the date.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdate.Focus();
                return;
            }
            else if (txtInvoice.Text == "")
            {
                string message = "alert('* Enter the Contract No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtContrtno.Focus();
                return;
            }
            else if (ddVendor.SelectedIndex == 0)
            {
                string message = "alert('* Select Vendor.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                ddVendor.Focus();
                return;
            }
            else if (grdVendor.Rows.Count <= 0)
            {
                string message = "alert('* Enter a valid Quotation No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtQuotaion.Focus();
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[9];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@CONTRACTID", SqlDbType.VarChar, 500, lblEditgrd.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@VENDERNAME", SqlDbType.VarChar, 500, ddVendor.SelectedValue);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);

            SQL_PARAMS[5] = OBJ_METHOD.createParams("@QUTIONNO", SqlDbType.VarChar, 500, txtQuotaion.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@TOTPRICE", SqlDbType.Decimal, 0, lbltotalprice.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@TOTGSTAMT", SqlDbType.Decimal, 0, lblgstamt.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@GRANDTOT", SqlDbType.Decimal, 0, lblgrandtotal.Text);
            OBJ_METHOD.ExecuteProceedure("USP_VENDOR_COTRACT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
            if (OBJ_METHOD._RESULT > 0)
            {
                int chkedcounter = 0;
                int correctinput = 0;
                if (OBJ_METHOD._RESULT > 0)
                {
                    foreach (GridViewRow row in grdVendor.Rows)
                    {
                        CheckBox chkRow = (row.Cells[1].FindControl("chkQuot") as CheckBox);
                        Label hsncodIt = (row.Cells[2].FindControl("lbl_hsncode") as Label);
                        Label nameIt = (row.Cells[3].FindControl("lbl_name") as Label);
                        Label unitIt = (row.Cells[4].FindControl("lbl_unit") as Label);
                        TextBox quantyIt = (row.Cells[5].FindControl("txt_qty") as TextBox);
                        TextBox priceIt = (row.Cells[6].FindControl("txt_price") as TextBox);
                        Label amtIt = (row.Cells[7].FindControl("lbl_Amt") as Label);
                        Label cgstIt = (row.Cells[8].FindControl("lbl_cgst") as Label);
                        Label sgstIt = (row.Cells[9].FindControl("lbl_sgst") as Label);
                        Label igstIt = (row.Cells[10].FindControl("lbl_igst") as Label);
                        Label gstamtIt = (row.Cells[11].FindControl("lbl_gstamt") as Label);
                        Label totamtIt = (row.Cells[12].FindControl("lbl_totamt") as Label);
                        chkedcounter++;
                        SQL_PARAMS = new SqlParameter[15];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDUPDATE");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@CONTRACTID", SqlDbType.VarChar, 500, lblEditgrd.Text);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@ITEM", SqlDbType.VarChar, 500, nameIt.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, unitIt.Text);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@QTY", SqlDbType.Decimal, 0, quantyIt.Text);
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, priceIt.Text);
                        SQL_PARAMS[8] = OBJ_METHOD.createParams("@TOTAMT", SqlDbType.Decimal, 0, totamtIt.Text);
                        SQL_PARAMS[9] = OBJ_METHOD.createParams("@AMOUNT", SqlDbType.Decimal, 0, amtIt.Text);
                        SQL_PARAMS[10] = OBJ_METHOD.createParams("@CGST", SqlDbType.Decimal, 0, cgstIt.Text);
                        SQL_PARAMS[11] = OBJ_METHOD.createParams("@SGST", SqlDbType.Decimal, 0, sgstIt.Text);
                        SQL_PARAMS[12] = OBJ_METHOD.createParams("@GSTAMT", SqlDbType.Decimal, 0, gstamtIt.Text);
                        SQL_PARAMS[13] = OBJ_METHOD.createParams("@CHKSEL", SqlDbType.VarChar, 500, chkRow.Checked);
                        SQL_PARAMS[14] = OBJ_METHOD.createParams("@HSNCODE", SqlDbType.Decimal, 0, hsncodIt.Text);
                        OBJ_METHOD.ExecuteProceedure("USP_VENDOR_COTRACT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
                        if (OBJ_METHOD._RESULT > 0)
                        {
                            correctinput++;

                        }
                    }
                    if (chkedcounter == correctinput && chkedcounter > 0)
                    {

                        OBJ_METHOD.commitOrRollbackTran("commit");
                        message1 = "alert('" + OBJ_METHOD._objOut + "')";
                        binddata();
                        clearfield();
                    }
                    else
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        message1 = "alert('Error occurred while processing data... Rolling back...')";
                    }
                }
            }
            #region updatecode
            //using (SqlCommand VN_cmd = new SqlCommand("USP_VENDOR_COTRACT", con))
            //{
            //    VN_cmd.CommandType = CommandType.StoredProcedure;
            //    VN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    VN_cmd.Parameters.Add("@CONTRACTID", SqlDbType.VarChar).Value = lblEditgrd.Text;
            //    VN_cmd.Parameters.Add("@VENDERNAME", SqlDbType.VarChar).Value = ddVendor.SelectedValue;
            //    VN_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //    VN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

            //    VN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = txtQuotaion.Text;
            //    VN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
            //    VN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = lblgstamt.Text;
            //    VN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = lblgrandtotal.Text;

            //    VN_cmd.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = "";
            //    VN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
            //    VN_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = 0.00;

            //    VN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "";
            //    VN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "";

            //    VN_cmd.ExecuteNonQuery();

            //}
            //foreach (GridViewRow row in grdVendor.Rows)
            //{
            //    CheckBox chkRow = (row.Cells[1].FindControl("chkQuot") as CheckBox);
            //    Label hsncodIt = (row.Cells[2].FindControl("lbl_hsncode") as Label);
            //    Label nameIt = (row.Cells[3].FindControl("lbl_name") as Label);
            //    Label unitIt = (row.Cells[4].FindControl("lbl_unit") as Label);
            //    TextBox quantyIt = (row.Cells[5].FindControl("txt_qty") as TextBox);
            //    TextBox priceIt = (row.Cells[6].FindControl("txt_price") as TextBox);
            //    Label amtIt = (row.Cells[7].FindControl("lbl_Amt") as Label);
            //    Label cgstIt = (row.Cells[8].FindControl("lbl_cgst") as Label);
            //    Label sgstIt = (row.Cells[9].FindControl("lbl_sgst") as Label);
            //    Label igstIt = (row.Cells[10].FindControl("lbl_igst") as Label);
            //    Label gstamtIt = (row.Cells[11].FindControl("lbl_gstamt") as Label);
            //    Label totamtIt = (row.Cells[12].FindControl("lbl_totamt") as Label);


            //    using (SqlCommand MRitem_cmd = new SqlCommand("USP_VENDOR_COTRACT", con))
            //    {
            //        MRitem_cmd.CommandType = CommandType.StoredProcedure;
            //        MRitem_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDUPDATE";
            //        MRitem_cmd.Parameters.Add("@CONTRACTID", SqlDbType.VarChar).Value = lblEditgrd.Text;
            //        MRitem_cmd.Parameters.Add("@VENDERNAME", SqlDbType.VarChar).Value = ddVendor.SelectedValue;
            //        MRitem_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //        MRitem_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

            //        MRitem_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = "";
            //        MRitem_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = 0.00;
            //        MRitem_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = 0.00;
            //        MRitem_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = 0.00;


            //        MRitem_cmd.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = nameIt.Text;
            //        MRitem_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitIt.Text;
            //        MRitem_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = quantyIt.Text;
            //        MRitem_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = priceIt.Text;
            //        MRitem_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = totamtIt.Text;

            //        MRitem_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = amtIt.Text;
            //        MRitem_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = cgstIt.Text;
            //        MRitem_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = sgstIt.Text;
            //        MRitem_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = igstIt.Text;
            //        MRitem_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = gstamtIt.Text;
            //        MRitem_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = chkRow.Checked;
            //        MRitem_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = hsncodIt.Text;

            //        MRitem_cmd.ExecuteNonQuery();
            //    }
            //}
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

    protected void btndelete_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@CONTRACTID", SqlDbType.VarChar, 500, lblEditgrd.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            OBJ_METHOD.ExecuteProceedure("USP_VENDOR_COTRACT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clearfield();

            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            #region deletecode
            //using (SqlCommand VN_cmd = new SqlCommand("USP_VENDOR_COTRACT", con))
            //{
            //    VN_cmd.CommandType = CommandType.StoredProcedure;
            //    VN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //    VN_cmd.Parameters.Add("@CONTRACTID", SqlDbType.VarChar).Value = lblEditgrd.Text;
            //    VN_cmd.Parameters.Add("@VENDERNAME", SqlDbType.VarChar).Value = ddVendor.SelectedValue;
            //    VN_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //    VN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

            //    VN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = txtQuotaion.Text;
            //    VN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
            //    VN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = lblgstamt.Text;
            //    VN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = lblgrandtotal.Text;

            //    VN_cmd.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = "";
            //    VN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
            //    VN_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = 0.00;

            //    VN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = 0.00;
            //    VN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "";
            //    VN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "";

            //    VN_cmd.ExecuteNonQuery();

            //}
            //con.Close();
            #endregion
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
}