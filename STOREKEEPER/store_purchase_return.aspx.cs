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


public partial class STOREKEEPER_store_purchase_return : System.Web.UI.Page
{
    string num1 = "0";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1;
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
                cmd.CommandText = "select DISTINCT NAME from MATERIAL_MASTER_TABLE where NAME like '%'+@SearchText+'%'";
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
        string qry1 = "select slno AS ID from MRN_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        //num1 = string.Format("PU{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtpono.Text = "MRN-" + num1 + 1 + "-" + lblfyear.Text;
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
        //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,CONVERT(VARCHAR(10),INVDATE,105) AS DATE,VENDOR AS VENDOR FROM PO_TABLE WHERE FYEAR='" + lblfyear.Text + "' ORDER BY ID DESC", con);
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
        con.Close();
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
        txtdateofissue.Text = DateTime.Now.ToString("dd-MM-yyyy");
        if (!IsPostBack)
        {
            binddata();
            bindItem_Grd();
        }
        con.Close();
    }
    public void bindItem_Grd()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[11] { new DataColumn("NAME"), new DataColumn("HSNCODE"), new DataColumn("UNIT"), new DataColumn("PRICE"), new DataColumn("QTY"), new DataColumn("AMOUNT"), new DataColumn("CGST"), new DataColumn("SGST"), new DataColumn("IGST"), new DataColumn("GSTAMT"), new DataColumn("TOTALAMOUNT") });
        ViewState["ITEM"] = dt;
        this.BindGrid();
    }
    protected void BindGrid()
    {
        try
        {
            grvStudentDetails.DataSource = (DataTable)ViewState["ITEM"];
            grvStudentDetails.DataBind();

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
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
            if (txtstatecode.Text == txtdstatecode.Text)
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

                if (txtstatecode.Text == txtdstatecode.Text)
                {
                    txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text))) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
                }
                else
                {
                    txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtIGST.Text)))) / 100).ToString();
                }

                txttotalamount.Text = Math.Round((Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtgstamount.Text))).ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
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
        try
        {

            SqlCommand com = new SqlCommand("select * from VENDER_MASTER_TABLE where ID='" + dropVendor.SelectedValue + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                txtstatecode.Text = dr["STATECODE"].ToString();
            }
            dr.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
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
            dt.Rows.Add(txtitemname.Text.Trim(), txthsncode.Text.Trim(), txtunit.Text.Trim(), txtpprice.Text.Trim(), txtopening.Text.Trim(), txtamount.Text.Trim(), txtcgst.Text.Trim(), txtSgst.Text.Trim(), txtIGST.Text.Trim(), txtgstamount.Text.Trim(), txttotalamount.Text.Trim());
            ViewState["ITEM"] = dt;
            this.BindGrid();
            lbltotalprice.Text = (Convert.ToDecimal(txtpprice.Text) * (Convert.ToDecimal(txtopening.Text)) + Convert.ToDecimal(lbltotalprice.Text)).ToString();

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
        using (SqlCommand stock_cmd = new SqlCommand("MRN_OPERATION", con))
        {
            stock_cmd.CommandType = CommandType.StoredProcedure;
            stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            stock_cmd.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
            stock_cmd.Parameters.Add("@VSCODE", SqlDbType.VarChar).Value = txtstatecode.Text;
            stock_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
            stock_cmd.Parameters.Add("@DATEOFISSUE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateofissue.Text).ToString("yyyy-MM-dd");
            stock_cmd.Parameters.Add("@OSTATECODE", SqlDbType.VarChar).Value = txtdstatecode.Text;
            stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
            stock_cmd.Parameters.Add("@GSTAMOUNT", SqlDbType.VarChar).Value = lblgstamt.Text;
            stock_cmd.Parameters.Add("@GRANDTOTAL", SqlDbType.VarChar).Value = lblgrandtotal.Text;
            stock_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "";
            stock_cmd.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = "";
            stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
            stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = "";
            stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "";
            stock_cmd.Parameters.Add("@AMOUNT", SqlDbType.VarChar).Value = "";
            stock_cmd.Parameters.Add("@CGST", SqlDbType.VarChar).Value = "";
            stock_cmd.Parameters.Add("@SGST", SqlDbType.VarChar).Value = "";
            stock_cmd.Parameters.Add("@IGST", SqlDbType.VarChar).Value = "";
            stock_cmd.Parameters.Add("@GSTAMT", SqlDbType.VarChar).Value = "";
            stock_cmd.Parameters.Add("@TOTALAMT", SqlDbType.VarChar).Value = "";
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
            using (SqlCommand stock_cmd = new SqlCommand("MRN_OPERATION", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@VENDOR", SqlDbType.VarChar).Value = dropVendor.SelectedValue;
                stock_cmd.Parameters.Add("@VSCODE", SqlDbType.VarChar).Value = txtstatecode.Text;
                stock_cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = txtpono.Text;
                stock_cmd.Parameters.Add("@DATEOFISSUE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateofissue.Text).ToString("yyyy-MM-dd");
                stock_cmd.Parameters.Add("@OSTATECODE", SqlDbType.VarChar).Value = txtdstatecode.Text;
                stock_cmd.Parameters.Add("@TOTALPRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
                stock_cmd.Parameters.Add("@GSTAMOUNT", SqlDbType.VarChar).Value = lblgstamt.Text;
                stock_cmd.Parameters.Add("@GRANDTOTAL", SqlDbType.VarChar).Value = lblgrandtotal.Text;
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
            using (SqlCommand stock_cmd = new SqlCommand("STORE_MRN_Tran_VENDOR", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateofissue.Text).ToString("yyyy-MM-dd");
                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@PURCHES", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@RETN", SqlDbType.VarChar).Value = gv1.Cells[4].Text; ;
                stock_cmd.Parameters.Add("@ISSUE", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@REF", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@CLOSING", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.ExecuteNonQuery();
            }

        }
        //}
        //catch { }
        con.Close();
    }
    protected void Btncreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropVendor.SelectedIndex == 0)
            {
                string message = "alert('* Select Vendor.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtdateofissue.Text == "")
            {
                string message = "alert('* Select MRN Date.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            //else if (grvStudentDetails.Rows.Count <= 0)
            //{

            //    string message = "alert('*Add item to Save.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            //SqlCommand cm = new SqlCommand("select * from MRN_TABLE where PONO ='" + txtpono.Text + "'", con);
            //dr = cm.ExecuteReader();
            //if (dr.Read())
            //{
            //    string message = "alert('*Cant enter duplicate MRN number.MRN number alredy exist.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            auto();
            dr.Close();
            STOCK_TABLE();
            STOCK_TRAN();
            Response.Write("<script LANGUAGE='JavaScript' >alert('Save Successful')</script>");
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/STOREKEEPER/MRN.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void Btndelete_Click(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/STOREKEEPER/MRN.aspx");
    }


    protected void txtopening_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        if (txtopening.Text == "0")
        {
            //string message = "alert('')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        else
        {
            SqlCommand com = new SqlCommand("select NAME,HSNCODE,PRICE,GST,GST/2 AS GST2,UNIT from MATERIAL_MASTER_TABLE where NAME='" + txtitemname.Text + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                // clearinercontrol();
                txthsncode.Text = dr["HSNCODE"].ToString();
                txtitemname.Text = dr["NAME"].ToString();
                txtpprice.Text = dr["PRICE"].ToString();
                txtunit.Text = dr["UNIT"].ToString();
                if (txtstatecode.Text == txtdstatecode.Text)
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

                    if (txtstatecode.Text == txtdstatecode.Text)
                    {
                        txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text))) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
                    }
                    else
                    {
                        txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtIGST.Text)))) / 100).ToString();
                    }

                    txttotalamount.Text = Math.Round((Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtgstamount.Text))).ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: '{0}'", ex);
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
    }
}