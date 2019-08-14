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

public partial class STOREKEEPER_Purchase : System.Web.UI.Page
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
    decimal gstamount = 0;
    DateTime DT;
    string rec;
    double totalamt1, totamt, totalgstamt, dis, dism;
    GridViewRow gr;

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
                //cmd.CommandText = "select DISTINCT PONO from PO_TABLE where PONO like '%'+@SearchText+'%'";
                //cmd.Parameters.AddWithValue("@SearchText", prefix);
                //cmd.Connection = con;

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

        //com = new SqlCommand(qry1, con);
        //dr = null;

        //dr = com.ExecuteReader();

        //while (dr.Read())
        //{
        //    num1 = dr["ID"].ToString();
        //}
        ////num1 = string.Format("PU{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        //txtgrnno.Text = "GRN-" + num1 + 1 + "-" + lblfyear.Text;
        //dr.Close();
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
        using (SqlCommand cmd = new SqlCommand("STORE_PURACHASE_SELECT", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BIND1";
            cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
            da1 = new SqlDataAdapter(cmd);
            //da1 = new SqlDataAdapter("Select NAME1,ID FROM VENDER_MASTER_TABLE", con);
            DataTable ds1 = new DataTable();
            da1.Fill(ds1);
            dropVendor.DataSource = ds1;
            dropVendor.DataTextField = "NAME1";
            dropVendor.DataValueField = "ID";
            dropVendor.DataBind();
            dropVendor.Items.Insert(0, "Please Select");
        }
        //------------------------------------
        using (SqlCommand cmd1 = new SqlCommand("STORE_PURACHASE_SELECT", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BIND2_PAGE";
            cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            //SqlDataAdapter da = new SqlDataAdapter("select b.NAME1,a.GRNNO,a.GRNDATE,a.INVOICENO,a.INVOICEDATE from GRN_TABLE a,VENDER_MASTER_TABLE b where a.VENDOR=b.ID order by a.GRNNO desc", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            grvPurchase.DataSource = dt;
            grvPurchase.DataKeyNames = new string[] { "GRNNO" };
            grvPurchase.DataBind();
        }
        SqlCommand comid = new SqlCommand("select max(GRNNO) as grnid from GRN_TABLE ", con);
        dr1 = comid.ExecuteReader();
        if (dr1.Read())
        {
            txtgrnno.Text = dr1["grnid"].ToString();
        }
        dr1.Close();


        //SqlCommand COM1 = new SqlCommand("SELECT * FROM ORG_TABLE", con);
        //dr = COM1.ExecuteReader();
        //if (dr.Read())
        //{
        //    //Label1.Text = dr["FYEAR"].ToString();
        //    txtorgname.Text = dr["FYEAR"].ToString();
        //    txtaddress.Text = dr["FYEAR"].ToString();
        //    txtgstin.Text = dr["FYEAR"].ToString();
        //    txtpin.Text = dr["FYEAR"].ToString();
        //    txtphone.Text = dr["FYEAR"].ToString();
        //    txtcity.Text = dr["FYEAR"].ToString();
        //    txtdstatecode.Text = dr["FYEAR"].ToString();
        //    txtstate.Text = dr["FYEAR"].ToString();

        //}
        //dr.Close();
        con.Close();
    }
    protected void GVOnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lbl_slno") as Label).Text = (e.Row.RowIndex + 1).ToString();
        }
    }
    protected void BindGrid()
    {
        try
        {
            grvpurchsItem.DataSource = (DataTable)ViewState["ITEM"];
            grvpurchsItem.DataBind();
        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
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
        txtgrndate.Text = DateTime.Now.ToString("dd-MM-yyyy"); 
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[11] { new DataColumn("NAME"), new DataColumn("HSNCODE"), new DataColumn("UNIT"), new DataColumn("PRICE"), new DataColumn("QTY"), new DataColumn("AMOUNT"), new DataColumn("CGST"), new DataColumn("SGST"), new DataColumn("IGST"), new DataColumn("GSTAMT"), new DataColumn("TOTALAMOUNT") });
            ViewState["ITEM"] = dt;
            this.BindGrid();

            binddata();
            bindTGrid();
        }
        con.Close();
    }
    public void STOCK_TABLE()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand stock_cmd = new SqlCommand("GRN_OPERATION", con))
        {
            stock_cmd.CommandType = CommandType.StoredProcedure;
            stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            stock_cmd.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
            stock_cmd.Parameters.Add("@VSCODE", SqlDbType.VarChar).Value = txtstatecode.Text;
            stock_cmd.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = txtgrnno.Text;
            stock_cmd.Parameters.Add("@GRNDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtgrndate.Text).ToString("yyyy-MM-dd");
            stock_cmd.Parameters.Add("@INVOICENO", SqlDbType.VarChar).Value = txtinvoiceno.Text;
            stock_cmd.Parameters.Add("@INVOICEDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvoicedate.Text).ToString("yyyy-MM-dd");
            stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
            stock_cmd.Parameters.Add("@DISCOUNTAMOUNT", SqlDbType.VarChar).Value = lbltotaldisc.Text;
            stock_cmd.Parameters.Add("@GSTAMOUNT", SqlDbType.VarChar).Value = lblgstamt.Text;
            stock_cmd.Parameters.Add("@GRANDTOTAL", SqlDbType.VarChar).Value = lblgrandtotal.Text;
            stock_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@RECQTY", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@REMQTY", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
          //  stock_cmd.Parameters.Add("@PODATE", SqlDbType.Date).Value = Convert.ToDateTime(txtpodate.Text).ToString("yyyy-MM-dd");

            stock_cmd.Parameters.Add("@PODATE", SqlDbType.Date).Value = Convert.ToDateTime(txtpodate.Text).ToString("yyyy-MM-dd");
           
            stock_cmd.Parameters.Add("@IsSelected", SqlDbType.Bit).Value = "True";
            stock_cmd.ExecuteNonQuery();
        }
        con.Close();
        //}
        //catch { }
    }
    public void STOCK_TRAN()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
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
                    var rqty = row.FindControl("txt_qty") as TextBox;
                    var amount = row.FindControl("lbl_amount") as Label;
                    var cgst = row.FindControl("lbl_cgst") as Label;
                    var sgst = row.FindControl("lbl_sgst") as Label;
                    var igst = row.FindControl("lbl_igst") as Label;
                    var gstamt = row.FindControl("lbl_gstamt") as Label;
                    var totalamt = row.FindControl("lbl_totalamount") as Label;
                    using (SqlCommand stock_cmd = new SqlCommand("GRN_OPERATION", con))
                    {
                        stock_cmd.CommandType = CommandType.StoredProcedure;
                        stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
                        stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                        stock_cmd.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
                        stock_cmd.Parameters.Add("@VSCODE", SqlDbType.VarChar).Value = txtstatecode.Text;
                        stock_cmd.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = txtgrnno.Text;
                        stock_cmd.Parameters.Add("@GRNDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtgrndate.Text).ToString("yyyy-MM-dd");
                        stock_cmd.Parameters.Add("@INVOICENO", SqlDbType.VarChar).Value = txtinvoiceno.Text;
                        stock_cmd.Parameters.Add("@INVOICEDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvoicedate.Text).ToString("yyyy-MM-dd");
                        stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
                        stock_cmd.Parameters.Add("@DISCOUNTAMOUNT", SqlDbType.VarChar).Value = lbltotaldisc.Text;
                        stock_cmd.Parameters.Add("@GSTAMOUNT", SqlDbType.VarChar).Value = lblgstamt.Text;
                        stock_cmd.Parameters.Add("@GRANDTOTAL", SqlDbType.VarChar).Value = lblgrandtotal.Text;
                        stock_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
                        stock_cmd.Parameters.Add("@PODATE", SqlDbType.Date).Value = txtpodate.Text;
                        stock_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = name.Text;
                        stock_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = hsncode.Text;
                        stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unit.Text;
                        stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = price.Text;
                        stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = qty.Text;
                        stock_cmd.Parameters.Add("@RECQTY", SqlDbType.VarChar).Value = rqty.Text;
                        stock_cmd.Parameters.Add("@REMQTY", SqlDbType.VarChar).Value = remqty.Text;
                        stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = amount.Text;
                        stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = cgst.Text;
                        stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = sgst.Text;
                        stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = igst.Text;
                        stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = gstamt.Text;
                        stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = totalamt.Text;
                        stock_cmd.Parameters.Add("@IsSelected", SqlDbType.Bit).Value = chkRow.Checked;
                        stock_cmd.ExecuteNonQuery();
                    }
                    //using (SqlCommand stock_cmd = new SqlCommand("STORE_GRN_Tran", con))
                    //{
                    //    stock_cmd.CommandType = CommandType.StoredProcedure;
                    //    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                    //    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    //    stock_cmd.Parameters.Add("@DATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtgrndate.Text).ToString("yyyy-MM-dd");
                    //    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = name.Text;
                    //    stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0.00";
                    //    stock_cmd.Parameters.Add("@PURCHES", SqlDbType.VarChar).Value = qty.Text;
                    //    stock_cmd.Parameters.Add("@RETN", SqlDbType.VarChar).Value = "0.00";
                    //    stock_cmd.Parameters.Add("@ISSUE", SqlDbType.VarChar).Value = "0.00";
                    //    stock_cmd.Parameters.Add("@REF", SqlDbType.VarChar).Value = "0.00";
                    //    stock_cmd.Parameters.Add("@CLOSING", SqlDbType.VarChar).Value = "0.00";
                    //    stock_cmd.ExecuteNonQuery();
                    //}

                //}
            }
        }
        //try
        //{
      
        //}
        //catch { }
        con.Close();
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
            //else if (grvpurchsItem.Rows.Count <= 0)
            //{

            //    string message = "alert('*Add item to Save.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("STORE_PURACHASE_SELECT", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CREATE";
                cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = txtinvoiceno.Text;
                //SqlCommand cm = new SqlCommand("select * from GRN_TABLE where INVOICENO ='" + txtinvoiceno.Text + "'", con);
                dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string message = "alert('*Cant enter duplicate INVOICENO number.INVOICENO number alredy exist.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
            }
            auto();
            dr.Close();
            STOCK_TABLE();
            STOCK_TRAN();
            //VENDOR_TRAN();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/STOREKEEPER/Purchase.aspx");
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
                    var RECQTY = row.FindControl("txt_qty") as TextBox;
                    var NAME = row.FindControl("lbl_name") as Label;

                    if (chkRow.Checked)
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
                        con.Open();
                        using (SqlCommand com = new SqlCommand("STORE_PURACHASE_SELECT", con))
                        {
                            com.CommandType = CommandType.StoredProcedure;
                            com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CHECK_TEXT";
                            com.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = NAME.Text;
                            //SqlCommand com = new SqlCommand("select ISNULL(SUM(RQTY),0) AS RQTY from PO_TRAN WHERE ITEMNAME='" + NAME.Text + "'", con);
                            dr = com.ExecuteReader();
                            if (dr.Read())
                            {
                                rec = "0";
                                rec = dr["RQTY"].ToString();
                            }
                            dr.Close();
                        }


                        con.Close();

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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //SqlCommand COM1 = new SqlCommand("SELECT VENDOR,VSCODE,PONO,CONVERT(VARCHAR(10),DATEOFISSUE,105) AS DATEOFISSUE FROM PO_TABLE where PONO='" + txtpono.Text + "'", con);
            using (SqlCommand VN_cmd = new SqlCommand("SP_Purchase_View", con))
            {
                VN_cmd.CommandType = CommandType.StoredProcedure;
                VN_cmd.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT";
                VN_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
                dr = VN_cmd.ExecuteReader();
                if (dr.Read())
                {
                    //Label1.Text = dr["FYEAR"].ToString();
                    dropVendor.SelectedValue = dr["VENDOR"].ToString();
                    txtstatecode.Text = dr["VSCODE"].ToString();
                    txtpono.Text = dr["PONO"].ToString();
                    txtpodate.Text = dr["DATEOFISSUE"].ToString();
                    dr.Close();
                    //SqlDataAdapter da = new SqlDataAdapter("Select ITEMNAME AS NAME,HSNCODE,UNIT,QTY, REMQTY,RQTY,PRICE,AMOUNT,CGST,SGST,IGST,GSTMAT,TOTALAMT AS TOTALAMOUNT,Isselected from PO_TRAN where PONO='" + txtpono.Text + "' and Isselected='False' or QTY>RQTY", con);
                    //foreach (GridViewRow row in grvpurchsItem.Rows)
                    //{
                        using (SqlCommand com = new SqlCommand("SP_Purchase_View", con))
                        {
                            com.CommandType = CommandType.StoredProcedure;
                            com.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT_1";
                            com.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
                            SqlDataAdapter da = new SqlDataAdapter(com);
                            DataTable dt = (DataTable)ViewState["ITEM"];
                            dt.Clear();
                            da.Fill(dt);
                            grvpurchsItem.DataSource = dt;
                            //grvquotion.DataKeyNames = new string[] { "ID" };
                            grvpurchsItem.DataBind();
                            ViewState["ITEM"] = dt;
                            this.BindGrid();
                        }
                        normaldiv.Visible = true;
                   // }
                }
                else
                {
                    string message = "alert('* Incorrect PONO.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
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
    protected void txt_price_TextChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grvpurchsItem.Rows)
            {
                var NAME = row.FindControl("lbl_name") as Label;
                var REMQTY = row.FindControl("lbl_remqty") as Label;
                var oqty = row.FindControl("lbl_qty") as Label;
                var qty = row.FindControl("txt_qty") as TextBox;
                var price = row.FindControl("lbl_price") as Label;
                var amount = row.FindControl("lbl_amount") as Label;
                var cgst = row.FindControl("lbl_cgst") as Label;
                var sgst = row.FindControl("lbl_sgst") as Label;
                var igst = row.FindControl("lbl_igst") as Label;
                var gstamt = row.FindControl("lbl_gstamt") as Label;
                var totalamt = row.FindControl("lbl_totalamount") as Label;
                if (Convert.ToDouble(oqty.Text) < Convert.ToDouble(qty.Text))
                {
                    qty.Text = "0.00";
                    string message = "alert('* Receive quantity must not be greater than the order quantity.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                //------------------------------------------------------------------

                CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);
                if (row.RowType == DataControlRowType.DataRow)
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
                    con.Open();
                    using (SqlCommand com = new SqlCommand("STORE_PURACHASE_SELECT", con))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CHECK_TEXT";
                        com.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = NAME.Text;
                        //SqlCommand com = new SqlCommand("select ISNULL(SUM(RQTY),0) AS RQTY from PO_TRAN WHERE ITEMNAME='" + NAME.Text + "'", con);
                        dr = com.ExecuteReader();
                        if (dr.Read())
                        {
                            rec = "0";
                            rec = dr["RQTY"].ToString();
                        }
                        dr.Close();
                    }
                    con.Close();

                    try
                    {
                        amount.Text = Math.Round((Convert.ToDouble(qty.Text)) * Convert.ToDouble(price.Text)).ToString();
                        if (txtstatecode.Text == "21")
                        {
                            gstamt.Text = Math.Round(((Convert.ToDouble(amount.Text) / 100) * (Convert.ToDouble(cgst.Text))) * 2).ToString();
                            sgst.Text = cgst.Text;
                        }
                        else
                        {
                            gstamt.Text = Math.Round((Convert.ToDouble(amount.Text) / 100) * (Convert.ToDouble(igst.Text))).ToString();
                        }

                        // gstamIt.Text = Math.Round((Convert.ToDouble(amtIt.Text) / 100) * (Convert.ToDouble(sgstIt.Text))).ToString();
                        totalamt.Text = Math.Round(Convert.ToDouble(amount.Text) + Convert.ToDouble(gstamt.Text)).ToString();
                        // totamtIt.Text = Math.Round(( ((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) ) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: '{0}'", ex);
                    }
                    if (chkRow.Checked)
                    {
                        REMQTY.Text = (Convert.ToDouble(oqty.Text) - (Convert.ToDouble(qty.Text) + Convert.ToDouble(rec.ToString()))).ToString();
                        amount1 = amount1 + Convert.ToDecimal(totalamt.Text);
                        gstamount = gstamount + Convert.ToDecimal(gstamt.Text);
                    }
                    else
                    {
                        qty.Text = "0.00";
                        REMQTY.Text = (Convert.ToDouble(oqty.Text) - (Convert.ToDouble(qty.Text) + Convert.ToDouble(rec))).ToString();
                    }
                }

                lblgrandtotal.Text = amount1.ToString();
                lblgstamt.Text = gstamount.ToString();
                lbltotalprice.Text = (Convert.ToDouble(lblgrandtotal.Text) - Convert.ToDouble(lblgstamt.Text)).ToString();
            }
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
           // SqlCommand com = new SqlCommand("select VENDOR,VSCODE,GRNNO,CONVERT(varchar, GRNDATE, 105) as GRNDATE ,INVOICENO,CONVERT(varchar, INVOICEDATE, 105) as INVOICEDATE ,TOTALPRICE,GSTAMOUNT,DISCOUNTAMOUNT,GRANDTOTAL,PONO,CONVERT(varchar, PODATE, 105) as PODATE from GRN_TABLE  where GRNNO='" + slno + "'", con);
            using (SqlCommand com = new SqlCommand("SP_Purchase_page", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH_ByINDEX";
                com.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = slno;
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    btncancel.Visible = true;
                    btndelete.Visible = true;
                    lblEditgrd.Text = slno.ToString();
                    dropVendor.Text = dr["VENDOR"].ToString();
                    txtstatecode.Text = dr["VSCODE"].ToString();
                    txtgrnno.Text = dr["GRNNO"].ToString();
                    txtgrndate.Text = dr["GRNDATE"].ToString();
                    txtinvoiceno.Text = dr["INVOICENO"].ToString();
                    txtinvoicedate.Text = dr["INVOICEDATE"].ToString();
                    txtpono.Text = dr["PONO"].ToString();
                    txtpodate.Text = dr["PODATE"].ToString();

                    lbltotalprice.Text = dr["TOTALPRICE"].ToString();
                    lblgstamt.Text = dr["GSTAMOUNT"].ToString();
                    lbltotaldisc.Text = dr["DISCOUNTAMOUNT"].ToString();
                    lblgrandtotal.Text = dr["GRANDTOTAL"].ToString();
                    dr.Close();
                    DataTable dt = new DataTable();
                    //SqlDataAdapter da = new SqlDataAdapter("select HSNCODE,ITEMNAME as NAME,UNIT,QTY,PRICE,AMOUNT,CGST,SGST,IGST,GSTAMT as GSTMAT,TOTALAMT as TOTALAMOUNT,RECQTY,IsSelected,REMQTY from GRN_ITEM_TABLE where GRNNO='" + slno + "'", con);

                    using (SqlCommand com1 = new SqlCommand("SP_Purchase_page", con))
                    {
                        com1.CommandType = CommandType.StoredProcedure;
                        com1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH_BYid";
                        com.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = slno;
                        SqlDataAdapter da = new SqlDataAdapter(com1);
                        da.Fill(dt);
                        grvpuritemTemp.DataSource = dt;
                        // grdrfq.DataKeyNames = new string[] { "ID" };
                        grvpuritemTemp.DataBind();
                        // DataTable dt = ds2.Tables["Table"];
                        ViewState["ITEM"] = dt;
                        grvpuritemTemp.Visible = true;
                    }
                }
            }
            normaldiv.Visible = true;
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand com = new SqlCommand("STORE_PURACHASE_SELECT", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BIND2_PAGE";
                com.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
                SqlDataAdapter adp = new SqlDataAdapter(com);
                //SqlDataAdapter adp = new SqlDataAdapter("select b.NAME1,a.GRNNO,a.GRNDATE,a.INVOICENO,a.INVOICEDATE from GRN_TABLE a,VENDER_MASTER_TABLE b where a.VENDOR=b.ID order by a.GRNNO desc", con);
                DataTable dt1 = new DataTable();
                adp.Fill(dt1);
                grvPurchase.DataSource = dt1;
                grvPurchase.PageIndex = e.NewPageIndex;
                //    grdrfq.DataKeyNames = new string[] { "id" };
                grvPurchase.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
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
            //else if (grvpuritemTemp.Rows.Count <= 0)
            //{

            //    string message = "alert('*Add item to Save.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();

            //SqlCommand cm = new SqlCommand("select * from GRN_TABLE where INVOICENO ='" + txtinvoiceno.Text + "'", con);
            //dr = cm.ExecuteReader();
            //if (dr.Read())
            //{
            //    string message = "alert('*Cant enter duplicate INVOICENO number.INVOICENO number alredy exist.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}

            //dr.Close();
            STOCK_TABLEUpd();
            STOCK_TRANUpd();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        //VENDOR_TRAN();
        //con.Close();
        Response.Redirect("~/STOREKEEPER/Purchase.aspx");
    }
    public void STOCK_TABLEUpd()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand stock_cmd = new SqlCommand("GRN_OPERATION", con))
        {
            stock_cmd.CommandType = CommandType.StoredProcedure;
            stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            stock_cmd.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
            stock_cmd.Parameters.Add("@VSCODE", SqlDbType.VarChar).Value = txtstatecode.Text;
            stock_cmd.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = lblEditgrd.Text;
            stock_cmd.Parameters.Add("@GRNDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtgrndate.Text).ToString("yyyy-MM-dd");
            stock_cmd.Parameters.Add("@INVOICENO", SqlDbType.VarChar).Value = txtinvoiceno.Text;
            stock_cmd.Parameters.Add("@INVOICEDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvoicedate.Text).ToString("yyyy-MM-dd");
            stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
            stock_cmd.Parameters.Add("@DISCOUNTAMOUNT", SqlDbType.VarChar).Value = lbltotaldisc.Text;
            stock_cmd.Parameters.Add("@GSTAMOUNT", SqlDbType.VarChar).Value = lblgstamt.Text;
            stock_cmd.Parameters.Add("@GRANDTOTAL", SqlDbType.VarChar).Value = lblgrandtotal.Text;
            stock_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@RECQTY", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@REMQTY", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
            stock_cmd.Parameters.Add("@PODATE", SqlDbType.Date).Value = Convert.ToDateTime(txtpodate.Text).ToString("yyyy-MM-dd");
            stock_cmd.Parameters.Add("@IsSelected", SqlDbType.Bit).Value = "True";
            stock_cmd.ExecuteNonQuery();
        }
        con.Close();
        //}
        //catch { }
    }
    public void STOCK_TRANUpd()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        foreach (GridViewRow row in grvpuritemTemp.Rows)
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
                var rqty = row.FindControl("txt_qty") as TextBox;
                var amount = row.FindControl("lbl_amount") as Label;
                var cgst = row.FindControl("lbl_cgst") as Label;
                var sgst = row.FindControl("lbl_sgst") as Label;
                var igst = row.FindControl("lbl_igst") as Label;
                var gstamt = row.FindControl("lbl_gstamt") as Label;
                var totalamt = row.FindControl("lbl_totalamount") as Label;
                using (SqlCommand stock_cmd = new SqlCommand("GRN_OPERATION", con))
                {
                    stock_cmd.CommandType = CommandType.StoredProcedure;
                    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDUPDATE";
                    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                    stock_cmd.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
                    stock_cmd.Parameters.Add("@VSCODE", SqlDbType.VarChar).Value = txtstatecode.Text;
                    stock_cmd.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = lblEditgrd.Text;
                    stock_cmd.Parameters.Add("@GRNDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtgrndate.Text).ToString("yyyy-MM-dd");
                    stock_cmd.Parameters.Add("@INVOICENO", SqlDbType.VarChar).Value = txtinvoiceno.Text;
                    stock_cmd.Parameters.Add("@INVOICEDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvoicedate.Text).ToString("yyyy-MM-dd");
                    stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
                    stock_cmd.Parameters.Add("@DISCOUNTAMOUNT", SqlDbType.VarChar).Value = lbltotaldisc.Text;
                    stock_cmd.Parameters.Add("@GSTAMOUNT", SqlDbType.VarChar).Value = lblgstamt.Text;
                    stock_cmd.Parameters.Add("@GRANDTOTAL", SqlDbType.VarChar).Value = lblgrandtotal.Text;
                    stock_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
                    stock_cmd.Parameters.Add("@PODATE", SqlDbType.Date).Value = txtpodate.Text;
                    stock_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = name.Text;
                    stock_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = hsncode.Text;
                    stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unit.Text;
                    stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = price.Text;
                    stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = qty.Text;
                    stock_cmd.Parameters.Add("@RECQTY", SqlDbType.VarChar).Value = rqty.Text;
                    stock_cmd.Parameters.Add("@REMQTY", SqlDbType.VarChar).Value = remqty.Text;
                    stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = amount.Text;
                    stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = cgst.Text;
                    stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = sgst.Text;
                    stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = igst.Text;
                    stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = gstamt.Text;
                    stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = totalamt.Text;
                    stock_cmd.Parameters.Add("@IsSelected", SqlDbType.Bit).Value = chkRow.Checked;
                    stock_cmd.ExecuteNonQuery();
                }
           
            }
        }
        //try
        //{

        //}
        //catch { }
        con.Close();
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand stock_cmd = new SqlCommand("GRN_OPERATION", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
                stock_cmd.Parameters.Add("@VSCODE", SqlDbType.VarChar).Value = txtstatecode.Text;
                stock_cmd.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = lblEditgrd.Text;
                stock_cmd.Parameters.Add("@GRNDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtgrndate.Text).ToString("yyyy-MM-dd");
                stock_cmd.Parameters.Add("@INVOICENO", SqlDbType.VarChar).Value = txtinvoiceno.Text;
                stock_cmd.Parameters.Add("@INVOICEDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvoicedate.Text).ToString("yyyy-MM-dd");
                stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
                stock_cmd.Parameters.Add("@DISCOUNTAMOUNT", SqlDbType.VarChar).Value = lbltotaldisc.Text;
                stock_cmd.Parameters.Add("@GSTAMOUNT", SqlDbType.VarChar).Value = lblgstamt.Text;
                stock_cmd.Parameters.Add("@GRANDTOTAL", SqlDbType.VarChar).Value = lblgrandtotal.Text;
                stock_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@RECQTY", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@REMQTY", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
                stock_cmd.Parameters.Add("@PODATE", SqlDbType.Date).Value = Convert.ToDateTime(txtpodate.Text).ToString("yyyy-MM-dd");
                stock_cmd.Parameters.Add("@IsSelected", SqlDbType.Bit).Value = "True";
                stock_cmd.ExecuteNonQuery();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/STOREKEEPER/Purchase.aspx");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/STOREKEEPER/Purchase.aspx");
    }
    protected void txtinvoicedate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //string dtpo = Convert.ToDateTime(txtpodate.Text).ToString("yyyy-MM-dd");
            DateTime dtpo = Convert.ToDateTime(txtpodate.Text.ToString());
           // string dtInvoic = Convert.ToDateTime(txtinvoicedate.Text).ToString("yyyy-MM-dd");
            DateTime dtInvoic = Convert.ToDateTime(txtinvoicedate.Text.ToString());

            if (dtpo >= dtInvoic)
            {
                string message = "alert('*Invoice Date Is not less than PO Date .')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtinvoicedate.Text = "";
                return;
            }       
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(DropDownList1.SelectedIndex==1)
        {
            normaldiv.Visible = false;
            txtpono.Enabled = false;
            txtpono.Text = "";
            directdiv.Visible = true;
        }
        else if (DropDownList1.SelectedIndex == 2)
        {
            normaldiv.Visible = true;
            txtpono.Enabled = true;
            directdiv.Visible = false;
        }

    }
    protected void txtMaterial_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            SqlCommand com = new SqlCommand("select * from MATERIAL_MASTER_TABLE where NAME='" + txtMaterial.Text + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                txtUnit.Text = dr["UNIT"].ToString();
                hdnprice.Value = dr["PRICE"].ToString();
                hdngst.Value = dr["GST"].ToString();
                hdnhsncd.Value = dr["HSNCODE"].ToString();
                hdnqty.Value = dr["QTY"].ToString();
                //txtUnit.Text = dr["UNIT"].ToString();
                //txtUnit.Text = dr["UNIT"].ToString();
                //txtUnit.Text = dr["UNIT"].ToString();
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
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[11] {  new DataColumn("NAME"), new DataColumn("UNIT"), new DataColumn("QTY"), new DataColumn("HSNCODE"), new DataColumn("PRICE"), new DataColumn("AMOUNT"), new DataColumn("CGST"), new DataColumn("SGST"), new DataColumn("IGST"), new DataColumn("GSTAMT"), new DataColumn("TOTALAMOUNT") });
        ViewState["ITEM"] = dt;
        this.BindGrid1();
    }
    protected void BindGrid1()
    {
        try
        {
            grdMaterial.DataSource = (DataTable)ViewState["ITEM"];
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            DataTable dt = (DataTable)ViewState["ITEM"];
            dt.Rows.Add(txtMaterial.Text.Trim(), txtUnit.Text.Trim(), txtqty.Text.Trim());
            ViewState["ITEM"] = dt;
            this.BindGrid1();

            con.Close();
            txtMaterial.Text = "";
            txtqty.Text = "";
            txtUnit.Text = "";
        }
        catch (Exception ex)
        {
            //Console.WriteLine("An error occurred: '{0}'", ex);
            Trace.Write(ex.Message);
        }
    }
    protected void grdMaterial_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = (DataTable)ViewState["ITEM"];
            GridViewRow row = (GridViewRow)grdMaterial.Rows[e.RowIndex];

            Label nameit = (Label)row.FindControl("lbl_Name");
            Label unitit = (Label)row.FindControl("lbl_Unit");
            Label quantityit = (Label)row.FindControl("lbl_Qty");

            string NAME = nameit.Text.ToString();
            string UNIT = unitit.Text.ToString();
            string QTY = quantityit.Text.ToString();

            dt.Rows[index].Delete();
            ViewState["ITEM"] = dt;
            //  lblgrandtotal.Text = (Convert.ToDecimal(lblgrandtotal.Text) - Convert.ToDecimal(TOTALAMT)).ToString();

            this.BindGrid1();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}