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
public partial class PHARMACYSTORE_USER_Default : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9, cmd10, cmd11, cmd12, cmd13, cmd14, cmd15, cmd16;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    int i, no, no1, sl, F;
    int flag = 0, stock = 0, stock_tran = 0, trans = 0, stock1 = 0, S, CLOSEING, slno, CLOSE;
    string id, id1, ph, val1, val2, p, q, des1, name, k, PIN, PAIDAMOUNT, BALANCEAMT;
    public string id_hist;
    public double amt = 0, qty = 0, amt1 = 0, t_amt = 0, qt = 0, c_rs = 0, avg = 0;
    decimal a, b, c, d, e, f, g, h, j;
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
                cmd.CommandText = "select DISTINCT NAME from STOCK_TABLE where NAME like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
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
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            string qry1 = "select ID from SR_TABLE";

            com = new SqlCommand(qry1, con);
            dr = null;

            dr = com.ExecuteReader();

            while (dr.Read())
            {
                num1 = dr["ID"].ToString();
            }
            num1 = string.Format("SR{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
            TXTID.Text = num1;

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
            using (SqlCommand COM = new SqlCommand("FINANCIAL_YEAR", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                SqlDataReader dr = COM.ExecuteReader();
                if (dr.Read())
                {
                    lblfyear.Text = dr["FYEAR"].ToString();
                }
                dr.Close();
            }
            //------------------------
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,CONVERT(VARCHAR(10),INVDATE,105) AS DATE,PNAME AS PARTY FROM SR_TABLE WHERE  ORGID='" + lblorgid.Text + "' AND FYEAR='" + lblfyear.Text + "' ORDER BY ID DESC", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            using (SqlCommand cmd = new SqlCommand("phrmc_saleRetunSelDel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                //GridView2.SelectedIndex = 0;
                GridView2.DataSource = dt;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
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
       
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[17] { new DataColumn("COMPANY"), new DataColumn("NAME"), new DataColumn("HSNCODE"), new DataColumn("BATCHNO"), new DataColumn("CATEGORY"), new DataColumn("EXP"), new DataColumn("UNIT"), new DataColumn("PRICE"), new DataColumn("QTY"), new DataColumn("DISC"), new DataColumn("DISCAMT"), new DataColumn("AMOUNT"), new DataColumn("CGST"), new DataColumn("SGST"), new DataColumn("IGST"), new DataColumn("GSTAMT"), new DataColumn("TOTALAMOUNT") });
            ViewState["ITEM"] = dt;
            this.BindGrid();

            binddata();

            //foreach (ListItem item in droprtype.Items)
            //{
            //    if (item.Text == "ONCOUNTER")
            //    {
            //        txtIPNO.Enabled = false;
            //        break;
            //    }
            //    //if (droprtype.SelectedValue == )
            //    //{
                    
            //    //}
            //    //else
            //    //{
            //    //    txtIPNO.Enabled = true;
            //    //}
            //}
        

        }
        con.Close();
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
    public void clearinercontrol()
    {
        txtbatchno.Text = "";
        txtcgst.Text = "0";
        txtname.Text = "";
        txtpprice.Text = "0";
        txtopening.Text = "0";
        txtSgst.Text = "0";
        txtIGST.Text = "0";
        txtamount.Text = "0";
        txtgstamount.Text = "0";
        txthsncode.Text = "";
    }
    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand("select COMPANY, HSNCODE,NAME,BATCHNO,SPRICE,GST,GST/2 AS GST2 from ITEM_TABLE where NAME='" + txtname.Text + "' AND ORGID='" + lblorgid.Text + "'", con);
        dr = com.ExecuteReader();
        if (dr.Read())
        {
            clearinercontrol();
            txtcomp.Text = dr["COMPANY"].ToString();
            txthsncode.Text = dr["HSNCODE"].ToString();
            txtname.Text = dr["NAME"].ToString();
            txtbatchno.Text = dr["BATCHNO"].ToString();
            txtpprice.Text = dr["SPRICE"].ToString();
            if (txtstatecode.Text == txtcstatecode.Text)
            {
                txtcgst.Text = dr["GST2"].ToString();
                txtSgst.Text = dr["GST2"].ToString();
            }
            else
            {
                txtIGST.Text = dr["GST"].ToString();
            }
        

            dr.Close();
            da = new SqlDataAdapter("select CATEGORY FROM ITEM_TABLE where NAME='" + txtname.Text + "' AND ORGID='" + lblorgid.Text + "'", con);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dropcate.DataSource = ds;
            dropcate.DataTextField = "CATEGORY";
            dropcate.DataValueField = "CATEGORY";
            dropcate.DataBind();
            try
            {
                dis = 0;
                dism = 0;
                totalamt1 = 0;
                totamt = 0;
                totalgstamt = 0;
                txtdiscamt.Text = Math.Round((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtdisc.Text)) / 100).ToString();
                txtamount.Text = Math.Round((Convert.ToDouble(txtopening.Text) * Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))).ToString();
              
                if (txtstatecode.Text == txtcstatecode.Text)
                {
                    txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
                }
                else
                {
                    txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * (Convert.ToDouble(txtIGST.Text))) / 100).ToString();
                }
               
                txttotalamount.Text = Math.Round((Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtgstamount.Text))).ToString();
            }
            catch
            {
            }
            
        }
       
        else
        {
            dr.Close();
            con.Close();
            string message = "alert('* Incorrect Name.Try Correct Name.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        con.Close();
    }
    protected void txtopening_TextChanged(object sender, EventArgs e)
    {
        if (txtopening.Text == "")
        {
            txtopening.Text = "0";
        }
        try
        {
            dis = 0;
            dism = 0;
            totalamt1 = 0;
            totamt = 0;
            totalgstamt = 0;
            txtdiscamt.Text = Math.Round((Convert.ToDouble(txtopening.Text))*(Convert.ToDouble(txtpprice.Text))  * (Convert.ToDouble(txtdisc.Text)) / 100).ToString();
            txtamount.Text = Math.Round((Convert.ToDouble(txtopening.Text) * Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))).ToString();
            if (txtstatecode.Text == txtcstatecode.Text)
            {
                txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
            }
            else
            {
                txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * (Convert.ToDouble(txtIGST.Text))) / 100).ToString();
              
            }
            txttotalamount.Text = Math.Round((Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtgstamount.Text))).ToString();
        }
        catch
        {
        }
    }
    protected void txtdisc_TextChanged(object sender, EventArgs e)
    {
        if (txtdisc.Text == "")
        {
            txtdisc.Text = "0";
        }
        try
        {
            dis = 0;
            dism = 0;
            totalamt1 = 0;
            totamt = 0;
            totalgstamt = 0;
            
            txtdiscamt.Text = Math.Round((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtdisc.Text)) / 100).ToString();
            txtamount.Text = Math.Round((Convert.ToDouble(txtopening.Text) * Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))).ToString();
            if (txtstatecode.Text == txtcstatecode.Text)
            {
                txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
            }
            else
            {
                txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * (Convert.ToDouble(txtIGST.Text))) / 100).ToString();
            }
            txttotalamount.Text = Math.Round((Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtgstamount.Text))).ToString();
        }
        catch
        {
        }
    }
    protected void GVOnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lbl_slno") as Label).Text = (e.Row.RowIndex + 1).ToString();
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('*Add Items To Add.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtpprice.Text == "")
            {
                string message = "alert('*Add Items To Add.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtopening.Text == "")
            {
                string message = "alert('*Add Items To Add.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (Convert.ToInt32(txtopening.Text) <= 0)
            {
                string message = "alert('*Add Items To Add.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (Convert.ToDecimal(txtpprice.Text) <= 0)
            {
                string message = "alert('*Add Items To Add.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            if (txtexpirydate.Text == "")
            {
                txtexpirydate.Text = "NULL";
            }
            DataTable dt = (DataTable)ViewState["ITEM"];
            dt.Rows.Add(txtcomp.Text.Trim(), txtname.Text.Trim(), txthsncode.Text.Trim(), txtbatchno.Text.Trim(), dropcate.Text.Trim(), txtexpirydate.Text.Trim(), droppurchaseunit.Text.Trim(), txtpprice.Text.Trim(), txtopening.Text.Trim(), txtdisc.Text.Trim(), txtdiscamt.Text.Trim(), txtamount.Text.Trim(), txtcgst.Text.Trim(), txtSgst.Text.Trim(), txtIGST.Text.Trim(), txtgstamount.Text.Trim(), txttotalamount.Text.Trim());
            ViewState["ITEM"] = dt;
            this.BindGrid();
            lbltotalprice.Text = (Convert.ToDecimal(txtpprice.Text) * Convert.ToDecimal(txtopening.Text) + Convert.ToDecimal(lbltotalprice.Text)).ToString();
            lbldiscamt.Text = ((Convert.ToDecimal(txtdiscamt.Text) + Convert.ToDecimal(lbldiscamt.Text))).ToString();
            lbltotalamt.Text = (Convert.ToDecimal(lbltotalamt.Text) + Convert.ToDecimal(txtamount.Text)).ToString();
            lblgstamt.Text = (Convert.ToDecimal(lblgstamt.Text) + Convert.ToDecimal(txtgstamount.Text)).ToString();
            lblgrandtotal.Text = (Convert.ToDecimal(lblgrandtotal.Text) + Convert.ToDecimal(txttotalamount.Text)).ToString();
            clearinercontrol();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = (DataTable)ViewState["ITEM"];
            GridViewRow row = (GridViewRow)grvStudentDetails.Rows[e.RowIndex];
            Label qty = (Label)row.FindControl("lbl_qty");
            Label price = (Label)row.FindControl("lbl_price");
            Label discamt = (Label)row.FindControl("lbl_discamt");
            Label amt = (Label)row.FindControl("lbl_amount");
            Label gstamt = (Label)row.FindControl("lbl_gstamt");
            Label totalamt = (Label)row.FindControl("lbl_totalamount");
            string QTY = qty.Text.ToString();
            string PRICE = price.Text.ToString();
            string DISCAMT = discamt.Text.ToString();
            string AMOUNT = amt.Text.ToString();
            string GSTAMT = gstamt.Text.ToString();
            string TOTALAMT = totalamt.Text.ToString();

            dt.Rows[index].Delete();
            ViewState["ITEM"] = dt;
            lbltotalprice.Text = (Convert.ToDecimal(lbltotalprice.Text) - (Convert.ToDecimal(QTY) * Convert.ToDecimal(PRICE))).ToString();
            lbldiscamt.Text = (Convert.ToDecimal(lbldiscamt.Text) - Convert.ToDecimal(DISCAMT)).ToString();
            lbltotalamt.Text = (Convert.ToDecimal(lbltotalamt.Text) - Convert.ToDecimal(AMOUNT)).ToString();
            lblgstamt.Text = (Convert.ToDecimal(lblgstamt.Text) - Convert.ToDecimal(GSTAMT)).ToString();
            lblgrandtotal.Text = (Convert.ToDecimal(lblgrandtotal.Text) - Convert.ToDecimal(TOTALAMT)).ToString();

            this.BindGrid();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
     
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtpartyname.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtinvdate.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtcstatecode.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (grvStudentDetails.Rows.Count <= 0)
            {
                string message = "alert('*Add item to sale.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();


            auto();
            dr.Close();
            STOCK_TABLE();
            STOCK_TRAN();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/PHARMACYSTORE/USER/Sale_return.aspx");
    }
    public void STOCK_TABLE()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

           // SqlCommand stock_cmd = new SqlCommand("insert into SR_TABLE (FYEAR,ORGID,ID,IPNO,PNAME,STATECODE,INVDATE,TOTALPRICE,TOTALDISCAMT,TOTALAMT,GSTAMT,GT) values (@FYEAR,@ORGID,@ID,@IPNO,@PNAME,@STATECODE,@INVDATE,@TOTALPRICE,@TOTALDISCAMT,@TOTALAMT,@GSTAMT,@GT)", con);
            using (SqlCommand stock_cmd = new SqlCommand("phrmc_saleRetunInsUp", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                stock_cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = txtIPNO.Text;
                stock_cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtpartyname.Text;
                stock_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtstatecode.Text;
                stock_cmd.Parameters.Add("@INVOICENO", SqlDbType.VarChar).Value = TXTID.Text;
                stock_cmd.Parameters.Add("@INVDATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd");
                stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
                stock_cmd.Parameters.Add("@TOTALDISCAMT", SqlDbType.VarChar).Value = lbldiscamt.Text;
                stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = lbltotalamt.Text;
                stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = lblgstamt.Text;
                stock_cmd.Parameters.Add("@GT", SqlDbType.VarChar).Value = lblgrandtotal.Text;
                stock_cmd.ExecuteNonQuery();
            }
                using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
                    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
                    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
                    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "MEDICINE CRV BILL";
                    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = "-"+lblgrandtotal.Text;
                    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.Text;
                    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "-"+lblgrandtotal.Text;
                    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtIPNO.Text;
                    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "MEDICINE CHARGES";
                    cm.ExecuteNonQuery();
                }
               
                con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void STOCK_TRAN()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            DataTable dt1 = ViewState["ITEM"] as DataTable;
            GridView1.DataSource = dt1;
            GridView1.DataBind();

            foreach (GridViewRow gv1 in GridView1.Rows)
            {
                // SqlCommand cmd2 = new SqlCommand("insert into SRETURN_TABLE (ORGID,ID, COMPANY,NAME,HSNCODE,BATCHNO,CATEGORY,EXPDATE,UNIT,PRICE,QTY,DISC,DISCAMT,AMOUNT,CGST,SGST,IGST,GSTAMT,TOTALAMT) values (@ORGID,@ID, @COMPANY,@NAME,@HSNCODE,@BATCHNO,@CATEGORY,@EXPDATE,@UNIT,@PRICE,@QTY,@DISC,@DISCAMT,@AMOUNT,@CGST,@SGST,@IGST,@GSTAMT,@TOTALAMT)", con);
                using (SqlCommand cmd2 = new SqlCommand("phrmc_saleRetunIns", con))
                {
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                    cmd2.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                    cmd2.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                    cmd2.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = gv1.Cells[2].Text;
                    cmd2.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
                    cmd2.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
                    cmd2.Parameters.Add("@EXPDATE", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                    cmd2.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
                    cmd2.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = gv1.Cells[7].Text;
                    cmd2.Parameters.Add("@QTY", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
                    cmd2.Parameters.Add("@DISC", SqlDbType.VarChar).Value = gv1.Cells[9].Text;
                    cmd2.Parameters.Add("@DISCAMT", SqlDbType.VarChar).Value = gv1.Cells[10].Text;
                    cmd2.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = gv1.Cells[11].Text;
                    cmd2.Parameters.Add("@CGST", SqlDbType.VarChar).Value = gv1.Cells[12].Text;
                    cmd2.Parameters.Add("@SGST", SqlDbType.VarChar).Value = gv1.Cells[13].Text;
                    cmd2.Parameters.Add("@IGST", SqlDbType.VarChar).Value = gv1.Cells[14].Text;
                    cmd2.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = gv1.Cells[15].Text;
                    cmd2.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = gv1.Cells[16].Text;
                    cmd2.ExecuteNonQuery();
                }
                
                using (SqlCommand stock_cmd = new SqlCommand("sreturn_Tran", con))
                {
                    stock_cmd.CommandType = CommandType.StoredProcedure;
                    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd");
                    stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                    stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                    stock_cmd.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
                    stock_cmd.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
                    stock_cmd.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = gv1.Cells[7].Text;
                    stock_cmd.Parameters.Add("@SPRICE", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                    stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@PURCHASE", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@PRETURN", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@SALE", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@SRETURN", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
                    stock_cmd.Parameters.Add("@CLOSEING", SqlDbType.VarChar).Value = "0";
                    stock_cmd.ExecuteNonQuery();
                }

                using (SqlCommand stock_cmd = new SqlCommand("SR_STOCK_TABLE", con))
                {
                    stock_cmd.CommandType = CommandType.StoredProcedure;
                    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    stock_cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = TXTID.Text;
                    stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                    stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                    stock_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = gv1.Cells[8].Text;
                    stock_cmd.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
                    stock_cmd.ExecuteNonQuery();
                }
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void STOCK_TABLE_UPDATE()
    {
        try
        {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        //SqlCommand stock_cmd = new SqlCommand("UPDATE SR_TABLE SET IPNO=@IPNO,PNAME=@PNAME,STATECODE=@STATECODE,INVDATE=@INVDATE,TOTALPRICE=@TOTALPRICE,TOTALDISCAMT=@TOTALDISCAMT,TOTALAMT=@TOTALAMT,GSTAMT=@GSTAMT,GT=@GT WHERE ID=@ID", con);
        using (SqlCommand stock_cmd = new SqlCommand("phrmc_saleRetunInsUp", con))
        {
            stock_cmd.CommandType = CommandType.StoredProcedure;
            stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            stock_cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = txtIPNO.Text;
            stock_cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtpartyname.Text;
            stock_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtstatecode.Text;
            stock_cmd.Parameters.Add("@INVOICENO", SqlDbType.VarChar).Value = TXTID.Text;
            stock_cmd.Parameters.Add("@INVDATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd");
            stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
            stock_cmd.Parameters.Add("@TOTALDISCAMT", SqlDbType.VarChar).Value = lbldiscamt.Text;
            stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = lbltotalamt.Text;
            stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = lblgstamt.Text;
            stock_cmd.Parameters.Add("@GT", SqlDbType.VarChar).Value = lblgrandtotal.Text;
            stock_cmd.ExecuteNonQuery();
        }

        using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
        {
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE1";
            cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
            cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
            cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
            cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
            cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "MEDICINE CRV BILL";
            cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = "-" + LBPAIDAMT.Text;
            cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.Text;
            cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
            cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "-" + LBPAIDAMT.Text;
            cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtIPNO.Text;
            cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "MEDICINE CHARGES";
            cm.ExecuteNonQuery();
        }
        using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
        {
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
            cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
            cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
            cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
            cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "MEDICINE CRV BILL";
            cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = "-" + lblgrandtotal.Text;
            cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.Text;
            cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
            cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "-" + lblgrandtotal.Text;
            cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtIPNO.Text;
            cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "MEDICINE CHARGES";
            cm.ExecuteNonQuery();
        }
        con.Close();

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void STOCK_TRAN_UPDATE()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();


            //SqlDataAdapter da = new SqlDataAdapter("delete from SRETURN_TABLE where ID='" + TXTID.Text + "'", con);
            //DataSet d = new DataSet();
            //da.Fill(d);
            using (SqlCommand cmd = new SqlCommand("phrmc_saleRetunSelDel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELSRETN";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
                cmd.ExecuteNonQuery();
            }
            foreach (GridViewRow gv1 in GridView1.Rows)
            {
                using (SqlCommand stock_cmd = new SqlCommand("sreturn_Tran", con))
                {
                    stock_cmd.CommandType = CommandType.StoredProcedure;
                    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                    stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd");
                    stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                    stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                    stock_cmd.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
                    stock_cmd.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
                    stock_cmd.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = gv1.Cells[7].Text;
                    stock_cmd.Parameters.Add("@SPRICE", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                    stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@PURCHASE", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@PRETURN", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@SALE", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@SRETURN", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
                    stock_cmd.Parameters.Add("@CLOSEING", SqlDbType.VarChar).Value = "0";
                    stock_cmd.ExecuteNonQuery();
                }

                using (SqlCommand stock_cmd = new SqlCommand("SR_STOCK_TABLE", con))
                {
                    stock_cmd.CommandType = CommandType.StoredProcedure;
                    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                    stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    stock_cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = TXTID.Text;
                    stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                    stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                    stock_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = gv1.Cells[8].Text;
                    stock_cmd.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
                    stock_cmd.ExecuteNonQuery();

                }
            }
            DataTable dt1 = ViewState["ITEM"] as DataTable;
            GridView1.DataSource = dt1;
            GridView1.DataBind();
            foreach (GridViewRow gv1 in GridView1.Rows)
            {
              //  SqlCommand cmd2 = new SqlCommand("insert into SRETURN_TABLE (ORGID,ID, COMPANY,NAME,HSNCODE,BATCHNO,CATEGORY,EXPDATE,UNIT,PRICE,QTY,DISC,DISCAMT,AMOUNT,CGST,SGST,IGST,GSTAMT,TOTALAMT) values (@ORGID,@ID, @COMPANY,@NAME,@HSNCODE,@BATCHNO,@CATEGORY,@EXPDATE,@UNIT,@PRICE,@QTY,@DISC,@DISCAMT,@AMOUNT,@CGST,@SGST,@IGST,@GSTAMT,@TOTALAMT)", con);
                using (SqlCommand cmd2 = new SqlCommand("phrmc_saleRetunIns", con))
                {
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                    cmd2.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                    cmd2.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                    cmd2.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = gv1.Cells[2].Text;
                    cmd2.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
                    cmd2.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
                    cmd2.Parameters.Add("@EXPDATE", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                    cmd2.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
                    cmd2.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = gv1.Cells[7].Text;
                    cmd2.Parameters.Add("@QTY", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
                    cmd2.Parameters.Add("@DISC", SqlDbType.VarChar).Value = gv1.Cells[9].Text;
                    cmd2.Parameters.Add("@DISCAMT", SqlDbType.VarChar).Value = gv1.Cells[10].Text;
                    cmd2.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = gv1.Cells[11].Text;
                    cmd2.Parameters.Add("@CGST", SqlDbType.VarChar).Value = gv1.Cells[12].Text;
                    cmd2.Parameters.Add("@SGST", SqlDbType.VarChar).Value = gv1.Cells[13].Text;
                    cmd2.Parameters.Add("@IGST", SqlDbType.VarChar).Value = gv1.Cells[14].Text;
                    cmd2.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = gv1.Cells[15].Text;
                    cmd2.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = gv1.Cells[16].Text;
                    cmd2.ExecuteNonQuery();
                }


                using (SqlCommand stock_cmd = new SqlCommand("sreturn_Tran", con))
                {
                    stock_cmd.CommandType = CommandType.StoredProcedure;
                    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd");
                    stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                    stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                    stock_cmd.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
                    stock_cmd.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
                    stock_cmd.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = gv1.Cells[7].Text;
                    stock_cmd.Parameters.Add("@SPRICE", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                    stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@PURCHASE", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@PRETURN", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@SALE", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@SRETURN", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
                    stock_cmd.Parameters.Add("@CLOSEING", SqlDbType.VarChar).Value = "0";
                    stock_cmd.ExecuteNonQuery();
                }

                using (SqlCommand stock_cmd = new SqlCommand("SR_STOCK_TABLE", con))
                {
                    stock_cmd.CommandType = CommandType.StoredProcedure;
                    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    stock_cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = TXTID.Text;
                    stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                    stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                    stock_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = gv1.Cells[8].Text;
                    stock_cmd.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
                    stock_cmd.ExecuteNonQuery();


                }
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void STOCK_TABLE_DEL()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            //SqlDataAdapter da = new SqlDataAdapter("delete from SR_TABLE where ID='" + TXTID.Text + "'", con);
            //DataSet d = new DataSet();
            //da.Fill(d);  
            using (SqlCommand cmd = new SqlCommand("phrmc_saleRetunSelDel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELSRT";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
                cmd.ExecuteNonQuery();
            }
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = txtIPNO.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "MEDICINE CRV BILL";
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = "-" + lblgrandtotal.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = droprtype.Text;
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "-" + lblgrandtotal.Text;
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtIPNO.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "MEDICINE CHARGES";
                cm.ExecuteNonQuery();
            }

            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void STOCK_TRAN_DEL()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            DataTable dt1 = ViewState["ITEM"] as DataTable;
            GridView1.DataSource = dt1;
            GridView1.DataBind();
            //SqlDataAdapter da = new SqlDataAdapter("delete from SRETURN_TABLE where ID='" + TXTID.Text + "'", con);
            //DataSet d = new DataSet();
            //da.Fill(d); 
            using (SqlCommand cmd = new SqlCommand("phrmc_saleRetunSelDel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELSRETN";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
                cmd.ExecuteNonQuery();
            }
            foreach (GridViewRow gv1 in GridView1.Rows)
            {
                using (SqlCommand stock_cmd = new SqlCommand("sreturn_Tran", con))
                {
                    stock_cmd.CommandType = CommandType.StoredProcedure;
                    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                    stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = txtinvdate.Text;
                    stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                    stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                    stock_cmd.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
                    stock_cmd.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
                    stock_cmd.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = gv1.Cells[7].Text;
                    stock_cmd.Parameters.Add("@SPRICE", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                    stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@PURCHASE", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@PRETURN", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@SALE", SqlDbType.VarChar).Value = "0";
                    stock_cmd.Parameters.Add("@SRETURN", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
                    stock_cmd.Parameters.Add("@CLOSEING", SqlDbType.VarChar).Value = "0";
                    stock_cmd.ExecuteNonQuery();
                }

                using (SqlCommand stock_cmd = new SqlCommand("SR_STOCK_TABLE", con))
                {
                    stock_cmd.CommandType = CommandType.StoredProcedure;
                    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                    stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    stock_cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = TXTID.Text;
                    stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                    stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                    stock_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = gv1.Cells[8].Text;
                    stock_cmd.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
                    stock_cmd.ExecuteNonQuery();


                }
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //SqlCommand com = new SqlCommand("select ID,IPNO,PNAME,STATECODE,CONVERT(VARCHAR(10),INVDATE,105) AS INVDATE,TOTALPRICE,TOTALDISCAMT,TOTALAMT,GSTAMT,GT from SR_TABLE where ID='" + slno + "'", con);
           // dr = com.ExecuteReader();
            using (SqlCommand cmd = new SqlCommand("phrmc_saleRetunSelDel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    TXTID.Text = dr["ID"].ToString();
                    txtIPNO.Text = dr["IPNO"].ToString();
                    txtpartyname.Text = dr["PNAME"].ToString();
                    txtstatecode.Text = dr["STATECODE"].ToString();
                    txtinvdate.Text = dr["INVDATE"].ToString();
                    lbltotalprice.Text = dr["TOTALPRICE"].ToString();
                    lbldiscamt.Text = dr["TOTALDISCAMT"].ToString();
                    lbltotalamt.Text = dr["TOTALAMT"].ToString();
                    lblgstamt.Text = dr["GSTAMT"].ToString();
                    lblgrandtotal.Text = dr["GT"].ToString();
                    LBPAIDAMT.Text = dr["GT"].ToString();
                }
                dr.Close();
            }
            //--------------------------
            //da = new SqlDataAdapter("select COMPANY,NAME,HSNCODE,BATCHNO,CATEGORY,EXPDATE as EXP,UNIT,PRICE,QTY,DISC,DISCAMT,AMOUNT,CGST,SGST,IGST,GSTAMT,TOTALAMT AS TOTALAMOUNT FROM SRETURN_TABLE where ID='" + TXTID.Text + "' and ORGID='" + lblorgid.Text + "'", con);
            //DataSet ds2 = new DataSet();
            //da.Fill(ds2);
            using (SqlCommand cmd1 = new SqlCommand("phrmc_saleRetunSelDel", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECTITEM";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                cmd1.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataSet ds2 = new DataSet();
                da.Fill(ds2);
                grvStudentDetails.DataSource = ds2.Tables["Table"];
                grvStudentDetails.DataBind();

                DataTable dt = ds2.Tables["Table"];
                ViewState["ITEM"] = dt;
            }

            //SqlDataAdapter da1 = new SqlDataAdapter("select COMPANY,NAME,HSNCODE,BATCHNO,CATEGORY,EXPDATE as EXP,UNIT,PRICE,QTY,DISC,DISCAMT,AMOUNT,CGST,SGST,IGST,GSTAMT,TOTALAMT AS TOTALAMOUNT FROM SRETURN_TABLE where ID='" + TXTID.Text + "' and ORGID='" + lblorgid.Text + "'", con);
            //DataSet ds3 = new DataSet();
            //da1.Fill(ds3);
            using (SqlCommand cmd2 = new SqlCommand("phrmc_saleRetunSelDel", con))
            {
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECTITEM";
                cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                cmd2.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
                cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
                DataSet ds3 = new DataSet();
                da1.Fill(ds3);
                GridView1.DataSource = ds3.Tables["Table"];
                GridView1.DataBind();
            }
            btndelete.Visible = true;
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,CONVERT(VARCHAR(10),INVDATE,105) AS DATE,PNAME AS PARTY FROM SR_TABLE WHERE  ORGID='" + lblorgid.Text + "' AND FYEAR='" + lblfyear.Text + "' ORDER BY ID DESC", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            using (SqlCommand cmd = new SqlCommand("phrmc_saleRetunSelDel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INDEXCHG";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                // GridView2.SelectedIndex = 0;
                GridView2.PageIndex = e.NewPageIndex;
                GridView2.DataSource = dt;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtinvdate.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtcstatecode.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (grvStudentDetails.Rows.Count <= 0)
            {
                string message = "alert('*Add item to sale.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if (txtexpirydate.Text == "")
            {
                txtexpirydate.Text = "NULL";
            }
            STOCK_TABLE_UPDATE();
            STOCK_TRAN_UPDATE();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/PHARMACYSTORE/USER/Sale_return.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PHARMACYSTORE/USER/Sale_return.aspx");
    }
    protected void Btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            STOCK_TABLE_DEL();
            STOCK_TRAN_DEL();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/PHARMACYSTORE/USER/Sale_return.aspx");
    }
    protected void txtbatchno_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand("select COMPANY, HSNCODE,NAME,BATCHNO,SPRICE,GST,GST/2 AS GST2 from ITEM_TABLE where BATCHNO='" + txtbatchno.Text + "' AND ORGID='" + lblorgid.Text + "'", con);
        dr = com.ExecuteReader();
        if (dr.Read())
        {
            clearinercontrol();
            txtcomp.Text = dr["COMPANY"].ToString();
            txthsncode.Text = dr["HSNCODE"].ToString();
            txtname.Text = dr["NAME"].ToString();
            txtbatchno.Text = dr["BATCHNO"].ToString();
            txtpprice.Text = dr["SPRICE"].ToString();
            if (txtstatecode.Text == txtcstatecode.Text)
            {
                txtcgst.Text = dr["GST2"].ToString();
                txtSgst.Text = dr["GST2"].ToString();
            }
            else
            {
                txtIGST.Text = dr["GST"].ToString();
            }


            dr.Close();
            da = new SqlDataAdapter("select CATEGORY FROM ITEM_TABLE where NAME='" + txtname.Text + "' AND ORGID='" + lblorgid.Text + "'", con);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dropcate.DataSource = ds;
            dropcate.DataTextField = "CATEGORY";
            dropcate.DataValueField = "CATEGORY";
            dropcate.DataBind();
            try
            {
                dis = 0;
                dism = 0;
                totalamt1 = 0;
                totamt = 0;
                totalgstamt = 0;
                txtdiscamt.Text = Math.Round((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtdisc.Text)) / 100).ToString();
                txtamount.Text = Math.Round((Convert.ToDouble(txtopening.Text) * Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))).ToString();

                if (txtstatecode.Text == txtcstatecode.Text)
                {
                    txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
                }
                else
                {
                    txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * (Convert.ToDouble(txtIGST.Text))) / 100).ToString();
                }

                txttotalamount.Text = Math.Round((Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtgstamount.Text))).ToString();
            }
            catch
            {
            }

        }

        else
        {
            dr.Close();
            con.Close();
            string message = "alert('* Incorrect Name.Try Correct Name.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        con.Close();
    }
    protected void txtIPNO_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        if (droprtype.SelectedIndex == 0)
        {
            SqlCommand com = new SqlCommand("select * from PATIENT_REG_TABLE where ID='" + txtIPNO.Text + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {

                txtpartyname.Text = dr["NAME"].ToString();
                dr.Close();
            }
            else
            {
                string message = "alert('*Invalid OPNO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dr.Close();
            }
            dr.Close();
        }
        else if (droprtype.SelectedIndex == 1)
        {
            SqlCommand com = new SqlCommand("select * from ADMISSION_TABLE where VN='" + txtIPNO.Text + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {

                txtpartyname.Text = dr["NAME"].ToString();
                dr.Close();
            }
            else
            {
                string message = "alert('*Invalid IPNO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dr.Close();
            }
            dr.Close();
        }
        con.Close();
    }
    protected void droprtype_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
}