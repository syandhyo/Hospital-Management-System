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
public partial class PHARMACYSTORE_USER_PharmacyIndent_View : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9, cmd10, cmd11, cmd12, cmd13, cmd14, cmd15, cmd16;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    int i, no, no1, sl;
    int flag = 0, stock = 0, stock_tran = 0, trans = 0, stock1 = 0, S, CLOSEING, slno, CLOSE;
    string id, id1, ph, val1, val2, p, q, des1, name, k, PIN;
    public string id_hist;
    public double amt = 0, qty = 0, amt1 = 0, t_amt = 0, qt = 0, c_rs = 0, avg = 0;
    decimal a, b, c, d, e, f, g, h, j, F;
    DateTime DT;
    double totalamt1, totamt, totalgstamt, dis, dism;
    GridViewRow gr;
    [WebMethod]
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand COM = new SqlCommand("SELECT FYEAR FROM FYEAR1_TABLE WHERE  ORGID='" + lblorgid.Text + "'", con);
        dr = COM.ExecuteReader();
        if (dr.Read())
        {
            lblfyear.Text = dr["FYEAR"].ToString();
        }
        dr.Close();
        SqlDataAdapter da = new SqlDataAdapter("SELECT A.ID AS ID,A.DATE AS DATE,A.PID AS PID,A.PNAME AS PNAME,A.BEDNO AS BEDNO,A.WARD AS WARD,B.NAME AS ITEMNAME,B.QTY AS QTY FROM MIND_TABLE A,MINDENT_TABLE B WHERE A.ID=B.ID AND  A.ORGID='"+lblorgid.Text+"' AND A.ID='"+lblindentno.Text+"'", con);
        DataSet dt = new DataSet();
        da.Fill(dt);
        if (dt.Tables[0].Rows.Count>0)
        {
            lblpid.Text = dt.Tables[0].Rows[0]["PID"].ToString();
            lblname.Text = dt.Tables[0].Rows[0]["PNAME"].ToString();
            lblward.Text = dt.Tables[0].Rows[0]["WARD"].ToString();
            lbldate.Text = dt.Tables[0].Rows[0]["DATE"].ToString();
            lblbedno.Text = dt.Tables[0].Rows[0]["BEDNO"].ToString();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT A.ID AS ID,A.DATE AS DATE,A.PID AS PID,A.PNAME AS PNAME,A.BEDNO AS BEDNO,A.WARD AS WARD,B.NAME AS ITEMNAME,B.QTY AS QTY FROM OTMIND_TABLE A,OTMINDENT_TABLE B WHERE A.ID=B.ID AND  A.ORGID='" + lblorgid.Text + "' AND A.ID='" + lblindentno.Text + "'", con);
            DataSet dt1 = new DataSet();
            da1.Fill(dt1);
            lblpid.Text = dt1.Tables[0].Rows[0]["PID"].ToString();
            lblname.Text = dt1.Tables[0].Rows[0]["PNAME"].ToString();
            lblward.Text = dt1.Tables[0].Rows[0]["WARD"].ToString();
            lbldate.Text = dt1.Tables[0].Rows[0]["DATE"].ToString();
            lblbedno.Text = dt1.Tables[0].Rows[0]["BEDNO"].ToString();
            GridView1.DataSource = dt1;
            GridView1.DataBind();
        }
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
        lblindentno.Text = Session["INDID"].ToString();
        binddata();
        con.Close();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand stock_cmd = new SqlCommand("update MEDICINE_STATUS_TABLE set STATUS=@STATUS where ID=@ID AND ORGID=@ORGID", con);
        stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblindentno.Text;
        stock_cmd.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = "Declined";
        stock_cmd.ExecuteNonQuery();
        SqlCommand cmd1 = new SqlCommand("update MIND_TABLE set STATUS=@STATUS where ID=@ID AND ORGID=@ORGID", con);
        cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblindentno.Text;
        cmd1.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = "Declined";
        cmd1.ExecuteNonQuery();
        SqlCommand cmd2 = new SqlCommand("update OTMIND_TABLE set STATUS=@STATUS where ID=@ID AND ORGID=@ORGID", con);
        cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblindentno.Text;
        cmd2.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = "Declined";
        cmd2.ExecuteNonQuery();
        con.Close();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Session["PAID"]=lblpid.Text;
        Response.Redirect("~/PHARMACYSTORE/USER/IndSale.aspx");
    }
}