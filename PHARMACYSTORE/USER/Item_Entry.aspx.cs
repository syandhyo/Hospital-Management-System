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

public partial class ADMIN_PHARMACYSTORE_Item_Entry : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr;
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
        string qry1 = "select ID from ITEM_TABLE";
        com = new SqlCommand(qry1, con);
        dr = null;
        dr = com.ExecuteReader();
        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("IT{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
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
        if (!IsPostBack)
        {
            binddata();
        }
    }
    public void binddata()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME,CATEGORY,QTY FROM ITEM_TABLE WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            using (SqlCommand cmd = new SqlCommand("phrmc_ItemMstSelDel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
               // GridView1.SelectedIndex = 0;
                GridView1.DataSource = dt;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }

            SqlDataAdapter da1 = new SqlDataAdapter("select distinct NAME FROM COMPANY_TABLE where ORGID='" + lblorgid.Text + "'", con);
            DataTable ds1 = new DataTable();
            da1.Fill(ds1);
            dropcompany.DataSource = ds1;
            dropcompany.DataTextField = "NAME";
            dropcompany.DataValueField = "NAME";
            dropcompany.DataBind();

            SqlDataAdapter da2 = new SqlDataAdapter("select distinct NAME FROM CATEGORY_TABLE where ORGID='" + lblorgid.Text + "'", con);
            DataTable ds2 = new DataTable();
            da2.Fill(ds2);
            dropcate.DataSource = ds2;
            dropcate.DataTextField = "NAME";
            dropcate.DataValueField = "NAME";
            dropcate.DataBind();

            using (SqlCommand COM = new SqlCommand("FINANCIAL_YEAR", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                SqlDataReader dr = COM.ExecuteReader();
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[3].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    public void clear_control()
    {
        lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        txtid.Text = "";
        txtname.Text = "";
        txtpprice.Text = "0";
        txtsprice.Text = "0";
        txtopening.Text = "0";
        txthsncode.Text = "0";
        TXTGST.Text = "0";
        txtbatchno.Text = "0";        
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('Please!! Enter The Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (txthsncode.Text == "")
            {
                string message = "alert('Please!! Enter HSN Code..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txthsncode.Focus();
                return;
            }
            else if (txtbatchno.Text == "")
            {
                string message = "alert('Please!! Enter Batch Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtbatchno.Focus();
                return;
            }
            else if (txtopening.Text == "")
            {
                string message = "alert('Please!! Enter Opening Balance..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtopening.Focus();
                return;
            }
            else if (txtpprice.Text == "")
            {
                string message = "alert('Please!! Enter Purchase Price..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpprice.Focus();
                return;
            }
            else if (Convert.ToDecimal(txtpprice.Text) <= 0)
            {
                string message = "alert('Please!! Enter Purchase Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpprice.Focus();
                return;
            }
            else if (txtsprice.Text == "")
            {
                string message = "alert('Please!! Enter Sale Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtsprice.Focus();
                return;
            }
            else if (TXTGST.Text == "")
            {
                string message = "alert('Please!! Enter GST In %..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                TXTGST.Focus();
                return;
            }
            else if (txttabletperstrip.Text == "")
            {
                txttabletperstrip.Text = "0";

            }
            else if (Convert.ToDecimal(txttabletperstrip.Text) < 0)
            {
                txttabletperstrip.Text = "0";

            }
            else if (Convert.ToDecimal(txtsprice.Text) <= 0)
            {
                string message = "alert('Please!! Enter Sale Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtsprice.Focus();
                return;
            }
            else if (txtself.Text == "")
            {
                string message = "alert('Please!! Enter self.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtself.Focus();
                return;
            }
            else if (txtrack.Text == "")
            {
                string message = "alert('Please!! Enter Rack.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtrack.Focus();
                return;
            }
            else if (txtexpirydate.Text == "")
            {
                string message = "alert('Please!! Enter Expiry Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtexpirydate.Focus();
                return;
            }
            else if (txtmfgdate0.Text == "")
            {
                string message = "alert('Please!! Enter Manufacturing Date.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtmfgdate0.Focus();               
                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            SqlCommand com = new SqlCommand("select * from ITEM_TABLE where NAME='" + txtname.Text.ToUpper() + "' AND BATCHNO='" + txtbatchno.Text + "' and ORGID='" + lblorgid.Text + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                string message = "alert('* Alredy Exist.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            dr.Close();

            auto();
            //if (txtmfgdate0.Text == "")
            //{
            //    txtmfgdate0.Text = "NULL";
            //}
            //SqlCommand cmd1 = new SqlCommand("insert into ITEM_TABLE (ID,COMPANY,NAME,CATEGORY,PUNIT,SUNIT,PPRICE,SPRICE,GST,HSNCODE,ORGID,BATCHNO,QTY,TPS,SELF,RACK,ROP,MFG,EXPDATE) values (@ID,@COMPANY,@NAME,@CATEGORY,@PUNIT,@SUNIT,@PPRICE,@SPRICE,@GST,@HSNCODE,@ORGID,@BATCHNO,@QTY,@TPS,@SELF,@RACK,@ROP,@MFG,@EXPDATE)", con);

            using (SqlCommand cmd1 = new SqlCommand("phrmc_ItemMstInsUp", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd1.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = dropcompany.Text.ToUpper();
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
                cmd1.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = dropcate.Text;
                cmd1.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = droppurchaseunit.Text;
                cmd1.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = dropsaleunit.Text;
                cmd1.Parameters.Add("@GST", SqlDbType.Decimal).Value = TXTGST.Text;
                cmd1.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = txtpprice.Text;
                cmd1.Parameters.Add("@SPRICE", SqlDbType.Decimal).Value = txtsprice.Text;
                cmd1.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = txthsncode.Text;
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = txtbatchno.Text;
                cmd1.Parameters.Add("@QTY", SqlDbType.Decimal).Value = txtopening.Text;
                cmd1.Parameters.Add("@TPS", SqlDbType.Decimal).Value = txttabletperstrip.Text;
                cmd1.Parameters.Add("@SELF", SqlDbType.VarChar).Value = txtself.Text;
                cmd1.Parameters.Add("@RACK", SqlDbType.VarChar).Value = txtrack.Text;
                cmd1.Parameters.Add("@ROP", SqlDbType.Decimal).Value = txtreorder.Text;
                cmd1.Parameters.Add("@MFG", SqlDbType.VarChar).Value = txtmfgdate0.Text;
                cmd1.Parameters.Add("@EXPDATE", SqlDbType.VarChar).Value = txtexpirydate.Text;
                cmd1.ExecuteNonQuery();
            }

            //SqlCommand cmd3 = new SqlCommand("insert into STOCK_TRAN (FYEAR,ORGID,DATE,ID,COMPANY,ITEMNAME,CATEGORY,PUNIT,SUNIT,PPRICE,SPRICE,OPENING,PURCHASE,PRETURN,SALE,SRETURN,CLOSEING,EXP) values (@FYEAR,@ORGID,@DATE,@ID,@COMPANY,@ITEMNAME,@CATEGORY,@PUNIT,@SUNIT,@PPRICE,@SPRICE,@OPENING,@PURCHASE,@PRETURN,@SALE,@SRETURN,@CLOSEING,@EXP)", con);
            using (SqlCommand cmd3 = new SqlCommand("phrmc_ItemStkTrInsUp", con))
            {
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd3.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                cmd3.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd3.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd3.Parameters.Add("@DATE", SqlDbType.Date).Value = lbldate.Text;
                cmd3.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = dropcompany.Text.ToUpper();
                cmd3.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
                cmd3.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = dropcate.Text;
                cmd3.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = droppurchaseunit.Text;
                cmd3.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = dropsaleunit.Text;
                cmd3.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = txtpprice.Text;
                cmd3.Parameters.Add("@SPRICE", SqlDbType.Decimal).Value = txtsprice.Text;              
                cmd3.Parameters.Add("@OPENING", SqlDbType.Decimal).Value = txtopening.Text;
                cmd3.Parameters.Add("@PURCHASE", SqlDbType.Decimal).Value = "0";
                cmd3.Parameters.Add("@PRETURN", SqlDbType.Decimal).Value = "0";
                cmd3.Parameters.Add("@SALE", SqlDbType.Decimal).Value = "0";
                cmd3.Parameters.Add("@SRETURN", SqlDbType.Decimal).Value = "0";
                cmd3.Parameters.Add("@CLOSEING", SqlDbType.Decimal).Value = txtopening.Text;
                cmd3.Parameters.Add("@EXP", SqlDbType.VarChar).Value = txtexpirydate.Text;
                cmd3.ExecuteNonQuery();                
            }
           // SqlCommand cmd2 = new SqlCommand("insert into STOCK_TABLE (ID,COMPANY,NAME,EXPDATE,QTY,ORGID,BATCHNO) values (@ID,@COMPANY,@NAME,@EXPDATE,@QTY,@ORGID,@BATCHNO)", con);
            using (SqlCommand cmd2 = new SqlCommand("phrmc_ItemStkTblIns", con))
            {
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd2.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = dropcompany.Text;
                cmd2.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
                cmd2.Parameters.Add("@EXPDATE", SqlDbType.VarChar).Value = txtexpirydate.Text;
                cmd2.Parameters.Add("@QTY", SqlDbType.Decimal).Value = txtopening.Text;
                cmd2.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = txtbatchno.Text;
                cmd2.ExecuteNonQuery();
            }
            //---------------------------
            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/PHARMACYSTORE/USER/Item_Entry.aspx");
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //SqlCommand com = new SqlCommand("select * from ITEM_TABLE where ID='" + slno + "'", con);
            //dr = com.ExecuteReader();
            using (SqlCommand cmd = new SqlCommand("phrmc_ItemMstSelDel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = dr["ID"].ToString();
                    dropcompany.Text = dr["COMPANY"].ToString();
                    txtname.Text = dr["NAME"].ToString();
                    txthsncode.Text = dr["HSNCODE"].ToString();
                    txtbatchno.Text = dr["BATCHNO"].ToString();
                    dropcate.Text = dr["CATEGORY"].ToString();
                    droppurchaseunit.Text = dr["PUNIT"].ToString();
                    dropsaleunit.Text = dr["SUNIT"].ToString();
                    txtpprice.Text = dr["PPRICE"].ToString();
                    txtsprice.Text = dr["SPRICE"].ToString();
                    TXTGST.Text = dr["GST"].ToString();
                    txtopening.Text = dr["QTY"].ToString();
                    txtexpirydate.Text = dr["EXPDATE"].ToString();
                    txtmfgdate0.Text = dr["MFG"].ToString();
                    txttabletperstrip.Text = dr["TPS"].ToString();
                    txtself.Text = dr["SELF"].ToString();
                    txtrack.Text = dr["RACK"].ToString();
                    txtreorder.Text = dr["ROP"].ToString();
                }
            }
                dr.Close();            
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
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txthsncode.Text == "")
            {
                string message = "alert('* Enter Hsncode.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtopening.Text == "")
            {
                string message = "alert('* Enter Opening Balance.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtpprice.Text == "")
            {
                string message = "alert('* Enter Purchase Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (Convert.ToDecimal(txtpprice.Text) <= 0)
            {
                string message = "alert('* Enter Purchase Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtsprice.Text == "")
            {
                string message = "alert('* Enter Sale Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txttabletperstrip.Text == "")
            {
                txttabletperstrip.Text = "0";

            }
            else if (Convert.ToDecimal(txttabletperstrip.Text) < 0)
            {
                txttabletperstrip.Text = "0";

            }
            else if (Convert.ToDecimal(txtsprice.Text) <= 0)
            {
                string message = "alert('* Enter Sale Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtself.Text == "")
            {
                string message = "alert('* Enter self.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtrack.Text == "")
            {
                string message = "alert('* Enter Rack.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            if (txtexpirydate.Text == "")
            {
                txtexpirydate.Text = "NULL";
            }
            //SqlCommand cmd1 = new SqlCommand("UPDATE ITEM_TABLE SET COMPANY=@COMPANY, NAME=@NAME,CATEGORY=@CATEGORY,PUNIT=@PUNIT,SUNIT=@SUNIT,PPRICE=@PPRICE,SPRICE=@SPRICE,GST=@GST,HSNCODE=@HSNCODE,BATCHNO=@BATCHNO,QTY=@QTY,TPS=@TPS,SELF=@SELF,RACK=@RACK,ROP=@ROP,MFG=@MFG,EXPDATE=@EXPDATE WHERE ID=@ID AND ORGID=@ORGID ", con);
            using (SqlCommand cmd1 = new SqlCommand("phrmc_ItemMstInsUp", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd1.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = dropcompany.Text.ToUpper();
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
                cmd1.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = dropcate.Text;
                cmd1.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = droppurchaseunit.Text;
                cmd1.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = dropsaleunit.Text;
                cmd1.Parameters.Add("@GST", SqlDbType.Decimal).Value = TXTGST.Text;
                cmd1.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = txtpprice.Text;
                cmd1.Parameters.Add("@SPRICE", SqlDbType.Decimal).Value = txtsprice.Text;
                cmd1.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = txthsncode.Text;
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = txtbatchno.Text;
                cmd1.Parameters.Add("@QTY", SqlDbType.Decimal).Value = txtopening.Text;
                cmd1.Parameters.Add("@TPS", SqlDbType.Decimal).Value = txttabletperstrip.Text;
                cmd1.Parameters.Add("@SELF", SqlDbType.VarChar).Value = txtself.Text;
                cmd1.Parameters.Add("@RACK", SqlDbType.VarChar).Value = txtrack.Text;
                cmd1.Parameters.Add("@ROP", SqlDbType.Decimal).Value = txtreorder.Text;
                cmd1.Parameters.Add("@MFG", SqlDbType.VarChar).Value = txtmfgdate0.Text;
                cmd1.Parameters.Add("@EXPDATE", SqlDbType.VarChar).Value = txtexpirydate.Text;
                cmd1.ExecuteNonQuery();
            }
           
            //-----------------------------
            SqlDataAdapter da = new SqlDataAdapter("delete from STOCK_TRAN WHERE ID='" + txtid.Text + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "STOCK_TRAN");
            //SqlCommand cmd3 = new SqlCommand("insert into STOCK_TRAN (FYEAR,ORGID,DATE,ID,COMPANY,ITEMNAME,CATEGORY,PUNIT,SUNIT,PPRICE,SPRICE,OPENING,PURCHASE,PRETURN,SALE,SRETURN,CLOSEING,EXP) values (@FYEAR,@ORGID,@DATE,@ID,@COMPANY,@ITEMNAME,@CATEGORY,@PUNIT,@SUNIT,@PPRICE,@SPRICE,@OPENING,@PURCHASE,@PRETURN,@SALE,@SRETURN,@CLOSEING,@EXP)", con);
           using (SqlCommand cmd3 = new SqlCommand("phrmc_ItemStkTrInsUp", con))
            {
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd3.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                cmd3.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd3.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd3.Parameters.Add("@DATE", SqlDbType.Date).Value = lbldate.Text;
                cmd3.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = dropcompany.Text.ToUpper();
                cmd3.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
                cmd3.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = dropcate.Text;
                cmd3.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = droppurchaseunit.Text;
                cmd3.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = dropsaleunit.Text;
                cmd3.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = txtpprice.Text;
                cmd3.Parameters.Add("@SPRICE", SqlDbType.Decimal).Value = txtsprice.Text;
                cmd3.Parameters.Add("@GST", SqlDbType.Decimal).Value = TXTGST.Text;
                cmd3.Parameters.Add("@OPENING", SqlDbType.Decimal).Value = txtopening.Text;
                cmd3.Parameters.Add("@PURCHASE", SqlDbType.Decimal).Value = "0";
                cmd3.Parameters.Add("@PRETURN", SqlDbType.Decimal).Value = "0";
                cmd3.Parameters.Add("@SALE", SqlDbType.Decimal).Value = "0";
                cmd3.Parameters.Add("@SRETURN", SqlDbType.Decimal).Value = "0";
                cmd3.Parameters.Add("@CLOSEING", SqlDbType.Decimal).Value = txtopening.Text;
                cmd3.Parameters.Add("@EXP", SqlDbType.VarChar).Value = txtexpirydate.Text;
                cmd3.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
            }
            //--------------------------------------------
            SqlDataAdapter da1 = new SqlDataAdapter("delete from STOCK_TABLE WHERE ID='" + txtid.Text + "' AND ORGID='" + lblorgid.Text + "'", con);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1, "STOCK_TABLE");
           // SqlCommand cmd2 = new SqlCommand("insert into STOCK_TABLE (ID,COMPANY,NAME,EXPDATE,QTY,ORGID,BATCHNO) values (@ID,@COMPANY,@NAME,@EXPDATE,@QTY,@ORGID,@BATCHNO)", con);
            using (SqlCommand cmd2 = new SqlCommand("phrmc_ItemStkTblIns", con))
            {
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd2.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = dropcompany.Text;
                cmd2.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
                cmd2.Parameters.Add("@EXPDATE", SqlDbType.VarChar).Value = txtexpirydate.Text;
                cmd2.Parameters.Add("@QTY", SqlDbType.Decimal).Value = txtopening.Text;
                cmd2.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = txtbatchno.Text;
                cmd2.ExecuteNonQuery();
            }
            //---------------------------

            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/PHARMACYSTORE/USER/Item_Entry.aspx");
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
            //SqlCommand cm = new SqlCommand("delete from STOCK_TRAN where ID='" + slno + "'", con);        
            using (SqlCommand cm = new SqlCommand("phrmc_ItemMstSelDel", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELSTTRAN";
                cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                cm.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
                cm.ExecuteNonQuery();
            }
            //SqlCommand cm1 = new SqlCommand("delete from ITEM_TABLE where ID='" + slno + "'", con);

            using (SqlCommand cm1 = new SqlCommand("phrmc_ItemMstSelDel", con))
            {
                cm1.CommandType = CommandType.StoredProcedure;
                cm1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELITEMTBL";
                cm1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                cm1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                cm1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
                cm1.ExecuteNonQuery();
            }
            //SqlDataAdapter da1 = new SqlDataAdapter("delete from STOCK_TABLE WHERE ID='" + slno + "'", con);
            using (SqlCommand cm2 = new SqlCommand("phrmc_ItemMstSelDel", con))
            {
                cm2.CommandType = CommandType.StoredProcedure;
                cm2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELSTTBL";
                cm2.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                cm2.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                cm2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "";
                SqlDataAdapter adp = new SqlDataAdapter(cm2);
                DataSet dt = new DataSet();
                adp.Fill(dt, "STOCK_TABLE");
            }
            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/PHARMACYSTORE/USER/Item_Entry.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();       
        Response.Redirect("~/PHARMACYSTORE/USER/Item_Entry.aspx");
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME,CATEGORY,QTY FROM ITEM_TABLE WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
            using (SqlCommand cmd = new SqlCommand("phrmc_ItemMstSelDel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INDEXCHG";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                // GridView1.SelectedIndex = 0;
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
        if (dropcate.Text != "TABLET" )
        {           
            txttabletperstrip.Enabled = false;
            txttabletperstrip.Text = "0";
        }      
        else
        {
            txttabletperstrip.Enabled = true;
        }
    }
    protected void Btnsearch_click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME,CATEGORY,QTY FROM ITEM_TABLE WHERE NAME like'" + txtsearchname.Text + "%' AND  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
            using (SqlCommand cmd = new SqlCommand("phrmc_ItemMstSelDel", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SEARCH";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtsearchname.Text;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                // GridView1.SelectedIndex = 0;
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
}