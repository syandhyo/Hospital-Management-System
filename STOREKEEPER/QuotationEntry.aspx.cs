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

public partial class STOREKEEPER_QuotationEntry : System.Web.UI.Page
{
    string num1 = "0";
    SqlConnection con;
    SqlDataAdapter da, da1 ;
    DataSet ds = new DataSet();
    SqlCommand com, cmd;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    int i, no, no1, sl, F;
    public double amt = 0, qty = 0, amt1 = 0, t_amt = 0, qt = 0, c_rs = 0, avg = 0;
    decimal a, b, c, d, e, f, g, h, j, amount, gstamount;
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
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT DISTINCT RFQID FROM RFQ_TABLE WHERE RFQID not in(select RFQNO from QUOTATION_TBL) and RFQID like '%'+@SearchText+'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = con;

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["RFQID"]));
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
        lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        lblid.Text = Session["NAME"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        txtQuotDate.Text = DateTime.Now.ToString("dd-MM-yyyy"); 

        if (!IsPostBack)
        {
            binddata();
            TempGrid();
           // auto();
        }
    }
    public void auto()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            string qry1 = "select max(QUTIONID) as ID from QUOTATION_TBL";

            com = new SqlCommand(qry1, con);
            dr = null;
            dr = com.ExecuteReader();
            string str1 = "1";
            if (dr.Read() && dr["ID"].ToString() != "")
            {
               // num1 = dr["ID"].ToString();
                //------------------
                num1 = dr["ID"].ToString();
                // string str="0";
                string str = num1.Substring(0, num1.Length - 10);//delete last 10 record
                string d = str.Substring(4);//delete first 3 record
                str1 = (Convert.ToInt64(d) + 1).ToString();//add in numbers
            }
            //-----------------------------------------------------
            //num1 = string.Format("INV{0}", (Convert.ToUInt64(num1.Substring(3)) + 1).ToString("D4"));
            lblAuto.Text = "QTN-" + str1 + "-" + lblfyear.Text;

            dr.Close();
            con.Close();
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
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME1 FROM VENDER_MASTER_TABLE", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddvendrNm.DataSource = dt;
        ddvendrNm.DataTextField = "NAME1";
        ddvendrNm.DataValueField = "ID";
        ddvendrNm.DataBind();   
     
       
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
        
       // con.Close();
            SqlCommand comid = new SqlCommand("select max(QUTIONID) as qutid from QUOTATION_TBL ", con);
            dr1 = comid.ExecuteReader();
            if (dr1.Read())
            {
                txtQuotNo.Text = dr1["qutid"].ToString();
            }
            dr1.Close();
        //------------FOR DISPLAY GRID-----------
        //SqlDataAdapter Adp = new SqlDataAdapter("select a.QUTIONID,CONVERT(varchar, a.QUOTIONDATE, 105) QUOTIONDATE,CONVERT(varchar, a.REFFDATE, 105) REFFDATE,b.NAME1 from QUOTATION_TBL a,VENDER_MASTER_TABLE b where a.VENDORNAME=b.ID ORDER BY a.QUTIONID DESC", con);
        
        using (SqlCommand cmd1 = new SqlCommand("SP_QUTO_DISPLAY", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH";
            SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
            DataTable Dt1 = new DataTable();
            Adp.Fill(Dt1);
            grdquton.DataSource = Dt1;
            grdquton.DataBind();
        }
        con.Close();
         }
         catch (Exception ex)
         {
             Console.WriteLine("An error occurred: '{0}'", ex);
         }
    }
   
    protected void btn_search_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if (txtRfqNo.Text == "")
            {
                string message = "alert('* RFQ No Is Blank.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            //SqlCommand COM = new SqlCommand("SELECT a.RFQID,CONVERT(VARCHAR(10),a.DATE,105) as DATE,b.RFQID,b.ITEMNAME,b.QTY,b.UNIT,c.STATECODE as STATECODE,c.NAME1 as NAME1,c.ID as ID,d.HSNCODE,d.PRICE,d.GST,(d.GST*2) as GSTAMT,(d.PRICE+(d.GST*2)) as TOTAMT  FROM RFQ_TABLE a,RFQ_ITEM_TABLE b,VENDER_MASTER_TABLE c,MATERIAL_MASTER_TABLE d WHERE a.VENDORID=c.ID and a.RFQID=b.RFQID  and d.NAME=b.ITEMNAME and   a.RFQID='" + txtRfqNo.Text + "'", con);
            SqlCommand COM = new SqlCommand("SELECT a.RFQID,CONVERT(VARCHAR(10),a.DATE,105) as DATE,b.RFQID,b.ITEMNAME,b.QTY,b.UNIT,c.STATECODE as STATECODE,c.NAME1 as NAME1,c.ID as ID  FROM RFQ_TABLE a, RFQ_ITEM_TABLE b,VENDER_MASTER_TABLE c  WHERE a.VENDORID=c.ID and a.RFQID=b.RFQID  and   a.RFQID='" + txtRfqNo.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(COM);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                grvquonItem.DataSource = dt1;
                //grvquotion.DataKeyNames = new string[] { "ID" };
                grvquonItem.DataBind();
                dr = COM.ExecuteReader();

                if (dr.Read())
                {
                    txtdate.Text = dr["DATE"].ToString();
                    ddvendrNm.SelectedValue = dr["ID"].ToString();
                    txtStatecd.Text = dr["STATECODE"].ToString();
                }
                dr.Close();
                divGrdTot.Visible = true;
            }
            else
            {
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtRfqNo.Text = "";
                txtRfqNo.Focus();
            }
           
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void TempGrid()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[11] { new DataColumn("HSNCODE"), new DataColumn("ITEMNAME"), new DataColumn("UNIT"), new DataColumn("QTY"), new DataColumn("PRICE"), new DataColumn("AMOUNT"), new DataColumn("CGST"), new DataColumn("SGST"), new DataColumn("IGST"), new DataColumn("GSTAMT"), new DataColumn("TOTALAMOUNT") });
        ViewState["ITEM"] = dt;
        this.BindGrid();
    }
    protected void BindGrid()
    {
        try
        {
            grvquonItem.DataSource = (DataTable)ViewState["ITEM"];
            grvquonItem.DataBind();
        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
 
    protected void txt_price_TextChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grvquonItem.Rows)
            {                 
                TextBox hsncodeIt = (row.Cells[2].FindControl("txt_hsncode") as TextBox);
                Label nameIt = (row.Cells[3].FindControl("lbl_name") as Label);
                Label unitIt = (row.Cells[4].FindControl("lbl_unit") as Label);
                Label quntIt = (row.Cells[5].FindControl("lbl_qty") as Label);

                TextBox priceIt = (row.Cells[6].FindControl("txt_price") as TextBox);
                TextBox amtIt = (row.Cells[7].FindControl("txt_Amt") as TextBox);
                TextBox cgstIt = (row.Cells[8].FindControl("txt_cgst") as TextBox);
                TextBox sgstIt = (row.Cells[9].FindControl("txt_sgst") as TextBox);
                TextBox igstIt = (row.Cells[10].FindControl("txt_igst") as TextBox);
                TextBox gstamIt = (row.Cells[11].FindControl("txt_gstamt") as TextBox);
                TextBox totamtIt = (row.Cells[12].FindControl("txt_TotAmt") as TextBox);

                //------------------------------------------------------------------
                try
                {
                    amtIt.Text = Math.Round((Convert.ToDouble(quntIt.Text)) * Convert.ToDouble(priceIt.Text)).ToString();
                    if (txtStatecd.Text == "21")
                    {
                        gstamIt.Text = Math.Round(((Convert.ToDouble(amtIt.Text) / 100) * (Convert.ToDouble(cgstIt.Text))) * 2).ToString();
                        sgstIt.Text = cgstIt.Text;
                    }
                    else
                    {
                        gstamIt.Text = Math.Round((Convert.ToDouble(amtIt.Text) / 100) * (Convert.ToDouble(igstIt.Text))).ToString();
                    }

                    // gstamIt.Text = Math.Round((Convert.ToDouble(amtIt.Text) / 100) * (Convert.ToDouble(sgstIt.Text))).ToString();
                    totamtIt.Text = Math.Round(Convert.ToDouble(amtIt.Text) + Convert.ToDouble(gstamIt.Text)).ToString();
                    // totamtIt.Text = Math.Round(( ((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) ) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("An error occurred: '{0}'", ex);
                }
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[1].FindControl("chkQuot") as CheckBox);

                    if (chkRow.Checked)
                    {
                        var price = row.FindControl("txt_price") as TextBox;
                        var gstamt = row.FindControl("txt_gstamt") as TextBox;
                        var totalamt = row.FindControl("txt_TotAmt") as TextBox;
                        amount = amount + Convert.ToDecimal(totalamt.Text);
                        gstamount = gstamount + Convert.ToDecimal(gstamt.Text);
                    }
                }
            }
            //----------------------------------
            foreach (GridViewRow row in grvquItemTemp.Rows)
            {
                TextBox hsncodeIt = (row.Cells[2].FindControl("txt_hsncode") as TextBox);
                Label nameIt = (row.Cells[3].FindControl("lbl_name") as Label);
                Label unitIt = (row.Cells[4].FindControl("lbl_unit") as Label);
                Label quntIt = (row.Cells[5].FindControl("lbl_qty") as Label);

                TextBox priceIt = (row.Cells[6].FindControl("txt_price") as TextBox);
                TextBox amtIt = (row.Cells[7].FindControl("txt_Amt") as TextBox);
                TextBox cgstIt = (row.Cells[8].FindControl("txt_cgst") as TextBox);
                TextBox sgstIt = (row.Cells[9].FindControl("txt_sgst") as TextBox);
                TextBox igstIt = (row.Cells[10].FindControl("txt_igst") as TextBox);
                TextBox gstamIt = (row.Cells[11].FindControl("txt_gstamt") as TextBox);
                TextBox totamtIt = (row.Cells[12].FindControl("txt_TotAmt") as TextBox);

                //------------------------------------------------------------------
                try
                {
                    amtIt.Text = Math.Round((Convert.ToDouble(quntIt.Text)) * Convert.ToDouble(priceIt.Text)).ToString();
                    if (txtStatecd.Text == "21")
                    {
                        gstamIt.Text = Math.Round(((Convert.ToDouble(amtIt.Text) / 100) * (Convert.ToDouble(cgstIt.Text))) * 2).ToString();
                        sgstIt.Text = cgstIt.Text;
                    }
                    else
                    {
                        gstamIt.Text = Math.Round((Convert.ToDouble(amtIt.Text) / 100) * (Convert.ToDouble(igstIt.Text))).ToString();
                    }

                    // gstamIt.Text = Math.Round((Convert.ToDouble(amtIt.Text) / 100) * (Convert.ToDouble(sgstIt.Text))).ToString();
                    totamtIt.Text = Math.Round(Convert.ToDouble(amtIt.Text) + Convert.ToDouble(gstamIt.Text)).ToString();
                    // totamtIt.Text = Math.Round(( ((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) ) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: '{0}'", ex);
                }
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[1].FindControl("chkQuot") as CheckBox);

                    if (chkRow.Checked)
                    {
                        var price = row.FindControl("txt_price") as TextBox;
                        var gstamt = row.FindControl("txt_gstamt") as TextBox;
                        var totalamt = row.FindControl("txt_TotAmt") as TextBox;
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
    protected void chkQuot_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grvquonItem.Rows)
            {
                TextBox hsncodeIt = (row.Cells[2].FindControl("txt_hsncode") as TextBox);
                Label nameIt = (row.Cells[3].FindControl("lbl_name") as Label);
                Label unitIt = (row.Cells[4].FindControl("lbl_unit") as Label);
                Label quntIt = (row.Cells[5].FindControl("lbl_qty") as Label);

                TextBox priceIt = (row.Cells[6].FindControl("txt_price") as TextBox);
                TextBox amtIt = (row.Cells[7].FindControl("txt_Amt") as TextBox);
                TextBox cgstIt = (row.Cells[8].FindControl("txt_cgst") as TextBox);
                TextBox sgstIt = (row.Cells[9].FindControl("txt_sgst") as TextBox);
                TextBox igstIt = (row.Cells[10].FindControl("txt_igst") as TextBox);
                TextBox gstamIt = (row.Cells[11].FindControl("txt_gstamt") as TextBox);
                TextBox totamtIt = (row.Cells[12].FindControl("txt_TotAmt") as TextBox);

                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[1].FindControl("chkQuot") as CheckBox);

                    if (chkRow.Checked)
                    {
                        var price = row.FindControl("txt_price") as TextBox;
                        var gstamt = row.FindControl("txt_gstamt") as TextBox;
                        var totalamt = row.FindControl("txt_TotAmt") as TextBox;
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
 
    protected void btncreate_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //if (ifexit(lblminno.Text) == true)
            //{
            //    string message = "alert('MIN Is Already Exists !')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            if (txtQuotNo.Text == "" || txtQuotNo.Text == "0")
            {
                string message = "alert('* Quotation No Is Blank.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtQuotDate.Text == "")
            {
                string message = "alert('* Quotation Date Is Blank')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            //else if (Convert.ToDouble(lblgrandtotal.Text) <= 0)
            //{
            //    string message = "alert('* Please Select Item To Save')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            auto();

            QUTIONITEM();
            using (SqlCommand GRN_cmd = new SqlCommand("USP_QUOTATION", con))
            {
                GRN_cmd.CommandType = CommandType.StoredProcedure;
                GRN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                GRN_cmd.Parameters.Add("@QUTIONID", SqlDbType.VarChar).Value = lblAuto.Text;
                GRN_cmd.Parameters.Add("@RFQNO", SqlDbType.VarChar).Value = txtRfqNo.Text;
                GRN_cmd.Parameters.Add("@REFFDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                GRN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = txtQuotNo.Text;
                GRN_cmd.Parameters.Add("@QUOTIONDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtQuotDate.Text).ToString("yyyy-MM-dd");
                GRN_cmd.Parameters.Add("@VENDORNAME", SqlDbType.VarChar).Value = ddvendrNm.SelectedValue;
                GRN_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtStatecd.Text;
                GRN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
                GRN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = lblgstamt.Text;
                GRN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = lblgrandtotal.Text;
                GRN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                GRN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "";
                GRN_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                GRN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
                GRN_cmd.Parameters.Add("@QUANTITY", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "";

                GRN_cmd.ExecuteNonQuery();
            }

            //string message = "alert('*GRN Create Successfully.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
       // BindGrnNo();
        Response.Redirect("~/STOREKEEPER/QuotationEntry.aspx");
    }
    public void QUTIONITEM()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
       // SS = "FALSE";

        foreach (GridViewRow row in grvquonItem.Rows)
        {
            CheckBox chkRow = (row.Cells[1].FindControl("chkQuot") as CheckBox);
            TextBox hsncodeIt = (row.Cells[2].FindControl("txt_hsncode") as TextBox);
            Label nameIt = (row.Cells[3].FindControl("lbl_name") as Label);
            Label unitIt = (row.Cells[4].FindControl("lbl_unit") as Label);
            Label quntIt = (row.Cells[5].FindControl("lbl_qty") as Label);

            TextBox priceIt = (row.Cells[6].FindControl("txt_price") as TextBox);
            TextBox amtIt = (row.Cells[7].FindControl("txt_Amt") as TextBox);
            TextBox cgstIt = (row.Cells[8].FindControl("txt_cgst") as TextBox);
            TextBox sgstIt = (row.Cells[9].FindControl("txt_sgst") as TextBox);
            TextBox igstIt = (row.Cells[10].FindControl("txt_igst") as TextBox);
            TextBox gstamIt = (row.Cells[11].FindControl("txt_gstamt") as TextBox);
            TextBox totamtIt = (row.Cells[12].FindControl("txt_TotAmt") as TextBox);

            if (chkRow.Checked)
            {
               // SS = "TRUE";
            using (SqlCommand GRN_cmd = new SqlCommand("USP_QUOTATION", con))
                {
                    GRN_cmd.CommandType = CommandType.StoredProcedure;
                    GRN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
                    GRN_cmd.Parameters.Add("@QUTIONID", SqlDbType.VarChar).Value = lblAuto.Text;
                    GRN_cmd.Parameters.Add("@RFQNO", SqlDbType.VarChar).Value = txtRfqNo.Text;
                    GRN_cmd.Parameters.Add("@REFFDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                    GRN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value =txtQuotNo.Text;
                    GRN_cmd.Parameters.Add("@QUOTIONDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtQuotDate.Text).ToString("yyyy-MM-dd");
                    GRN_cmd.Parameters.Add("@VENDORNAME", SqlDbType.VarChar).Value = ddvendrNm.SelectedValue;
                    GRN_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtStatecd.Text;
                    GRN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = "0.00";
                    GRN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = "0.00";
                    GRN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = "0.00";
                    GRN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;


                    GRN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = hsncodeIt.Text;
                    GRN_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = nameIt.Text;
                    GRN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitIt.Text;
                    GRN_cmd.Parameters.Add("@QUANTITY", SqlDbType.Decimal).Value = quntIt.Text;
                    GRN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = priceIt.Text;
                    GRN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = amtIt.Text;
                    GRN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = cgstIt.Text;
                    GRN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = sgstIt.Text;
                    GRN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = igstIt.Text;
                    GRN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = gstamIt.Text;
                    GRN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = totamtIt.Text;
                    GRN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = chkRow.Checked;

                    GRN_cmd.ExecuteNonQuery();
                }
            }
            //-----------------------------------------------------
        }

        //string message = "alert('*GRN Create Successfully.')";
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        con.Close();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/STOREKEEPER/QuotationEntry.aspx");
    }
    protected void grdquton_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = grdquton.DataKeys[e.NewSelectedIndex].Values["QUTIONID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
           // SqlCommand com = new SqlCommand("select RFQNO,QUTIONID,CONVERT(varchar, QUOTIONDATE, 105) QUOTIONDATE,VENDORNAME,CONVERT(varchar, REFFDATE, 105) REFFDATE,STATECODE,TOTPRICE,TOTGSTAMT,GRANDTOT from QUOTATION_TBL where  QUTIONID='" + slno + "'", con);

            using (SqlCommand COM = new SqlCommand("SP_QUTO_page_display", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH_ByINDEX";
                COM.Parameters.Add("@QUTIONID", SqlDbType.VarChar).Value = slno;
                dr = COM.ExecuteReader();
                if (dr.Read())
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    btncancel.Visible = true;
                    btndelete.Visible = true;
                    lblEditgrd.Text = slno.ToString();

                    txtRfqNo.Text = dr["RFQNO"].ToString();
                    txtdate.Text = dr["REFFDATE"].ToString();
                    txtQuotNo.Text = dr["QUTIONID"].ToString();
                    txtQuotDate.Text = dr["QUOTIONDATE"].ToString();
                    ddvendrNm.Text = dr["VENDORNAME"].ToString();
                    txtStatecd.Text = dr["STATECODE"].ToString();
                    lbltotalprice.Text = dr["TOTPRICE"].ToString();
                    lblgstamt.Text = dr["TOTGSTAMT"].ToString();
                    lblgrandtotal.Text = dr["GRANDTOT"].ToString();

                    dr.Close();
                    DataTable dt = new DataTable();
                    //SqlDataAdapter da = new SqlDataAdapter("select CHKSEL,HSNCODE,NAME as ITEMNAME,UNIT,QUANTITY as QTY,PRICE, AMOUNT,CGST,SGST,IGST,GSTAMT,TOTAMT from QUOTATION_ITEM_TBL where  QUTIONID='" + slno + "'", con);

                    using (SqlCommand COM1 = new SqlCommand("SP_QUTO_page_display", con))
                    {
                        COM1.CommandType = CommandType.StoredProcedure;
                        COM1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH_BYid";
                        COM1.Parameters.Add("@QUTIONID", SqlDbType.VarChar).Value = slno;
                        SqlDataAdapter da = new SqlDataAdapter(COM1);
                        da.Fill(dt);
                        grvquItemTemp.DataSource = dt;
                        // grdrfq.DataKeyNames = new string[] { "ID" };
                        grvquItemTemp.DataBind();
                        // DataTable dt = ds2.Tables["Table"];
                        ViewState["ITEM"] = dt;
                        divGrdTot.Visible = true;
                        grvquItemTemp.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grdquton_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
           // SqlDataAdapter adp = new SqlDataAdapter("select a.QUTIONID,CONVERT(varchar, a.QUOTIONDATE, 105) QUOTIONDATE,CONVERT(varchar, a.REFFDATE, 105) REFFDATE,b.NAME1 from QUOTATION_TBL a,VENDER_MASTER_TABLE b where a.VENDORNAME=b.ID ORDER BY a.QUTIONID DESC", con);
            using (SqlCommand cmd1 = new SqlCommand("SP_QUTO_page_display", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH_BYid";
                SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                Adp.Fill(dt1);
                grdquton.DataSource = dt1;
                grdquton.PageIndex = e.NewPageIndex;
                //    grdrfq.DataKeyNames = new string[] { "id" };
                grdquton.DataBind();
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
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            if (txtQuotNo.Text == "")
            {
                string message = "alert('* Quotation No Is Blank.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtQuotDate.Text == "")
            {
                string message = "alert('* Quotation Date Is Blank')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            //else if (Convert.ToDouble(lblgrandtotal.Text) <= 0)
            //{
            //    string message = "alert('* Please Select Item To Save')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}

            //  auto();      
            using (SqlCommand GRN_cmd = new SqlCommand("USP_QUOTATION", con))
            {
                GRN_cmd.CommandType = CommandType.StoredProcedure;
                GRN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                GRN_cmd.Parameters.Add("@QUTIONID", SqlDbType.VarChar).Value = lblEditgrd.Text;
                GRN_cmd.Parameters.Add("@RFQNO", SqlDbType.VarChar).Value = txtRfqNo.Text;
                GRN_cmd.Parameters.Add("@REFFDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                GRN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = txtQuotNo.Text;
                GRN_cmd.Parameters.Add("@QUOTIONDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtQuotDate.Text).ToString("yyyy-MM-dd");
                GRN_cmd.Parameters.Add("@VENDORNAME", SqlDbType.VarChar).Value = ddvendrNm.SelectedValue;
                GRN_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtStatecd.Text;
                GRN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
                GRN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = lblgstamt.Text;
                GRN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = lblgrandtotal.Text;
                GRN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                GRN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "";
                GRN_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                GRN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
                GRN_cmd.Parameters.Add("@QUANTITY", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "";

                GRN_cmd.ExecuteNonQuery();
            }
            QUTIONITEMUpdt();
            //string message = "alert('*GRN Create Successfully.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        con.Close();
        Response.Redirect("~/STOREKEEPER/QuotationEntry.aspx");
    }
    public void QUTIONITEMUpdt()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        // SS = "FALSE";

        foreach (GridViewRow row in grvquItemTemp.Rows)
        {
            CheckBox chkRow = (row.Cells[1].FindControl("chkQuot") as CheckBox);
            TextBox hsncodeIt = (row.Cells[2].FindControl("txt_hsncode") as TextBox);
            Label nameIt = (row.Cells[3].FindControl("lbl_name") as Label);
            Label unitIt = (row.Cells[4].FindControl("lbl_unit") as Label);
            Label quntIt = (row.Cells[5].FindControl("lbl_qty") as Label);

            TextBox priceIt = (row.Cells[6].FindControl("txt_price") as TextBox);
            TextBox amtIt = (row.Cells[7].FindControl("txt_Amt") as TextBox);
            TextBox cgstIt = (row.Cells[8].FindControl("txt_cgst") as TextBox);
            TextBox sgstIt = (row.Cells[9].FindControl("txt_sgst") as TextBox);
            TextBox igstIt = (row.Cells[10].FindControl("txt_igst") as TextBox);
            TextBox gstamIt = (row.Cells[11].FindControl("txt_gstamt") as TextBox);
            TextBox totamtIt = (row.Cells[12].FindControl("txt_TotAmt") as TextBox);

            if (chkRow.Checked)
            {
                // SS = "TRUE";
                using (SqlCommand GRN_cmd = new SqlCommand("USP_QUOTATION", con))
                {
                    GRN_cmd.CommandType = CommandType.StoredProcedure;
                    GRN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDUPDATE";
                    GRN_cmd.Parameters.Add("@QUTIONID", SqlDbType.VarChar).Value = lblEditgrd.Text;
                    GRN_cmd.Parameters.Add("@RFQNO", SqlDbType.VarChar).Value = txtRfqNo.Text;
                    GRN_cmd.Parameters.Add("@REFFDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                    GRN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = txtQuotNo.Text;
                    GRN_cmd.Parameters.Add("@QUOTIONDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtQuotDate.Text).ToString("yyyy-MM-dd");
                    GRN_cmd.Parameters.Add("@VENDORNAME", SqlDbType.VarChar).Value = ddvendrNm.SelectedValue;
                    GRN_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtStatecd.Text;
                    GRN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = "0.00";
                    GRN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = "0.00";
                    GRN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = "0.00";
                    GRN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;


                    GRN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = hsncodeIt.Text;
                    GRN_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = nameIt.Text;
                    GRN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitIt.Text;
                    GRN_cmd.Parameters.Add("@QUANTITY", SqlDbType.Decimal).Value = quntIt.Text;
                    GRN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = priceIt.Text;
                    GRN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = amtIt.Text;
                    GRN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = cgstIt.Text;
                    GRN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = sgstIt.Text;
                    GRN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = igstIt.Text;
                    GRN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = gstamIt.Text;
                    GRN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = totamtIt.Text;
                    GRN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = chkRow.Checked;

                    GRN_cmd.ExecuteNonQuery();
                }
            }
            //-----------------------------------------------------
        }

        //string message = "alert('*GRN Create Successfully.')";
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        con.Close();
    }
    
    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand GRN_cmd = new SqlCommand("USP_QUOTATION", con))
            {
                GRN_cmd.CommandType = CommandType.StoredProcedure;
                GRN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                GRN_cmd.Parameters.Add("@QUTIONID", SqlDbType.VarChar).Value = lblEditgrd.Text;
                GRN_cmd.Parameters.Add("@RFQNO", SqlDbType.VarChar).Value = txtRfqNo.Text;
                GRN_cmd.Parameters.Add("@REFFDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                GRN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = txtQuotNo.Text;
                GRN_cmd.Parameters.Add("@QUOTIONDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtQuotDate.Text).ToString("yyyy-MM-dd");
                GRN_cmd.Parameters.Add("@VENDORNAME", SqlDbType.VarChar).Value = ddvendrNm.SelectedValue;
                GRN_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtStatecd.Text;
                GRN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
                GRN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = lblgstamt.Text;
                GRN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = lblgrandtotal.Text;
                GRN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                GRN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "";
                GRN_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                GRN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
                GRN_cmd.Parameters.Add("@QUANTITY", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = "0.00";
                GRN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "";

                GRN_cmd.ExecuteNonQuery();
            }
            //string message = "alert('*GRN Create Successfully.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        // BindGrnNo();
        Response.Redirect("~/STOREKEEPER/QuotationEntry.aspx");
    }
}