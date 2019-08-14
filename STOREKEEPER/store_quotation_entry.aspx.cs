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

public partial class STOREKEEPER_store_quotation_entry : System.Web.UI.Page
{
    string num1 = "0";
    SqlConnection con;
    SqlDataAdapter da, da1;
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
            lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            lblid.Text = Session["NAME"].ToString();
            lblorgid.Text = Session["ORGID"].ToString();
            txtQuotDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

            if (!IsPostBack)
            {
                binddata();
                TempGrid();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
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
                num1 = dr["ID"].ToString();
                string str = num1.Substring(0, num1.Length - 10);
                string d = str.Substring(4);
                str1 = (Convert.ToInt64(d) + 1).ToString();
            }
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
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT ID as ID,NAME1 FROM VENDER_MASTER_TABLE where Branch_ID = " + Session["Branch"] + "", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddvendrNm.DataSource = Ds;
                ddvendrNm.DataTextField = "NAME1";
                ddvendrNm.DataValueField = "ID";
                ddvendrNm.DataBind();
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select max(QUTIONID) as qutid from QUOTATION_TBL where Branch_ID = " + Session["Branch"] + "", false, false);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                txtQuotNo.Text = Ds1.Tables[0].Rows[0]["qutid"].ToString();
            }
            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SEARCH");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("SP_QUTO_DISPLAY", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                grdquton.DataSource = Ds2;
                grdquton.DataBind();
            }
            #region oldcode
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME1 FROM VENDER_MASTER_TABLE", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //ddvendrNm.DataSource = dt;
            //ddvendrNm.DataTextField = "NAME1";
            //ddvendrNm.DataValueField = "ID";
            //ddvendrNm.DataBind();
            //SqlCommand comid = new SqlCommand("select max(QUTIONID) as qutid from QUOTATION_TBL ", con);
            //dr1 = comid.ExecuteReader();
            //if (dr1.Read())
            //{
            //    txtQuotNo.Text = dr1["qutid"].ToString();
            //}
            //dr1.Close();

            //using (SqlCommand cmd1 = new SqlCommand("SP_QUTO_DISPLAY", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH";
            //    SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
            //    DataTable Dt1 = new DataTable();
            //    Adp.Fill(Dt1);
            //    grdquton.DataSource = Dt1;
            //    grdquton.DataBind();
            //}
            //con.Close();
            #endregion
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
            if (txtRfqNo.Text == "")
            {
                string message = "alert('* RFQ No Is Blank.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("SELECT a.RFQID,CONVERT(VARCHAR(10),a.DATE,105) as DATE,b.RFQID,b.ITEMNAME,b.QTY,b.UNIT,b.HSNCODE,b.SGST,b.CGST,c.STATECODE as STATECODE,c.NAME1 as NAME1,c.ID as ID  FROM RFQ_TABLE a, RFQ_ITEM_TABLE b,VENDER_MASTER_TABLE c  WHERE a.VENDORID=c.ID and a.RFQID=b.RFQID  and   a.RFQID='" + txtRfqNo.Text + "' and a.Branch_ID=b.Branch_ID and a.Branch_ID = " + Session["Branch"] + "", false, false);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                grvquonItem.DataSource = Ds1;
                //grvquotion.DataKeyNames = new string[] { "ID" };
                grvquonItem.DataBind();

                txtdate.Text = Ds1.Tables[0].Rows[0]["DATE"].ToString();
                ddvendrNm.SelectedValue = Ds1.Tables[0].Rows[0]["ID"].ToString();
                txtStatecd.Text = Ds1.Tables[0].Rows[0]["STATECODE"].ToString();
                divGrdTot.Visible = true;
            }
            else
            {
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtRfqNo.Text = "";
                txtRfqNo.Focus();
            }
            #region oldcode
            ////SqlCommand COM = new SqlCommand("SELECT a.RFQID,CONVERT(VARCHAR(10),a.DATE,105) as DATE,b.RFQID,b.ITEMNAME,b.QTY,b.UNIT,c.STATECODE as STATECODE,c.NAME1 as NAME1,c.ID as ID,d.HSNCODE,d.PRICE,d.GST,(d.GST*2) as GSTAMT,(d.PRICE+(d.GST*2)) as TOTAMT  FROM RFQ_TABLE a,RFQ_ITEM_TABLE b,VENDER_MASTER_TABLE c,MATERIAL_MASTER_TABLE d WHERE a.VENDORID=c.ID and a.RFQID=b.RFQID  and d.NAME=b.ITEMNAME and   a.RFQID='" + txtRfqNo.Text + "'", con);
            //SqlCommand COM = new SqlCommand("SELECT a.RFQID,CONVERT(VARCHAR(10),a.DATE,105) as DATE,b.RFQID,b.ITEMNAME,b.QTY,b.UNIT,c.STATECODE as STATECODE,c.NAME1 as NAME1,c.ID as ID  FROM RFQ_TABLE a, RFQ_ITEM_TABLE b,VENDER_MASTER_TABLE c  WHERE a.VENDORID=c.ID and a.RFQID=b.RFQID  and   a.RFQID='" + txtRfqNo.Text + "'", con);
            //SqlDataAdapter da = new SqlDataAdapter(COM);
            //DataTable dt1 = new DataTable();
            //da.Fill(dt1);
            //if (dt1.Rows.Count > 0)
            //{
            //    grvquonItem.DataSource = dt1;
            //    //grvquotion.DataKeyNames = new string[] { "ID" };
            //    grvquonItem.DataBind();
            //    dr = COM.ExecuteReader();

            //    if (dr.Read())
            //    {
            //        txtdate.Text = dr["DATE"].ToString();
            //        ddvendrNm.SelectedValue = dr["ID"].ToString();
            //        txtStatecd.Text = dr["STATECODE"].ToString();
            //    }
            //    dr.Close();
            //    divGrdTot.Visible = true;
            //}
            //con.Close();
            #endregion
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
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void clearfield()
    {
        try
        {
            divGrdTot.Visible = false;
            txtdate.Text = "";
            txtQuotDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtRfqNo.Text = "";
            ddvendrNm.SelectedIndex = 0;
            txtStatecd.Text = "";
            grvquonItem.DataSource = null;
            grvquonItem.DataBind();
            grvquItemTemp.DataSource = null;
            grvquItemTemp.DataBind();
            btncreate.Visible = true;
            btnupdate.Visible = false;
            btndelete.Visible = false;
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
        string message1 = string.Empty;
        try
        {
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
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[13];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@QUTIONID", SqlDbType.VarChar, 500, lblAuto.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@RFQNO", SqlDbType.VarChar, 500, txtRfqNo.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@REFFDATE", SqlDbType.Date, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@QUTIONNO", SqlDbType.VarChar, 500, txtQuotNo.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@QUOTIONDATE", SqlDbType.Date, 0, Convert.ToDateTime(txtQuotDate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@VENDORNAME", SqlDbType.VarChar, 500, ddvendrNm.SelectedValue);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@STATECODE", SqlDbType.VarChar, 500, txtStatecd.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@TOTPRICE", SqlDbType.Decimal, 0, lbltotalprice.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@GRANDTOT", SqlDbType.Decimal, 0, lblgrandtotal.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
           
            OBJ_METHOD.ExecuteProceedure("USP_QUOTATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
            if (OBJ_METHOD._RESULT > 0)
            {
                int chkedcounter = 0;
                int correctinput = 0;
                if (OBJ_METHOD._RESULT > 0)
                {
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
                            chkedcounter++;
                            if (priceIt.Text == "" || priceIt.Text == "0")
                            {
                                string message = "alert('* Please Enter The Price')";
                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                                priceIt.Focus();
                                return;
                            }
                            SQL_PARAMS = new SqlParameter[16];

                            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDINSERT");
                            SQL_PARAMS[1] = OBJ_METHOD.createParams("@QUTIONID", SqlDbType.VarChar, 500, lblAuto.Text);
                            SQL_PARAMS[2] = OBJ_METHOD.createParams("@HSNCODE", SqlDbType.VarChar, 500, hsncodeIt.Text);
                            SQL_PARAMS[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, nameIt.Text);
                            SQL_PARAMS[4] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, unitIt.Text);
                            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                            SQL_PARAMS[7] = OBJ_METHOD.createParams("@QUANTITY", SqlDbType.Decimal, 0, quntIt.Text);
                            SQL_PARAMS[8] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, priceIt.Text);
                            SQL_PARAMS[9] = OBJ_METHOD.createParams("@AMOUNT", SqlDbType.Decimal, 0, amtIt.Text);
                            SQL_PARAMS[10] = OBJ_METHOD.createParams("@CGST", SqlDbType.Decimal, 0, cgstIt.Text);

                            SQL_PARAMS[11] = OBJ_METHOD.createParams("@SGST", SqlDbType.Decimal, 0, sgstIt.Text);
                            SQL_PARAMS[12] = OBJ_METHOD.createParams("@IGST", SqlDbType.Decimal, 0, igstIt.Text);
                            SQL_PARAMS[13] = OBJ_METHOD.createParams("@GSTAMT", SqlDbType.Decimal, 0, gstamIt.Text);
                            SQL_PARAMS[14] = OBJ_METHOD.createParams("@TOTAMT", SqlDbType.Decimal, 0, totamtIt.Text);
                            SQL_PARAMS[15] = OBJ_METHOD.createParams("@CHKSEL", SqlDbType.VarChar, 500, chkRow.Checked);

                            OBJ_METHOD.ExecuteProceedure("USP_QUOTATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
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
                    else if (chkedcounter==0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        message1 = "alert('No row is selected.')";
                    }
                    else
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        message1 = "alert('Error occurred while processing data... Rolling back...')";
                    }
                }
            }
            #region insert cose
            //foreach (GridViewRow row in grvquonItem.Rows)
            //{
            //    CheckBox chkRow = (row.Cells[1].FindControl("chkQuot") as CheckBox);
            //    TextBox hsncodeIt = (row.Cells[2].FindControl("txt_hsncode") as TextBox);
            //    Label nameIt = (row.Cells[3].FindControl("lbl_name") as Label);
            //    Label unitIt = (row.Cells[4].FindControl("lbl_unit") as Label);
            //    Label quntIt = (row.Cells[5].FindControl("lbl_qty") as Label);

            //    TextBox priceIt = (row.Cells[6].FindControl("txt_price") as TextBox);
            //    TextBox amtIt = (row.Cells[7].FindControl("txt_Amt") as TextBox);
            //    TextBox cgstIt = (row.Cells[8].FindControl("txt_cgst") as TextBox);
            //    TextBox sgstIt = (row.Cells[9].FindControl("txt_sgst") as TextBox);
            //    TextBox igstIt = (row.Cells[10].FindControl("txt_igst") as TextBox);
            //    TextBox gstamIt = (row.Cells[11].FindControl("txt_gstamt") as TextBox);
            //    TextBox totamtIt = (row.Cells[12].FindControl("txt_TotAmt") as TextBox);

            //    if (chkRow.Checked)
            //    {
            //        // SS = "TRUE";
            //        using (SqlCommand GRN_cmd = new SqlCommand("USP_QUOTATION", con))
            //        {
            //            GRN_cmd.CommandType = CommandType.StoredProcedure;
            //            GRN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
            //            GRN_cmd.Parameters.Add("@QUTIONID", SqlDbType.VarChar).Value = lblAuto.Text;
            //            GRN_cmd.Parameters.Add("@RFQNO", SqlDbType.VarChar).Value = txtRfqNo.Text;
            //            GRN_cmd.Parameters.Add("@REFFDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //            GRN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = txtQuotNo.Text;
            //            GRN_cmd.Parameters.Add("@QUOTIONDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtQuotDate.Text).ToString("yyyy-MM-dd");
            //            GRN_cmd.Parameters.Add("@VENDORNAME", SqlDbType.VarChar).Value = ddvendrNm.SelectedValue;
            //            GRN_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtStatecd.Text;
            //            GRN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = "0.00";
            //            GRN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = "0.00";
            //            GRN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = "0.00";
            //            GRN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;


            //            GRN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = hsncodeIt.Text;
            //            GRN_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = nameIt.Text;
            //            GRN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitIt.Text;
            //            GRN_cmd.Parameters.Add("@QUANTITY", SqlDbType.Decimal).Value = quntIt.Text;
            //            GRN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = priceIt.Text;
            //            GRN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = amtIt.Text;
            //            GRN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = cgstIt.Text;
            //            GRN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = sgstIt.Text;
            //            GRN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = igstIt.Text;
            //            GRN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = gstamIt.Text;
            //            GRN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = totamtIt.Text;
            //            GRN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = chkRow.Checked;

            //            GRN_cmd.ExecuteNonQuery();
            //        }
            //    }
            //}
            //using (SqlCommand GRN_cmd = new SqlCommand("USP_QUOTATION", con))
            //{
            //    GRN_cmd.CommandType = CommandType.StoredProcedure;
            //    GRN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    GRN_cmd.Parameters.Add("@QUTIONID", SqlDbType.VarChar).Value = lblAuto.Text;
            //    GRN_cmd.Parameters.Add("@RFQNO", SqlDbType.VarChar).Value = txtRfqNo.Text;
            //    GRN_cmd.Parameters.Add("@REFFDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //    GRN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = txtQuotNo.Text;
            //    GRN_cmd.Parameters.Add("@QUOTIONDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtQuotDate.Text).ToString("yyyy-MM-dd");
            //    GRN_cmd.Parameters.Add("@VENDORNAME", SqlDbType.VarChar).Value = ddvendrNm.SelectedValue;
            //    GRN_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtStatecd.Text;
            //    GRN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
            //    GRN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = lblgstamt.Text;
            //    GRN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = lblgrandtotal.Text;
            //    GRN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

            //    GRN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "";
            //    GRN_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            //    GRN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
            //    GRN_cmd.Parameters.Add("@QUANTITY", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "";

            //    GRN_cmd.ExecuteNonQuery();
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
    //public void QUTIONITEM()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //    con.Open();
    //    // SS = "FALSE";

    //    foreach (GridViewRow row in grvquonItem.Rows)
    //    {
    //        CheckBox chkRow = (row.Cells[1].FindControl("chkQuot") as CheckBox);
    //        TextBox hsncodeIt = (row.Cells[2].FindControl("txt_hsncode") as TextBox);
    //        Label nameIt = (row.Cells[3].FindControl("lbl_name") as Label);
    //        Label unitIt = (row.Cells[4].FindControl("lbl_unit") as Label);
    //        Label quntIt = (row.Cells[5].FindControl("lbl_qty") as Label);

    //        TextBox priceIt = (row.Cells[6].FindControl("txt_price") as TextBox);
    //        TextBox amtIt = (row.Cells[7].FindControl("txt_Amt") as TextBox);
    //        TextBox cgstIt = (row.Cells[8].FindControl("txt_cgst") as TextBox);
    //        TextBox sgstIt = (row.Cells[9].FindControl("txt_sgst") as TextBox);
    //        TextBox igstIt = (row.Cells[10].FindControl("txt_igst") as TextBox);
    //        TextBox gstamIt = (row.Cells[11].FindControl("txt_gstamt") as TextBox);
    //        TextBox totamtIt = (row.Cells[12].FindControl("txt_TotAmt") as TextBox);

    //        if (chkRow.Checked)
    //        {
    //            // SS = "TRUE";
    //            using (SqlCommand GRN_cmd = new SqlCommand("USP_QUOTATION", con))
    //            {
    //                GRN_cmd.CommandType = CommandType.StoredProcedure;
    //                GRN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
    //                GRN_cmd.Parameters.Add("@QUTIONID", SqlDbType.VarChar).Value = lblAuto.Text;
    //                GRN_cmd.Parameters.Add("@RFQNO", SqlDbType.VarChar).Value = txtRfqNo.Text;
    //                GRN_cmd.Parameters.Add("@REFFDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
    //                GRN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = txtQuotNo.Text;
    //                GRN_cmd.Parameters.Add("@QUOTIONDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtQuotDate.Text).ToString("yyyy-MM-dd");
    //                GRN_cmd.Parameters.Add("@VENDORNAME", SqlDbType.VarChar).Value = ddvendrNm.SelectedValue;
    //                GRN_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtStatecd.Text;
    //                GRN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = "0.00";
    //                GRN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = "0.00";
    //                GRN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = "0.00";
    //                GRN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;


    //                GRN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = hsncodeIt.Text;
    //                GRN_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = nameIt.Text;
    //                GRN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitIt.Text;
    //                GRN_cmd.Parameters.Add("@QUANTITY", SqlDbType.Decimal).Value = quntIt.Text;
    //                GRN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = priceIt.Text;
    //                GRN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = amtIt.Text;
    //                GRN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = cgstIt.Text;
    //                GRN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = sgstIt.Text;
    //                GRN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = igstIt.Text;
    //                GRN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = gstamIt.Text;
    //                GRN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = totamtIt.Text;
    //                GRN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = chkRow.Checked;

    //                GRN_cmd.ExecuteNonQuery();
    //            }
    //        }
    //    }
    //}
    protected void btncancel_Click(object sender, EventArgs e)
    {
        binddata();
        clearfield();
    }
    protected void grdquton_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = grdquton.DataKeys[e.NewSelectedIndex].Values["QUTIONID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SEARCH_ByINDEX");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@QUTIONID", SqlDbType.VarChar, 500, slno);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("SP_QUTO_page_display", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                btncancel.Visible = true;
                btndelete.Visible = true;
                lblEditgrd.Text = slno.ToString();

                txtRfqNo.Text = Ds2.Tables[0].Rows[0]["RFQNO"].ToString();
                txtdate.Text = Ds2.Tables[0].Rows[0]["REFFDATE"].ToString();
                txtQuotNo.Text = Ds2.Tables[0].Rows[0]["QUTIONID"].ToString();
                txtQuotDate.Text = Ds2.Tables[0].Rows[0]["QUOTIONDATE"].ToString();
                ddvendrNm.Text = Ds2.Tables[0].Rows[0]["VENDORNAME"].ToString();
                txtStatecd.Text = Ds2.Tables[0].Rows[0]["STATECODE"].ToString();
                lbltotalprice.Text = Ds2.Tables[0].Rows[0]["TOTPRICE"].ToString();
                lblgstamt.Text = Ds2.Tables[0].Rows[0]["TOTGSTAMT"].ToString();
                lblgrandtotal.Text = Ds2.Tables[0].Rows[0]["GRANDTOT"].ToString();
                
                SQL_PARAMS = new SqlParameter[3];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SEARCH_BYid");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@QUTIONID", SqlDbType.VarChar, 500, slno);

                DataSet Ds = OBJ_METHOD.Get_DataSet("SP_QUTO_page_display", false, true, SQL_PARAMS);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    grvquItemTemp.DataSource = Ds;
                    grvquItemTemp.DataBind();
                    ViewState["ITEM"] = Ds;
                    divGrdTot.Visible = true;
                    grvquItemTemp.Visible = true;
                }
            }
            #region oldecode
            //using (SqlCommand COM = new SqlCommand("SP_QUTO_page_display", con))
            //{
            //    COM.CommandType = CommandType.StoredProcedure;
            //    COM.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH_ByINDEX";
            //    COM.Parameters.Add("@QUTIONID", SqlDbType.VarChar).Value = slno;
            //    dr = COM.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        btncreate.Visible = false;
            //        btnupdate.Visible = true;
            //        btncancel.Visible = true;
            //        btndelete.Visible = true;
            //        lblEditgrd.Text = slno.ToString();

            //        txtRfqNo.Text = dr["RFQNO"].ToString();
            //        txtdate.Text = dr["REFFDATE"].ToString();
            //        txtQuotNo.Text = dr["QUTIONID"].ToString();
            //        txtQuotDate.Text = dr["QUOTIONDATE"].ToString();
            //        ddvendrNm.Text = dr["VENDORNAME"].ToString();
            //        txtStatecd.Text = dr["STATECODE"].ToString();
            //        lbltotalprice.Text = dr["TOTPRICE"].ToString();
            //        lblgstamt.Text = dr["TOTGSTAMT"].ToString();
            //        lblgrandtotal.Text = dr["GRANDTOT"].ToString();

            //        dr.Close();
            //        DataTable dt = new DataTable();
            //        //SqlDataAdapter da = new SqlDataAdapter("select CHKSEL,HSNCODE,NAME as ITEMNAME,UNIT,QUANTITY as QTY,PRICE, AMOUNT,CGST,SGST,IGST,GSTAMT,TOTAMT from QUOTATION_ITEM_TBL where  QUTIONID='" + slno + "'", con);

            //        using (SqlCommand COM1 = new SqlCommand("SP_QUTO_page_display", con))
            //        {
            //            COM1.CommandType = CommandType.StoredProcedure;
            //            COM1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH_BYid";
            //            COM1.Parameters.Add("@QUTIONID", SqlDbType.VarChar).Value = slno;
            //            SqlDataAdapter da = new SqlDataAdapter(COM1);
            //            da.Fill(dt);
            //            grvquItemTemp.DataSource = dt;
            //            // grdrfq.DataKeyNames = new string[] { "ID" };
            //            grvquItemTemp.DataBind();
            //            // DataTable dt = ds2.Tables["Table"];
            //            ViewState["ITEM"] = dt;
            //            divGrdTot.Visible = true;
            //            grvquItemTemp.Visible = true;
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
    protected void grdquton_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOperation", SqlDbType.VarChar, 500, "SEARCH");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("SP_QUTO_DISPLAY", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                grdquton.DataSource = Ds2;
                grdquton.PageIndex = e.NewPageIndex;
                grdquton.DataBind();
            }
            #region oldcode
            //using (SqlCommand cmd1 = new SqlCommand("SP_QUTO_page_display", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH_BYid";
            //    SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
            //    DataTable dt1 = new DataTable();
            //    Adp.Fill(dt1);
            //    grdquton.DataSource = dt1;
            //    grdquton.PageIndex = e.NewPageIndex;
            //    //    grdrfq.DataKeyNames = new string[] { "id" };
            //    grdquton.DataBind();
            //}
            //con.Close();
            #endregion
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
            SqlParameter[] SQL_PARAMS = new SqlParameter[13];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@QUTIONID", SqlDbType.VarChar, 500, lblEditgrd.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@RFQNO", SqlDbType.VarChar, 500, txtRfqNo.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@REFFDATE", SqlDbType.Date, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@QUTIONNO", SqlDbType.VarChar, 500, txtQuotNo.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@QUOTIONDATE", SqlDbType.Date, 0, Convert.ToDateTime(txtQuotDate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@VENDORNAME", SqlDbType.VarChar, 500, ddvendrNm.SelectedValue);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@STATECODE", SqlDbType.VarChar, 500, txtStatecd.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@TOTPRICE", SqlDbType.Decimal, 0, lbltotalprice.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@GRANDTOT", SqlDbType.Decimal, 0, lblgrandtotal.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);

            OBJ_METHOD.ExecuteProceedure("USP_QUOTATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
            if (OBJ_METHOD._RESULT > 0)
            {
                int chkedcounter = 0;
                int correctinput = 0;
                if (OBJ_METHOD._RESULT > 0)
                {
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
                            chkedcounter++;
                            SQL_PARAMS = new SqlParameter[16];

                            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRIDUPDATE");
                            SQL_PARAMS[1] = OBJ_METHOD.createParams("@QUTIONID", SqlDbType.VarChar, 500, lblEditgrd.Text);
                            SQL_PARAMS[2] = OBJ_METHOD.createParams("@HSNCODE", SqlDbType.VarChar, 500, hsncodeIt.Text);
                            SQL_PARAMS[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, nameIt.Text);
                            SQL_PARAMS[4] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, unitIt.Text);
                            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                            SQL_PARAMS[7] = OBJ_METHOD.createParams("@QUANTITY", SqlDbType.Decimal, 0, quntIt.Text);
                            SQL_PARAMS[8] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, priceIt.Text);
                            SQL_PARAMS[9] = OBJ_METHOD.createParams("@AMOUNT", SqlDbType.Decimal, 0, amtIt.Text);
                            SQL_PARAMS[10] = OBJ_METHOD.createParams("@CGST", SqlDbType.Decimal, 0, cgstIt.Text);

                            SQL_PARAMS[11] = OBJ_METHOD.createParams("@SGST", SqlDbType.Decimal, 0, sgstIt.Text);
                            SQL_PARAMS[12] = OBJ_METHOD.createParams("@IGST", SqlDbType.Decimal, 0, igstIt.Text);
                            SQL_PARAMS[13] = OBJ_METHOD.createParams("@GSTAMT", SqlDbType.Decimal, 0, gstamIt.Text);
                            SQL_PARAMS[14] = OBJ_METHOD.createParams("@TOTAMT", SqlDbType.Decimal, 0, totamtIt.Text);
                            SQL_PARAMS[15] = OBJ_METHOD.createParams("@CHKSEL", SqlDbType.VarChar, 500, chkRow.Checked);
                               
                            OBJ_METHOD.ExecuteProceedure("USP_QUOTATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
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
            #region update code
            //using (SqlCommand GRN_cmd = new SqlCommand("USP_QUOTATION", con))
            //{
            //    GRN_cmd.CommandType = CommandType.StoredProcedure;
            //    GRN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    GRN_cmd.Parameters.Add("@QUTIONID", SqlDbType.VarChar).Value = lblEditgrd.Text;
            //    GRN_cmd.Parameters.Add("@RFQNO", SqlDbType.VarChar).Value = txtRfqNo.Text;
            //    GRN_cmd.Parameters.Add("@REFFDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //    GRN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = txtQuotNo.Text;
            //    GRN_cmd.Parameters.Add("@QUOTIONDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtQuotDate.Text).ToString("yyyy-MM-dd");
            //    GRN_cmd.Parameters.Add("@VENDORNAME", SqlDbType.VarChar).Value = ddvendrNm.SelectedValue;
            //    GRN_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtStatecd.Text;
            //    GRN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
            //    GRN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = lblgstamt.Text;
            //    GRN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = lblgrandtotal.Text;
            //    GRN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

            //    GRN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "";
            //    GRN_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            //    GRN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
            //    GRN_cmd.Parameters.Add("@QUANTITY", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "";

            //    GRN_cmd.ExecuteNonQuery();
            //}
            //foreach (GridViewRow row in grvquItemTemp.Rows)
            //{
            //    CheckBox chkRow = (row.Cells[1].FindControl("chkQuot") as CheckBox);
            //    TextBox hsncodeIt = (row.Cells[2].FindControl("txt_hsncode") as TextBox);
            //    Label nameIt = (row.Cells[3].FindControl("lbl_name") as Label);
            //    Label unitIt = (row.Cells[4].FindControl("lbl_unit") as Label);
            //    Label quntIt = (row.Cells[5].FindControl("lbl_qty") as Label);

            //    TextBox priceIt = (row.Cells[6].FindControl("txt_price") as TextBox);
            //    TextBox amtIt = (row.Cells[7].FindControl("txt_Amt") as TextBox);
            //    TextBox cgstIt = (row.Cells[8].FindControl("txt_cgst") as TextBox);
            //    TextBox sgstIt = (row.Cells[9].FindControl("txt_sgst") as TextBox);
            //    TextBox igstIt = (row.Cells[10].FindControl("txt_igst") as TextBox);
            //    TextBox gstamIt = (row.Cells[11].FindControl("txt_gstamt") as TextBox);
            //    TextBox totamtIt = (row.Cells[12].FindControl("txt_TotAmt") as TextBox);

            //    if (chkRow.Checked)
            //    {
            //        // SS = "TRUE";
            //        using (SqlCommand GRN_cmd = new SqlCommand("USP_QUOTATION", con))
            //        {
            //            GRN_cmd.CommandType = CommandType.StoredProcedure;
            //            GRN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDUPDATE";
            //            GRN_cmd.Parameters.Add("@QUTIONID", SqlDbType.VarChar).Value = lblEditgrd.Text;
            //            GRN_cmd.Parameters.Add("@RFQNO", SqlDbType.VarChar).Value = txtRfqNo.Text;
            //            GRN_cmd.Parameters.Add("@REFFDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //            GRN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = txtQuotNo.Text;
            //            GRN_cmd.Parameters.Add("@QUOTIONDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtQuotDate.Text).ToString("yyyy-MM-dd");
            //            GRN_cmd.Parameters.Add("@VENDORNAME", SqlDbType.VarChar).Value = ddvendrNm.SelectedValue;
            //            GRN_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtStatecd.Text;
            //            GRN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = "0.00";
            //            GRN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = "0.00";
            //            GRN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = "0.00";
            //            GRN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;


            //            GRN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = hsncodeIt.Text;
            //            GRN_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = nameIt.Text;
            //            GRN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitIt.Text;
            //            GRN_cmd.Parameters.Add("@QUANTITY", SqlDbType.Decimal).Value = quntIt.Text;
            //            GRN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = priceIt.Text;
            //            GRN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = amtIt.Text;
            //            GRN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = cgstIt.Text;
            //            GRN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = sgstIt.Text;
            //            GRN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = igstIt.Text;
            //            GRN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = gstamIt.Text;
            //            GRN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = totamtIt.Text;
            //            GRN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = chkRow.Checked;

            //            GRN_cmd.ExecuteNonQuery();
            //        }
            //    }
            //    //-----------------------------------------------------
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
    #region oldcode
    //public void QUTIONITEMUpdt()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //    con.Open();
    //    // SS = "FALSE";

    //    foreach (GridViewRow row in grvquItemTemp.Rows)
    //    {
    //        CheckBox chkRow = (row.Cells[1].FindControl("chkQuot") as CheckBox);
    //        TextBox hsncodeIt = (row.Cells[2].FindControl("txt_hsncode") as TextBox);
    //        Label nameIt = (row.Cells[3].FindControl("lbl_name") as Label);
    //        Label unitIt = (row.Cells[4].FindControl("lbl_unit") as Label);
    //        Label quntIt = (row.Cells[5].FindControl("lbl_qty") as Label);

    //        TextBox priceIt = (row.Cells[6].FindControl("txt_price") as TextBox);
    //        TextBox amtIt = (row.Cells[7].FindControl("txt_Amt") as TextBox);
    //        TextBox cgstIt = (row.Cells[8].FindControl("txt_cgst") as TextBox);
    //        TextBox sgstIt = (row.Cells[9].FindControl("txt_sgst") as TextBox);
    //        TextBox igstIt = (row.Cells[10].FindControl("txt_igst") as TextBox);
    //        TextBox gstamIt = (row.Cells[11].FindControl("txt_gstamt") as TextBox);
    //        TextBox totamtIt = (row.Cells[12].FindControl("txt_TotAmt") as TextBox);

    //        if (chkRow.Checked)
    //        {
    //            // SS = "TRUE";
    //            using (SqlCommand GRN_cmd = new SqlCommand("USP_QUOTATION", con))
    //            {
    //                GRN_cmd.CommandType = CommandType.StoredProcedure;
    //                GRN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDUPDATE";
    //                GRN_cmd.Parameters.Add("@QUTIONID", SqlDbType.VarChar).Value = lblEditgrd.Text;
    //                GRN_cmd.Parameters.Add("@RFQNO", SqlDbType.VarChar).Value = txtRfqNo.Text;
    //                GRN_cmd.Parameters.Add("@REFFDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
    //                GRN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = txtQuotNo.Text;
    //                GRN_cmd.Parameters.Add("@QUOTIONDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtQuotDate.Text).ToString("yyyy-MM-dd");
    //                GRN_cmd.Parameters.Add("@VENDORNAME", SqlDbType.VarChar).Value = ddvendrNm.SelectedValue;
    //                GRN_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtStatecd.Text;
    //                GRN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = "0.00";
    //                GRN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = "0.00";
    //                GRN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = "0.00";
    //                GRN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;


    //                GRN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = hsncodeIt.Text;
    //                GRN_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = nameIt.Text;
    //                GRN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitIt.Text;
    //                GRN_cmd.Parameters.Add("@QUANTITY", SqlDbType.Decimal).Value = quntIt.Text;
    //                GRN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = priceIt.Text;
    //                GRN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = amtIt.Text;
    //                GRN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = cgstIt.Text;
    //                GRN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = sgstIt.Text;
    //                GRN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = igstIt.Text;
    //                GRN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = gstamIt.Text;
    //                GRN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = totamtIt.Text;
    //                GRN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = chkRow.Checked;

    //                GRN_cmd.ExecuteNonQuery();
    //            }
    //        }
    //        //-----------------------------------------------------
    //    }

    //    //string message = "alert('*GRN Create Successfully.')";
    //    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
    //    con.Close();
    //}
    #endregion
    protected void btndelete_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@QUTIONID", SqlDbType.VarChar, 500, lblEditgrd.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

            OBJ_METHOD.ExecuteProceedure("USP_QUOTATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clearfield();

            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            #region deletecode
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //using (SqlCommand GRN_cmd = new SqlCommand("USP_QUOTATION", con))
            //{
            //    GRN_cmd.CommandType = CommandType.StoredProcedure;
            //    GRN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //    GRN_cmd.Parameters.Add("@QUTIONID", SqlDbType.VarChar).Value = lblEditgrd.Text;
            //    GRN_cmd.Parameters.Add("@RFQNO", SqlDbType.VarChar).Value = txtRfqNo.Text;
            //    GRN_cmd.Parameters.Add("@REFFDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //    GRN_cmd.Parameters.Add("@QUTIONNO", SqlDbType.VarChar).Value = txtQuotNo.Text;
            //    GRN_cmd.Parameters.Add("@QUOTIONDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtQuotDate.Text).ToString("yyyy-MM-dd");
            //    GRN_cmd.Parameters.Add("@VENDORNAME", SqlDbType.VarChar).Value = ddvendrNm.SelectedValue;
            //    GRN_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtStatecd.Text;
            //    GRN_cmd.Parameters.Add("@TOTPRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
            //    GRN_cmd.Parameters.Add("@TOTGSTAMT", SqlDbType.Decimal).Value = lblgstamt.Text;
            //    GRN_cmd.Parameters.Add("@GRANDTOT", SqlDbType.Decimal).Value = lblgrandtotal.Text;
            //    GRN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

            //    GRN_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "";
            //    GRN_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            //    GRN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
            //    GRN_cmd.Parameters.Add("@QUANTITY", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@CGST", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@SGST", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@IGST", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@GSTAMT", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = "0.00";
            //    GRN_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "";

            //    GRN_cmd.ExecuteNonQuery();
            //}
            ////string message = "alert('*GRN Create Successfully.')";
            ////ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}