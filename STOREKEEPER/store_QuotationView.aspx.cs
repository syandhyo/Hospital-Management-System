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

public partial class STOREKEEPER_store_QuotationView : System.Web.UI.Page
{
    string num1 = "0";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6;
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
        lblQotation.Text = Session["Viwpo"].ToString();
        // lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {
            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[11] { new DataColumn("HSN"), new DataColumn("NAME"), new DataColumn("UNIT"), new DataColumn("PRICE"), new DataColumn("QTY"), new DataColumn("AMOUNT"), new DataColumn("CGST"), new DataColumn("SGST"), new DataColumn("IGST"), new DataColumn("GSTAMT"), new DataColumn("TOTALAMOUNT") });
            //ViewState["ITEM"] = dt;
            //this.BindGrid();
            viewQuotion();
            binddata();

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
        //---------------------------------------------------------------
        using (SqlCommand COM1 = new SqlCommand("STORE_QUATATION_VIEW", con))
        {
            COM1.CommandType = CommandType.StoredProcedure;
            COM1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_VENDOR";
            COM1.Parameters.Add("@QUTO", SqlDbType.VarChar).Value = "";
            da1 = new SqlDataAdapter(COM1);
            //da1 = new SqlDataAdapter("select NAME1,ID FROM VENDER_MASTER_TABLE ", con);
            DataTable ds1 = new DataTable();
            da1.Fill(ds1);
            ddvendrNm.DataSource = ds1;
            ddvendrNm.DataTextField = "NAME1";
            ddvendrNm.DataValueField = "ID";
            ddvendrNm.DataBind();
            ddvendrNm.Items.Insert(0, "Please Select");
        }
        con.Close();
    }
    public void viewQuotion()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand COM = new SqlCommand("STORE_QUATATION_VIEW", con))
        {
            COM.CommandType = CommandType.StoredProcedure;
            COM.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            COM.Parameters.Add("@QUTO", SqlDbType.VarChar).Value = lblQotation.Text;
            SqlDataAdapter da = new SqlDataAdapter(COM);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);

            grvquotion.DataSource = dt1;

            grvquotion.DataBind();

            dr = COM.ExecuteReader();
            if (dr.Read())
            {
                // dropVendor.SelectedValue = dr["VENDOR"].ToString();
                txtdate.Text = dr["REFFDATE"].ToString();
                txtRfqNo.Text = dr["RFQNO"].ToString();
                txtQuotNo.Text = dr["QUTIONNO"].ToString();
                txtQuotDate.Text = dr["QUOTIONDATE"].ToString();
                ddvendrNm.SelectedValue = dr["VENDORNAME"].ToString();
                txtStatecd.Text = dr["STATECODE"].ToString();
                lbltotalprice.Text = dr["TOTPRICE"].ToString();

                lblgstamt.Text = dr["TOTGSTAMT"].ToString();
                lblgrandtotal.Text = dr["GRANDTOT"].ToString();

            }
            dr.Close();
        }
        con.Close();
    }
}