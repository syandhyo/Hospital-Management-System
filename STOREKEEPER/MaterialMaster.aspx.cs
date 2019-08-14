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

public partial class STOREKEEPER_MaterialMaster : System.Web.UI.Page
{
    string num1 = "MA000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr, dr1;
    SqlDataAdapter da;
    DataTable dt;
    decimal total_amt = 0;
    decimal total_vat_amt = 0;
    int i, no, no1, sl;
    GridViewRow gr;


    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from MATERIAL_MASTER_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("MA{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;

        dr.Close();
        con.Close();
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
        lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        lblid.Text = Session["NAME"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        if (!IsPostBack)
        {
            binddata();
            auto();
            txttag.Visible = false ;
        }
    }
    public void binddata()
    {

        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("SP_MATERIAL_PAGING", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT_PAGE";
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME,PRICE,QTY FROM MATERIAL_MASTER_TABLE WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                // GridView1.SelectedIndex = 0;
                GridView1.DataSource = dt;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            SqlCommand comid = new SqlCommand("select max(ID) as matid from MATERIAL_MASTER_TABLE ", con);
            dr1 = comid.ExecuteReader();
            if (dr1.Read())
            {
                txtmatid.Text = dr1["matid"].ToString();
            }
            dr1.Close();


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
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
       
    }
    
    public void clear_control()
    {
        lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        txtid.Text = "";
        txtname.Text = "";
        txtprice.Text = "0";
        txthsncode.Text = "0";
        txtgst.Text = "0";

        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtname.Text == "")
            {

                string message = "alert(Please!! Enter The Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txthsncode.Text == "" || txthsncode.Text  == "0")
            {

                string message = "alert('Please!! Enter Hsncode..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtprice.Text == "" || txtprice.Text == "0.00")
            {
                string message = "alert('Please!! Enter Purchase Price..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand cmd1 = new SqlCommand("sp_material", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
                cmd1.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = droptype.Text;
                cmd1.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = txtprice.Text;
                cmd1.Parameters.Add("@QTY", SqlDbType.VarChar).Value = txtqty.Text;
                cmd1.Parameters.Add("@GST", SqlDbType.Decimal).Value = txtgst.Text;
                cmd1.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = txthsncode.Text;
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                cmd1.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = txtunit.Text;
                cmd1.Parameters.Add("@TAG", SqlDbType.VarChar).Value = txttag.Text;
                cmd1.Parameters.Add("@MATERIAL_NAME", SqlDbType.VarChar).Value = txtname.Text;
                cmd1.Parameters.Add("@OPENING", SqlDbType.Decimal).Value = txtqty.Text;
                cmd1.Parameters.Add("@PURCHES", SqlDbType.Decimal).Value = "0.00";
                cmd1.Parameters.Add("@RETN", SqlDbType.Decimal).Value = "0.00";
                cmd1.Parameters.Add("@ISSUE", SqlDbType.Decimal).Value = "0.00";
                cmd1.Parameters.Add("@REF", SqlDbType.Decimal).Value = "0.00";
                cmd1.Parameters.Add("@CLOSING", SqlDbType.Decimal).Value = txtqty.Text;


                cmd1.ExecuteNonQuery();
                string message = "alert('Successfully Inserted.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            }

            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/STOREKEEPER/MaterialMaster.aspx");
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("SP_MATERIAL_PAGING", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT_EVENT";
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                //SqlDataAdapter da = new SqlDataAdapter(cmd1);
                //SqlCommand com = new SqlCommand("select * from MATERIAL_MASTER_TABLE where ID='" + slno + "'", con);
                dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = dr["ID"].ToString();
                    txtname.Text = dr["NAME"].ToString();
                    droptype.Text = dr["TYPE"].ToString();
                    txtprice.Text = dr["PRICE"].ToString();
                    txtqty.Text = dr["QTY"].ToString();
                    txtgst.Text = dr["GST"].ToString();
                    txthsncode.Text = dr["HSNCODE"].ToString();
                    txtdate.Text = Convert.ToDateTime(dr["DATE"]).ToString("dd-MM-yyyy");
                    txtunit.Text = dr["UNIT"].ToString();
                    txttag.Text = dr["TAG"].ToString();
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('Please!! Enter The Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txthsncode.Text == "" || txthsncode.Text == "0")
            {

                string message = "alert('Please!! Enter The HSN Code..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtprice.Text == "" || txtprice.Text == "0.00")
            {
                string message = "alert('Please!! Enter Purchase Price..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            using (SqlCommand cmd1 = new SqlCommand("sp_material", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
                cmd1.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = droptype.Text;
                cmd1.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = txtprice.Text;
                cmd1.Parameters.Add("@QTY", SqlDbType.VarChar).Value = txtqty.Text;
                cmd1.Parameters.Add("@GST", SqlDbType.Decimal).Value = txtgst.Text;
                cmd1.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = txthsncode.Text.ToUpper();
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                cmd1.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = txtunit.Text;
                cmd1.Parameters.Add("@TAG", SqlDbType.VarChar).Value = txttag.Text;
                cmd1.Parameters.Add("@MATERIAL_NAME", SqlDbType.VarChar).Value = txtname.Text;
                cmd1.Parameters.Add("@OPENING", SqlDbType.Decimal).Value = txtqty.Text;
                cmd1.Parameters.Add("@PURCHES", SqlDbType.Decimal).Value = "0.00";
                cmd1.Parameters.Add("@RETN", SqlDbType.Decimal).Value = "0.00";
                cmd1.Parameters.Add("@ISSUE", SqlDbType.Decimal).Value = "0.00";
                cmd1.Parameters.Add("@REF", SqlDbType.Decimal).Value = "0.00";
                cmd1.Parameters.Add("@CLOSING", SqlDbType.Decimal).Value = txtqty.Text;
                cmd1.ExecuteNonQuery();
                string message = "alert('Successfully Updated.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/STOREKEEPER/MaterialMaster.aspx");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[3].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
            using (SqlCommand cmd1 = new SqlCommand("SP_MATERIAL_PAGING", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "DELETE";
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                //SqlCommand cm = new SqlCommand("delete from MATERIAL_STORE_TABLE where ID='" + slno + "'", con);
                //cm.ExecuteNonQuery();
                //SqlCommand cm1 = new SqlCommand("delete from MATERIAL_MASTER_TABLE where ID='" + slno + "'", con);
                cmd1.ExecuteNonQuery();
                //SqlDataAdapter da1 = new SqlDataAdapter("delete from MATERIAL_STOCK_TABLE WHERE ID='" + slno + "'", con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "MATERIAL_STOCK_TABLE");
                binddata();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/STOREKEEPER/MaterialMaster.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {        
        Response.Redirect("~/STOREKEEPER/MaterialMaster.aspx");
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
           // SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME,PRICE,QTY FROM MATERIAL_MASTER_TABLE WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);

            using (SqlCommand cmd1 = new SqlCommand("SP_MATERIAL_PAGING", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT_PAGE";
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = dt;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void dropcate_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (dropcate.Text != "TABLET")
        //{

        //    txttabletperstrip.Enabled = false;
        //    txttabletperstrip.Text = "0";
        //}

        //else
        //{
        //    txttabletperstrip.Enabled = true;
        //}
    }
    protected void Btnsearch_click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
           // SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME,PRICE,QTY FROM MATERIAL_MASTER_TABLE WHERE NAME like'" + txtsearchname.Text + "%' AND  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
            using (SqlCommand cmd1 = new SqlCommand("SP_MATERIAL_NAME_SEARCH", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SEARCH";
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtsearchname.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = dt;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void droptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (droptype.SelectedIndex == 0)
        {
            tag.Visible = false;
            txttag.Visible = false;
            txttag.Text = "";
        }
       else if (droptype.SelectedIndex == 1)
        {
            tag.Visible = false;
            txttag.Visible = false;
            txttag.Text = "";
        }
        else
        {
            tag.Visible = true;
            txttag.Visible = true;
            txttag.Text = "";
        }
    }
}