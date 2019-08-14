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

public partial class STOREKEEPER_Vendor_Contract : System.Web.UI.Page
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
        con.Close();



       // SqlDataAdapter da = new SqlDataAdapter("select a.CONTRACTID,CONVERT(varchar,a.DATE, 105) DATE,a.QUTIONNO,b.NAME1 from  VENDOR_COTRACT_TBL a,VENDER_MASTER_TABLE b where a.VENDERNAME=b.ID  ORDER BY a.CONTRACTID DESC", con);
        using (SqlCommand GRN_cmd = new SqlCommand("SP_Vendor_contract", con))
        {
            GRN_cmd.CommandType = CommandType.StoredProcedure;
            GRN_cmd.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT";
            SqlDataAdapter da = new SqlDataAdapter(GRN_cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grdVendContrct.DataSource = dt;
            // grdVendContrct.DataKeyNames = new string[] { "ID" };
            grdVendContrct.DataBind();
        }



        //----------------------------
        da1 = new SqlDataAdapter("select NAME1,ID FROM VENDER_MASTER_TABLE ", con);
        DataTable ds1 = new DataTable();
        da1.Fill(ds1);
        ddVendor.DataSource = ds1;
        ddVendor.DataTextField = "NAME1";
        ddVendor.DataValueField = "ID";
        ddVendor.DataBind();
        ddVendor.Items.Insert(0, "Please Select");

        //SqlCommand COM1 = new SqlCommand("SELECT * FROM ORG_TABLE", con);
        //dr = COM1.ExecuteReader();
        //if (dr.Read())
        //{
        //    //Label1.Text = dr["FYEAR"].ToString();
        //    txtorgname.Text = dr["NAME"].ToString();
        //    txtaddress.Text = dr["ADDRESS"].ToString();
        //    txtgstin.Text = dr["GSTNO"].ToString();
        //    txtpin.Text = "752055";
        //    txtphone.Text = dr["PHONE"].ToString();
        //    txtcity.Text = "BHUBANESWAR";
        //    txtdstatecode.Text = dr["STATECODE"].ToString();
        //    txtstate.Text = "ODISHA";

        //}
        //dr.Close();
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
    //protected void BindGrid()
    //{
    //    try
    //    {
    //        grdMaterial.DataSource = (DataTable)ViewState["ITEM"];
    //        grdMaterial.DataBind();
    //    }
    //    catch
    //    {
    //    }
    //}

    //protected void txtPrice_TextChanged(object sender, EventArgs e)
    //{
    //     try
    //    {
    //        double var1 = Convert.ToDouble(txtquantity.Text);
    //        double var2 = Convert.ToDouble(txtPrice.Text);
    //        double res = var1 * var2;
    //        txtTotamt.Text = Convert.ToString(res);
    //    }
    //    catch
    //    {
            
    //    }
    //}
    //protected void txtMaterial_TextChanged(object sender, EventArgs e)
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //    con.Open();

    //    SqlCommand com = new SqlCommand("select ID, UNIT,NAME from MATERIAL_MASTER_TABLE where NAME='" + txtMaterial.Text + "'", con);
    //    dr = com.ExecuteReader();
    //    if (dr.Read())
    //    {
    //        txtUnit.Text = dr["UNIT"].ToString();
    //    //    txtunit.Text = dr["UNIT"].ToString();

    //    }
    //    con.Close();
    //}
    //protected void txtquantity_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        double var1 = Convert.ToDouble(txtquantity.Text);
    //        double var2 = Convert.ToDouble(txtPrice.Text);
    //        double res = var1 * var2;
    //        txtTotamt.Text = Convert.ToString(res);
    //    }
    //    catch
    //    {

    //    }
    //}
    protected void btncreate_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand VN_cmd = new SqlCommand("USP_VENDOR_COTRACT", con))
            {
                VN_cmd.CommandType = CommandType.StoredProcedure;
                VN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                VN_cmd.Parameters.Add("@CONTRACTID", SqlDbType.VarChar).Value = txtContrtno.Text;
                VN_cmd.Parameters.Add("@VENDERNAME", SqlDbType.VarChar).Value = ddVendor.SelectedValue;
                VN_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                VN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                VN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = txtQuotaion.Text;
                VN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
                VN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = lblgstamt.Text;
                VN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = lblgrandtotal.Text;

                VN_cmd.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = "";
                VN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
                VN_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = 0.00;

                VN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "";
                VN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "";

                VN_cmd.ExecuteNonQuery();

            }
            ItemVN();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        //string message = "alert('*Please select any test to save.')";
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        Response.Redirect("~/STOREKEEPER/Vendor_Contract.aspx");
    }
    public void ItemVN()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
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
        

            using (SqlCommand MRitem_cmd = new SqlCommand("USP_VENDOR_COTRACT", con))
            {
                MRitem_cmd.CommandType = CommandType.StoredProcedure;
                MRitem_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
                MRitem_cmd.Parameters.Add("@CONTRACTID", SqlDbType.VarChar).Value = txtContrtno.Text;
                MRitem_cmd.Parameters.Add("@VENDERNAME", SqlDbType.VarChar).Value = ddVendor.SelectedValue;
                MRitem_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                MRitem_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                MRitem_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = "";
                MRitem_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = 0.00;
                MRitem_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = 0.00;
                MRitem_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = 0.00;


                MRitem_cmd.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = nameIt.Text;
                MRitem_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitIt.Text;
                MRitem_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = quantyIt.Text;
                MRitem_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = priceIt.Text;
                MRitem_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = totamtIt.Text;

                MRitem_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = amtIt.Text;
                MRitem_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = cgstIt.Text;
                MRitem_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = sgstIt.Text;
                MRitem_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = igstIt.Text;
                MRitem_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = gstamtIt.Text;
                MRitem_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = chkRow.Checked;
                MRitem_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = hsncodIt.Text;


                MRitem_cmd.ExecuteNonQuery();
            }
        }
        con.Close();
    }
    //protected void grdMaterial_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    int index = Convert.ToInt32(e.RowIndex);
    //    DataTable dt = (DataTable)ViewState["ITEM"];
    //    GridViewRow row = (GridViewRow)grdMaterial.Rows[e.RowIndex];

    //    Label name = (Label)row.FindControl("lbl_Name");
    //    Label unit = (Label)row.FindControl("lbl_Unit");
    //    Label quantity = (Label)row.FindControl("lbl_Qty");
    //    Label price = (Label)row.FindControl("lbl_Price");
    //    Label ToAmt = (Label)row.FindControl("lbl_TAmt");

    //    string name1 = name.Text.ToString();
    //    string unit1 = unit.Text.ToString();
    //    string quantity1 = quantity.Text.ToString();
    //    string price1 = price.Text.ToString();
    //    string ToAmt1 = ToAmt.Text.ToString();

    //    dt.Rows[index].Delete();
    //    ViewState["ITEM"] = dt;
    //    //  lblgrandtotal.Text = (Convert.ToDecimal(lblgrandtotal.Text) - Convert.ToDecimal(TOTALAMT)).ToString();

    //    this.BindGrid();
    //}
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/STOREKEEPER/Vendor_Contract.aspx");
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //if (txtRfqNo.Text == "")
            //{
            //    string message = "alert('* RFQ No Is Blank.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}

            SqlCommand COM = new SqlCommand("SELECT a.QUTIONID,a.QUTIONNO,b.QUTIONID,b.NAME,b.UNIT,b.QUANTITY,b.PRICE,b.AMOUNT,b.CGST,b.SGST,b.IGST,b.GSTAMT,b.TOTAMT,b.CHKSEL,b.HSNCODE,a.TOTPRICE,a.TOTGSTAMT,a.GRANDTOT FROM QUOTATION_TBL a,QUOTATION_ITEM_TBL b WHERE a.QUTIONID=b.QUTIONID and  a.QUTIONNO='" + txtQuotaion.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(COM);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);

            grdVendor.DataSource = dt1;
            //grvquotion.DataKeyNames = new string[] { "ID" };
            grdVendor.DataBind();

            dr = COM.ExecuteReader();
            if (dr.Read())
            {
                lbltotalprice.Text = dr["TOTPRICE"].ToString();
                lblgstamt.Text = dr["TOTGSTAMT"].ToString();
                lblgrandtotal.Text = dr["GRANDTOT"].ToString();

            }
            dr.Close();
            //btncreate.Visible = true;
            //btncancel.Visible = true;
            con.Close();
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //SqlCommand com = new SqlCommand("select CONTRACTID,CONVERT(varchar, DATE, 105) as DATE,QUTIONNO,VENDERNAME,TOTPRICE,TOTGSTAMT,GRANDTOT from  VENDOR_COTRACT_TBL  where CONTRACTID='" + slno + "'", con);

            using (SqlCommand COM = new SqlCommand("SP_VendorContract", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH_ByINDEX";
                COM.Parameters.Add("@CONTRACTID", SqlDbType.VarChar).Value = slno;
                dr = COM.ExecuteReader();
                if (dr.Read())
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    btncancel.Visible = true;
                    btndelete.Visible = true;
                    lblEditgrd.Text = slno.ToString();
                    txtdate.Text = dr["DATE"].ToString();
                    txtInvoice.Text = dr["CONTRACTID"].ToString();
                    ddVendor.Text = dr["VENDERNAME"].ToString();
                    txtQuotaion.Text = dr["QUTIONNO"].ToString();
                    lbltotalprice.Text = dr["TOTPRICE"].ToString();
                    lblgstamt.Text = dr["TOTGSTAMT"].ToString();
                    lblgrandtotal.Text = dr["GRANDTOT"].ToString();
                    dr.Close();
                    DataTable dt = new DataTable();
                    //SqlDataAdapter da = new SqlDataAdapter("select CHKSEL,HSNCODE,ITEM as NAME,UNIT,QTY as QUANTITY,PRICE,AMOUNT,CGST,SGST,IGST,GSTAMT,TOTAMT  from VENDOR_COTRACT_ITEM_TBL where CONTRACTID='" + slno + "'", con);

                    using (SqlCommand COM1 = new SqlCommand("SP_QUTO_page_display", con))
                    {
                        COM1.CommandType = CommandType.StoredProcedure;
                        COM1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH_BYid";
                        COM1.Parameters.Add("@CONTRACTID", SqlDbType.VarChar).Value = slno;
                        SqlDataAdapter da = new SqlDataAdapter(COM1);
                        da.Fill(dt);
                        grdVendor.DataSource = dt;
                        // grdrfq.DataKeyNames = new string[] { "ID" };
                        grdVendor.DataBind();
                        // DataTable dt = ds2.Tables["Table"];
                        ViewState["ITEM"] = dt;
                    }
                }
            }
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //SqlDataAdapter adp = new SqlDataAdapter("select a.CONTRACTID,CONVERT(varchar,a.DATE, 105) DATE,a.QUTIONNO,b.NAME1 from  VENDOR_COTRACT_TBL a,VENDER_MASTER_TABLE b where a.VENDERNAME=b.ID  ORDER BY a.CONTRACTID DESC", con);

            using (SqlCommand COM1 = new SqlCommand("SP_Vendor_contract", con))
              {
               COM1.CommandType = CommandType.StoredProcedure;
               COM1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT";
               SqlDataAdapter adp = new SqlDataAdapter(COM1);
               DataTable dt1 = new DataTable();
               adp.Fill(dt1);
               grdVendContrct.DataSource = dt1;
               grdVendContrct.PageIndex = e.NewPageIndex;
            // grdrfq.DataKeyNames = new string[] { "id" };
               grdVendContrct.DataBind();
            
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            // auto();
            using (SqlCommand VN_cmd = new SqlCommand("USP_VENDOR_COTRACT", con))
            {
                VN_cmd.CommandType = CommandType.StoredProcedure;
                VN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                VN_cmd.Parameters.Add("@CONTRACTID", SqlDbType.VarChar).Value = lblEditgrd.Text;
                VN_cmd.Parameters.Add("@VENDERNAME", SqlDbType.VarChar).Value = ddVendor.SelectedValue;
                VN_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                VN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                VN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = txtQuotaion.Text;
                VN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
                VN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = lblgstamt.Text;
                VN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = lblgrandtotal.Text;

                VN_cmd.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = "";
                VN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
                VN_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = 0.00;

                VN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "";
                VN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "";

                VN_cmd.ExecuteNonQuery();

            }
            ItemVNUpd();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        //string message = "alert('*Please select any test to save.')";
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        Response.Redirect("~/STOREKEEPER/Vendor_Contract.aspx");
    }
    public void ItemVNUpd()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
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


            using (SqlCommand MRitem_cmd = new SqlCommand("USP_VENDOR_COTRACT", con))
            {
                MRitem_cmd.CommandType = CommandType.StoredProcedure;
                MRitem_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDUPDATE";
                MRitem_cmd.Parameters.Add("@CONTRACTID", SqlDbType.VarChar).Value = lblEditgrd.Text;
                MRitem_cmd.Parameters.Add("@VENDERNAME", SqlDbType.VarChar).Value = ddVendor.SelectedValue;
                MRitem_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                MRitem_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                MRitem_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = "";
                MRitem_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = 0.00;
                MRitem_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = 0.00;
                MRitem_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = 0.00;


                MRitem_cmd.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = nameIt.Text;
                MRitem_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitIt.Text;
                MRitem_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = quantyIt.Text;
                MRitem_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = priceIt.Text;
                MRitem_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = totamtIt.Text;

                MRitem_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = amtIt.Text;
                MRitem_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = cgstIt.Text;
                MRitem_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = sgstIt.Text;
                MRitem_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = igstIt.Text;
                MRitem_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = gstamtIt.Text;
                MRitem_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = chkRow.Checked;
                MRitem_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = hsncodIt.Text;

                MRitem_cmd.ExecuteNonQuery();
            }
        }
        con.Close();
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand VN_cmd = new SqlCommand("USP_VENDOR_COTRACT", con))
            {
                VN_cmd.CommandType = CommandType.StoredProcedure;
                VN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                VN_cmd.Parameters.Add("@CONTRACTID", SqlDbType.VarChar).Value = lblEditgrd.Text;
                VN_cmd.Parameters.Add("@VENDERNAME", SqlDbType.VarChar).Value = ddVendor.SelectedValue;
                VN_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                VN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                VN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = txtQuotaion.Text;
                VN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
                VN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = lblgstamt.Text;
                VN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = lblgrandtotal.Text;

                VN_cmd.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = "";
                VN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
                VN_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = 0.00;

                VN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = 0.00;
                VN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "";
                VN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "";

                VN_cmd.ExecuteNonQuery();

            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        //string message = "alert('*Please select any test to save.')";
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        Response.Redirect("~/STOREKEEPER/Vendor_Contract.aspx");
    }
}