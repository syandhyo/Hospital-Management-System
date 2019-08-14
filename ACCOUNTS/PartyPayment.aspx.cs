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

public partial class ACCOUNTS_PartyPayment : System.Web.UI.Page
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
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //con.Open();
        //string qry1 = "select ID from PARTY_PAYMENT_TBL";

        //com = new SqlCommand(qry1, con);
        //dr = null;

        //dr = com.ExecuteReader();

        //while (dr.Read())
        //{
        //    num1 = dr["ID"].ToString();
        //}
        //num1 = string.Format("VN{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        //txtid.Text = num1;

        //dr.Close();
        //con.Close();
    }
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand COM = new SqlCommand("SELECT FYEAR FROM FYEAR1_TABLE WHERE  ORGID='" + lblorgid.Text + "'", con);
        dr = COM.ExecuteReader();
        if (dr.Read())
        {
            //Label1.Text = dr["FYEAR"].ToString();
            lblfyear.Text = dr["FYEAR"].ToString();
        }
        dr.Close();

        //SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM PARTY_PAYMENT_TBL WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //GridView1.SelectedIndex = 0;
        //GridView1.DataSource = dt;
        //GridView1.DataKeyNames = new string[] { "ID" };
        //GridView1.DataBind();



       
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
            txtcard.Visible = false;
            binddata();
            da = new SqlDataAdapter("select distinct NAME1,ID FROM VENDER_MASTER_TABLE", con);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dropvendor.DataSource = ds;
            dropvendor.DataTextField = "NAME1";
            dropvendor.DataValueField = "ID";
            dropvendor.DataBind();
            dropvendor.Items.Insert(0, "Please Select");            
        }
        con.Close();
    }

    protected void GVOnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lbl_slno") as Label).Text = (e.Row.RowIndex + 1).ToString();
        }
    }

  
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtprice.Text == "")
            {

                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand cmd1 = new SqlCommand("PARTY_PAYMENT", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = '1';
                cmd1.Parameters.Add("@VENDOR_ID", SqlDbType.VarChar).Value = dropvendor.Text;
                cmd1.Parameters.Add("@VOUCHER_NO", SqlDbType.VarChar).Value = txtvoucher.Text;
                cmd1.Parameters.Add("@MODE_OF_PAYMENT", SqlDbType.VarChar).Value = droppayment.Text;
                cmd1.Parameters.Add("@PNO", SqlDbType.VarChar).Value = txtcard.Text;
                cmd1.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = txtprice.Text;
                cmd1.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                //cmd1.Parameters.Add("@OPENING", SqlDbType.Decimal).Value = txtqty.Text;
                //cmd1.Parameters.Add("@PURCHES", SqlDbType.Decimal).Value = "0.00";
                //cmd1.Parameters.Add("@RETN", SqlDbType.Decimal).Value = "0.00";
                //cmd1.Parameters.Add("@ISSUE", SqlDbType.Decimal).Value = "0.00";
                //cmd1.Parameters.Add("@REF", SqlDbType.Decimal).Value = "0.00";
                //cmd1.Parameters.Add("@CLOSING", SqlDbType.Decimal).Value = txtqty.Text;


                cmd1.ExecuteNonQuery();
                string message = "alert('Successfully Inserted.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            }
            VENDOR_TRAN();
            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

        Response.Redirect("~/STOREKEEPER/PartyPayment.aspx");
    }

    public void VENDOR_TRAN()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand stock_cmd = new SqlCommand("VENDOR_DEBIT", con))
        {
            stock_cmd.CommandType = CommandType.StoredProcedure;
            stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            stock_cmd.Parameters.Add("@VENDOR_ID", SqlDbType.VarChar).Value = dropvendor.SelectedValue;
            stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0";
            stock_cmd.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = txtprice.Text;
            stock_cmd.Parameters.Add("@CLOSING", SqlDbType.VarChar).Value = "0.00";
            stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            stock_cmd.ExecuteNonQuery();
        }
        con.Close();
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //con.Open();
        //SqlCommand com = new SqlCommand("select ID,ISSUEDTO,RECBY,CONVERT TO(VARCHAR(10),MINDATE,105) AS INVDATE from MIN_TABLE where ID='" + slno + "'", con);
        //dr = com.ExecuteReader();
        //if (dr.Read())
        //{
        //    btncreate.Visible = false;
        //    btnupdate.Visible = true;
        //    txtminnumber.Text = dr["ID"].ToString();
        //    txtdate.Text = dr["INVDATE"].ToString();
        //    txtrecivedperson.Text = dr["RECBY"].ToString();
        //    dropissuedto.Text = dr["ISSUEDTO"].ToString();

        //}
        //dr.Close();
        //da = new SqlDataAdapter("select NAME,QTY,UNIT FROM MINITEM_TABLE where ID='" + txtminnumber.Text + "'", con);
        //DataSet ds2 = new DataSet();
        //da.Fill(ds2);
        //grvStudentDetails.DataSource = ds2.Tables["Table"];
        //grvStudentDetails.DataBind();

        //DataTable dt = ds2.Tables["Table"];
        //ViewState["ITEM"] = dt;

        //SqlDataAdapter da1 = new SqlDataAdapter("select NAME,QTY,UNIT FROM MINITEM_TABLE where ID='" + txtminnumber.Text + "'", con);
        //DataSet ds3 = new DataSet();
        //da1.Fill(ds3);
        //GridView1.DataSource = ds3.Tables["Table"];
        //GridView1.DataBind();
        //btndelete.Visible = true;
        //con.Close();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,CONVERT(VARCHAR(10),MINDATE,105) AS DATE FROM MIN_TABLE WHERE   FYEAR='" + lblfyear.Text + "' ORDER BY ID DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView2.SelectedIndex = 0;
            GridView2.DataSource = dt;
            GridView2.PageIndex = e.NewPageIndex;
            GridView2.DataKeyNames = new string[] { "ID" };
            GridView2.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void Btndelete_Click(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/STOREKEEPER/PartyPayment.aspx");
    }
    protected void droppayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (droppayment.SelectedIndex == 0)
        {
            txtcard.Visible = false;
            txtcard.Text = "";
        }
        else
        {
            txtcard.Visible = true;
            txtcard.Text = "";
        }


    }
}