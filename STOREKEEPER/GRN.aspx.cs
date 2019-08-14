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

public partial class STOREKEEPER_GRN : System.Web.UI.Page
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
                cmd.CommandText = "select DISTINCT NAME from MATERIAL_MASTER_TABLE where NAME like @SearchText+'%'";
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
    public void clearinercontrol()
    {
        //txtbatchno.Text = "";
        txtcgst.Text = "0";
        txtitemname.Text = "";
        txtpprice.Text = "0";
        txtopening.Text = "0";
        txtSgst.Text = "0";
        txtIGST.Text = "0";
        txtamount.Text = "0";
        txtgstamount.Text = "0";
        txthsncode.Text = "";
    }
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand COM = new SqlCommand("SELECT FYEAR FROM FYEAR1_TABLE", con);
        dr = COM.ExecuteReader();
        if (dr.Read())
        {            
            lblfyear.Text = dr["FYEAR"].ToString();
        }
        dr.Close();
        //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,CONVERT(VARCHAR(10),INVDATE,105) AS DATE,VENDOR AS VENDOR FROM GRN_TABLE WHERE FYEAR='" + lblfyear.Text + "' ORDER BY ID DESC", con);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //GridView2.SelectedIndex = 0;
        //GridView2.DataSource = dt;
        //GridView2.DataKeyNames = new string[] { "ID" };
        //GridView2.DataBind();
        da1 = new SqlDataAdapter("select NAME1,ID FROM VENDER_MASTER_TABLE ", con);
        DataTable ds1 = new DataTable();
        da1.Fill(ds1);
        dropVendor.DataSource = ds1;
        dropVendor.DataTextField = "NAME1";
        dropVendor.DataValueField = "ID";
        dropVendor.DataBind();
        dropVendor.Items.Insert(0, "Please Select");


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
            dt.Columns.AddRange(new DataColumn[11] {  new DataColumn("NAME"), new DataColumn("HSNCODE"), new DataColumn("UNIT"), new DataColumn("PRICE"), new DataColumn("QTY"), new DataColumn("AMOUNT"), new DataColumn("CGST"), new DataColumn("SGST"), new DataColumn("IGST"), new DataColumn("GSTAMT"), new DataColumn("TOTALAMOUNT") });
            ViewState["ITEM"] = dt;
            this.BindGrid();
            binddata();
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
    protected void txtname_TextChanged(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        SqlCommand com = new SqlCommand("select NAME,HSNCODE,PRICE,GST,GST/2 AS GST2,UNIT from MATERIAL_MASTER_TABLE where NAME='" + txtitemname.Text + "'", con);
        dr = com.ExecuteReader();
        if (dr.Read())
        {
            //clearinercontrol();
            txthsncode.Text = dr["HSNCODE"].ToString();
            txtitemname.Text = dr["NAME"].ToString();
            txtpprice.Text = dr["PRICE"].ToString();
            txtunit.Text = dr["UNIT"].ToString();
            if (txtstatecode.Text == "21")
            {
                txtcgst.Text = dr["GST2"].ToString();
                txtSgst.Text = dr["GST2"].ToString();
            }
            else
            {
                txtIGST.Text = dr["GST"].ToString();
            }


            dr.Close();
            try
            {
                dis = 0;
                dism = 0;
                totalamt1 = 0;
                totamt = 0;
                totalgstamt = 0;
                //txtdiscamt.Text = Math.Round((Convert.ToDouble(txtopening.Text)) - (Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtdisc.Text)) / 100).ToString();
                txtamount.Text = Math.Round((Convert.ToDouble(txtopening.Text)) * Convert.ToDouble(txtpprice.Text)).ToString();

                if (txtstatecode.Text == "21")
                {
                    txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text))) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
                }
                else
                {
                    txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtIGST.Text)))) / 100).ToString();
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
            string message = "alert('* Incorrect Name.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        con.Close();

    }
    protected void txtopening_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        SqlCommand com = new SqlCommand("select NAME,HSNCODE,PRICE,GST,GST/2 AS GST2,UNIT from MATERIAL_MASTER_TABLE where NAME='" + txtitemname.Text + "'", con);
        dr = com.ExecuteReader();
        if (dr.Read())
        {
            //clearinercontrol();
            txthsncode.Text = dr["HSNCODE"].ToString();
            txtitemname.Text = dr["NAME"].ToString();
            txtpprice.Text = dr["PRICE"].ToString();
            txtunit.Text = dr["UNIT"].ToString();
            if (txtstatecode.Text == "21")
            {
                txtcgst.Text = dr["GST2"].ToString();
                txtSgst.Text = dr["GST2"].ToString();
            }
            else
            {
                txtIGST.Text = dr["GST"].ToString();
            }


            dr.Close();
            try
            {
                dis = 0;
                dism = 0;
                totalamt1 = 0;
                totamt = 0;
                totalgstamt = 0;
                //txtdiscamt.Text = Math.Round((Convert.ToDouble(txtopening.Text)) - (Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtdisc.Text)) / 100).ToString();
                txtamount.Text = Math.Round((Convert.ToDouble(txtopening.Text)) * Convert.ToDouble(txtpprice.Text)).ToString();

                if (txtstatecode.Text == "21")
                {
                    txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text))) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
                }
                else
                {
                    txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtIGST.Text)))) / 100).ToString();
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
            string message = "alert('* Incorrect Name.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        con.Close();

    }
    protected void txtprice_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        SqlCommand com = new SqlCommand("select NAME,HSNCODE,PRICE,GST,GST/2 AS GST2,UNIT from MATERIAL_MASTER_TABLE where NAME='" + txtitemname.Text + "'", con);
        dr = com.ExecuteReader();
        if (dr.Read())
        {
            //clearinercontrol();
            txthsncode.Text = dr["HSNCODE"].ToString();
            txtitemname.Text = dr["NAME"].ToString();
           // txtpprice.Text = dr["PRICE"].ToString();
            txtunit.Text = dr["UNIT"].ToString();
            if (txtstatecode.Text == "21")
            {
                txtcgst.Text = dr["GST2"].ToString();
                txtSgst.Text = dr["GST2"].ToString();
            }
            else
            {
                txtIGST.Text = dr["GST"].ToString();
            }


            dr.Close();
            try
            {
                dis = 0;
                dism = 0;
                totalamt1 = 0;
                totamt = 0;
                totalgstamt = 0;
                //txtdiscamt.Text = Math.Round((Convert.ToDouble(txtopening.Text)) - (Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtdisc.Text)) / 100).ToString();
                txtamount.Text = Math.Round((Convert.ToDouble(txtopening.Text)) * Convert.ToDouble(txtpprice.Text)).ToString();

                if (txtstatecode.Text == "21")
                {
                    txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text))) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
                }
                else
                {
                    txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtIGST.Text)))) / 100).ToString();
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
            string message = "alert('* Incorrect Name.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        con.Close();

    }
    protected void dropVendor_SelectedIndexChanged(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand("select * from VENDER_MASTER_TABLE where ID='" + dropVendor.SelectedValue + "'", con);
        dr = com.ExecuteReader();
        if (dr.Read())
        {
            txtstatecode.Text = dr["STATECODE"].ToString();
        }
        dr.Close();
        con.Close();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txtitemname.Text == "")
        {
            string message = "alert('* Select Itemname.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        else if (txtpprice.Text == "")
        {
            string message = "alert('* Select Price.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        else if (txtopening.Text == "")
        {
            string message = "alert('* Select Quantity.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        else if (Convert.ToDecimal(txtopening.Text) <= 0)
        {
            string message = "alert('* Select Quantity.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }

        else if (Convert.ToDecimal(txtpprice.Text) <= 0)
        {
            string message = "alert('* Select Price.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }

        DataTable dt = (DataTable)ViewState["ITEM"];
        dt.Rows.Add( txtitemname.Text.Trim(), txthsncode.Text.Trim(), txtunit.Text.Trim(),txtpprice.Text.Trim(), txtopening.Text.Trim(), txtamount.Text.Trim(), txtcgst.Text.Trim(), txtSgst.Text.Trim(), txtIGST.Text.Trim(), txtgstamount.Text.Trim(), txttotalamount.Text.Trim());
        ViewState["ITEM"] = dt;
        this.BindGrid();
        lbltotalprice.Text = (Convert.ToDecimal(txtpprice.Text) * (Convert.ToDecimal(txtopening.Text)) + Convert.ToDecimal(lbltotalprice.Text)).ToString();

        lblgstamt.Text = (Convert.ToDecimal(lblgstamt.Text) + Convert.ToDecimal(txtgstamount.Text)).ToString();
        lblgrandtotal.Text = (Convert.ToDecimal(lblgrandtotal.Text) + Convert.ToDecimal(txttotalamount.Text)).ToString();
        clearinercontrol();
    }
    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        DataTable dt = (DataTable)ViewState["ITEM"];
        GridViewRow row = (GridViewRow)grvStudentDetails.Rows[e.RowIndex];
        Label qty = (Label)row.FindControl("lbl_qty");
        Label price = (Label)row.FindControl("lbl_price");
        Label amt = (Label)row.FindControl("lbl_amount");
        Label gstamt = (Label)row.FindControl("lbl_gstamt");
        Label totalamt = (Label)row.FindControl("lbl_totalamount");
        string QTY = qty.Text.ToString();
        string PRICE = price.Text.ToString();
        string AMOUNT = amt.Text.ToString();
        string GSTAMT = gstamt.Text.ToString();
        string TOTALAMT = totalamt.Text.ToString();

        dt.Rows[index].Delete();
        ViewState["ITEM"] = dt;
        lbltotalprice.Text = (Convert.ToDecimal(lbltotalprice.Text) - ((Convert.ToDecimal(QTY)) * Convert.ToDecimal(PRICE))).ToString();
        lblgstamt.Text = (Convert.ToDecimal(lblgstamt.Text) - Convert.ToDecimal(GSTAMT)).ToString();
        lblgrandtotal.Text = (Convert.ToDecimal(lblgrandtotal.Text) - Convert.ToDecimal(TOTALAMT)).ToString();

        this.BindGrid();

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
            stock_cmd.Parameters.Add("@DISCOUNTAMOUNT", SqlDbType.VarChar).Value = lbltotalprice.Text;            
            stock_cmd.Parameters.Add("@GSTAMOUNT", SqlDbType.VarChar).Value = lblgstamt.Text;
            stock_cmd.Parameters.Add("@GRANDTOTAL", SqlDbType.VarChar).Value = lblgrandtotal.Text;
            stock_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = txtitemname.Text;
            stock_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = txthsncode.Text;
            stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = txtunit.Text;
            stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = txtopening.Text;
            stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = txtpprice.Text;
            stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = txtamount.Text;
            stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = txtcgst.Text;
            stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = txtSgst.Text;
            stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = txtIGST.Text;
            stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = txtgstamount.Text;
            stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = txttotalamount.Text;
            stock_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
            stock_cmd.Parameters.Add("@PODATE", SqlDbType.Date).Value = Convert.ToDateTime(txtpodate.Text).ToString("yyyy-MM-dd"); 
            stock_cmd.ExecuteNonQuery();
        }
        con.Close();
        //}
        //catch { }
    }
    public void STOCK_TRAN()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        DataTable dt1 = ViewState["ITEM"] as DataTable;
        GridView1.DataSource = dt1;
        GridView1.DataBind();

        foreach (GridViewRow gv1 in GridView1.Rows)
        {
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
                stock_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                stock_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = gv1.Cells[2].Text;
                stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
                stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
                stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
                stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = gv1.Cells[7].Text;
                stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
                stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = gv1.Cells[9].Text;
                stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = gv1.Cells[10].Text;
                stock_cmd.ExecuteNonQuery();
            }
            using (SqlCommand stock_cmd = new SqlCommand("STORE_GRN_Tran", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                stock_cmd.Parameters.Add("@DATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtgrndate.Text).ToString("yyyy-MM-dd");
                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@PURCHES", SqlDbType.VarChar).Value = gv1.Cells[4].Text; ;
                stock_cmd.Parameters.Add("@RETN", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@ISSUE", SqlDbType.VarChar).Value ="0.00";
                stock_cmd.Parameters.Add("@REF", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@CLOSING", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.ExecuteNonQuery();
            }

        }
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

        SqlCommand cm = new SqlCommand("select * from GRN_TABLE where INVOICENO ='" + txtinvoiceno.Text + "'", con);
        dr = cm.ExecuteReader();
        if (dr.Read())
        {
            string message = "alert('*Cant enter duplicate INVOICENO number.INVOICENO number alredy exist.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        auto();
        dr.Close();
        STOCK_TABLE();
        STOCK_TRAN();
        VENDOR_TRAN();
        con.Close();
        Response.Redirect("~/STOREKEEPER/GRN.aspx");
    }
}