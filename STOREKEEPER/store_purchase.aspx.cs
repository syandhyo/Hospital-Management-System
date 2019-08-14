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

public partial class STOREKEEPER_store_purchase : System.Web.UI.Page
{
    string num1 = "0";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3, cmd4;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    int i, no, no1, sl, F;
    int flag = 0, stock = 0, stock_tran = 0, trans = 0, stock1 = 0, S, CLOSEING, slno, CLOSE;
    string id, id1, ph, val1, val2, p, q, des1, name, k, PIN;
    public string id_hist;
    public double amt = 0, qty = 0, amt1 = 0, t_amt = 0, qt = 0, c_rs = 0, avg = 0;
    decimal a, b, c, d, e, f, g, h, j;
    decimal amount = 0;
    decimal amount1 = 0;
    decimal gstamount = 0,totalamount=0;
    DateTime DT;
    string rec;
    double totalamt1, totamt, totalgstamt, dis, dism;
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
            using (SqlCommand cmd = new SqlCommand("STORE_PURACHASE_SELECT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PO_TABLE";
                cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = prefix;
                
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["PONO"]));
                    }
                }
                con.Close();
            }
        }
        return customers.ToArray();
    }
    [WebMethod]
    public static string[] GetCustomers1(string prefix1)
    {
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select DISTINCT NAME from MATERIAL_MASTER_TABLE where NAME like @SearchText1+'%'";
                cmd.Parameters.AddWithValue("@SearchText1", prefix1);
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
        string qry1 = "select max(GRNNO) AS ID from GRN_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;
        dr = com.ExecuteReader();
        string str1 = "1";
        if (dr.Read() && dr["ID"].ToString() != "")
        {
            num1 = dr["ID"].ToString();
            string str = num1.Substring(0, num1.Length - 10);//delete last 10 record
            string d = str.Substring(4);//delete first 3 record
            str1 = (Convert.ToInt64(d) + 1).ToString();//add in numbers
        }
        //num1 = string.Format("INV{0}", (Convert.ToUInt64(num1.Substring(3)) + 1).ToString("D4"));
        txtgrnno.Text = "GRN-" + str1 + "-" + lblfyear.Text;

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

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BIND1");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("STORE_PURACHASE_SELECT", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropVendor.DataSource = Ds;
                dropVendor.DataTextField = "NAME1";
                dropVendor.DataValueField = "ID";
                dropVendor.DataBind();
                dropVendor.Items.Insert(0,new ListItem("Please Select"));
            }
            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BIND2_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("STORE_PURACHASE_SELECT", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                grvPurchase.DataSource = Ds1;
                grvPurchase.DataKeyNames = new string[] { "GRNNO" };
                grvPurchase.DataBind();
            }
            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select max(GRNNO) as grnid from GRN_TABLE", false, false);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                txtgrnno.Text = Ds2.Tables[0].Rows[0]["grnid"].ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        #region oldcode
        //using (SqlCommand cmd = new SqlCommand("STORE_PURACHASE_SELECT", con))
        //{
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BIND1";
        //    cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
        //    da1 = new SqlDataAdapter(cmd);
        //    //da1 = new SqlDataAdapter("Select NAME1,ID FROM VENDER_MASTER_TABLE", con);
        //    DataTable ds1 = new DataTable();
        //    da1.Fill(ds1);
        //    dropVendor.DataSource = ds1;
        //    dropVendor.DataTextField = "NAME1";
        //    dropVendor.DataValueField = "ID";
        //    dropVendor.DataBind();
        //    dropVendor.Items.Insert(0, "Please Select");
        //}
        //------------------------------------
        //using (SqlCommand cmd1 = new SqlCommand("STORE_PURACHASE_SELECT", con))
        //{
        //    cmd1.CommandType = CommandType.StoredProcedure;
        //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BIND2_PAGE";
        //    cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
        //    SqlDataAdapter da = new SqlDataAdapter(cmd1);
        //    //SqlDataAdapter da = new SqlDataAdapter("select b.NAME1,a.GRNNO,a.GRNDATE,a.INVOICENO,a.INVOICEDATE from GRN_TABLE a,VENDER_MASTER_TABLE b where a.VENDOR=b.ID order by a.GRNNO desc", con);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);

        //    grvPurchase.DataSource = dt;
        //    grvPurchase.DataKeyNames = new string[] { "GRNNO" };
        //    grvPurchase.DataBind();
        //}
        //SqlCommand comid = new SqlCommand("select max(GRNNO) as grnid from GRN_TABLE ", con);
        //dr1 = comid.ExecuteReader();
        //if (dr1.Read())
        //{
        //    txtgrnno.Text = dr1["grnid"].ToString();
        //}
        //dr1.Close();

        //con.Close();
        #endregion
    }
    public void clearfield()
    {
        txtContrtno.Text = "";
        auto();
        DropDownList1.SelectedIndex = 0;
        txtstatecode.Text = "";
        txtinvoiceno.Text = "";
        dropVendor.SelectedIndex = 0;
        txtinvoicedate.Text = "";
        txtpono.Text = "";
        txtpodate.Text = "";
        lbltotaldisc.Text = "0.00";
        lblgrandtotal.Text = "0.00";
        lblgstamt.Text = "0.00";
        lbltotalprice.Text = "0.00";
        lbltotaladd.Text = "0.00";
        lbltotalamt.Text = "0.00";
        lbltotaldicadd.Text = "0.00";
        lbltotaldisc.Text = "0.00";
        lbltotalgstadd.Text = "0.00";
        lbltotalprice.Text = "0.00";
        lblpriceadd.Text = "0.00";
        lblsgst.Text = "0.00";
        lblcgst.Text = "0.00";
        normaldiv.Visible = false;
        directdiv.Visible = false;
        btncreate.Visible = true;
        btndelete.Visible = false;
        btnupdate.Visible = false;
        
        grvpurchsItem.DataSource = null;
        grvpurchsItem.DataBind();
        grvpuritemTemp.DataSource = null;
        grvpuritemTemp.DataBind();
    }
    protected void GVOnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("lbl_slno") as Label).Text = (e.Row.RowIndex + 1).ToString();
            }
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
            grvpurchsItem.DataSource = (DataTable)ViewState["ITEM"];
            grvpurchsItem.DataBind();
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
            txtgrndate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[11] { new DataColumn("NAME"), new DataColumn("HSNCODE"), new DataColumn("UNIT"), new DataColumn("PRICE"), new DataColumn("QTY"), new DataColumn("AMOUNT"), new DataColumn("CGST"), new DataColumn("SGST"), new DataColumn("IGST"), new DataColumn("GSTAMT"), new DataColumn("TOTALAMOUNT") });
                ViewState["ITEM"] = dt;
                this.BindGrid();

                binddata();
                bindTGrid();
                
                auto();
                normaldiv.Visible = false;
                txtpono.Enabled = false;
                txtpono.Text = "";
                directdiv.Visible = false;
                
                //DropDownList1.TabIndex = 0;
                //dropVendor.TabIndex = 1;
                //txtinvoiceno.TabIndex = 2;
                //txtinvoicedate.TabIndex = 3;
                DropDownList1.Focus();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void VENDOR_TRAN()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand stock_cmd = new SqlCommand("VENDOR_CREDIT", con))
        {
            stock_cmd.CommandType = CommandType.StoredProcedure;
            stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            stock_cmd.Parameters.Add("@VENDOR_ID", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
            stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0";
            stock_cmd.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = lblgrandtotal.Text;
            stock_cmd.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@CLOSING", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtgrndate.Text).ToString("yyyy-MM-dd");
            stock_cmd.ExecuteNonQuery();
        }
        con.Close();
    }
    protected void Btncreate_Click(object sender, EventArgs e)
    
    {
        string message1 = string.Empty;
        try
        {
            if (DropDownList1.SelectedIndex == 0)
            {
                string message = "alert('* Select Type.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                DropDownList1.Focus();
                return;
            }
            else if (txtgrndate.Text == "")
            {
                string message = "alert('* Select GRN Date.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtgrndate.Focus();
                return;
            }
            else if (dropVendor.SelectedIndex == 0)
            {
                string message = "alert('* Select Vendor.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropVendor.Focus();
                return;
            }
            else if (txtinvoiceno.Text == "")
            {
                string message = "alert('* Select Invoice No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtinvoiceno.Focus();
                return;
            }
            else if (txtinvoicedate.Text == "")
            {
                string message = "alert('* Select Invoice Date.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtinvoicedate.Focus();
                return;
            }
            auto();
            if (DropDownList1.SelectedIndex == 2)
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[17];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@VENDOR", SqlDbType.VarChar, 500, dropVendor.SelectedValue);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                SQL_PARAMS[5] = OBJ_METHOD.createParams("@GRNDATE", SqlDbType.Date, 0, Convert.ToDateTime(txtgrndate.Text).ToString("yyyy-MM-dd"));
                SQL_PARAMS[6] = OBJ_METHOD.createParams("@VSCODE", SqlDbType.VarChar, 500, txtstatecode.Text);

                SQL_PARAMS[7] = OBJ_METHOD.createParams("@GRNNO", SqlDbType.VarChar, 500, txtgrnno.Text);
                SQL_PARAMS[8] = OBJ_METHOD.createParams("@INVOICENO", SqlDbType.VarChar, 500, txtinvoiceno.Text);
                SQL_PARAMS[9] = OBJ_METHOD.createParams("@INVOICEDATE", SqlDbType.Date, 0, Convert.ToDateTime(txtinvoicedate.Text).ToString("yyyy-MM-dd"));
                SQL_PARAMS[10] = OBJ_METHOD.createParams("@TOTALPRICE", SqlDbType.VarChar, 500, lbltotalprice.Text);

                SQL_PARAMS[11] = OBJ_METHOD.createParams("@DISCOUNTAMOUNT", SqlDbType.VarChar, 500, lbltotaldisc.Text);
                SQL_PARAMS[12] = OBJ_METHOD.createParams("@GSTAMOUNT", SqlDbType.VarChar, 500, lblgstamt.Text);
                SQL_PARAMS[13] = OBJ_METHOD.createParams("@GRANDTOTAL", SqlDbType.VarChar, 500, lblgrandtotal.Text);
                SQL_PARAMS[14] = OBJ_METHOD.createParams("@PODATE", SqlDbType.Date, 0, txtpodate.Text);
                SQL_PARAMS[15] = OBJ_METHOD.createParams("@PONO", SqlDbType.VarChar, 500, txtpono.Text);
                SQL_PARAMS[16] = OBJ_METHOD.createParams("@SearchText", SqlDbType.VarChar, 500, txtinvoiceno.Text);
                OBJ_METHOD.ExecuteProceedure("GRN_OPERATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
                if (OBJ_METHOD._RESULT > 0)
                {
                    int chkedcounter = 0;
                    int correctinput = 0;
                    if (OBJ_METHOD._RESULT > 0)
                    {
                        foreach (GridViewRow row in grvpurchsItem.Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);

                                if (chkRow.Checked)
                                {
                                    var name = row.FindControl("lbl_name") as Label;
                                    var hsncode = row.FindControl("lbl_hsn") as Label;
                                    var unit = row.FindControl("lbl_unit") as Label;
                                    var price = row.FindControl("lbl_price") as Label;
                                    var qty = row.FindControl("lbl_qty") as Label;
                                    //var remqty = row.FindControl("lbl_remqty") as Label;
                                    var rqty = row.FindControl("txt_Qty") as TextBox;
                                    var amount = row.FindControl("lbl_amount") as Label;
                                    var cgst = row.FindControl("lbl_cgst") as Label;
                                    var sgst = row.FindControl("lbl_sgst") as Label;
                                    var igst = row.FindControl("lbl_igst") as Label;
                                    var gstamt = row.FindControl("lbl_gstamt") as Label;
                                    var totalamt = row.FindControl("lbl_totalamount") as Label;

                                    chkedcounter++;
                                    SQL_PARAMS = new SqlParameter[10];

                                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Convert.ToDateTime(txtinvoicedate.Text).ToString("yyyy-MM-dd"));
                                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, name.Text);
                                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@REF_ID", SqlDbType.VarChar, 500, txtgrnno.Text);
                                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@REC", SqlDbType.Decimal, 0, qty.Text);
                                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@ISSUE", SqlDbType.Decimal, 0, "0.00");
                                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@ISS_RTN", SqlDbType.Decimal, 0, "0.00");
                                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@PUR_RTN", SqlDbType.Decimal, 0, "0.00");

                                    OBJ_METHOD.ExecuteProceedure("MAT_STORE_INSERT", "", "", SqlDbType.VarChar, SQL_PARAMS, true);
                                    if (OBJ_METHOD._RESULT > 0)
                                    {
                                        SQL_PARAMS = new SqlParameter[18];

                                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDINSERT");
                                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@GRNNO", SqlDbType.VarChar, 500, txtgrnno.Text);
                                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@ITEMNAME", SqlDbType.VarChar, 500, name.Text);
                                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@HSNCODE", SqlDbType.VarChar, 500, hsncode.Text);
                                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, unit.Text);
                                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@PRICE", SqlDbType.VarChar, 500, price.Text);
                                        SQL_PARAMS[8] = OBJ_METHOD.createParams("@QTY", SqlDbType.VarChar, 500, qty.Text);
                                        SQL_PARAMS[9] = OBJ_METHOD.createParams("@RECQTY", SqlDbType.VarChar, 500, rqty.Text);
                                        SQL_PARAMS[10] = OBJ_METHOD.createParams("@REMQTY", SqlDbType.VarChar, 500, null);
                                        SQL_PARAMS[11] = OBJ_METHOD.createParams("@AMOUNT", SqlDbType.VarChar, 500, amount.Text);
                                        SQL_PARAMS[12] = OBJ_METHOD.createParams("@CGST", SqlDbType.VarChar, 500, cgst.Text);
                                        SQL_PARAMS[13] = OBJ_METHOD.createParams("@SGST", SqlDbType.VarChar, 500, sgst.Text);
                                        SQL_PARAMS[14] = OBJ_METHOD.createParams("@IGST", SqlDbType.VarChar, 500, igst.Text);
                                        SQL_PARAMS[15] = OBJ_METHOD.createParams("@GSTAMT", SqlDbType.VarChar, 500, gstamt.Text);
                                        SQL_PARAMS[16] = OBJ_METHOD.createParams("@TOTALAMT", SqlDbType.VarChar, 500, totalamt.Text);
                                        SQL_PARAMS[17] = OBJ_METHOD.createParams("@SearchText", SqlDbType.VarChar, 500, txtinvoiceno.Text);

                                        OBJ_METHOD.ExecuteProceedure("GRN_OPERATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
                                        if (OBJ_METHOD._RESULT > 0)
                                        {
                                            correctinput++;
                                        }
                                    }
                                    else
                                    {
                                        OBJ_METHOD.commitOrRollbackTran("rollback");
                                        message1 = "alert('Error occurred while processing data... Rolling back...')";
                                    }
                                }
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
            }
            else
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[17];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@VENDOR", SqlDbType.VarChar, 500, dropVendor.SelectedValue);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                SQL_PARAMS[5] = OBJ_METHOD.createParams("@GRNDATE", SqlDbType.Date, 0, Convert.ToDateTime(txtgrndate.Text).ToString("yyyy-MM-dd"));
                SQL_PARAMS[6] = OBJ_METHOD.createParams("@VSCODE", SqlDbType.VarChar, 500, txtstatecode.Text);

                SQL_PARAMS[7] = OBJ_METHOD.createParams("@GRNNO", SqlDbType.VarChar, 500, txtgrnno.Text);
                SQL_PARAMS[8] = OBJ_METHOD.createParams("@INVOICENO", SqlDbType.VarChar, 500, txtinvoiceno.Text);
                SQL_PARAMS[9] = OBJ_METHOD.createParams("@INVOICEDATE", SqlDbType.Date, 0, Convert.ToDateTime(txtinvoicedate.Text).ToString("yyyy-MM-dd"));
                SQL_PARAMS[10] = OBJ_METHOD.createParams("@TOTALPRICE", SqlDbType.VarChar, 500, lbltotalprice.Text);

                SQL_PARAMS[11] = OBJ_METHOD.createParams("@DISCOUNTAMOUNT", SqlDbType.VarChar, 500, lbltotaldisc.Text);
                SQL_PARAMS[12] = OBJ_METHOD.createParams("@GSTAMOUNT", SqlDbType.VarChar, 500, lblgstamt.Text);
                SQL_PARAMS[13] = OBJ_METHOD.createParams("@GRANDTOTAL", SqlDbType.VarChar, 500, lblgrandtotal.Text);
                SQL_PARAMS[14] = OBJ_METHOD.createParams("@PODATE", SqlDbType.Date, 0, DateTime.Now.ToString("yyyy-MM-dd"));
                SQL_PARAMS[15] = OBJ_METHOD.createParams("@PONO", SqlDbType.VarChar, 500, txtpono.Text);
                SQL_PARAMS[16] = OBJ_METHOD.createParams("@SearchText", SqlDbType.VarChar, 500, txtinvoiceno.Text);
                OBJ_METHOD.ExecuteProceedure("GRN_OPERATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
                if (OBJ_METHOD._RESULT > 0)
                {
                    int chkedcounter = 0;
                    int correctinput = 0;
                    if (OBJ_METHOD._RESULT > 0)
                    {
                        foreach (GridViewRow row in grdMaterial.Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);

                                
                                Label nameit = (Label)row.FindControl("lbl_name");
                                Label unitit = (Label)row.FindControl("lbl_unit");
                                //Label quantityit = (Label)row.FindControl("lbl_qty");
                                Label hsnno = (Label)row.FindControl("lbl_hsn");
                                Label prce = (Label)row.FindControl("lbl_price");
                                //TextBox recqunt = (TextBox)row.FindControl("txt_qty");
                                Label amnt = (Label)row.FindControl("lbl_amount");
                                Label cgstit = (Label)row.FindControl("lbl_cgst");
                                Label sgstit = (Label)row.FindControl("lbl_sgst");
                                Label igstit = (Label)row.FindControl("lbl_igst");
                                Label gstamtit = (Label)row.FindControl("lbl_gstamt");
                                Label totalit = (Label)row.FindControl("lbl_totalamount");

                                var rec = row.FindControl("txt_qty") as TextBox;
                                chkedcounter++;
                                SQL_PARAMS = new SqlParameter[10];

                                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                                SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Convert.ToDateTime(txtinvoicedate.Text).ToString("yyyy-MM-dd"));
                                SQL_PARAMS[2] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, nameit.Text);
                                SQL_PARAMS[3] = OBJ_METHOD.createParams("@REF_ID", SqlDbType.VarChar, 500, txtgrnno.Text);
                                SQL_PARAMS[4] = OBJ_METHOD.createParams("@REC", SqlDbType.Decimal, 0, rec.Text);
                                SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                                SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                                SQL_PARAMS[7] = OBJ_METHOD.createParams("@ISSUE", SqlDbType.Decimal, 0, "0.00");
                                SQL_PARAMS[8] = OBJ_METHOD.createParams("@ISS_RTN", SqlDbType.Decimal, 0, "0.00");
                                SQL_PARAMS[9] = OBJ_METHOD.createParams("@PUR_RTN", SqlDbType.Decimal, 0, "0.00");

                                OBJ_METHOD.ExecuteProceedure("MAT_STORE_INSERT", "", "", SqlDbType.VarChar, SQL_PARAMS, true);
                                if (OBJ_METHOD._RESULT > 0)
                                {
                                    SQL_PARAMS = new SqlParameter[18];

                                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDINSERT");
                                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@GRNNO", SqlDbType.VarChar, 500, txtgrnno.Text);
                                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@ITEMNAME", SqlDbType.VarChar, 500, nameit.Text);
                                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@HSNCODE", SqlDbType.VarChar, 500, hsnno.Text);
                                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, unitit.Text);
                                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@PRICE", SqlDbType.VarChar, 500, prce.Text);
                                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@QTY", SqlDbType.VarChar, 500, null);
                                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@RECQTY", SqlDbType.VarChar, 500, rec.Text);
                                    SQL_PARAMS[10] = OBJ_METHOD.createParams("@REMQTY", SqlDbType.VarChar, 500, null);
                                    SQL_PARAMS[11] = OBJ_METHOD.createParams("@AMOUNT", SqlDbType.VarChar, 500, amnt.Text);
                                    SQL_PARAMS[12] = OBJ_METHOD.createParams("@CGST", SqlDbType.VarChar, 500, cgstit.Text);
                                    SQL_PARAMS[13] = OBJ_METHOD.createParams("@SGST", SqlDbType.VarChar, 500, sgstit.Text);
                                    SQL_PARAMS[14] = OBJ_METHOD.createParams("@IGST", SqlDbType.VarChar, 500, igstit.Text);
                                    SQL_PARAMS[15] = OBJ_METHOD.createParams("@GSTAMT", SqlDbType.VarChar, 500, gstamtit.Text);
                                    SQL_PARAMS[16] = OBJ_METHOD.createParams("@TOTALAMT", SqlDbType.VarChar, 500, totalit.Text);
                                    SQL_PARAMS[17] = OBJ_METHOD.createParams("@SearchText", SqlDbType.VarChar, 500, txtinvoiceno.Text);

                                    OBJ_METHOD.ExecuteProceedure("GRN_OPERATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
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
                            message1 = "alert('" + OBJ_METHOD._objOut + "')";
                            binddata();
                            clearfield();
                        }
                        else if (chkedcounter<=0)
                        {
                            OBJ_METHOD.commitOrRollbackTran("rollback");
                            message1 = "alert('No Row Is Selected.')";
                        }
                        else
                        {
                            OBJ_METHOD.commitOrRollbackTran("rollback");
                            message1 = "alert('Error occurred while processing data... Rolling back...')";
                        }
                    }
                }
            }
            #region Insert code
            //using (SqlCommand stock_cmd = new SqlCommand("GRN_OPERATION", con))
            //{
            //    stock_cmd.CommandType = CommandType.StoredProcedure;
            //    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //    stock_cmd.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
            //    stock_cmd.Parameters.Add("@VSCODE", SqlDbType.VarChar).Value = txtstatecode.Text;
            //    stock_cmd.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = txtgrnno.Text;
            //    stock_cmd.Parameters.Add("@GRNDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtgrndate.Text).ToString("yyyy-MM-dd");
            //    stock_cmd.Parameters.Add("@INVOICENO", SqlDbType.VarChar).Value = txtinvoiceno.Text;
            //    stock_cmd.Parameters.Add("@INVOICEDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvoicedate.Text).ToString("yyyy-MM-dd");
            //    stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
            //    stock_cmd.Parameters.Add("@DISCOUNTAMOUNT", SqlDbType.VarChar).Value = lbltotaldisc.Text;
            //    stock_cmd.Parameters.Add("@GSTAMOUNT", SqlDbType.VarChar).Value = lblgstamt.Text;
            //    stock_cmd.Parameters.Add("@GRANDTOTAL", SqlDbType.VarChar).Value = lblgrandtotal.Text;
            //    stock_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@RECQTY", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@REMQTY", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
            //    //  stock_cmd.Parameters.Add("@PODATE", SqlDbType.Date).Value = Convert.ToDateTime(txtpodate.Text).ToString("yyyy-MM-dd");

            //    stock_cmd.Parameters.Add("@PODATE", SqlDbType.Date).Value = Convert.ToDateTime(txtpodate.Text).ToString("yyyy-MM-dd");

            //    stock_cmd.Parameters.Add("@IsSelected", SqlDbType.Bit).Value = "True";
            //    stock_cmd.ExecuteNonQuery();
            //}
            //foreach (GridViewRow row in grvpurchsItem.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);

            //        //if (chkRow.Checked)
            //        //{
            //        var name = row.FindControl("lbl_name") as Label;
            //        var hsncode = row.FindControl("lbl_hsn") as Label;
            //        var unit = row.FindControl("lbl_unit") as Label;
            //        var price = row.FindControl("lbl_price") as Label;
            //        var qty = row.FindControl("lbl_qty") as Label;
            //        var remqty = row.FindControl("lbl_remqty") as Label;
            //        var rqty = row.FindControl("txt_qty") as TextBox;
            //        var amount = row.FindControl("lbl_amount") as Label;
            //        var cgst = row.FindControl("lbl_cgst") as Label;
            //        var sgst = row.FindControl("lbl_sgst") as Label;
            //        var igst = row.FindControl("lbl_igst") as Label;
            //        var gstamt = row.FindControl("lbl_gstamt") as Label;
            //        var totalamt = row.FindControl("lbl_totalamount") as Label;
            //        using (SqlCommand stock_cmd = new SqlCommand("GRN_OPERATION", con))
            //        {
            //            stock_cmd.CommandType = CommandType.StoredProcedure;
            //            stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
            //            stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //            stock_cmd.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
            //            stock_cmd.Parameters.Add("@VSCODE", SqlDbType.VarChar).Value = txtstatecode.Text;
            //            stock_cmd.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = txtgrnno.Text;
            //            stock_cmd.Parameters.Add("@GRNDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtgrndate.Text).ToString("yyyy-MM-dd");
            //            stock_cmd.Parameters.Add("@INVOICENO", SqlDbType.VarChar).Value = txtinvoiceno.Text;
            //            stock_cmd.Parameters.Add("@INVOICEDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvoicedate.Text).ToString("yyyy-MM-dd");
            //            stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
            //            stock_cmd.Parameters.Add("@DISCOUNTAMOUNT", SqlDbType.VarChar).Value = lbltotaldisc.Text;
            //            stock_cmd.Parameters.Add("@GSTAMOUNT", SqlDbType.VarChar).Value = lblgstamt.Text;
            //            stock_cmd.Parameters.Add("@GRANDTOTAL", SqlDbType.VarChar).Value = lblgrandtotal.Text;
            //            stock_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
            //            stock_cmd.Parameters.Add("@PODATE", SqlDbType.Date).Value = txtpodate.Text;
            //            stock_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = name.Text;
            //            stock_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = hsncode.Text;
            //            stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unit.Text;
            //            stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = price.Text;
            //            stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = qty.Text;
            //            stock_cmd.Parameters.Add("@RECQTY", SqlDbType.VarChar).Value = rqty.Text;
            //            stock_cmd.Parameters.Add("@REMQTY", SqlDbType.VarChar).Value = remqty.Text;
            //            stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = amount.Text;
            //            stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = cgst.Text;
            //            stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = sgst.Text;
            //            stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = igst.Text;
            //            stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = gstamt.Text;
            //            stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = totalamt.Text;
            //            stock_cmd.Parameters.Add("@IsSelected", SqlDbType.Bit).Value = chkRow.Checked;
            //            stock_cmd.ExecuteNonQuery();
            //        }

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
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grvpurchsItem.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);
                    var ORQTY = row.FindControl("lbl_qty") as Label;
                    var REMQTY = row.FindControl("lbl_remqty") as Label;
                    var RECQTY = row.FindControl("txt_Qty") as TextBox;
                    var NAME = row.FindControl("lbl_name") as Label;

                    if (chkRow.Checked)
                    {
                        SqlParameter[] SQL_PARAMS = new SqlParameter[3];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_CHECK_TEXT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@SearchText", SqlDbType.VarChar, 500, NAME.Text);

                        DataSet Ds1 = OBJ_METHOD.Get_DataSet("STORE_PURACHASE_SELECT", false, true, SQL_PARAMS);
                        if (Ds1.Tables[0].Rows.Count > 0)
                        {
                            rec = "0";
                            rec = Ds1.Tables[0].Rows[0]["RQTY"].ToString();
                        }
                        #region oldcode
                        //using (SqlCommand com = new SqlCommand("STORE_PURACHASE_SELECT", con))
                        //{
                        //    com.CommandType = CommandType.StoredProcedure;
                        //    com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CHECK_TEXT";
                        //    com.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = NAME.Text;
                        //    //SqlCommand com = new SqlCommand("select ISNULL(SUM(RQTY),0) AS RQTY from PO_TRAN WHERE ITEMNAME='" + NAME.Text + "'", con);
                        //    dr = com.ExecuteReader();
                        //    if (dr.Read())
                        //    {
                        //        rec = "0";
                        //        rec = dr["RQTY"].ToString();
                        //    }
                        //    dr.Close();
                        //}
                        //con.Close();
                        #endregion
                        var price = row.FindControl("lbl_amount") as Label;
                        var gstamt = row.FindControl("lbl_gstamt") as Label;
                        var totalamt = row.FindControl("lbl_totalamount") as Label;
                        amount = amount + Convert.ToDecimal(totalamt.Text);
                        gstamount = gstamount + Convert.ToDecimal(gstamt.Text);
                        REMQTY.Text = (Convert.ToDouble(ORQTY.Text) - (Convert.ToDouble(RECQTY.Text) + Convert.ToDouble(rec))).ToString();

                    }
                    else
                    {
                        RECQTY.Text = "0.00";
                        REMQTY.Text = (Convert.ToDouble(ORQTY.Text) - (Convert.ToDouble(RECQTY.Text) + Convert.ToDouble(rec))).ToString();
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
    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList1.SelectedIndex == 0)
            {
                string message = "alert('* Select Type.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                DropDownList1.Focus();
                return;
            }
            else if (DropDownList1.SelectedIndex == 1)
            {
                string message = "alert('* You are not able to do this operation.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                DropDownList1.Focus();
                return;
            }
            else if (txtpono.Text == "")
            {
                string message = "alert('* Enter the PONO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpono.Focus();
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@PONO", SqlDbType.VarChar, 500, txtpono.Text);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("SP_Purchase_View", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                dropVendor.SelectedValue = Ds1.Tables[0].Rows[0]["VENDOR"].ToString();
                txtstatecode.Text = Ds1.Tables[0].Rows[0]["VSCODE"].ToString();
                txtpono.Text = Ds1.Tables[0].Rows[0]["PONO"].ToString();
                txtpodate.Text = Ds1.Tables[0].Rows[0]["DATEOFISSUE"].ToString();
                //lbltotalprice.Text = Ds1.Tables[0].Rows[0]["TOTALPRICE"].ToString();
                //lblgstamt.Text = Ds1.Tables[0].Rows[0]["GSTAMOUNT"].ToString();
                //lblgrandtotal.Text = Ds1.Tables[0].Rows[0]["GRANDTOTAL"].ToString();

                SQL_PARAMS = new SqlParameter[3];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SELECT_1");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@PONO", SqlDbType.VarChar, 500, txtpono.Text);

                DataSet Ds = OBJ_METHOD.Get_DataSet("SP_Purchase_View", false, true, SQL_PARAMS);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    grvpurchsItem.DataSource = Ds.Tables[0];
                    grvpurchsItem.DataBind();
                    ViewState["ITEM"] = Ds.Tables[0];
                    this.BindGrid();
                    normaldiv.Visible = true;
                }
                
            }
            else
            {
                string message = "alert('* Incorrect PONO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            #region oldcode
            //using (SqlCommand VN_cmd = new SqlCommand("SP_Purchase_View", con))
            //{
            //    VN_cmd.CommandType = CommandType.StoredProcedure;
            //    VN_cmd.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT";
            //    VN_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
            //    dr = VN_cmd.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        //Label1.Text = dr["FYEAR"].ToString();
            //        dropVendor.SelectedValue = dr["VENDOR"].ToString();
            //        txtstatecode.Text = dr["VSCODE"].ToString();
            //        txtpono.Text = dr["PONO"].ToString();
            //        txtpodate.Text = dr["DATEOFISSUE"].ToString();
            //        dr.Close();
            //        using (SqlCommand com = new SqlCommand("SP_Purchase_View", con))
            //        {
            //            com.CommandType = CommandType.StoredProcedure;
            //            com.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT_1";
            //            com.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
            //            SqlDataAdapter da = new SqlDataAdapter(com);
            //            DataTable dt = (DataTable)ViewState["ITEM"];
            //            dt.Clear();
            //            da.Fill(dt);
            //            grvpurchsItem.DataSource = dt;
            //            //grvquotion.DataKeyNames = new string[] { "ID" };
            //            grvpurchsItem.DataBind();
            //            ViewState["ITEM"] = dt;
            //            this.BindGrid();
            //        }
            //        normaldiv.Visible = true;
            //        // }
            //    }
            //    else
            //    {
            //        string message = "alert('* Incorrect PONO.')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //        return;
            //    }
            //    dr.Close();
            //}
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void txt_price_TextChanged(object sender, EventArgs e)
    {
        try
        {
            #region oldcode
            //foreach (GridViewRow row in grvpurchsItem.Rows)
            //{
            //    var NAME = row.FindControl("lbl_name") as Label;
            //    var REMQTY = row.FindControl("lbl_remqty") as Label;
            //    var oqty = row.FindControl("lbl_qty") as Label;
            //    var qty = row.FindControl("txt_Qty") as TextBox;
            //    var price = row.FindControl("lbl_price") as Label;
            //    var amount = row.FindControl("lbl_amount") as Label;
            //    var cgst = row.FindControl("lbl_cgst") as Label;
            //    var sgst = row.FindControl("lbl_sgst") as Label;
            //    var igst = row.FindControl("lbl_igst") as Label;
            //    var gstamt = row.FindControl("lbl_gstamt") as Label;
            //    var totalamt = row.FindControl("lbl_totalamount") as Label;
            //    if (Convert.ToDouble(oqty.Text) < Convert.ToDouble(qty.Text))
            //    {
            //        qty.Text = "0.00";
            //        string message = "alert('* Receive quantity must not be greater than the order quantity.')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //        qty.Focus();
            //        return;
            //    }
            //    //------------------------------------------------------------------

            //    CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            //        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_CHECK_TEXT");
            //        SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            //        SQL_PARAMS[2] = OBJ_METHOD.createParams("@SearchText", SqlDbType.VarChar, 500, NAME.Text);

            //        DataSet Ds1 = OBJ_METHOD.Get_DataSet("STORE_PURACHASE_SELECT", false, true, SQL_PARAMS);
            //        if (Ds1.Tables[0].Rows.Count > 0)
            //        {
            //            rec = "0";
            //            rec = Ds1.Tables[0].Rows[0]["RQTY"].ToString();
            //        }
            //        #region oldcode
            //        //using (SqlCommand com = new SqlCommand("STORE_PURACHASE_SELECT", con))
            //        //{
            //        //    com.CommandType = CommandType.StoredProcedure;
            //        //    com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CHECK_TEXT";
            //        //    com.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = NAME.Text;
            //        //    //SqlCommand com = new SqlCommand("select ISNULL(SUM(RQTY),0) AS RQTY from PO_TRAN WHERE ITEMNAME='" + NAME.Text + "'", con);
            //        //    dr = com.ExecuteReader();
            //        //    if (dr.Read())
            //        //    {
            //        //        rec = "0";
            //        //        rec = dr["RQTY"].ToString();
            //        //    }
            //        //    dr.Close();
            //        //}
            //        //con.Close();
            //        #endregion
            //        try
            //        {
            //            amount.Text = Math.Round((Convert.ToDouble(qty.Text)) * Convert.ToDouble(price.Text)).ToString();
            //            if (txtstatecode.Text == "21")
            //            {
            //                gstamt.Text = Math.Round(((Convert.ToDouble(amount.Text) / 100) * (Convert.ToDouble(cgst.Text))) * 2).ToString();
            //                sgst.Text = cgst.Text;
            //            }
            //            else
            //            {
            //                gstamt.Text = Math.Round((Convert.ToDouble(amount.Text) / 100) * (Convert.ToDouble(igst.Text))).ToString();
            //            }

            //            // gstamIt.Text = Math.Round((Convert.ToDouble(amtIt.Text) / 100) * (Convert.ToDouble(sgstIt.Text))).ToString();
            //            totalamt.Text = Math.Round(Convert.ToDouble(amount.Text) + Convert.ToDouble(gstamt.Text)).ToString();
            //            // totamtIt.Text = Math.Round(( ((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) ) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine("An error occurred: '{0}'", ex);
            //        }
            //        if (chkRow.Checked)
            //        {
            //            REMQTY.Text = (Convert.ToDouble(oqty.Text) - (Convert.ToDouble(qty.Text) + Convert.ToDouble(rec.ToString()))).ToString();
            //            amount1 = amount1 + Convert.ToDecimal(totalamt.Text);
            //            gstamount = gstamount + Convert.ToDecimal(gstamt.Text);
            //        }
            //        else
            //        {
            //            qty.Text = "0.00";
            //            REMQTY.Text = (Convert.ToDouble(oqty.Text) - (Convert.ToDouble(qty.Text) + Convert.ToDouble(rec))).ToString();
            //        }
            //    }

            //    lblgrandtotal.Text = amount1.ToString();
            //    lblgstamt.Text = gstamount.ToString();
            //    lbltotalprice.Text = (Convert.ToDouble(lblgrandtotal.Text) - Convert.ToDouble(lblgstamt.Text)).ToString();
            //}
            #endregion

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void grvPurchase_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = grvPurchase.DataKeys[e.NewSelectedIndex].Values["GRNNO"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SEARCH_ByINDEX");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@GRNNO", SqlDbType.VarChar, 500, slno);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("SP_Purchase_page", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                btncancel.Visible = true;
                btndelete.Visible = true;
                lblEditgrd.Text = slno.ToString();
                dropVendor.Text = Ds1.Tables[0].Rows[0]["VENDOR"].ToString();
                txtstatecode.Text = Ds1.Tables[0].Rows[0]["VSCODE"].ToString();
                txtgrnno.Text = Ds1.Tables[0].Rows[0]["GRNNO"].ToString();
                txtgrndate.Text = Ds1.Tables[0].Rows[0]["GRNDATE"].ToString();
                txtinvoiceno.Text = Ds1.Tables[0].Rows[0]["INVOICENO"].ToString();
                txtinvoicedate.Text = Ds1.Tables[0].Rows[0]["INVOICEDATE"].ToString();
                txtpono.Text = Ds1.Tables[0].Rows[0]["PONO"].ToString();
                txtpodate.Text = Ds1.Tables[0].Rows[0]["PODATE"].ToString();

                lbltotalprice.Text = Ds1.Tables[0].Rows[0]["TOTALPRICE"].ToString();
                lblgstamt.Text = Ds1.Tables[0].Rows[0]["GSTAMOUNT"].ToString();
                lbltotaldisc.Text = Ds1.Tables[0].Rows[0]["DISCOUNTAMOUNT"].ToString();
                lblgrandtotal.Text = Ds1.Tables[0].Rows[0]["GRANDTOTAL"].ToString();
                SQL_PARAMS = new SqlParameter[3];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SEARCH_BYid");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@GRNNO", SqlDbType.VarChar, 500, slno);

                DataSet Ds = OBJ_METHOD.Get_DataSet("SP_Purchase_page", false, true, SQL_PARAMS);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    grvpuritemTemp.DataSource = Ds.Tables[0];
                    grvpuritemTemp.DataBind();
                    ViewState["ITEM"] = Ds.Tables[0];
                    grvpuritemTemp.Visible = true;
                }
            }
            normaldiv.Visible = true;
            #region oldcode
            //using (SqlCommand com = new SqlCommand("SP_Purchase_page", con))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH_BYid";
            //    com.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = slno;
            //    dr = com.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        btncreate.Visible = false;
            //        btnupdate.Visible = true;
            //        btncancel.Visible = true;
            //        btndelete.Visible = true;
            //        lblEditgrd.Text = slno.ToString();
            //        dropVendor.Text = dr["VENDOR"].ToString();
            //        txtstatecode.Text = dr["VSCODE"].ToString();
            //        txtgrnno.Text = dr["GRNNO"].ToString();
            //        txtgrndate.Text = dr["GRNDATE"].ToString();
            //        txtinvoiceno.Text = dr["INVOICENO"].ToString();
            //        txtinvoicedate.Text = dr["INVOICEDATE"].ToString();
            //        txtpono.Text = dr["PONO"].ToString();
            //        txtpodate.Text = dr["PODATE"].ToString();

            //        lbltotalprice.Text = dr["TOTALPRICE"].ToString();
            //        lblgstamt.Text = dr["GSTAMOUNT"].ToString();
            //        lbltotaldisc.Text = dr["DISCOUNTAMOUNT"].ToString();
            //        lblgrandtotal.Text = dr["GRANDTOTAL"].ToString();
            //        dr.Close();
            //        DataTable dt = new DataTable();
            //        //SqlDataAdapter da = new SqlDataAdapter("select HSNCODE,ITEMNAME as NAME,UNIT,QTY,PRICE,AMOUNT,CGST,SGST,IGST,GSTAMT as GSTMAT,TOTALAMT as TOTALAMOUNT,RECQTY,IsSelected,REMQTY from GRN_ITEM_TABLE where GRNNO='" + slno + "'", con);

            //        using (SqlCommand com1 = new SqlCommand("SP_Purchase_page", con))
            //        {
            //            com1.CommandType = CommandType.StoredProcedure;
            //            com1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH_BYid";
            //            com.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = slno;
            //            SqlDataAdapter da = new SqlDataAdapter(com1);
            //            da.Fill(dt);
            //            grvpuritemTemp.DataSource = dt;
            //            // grdrfq.DataKeyNames = new string[] { "ID" };
            //            grvpuritemTemp.DataBind();
            //            // DataTable dt = ds2.Tables["Table"];
            //            ViewState["ITEM"] = dt;
            //            grvpuritemTemp.Visible = true;
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
    protected void grvPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BIND2_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("STORE_PURACHASE_SELECT", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                grvPurchase.DataSource = Ds1;
                grvPurchase.PageIndex = e.NewPageIndex;
                grvPurchase.DataBind();
            }
            //using (SqlCommand com = new SqlCommand("STORE_PURACHASE_SELECT", con))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BIND2_PAGE";
            //    com.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter adp = new SqlDataAdapter(com);
            //    //SqlDataAdapter adp = new SqlDataAdapter("select b.NAME1,a.GRNNO,a.GRNDATE,a.INVOICENO,a.INVOICEDATE from GRN_TABLE a,VENDER_MASTER_TABLE b where a.VENDOR=b.ID order by a.GRNNO desc", con);
            //    DataTable dt1 = new DataTable();
            //    adp.Fill(dt1);
            //    grvPurchase.DataSource = dt1;
            //    grvPurchase.PageIndex = e.NewPageIndex;
            //    //    grdrfq.DataKeyNames = new string[] { "id" };
            //    grvPurchase.DataBind();
            //}
            //con.Close();
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
            if (dropVendor.Text == "")
            {
                string message = "alert('* Select Vendor.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtgrndate.Text == "")
            {
                string message = "alert('* Select GRN Date.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtinvoiceno.Text == "")
            {
                string message = "alert('* Select Invoice No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtinvoicedate.Text == "")
            {
                string message = "alert('* Select Invoice Date.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[14];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@VENDOR", SqlDbType.VarChar, 500, dropVendor.SelectedValue);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@GRNDATE", SqlDbType.Date, 0, Convert.ToDateTime(txtgrndate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@VSCODE", SqlDbType.VarChar, 500, txtstatecode.Text);

            SQL_PARAMS[5] = OBJ_METHOD.createParams("@GRNNO", SqlDbType.VarChar, 500, lblEditgrd.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@INVOICENO", SqlDbType.VarChar, 500, txtinvoiceno.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@INVOICEDATE", SqlDbType.Date, 0, lblgstamt.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@TOTALPRICE", SqlDbType.VarChar, 500, lbltotaldisc.Text);

            SQL_PARAMS[9] = OBJ_METHOD.createParams("@DISCOUNTAMOUNT", SqlDbType.VarChar, 500, lbltotaldisc.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@GSTAMOUNT", SqlDbType.VarChar, 500, lblgstamt.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@GRANDTOTAL", SqlDbType.VarChar, 500, lblgrandtotal.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@PODATE", SqlDbType.Date, 0, txtpodate.Text);
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@PONO", SqlDbType.VarChar, 500, txtpono.Text);

            OBJ_METHOD.ExecuteProceedure("GRN_OPERATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
            if (OBJ_METHOD._RESULT > 0)
            {
                int chkedcounter = 0;
                int correctinput = 0;
                if (OBJ_METHOD._RESULT > 0)
                {
                    foreach (GridViewRow row in grvpurchsItem.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);

                            //if (chkRow.Checked)
                            //{
                            var name = row.FindControl("lbl_name") as Label;
                            var hsncode = row.FindControl("lbl_hsn") as Label;
                            var unit = row.FindControl("lbl_unit") as Label;
                            var price = row.FindControl("lbl_price") as Label;
                            var qty = row.FindControl("lbl_qty") as Label;
                            var remqty = row.FindControl("lbl_remqty") as Label;
                            var rqty = row.FindControl("txt_Qty") as TextBox;
                            var amount = row.FindControl("lbl_amount") as Label;
                            var cgst = row.FindControl("lbl_cgst") as Label;
                            var sgst = row.FindControl("lbl_sgst") as Label;
                            var igst = row.FindControl("lbl_igst") as Label;
                            var gstamt = row.FindControl("lbl_gstamt") as Label;
                            var totalamt = row.FindControl("lbl_totalamount") as Label;
                            chkedcounter++;
                            SQL_PARAMS = new SqlParameter[18];

                            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDINSERT");
                            SQL_PARAMS[1] = OBJ_METHOD.createParams("@GRNNO", SqlDbType.VarChar, 500, lblEditgrd.Text);
                            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ITEMNAME", SqlDbType.VarChar, 500, name.Text);
                            SQL_PARAMS[3] = OBJ_METHOD.createParams("@HSNCODE", SqlDbType.VarChar, 500, hsncode.Text);
                            SQL_PARAMS[4] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, unit.Text);
                            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                            SQL_PARAMS[7] = OBJ_METHOD.createParams("@PRICE", SqlDbType.VarChar, 500, price.Text);
                            SQL_PARAMS[8] = OBJ_METHOD.createParams("@QTY", SqlDbType.VarChar, 500, qty.Text);
                            SQL_PARAMS[9] = OBJ_METHOD.createParams("@RECQTY", SqlDbType.VarChar, 500, rqty.Text);
                            SQL_PARAMS[10] = OBJ_METHOD.createParams("@REMQTY", SqlDbType.VarChar, 500, remqty.Text);
                            SQL_PARAMS[11] = OBJ_METHOD.createParams("@AMOUNT", SqlDbType.VarChar, 500, amount.Text);
                            SQL_PARAMS[12] = OBJ_METHOD.createParams("@CGST", SqlDbType.VarChar, 500, cgst.Text);
                            SQL_PARAMS[13] = OBJ_METHOD.createParams("@SGST", SqlDbType.VarChar, 500, sgst.Text);
                            SQL_PARAMS[14] = OBJ_METHOD.createParams("@IGST", SqlDbType.VarChar, 500, igst.Text);
                            SQL_PARAMS[15] = OBJ_METHOD.createParams("@GSTAMT", SqlDbType.VarChar, 500, gstamt.Text);
                            SQL_PARAMS[16] = OBJ_METHOD.createParams("@TOTALAMT", SqlDbType.VarChar, 500, totalamt.Text);
                            SQL_PARAMS[17] = OBJ_METHOD.createParams("@IsSelected", SqlDbType.Bit, 0, chkRow.Checked);
                            OBJ_METHOD.ExecuteProceedure("GRN_OPERATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
                            if (OBJ_METHOD._RESULT > 0)
                            {
                                correctinput++;
                            }
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
            //    using (SqlCommand stock_cmd = new SqlCommand("GRN_OPERATION", con))
            //    {
            //        stock_cmd.CommandType = CommandType.StoredProcedure;
            //        stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //        stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //        stock_cmd.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
            //        stock_cmd.Parameters.Add("@VSCODE", SqlDbType.VarChar).Value = txtstatecode.Text;
            //        stock_cmd.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = lblEditgrd.Text;
            //        stock_cmd.Parameters.Add("@GRNDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtgrndate.Text).ToString("yyyy-MM-dd");
            //        stock_cmd.Parameters.Add("@INVOICENO", SqlDbType.VarChar).Value = txtinvoiceno.Text;
            //        stock_cmd.Parameters.Add("@INVOICEDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvoicedate.Text).ToString("yyyy-MM-dd");
            //        stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
            //        stock_cmd.Parameters.Add("@DISCOUNTAMOUNT", SqlDbType.VarChar).Value = lbltotaldisc.Text;
            //        stock_cmd.Parameters.Add("@GSTAMOUNT", SqlDbType.VarChar).Value = lblgstamt.Text;
            //        stock_cmd.Parameters.Add("@GRANDTOTAL", SqlDbType.VarChar).Value = lblgrandtotal.Text;
            //        stock_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "0.00";
            //        stock_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "0.00";
            //        stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "0.00";
            //        stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = "0.00";
            //        stock_cmd.Parameters.Add("@RECQTY", SqlDbType.VarChar).Value = "0.00";
            //        stock_cmd.Parameters.Add("@REMQTY", SqlDbType.VarChar).Value = "0.00";
            //        stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "0.00";
            //        stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = "0.00";
            //        stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = "0.00";
            //        stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = "0.00";
            //        stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = "0.00";
            //        stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = "0.00";
            //        stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = "0.00";
            //        stock_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
            //        stock_cmd.Parameters.Add("@PODATE", SqlDbType.Date).Value = Convert.ToDateTime(txtpodate.Text).ToString("yyyy-MM-dd");
            //        stock_cmd.Parameters.Add("@IsSelected", SqlDbType.Bit).Value = "True";
            //        stock_cmd.ExecuteNonQuery();
            //    }
            //    foreach (GridViewRow row in grvpuritemTemp.Rows)
            //    {
            //        if (row.RowType == DataControlRowType.DataRow)
            //        {
            //            CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);

            //            var name = row.FindControl("lbl_name") as Label;
            //            var hsncode = row.FindControl("lbl_hsn") as Label;
            //            var unit = row.FindControl("lbl_unit") as Label;
            //            var price = row.FindControl("lbl_price") as Label;
            //            var qty = row.FindControl("lbl_qty") as Label;
            //            var remqty = row.FindControl("lbl_remqty") as Label;
            //            var rqty = row.FindControl("txt_qty") as TextBox;
            //            var amount = row.FindControl("lbl_amount") as Label;
            //            var cgst = row.FindControl("lbl_cgst") as Label;
            //            var sgst = row.FindControl("lbl_sgst") as Label;
            //            var igst = row.FindControl("lbl_igst") as Label;
            //            var gstamt = row.FindControl("lbl_gstamt") as Label;
            //            var totalamt = row.FindControl("lbl_totalamount") as Label;
            //            using (SqlCommand stock_cmd = new SqlCommand("GRN_OPERATION", con))
            //            {
            //                stock_cmd.CommandType = CommandType.StoredProcedure;
            //                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDUPDATE";
            //                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //                stock_cmd.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
            //                stock_cmd.Parameters.Add("@VSCODE", SqlDbType.VarChar).Value = txtstatecode.Text;
            //                stock_cmd.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = lblEditgrd.Text;
            //                stock_cmd.Parameters.Add("@GRNDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtgrndate.Text).ToString("yyyy-MM-dd");
            //                stock_cmd.Parameters.Add("@INVOICENO", SqlDbType.VarChar).Value = txtinvoiceno.Text;
            //                stock_cmd.Parameters.Add("@INVOICEDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvoicedate.Text).ToString("yyyy-MM-dd");
            //                stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
            //                stock_cmd.Parameters.Add("@DISCOUNTAMOUNT", SqlDbType.VarChar).Value = lbltotaldisc.Text;
            //                stock_cmd.Parameters.Add("@GSTAMOUNT", SqlDbType.VarChar).Value = lblgstamt.Text;
            //                stock_cmd.Parameters.Add("@GRANDTOTAL", SqlDbType.VarChar).Value = lblgrandtotal.Text;
            //                stock_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
            //                stock_cmd.Parameters.Add("@PODATE", SqlDbType.Date).Value = txtpodate.Text;
            //                stock_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = name.Text;
            //                stock_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = hsncode.Text;
            //                stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unit.Text;
            //                stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = price.Text;
            //                stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = qty.Text;
            //                stock_cmd.Parameters.Add("@RECQTY", SqlDbType.VarChar).Value = rqty.Text;
            //                stock_cmd.Parameters.Add("@REMQTY", SqlDbType.VarChar).Value = remqty.Text;
            //                stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = amount.Text;
            //                stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = cgst.Text;
            //                stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = sgst.Text;
            //                stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = igst.Text;
            //                stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = gstamt.Text;
            //                stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = totalamt.Text;
            //                stock_cmd.Parameters.Add("@IsSelected", SqlDbType.Bit).Value = chkRow.Checked;
            //                stock_cmd.ExecuteNonQuery();
            //            }

            //        }
            //    }
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
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@GRNNO", SqlDbType.VarChar, 500, lblEditgrd.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

            OBJ_METHOD.ExecuteProceedure("GRN_OPERATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clearfield();

            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            #region delete code
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //using (SqlCommand stock_cmd = new SqlCommand("GRN_OPERATION", con))
            //{
            //    stock_cmd.CommandType = CommandType.StoredProcedure;
            //    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //    stock_cmd.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
            //    stock_cmd.Parameters.Add("@VSCODE", SqlDbType.VarChar).Value = txtstatecode.Text;
            //    stock_cmd.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = lblEditgrd.Text;
            //    stock_cmd.Parameters.Add("@GRNDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtgrndate.Text).ToString("yyyy-MM-dd");
            //    stock_cmd.Parameters.Add("@INVOICENO", SqlDbType.VarChar).Value = txtinvoiceno.Text;
            //    stock_cmd.Parameters.Add("@INVOICEDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvoicedate.Text).ToString("yyyy-MM-dd");
            //    stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
            //    stock_cmd.Parameters.Add("@DISCOUNTAMOUNT", SqlDbType.VarChar).Value = lbltotaldisc.Text;
            //    stock_cmd.Parameters.Add("@GSTAMOUNT", SqlDbType.VarChar).Value = lblgstamt.Text;
            //    stock_cmd.Parameters.Add("@GRANDTOTAL", SqlDbType.VarChar).Value = lblgrandtotal.Text;
            //    stock_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@RECQTY", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@REMQTY", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = "0.00";
            //    stock_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
            //    stock_cmd.Parameters.Add("@PODATE", SqlDbType.Date).Value = Convert.ToDateTime(txtpodate.Text).ToString("yyyy-MM-dd");
            //    stock_cmd.Parameters.Add("@IsSelected", SqlDbType.Bit).Value = "True";
            //    stock_cmd.ExecuteNonQuery();
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
    protected void btncancel_Click(object sender, EventArgs e)
    {
        
        
        binddata();
        clearfield();
    }
    protected void txtinvoicedate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime dtpo = Convert.ToDateTime(txtpodate.Text.ToString());
            DateTime dtInvoic = Convert.ToDateTime(txtinvoicedate.Text.ToString());

            if (dtpo > dtInvoic)
            {
                string message = "alert('*Invoice Date Is not less than PO Date .')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtinvoicedate.Text = "";
                return;
            }
            txtMaterial.Focus();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedIndex == 1)
        {
            normaldiv.Visible = false;
            txtpono.Enabled = false;
            txtpono.Text = "";
            directdiv.Visible = true;
            
            grvpurchsItem.DataSource = null;
            grvpurchsItem.DataBind();
            grvpuritemTemp.DataSource = null;
            grvpuritemTemp.DataBind();
            //DataTable dt = new DataTable();
            //dt = null;
            //ViewState["ITEM1"] = dt;
            lbltotaldisc.Text = "0";
            lblgrandtotal.Text = "0";
            lblgstamt.Text = "0";
            lbltotalprice.Text = "0";
            lbltotaladd.Text = "0";
            lbltotalamt.Text = "";
            lbltotaldicadd.Text = "0";
            lbltotaldisc.Text = "0";
            lbltotalgstadd.Text = "0";
            lbltotalprice.Text = "0";
            lblpriceadd.Text = "0";
            dropVendor.Focus();
           
        }
        else if (DropDownList1.SelectedIndex == 2)
        {
            normaldiv.Visible = true;

            txtpono.Enabled = true;
            directdiv.Visible = false;
            
            grvpurchsItem.DataSource = null;
            grvpurchsItem.DataBind();
            grvpuritemTemp.DataSource = null;
            grvpuritemTemp.DataBind();
            
            lbltotaldisc.Text = "0";
            lblgrandtotal.Text = "0";
            lblgstamt.Text = "0";
            lbltotalprice.Text = "0";
            lbltotaladd.Text = "0";
            lbltotalamt.Text = "";
            lbltotaldicadd.Text = "0";
            lbltotaldisc.Text = "0";
            lbltotalgstadd.Text = "0";
            lbltotalprice.Text = "0";
            lblpriceadd.Text = "0";
            txtinvoiceno.Focus();
            txtpodate.Enabled = false;
        }
        else
        {
            normaldiv.Visible = false;
            txtpono.Enabled = false;
            txtpono.Text = "";
            directdiv.Visible = false;
            
            
            grvpurchsItem.DataSource = null;
            grvpurchsItem.DataBind();
            grvpuritemTemp.DataSource = null;
            grvpuritemTemp.DataBind();
            lbltotaldisc.Text = "0";
            lblgrandtotal.Text = "0";
            lblgstamt.Text = "0";
            lbltotalprice.Text = "0";
            lbltotaladd.Text = "0";
            lbltotalamt.Text = "";
            lbltotaldicadd.Text = "0";
            lbltotaldisc.Text = "0";
            lbltotalgstadd.Text = "0";
            lbltotalprice.Text = "0";
            lblpriceadd.Text = "0";
            DropDownList1.Focus();
        }

    }
    public void clear_control()
    {
        txtMaterial.Text = "";
        txtUnit.Text = "";
        txtqty.Text = "";
        hdngst.Value = "";
        hdnhsncd.Value = "";
        hdnprice.Value = "";
        hdnqty.Value = "";
        lblamt.Text = "";
        lblcgst.Text = "";
        lblsgst.Text = "";
        lblgstamnt.Text = "";
        lbltotalamt.Text = "";
    }
    protected void txtMaterial_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from MATERIAL_MASTER_TABLE where NAME='" + txtMaterial.Text + "' and Branch_ID='"+ Session["Branch"] +"' ", false, false);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                txtUnit.Text = Ds1.Tables[0].Rows[0]["UNIT"].ToString();
                hdnprice.Value = Ds1.Tables[0].Rows[0]["PRICE"].ToString();
                hdngst.Value = Ds1.Tables[0].Rows[0]["GST"].ToString();
                hdnhsncd.Value = Ds1.Tables[0].Rows[0]["HSNCODE"].ToString();
                hdnqty.Value = Ds1.Tables[0].Rows[0]["QTY"].ToString();
                lblsgst.Text = lblcgst.Text = Math.Round(Convert.ToDouble(hdngst.Value) / 2).ToString(".00");
            }
            //SqlCommand com = new SqlCommand("select * from MATERIAL_MASTER_TABLE where NAME='" + txtMaterial.Text + "'", con);
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    txtUnit.Text = dr["UNIT"].ToString();
            //    hdnprice.Value = dr["PRICE"].ToString();
            //    hdngst.Value = dr["GST"].ToString();
            //    hdnhsncd.Value = dr["HSNCODE"].ToString();
            //    hdnqty.Value = dr["QTY"].ToString();
            //}
            //con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void bindTGrid()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[11] { new DataColumn("NAME"), new DataColumn("UNIT"), new DataColumn("QTY"), new DataColumn("HSNCODE"), new DataColumn("PRICE"), new DataColumn("AMOUNT"), new DataColumn("CGST"), new DataColumn("SGST"), new DataColumn("IGST"), new DataColumn("GSTAMT"), new DataColumn("TOTALAMOUNT") });
        ViewState["ITEM1"] = dt;
        this.BindGrid1();
    }
    protected void BindGrid1()
    {
        try
        {
            //grdMaterial.DataSource = null;
            grdMaterial.DataSource = (DataTable)ViewState["ITEM1"];
            grdMaterial.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if(txtMaterial.Text=="")
            {
                string message = "alert('*Item Name Is Mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtMaterial.Focus();
                return;
            }
            if (txtqty.Text == "" || txtqty.Text == "0")
            {
                string message = "alert('*Quantity Is Mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtqty.Focus();
                return;
            }
            
            DataTable dt = (DataTable)ViewState["ITEM1"];
            dt.Rows.Add(txtMaterial.Text.Trim(), txtUnit.Text.Trim(), txtqty.Text.Trim(),hdnhsncd.Value,hdnprice.Value,lblamt.Text.Trim(),lblcgst.Text.Trim(),lblsgst.Text.Trim(),lbligst.Text.Trim(),lblgstamnt.Text,lbltotalamt.Text.Trim());
            ViewState["ITEM1"] = dt;
            this.BindGrid1();

            if (lblpriceadd.Text == "0")
            {
                lblpriceadd.Text = Convert.ToDecimal(lblamt.Text).ToString();
            }
            else
            {
                lblpriceadd.Text = Convert.ToDecimal(Convert.ToDecimal(lblpriceadd.Text) +Convert.ToDecimal(lblamt.Text)).ToString();
            }
            if (lbltotalgstadd.Text == "0")
            {
                lbltotalgstadd.Text = Convert.ToDecimal(lblgstamnt.Text).ToString();
            }
            else
            {
                lbltotalgstadd.Text = Convert.ToDecimal(Convert.ToDecimal(lbltotalgstadd.Text) + Convert.ToDecimal(lblgstamnt.Text)).ToString();
            }
            if (lbltotaladd.Text == "0")
            {
                lbltotaladd.Text = Convert.ToDecimal(lbltotalamt.Text).ToString();
            }
            else 
            {
                lbltotaladd.Text = Convert.ToDecimal(Convert.ToDecimal(lbltotaladd.Text) + Convert.ToDecimal(lbltotalamt.Text)).ToString();
            }
           
            clear_control();
        }
        catch (Exception ex)
        {
            Trace.Write(ex.Message);
        }
    }
    protected void grdMaterial_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = (DataTable)ViewState["ITEM1"];
            GridViewRow row = (GridViewRow)grdMaterial.Rows[e.RowIndex];

            Label nameit = (Label)row.FindControl("lbl_Name");
            Label unitit = (Label)row.FindControl("lbl_Unit");
            TextBox quantityit = (TextBox)row.FindControl("txt_qty");
            Label hsnno = (Label)row.FindControl("lbl_hsn");
            Label prce = (Label)row.FindControl("lbl_price");
            Label amnt = (Label)row.FindControl("lbl_amount");
            Label cgstit = (Label)row.FindControl("lbl_cgst");
            Label sgstit = (Label)row.FindControl("lbl_sgst");
            Label igstit = (Label)row.FindControl("lbl_igst");
            Label gstamtit = (Label)row.FindControl("lbl_gstamt");
            Label totalit = (Label)row.FindControl("lbl_totalamount");

            string NAME = nameit.Text.ToString();
            string UNIT = unitit.Text.ToString();
            string QTY = quantityit.Text.ToString();
            string HSN = quantityit.Text.ToString();
            string PRICE = prce.Text.ToString();
            string AMT = amnt.Text.ToString();
            string CGST = cgstit.Text.ToString();
            string SGST = sgstit.Text.ToString();
            string IGST = igstit.Text.ToString();
            string GSTAMT = gstamtit.Text.ToString();
            string TOTALAMT = totalit.Text.ToString();

            
            //  lblgrandtotal.Text = (Convert.ToDecimal(lblgrandtotal.Text) - Convert.ToDecimal(TOTALAMT)).ToString();
            lblpriceadd.Text = Convert.ToDecimal(Convert.ToDecimal(lblpriceadd.Text)-Convert.ToDecimal(AMT)).ToString();
            lbltotalgstadd.Text = Convert.ToDecimal(Convert.ToDecimal(lbltotalgstadd.Text) - Convert.ToDecimal(GSTAMT)).ToString();
            lbltotaladd.Text = Convert.ToDecimal(Convert.ToDecimal(lbltotaladd.Text) - Convert.ToDecimal(TOTALAMT)).ToString();
            dt.Rows[index].Delete();
            ViewState["ITEM1"] = dt;
            this.BindGrid1();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void txtqty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblamt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtqty.Text) * Convert.ToDouble(hdnprice.Value))).ToString(".00");
            lblgstamnt.Text = Math.Round(Convert.ToDouble(lblamt.Text) * (Convert.ToDouble(hdngst.Value) / 100)).ToString(".00");
            lbltotalamt.Text = Math.Round(Convert.ToDouble(lblamt.Text) + Convert.ToDouble(lblgstamnt.Text)).ToString(".00");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grvpurchsItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = (DataTable)ViewState["ITEM"];
            GridViewRow row = (GridViewRow)grvpurchsItem.Rows[e.RowIndex];

            Label nameit = (Label)row.FindControl("lbl_name");
            Label unitit = (Label)row.FindControl("lbl_unit");
            Label quantityit = (Label)row.FindControl("lbl_qty");
            Label hsnno = (Label)row.FindControl("lbl_hsn");
            Label prce = (Label)row.FindControl("lbl_price");
            TextBox recqunt = (TextBox)row.FindControl("txt_Qty");
            Label amnt = (Label)row.FindControl("lbl_amount");
            Label cgstit = (Label)row.FindControl("lbl_cgst");
            Label sgstit = (Label)row.FindControl("lbl_sgst");
            Label igstit = (Label)row.FindControl("lbl_igst");
            Label gstamtit = (Label)row.FindControl("lbl_gstamt");
            Label totalit = (Label)row.FindControl("lbl_totalamount");

            string NAME = nameit.Text.ToString();
            string UNIT = unitit.Text.ToString();
            string QTY = quantityit.Text.ToString();
            string HSN = quantityit.Text.ToString();
            string PRICE = prce.Text.ToString();
            string AMT = amnt.Text.ToString();
            string CGST = cgstit.Text.ToString();
            string SGST = sgstit.Text.ToString();
            string IGST = igstit.Text.ToString();
            string GSTAMT = gstamtit.Text.ToString();
            string TOTALAMT = totalit.Text.ToString();


            //  lblgrandtotal.Text = (Convert.ToDecimal(lblgrandtotal.Text) - Convert.ToDecimal(TOTALAMT)).ToString();
            lbltotalprice.Text = Math.Round(Convert.ToDecimal(lbltotalprice.Text) - Convert.ToDecimal(AMT)).ToString(".00");
            lblgstamt.Text = Math.Round(Convert.ToDecimal(lblgstamt.Text) - Convert.ToDecimal(GSTAMT)).ToString(".00");
            lblgrandtotal.Text = Math.Round(Convert.ToDecimal(lblgrandtotal.Text) - Convert.ToDecimal(TOTALAMT)).ToString(".00");
            dt.Rows[index].Delete();
            ViewState["ITEM"] = dt;
            this.BindGrid1();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
    {
        try
        {
           foreach (GridViewRow row in grvpurchsItem.Rows)
           {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);

                    Label nameit = (Label)row.FindControl("lbl_name");
                    Label unitit = (Label)row.FindControl("lbl_unit");
                    Label quantityit = (Label)row.FindControl("lbl_qty");
                    Label hsnno = (Label)row.FindControl("lbl_hsn");
                    Label prce = (Label)row.FindControl("lbl_price");
                    TextBox recqunt = (TextBox)row.FindControl("txt_Qty");
                    Label amnt = (Label)row.FindControl("lbl_amount");
                    Label cgstit = (Label)row.FindControl("lbl_cgst");
                    Label sgstit = (Label)row.FindControl("lbl_sgst");
                    Label igstit = (Label)row.FindControl("lbl_igst");
                    Label gstamtit = (Label)row.FindControl("lbl_gstamt");
                    Label totalit = (Label)row.FindControl("lbl_totalamount");

                    string NAME = nameit.Text.ToString();
                    string UNIT = unitit.Text.ToString();
                    string QTY = quantityit.Text.ToString();
                    string HSN = quantityit.Text.ToString();
                    string PRICE = prce.Text.ToString();
                    string AMT = amnt.Text.ToString();
                    string CGST = cgstit.Text.ToString();
                    string SGST = sgstit.Text.ToString();
                    string IGST = igstit.Text.ToString();
                    string GSTAMT = gstamtit.Text.ToString();
                    string TOTALAMT = totalit.Text.ToString();
                    if (chkRow.Checked)
                    {
                        recqunt.Focus();
                        //if (recqunt.Text == "0.00" || recqunt.Text == "")
                        //{
                        //    string message = "alert('* Required Quantity Is mandatory.')";
                        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        //    recqunt.Focus();
                        //    return;
                        //}
                    }

                    //if (chkRow.Checked == false)
                    //{
                    //    lbltotalprice.Text = Convert.ToDecimal(Convert.ToDecimal(lbltotalprice.Text) - Convert.ToDecimal(AMT)).ToString();
                    //    lblgstamt.Text = Convert.ToDecimal(Convert.ToDecimal(lblgstamt.Text) - Convert.ToDecimal(GSTAMT)).ToString();
                    //    lblgrandtotal.Text = Convert.ToDecimal(Convert.ToDecimal(lblgrandtotal.Text) - Convert.ToDecimal(TOTALAMT)).ToString();
                        
                    //}
                    //else
                    //{
                    //    lbltotalprice.Text = Convert.ToDecimal(Convert.ToDecimal(lbltotalprice.Text) + Convert.ToDecimal(AMT)).ToString();
                    //    lblgstamt.Text = Convert.ToDecimal(Convert.ToDecimal(lblgstamt.Text) + Convert.ToDecimal(GSTAMT)).ToString();
                    //    lblgrandtotal.Text = Convert.ToDecimal(Convert.ToDecimal(lblgrandtotal.Text) + Convert.ToDecimal(TOTALAMT)).ToString();
                    //}
                }
           }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void txt_Qty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            
            foreach (GridViewRow row in grvpurchsItem.Rows)
            {
                Label nameit = (Label)row.FindControl("lbl_name");
                Label unitit = (Label)row.FindControl("lbl_unit");
                Label quantityit = (Label)row.FindControl("lbl_qty");
                Label hsnno = (Label)row.FindControl("lbl_hsn");
                Label prce = (Label)row.FindControl("lbl_price");
                TextBox recqunt = (TextBox)row.FindControl("txt_Qty");
                Label amnt = (Label)row.FindControl("lbl_amount");
                Label cgstit = (Label)row.FindControl("lbl_cgst");
                Label sgstit = (Label)row.FindControl("lbl_sgst");
                Label igstit = (Label)row.FindControl("lbl_igst");
                Label gstamtit = (Label)row.FindControl("lbl_gstamt");
                Label totalit = (Label)row.FindControl("lbl_totalamount");

                string NAME = nameit.Text.ToString();
                string UNIT = unitit.Text.ToString();
                string QTY = quantityit.Text.ToString();
                string HSN = quantityit.Text.ToString();
                string REQTY = recqunt.Text.ToString();
                string PRICE = prce.Text.ToString();
                string AMT = amnt.Text.ToString();
                string CGST = cgstit.Text.ToString();
                string SGST = sgstit.Text.ToString();
                string IGST = igstit.Text.ToString();
                string GSTAMT = gstamtit.Text.ToString();
                string TOTALAMT = totalit.Text.ToString();
                CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);
                if (row.RowType == DataControlRowType.DataRow)
                {
                    if (chkRow.Checked)
                    {
                        if (recqunt.Text == "0.00" || recqunt.Text == "")
                        {
                            string message = "alert('* Required Quantity Is mandatory.')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            recqunt.Focus();
                            return;
                        }
                        amnt.Text = Math.Round(Convert.ToDouble(PRICE) * Convert.ToDouble(REQTY)).ToString(".00");
                        gstamtit.Text = Math.Round(Convert.ToDouble(amnt.Text) * (Convert.ToDouble(Convert.ToDouble(((Convert.ToDouble(CGST)) + (Convert.ToDouble(SGST)) + (Convert.ToDouble(IGST))) / 100)))).ToString(".00");
                        totalit.Text = Math.Round(Convert.ToDouble(amnt.Text) + Convert.ToDouble(gstamtit.Text)).ToString(".00");
                        amount = amount + Convert.ToDecimal(amnt.Text);
                        gstamount = gstamount + Convert.ToDecimal(gstamtit.Text);
                        totalamount = totalamount + Convert.ToDecimal(totalit.Text);
                        
                    }
                    else
                    {
                        //lbltotalprice.Text = Convert.ToDecimal(Convert.ToDecimal(lbltotalprice.Text) - Convert.ToDecimal(amnt.Text)).ToString();
                        //lblgstamt.Text = Convert.ToDecimal(Convert.ToDecimal(lblgstamt.Text) - Convert.ToDecimal(gstamtit.Text)).ToString();
                        //lblgrandtotal.Text = Convert.ToDecimal(Convert.ToDecimal(lblgrandtotal.Text) - Convert.ToDecimal(totalit.Text)).ToString();
                        recqunt.Text = "0.00";

                    }
                }
            }
            lbltotalprice.Text = Decimal.Round(amount).ToString(".00");
            lblgstamt.Text = Math.Round(gstamount).ToString(".00");
            lblgrandtotal.Text = Math.Round(totalamount).ToString(".00");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void dropVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select STATECODE from VENDER_MASTER_TABLE WHERE ID='"+dropVendor.SelectedValue+"'", false, false);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                txtstatecode.Text = Ds2.Tables[0].Rows[0]["STATECODE"].ToString();
                txtinvoiceno.Focus();
            }
          
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}