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
public partial class STOREKEEPER_store_POView : System.Web.UI.Page
{
    string num1 = "0";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9;
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
        lblPOView.Text = Session["Viewpo"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {
            viewPo();
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
            using (SqlCommand COM = new SqlCommand("STORE_PO_VIEW", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ORG";
                COM.Parameters.Add("@PONO", SqlDbType.VarChar).Value = "";
                da1 = new SqlDataAdapter(COM);


                //da1 = new SqlDataAdapter("select NAME1,ID FROM VENDER_MASTER_TABLE ", con);
                DataTable ds1 = new DataTable();
                da1.Fill(ds1);
                dropVendor.DataSource = ds1;
                dropVendor.DataTextField = "NAME1";
                dropVendor.DataValueField = "ID";
                dropVendor.DataBind();
                dropVendor.Items.Insert(0, "Please Select");
            }

            using (SqlCommand COM1 = new SqlCommand("STORE_PO_VIEW", con))
            {
                COM1.CommandType = CommandType.StoredProcedure;
                COM1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ORG";
                COM1.Parameters.Add("@PONO", SqlDbType.VarChar).Value = "";
                //SqlCommand COM1 = new SqlCommand("SELECT * FROM ORG_TABLE", con);
                dr = COM1.ExecuteReader();
                if (dr.Read())
                {
                    //Label1.Text = dr["FYEAR"].ToString();
                    txtorgname.Text = dr["NAME"].ToString();
                    txtaddress.Text = dr["ADDRESS"].ToString();
                    txtgstin.Text = dr["GSTNO"].ToString();
                    txtpin.Text = "752055";
                    txtphone.Text = dr["PHONE"].ToString();
                    txtcity.Text = "BHUBANESWAR";
                    txtdstatecode.Text = dr["STATECODE"].ToString();
                    txtstate.Text = "ODISHA";

                }
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        con.Close();
    }
    public void viewPo()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand COM = new SqlCommand("STORE_PO_VIEW", con))
        {
            COM.CommandType = CommandType.StoredProcedure;
            COM.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PO";
            COM.Parameters.Add("@PONO", SqlDbType.VarChar).Value = lblPOView.Text.ToString();
            SqlDataAdapter da = new SqlDataAdapter(COM);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);

            grvpoView.DataSource = dt1;

            grvpoView.DataBind();

            dr = COM.ExecuteReader();
            if (dr.Read())
            {
                dropVendor.SelectedValue = dr["VENDOR"].ToString();
                txtstatecode.Text = dr["VSCODE"].ToString();
                txtpono.Text = dr["PONO"].ToString();
                txtdateofissue.Text = dr["DATEOFISSUE"].ToString();
                txtrefno.Text = dr["REFNO"].ToString();
                txtrefdate.Text = dr["REFDATE"].ToString();
                txtorgname.Text = dr["ORGNAME"].ToString();
                txtpin.Text = dr["PIN"].ToString();
                txtcity.Text = dr["CITY"].ToString();
                txtstate.Text = dr["STATE"].ToString();
                txtgstin.Text = dr["GSTIN"].ToString();
                txtdstatecode.Text = dr["OSTATECODE"].ToString();
                txtaddress.Text = dr["ADDRESS"].ToString();
                lbltotalprice.Text = dr["TOTALPRICE"].ToString();
                lblgstamt.Text = dr["GSTAMOUNT"].ToString();
                lblgrandtotal.Text = dr["GRANDTOTAL"].ToString();
                txt_term_condition.Text = dr["TC1"].ToString();
                txt_TEST_REPORT0.Text = dr["TC2"].ToString();
                txt_DELIVERY_TERM.Text = dr["TC3"].ToString();
                txt_INSPECTION0.Text = dr["TC4"].ToString();
                txt_DELIVERY_TIME.Text = dr["TC5"].ToString();
                txt_WARANTY0.Text = dr["TC6"].ToString();
                txt_FREIGHT.Text = dr["TC7"].ToString();
                txt_LICENCE_PERMIT0.Text = dr["TC8"].ToString();
                txt_make.Text = dr["TC9"].ToString();
                txt_PRICE_PAYMENT.Text = dr["TC10"].ToString();
                txt_PACKING_FORWARDING.Text = dr["TC11"].ToString();
                txt_ACCEPTANCE0.Text = dr["TC12"].ToString();
                txt_TRANSIT_INSURANCE.Text = dr["TC13"].ToString();
                txt_TERMINATION0.Text = dr["TC14"].ToString();
                txt_LOADING_UNLOADING.Text = dr["TC15"].ToString();
                txt_INSTALLATION_COMMI1.Text = dr["TC16"].ToString();
            }
            dr.Close();
        }
        con.Close();
    }
}