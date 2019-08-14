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

public partial class STOREKEEPER_Nonpobill : System.Web.UI.Page
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
            using (SqlCommand cmd = new SqlCommand("STORE_NONPOBILL", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_VENDOR_CONTRACT";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = prefix;
                //cmd.CommandText = "select DISTINCT CONTRACTID from VENDOR_COTRACT_TBL where CONTRACTID like @SearchText+'%'";
                //cmd.Parameters.AddWithValue("@SearchText", prefix);
                //cmd.Connection = con;

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["CONTRACTID"]));
                    }
                }
                con.Close();
            }
        }
        return customers.ToArray();
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
        using (SqlCommand cmd = new SqlCommand("STORE_NONPOBILL", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_VENDOR_MASERT";

            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
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
            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[11] { new DataColumn("NAME"), new DataColumn("HSNCODE"), new DataColumn("UNIT"), new DataColumn("PRICE"), new DataColumn("QTY"), new DataColumn("AMOUNT"), new DataColumn("CGST"), new DataColumn("SGST"), new DataColumn("IGST"), new DataColumn("GSTAMT"), new DataColumn("TOTALAMOUNT") });
            //ViewState["ITEM"] = dt;
            //this.BindGrid();
            binddata();
        }
        con.Close();
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtcontno.Text == "")
            {
                string message = "alert('Please!! Put The Contract No To View..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //  SqlCommand COM1 = new SqlCommand("select a.CONTRACTID,a.TOTPRICE,a.TOTGSTAMT,a.GRANDTOT,a.DATE,b.NAME1,b.STATECODE,b.ID from VENDOR_COTRACT_TBL a,VENDER_MASTER_TABLE b where a.VENDERNAME=b.ID and a.CONTRACTID='" + txtcontno.Text + "'", con);
            using (SqlCommand cmd = new SqlCommand("STORE_NONPOBILL", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_VENDOR_MASERT";

                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtcontno.Text;
                cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
                //da1 = new SqlDataAdapter(cmd);
                //SqlCommand COM1 = new SqlCommand("select a.CONTRACTID,a.TOTPRICE,a.TOTGSTAMT,a.GRANDTOT,a.DATE,b.NAME1,b.STATECODE,b.ID,c.ITEM as NAME,c.PRICE,c.QTY,c.UNIT, c.TOTAMT,c.AMOUNT,c.CGST,c.SGST,c.IGST,c.GSTAMT,c.CHKSEL,c.HSNCODE from VENDOR_COTRACT_TBL a,VENDER_MASTER_TABLE b, VENDOR_COTRACT_ITEM_TBL c  where a.VENDERNAME=b.ID and a.CONTRACTID=c.CONTRACTID and a.CONTRACTID='" + txtcontno.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dropVendor.SelectedValue = dr["ID"].ToString();
                    txtstatecode.Text = dr["STATECODE"].ToString();
                    txtcontno.Text = dr["CONTRACTID"].ToString();
                    txtcontdate.Text = dr["DATE"].ToString();
                    lbltotalprice.Text = dr["TOTPRICE"].ToString();
                    lblgstamt.Text = dr["TOTGSTAMT"].ToString();
                    lblgrandtotal.Text = dr["GRANDTOT"].ToString();
                    dr.Close();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    // DataTable dt = (DataTable)ViewState["ITEM"];
                    DataTable dt = new DataTable();
                    // dt.Clear();
                    da.Fill(dt);
                    grdnonpo.DataSource = dt;
                    grdnonpo.DataBind();
                    //ViewState["ITEM"] = dt;
                    //this.BindGrid();

                }
                else
                {
                    string message = "alert('* Incorrect ContractNO.')";
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
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grdnonpo.Rows)
            {
                // CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);
                Label nameIt = (row.Cells[2].FindControl("lbl_name") as Label);
                Label hsncodIt = (row.Cells[3].FindControl("lbl_hsn") as Label);

                Label unitIt = (row.Cells[4].FindControl("lbl_unit") as Label);
                Label priceIt = (row.Cells[5].FindControl("lbl_price") as Label);
                Label quantyIt = (row.Cells[6].FindControl("lbl_qty") as Label);

                Label amtIt = (row.Cells[7].FindControl("lbl_amount") as Label);
                Label cgstIt = (row.Cells[8].FindControl("lbl_cgst") as Label);
                Label sgstIt = (row.Cells[9].FindControl("lbl_sgst") as Label);
                Label igstIt = (row.Cells[10].FindControl("lbl_igst") as Label);
                Label gstamtIt = (row.Cells[11].FindControl("lbl_gstamt") as Label);
                Label totamtIt = (row.Cells[12].FindControl("lbl_totalamount") as Label);

                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[1].FindControl("CheckBox1") as CheckBox);

                    if (chkRow.Checked)
                    {
                        //var price = row.FindControl("txt_price") as TextBox;
                        var gstamt = row.FindControl("lbl_gstamt") as Label;
                        var totalamt = row.FindControl("lbl_totalamount") as Label;
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
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/STOREKEEPER/Nonpobill.aspx");
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {

    }
}