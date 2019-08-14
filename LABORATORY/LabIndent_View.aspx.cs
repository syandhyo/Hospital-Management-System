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
public partial class LABORATORY_LabIndent_View : System.Web.UI.Page
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
    string pid, id1, ph, val1, val2, p, q, des1, name, k, PIN;
    public string id_hist;
    public double amt = 0, qty = 0, amt1 = 0, t_amt = 0, qt = 0, c_rs = 0, avg = 0;
    decimal amount = 0;
    DateTime DT;
    double totalamt1, totamt, totalgstamt, dis, dism;
    GridViewRow gr;
    [WebMethod]
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from LABRES_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("LT{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        TXTID.Text = num1;

        dr.Close();
        con.Close();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton img = (ImageButton)e.Row.FindControl("img_user");
            if (e.Row.Cells[4].Text == "Waiting")
            {

                img.ImageUrl = "~/gimg/Waiting.png";
            }
            else if (e.Row.Cells[4].Text == "Finished")
            {

                img.ImageUrl = "~/gimg/Finished.png";
            }
            else if (e.Row.Cells[4].Text == "Declined")
            {

                img.ImageUrl = "~/gimg/Declined.png";
            }

        }
        con.Close();

    }
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
        SqlCommand com = new SqlCommand("select * from LABIND_TABLE WHERE ID='" + Session["LINDID"].ToString() + "' AND ORGID='" + lblorgid.Text + "'", con);
        dr = com.ExecuteReader();
            if(dr.Read())
            {
                pid = dr["PID"].ToString();
                lblpname.Text=dr["PNAME"].ToString();
                lblbedno.Text=dr["BEDNO"].ToString();
                lblward.Text=dr["WARD"].ToString();
            }
        dr.Close();
       
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
        lbluid.Text = Session["UID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        txtinvdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        binddata();
        try
        {
            if (!IsPostBack)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                SqlDataAdapter da = new SqlDataAdapter("SELECT A.INV AS ID,B.INV AS INV,B.PRICE AS PRICE FROM LABIND_TABLE_1 A, TEST_COMPONENT_TABLE B WHERE A.INDID='" + Session["LINDID"].ToString() + "' AND B.slno=A.INV AND A.IsSelected='True'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = null;
                GridView1.DataSource = dt;
                GridView1.DataBind();

                ViewState["CurrentTable"] = dt;
                SqlDataAdapter da1 = new SqlDataAdapter("SELECT A.ID as ID,A.DATE AS DATE,A.PID AS PID,A.TESTTYPE AS TESTTYPE,A.TESTNAME AS TESTNAME FROM LABRES_TABLE A  WHERE A.LINDID='" + Session["LINDID"].ToString() + "' AND A.ORGID='" + lblorgid.Text + "'ORDER BY A.ID DESC", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                GridView2.SelectedIndex = 0;
                GridView2.DataSource = dt1;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        con.Close();

    }
    protected void dropbedno_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //con.Open();
        //SqlDataAdapter da = new SqlDataAdapter("SELECT B.ID AS ID,B.NAME AS NAME FROM TEST_NAME_TABLE B WHERE B.CATEGORY='" + droptesttype.SelectedValue.ToString() + "' AND B.ORGID='" + lblorgid.Text + "'", con);
        //DataTable ds1 = new DataTable();
        //da.Fill(ds1);
        //droptestname.DataSource = ds1;
        //droptestname.DataTextField = "NAME";
        //droptestname.DataValueField = "ID";
        //droptestname.DataBind();
        //droptestname.Items.Insert(0, "-----Select-----");
        //con.Close();
    }
    protected void droptestname_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //con.Open();
        //SqlDataAdapter da = new SqlDataAdapter("SELECT INV FROM TEST_COMPONENT_TABLE WHERE NAME='"+droptestname.SelectedItem.Text.ToString()+"' AND ORGID='"+lblorgid.Text+"'", con);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //GridView1.SelectedIndex = 0;
        //GridView1.DataSource = null;
        //GridView1.DataSource = dt;
        //GridView1.DataBind();
        //con.Close();
    }
    public void datatable_po_item1()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        SqlCommand cmd = new SqlCommand("insert into LABRES_TABLE (ID,LINDID,PID,ORGID,DATE,UNAME,UID,TYPE,PRICE)VALUES(@ID,@LINDID,@PID,@ORGID,@DATE,@UNAME,@UID,@TYPE,@PRICE)", con);
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;    
            cmd.Parameters.Add("@LINDID", SqlDbType.VarChar).Value = Session["LINDID"].ToString();
            cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = pid.ToString();
            cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtinvdate.Text;
            cmd.Parameters.Add("@UNAME", SqlDbType.VarChar).Value = lblid.Text;
            cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = lbluid.Text;
            cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = lblindetype.Text;
            cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = amount.ToString();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter("UPDATE LABIND_TABLE SET STATUS='Finished' WHERE ID='" + Session["LINDID"].ToString() + "'", con);
            DataTable ds = new DataTable();
            da.Fill(ds);
            con.Close();
        //}
        //catch { }
    }
    public void datatable_po_item()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
         
        foreach (GridViewRow row in GridView1.Rows)
        {
            var inv = row.FindControl("lblinv") as Label;
            var price = row.FindControl("lblprice") as Label;
            var value = row.FindControl("txt_value") as TextBox;
            amount = amount + Convert.ToDecimal(price.Text);
            var INV = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0]);
            SqlCommand stock_cmd = new SqlCommand("INSERT INTO LABRESULT_TABLE (ID,LINDID,INV,VALUE,PRICE) VALUES (@ID,@LINDID,@INV,@VALUE,@PRICE)", con);
                stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                stock_cmd.Parameters.Add("@LINDID", SqlDbType.VarChar).Value = Session["LINDID"].ToString();
                stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = INV.ToString();
                stock_cmd.Parameters.Add("@VALUE", SqlDbType.VarChar).Value = value.Text.ToString();
                stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = price.Text.ToString();
                stock_cmd.ExecuteNonQuery();
              
        }
        SqlCommand cmd = new SqlCommand("update LABIND_TABLE set STATUS=@STATUS where ID=@ID AND ORGID=@ORGID", con);
        cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = Session["LINDID"].ToString();
        cmd.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = "Finished";
        cmd.ExecuteNonQuery();
        SqlCommand C = new SqlCommand("INSERT INTO PA_TRANS(VOUCHERNO,DATETIME,PID,DESCRIPTION,CHARGES,CREDITS,MODE)VALUES(@VOUCHERNO,@DATETIME,@PID,@DESCRIPTION,@CHARGES,@CREDITS,@MODE)", con);
        C.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
        C.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
        C.Parameters.Add("@PID", SqlDbType.VarChar).Value = pid.ToString();
        C.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LABTEST CHARGE@";
        C.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = "0";
        C.Parameters.Add("@CREDITS", SqlDbType.VarChar).Value = amount.ToString();
        C.Parameters.Add("@MODE", SqlDbType.VarChar).Value = "CREDIT";
        C.ExecuteNonQuery();
        //}
        //catch (Exception ex)
        //{

        //}
        con.Close();
    }
    public void datatable_po_item1_UPDATE()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand("UPDATE LABRES_TABLE SET PRICE=@PRICE,UID=@UID,UNAME=@UNAME WHERE ID=@ID", con);
        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
        cmd.Parameters.Add("@LINDID", SqlDbType.VarChar).Value = Session["LINDID"].ToString();
        cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = pid.ToString();
        cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtinvdate.Text;
        cmd.Parameters.Add("@UNAME", SqlDbType.VarChar).Value = lblid.Text;
        cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = lbluid.Text;
        cmd.ExecuteNonQuery();
        con.Close();
        //}
        //catch { }
    }
    public void datatable_po_item_UPDATE()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        foreach (GridViewRow row in GridView3.Rows)
        {
            var inv = row.FindControl("lblinv0") as Label;
            var price = row.FindControl("lblprice0") as Label;
            var value = row.FindControl("txt_value0") as TextBox;
            amount = amount + Convert.ToDecimal(price.Text);
            var INV = Convert.ToInt32(GridView3.DataKeys[row.RowIndex].Values[0]);
            SqlCommand stock_cmd = new SqlCommand("UPDATE LABRESULT_TABLE SET PRICE=@PRICE,VALUE=@VALUE WHERE ID=@ID AND INV=@INV", con);
            stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            stock_cmd.Parameters.Add("@LINDID", SqlDbType.VarChar).Value = Session["LINDID"].ToString();
            stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = INV.ToString();
            stock_cmd.Parameters.Add("@VALUE", SqlDbType.VarChar).Value = value.Text.ToString();
            stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = price.Text.ToString();
            stock_cmd.ExecuteNonQuery();
        
           
        }
        //}
        //catch (Exception ex)
        //{

        //}
        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            datatable_po_item();
            datatable_po_item1();
            Session["LABID"] = TXTID.Text;
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/LABORATORY/Bill.aspx");
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT A.ID as ID,A.DATE AS DATE,A.PID AS PID,A.TESTTYPE AS TESTTYPE,A.TESTNAME AS TESTNAME FROM LABRES_TABLE A  WHERE A.LINDID='" + Session["LINDID"].ToString() + "' A.ORGID='" + lblorgid.Text + "'ORDER BY A.ID DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView2.SelectedIndex = 0;
            GridView2.DataSource = dt;
            GridView2.PageIndex = e.NewPageIndex;
            GridView2.DataKeyNames = new string[] { "ID" };
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand("select B.BEDNO AS BEDNO,B.PNAME AS PNAME,B.WARD AS WARD,A.TESTTYPE AS TESTTYPE,A.TESTNAME AS TESTNAME from LABRES_TABLE A,LABIND_TABLE B where A.LINDID=B.ID AND A.ID='" + slno + "'", con);
        dr = com.ExecuteReader();
        if (dr.Read())
        {
            btncreate.Visible = false;
            btnupdate.Visible = true;
            TXTID.Text = slno.ToString();
            lblbedno.Text = dr["BEDNO"].ToString();
            lblpname.Text = dr["PNAME"].ToString();
            lblward.Text = dr["WARD"].ToString();
        }
        dr.Close();
        SqlDataAdapter da = new SqlDataAdapter("SELECT A.INV AS ID,B.INV AS INV,A.VALUE AS VALUE,B.PRICE AS PRICE FROM LABRESULT_TABLE A,TEST_COMPONENT_TABLE B WHERE A.INV=B.slno AND A.ID='" + TXTID.Text + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView3.SelectedIndex = 0;
        GridView3.DataSource = null;
        GridView3.DataSource = dt;
        GridView3.DataBind();
        btndelete.Visible = true;
        con.Close();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            datatable_po_item_UPDATE();
            datatable_po_item1_UPDATE();
            con.Close();
            Session["LABID"] = TXTID.Text;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

        Response.Redirect("~/LABORATORY/Bill.aspx");
    }
    protected void Btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("delete from LABRESULT_TABLE where ID='" + TXTID.Text + "'", con);
            DataSet d = new DataSet();
            da.Fill(d);
            SqlDataAdapter da1 = new SqlDataAdapter("delete from LABRES_TABLE where ID='" + TXTID.Text + "'", con);
            DataSet d1 = new DataSet();
            da1.Fill(d1);
            SqlDataAdapter da2 = new SqlDataAdapter("UPDATE LABIND_TABLE SET STATUS='Waiting' WHERE ID='" + Session["LINDID"].ToString() + "'", con);
            DataTable ds2 = new DataTable();
            da.Fill(ds2);
            SqlDataAdapter da3 = new SqlDataAdapter("delete from PA_TRANS where VOUCHERNO='" + TXTID.Text + "'", con);
            ds = new DataSet();
            da3.Fill(ds);
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/LABORATORY/LabIndent_View.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/LABORATORY/Default.aspx");
    }
}