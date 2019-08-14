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

public partial class PHARMACYSTORE_pharmacy_purchase : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9, cmd10, cmd11, cmd12, cmd16;
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
    DataMathods OBJ_METHOD = new DataMathods();

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
                cmd.CommandText = "select DISTINCT NAME from STOCK_TABLE where NAME like @SearchText+'%'";
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
        string qry1 = "select ID from PUR_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("PU{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        TXTID.Text = num1;

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

            SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("phrmc_PurchaseSelDel", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = Ds;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
            }
            #region old code
            //-------------------------
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,INVOICENO AS INVOICE,CONVERT(VARCHAR(10),INVDATE,105) AS DATE,PNAME AS PARTY FROM PUR_TABLE WHERE  ORGID='" + lblorgid.Text + "' AND FYEAR='" + lblfyear.Text + "' ORDER BY ID DESC", con);
            //using (SqlCommand cmd = new SqlCommand("phrmc_PurchaseSelDel", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

            //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    adp.Fill(dt);
            //    //GridView2.SelectedIndex = 0;
            //    GridView2.DataSource = dt;
            //    GridView2.DataKeyNames = new string[] { "ID" };
            //    GridView2.DataBind();
            //}
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
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
            auto();
            binddata();
            puritemGrid();
        }
    }
    public void puritemGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[18] { new DataColumn("COMPANY"), new DataColumn("NAME"), new DataColumn("HSNCODE"), new DataColumn("BATCHNO"), new DataColumn("CATEGORY"), new DataColumn("EXP"), new DataColumn("UNIT"), new DataColumn("PRICE"), new DataColumn("QTY"), new DataColumn("FQTY"), new DataColumn("DISC"), new DataColumn("DISCAMT"), new DataColumn("AMOUNT"), new DataColumn("CGST"), new DataColumn("SGST"), new DataColumn("IGST"), new DataColumn("GSTAMT"), new DataColumn("TOTALAMOUNT") });
            ViewState["ITEM"] = dt;
            this.BindGrid();

            DataSet ds1 = OBJ_METHOD.Get_DataSet("select distinct NAME FROM SUPPLIER_TABLE where ORGID='" + lblorgid.Text + "'", false, false);
            
            dropparty.DataSource = ds1;
            dropparty.DataTextField = "NAME";
            dropparty.DataValueField = "NAME";
            dropparty.DataBind();
            dropparty.Items.Insert(0,new ListItem("Please Select","0"));
            
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void dropparty_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = OBJ_METHOD.Get_DataSet("select * from SUPPLIER_TABLE where NAME='" + dropparty.Text + "' AND ORGID='" + lblorgid.Text + "'", false, false);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtgstno.Text = ds.Tables[0].Rows[0]["GST"].ToString();
                txtstatecode.Text = ds.Tables[0].Rows[0]["STATECODE"].ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = OBJ_METHOD.Get_DataSet("select COMPANY, HSNCODE,NAME,BATCHNO,PPRICE,GST,GST/2 AS GST2 from ITEM_TABLE where NAME='" + txtname.Text + "' AND ORGID='" + lblorgid.Text + "' order by ID DESC", false, false);
            if (ds.Tables[0].Rows.Count > 0)
            {
                clearinercontrol();
                txtcomp.Text = ds.Tables[0].Rows[0]["COMPANY"].ToString();
                txthsncode.Text = ds.Tables[0].Rows[0]["HSNCODE"].ToString();
                txtname.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                txtbatchno.Text = ds.Tables[0].Rows[0]["BATCHNO"].ToString();
                txtpprice.Text = ds.Tables[0].Rows[0]["PPRICE"].ToString();
                if (txtstatecode.Text == txtcstatecode.Text)
                {
                    txtcgst.Text = ds.Tables[0].Rows[0]["GST2"].ToString();
                    txtSgst.Text = ds.Tables[0].Rows[0]["GST2"].ToString();
                }
                else
                {
                    txtIGST.Text = ds.Tables[0].Rows[0]["GST"].ToString();
                }
                DataSet ds1 = OBJ_METHOD.Get_DataSet("select CATEGORY FROM ITEM_TABLE where NAME='" + txtname.Text + "' AND ORGID='" + lblorgid.Text + "'", false, false);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    dropcate.DataSource = ds1;
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
                        txtdiscamt.Text = Math.Round((Convert.ToDouble(txtopening.Text)) - (Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtdisc.Text)) / 100).ToString();
                        txtamount.Text = Math.Round((Convert.ToDouble(txtopening.Text) - (Convert.ToDouble(txtfeeqty.Text)) * Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))).ToString();

                        if (txtstatecode.Text == txtcstatecode.Text)
                        {
                            txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) - (Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
                        }
                        else
                        {
                            txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) - (Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * (Convert.ToDouble(txtIGST.Text))) / 100).ToString();
                        }

                        txttotalamount.Text = Math.Round((Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtgstamount.Text))).ToString();
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                dr.Close();
                string message = "alert('* Incorrect Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            #region oldcode
            //SqlCommand com = new SqlCommand("select COMPANY, HSNCODE,NAME,BATCHNO,PPRICE,GST,GST/2 AS GST2 from ITEM_TABLE where NAME='" + txtname.Text + "' AND ORGID='" + lblorgid.Text + "' order by ID DESC", con);
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    clearinercontrol();
            //    txtcomp.Text = dr["COMPANY"].ToString();
            //    txthsncode.Text = dr["HSNCODE"].ToString();
            //    txtname.Text = dr["NAME"].ToString();
            //    txtbatchno.Text = dr["BATCHNO"].ToString();
            //    txtpprice.Text = dr["PPRICE"].ToString();
            //    if (txtstatecode.Text == txtcstatecode.Text)
            //    {
            //        txtcgst.Text = dr["GST2"].ToString();
            //        txtSgst.Text = dr["GST2"].ToString();
            //    }
            //    else
            //    {
            //        txtIGST.Text = dr["GST"].ToString();
            //    }

            //    dr.Close();
            //    da = new SqlDataAdapter("select CATEGORY FROM ITEM_TABLE where NAME='" + txtname.Text + "' AND ORGID='" + lblorgid.Text + "'", con);
            //    DataTable ds = new DataTable();
            //    da.Fill(ds);
            //    dropcate.DataSource = ds;
            //    dropcate.DataTextField = "CATEGORY";
            //    dropcate.DataValueField = "CATEGORY";
            //    dropcate.DataBind();
            //    try
            //    {
            //        dis = 0;
            //        dism = 0;
            //        totalamt1 = 0;
            //        totamt = 0;
            //        totalgstamt = 0;
            //        txtdiscamt.Text = Math.Round((Convert.ToDouble(txtopening.Text)) - (Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtdisc.Text)) / 100).ToString();
            //        txtamount.Text = Math.Round((Convert.ToDouble(txtopening.Text) - (Convert.ToDouble(txtfeeqty.Text)) * Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))).ToString();

            //        if (txtstatecode.Text == txtcstatecode.Text)
            //        {
            //            txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) - (Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
            //        }
            //        else
            //        {
            //            txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) - (Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * (Convert.ToDouble(txtIGST.Text))) / 100).ToString();
            //        }

            //        txttotalamount.Text = Math.Round((Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtgstamount.Text))).ToString();
            //    }
            //    catch
            //    {
            //    }

            //}

            //else
            //{
            //    dr.Close();
            //    string message = "alert('* Incorrect Name.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void txtopening_TextChanged(object sender, EventArgs e)
    {

        if (txtopening.Text == "")
        {
            txtopening.Text = "0";
        }
        else if (txtfeeqty.Text == "")
        {
            txtfeeqty.Text = "0";
        }
        try
        {
            dis = 0;
            dism = 0;
            totalamt1 = 0;
            totamt = 0;
            totalgstamt = 0;
            txtdiscamt.Text = Math.Round(((Convert.ToDouble(txtopening.Text) - Convert.ToDouble(txtfeeqty.Text))) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtdisc.Text)) / 100).ToString();
            txtamount.Text = Math.Round(((Convert.ToDouble(txtopening.Text) - Convert.ToDouble(txtfeeqty.Text)) * Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))).ToString();
            if (txtstatecode.Text == txtcstatecode.Text)
            {
                txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text) - Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
            }
            else
            {
                txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text) - Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * (Convert.ToDouble(txtIGST.Text))) / 100).ToString();

            }
            txttotalamount.Text = Math.Round((Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtgstamount.Text))).ToString();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void txtdisc_TextChanged(object sender, EventArgs e)
    {
        if (txtdisc.Text == "")
        {
            txtdisc.Text = "0";
        }
        else if (txtfeeqty.Text == "")
        {
            txtfeeqty.Text = "0";
        }
        try
        {
            dis = 0;
            dism = 0;
            totalamt1 = 0;
            totamt = 0;
            totalgstamt = 0;
            txtdiscamt.Text = Math.Round(((Convert.ToDouble(txtopening.Text) - Convert.ToDouble(txtfeeqty.Text))) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtdisc.Text)) / 100).ToString();
            txtamount.Text = Math.Round(((Convert.ToDouble(txtopening.Text) - Convert.ToDouble(txtfeeqty.Text)) * Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))).ToString();
            if (txtstatecode.Text == txtcstatecode.Text)
            {
                txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text) - Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
            }
            else
            {
                txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text) - Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * (Convert.ToDouble(txtIGST.Text))) / 100).ToString();

            }
            txttotalamount.Text = Math.Round((Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtgstamount.Text))).ToString();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void GVOnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lbl_slno") as Label).Text = (e.Row.RowIndex + 1).ToString();
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
        txtexpirydate.Text = "";
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtpprice.Text == "")
            {
                string message = "alert('* Price Fields Is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtopening.Text == "")
            {
                string message = "alert('* Total Quantity Is  mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (Convert.ToDecimal(txtopening.Text) <= 0)
            {
                string message = "alert('* Total Quantity Is  mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (Convert.ToDecimal(txtfeeqty.Text) < 0)
            {
                txtfeeqty.Text = "0";
            }
            else if (Convert.ToDecimal(txtpprice.Text) <= 0)
            {
                string message = "alert('* Enter Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtexpirydate.Text == "")
            {
                string message = "alert('Please!! Enter the Expiry Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtfeeqty.Text == "")
            {
                txtfeeqty.Text = "0";
            }
            
            DataTable dt = (DataTable)ViewState["ITEM"];
            dt.Rows.Add(txtcomp.Text.Trim(), txtname.Text.Trim(), txthsncode.Text.Trim(), txtbatchno.Text.Trim(), dropcate.Text.Trim(), txtexpirydate.Text.Trim(), droppurchaseunit.Text.Trim(), txtpprice.Text.Trim(), txtopening.Text.Trim(), txtfeeqty.Text.Trim(), txtdisc.Text.Trim(), txtdiscamt.Text.Trim(), txtamount.Text.Trim(), txtcgst.Text.Trim(), txtSgst.Text.Trim(), txtIGST.Text.Trim(), txtgstamount.Text.Trim(), txttotalamount.Text.Trim());
            ViewState["ITEM"] = dt;
            this.BindGrid();
            lbltotalprice.Text = (Convert.ToDecimal(txtpprice.Text) * (Convert.ToDecimal(txtopening.Text) - Convert.ToDecimal(txtfeeqty.Text)) + Convert.ToDecimal(lbltotalprice.Text)).ToString();
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
            Label fqty = (Label)row.FindControl("lbl_freeqty");
            Label price = (Label)row.FindControl("lbl_price");
            Label discamt = (Label)row.FindControl("lbl_discamt");
            Label amt = (Label)row.FindControl("lbl_amount");
            Label gstamt = (Label)row.FindControl("lbl_gstamt");
            Label totalamt = (Label)row.FindControl("lbl_totalamount");
            string QTY = qty.Text.ToString();
            string FQTY = fqty.Text.ToString();
            string PRICE = price.Text.ToString();
            string DISCAMT = discamt.Text.ToString();
            string AMOUNT = amt.Text.ToString();
            string GSTAMT = gstamt.Text.ToString();
            string TOTALAMT = totalamt.Text.ToString();

            dt.Rows[index].Delete();
            ViewState["ITEM"] = dt;
            lbltotalprice.Text = (Convert.ToDecimal(lbltotalprice.Text) - ((Convert.ToDecimal(QTY) - Convert.ToDecimal(FQTY)) * Convert.ToDecimal(PRICE))).ToString();
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
    public void clearfield()
    {
        txtamount.Text = "0";
        txtbatchno.Text = "0";
        txttotalamount.Text = "0";
        txtcgst.Text = txtSgst.Text=txtIGST.Text= "0";
        txtdisc.Text = "0";
        txtdiscamt.Text="0";
        txtexpirydate.Text = "";
        txtfeeqty.Text = "0";
        txtgstamount.Text = "0";
        txtgstno.Text = "";
        txthsncode.Text = "0";
        txtinvdate.Text = "";
        txtinvoiceno.Text = "";
        txtname.Text = "";
        txtopening.Text = "0";
        txtpprice.Text = "0";
        txtstatecode.Text = "";
        lbldiscamt.Text = "0";
        lblgrandtotal.Text = lblgstamt.Text = lbltotalamt.Text = lbltotalprice.Text = "0";
        grvStudentDetails.DataSource = null;
        grvStudentDetails.DataBind();
        btncreate.Visible = true; 
        btndelete.Visible = false;
        btnupdate.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (dropparty.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Party Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtinvoiceno.Text == "")
            {
                string message = "alert('* Invoice Number Is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtinvdate.Text == "")
            {
                string message = "alert('* Invoice Date Is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (grvStudentDetails.Rows.Count <= 0)
            {

                string message = "alert('*Add item to purchase.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            int chkedcounter = 0;
            int correctinput = 0;
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[16];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@PNAME", SqlDbType.VarChar, 500, dropparty.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@STATECODE", SqlDbType.VarChar, 500, txtstatecode.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@GSTNO", SqlDbType.VarChar, 500, txtgstno.Text);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@INVOICENO", SqlDbType.VarChar, 500, txtinvoiceno.Text.ToString());
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@INVDATE", SqlDbType.Date, 0, Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@TOTALPRICE", SqlDbType.VarChar, 500, lbltotalprice.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@TOTALDISCAMT", SqlDbType.VarChar, 500, lbldiscamt.Text);
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@TOTALAMT", SqlDbType.VarChar, 500, lbltotalamt.Text);
            SQL_PARAMS[14] = OBJ_METHOD.createParams("@GSTAMT", SqlDbType.VarChar, 500, lblgstamt.Text);
            SQL_PARAMS[15] = OBJ_METHOD.createParams("@GT", SqlDbType.VarChar, 500, lblgrandtotal.Text);


            OBJ_METHOD.ExecuteProceedure("phrmc_PurchasPurInsUp", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                DataTable dt1 = ViewState["ITEM"] as DataTable;
                GridView1.DataSource = dt1;
                GridView1.DataBind();

                foreach (GridViewRow gv1 in GridView1.Rows)
                {

                    chkedcounter++;
                    SQL_PARAMS = new SqlParameter[20];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd"));//invIt.Text
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@COMPANY", SqlDbType.VarChar, 500, gv1.Cells[0].Text);
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, gv1.Cells[1].Text);
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, gv1.Cells[4].Text);
                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@PUNIT", SqlDbType.VarChar, 500, gv1.Cells[6].Text);

                    SQL_PARAMS[10] = OBJ_METHOD.createParams("@SUNIT", SqlDbType.VarChar, 500, "0");
                    SQL_PARAMS[11] = OBJ_METHOD.createParams("@PPRICE", SqlDbType.Decimal, 0, gv1.Cells[7].Text);
                    SQL_PARAMS[12] = OBJ_METHOD.createParams("@SPRICE", SqlDbType.VarChar, 500, "0");
                    SQL_PARAMS[13] = OBJ_METHOD.createParams("@EXP", SqlDbType.VarChar, 500, gv1.Cells[5].Text);
                    SQL_PARAMS[14] = OBJ_METHOD.createParams("@OPENING", SqlDbType.VarChar, 500, "0");
                    SQL_PARAMS[15] = OBJ_METHOD.createParams("@PURCHASE", SqlDbType.VarChar, 500, gv1.Cells[8].Text);

                    SQL_PARAMS[16] = OBJ_METHOD.createParams("@PRETURN", SqlDbType.VarChar, 500, "0");
                    SQL_PARAMS[17] = OBJ_METHOD.createParams("@SALE", SqlDbType.VarChar, 500, "0");
                    SQL_PARAMS[18] = OBJ_METHOD.createParams("@SRETURN", SqlDbType.VarChar, 500, "0");
                    SQL_PARAMS[19] = OBJ_METHOD.createParams("@CLOSEING", SqlDbType.VarChar, 500, "0");

                    OBJ_METHOD.ExecuteProceedure("purchase_Tran", "", "", SqlDbType.VarChar, SQL_PARAMS, true);
                    
                    if (OBJ_METHOD._RESULT > 0)
                    {
                        SQL_PARAMS = new SqlParameter[10];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);//invIt.Text
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, TXTID.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@COMPANY", SqlDbType.VarChar, 500, gv1.Cells[0].Text);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, gv1.Cells[1].Text);
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@EXP", SqlDbType.VarChar, 500, gv1.Cells[5].Text);
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                        SQL_PARAMS[8] = OBJ_METHOD.createParams("@QTY", SqlDbType.VarChar, 500, gv1.Cells[8].Text);
                        SQL_PARAMS[9] = OBJ_METHOD.createParams("@BATCHNO", SqlDbType.VarChar, 500, gv1.Cells[3].Text);

                        OBJ_METHOD.ExecuteProceedure("PUR_STOCK_TABLE", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                        if (OBJ_METHOD._RESULT > 0)
                        {
                            SQL_PARAMS = new SqlParameter[23];

                            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);//invIt.Text
                            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
                            SQL_PARAMS[3] = OBJ_METHOD.createParams("@COMPANY", SqlDbType.VarChar, 500, gv1.Cells[0].Text);
                            SQL_PARAMS[4] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, gv1.Cells[1].Text);
                            SQL_PARAMS[5] = OBJ_METHOD.createParams("@HSNCODE", SqlDbType.VarChar, 500, gv1.Cells[2].Text);
                            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                            SQL_PARAMS[8] = OBJ_METHOD.createParams("@BATCHNO", SqlDbType.VarChar, 500, gv1.Cells[3].Text);
                            SQL_PARAMS[9] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, gv1.Cells[4].Text);

                            SQL_PARAMS[10] = OBJ_METHOD.createParams("@EXPDATE", SqlDbType.VarChar, 500, gv1.Cells[5].Text);
                            SQL_PARAMS[11] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, gv1.Cells[6].Text);
                            SQL_PARAMS[12] = OBJ_METHOD.createParams("@PRICE", SqlDbType.VarChar, 500, gv1.Cells[7].Text);
                            SQL_PARAMS[13] = OBJ_METHOD.createParams("@QTY", SqlDbType.VarChar, 500, gv1.Cells[8].Text);
                            SQL_PARAMS[14] = OBJ_METHOD.createParams("@FQTY", SqlDbType.VarChar, 500, gv1.Cells[9].Text);
                            SQL_PARAMS[15] = OBJ_METHOD.createParams("@DISC", SqlDbType.VarChar, 500, gv1.Cells[10].Text);

                            SQL_PARAMS[16] = OBJ_METHOD.createParams("@DISCAMT", SqlDbType.VarChar, 500, gv1.Cells[11].Text);
                            SQL_PARAMS[17] = OBJ_METHOD.createParams("@AMOUNT", SqlDbType.VarChar, 500, gv1.Cells[12].Text);
                            SQL_PARAMS[18] = OBJ_METHOD.createParams("@CGST", SqlDbType.VarChar, 500, gv1.Cells[13].Text);
                            SQL_PARAMS[19] = OBJ_METHOD.createParams("@SGST", SqlDbType.VarChar, 500, gv1.Cells[14].Text);
                            SQL_PARAMS[20] = OBJ_METHOD.createParams("@IGST", SqlDbType.VarChar, 500, gv1.Cells[15].Text);
                            SQL_PARAMS[21] = OBJ_METHOD.createParams("@GSTAMT", SqlDbType.VarChar, 500, gv1.Cells[16].Text);
                            SQL_PARAMS[22] = OBJ_METHOD.createParams("@TOTALAMT", SqlDbType.VarChar, 500, gv1.Cells[17].Text);

                            OBJ_METHOD.ExecuteProceedure("phrmc_PurchasPurchIns", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                           
                            if (OBJ_METHOD._RESULT > 0)
                            {
                                correctinput++;
                            }
                            else
                            {
                                OBJ_METHOD.commitOrRollbackTran("rollback");
                                message1 = "alert('Due to some issues, Data not saved.')";
                            }
                        }
                        else
                        {
                            OBJ_METHOD.commitOrRollbackTran("rollback");
                            message1 = "alert('Due to some issues, Data not saved.')";
                        }
                    }
                    else
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        message1 = "alert('Due to some issues, Data not saved.')";
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
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");

                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
            #region old code
            //using (SqlCommand stock_cmd = new SqlCommand("phrmc_PurchasPurInsUp", con))
            //{
            //    stock_cmd.CommandType = CommandType.StoredProcedure;
            //    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //    stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //    stock_cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = dropparty.Text;
            //    stock_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtstatecode.Text;
            //    stock_cmd.Parameters.Add("@GSTNO", SqlDbType.VarChar).Value = txtgstno.Text;
            //    stock_cmd.Parameters.Add("@INVOICENO", SqlDbType.VarChar).Value = txtinvoiceno.Text;
            //    stock_cmd.Parameters.Add("@INVDATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd");
            //    stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
            //    stock_cmd.Parameters.Add("@TOTALDISCAMT", SqlDbType.VarChar).Value = lbldiscamt.Text;
            //    stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = lbltotalamt.Text;
            //    stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = lblgstamt.Text;
            //    stock_cmd.Parameters.Add("@GT", SqlDbType.VarChar).Value = lblgrandtotal.Text;
            //    stock_cmd.ExecuteNonQuery();
            //}
            //DataTable dt1 = ViewState["ITEM"] as DataTable;
            //GridView1.DataSource = dt1;
            //GridView1.DataBind();

            //foreach (GridViewRow gv1 in GridView1.Rows)
            //{
            //    // SqlCommand cmd2 = new SqlCommand("insert into PURCHASE_TABLE (ORGID,ID, COMPANY,NAME,HSNCODE,BATCHNO,CATEGORY,EXPDATE,UNIT,PRICE,QTY,FQTY,DISC,DISCAMT,AMOUNT,CGST,SGST,IGST,GSTAMT,TOTALAMT) values (@ORGID,@ID, @COMPANY,@NAME,@HSNCODE,@BATCHNO,@CATEGORY,@EXPDATE,@UNIT,@PRICE,@QTY,@FQTY,@DISC,@DISCAMT,@AMOUNT,@CGST,@SGST,@IGST,@GSTAMT,@TOTALAMT)", con);
            //    using (SqlCommand cmd2 = new SqlCommand("phrmc_PurchasPurchIns", con))
            //    {
            //        cmd2.CommandType = CommandType.StoredProcedure;
            //        cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //        cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //        cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //        cmd2.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
            //        cmd2.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
            //        cmd2.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = gv1.Cells[2].Text;
            //        cmd2.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
            //        cmd2.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
            //        cmd2.Parameters.Add("@EXPDATE", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
            //        cmd2.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
            //        cmd2.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = gv1.Cells[7].Text;
            //        cmd2.Parameters.Add("@QTY", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
            //        cmd2.Parameters.Add("@FQTY", SqlDbType.VarChar).Value = gv1.Cells[9].Text;
            //        cmd2.Parameters.Add("@DISC", SqlDbType.VarChar).Value = gv1.Cells[10].Text;
            //        cmd2.Parameters.Add("@DISCAMT", SqlDbType.VarChar).Value = gv1.Cells[11].Text;
            //        cmd2.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = gv1.Cells[12].Text;
            //        cmd2.Parameters.Add("@CGST", SqlDbType.VarChar).Value = gv1.Cells[13].Text;
            //        cmd2.Parameters.Add("@SGST", SqlDbType.VarChar).Value = gv1.Cells[14].Text;
            //        cmd2.Parameters.Add("@IGST", SqlDbType.VarChar).Value = gv1.Cells[15].Text;
            //        cmd2.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = gv1.Cells[16].Text;
            //        cmd2.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = gv1.Cells[17].Text;
            //        cmd2.ExecuteNonQuery();
            //    }

            //    //-----------------------------------
            //    using (SqlCommand stock_cmd = new SqlCommand("purchase_Tran", con))
            //    {
            //        stock_cmd.CommandType = CommandType.StoredProcedure;
            //        stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //        stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd");
            //        stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //        stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //        stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
            //        stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
            //        stock_cmd.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
            //        stock_cmd.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
            //        stock_cmd.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = gv1.Cells[7].Text;
            //        stock_cmd.Parameters.Add("@SPRICE", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
            //        stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@PURCHASE", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
            //        stock_cmd.Parameters.Add("@PRETURN", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@SALE", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@SRETURN", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@CLOSEING", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.ExecuteNonQuery();
            //    }
            //    using (SqlCommand stock_cmd = new SqlCommand("PUR_STOCK_TABLE", con))
            //    {
            //        stock_cmd.CommandType = CommandType.StoredProcedure;
            //        stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //        stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //        stock_cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = TXTID.Text;
            //        stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
            //        stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
            //        stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
            //        stock_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = gv1.Cells[8].Text;
            //        stock_cmd.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
            //        stock_cmd.ExecuteNonQuery();
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
    #region Insert code
    //public void STOCK_TABLE()
    //{
    //    try
    //    {
    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //        con.Open();
    //        // SqlCommand stock_cmd = new SqlCommand("insert into PUR_TABLE (FYEAR,ORGID,ID,PNAME,STATECODE,GSTNO,INVOICENO,INVDATE,TOTALPRICE,TOTALDISCAMT,TOTALAMT,GSTAMT,GT) values (@FYEAR,@ORGID,@ID,@PNAME,@STATECODE,@GSTNO,@INVOICENO,@INVDATE,@TOTALPRICE,@TOTALDISCAMT,@TOTALAMT,@GSTAMT,@GT)", con);
    //        using (SqlCommand stock_cmd = new SqlCommand("phrmc_PurchasPurInsUp", con))
    //        {
    //            stock_cmd.CommandType = CommandType.StoredProcedure;
    //            stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
    //            stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
    //            stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
    //            stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
    //            stock_cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = dropparty.Text;
    //            stock_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtstatecode.Text;
    //            stock_cmd.Parameters.Add("@GSTNO", SqlDbType.VarChar).Value = txtgstno.Text;
    //            stock_cmd.Parameters.Add("@INVOICENO", SqlDbType.VarChar).Value = txtinvoiceno.Text;
    //            stock_cmd.Parameters.Add("@INVDATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd");
    //            stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
    //            stock_cmd.Parameters.Add("@TOTALDISCAMT", SqlDbType.VarChar).Value = lbldiscamt.Text;
    //            stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = lbltotalamt.Text;
    //            stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = lblgstamt.Text;
    //            stock_cmd.Parameters.Add("@GT", SqlDbType.VarChar).Value = lblgrandtotal.Text;
    //            stock_cmd.ExecuteNonQuery();
    //        }
    //        con.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine("An error occurred: '{0}'", ex);
    //    }
    //}
    //public void STOCK_TRAN()
    //{
    //    try
    //    {
    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //        con.Open();
    //        DataTable dt1 = ViewState["ITEM"] as DataTable;
    //        GridView1.DataSource = dt1;
    //        GridView1.DataBind();

    //        foreach (GridViewRow gv1 in GridView1.Rows)
    //        {
    //            // SqlCommand cmd2 = new SqlCommand("insert into PURCHASE_TABLE (ORGID,ID, COMPANY,NAME,HSNCODE,BATCHNO,CATEGORY,EXPDATE,UNIT,PRICE,QTY,FQTY,DISC,DISCAMT,AMOUNT,CGST,SGST,IGST,GSTAMT,TOTALAMT) values (@ORGID,@ID, @COMPANY,@NAME,@HSNCODE,@BATCHNO,@CATEGORY,@EXPDATE,@UNIT,@PRICE,@QTY,@FQTY,@DISC,@DISCAMT,@AMOUNT,@CGST,@SGST,@IGST,@GSTAMT,@TOTALAMT)", con);
    //            using (SqlCommand cmd2 = new SqlCommand("phrmc_PurchasPurchIns", con))
    //            {
    //                cmd2.CommandType = CommandType.StoredProcedure;
    //                cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
    //                cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
    //                cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
    //                cmd2.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
    //                cmd2.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
    //                cmd2.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = gv1.Cells[2].Text;
    //                cmd2.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
    //                cmd2.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
    //                cmd2.Parameters.Add("@EXPDATE", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
    //                cmd2.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
    //                cmd2.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = gv1.Cells[7].Text;
    //                cmd2.Parameters.Add("@QTY", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
    //                cmd2.Parameters.Add("@FQTY", SqlDbType.VarChar).Value = gv1.Cells[9].Text;
    //                cmd2.Parameters.Add("@DISC", SqlDbType.VarChar).Value = gv1.Cells[10].Text;
    //                cmd2.Parameters.Add("@DISCAMT", SqlDbType.VarChar).Value = gv1.Cells[11].Text;
    //                cmd2.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = gv1.Cells[12].Text;
    //                cmd2.Parameters.Add("@CGST", SqlDbType.VarChar).Value = gv1.Cells[13].Text;
    //                cmd2.Parameters.Add("@SGST", SqlDbType.VarChar).Value = gv1.Cells[14].Text;
    //                cmd2.Parameters.Add("@IGST", SqlDbType.VarChar).Value = gv1.Cells[15].Text;
    //                cmd2.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = gv1.Cells[16].Text;
    //                cmd2.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = gv1.Cells[17].Text;
    //                cmd2.ExecuteNonQuery();
    //            }

    //            //-----------------------------------
    //            using (SqlCommand stock_cmd = new SqlCommand("purchase_Tran", con))
    //            {
    //                stock_cmd.CommandType = CommandType.StoredProcedure;
    //                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
    //                stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd");
    //                stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
    //                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
    //                stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
    //                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
    //                stock_cmd.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
    //                stock_cmd.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
    //                stock_cmd.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = gv1.Cells[7].Text;
    //                stock_cmd.Parameters.Add("@SPRICE", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
    //                stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@PURCHASE", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
    //                stock_cmd.Parameters.Add("@PRETURN", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@SALE", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@SRETURN", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@CLOSEING", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.ExecuteNonQuery();
    //            }
    //            using (SqlCommand stock_cmd = new SqlCommand("PUR_STOCK_TABLE", con))
    //            {
    //                stock_cmd.CommandType = CommandType.StoredProcedure;
    //                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
    //                stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
    //                stock_cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = TXTID.Text;
    //                stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
    //                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
    //                stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
    //                stock_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = gv1.Cells[8].Text;
    //                stock_cmd.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
    //                stock_cmd.ExecuteNonQuery();
    //            }
    //        }
    //        con.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine("An error occurred: '{0}'", ex);
    //    }

    //}
    #endregion
    #region Update Code
    //public void STOCK_TABLE_UPDATE()
    //{
    //    try
    //    {
    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //        con.Open();

    //        // SqlCommand stock_cmd = new SqlCommand("UPDATE PUR_TABLE SET PNAME=@PNAME,STATECODE=@STATECODE,GSTNO=@GSTNO,INVOICENO=@INVOICENO,INVDATE=@INVDATE,TOTALPRICE=@TOTALPRICE,TOTALDISCAMT=@TOTALDISCAMT,TOTALAMT=@TOTALAMT,GSTAMT=@GSTAMT,GT=@GT WHERE ID=@ID", con);
    //        using (SqlCommand stock_cmd = new SqlCommand("phrmc_PurchasPurInsUp", con))
    //        {
    //            stock_cmd.CommandType = CommandType.StoredProcedure;
    //            stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
    //            stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
    //            stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
    //            stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
    //            stock_cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = dropparty.Text;
    //            stock_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtstatecode.Text;
    //            stock_cmd.Parameters.Add("@GSTNO", SqlDbType.VarChar).Value = txtgstno.Text;
    //            stock_cmd.Parameters.Add("@INVOICENO", SqlDbType.VarChar).Value = txtinvoiceno.Text;
    //            stock_cmd.Parameters.Add("@INVDATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd");
    //            stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
    //            stock_cmd.Parameters.Add("@TOTALDISCAMT", SqlDbType.VarChar).Value = lbldiscamt.Text;
    //            stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = lbltotalamt.Text;
    //            stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = lblgstamt.Text;
    //            stock_cmd.Parameters.Add("@GT", SqlDbType.VarChar).Value = lblgrandtotal.Text;
    //            stock_cmd.ExecuteNonQuery();
    //        }
    //        con.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine("An error occurred: '{0}'", ex);
    //    }
    //}
    //public void STOCK_TRAN_UPDATE()
    //{
    //    try
    //    {
    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //        con.Open();
    //        SqlDataAdapter da = new SqlDataAdapter("delete from PURCHASE_TABLE where id='" + TXTID.Text + "'", con);
    //        DataSet d = new DataSet();
    //        da.Fill(d);
    //        foreach (GridViewRow gv1 in GridView1.Rows)
    //        {
    //            using (SqlCommand stock_cmd = new SqlCommand("purchase_Tran", con))
    //            {
    //                stock_cmd.CommandType = CommandType.StoredProcedure;
    //                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
    //                stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd");
    //                stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
    //                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
    //                stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
    //                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
    //                stock_cmd.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
    //                stock_cmd.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
    //                stock_cmd.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = gv1.Cells[7].Text;
    //                stock_cmd.Parameters.Add("@SPRICE", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
    //                stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@PURCHASE", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
    //                stock_cmd.Parameters.Add("@PRETURN", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@SALE", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@SRETURN", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@CLOSEING", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.ExecuteNonQuery();
    //            }

    //            using (SqlCommand stock_cmd = new SqlCommand("PUR_STOCK_TABLE", con))
    //            {
    //                stock_cmd.CommandType = CommandType.StoredProcedure;
    //                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
    //                stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
    //                stock_cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = TXTID.Text;
    //                stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
    //                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
    //                stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
    //                stock_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = gv1.Cells[8].Text;
    //                stock_cmd.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
    //                stock_cmd.ExecuteNonQuery();
    //            }
    //        }
    //        DataTable dt1 = ViewState["ITEM"] as DataTable;
    //        GridView1.DataSource = dt1;
    //        GridView1.DataBind();
    //        foreach (GridViewRow gv1 in GridView1.Rows)
    //        {
    //            using (SqlCommand cmd2 = new SqlCommand("phrmc_PurchasPurchIns", con))
    //            {
    //                cmd2.CommandType = CommandType.StoredProcedure;
    //                cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
    //                cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
    //                cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
    //                cmd2.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
    //                cmd2.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
    //                cmd2.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = gv1.Cells[2].Text;
    //                cmd2.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
    //                cmd2.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
    //                cmd2.Parameters.Add("@EXPDATE", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
    //                cmd2.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
    //                cmd2.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = gv1.Cells[7].Text;
    //                cmd2.Parameters.Add("@QTY", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
    //                cmd2.Parameters.Add("@FQTY", SqlDbType.VarChar).Value = gv1.Cells[9].Text;
    //                cmd2.Parameters.Add("@DISC", SqlDbType.VarChar).Value = gv1.Cells[10].Text;
    //                cmd2.Parameters.Add("@DISCAMT", SqlDbType.VarChar).Value = gv1.Cells[11].Text;
    //                cmd2.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = gv1.Cells[12].Text;
    //                cmd2.Parameters.Add("@CGST", SqlDbType.VarChar).Value = gv1.Cells[13].Text;
    //                cmd2.Parameters.Add("@SGST", SqlDbType.VarChar).Value = gv1.Cells[14].Text;
    //                cmd2.Parameters.Add("@IGST", SqlDbType.VarChar).Value = gv1.Cells[15].Text;
    //                cmd2.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = gv1.Cells[16].Text;
    //                cmd2.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = gv1.Cells[17].Text;
    //                cmd2.ExecuteNonQuery();
    //            }

    //            using (SqlCommand stock_cmd = new SqlCommand("purchase_Tran", con))
    //            {
    //                stock_cmd.CommandType = CommandType.StoredProcedure;
    //                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
    //                stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd");
    //                stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
    //                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
    //                stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
    //                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
    //                stock_cmd.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
    //                stock_cmd.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
    //                stock_cmd.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = gv1.Cells[7].Text;
    //                stock_cmd.Parameters.Add("@SPRICE", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
    //                stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@PURCHASE", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
    //                stock_cmd.Parameters.Add("@PRETURN", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@SALE", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@SRETURN", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@CLOSEING", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.ExecuteNonQuery();
    //            }

    //            using (SqlCommand stock_cmd = new SqlCommand("PUR_STOCK_TABLE", con))
    //            {
    //                stock_cmd.CommandType = CommandType.StoredProcedure;
    //                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
    //                stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
    //                stock_cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = TXTID.Text;
    //                stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
    //                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
    //                stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
    //                stock_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = gv1.Cells[8].Text;
    //                stock_cmd.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
    //                stock_cmd.ExecuteNonQuery();

    //            }
    //        }
    //        con.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine("An error occurred: '{0}'", ex);
    //    }
    //}
    #endregion
    #region Delete code
    //public void STOCK_TABLE_DEL()
    //{
    //    try
    //    {
    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //        con.Open();
    //        using (SqlCommand cmd = new SqlCommand("phrmc_PurchaseSelDel", con))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELPUR";
    //            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
    //            cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
    //            cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
    //            cmd.ExecuteNonQuery();
    //        }
    //        con.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine("An error occurred: '{0}'", ex);
    //    }
    //}
    //public void STOCK_TRAN_DEL()
    //{
    //    try
    //    {
    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //        con.Open();
    //        DataTable dt1 = ViewState["ITEM"] as DataTable;
    //        GridView1.DataSource = dt1;
    //        GridView1.DataBind();
    //        //SqlDataAdapter da = new SqlDataAdapter("delete from PURCHASE_TABLE where ID='" + TXTID.Text + "'", con);
    //        using (SqlCommand cmd = new SqlCommand("phrmc_PurchaseSelDel", con))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELTPURCHASE";
    //            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
    //            cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
    //            cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
    //            cmd.ExecuteNonQuery();
    //        }
    //        foreach (GridViewRow gv1 in GridView1.Rows)
    //        {
    //            using (SqlCommand stock_cmd = new SqlCommand("purchase_Tran", con))
    //            {
    //                stock_cmd.CommandType = CommandType.StoredProcedure;
    //                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
    //                stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = txtinvdate.Text;
    //                stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
    //                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
    //                stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
    //                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
    //                stock_cmd.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
    //                stock_cmd.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
    //                stock_cmd.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = gv1.Cells[7].Text;
    //                stock_cmd.Parameters.Add("@SPRICE", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
    //                stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@PURCHASE", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
    //                stock_cmd.Parameters.Add("@PRETURN", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@SALE", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@SRETURN", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.Parameters.Add("@CLOSEING", SqlDbType.VarChar).Value = "0";
    //                stock_cmd.ExecuteNonQuery();
    //            }

    //            using (SqlCommand stock_cmd = new SqlCommand("PUR_STOCK_TABLE", con))
    //            {
    //                stock_cmd.CommandType = CommandType.StoredProcedure;
    //                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
    //                stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
    //                stock_cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = TXTID.Text;
    //                stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
    //                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
    //                stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
    //                stock_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = gv1.Cells[8].Text;
    //                stock_cmd.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
    //                stock_cmd.ExecuteNonQuery();
    //            }
    //        }
    //        con.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine("An error occurred: '{0}'", ex);
    //    }

    //}
    #endregion
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS = OBJ_METHOD.Get_DataSet("phrmc_PurchaseSelDel", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                btndelete.Visible = true;
                TXTID.Text = DS.Tables[0].Rows[0]["ID"].ToString();
                dropparty.Text = DS.Tables[0].Rows[0]["PNAME"].ToString();
                txtgstno.Text = DS.Tables[0].Rows[0]["GSTNO"].ToString();
                txtstatecode.Text = DS.Tables[0].Rows[0]["STATECODE"].ToString();
                txtinvoiceno.Text = DS.Tables[0].Rows[0]["INVOICENO"].ToString();
                txtinvdate.Text = DS.Tables[0].Rows[0]["INVDATE"].ToString();
                lbltotalprice.Text = DS.Tables[0].Rows[0]["TOTALPRICE"].ToString();
                lbldiscamt.Text = DS.Tables[0].Rows[0]["TOTALDISCAMT"].ToString();
                lbltotalamt.Text = DS.Tables[0].Rows[0]["TOTALAMT"].ToString();
                lblgstamt.Text = DS.Tables[0].Rows[0]["GSTAMT"].ToString();
                lblgrandtotal.Text = DS.Tables[0].Rows[0]["GT"].ToString();
            }

            SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECTITEM");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("phrmc_PurchaseSelDel", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                grvStudentDetails.DataSource = Ds.Tables["Table"];
                grvStudentDetails.DataBind();

                DataTable dt = Ds.Tables["Table"];
                ViewState["ITEM"] = dt;

                GridView1.DataSource = Ds.Tables["Table"];
                GridView1.DataBind();
            }
            #region old code
            //using (SqlCommand cmd = new SqlCommand("phrmc_PurchaseSelDel", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
            //    dr = cmd.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        btncreate.Visible = false;
            //        btnupdate.Visible = true;
            //        TXTID.Text = dr["ID"].ToString();
            //        dropparty.Text = dr["PNAME"].ToString();
            //        txtgstno.Text = dr["GSTNO"].ToString();
            //        txtstatecode.Text = dr["STATECODE"].ToString();
            //        txtinvoiceno.Text = dr["INVOICENO"].ToString();
            //        txtinvdate.Text = dr["INVDATE"].ToString();
            //        lbltotalprice.Text = dr["TOTALPRICE"].ToString();
            //        lbldiscamt.Text = dr["TOTALDISCAMT"].ToString();
            //        lbltotalamt.Text = dr["TOTALAMT"].ToString();
            //        lblgstamt.Text = dr["GSTAMT"].ToString();
            //        lblgrandtotal.Text = dr["GT"].ToString();
            //    }
            //}
            //dr.Close();
            ////------------------------------------
            ////da = new SqlDataAdapter("select COMPANY,NAME,HSNCODE,BATCHNO,CATEGORY,EXPDATE as EXP,UNIT,PRICE,QTY,FQTY,DISC,DISCAMT,AMOUNT,CGST,SGST,IGST,GSTAMT,TOTALAMT AS TOTALAMOUNT FROM PURCHASE_TABLE where ID='" + TXTID.Text + "' and ORGID='" + lblorgid.Text + "'", con);            
            //using (SqlCommand cmd1 = new SqlCommand("phrmc_PurchaseSelDel", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECTITEM";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //    cmd1.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;

            //    SqlDataAdapter da = new SqlDataAdapter(cmd1);
            //    DataSet ds2 = new DataSet();
            //    da.Fill(ds2);
            //    grvStudentDetails.DataSource = ds2.Tables["Table"];
            //    grvStudentDetails.DataBind();

            //    DataTable dt = ds2.Tables["Table"];
            //    ViewState["ITEM"] = dt;
            //}
            ////-----------------------------------         
            //// SqlDataAdapter da1 = new SqlDataAdapter("select COMPANY,NAME,HSNCODE,BATCHNO,CATEGORY,EXPDATE as EXP,UNIT,PRICE,QTY,FQTY,DISC,DISCAMT,AMOUNT,CGST,SGST,IGST,GSTAMT,TOTALAMT AS TOTALAMOUNT FROM PURCHASE_TABLE where ID='" + TXTID.Text + "' and ORGID='" + lblorgid.Text + "'", con);
            //using (SqlCommand cmd2 = new SqlCommand("phrmc_PurchaseSelDel", con))
            //{
            //    cmd2.CommandType = CommandType.StoredProcedure;
            //    cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECTITEM";
            //    cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //    cmd2.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
            //    cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;

            //    SqlDataAdapter da1 = new SqlDataAdapter(cmd2);

            //    DataSet ds3 = new DataSet();
            //    da1.Fill(ds3);
            //    GridView1.DataSource = ds3.Tables["Table"];
            //    GridView1.DataBind();
            //}
            //btndelete.Visible = true;
            //con.Close();
            #endregion
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
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("phrmc_PurchaseSelDel", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = Ds;
                GridView2.PageIndex = e.NewPageIndex;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
            }
            #region old code
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            ////SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,INVOICENO AS INVOICE,CONVERT(VARCHAR(10),INVDATE,105) AS DATE,PNAME AS PARTY FROM PUR_TABLE WHERE  ORGID='" + lblorgid.Text + "' AND FYEAR='" + lblfyear.Text + "' ORDER BY ID DESC", con);            
            //using (SqlCommand cmd = new SqlCommand("phrmc_PurchaseSelDel", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INDEXCHG";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    //  GridView2.SelectedIndex = 0;
            //    GridView2.DataSource = dt;
            //    GridView2.PageIndex = e.NewPageIndex;
            //    GridView2.DataKeyNames = new string[] { "ID" };
            //    GridView2.DataBind();
            //}
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (dropparty.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Party Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtinvoiceno.Text == "")
            {
                string message = "alert('* Invoice Number Is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtinvdate.Text == "")
            {
                string message = "alert('* Invoice Date Is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (grvStudentDetails.Rows.Count <= 0)
            {

                string message = "alert('*Add item to purchase.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            if (txtexpirydate.Text == "")
            {
                txtexpirydate.Text = "NULL";
            }
            int chkedcounter = 0;
            int correctinput = 0;
            SqlParameter[] SQL_PARAMS = new SqlParameter[14];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@PNAME", SqlDbType.VarChar, 500, dropparty.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@STATECODE", SqlDbType.VarChar, 500, txtstatecode.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@GSTNO", SqlDbType.VarChar, 500, txtgstno.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@INVOICENO", SqlDbType.VarChar, 500, txtinvoiceno.Text.ToString());
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@INVDATE", SqlDbType.Date, 0, Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@TOTALPRICE", SqlDbType.VarChar, 500, lbltotalprice.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@TOTALDISCAMT", SqlDbType.VarChar, 500, lbldiscamt.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@TOTALAMT", SqlDbType.VarChar, 500, lbltotalamt.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@GSTAMT", SqlDbType.VarChar, 500, lblgstamt.Text);
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@GT", SqlDbType.VarChar, 500, lblgrandtotal.Text);

            OBJ_METHOD.ExecuteProceedure("phrmc_PurchasPurInsUp", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
            
            if (OBJ_METHOD._RESULT > 0)
            {
                SQL_PARAMS = new SqlParameter[3];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELTPURCHASE");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                OBJ_METHOD.ExecuteProceedure("phrmc_PurchaseSelDel", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    DataTable dt1 = ViewState["ITEM"] as DataTable;
                    GridView1.DataSource = dt1;
                    GridView1.DataBind();

                    foreach (GridViewRow gv1 in GridView1.Rows)
                    {

                        chkedcounter++;
                        SQL_PARAMS = new SqlParameter[8];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd"));//invIt.Text
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@COMPANY", SqlDbType.VarChar, 500, gv1.Cells[0].Text);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, gv1.Cells[1].Text);
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@EXP", SqlDbType.VarChar, 500, gv1.Cells[5].Text);
                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@PURCHASE", SqlDbType.VarChar, 500, gv1.Cells[8].Text);

                        OBJ_METHOD.ExecuteProceedure("purchase_Tran", "", "", SqlDbType.VarChar, SQL_PARAMS, true);
                        
                        if (OBJ_METHOD._RESULT > 0)
                        {
                            SQL_PARAMS = new SqlParameter[8];

                            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                            SQL_PARAMS[2] = OBJ_METHOD.createParams("@COMPANY", SqlDbType.VarChar, 500, gv1.Cells[0].Text);
                            SQL_PARAMS[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, gv1.Cells[1].Text);
                            SQL_PARAMS[4] = OBJ_METHOD.createParams("@EXP", SqlDbType.VarChar, 500, gv1.Cells[5].Text);
                            SQL_PARAMS[5] = OBJ_METHOD.createParams("@QTY", SqlDbType.VarChar, 500, gv1.Cells[8].Text);
                            SQL_PARAMS[6] = OBJ_METHOD.createParams("@BATCHNO", SqlDbType.VarChar, 500, gv1.Cells[3].Text);
                            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

                            OBJ_METHOD.ExecuteProceedure("PUR_STOCK_TABLE", "", "", SqlDbType.VarChar, SQL_PARAMS, true);
                            
                            if (OBJ_METHOD._RESULT > 0)
                            {
                                SQL_PARAMS = new SqlParameter[20];

                                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                                SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd"));//invIt.Text
                                SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                                SQL_PARAMS[3] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
                                SQL_PARAMS[4] = OBJ_METHOD.createParams("@COMPANY", SqlDbType.VarChar, 500, gv1.Cells[0].Text);
                                SQL_PARAMS[5] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, gv1.Cells[1].Text);
                                SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                                SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                                SQL_PARAMS[8] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, gv1.Cells[4].Text);
                                SQL_PARAMS[9] = OBJ_METHOD.createParams("@PUNIT", SqlDbType.VarChar, 500, gv1.Cells[6].Text);

                                SQL_PARAMS[10] = OBJ_METHOD.createParams("@SUNIT", SqlDbType.VarChar, 500, "0");
                                SQL_PARAMS[11] = OBJ_METHOD.createParams("@PPRICE", SqlDbType.Decimal, 0, gv1.Cells[7].Text);
                                SQL_PARAMS[12] = OBJ_METHOD.createParams("@SPRICE", SqlDbType.VarChar, 500, "0");
                                SQL_PARAMS[13] = OBJ_METHOD.createParams("@EXP", SqlDbType.VarChar, 500, gv1.Cells[5].Text);
                                SQL_PARAMS[14] = OBJ_METHOD.createParams("@OPENING", SqlDbType.VarChar, 500, "0");
                                SQL_PARAMS[15] = OBJ_METHOD.createParams("@PURCHASE", SqlDbType.VarChar, 500, gv1.Cells[8].Text);

                                SQL_PARAMS[16] = OBJ_METHOD.createParams("@PRETURN", SqlDbType.VarChar, 500, "0");
                                SQL_PARAMS[17] = OBJ_METHOD.createParams("@SALE", SqlDbType.VarChar, 500, "0");
                                SQL_PARAMS[18] = OBJ_METHOD.createParams("@SRETURN", SqlDbType.VarChar, 500, "0");
                                SQL_PARAMS[19] = OBJ_METHOD.createParams("@CLOSEING", SqlDbType.VarChar, 500, "0");

                                OBJ_METHOD.ExecuteProceedure("purchase_Tran", "", "", SqlDbType.VarChar, SQL_PARAMS, true);
                                
                                if (OBJ_METHOD._RESULT > 0)
                                {
                                    SQL_PARAMS = new SqlParameter[10];

                                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);//invIt.Text
                                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, TXTID.Text);
                                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@COMPANY", SqlDbType.VarChar, 500, gv1.Cells[0].Text);
                                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, gv1.Cells[1].Text);
                                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@EXP", SqlDbType.VarChar, 500, gv1.Cells[5].Text);
                                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@QTY", SqlDbType.VarChar, 500, gv1.Cells[8].Text);
                                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@BATCHNO", SqlDbType.VarChar, 500, gv1.Cells[3].Text);

                                    OBJ_METHOD.ExecuteProceedure("PUR_STOCK_TABLE", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                                    if (OBJ_METHOD._RESULT > 0)
                                    {
                                        SQL_PARAMS = new SqlParameter[23];

                                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
                                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);//invIt.Text
                                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
                                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@COMPANY", SqlDbType.VarChar, 500, gv1.Cells[0].Text);
                                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, gv1.Cells[1].Text);
                                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@HSNCODE", SqlDbType.VarChar, 500, gv1.Cells[2].Text);
                                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                                        SQL_PARAMS[8] = OBJ_METHOD.createParams("@BATCHNO", SqlDbType.VarChar, 500, gv1.Cells[3].Text);
                                        SQL_PARAMS[9] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, gv1.Cells[4].Text);

                                        SQL_PARAMS[10] = OBJ_METHOD.createParams("@EXPDATE", SqlDbType.VarChar, 500, gv1.Cells[5].Text);
                                        SQL_PARAMS[11] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, gv1.Cells[6].Text);
                                        SQL_PARAMS[12] = OBJ_METHOD.createParams("@PRICE", SqlDbType.VarChar, 500, gv1.Cells[7].Text);
                                        SQL_PARAMS[13] = OBJ_METHOD.createParams("@QTY", SqlDbType.VarChar, 500, gv1.Cells[8].Text);
                                        SQL_PARAMS[14] = OBJ_METHOD.createParams("@FQTY", SqlDbType.VarChar, 500, gv1.Cells[9].Text);
                                        SQL_PARAMS[15] = OBJ_METHOD.createParams("@DISC", SqlDbType.VarChar, 500, gv1.Cells[10].Text);

                                        SQL_PARAMS[16] = OBJ_METHOD.createParams("@DISCAMT", SqlDbType.VarChar, 500, gv1.Cells[11].Text);
                                        SQL_PARAMS[17] = OBJ_METHOD.createParams("@AMOUNT", SqlDbType.VarChar, 500, gv1.Cells[12].Text);
                                        SQL_PARAMS[18] = OBJ_METHOD.createParams("@CGST", SqlDbType.VarChar, 500, gv1.Cells[13].Text);
                                        SQL_PARAMS[19] = OBJ_METHOD.createParams("@SGST", SqlDbType.VarChar, 500, gv1.Cells[14].Text);
                                        SQL_PARAMS[20] = OBJ_METHOD.createParams("@IGST", SqlDbType.VarChar, 500, gv1.Cells[15].Text);
                                        SQL_PARAMS[21] = OBJ_METHOD.createParams("@GSTAMT", SqlDbType.VarChar, 500, gv1.Cells[16].Text);
                                        SQL_PARAMS[22] = OBJ_METHOD.createParams("@TOTALAMT", SqlDbType.VarChar, 500, gv1.Cells[17].Text);

                                        OBJ_METHOD.ExecuteProceedure("phrmc_PurchasPurchIns", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);


                                        if (OBJ_METHOD._RESULT > 0)
                                        {
                                            correctinput++;
                                        }
                                        else
                                        {
                                            OBJ_METHOD.commitOrRollbackTran("rollback");
                                            message1 = "alert('Due to some issues, Data not updated.')";
                                        }
                                    }
                                    else
                                    {
                                        OBJ_METHOD.commitOrRollbackTran("rollback");
                                        message1 = "alert('Due to some issues, Data not updated.')";
                                    }
                                }
                                else
                                {
                                    OBJ_METHOD.commitOrRollbackTran("rollback");
                                    message1 = "alert('Due to some issues, Data not updated.')";
                                }
                            }
                            else
                            {
                                OBJ_METHOD.commitOrRollbackTran("rollback");
                                message1 = "alert('Due to some issues, Data not updated.')";
                            }
                        }
                        else
                        {
                            OBJ_METHOD.commitOrRollbackTran("rollback");
                            message1 = "alert('Due to some issues, Data not updated.')";
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
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");

                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
            #region old code
            //using (SqlCommand stock_cmd = new SqlCommand("phrmc_PurchasPurInsUp", con))
            //{
            //    stock_cmd.CommandType = CommandType.StoredProcedure;
            //    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //    stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //    stock_cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = dropparty.Text;
            //    stock_cmd.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtstatecode.Text;
            //    stock_cmd.Parameters.Add("@GSTNO", SqlDbType.VarChar).Value = txtgstno.Text;
            //    stock_cmd.Parameters.Add("@INVOICENO", SqlDbType.VarChar).Value = txtinvoiceno.Text;
            //    stock_cmd.Parameters.Add("@INVDATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd");
            //    stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
            //    stock_cmd.Parameters.Add("@TOTALDISCAMT", SqlDbType.VarChar).Value = lbldiscamt.Text;
            //    stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = lbltotalamt.Text;
            //    stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = lblgstamt.Text;
            //    stock_cmd.Parameters.Add("@GT", SqlDbType.VarChar).Value = lblgrandtotal.Text;
            //    stock_cmd.ExecuteNonQuery();
            //}
            //SqlDataAdapter da = new SqlDataAdapter("delete from PURCHASE_TABLE where id='" + TXTID.Text + "'", con);
            //DataSet d = new DataSet();
            //da.Fill(d);
            //foreach (GridViewRow gv1 in GridView1.Rows)
            //{
            //    using (SqlCommand stock_cmd = new SqlCommand("purchase_Tran", con))
            //    {
            //        stock_cmd.CommandType = CommandType.StoredProcedure;
            //        stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //        stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd");
            //        stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //        stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //        stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
            //        stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
            //        stock_cmd.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
            //        stock_cmd.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
            //        stock_cmd.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = gv1.Cells[7].Text;
            //        stock_cmd.Parameters.Add("@SPRICE", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
            //        stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@PURCHASE", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
            //        stock_cmd.Parameters.Add("@PRETURN", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@SALE", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@SRETURN", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@CLOSEING", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.ExecuteNonQuery();
            //    }

            //    using (SqlCommand stock_cmd = new SqlCommand("PUR_STOCK_TABLE", con))
            //    {
            //        stock_cmd.CommandType = CommandType.StoredProcedure;
            //        stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //        stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //        stock_cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = TXTID.Text;
            //        stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
            //        stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
            //        stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
            //        stock_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = gv1.Cells[8].Text;
            //        stock_cmd.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
            //        stock_cmd.ExecuteNonQuery();
            //    }
            //}
            //DataTable dt1 = ViewState["ITEM"] as DataTable;
            //GridView1.DataSource = dt1;
            //GridView1.DataBind();
            //foreach (GridViewRow gv1 in GridView1.Rows)
            //{
            //    using (SqlCommand cmd2 = new SqlCommand("phrmc_PurchasPurchIns", con))
            //    {
            //        cmd2.CommandType = CommandType.StoredProcedure;
            //        cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //        cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //        cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //        cmd2.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
            //        cmd2.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
            //        cmd2.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = gv1.Cells[2].Text;
            //        cmd2.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
            //        cmd2.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
            //        cmd2.Parameters.Add("@EXPDATE", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
            //        cmd2.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
            //        cmd2.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = gv1.Cells[7].Text;
            //        cmd2.Parameters.Add("@QTY", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
            //        cmd2.Parameters.Add("@FQTY", SqlDbType.VarChar).Value = gv1.Cells[9].Text;
            //        cmd2.Parameters.Add("@DISC", SqlDbType.VarChar).Value = gv1.Cells[10].Text;
            //        cmd2.Parameters.Add("@DISCAMT", SqlDbType.VarChar).Value = gv1.Cells[11].Text;
            //        cmd2.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = gv1.Cells[12].Text;
            //        cmd2.Parameters.Add("@CGST", SqlDbType.VarChar).Value = gv1.Cells[13].Text;
            //        cmd2.Parameters.Add("@SGST", SqlDbType.VarChar).Value = gv1.Cells[14].Text;
            //        cmd2.Parameters.Add("@IGST", SqlDbType.VarChar).Value = gv1.Cells[15].Text;
            //        cmd2.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = gv1.Cells[16].Text;
            //        cmd2.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = gv1.Cells[17].Text;
            //        cmd2.ExecuteNonQuery();
            //    }

            //    using (SqlCommand stock_cmd = new SqlCommand("purchase_Tran", con))
            //    {
            //        stock_cmd.CommandType = CommandType.StoredProcedure;
            //        stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //        stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd");
            //        stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //        stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //        stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
            //        stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
            //        stock_cmd.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
            //        stock_cmd.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
            //        stock_cmd.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = gv1.Cells[7].Text;
            //        stock_cmd.Parameters.Add("@SPRICE", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
            //        stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@PURCHASE", SqlDbType.VarChar).Value = gv1.Cells[8].Text;
            //        stock_cmd.Parameters.Add("@PRETURN", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@SALE", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@SRETURN", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.Parameters.Add("@CLOSEING", SqlDbType.VarChar).Value = "0";
            //        stock_cmd.ExecuteNonQuery();
            //    }

            //    using (SqlCommand stock_cmd = new SqlCommand("PUR_STOCK_TABLE", con))
            //    {
            //        stock_cmd.CommandType = CommandType.StoredProcedure;
            //        stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //        stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //        stock_cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = TXTID.Text;
            //        stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
            //        stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
            //        stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
            //        stock_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = gv1.Cells[8].Text;
            //        stock_cmd.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
            //        stock_cmd.ExecuteNonQuery();

            //    }
            //}
            //STOCK_TRAN_UPDATE();
            //con.Close();
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
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            binddata();
            puritemGrid();
            clearfield();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Btndelete_Click(object sender, EventArgs e)
    {
        string message1=string.Empty;
        try
        {
            int chkedcounter = 0;
            int correctinput = 0;
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);

            OBJ_METHOD.ExecuteProceedure("phrmc_PurchasPurInsUp", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                
                DataTable dt1 = ViewState["ITEM"] as DataTable;
                GridView1.DataSource = dt1;
                GridView1.DataBind();

                foreach (GridViewRow gv1 in GridView1.Rows)
                {

                    chkedcounter++;
                    SQL_PARAMS = new SqlParameter[8];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd"));//invIt.Text
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@COMPANY", SqlDbType.VarChar, 500, gv1.Cells[0].Text);
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, gv1.Cells[1].Text);
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@EXP", SqlDbType.VarChar, 500, gv1.Cells[5].Text);
                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@PURCHASE", SqlDbType.VarChar, 500, gv1.Cells[8].Text);

                    OBJ_METHOD.ExecuteProceedure("purchase_Tran", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        SQL_PARAMS = new SqlParameter[8];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@COMPANY", SqlDbType.VarChar, 500, gv1.Cells[0].Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, gv1.Cells[1].Text);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@EXP", SqlDbType.VarChar, 500, gv1.Cells[5].Text);
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@QTY", SqlDbType.VarChar, 500, gv1.Cells[8].Text);
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@BATCHNO", SqlDbType.VarChar, 500, gv1.Cells[3].Text);
                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);
                        OBJ_METHOD.ExecuteProceedure("PUR_STOCK_TABLE", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                        if (OBJ_METHOD._RESULT > 0)
                        {
                            correctinput++;
                        }
                        else
                        {
                            OBJ_METHOD.commitOrRollbackTran("rollback");
                            message1 = "alert('Due to some issues, Data not deleted.')";
                        }
                    }
                    else
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        message1 = "alert('Due to some issues, Data not deleted.')";
                    }
                    
                }
                
                if (chkedcounter == correctinput && chkedcounter > 0)
                {
                    SQL_PARAMS = new SqlParameter[2];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);

                    OBJ_METHOD.ExecuteProceedure("phrmc_PurchasPurchIns", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("commit");
                        message1 = "alert('" + OBJ_METHOD._objOut + "')";
                        binddata();
                        clearfield();
                    }
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Error occurred while processing data... Rolling back...')";
                }
            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }

        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not deleted.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void txtbatchno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = OBJ_METHOD.Get_DataSet("select COMPANY, HSNCODE,NAME,BATCHNO,PPRICE,GST,GST/2 AS GST2 from ITEM_TABLE where BATCHNO='" + txtbatchno.Text + "' AND ORGID='" + lblorgid.Text + "'", false, false);
            if (ds.Tables[0].Rows.Count > 0)
            {
                clearinercontrol();
                txtcomp.Text = ds.Tables[0].Rows[0]["COMPANY"].ToString();
                txthsncode.Text = ds.Tables[0].Rows[0]["HSNCODE"].ToString();
                txtname.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                txtbatchno.Text = ds.Tables[0].Rows[0]["BATCHNO"].ToString();
                txtpprice.Text = ds.Tables[0].Rows[0]["PPRICE"].ToString();
                if (txtstatecode.Text == txtcstatecode.Text)
                {
                    txtcgst.Text = ds.Tables[0].Rows[0]["GST2"].ToString();
                    txtSgst.Text = ds.Tables[0].Rows[0]["GST2"].ToString();
                }
                else
                {
                    txtIGST.Text = ds.Tables[0].Rows[0]["GST"].ToString();
                }
                DataSet ds1 = OBJ_METHOD.Get_DataSet("select CATEGORY FROM ITEM_TABLE where NAME='" + txtname.Text + "' AND ORGID='" + lblorgid.Text + "'", false, false);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    dropcate.DataSource = ds1;
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
                        txtdiscamt.Text = Math.Round((Convert.ToDouble(txtopening.Text)) - (Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtdisc.Text)) / 100).ToString();
                        txtamount.Text = Math.Round((Convert.ToDouble(txtopening.Text) - (Convert.ToDouble(txtfeeqty.Text)) * Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))).ToString();

                        if (txtstatecode.Text == txtcstatecode.Text)
                        {
                            txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) - (Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
                        }
                        else
                        {
                            txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) - (Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * (Convert.ToDouble(txtIGST.Text))) / 100).ToString();
                        }

                        txttotalamount.Text = Math.Round((Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtgstamount.Text))).ToString();
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                }
            }
            else
            {
                string message = "alert('* Incorrect Batchno.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            #region old code
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            ////clearinercontrol();
            //SqlCommand com = new SqlCommand("select COMPANY, HSNCODE,NAME,BATCHNO,PPRICE,GST,GST/2 AS GST2 from ITEM_TABLE where BATCHNO='" + txtbatchno.Text + "' AND ORGID='" + lblorgid.Text + "'", con);
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    clearinercontrol();
            //    txtcomp.Text = dr["COMPANY"].ToString();
            //    txthsncode.Text = dr["HSNCODE"].ToString();
            //    txtname.Text = dr["NAME"].ToString();
            //    txtbatchno.Text = dr["BATCHNO"].ToString();
            //    txtpprice.Text = dr["PPRICE"].ToString();
            //    if (txtstatecode.Text == txtcstatecode.Text)
            //    {
            //        txtcgst.Text = dr["GST2"].ToString();
            //        txtSgst.Text = dr["GST2"].ToString();
            //    }
            //    else
            //    {
            //        txtIGST.Text = dr["GST"].ToString();
            //    }

            //    dr.Close();
            //    da = new SqlDataAdapter("select CATEGORY FROM ITEM_TABLE where NAME='" + txtname.Text + "' AND ORGID='" + lblorgid.Text + "'", con);
            //    DataTable ds = new DataTable();
            //    da.Fill(ds);
            //    dropcate.DataSource = ds;
            //    dropcate.DataTextField = "CATEGORY";
            //    dropcate.DataValueField = "CATEGORY";
            //    dropcate.DataBind();
            //    try
            //    {
            //        dis = 0;
            //        dism = 0;
            //        totalamt1 = 0;
            //        totamt = 0;
            //        totalgstamt = 0;
            //        txtdiscamt.Text = Math.Round((Convert.ToDouble(txtopening.Text)) - (Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtdisc.Text)) / 100).ToString();
            //        txtamount.Text = Math.Round((Convert.ToDouble(txtopening.Text) - (Convert.ToDouble(txtfeeqty.Text)) * Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))).ToString();

            //        if (txtstatecode.Text == txtcstatecode.Text)
            //        {
            //            txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) - (Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
            //        }
            //        else
            //        {
            //            txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) - (Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) - (Convert.ToDouble(txtdiscamt.Text))) * (Convert.ToDouble(txtIGST.Text))) / 100).ToString();
            //        }

            //        txttotalamount.Text = Math.Round((Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtgstamount.Text))).ToString();
            //    }
            //    catch (Exception ex)
            //    {
            //        Response.Write(ex.Message);
            //    }

            //}

            //else
            //{
            //    dr.Close();
            //    string message = "alert('* Incorrect Batchno.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
}