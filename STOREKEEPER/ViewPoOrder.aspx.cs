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

public partial class STOREKEEPER_ViewPoOrder : System.Web.UI.Page
{
    string num1 = "0";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9, cmd10;
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
        lblPOId.Text = Session["POID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {
            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[11] { new DataColumn("HSN"), new DataColumn("NAME"), new DataColumn("UNIT"), new DataColumn("PRICE"), new DataColumn("QTY"), new DataColumn("AMOUNT"), new DataColumn("CGST"), new DataColumn("SGST"), new DataColumn("IGST"), new DataColumn("GSTAMT"), new DataColumn("TOTALAMOUNT") });
            //ViewState["ITEM"] = dt;
            //this.BindGrid();
            binddata();
            FillPo();
        }
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
        
        //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,CONVERT(VARCHAR(10),INVDATE,105) AS DATE,VENDOR AS VENDOR FROM PO_TABLE WHERE FYEAR='" + lblfyear.Text + "' ORDER BY ID DESC", con);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //GridView2.SelectedIndex = 0;
        //GridView2.DataSource = dt;
        //GridView2.DataKeyNames = new string[] { "ID" };
        //GridView2.DataBind();

        //---------------------------------------------------------------
            using (SqlCommand cm = new SqlCommand("STORE_VIEW_PO_ORDER", con))
        {
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_VENDOR_MASTER";
            cm.Parameters.Add("@PONO", SqlDbType.VarChar).Value = "NULL";
            cm.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = "NULL";
            cm.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da1 = new SqlDataAdapter(cm);
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
    public void FillPo()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        //--SqlCommand COM = new SqlCommand("SELECT a.*,CONVERT(varchar, a.DATEOFISSUE, 105) as DATEOFISSUE,CONVERT(varchar, a.REFDATE, 105) as REFDATE,b.*  FROM PO_TABLE a,PO_ITEM_TABLE b  WHERE a.PONO=b.ID and a.PONO=b.ID and a.PONO='" + lblPOId.Text + "'  ORDER BY A.PONO DESC", con);
       // da = new SqlDataAdapter(" SELECT a.*,b.*  FROM PO_TABLE a,PO_ITEM_TABLE b  WHERE a.PONO=b.ID and a.PONO=b.ID and a.PONO='"+lblPOId.Text+"'   ORDER BY A.PONO DESC='" + lblPOId.Text + "'", con);
        //DataTable ds = new DataTable();
        //da.Fill(ds);
        using (SqlCommand cm = new SqlCommand("STORE_VIEW_PO_ORDER", con))
        {
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PO_ITEM";
            cm.Parameters.Add("@PONO", SqlDbType.VarChar).Value = lblPOId.Text;
            cm.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = "NULL";
            cm.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);

            grvStudentDetails.DataSource = dt1;

            grvStudentDetails.DataBind();

            dr = cm.ExecuteReader();
            if (dr.Read())
            {
                dropVendor.SelectedValue = dr["VENDOR"].ToString();
                //txtstatecode.Text = dr["VSSTATECODE"].ToString();
                txtdateofissue.Text = dr["DATEOFISSUE"].ToString();
                txtrefno.Text = dr["REFNO"].ToString();
                txtrefdate.Text = dr["REFDATE"].ToString();
                txtorgname.Text = dr["ORGNAME"].ToString();
                txtpin.Text = dr["PIN"].ToString();
                txtcity.Text = dr["CITY"].ToString();
                // txtphone.Text = dr[""].ToString();
                txtstate.Text = dr["STATE"].ToString();
                txtdstatecode.Text = dr["OSTATECODE"].ToString();
                txtaddress.Text = dr["ADDRESS"].ToString();
                txtgstin.Text = dr["GSTIN"].ToString();
                lbltotalprice.Text = dr["TOTALPRICE"].ToString();
                lblgstamt.Text = dr["GSTAMOUNT"].ToString();
                lblgrandtotal.Text = dr["GRANDTOTAL"].ToString();

                //lbltotalprice.Text = dr["TOTPRICE"].ToString();
                //lblgstamt.Text = dr["TOTGSTAMT"].ToString();
                //lblgrandtotal.Text = dr["GRANDTOT"].ToString();

            }
            dr.Close();
        }
        //dropissuedto.DataSource = ds;
        //dropissuedto.DataTextField = "DeptName";
        //dropissuedto.DataValueField = "id";
        //dropissuedto.DataBind();
        con.Close();
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        foreach (GridViewRow row in grvStudentDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);

                if (chkRow.Checked)
                {
                    var item = row.FindControl("lbl_name") as Label;
                    var qty = row.FindControl("txt_qty") as TextBox;
                    var price = row.FindControl("lbl_price") as Label;
                    var amount = row.FindControl("lbl_amount") as Label;
                    var cgst = row.FindControl("lbl_cgst") as Label;
                    var sgst = row.FindControl("lbl_sgst") as Label;
                    var igst = row.FindControl("lbl_igst") as Label;
                    var gstamt = row.FindControl("lbl_gstamt") as Label;
                    var totalamt = row.FindControl("lbl_totalamount") as Label;
                    string CQTY = "0";
                    string QTY = "0";
                    using (SqlCommand cm = new SqlCommand("STORE_VIEW_PO_ORDER", con))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CHECK1";
                        cm.Parameters.Add("@PONO", SqlDbType.VarChar).Value = lblPOId.Text;
                        cm.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
                        cm.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = item.Text;
                        //SqlDataAdapter da = new SqlDataAdapter(cm);
                        //SqlCommand COM = new SqlCommand("SELECT ISNULL(B.QTY,0)as QTY FROM VENDOR_COTRACT_TBL A,VENDOR_COTRACT_ITEM_TBL B WHERE VENDERNAME='" + dropVendor.SelectedValue + "' AND A.CONTRACTID=B.CONTRACTID AND B.ITEM='" + item.Text + "'", con);
                        dr = cm.ExecuteReader();
                        CQTY = "0";
                        if (dr.Read())
                        {
                            CQTY = dr["QTY"].ToString();
                        }
                        dr.Close();
                    }
                    using (SqlCommand COM1 = new SqlCommand("STORE_VIEW_PO_ORDER", con))
                    {
                        COM1.CommandType = CommandType.StoredProcedure;
                        COM1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CHECK2";
                        COM1.Parameters.Add("@PONO", SqlDbType.VarChar).Value = lblPOId.Text;
                        COM1.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
                        COM1.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = item.Text;
                        //SqlCommand COM1 = new SqlCommand("SELECT ISNULL(SUM(B.QTY),0) AS QTY FROM PO_TABLE A,PO_ITEM_TABLE B WHERE A.VENDOR='" + dropVendor.SelectedValue + "' AND A.PONO=B.ID AND B.ITEMNAME='" + item.Text + "'", con);
                        dr = COM1.ExecuteReader();
                        QTY = "0";
                        if (dr.Read())
                        {
                            QTY = dr["QTY"].ToString();
                        }
                        dr.Close();
                    }
                    string mqty = (Convert.ToDouble(CQTY) - Convert.ToDouble(QTY)).ToString();
                    if ((Convert.ToDouble(qty.Text) > Convert.ToDouble(mqty)))
                    {
                        qty.Text = "0.00";
                        string message = "alert('*You can not add more than-" + mqty + " no.of quantity.')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        return;
                    }

                    //if (Convert.ToDouble(oqty.Text) < Convert.ToDouble(qty.Text))
                    //{
                    //    qty.Text = "0.00";
                    //    string message = "alert('* Receive quantity must not be greater than the order quantity.')";
                    //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    //    return;
                    //}

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
                    catch(Exception ex)
                    {
                        Console.WriteLine("An error occurred: '{0}'", ex);
                    }
                    amount1 = amount1 + Convert.ToDecimal(totalamt.Text);
                    gstamount = gstamount + Convert.ToDecimal(gstamt.Text);
                }
            }
            lblgrandtotal.Text = amount1.ToString();
            lblgstamt.Text = gstamount.ToString();
            lbltotalprice.Text = (Convert.ToDouble(lblgrandtotal.Text) - Convert.ToDouble(lblgstamt.Text)).ToString();
        }
        con.Close();
    }
    protected void txt_price_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            foreach (GridViewRow row in grvStudentDetails.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);

                    if (chkRow.Checked)
                    {
                        var item = row.FindControl("lbl_name") as Label;
                        var qty = row.FindControl("txt_qty") as TextBox;
                        var price = row.FindControl("lbl_price") as Label;
                        var amount = row.FindControl("lbl_amount") as Label;
                        var cgst = row.FindControl("lbl_cgst") as Label;
                        var sgst = row.FindControl("lbl_sgst") as Label;
                        var igst = row.FindControl("lbl_igst") as Label;
                        var gstamt = row.FindControl("lbl_gstamt") as Label;
                        var totalamt = row.FindControl("lbl_totalamount") as Label;
                        string CQTY = "0";
                        string QTY = "0";
                        using (SqlCommand COM = new SqlCommand("STORE_VIEW_PO_ORDER", con))
                        {
                            COM.CommandType = CommandType.StoredProcedure;
                            COM.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_TEXT1";
                            COM.Parameters.Add("@PONO", SqlDbType.VarChar).Value = lblPOId.Text;
                            COM.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
                            COM.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = item.Text;
                            //SqlCommand COM = new SqlCommand("SELECT ISNULL(B.QTY,0)as QTY FROM VENDOR_COTRACT_TBL A,VENDOR_COTRACT_ITEM_TBL B WHERE VENDERNAME='" + dropVendor.SelectedValue + "' AND A.CONTRACTID=B.CONTRACTID AND B.ITEM='" + item.Text + "'", con);
                            dr = COM.ExecuteReader();
                            CQTY = "0";
                            if (dr.Read())
                            {
                                CQTY = dr["QTY"].ToString();
                            }
                            dr.Close();
                        }
                        using (SqlCommand COM1 = new SqlCommand("STORE_VIEW_PO_ORDER", con))
                        {
                            COM1.CommandType = CommandType.StoredProcedure;
                            COM1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_TEXT2";
                            COM1.Parameters.Add("@PONO", SqlDbType.VarChar).Value = lblPOId.Text;
                            COM1.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
                            COM1.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = item.Text;
                            //SqlCommand COM1 = new SqlCommand("SELECT ISNULL(SUM(B.QTY),0) AS QTY FROM PO_TABLE A,PO_ITEM_TABLE B WHERE A.VENDOR='" + dropVendor.SelectedValue + "' AND A.PONO=B.ID AND B.ITEMNAME='" + item.Text + "'", con);
                            dr = COM1.ExecuteReader();
                            QTY = "0";
                            if (dr.Read())
                            {
                                QTY = dr["QTY"].ToString();
                            }
                            dr.Close();
                        }
                        string mqty = (Convert.ToDouble(CQTY) - Convert.ToDouble(QTY)).ToString();
                        if ((Convert.ToDouble(qty.Text) > Convert.ToDouble(mqty)))
                        {
                            qty.Text = "0.00";
                            string message = "alert('*You can not add more than-" + mqty + " no.of quantity.')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            return;
                        }

                        //if (Convert.ToDouble(oqty.Text) < Convert.ToDouble(qty.Text))
                        //{
                        //    qty.Text = "0.00";
                        //    string message = "alert('* Receive quantity must not be greater than the order quantity.')";
                        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        //    return;
                        //}

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
                        amount1 = amount1 + Convert.ToDecimal(totalamt.Text);
                        gstamount = gstamount + Convert.ToDecimal(gstamt.Text);
                    }

                }

                lblgrandtotal.Text = amount1.ToString();
                lblgstamt.Text = gstamount.ToString();
                lbltotalprice.Text = (Convert.ToDouble(lblgrandtotal.Text) - Convert.ToDouble(lblgstamt.Text)).ToString();
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
            using (SqlCommand stock_cmd = new SqlCommand("PO_OPERATION", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
                stock_cmd.Parameters.Add("@VSCODE", SqlDbType.VarChar).Value = txtstatecode.Text;
                stock_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = lblPOId.Text;
                stock_cmd.Parameters.Add("@DATEOFISSUE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateofissue.Text).ToString("yyyy-MM-dd");
                stock_cmd.Parameters.Add("@REFNO", SqlDbType.VarChar).Value = txtrefno.Text;
                stock_cmd.Parameters.Add("@REFDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtrefdate.Text).ToString("yyyy-MM-dd");
                stock_cmd.Parameters.Add("@ORGNAME", SqlDbType.VarChar).Value = txtorgname.Text;
                stock_cmd.Parameters.Add("@PIN", SqlDbType.VarChar).Value = txtpin.Text;
                stock_cmd.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtcity.Text;
                stock_cmd.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
                stock_cmd.Parameters.Add("@GSTIN", SqlDbType.VarChar).Value = txtgstin.Text;
                stock_cmd.Parameters.Add("@OSTATECODE", SqlDbType.VarChar).Value = txtdstatecode.Text;
                stock_cmd.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = txtaddress.Text;
                stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
                stock_cmd.Parameters.Add("@GSTAMOUNT", SqlDbType.VarChar).Value = lblgstamt.Text;
                stock_cmd.Parameters.Add("@GRANDTOTAL", SqlDbType.VarChar).Value = lblgrandtotal.Text;
                stock_cmd.Parameters.Add("@TC1", SqlDbType.VarChar).Value = txt_term_condition.Text;
                stock_cmd.Parameters.Add("@TC2", SqlDbType.VarChar).Value = txt_TEST_REPORT0.Text;
                stock_cmd.Parameters.Add("@TC3", SqlDbType.VarChar).Value = txt_DELIVERY_TERM.Text;
                stock_cmd.Parameters.Add("@TC4", SqlDbType.VarChar).Value = txt_INSPECTION0.Text;
                stock_cmd.Parameters.Add("@TC5", SqlDbType.VarChar).Value = txt_DELIVERY_TIME.Text;
                stock_cmd.Parameters.Add("@TC6", SqlDbType.VarChar).Value = txt_WARANTY0.Text;
                stock_cmd.Parameters.Add("@TC7", SqlDbType.VarChar).Value = txt_FREIGHT.Text;
                stock_cmd.Parameters.Add("@TC8", SqlDbType.VarChar).Value = txt_LICENCE_PERMIT0.Text;
                stock_cmd.Parameters.Add("@TC9", SqlDbType.VarChar).Value = txt_make.Text;
                stock_cmd.Parameters.Add("@TC10", SqlDbType.VarChar).Value = txt_PRICE_PAYMENT.Text;
                stock_cmd.Parameters.Add("@TC11", SqlDbType.VarChar).Value = txt_PACKING_FORWARDING.Text;
                stock_cmd.Parameters.Add("@TC12", SqlDbType.VarChar).Value = txt_ACCEPTANCE0.Text;
                stock_cmd.Parameters.Add("@TC13", SqlDbType.VarChar).Value = txt_TRANSIT_INSURANCE.Text;
                stock_cmd.Parameters.Add("@TC14", SqlDbType.VarChar).Value = txt_TERMINATION0.Text;
                stock_cmd.Parameters.Add("@TC15", SqlDbType.VarChar).Value = txt_LOADING_UNLOADING.Text;
                stock_cmd.Parameters.Add("@TC16", SqlDbType.VarChar).Value = txt_INSTALLATION_COMMI1.Text;
                stock_cmd.Parameters.Add("@HSN", SqlDbType.VarChar).Value = "";
                stock_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = txt_term_condition.Text;
                stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = txt_TEST_REPORT0.Text;
                stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = txt_DELIVERY_TERM.Text;
                stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = txt_INSPECTION0.Text;
                stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = txt_DELIVERY_TIME.Text;
                stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = txt_WARANTY0.Text;
                stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = txt_FREIGHT.Text;
                stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = txt_LICENCE_PERMIT0.Text;
                stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = txt_make.Text;
                stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = txt_PRICE_PAYMENT.Text;
                stock_cmd.Parameters.Add("@ischecked", SqlDbType.Bit).Value = "true";
                stock_cmd.Parameters.Add("@REMQTY", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.ExecuteNonQuery();
            }
            UpdItemPO();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        //string message = "alert('*Updated Succesfully !')";
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        Response.Redirect("~/STOREKEEPER/PORELEASER.aspx");
     
    }
  
    public void UpdItemPO()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        foreach (GridViewRow row in grvStudentDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);

                //if (chkRow.Checked)
                //{
                var item = row.FindControl("lbl_name") as Label;
                var hsncode = row.FindControl("lbl_hsn") as Label;
                var unit = row.FindControl("lbl_unit") as Label;
                var qty = row.FindControl("txt_qty") as TextBox;
                var price = row.FindControl("lbl_price") as Label;
                var amount = row.FindControl("lbl_amount") as Label;
                var cgst = row.FindControl("lbl_cgst") as Label;
                var sgst = row.FindControl("lbl_sgst") as Label;
                var igst = row.FindControl("lbl_igst") as Label;
                var gstamt = row.FindControl("lbl_gstamt") as Label;
                var totalamt = row.FindControl("lbl_totalamount") as Label;
                using (SqlCommand stock_cmd = new SqlCommand("PO_OPERATION", con))
                {
                    stock_cmd.CommandType = CommandType.StoredProcedure;
                    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDUPDATE";
                    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                    stock_cmd.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
                    stock_cmd.Parameters.Add("@VSCODE", SqlDbType.VarChar).Value = txtstatecode.Text;
                    stock_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = lblPOId.Text;
                    stock_cmd.Parameters.Add("@DATEOFISSUE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateofissue.Text).ToString("yyyy-MM-dd");
                    stock_cmd.Parameters.Add("@REFNO", SqlDbType.VarChar).Value = txtrefno.Text;
                    stock_cmd.Parameters.Add("@REFDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtrefdate.Text).ToString("yyyy-MM-dd");
                    stock_cmd.Parameters.Add("@ORGNAME", SqlDbType.VarChar).Value = txtorgname.Text;
                    stock_cmd.Parameters.Add("@PIN", SqlDbType.VarChar).Value = txtpin.Text;
                    stock_cmd.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtcity.Text;
                    stock_cmd.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
                    stock_cmd.Parameters.Add("@GSTIN", SqlDbType.VarChar).Value = txtgstin.Text;
                    stock_cmd.Parameters.Add("@OSTATECODE", SqlDbType.VarChar).Value = txtdstatecode.Text;
                    stock_cmd.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = txtaddress.Text;
                    stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
                    stock_cmd.Parameters.Add("@GSTAMOUNT", SqlDbType.VarChar).Value = lblgstamt.Text;
                    stock_cmd.Parameters.Add("@GRANDTOTAL", SqlDbType.VarChar).Value = lblgrandtotal.Text;
                    stock_cmd.Parameters.Add("@TC1", SqlDbType.VarChar).Value = txt_term_condition.Text;
                    stock_cmd.Parameters.Add("@TC2", SqlDbType.VarChar).Value = txt_TEST_REPORT0.Text;
                    stock_cmd.Parameters.Add("@TC3", SqlDbType.VarChar).Value = txt_DELIVERY_TERM.Text;
                    stock_cmd.Parameters.Add("@TC4", SqlDbType.VarChar).Value = txt_INSPECTION0.Text;
                    stock_cmd.Parameters.Add("@TC5", SqlDbType.VarChar).Value = txt_DELIVERY_TIME.Text;
                    stock_cmd.Parameters.Add("@TC6", SqlDbType.VarChar).Value = txt_WARANTY0.Text;
                    stock_cmd.Parameters.Add("@TC7", SqlDbType.VarChar).Value = txt_FREIGHT.Text;
                    stock_cmd.Parameters.Add("@TC8", SqlDbType.VarChar).Value = txt_LICENCE_PERMIT0.Text;
                    stock_cmd.Parameters.Add("@TC9", SqlDbType.VarChar).Value = txt_make.Text;
                    stock_cmd.Parameters.Add("@TC10", SqlDbType.VarChar).Value = txt_PRICE_PAYMENT.Text;
                    stock_cmd.Parameters.Add("@TC11", SqlDbType.VarChar).Value = txt_PACKING_FORWARDING.Text;
                    stock_cmd.Parameters.Add("@TC12", SqlDbType.VarChar).Value = txt_ACCEPTANCE0.Text;
                    stock_cmd.Parameters.Add("@TC13", SqlDbType.VarChar).Value = txt_TRANSIT_INSURANCE.Text;
                    stock_cmd.Parameters.Add("@TC14", SqlDbType.VarChar).Value = txt_TERMINATION0.Text;
                    stock_cmd.Parameters.Add("@TC15", SqlDbType.VarChar).Value = txt_LOADING_UNLOADING.Text;
                    stock_cmd.Parameters.Add("@TC16", SqlDbType.VarChar).Value = txt_INSTALLATION_COMMI1.Text;
                    stock_cmd.Parameters.Add("@HSN", SqlDbType.VarChar).Value = hsncode.Text;
                    stock_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = item.Text;

                    stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unit.Text;
                    stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = price.Text;
                    stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = qty.Text;
                    stock_cmd.Parameters.Add("@REMQTY", SqlDbType.VarChar).Value = qty.Text;
                    stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = amount.Text;
                    stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = cgst.Text;
                    stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = sgst.Text;
                    stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = igst.Text;
                    stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = gstamt.Text;
                    stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = totalamt.Text;
                    stock_cmd.Parameters.Add("@ischecked", SqlDbType.Bit).Value = chkRow.Checked;
                    stock_cmd.ExecuteNonQuery();
                }
            }
        }
        //}
        //catch { }
        con.Close();
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
       // txtrefno.Text = Session["POID"].ToString();
        Session["Viwpo"] = txtrefno.Text;

        Response.Redirect("~/STOREKEEPER/QuotationView.aspx");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/STOREKEEPER/PORELEASER.aspx");
    }
}