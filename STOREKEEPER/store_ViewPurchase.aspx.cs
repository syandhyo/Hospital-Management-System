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

public partial class STOREKEEPER_store_ViewPurchase : System.Web.UI.Page
{
    string num1 = "0";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9, cmd10, cmd11, cmd12, cmd13, cmd14, cmd15, cmd16;
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
            using (SqlCommand cmd = new SqlCommand("STORE_VIEW_PURCHASE", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PO";
                cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = prefix;
                cmd.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = "";
                //cmd.CommandText = "select DISTINCT PONO from PO_TABLE where PONO like @SearchText+'%'";
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
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select slno AS ID from GRN_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        //num1 = string.Format("PU{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtgrnno.Text = "GRN-" + num1 + 1 + "-" + lblfyear.Text;
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
            using (SqlCommand cmd = new SqlCommand("STORE_VIEW_PURCHASE", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_VENDOR";
                cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = "";
                da1 = new SqlDataAdapter(cmd);
                //da1 = new SqlDataAdapter("select NAME1,ID FROM VENDER_MASTER_TABLE ", con);
                DataTable ds1 = new DataTable();
                da1.Fill(ds1);
                dropVendor.DataSource = ds1;
                dropVendor.DataTextField = "NAME1";
                dropVendor.DataValueField = "ID";
                dropVendor.DataBind();
                dropVendor.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,CONVERT(VARCHAR(10),INVDATE,105) AS DATE,VENDOR AS VENDOR FROM GRN_TABLE WHERE FYEAR='" + lblfyear.Text + "' ORDER BY ID DESC", con);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //GridView2.SelectedIndex = 0;
        //GridView2.DataSource = dt;
        //GridView2.DataKeyNames = new string[] { "ID" };
        //GridView2.DataBind();



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
            grvStudentDetails.DataSource = (DataTable)ViewState["ITEM"];
            grvStudentDetails.DataBind();

        }
        catch
        {
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

        if (!IsPostBack)
        {
            FillPo();
            binddata();
        }
        con.Close();
    }
    public void FillPo()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand COM = new SqlCommand("STORE_VIEW_PURCHASE", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_GRN";
                COM.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
                COM.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = Session["GRNRID"].ToString();
                //SqlCommand COM = new SqlCommand("SELECT A.VENDOR,A.VSCODE,A.GRNNO,CONVERT(VARCHAR(10),A.GRNDATE,105) AS GRNDATE,A.INVOICENO,CONVERT(VARCHAR(10),A.INVOICEDATE,105) AS INVOICEDATE,A.TOTALPRICE,A.GSTAMOUNT,A.DISCOUNTAMOUNT,A.GRANDTOTAL,A.PONO,CONVERT(VARCHAR(10),A.PODATE,105) AS PODATE,B.HSNCODE,B.ITEMNAME,B.PRICE,B.QTY,B.RECQTY,B.REMQTY,B.AMOUNT,B.CGST,B.GSTAMT,B.IGST,B.Isselected,B.SGST,B.TOTALAMT,B.UNIT FROM GRN_TABLE a,GRN_ITEM_TABLE b WHERE a.GRNNO=b.GRNNO and a.GRNNO='" + Session["GRNRID"].ToString() + "'", con);
                // da = new SqlDataAdapter(" SELECT a.*,b.*  FROM PO_TABLE a,PO_ITEM_TABLE b  WHERE a.PONO=b.ID and a.PONO=b.ID and a.PONO='"+lblPOId.Text+"'   ORDER BY A.PONO DESC='" + lblPOId.Text + "'", con);
                //DataTable ds = new DataTable();
                //da.Fill(ds);
                SqlDataAdapter da = new SqlDataAdapter(COM);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                grvStudentDetails.DataSource = dt1;
                grvStudentDetails.DataBind();
                dr = COM.ExecuteReader();
                if (dr.Read())
                {
                    txtpono.Text = dr["PONO"].ToString();
                    txtpodate.Text = dr["PODATE"].ToString();
                    dropVendor.SelectedValue = dr["VENDOR"].ToString();
                    txtstatecode.Text = dr["VSCODE"].ToString();
                    txtgrnno.Text = dr["GRNNO"].ToString();
                    txtgrndate.Text = dr["GRNDATE"].ToString();
                    txtinvoiceno.Text = dr["INVOICENO"].ToString();
                    txtinvoicedate.Text = dr["INVOICEDATE"].ToString();
                    lblgstamt.Text = dr["GSTAMOUNT"].ToString();
                    lbltotaldisc.Text = dr["DISCOUNTAMOUNT"].ToString();
                    lblgrandtotal.Text = dr["GRANDTOTAL"].ToString();
                    lbltotalprice.Text = dr["TOTALPRICE"].ToString();
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
    public void STOCK_TABLE()
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
        foreach (GridViewRow row in grvStudentDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);

                var remqty = row.FindControl("lbl_remqty") as Label;
                var name = row.FindControl("lbl_name") as Label;
                var hsncode = row.FindControl("lbl_hsn") as Label;
                var unit = row.FindControl("lbl_unit") as Label;
                var price = row.FindControl("lbl_price") as Label;
                var qty = row.FindControl("lbl_qty") as Label;
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
                    stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = amount.Text;
                    stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = cgst.Text;
                    stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = sgst.Text;
                    stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = igst.Text;
                    stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = gstamt.Text;
                    stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = totalamt.Text;
                    stock_cmd.Parameters.Add("@IsSelected", SqlDbType.Bit).Value = chkRow.Checked;
                    stock_cmd.Parameters.Add("@REMQTY", SqlDbType.VarChar).Value = remqty.Text;
                    stock_cmd.ExecuteNonQuery();

                }
                if (chkRow.Checked)
                {
                    using (SqlCommand stock_cmd = new SqlCommand("GRN_OPERATION", con))
                    {
                        stock_cmd.CommandType = CommandType.StoredProcedure;
                        stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDMAT";
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
                        stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = amount.Text;
                        stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = cgst.Text;
                        stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = sgst.Text;
                        stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = igst.Text;
                        stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = gstamt.Text;
                        stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = totalamt.Text;
                        stock_cmd.Parameters.Add("@IsSelected", SqlDbType.Bit).Value = chkRow.Checked;
                        stock_cmd.Parameters.Add("@REMQTY", SqlDbType.VarChar).Value = remqty.Text;
                        stock_cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand stock_cmd = new SqlCommand("STORE_GRN_Tran", con))
                    {
                        stock_cmd.CommandType = CommandType.StoredProcedure;
                        stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                        stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                        stock_cmd.Parameters.Add("@DATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtgrndate.Text).ToString("yyyy-MM-dd");
                        stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = name.Text;
                        stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0.00";
                        stock_cmd.Parameters.Add("@PURCHES", SqlDbType.VarChar).Value = rqty.Text;
                        stock_cmd.Parameters.Add("@RETN", SqlDbType.VarChar).Value = "0.00";
                        stock_cmd.Parameters.Add("@ISSUE", SqlDbType.VarChar).Value = "0.00";
                        stock_cmd.Parameters.Add("@REF", SqlDbType.VarChar).Value = "0.00";
                        stock_cmd.Parameters.Add("@CLOSING", SqlDbType.VarChar).Value = "0.00";
                        stock_cmd.ExecuteNonQuery();
                    }

                }
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
            if (txtstatecode.Text == "")
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
            else if (grvStudentDetails.Rows.Count <= 0)
            {

                string message = "alert('*Add item to Save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();


            //auto();
            //dr.Close();
            STOCK_TABLE();
            STOCK_TRAN();
            VENDOR_TRAN();
            string message1 = "alert('Created Successfully..')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            return;
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/STOREKEEPER/store_ViewPurchase.aspx");
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grvStudentDetails.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);
                    var ORQTY = row.FindControl("lbl_qty") as Label;
                    var REMQTY = row.FindControl("lbl_remqty") as Label;
                    var RECQTY = row.FindControl("txt_qty") as TextBox;
                    if (chkRow.Checked)
                    {

                        var price = row.FindControl("lbl_amount") as Label;
                        var gstamt = row.FindControl("lbl_gstamt") as Label;
                        var totalamt = row.FindControl("lbl_totalamount") as Label;
                        amount = amount + Convert.ToDecimal(totalamt.Text);
                        gstamount = gstamount + Convert.ToDecimal(gstamt.Text);
                        REMQTY.Text = (Convert.ToDouble(ORQTY.Text) - Convert.ToDouble(RECQTY.Text)).ToString();

                    }
                    else
                    {
                        RECQTY.Text = "0.00";
                        REMQTY.Text = (Convert.ToDouble(ORQTY.Text) - Convert.ToDouble(RECQTY.Text)).ToString();
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
        Session["Viewpo"] = txtpono.Text;
        Response.Redirect("~/STOREKEEPER/store_POView.aspx");
        
    }
    protected void txt_price_TextChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grvStudentDetails.Rows)
            {
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
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);

                    if (chkRow.Checked)
                    {
                        REMQTY.Text = (Convert.ToDouble(oqty.Text) - Convert.ToDouble(qty.Text)).ToString();
                        amount1 = amount1 + Convert.ToDecimal(totalamt.Text);
                        gstamount = gstamount + Convert.ToDecimal(gstamt.Text);

                    }
                    else
                    {
                        qty.Text = "0.00";
                        REMQTY.Text = (Convert.ToDouble(oqty.Text) - Convert.ToDouble(qty.Text)).ToString();
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
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/STOREKEEPER/store_PurchaseReleaser.aspx");
    }
}