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
public partial class PHARMACYSTORE_USER_AddFreestock : System.Web.UI.Page
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
                cmd.CommandText = "select DISTINCT NAME from STOCK_TABLE where NAME like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = con;

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["NAME"], sdr["NAME"]));
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
        string qry1 = "select ID from FSTOCK_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("FS{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        TXTID.Text = num1;

        dr.Close();
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
            //Label1.Text = dr["FYEAR"].ToString();
            lblfyear.Text = dr["FYEAR"].ToString();
        }
        dr.Close();
        SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,CONVERT(VARCHAR(10),DATE,105) AS DATE FROM FSTOCK_TABLE WHERE  ORGID='" + lblorgid.Text + "' AND FYEAR='" + lblfyear.Text + "' ORDER BY ID DESC", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView2.SelectedIndex = 0;
        GridView2.DataSource = dt;
        GridView2.DataKeyNames = new string[] { "ID" };
        GridView2.DataBind();
        con.Close();
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
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("COMPANY"), new DataColumn("NAME"), new DataColumn("HSNCODE"), new DataColumn("BATCHNO"), new DataColumn("CATEGORY"), new DataColumn("EXP"), new DataColumn("UNIT"), new DataColumn("QTY") });
            ViewState["ITEM"] = dt;
            this.BindGrid();

            binddata();
          

        }
        con.Close();
    }
    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand("select COMPANY, HSNCODE,NAME,BATCHNO,PPRICE,GST,GST/2 AS GST2 from ITEM_TABLE where NAME='" + txtname.Text + "' AND ORGID='" + lblorgid.Text + "'", con);
        dr = com.ExecuteReader();
        if (dr.Read())
        {
            txtcomp.Text = dr["COMPANY"].ToString();
            txthsncode.Text = dr["HSNCODE"].ToString();
            txtname.Text = dr["NAME"].ToString();
            txtbatchno.Text = dr["BATCHNO"].ToString();

            dr.Close();
            da = new SqlDataAdapter("select CATEGORY FROM ITEM_TABLE where NAME='" + txtname.Text + "' AND ORGID='" + lblorgid.Text + "'", con);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dropcate.DataSource = ds;
            dropcate.DataTextField = "CATEGORY";
            dropcate.DataValueField = "CATEGORY";
            dropcate.DataBind();
          

        }

        else
        {
            dr.Close();
            Response.Write("<script LANGUAGE='JavaScript' >alert('* Incorrect Name.Try New Name.')</script>");
            return;
        }
        con.Close();
    }
    protected void txtopening_TextChanged(object sender, EventArgs e)
    {
        if (txtopening.Text == "")
        {
            txtopening.Text = "0";
        }
      
    }
   
    protected void GVOnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lbl_slno") as Label).Text = (e.Row.RowIndex + 1).ToString();
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (txtname.Text == "")
        {
            return;
        }
      
        else if (txtopening.Text == "")
        {
            return;
        }
        else if (Convert.ToInt32(txtopening.Text) <= 0)
        {
            return;
        }
       
        DataTable dt = (DataTable)ViewState["ITEM"];
        dt.Rows.Add(txtcomp.Text.Trim(), txtname.Text.Trim(), txthsncode.Text.Trim(), txtbatchno.Text.Trim(), dropcate.Text.Trim(), txtexpirydate.Text.Trim(), droppurchaseunit.Text.Trim(), txtopening.Text.Trim());
        ViewState["ITEM"] = dt;
        this.BindGrid();
     

    }
    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        DataTable dt = (DataTable)ViewState["ITEM"];
        GridViewRow row = (GridViewRow)grvStudentDetails.Rows[e.RowIndex];
        Label qty = (Label)row.FindControl("lbl_qty");
        string QTY = qty.Text.ToString();

        dt.Rows[index].Delete();
        ViewState["ITEM"] = dt;
     

        this.BindGrid();

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (grvStudentDetails.Rows.Count <= 0)
        {
            Response.Write("<script LANGUAGE='JavaScript' >alert('*Add item to purchase.')</script>");

            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        auto();
        dr.Close();
        STOCK_TABLE();
        STOCK_TRAN();
        con.Close();
        Response.Redirect("~/PHARMACYSTORE/USER/AddFreestock.aspx");
    }
    public void STOCK_TABLE()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();



        SqlCommand stock_cmd = new SqlCommand("insert into FSTOCK_TABLE (FYEAR,ORGID,ID,DATE) values (@FYEAR,@ORGID,@ID,@DATE)", con);
        stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
        stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
        stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = txtinvdate.Text;
        stock_cmd.ExecuteNonQuery();
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
            SqlCommand cmd2 = new SqlCommand("insert into FREEITEM_TABLE (ORGID,ID, COMPANY,NAME,HSNCODE,BATCHNO,CATEGORY,EXPDATE,UNIT,QTY) values (@ORGID,@ID, @COMPANY,@NAME,@HSNCODE,@BATCHNO,@CATEGORY,@EXPDATE,@UNIT,@QTY)", con);
            cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            cmd2.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
            cmd2.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
            cmd2.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = gv1.Cells[2].Text;
            cmd2.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
            cmd2.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
            cmd2.Parameters.Add("@EXPDATE", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
            cmd2.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
            cmd2.Parameters.Add("@QTY", SqlDbType.VarChar).Value = gv1.Cells[7].Text;
            cmd2.ExecuteNonQuery();



            using (SqlCommand stock_cmd = new SqlCommand("purchase_Tran", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = txtinvdate.Text;
                stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                stock_cmd.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
                stock_cmd.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
                stock_cmd.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = "0";
                stock_cmd.Parameters.Add("@SPRICE", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@PURCHASE", SqlDbType.VarChar).Value = gv1.Cells[7].Text;
                stock_cmd.Parameters.Add("@PRETURN", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@SALE", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@SRETURN", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@CLOSEING", SqlDbType.VarChar).Value = "0";
                stock_cmd.ExecuteNonQuery();
            }

            using (SqlCommand stock_cmd = new SqlCommand("PUR_STOCK_TABLE", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                stock_cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = TXTID.Text;
                stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                stock_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = gv1.Cells[7].Text;
                stock_cmd.ExecuteNonQuery();


            }
        }
        con.Close();
        //}
        //catch { }

    }
    public void STOCK_TABLE_UPDATE()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();



        SqlCommand stock_cmd = new SqlCommand("UPDATE FSTOCK_TABLE SET DATE=@DATE WHERE ID=@ID", con);
        stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
        stock_cmd.Parameters.Add("@DATE", SqlDbType.VarChar).Value = txtinvdate.Text;
        stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
     
        stock_cmd.ExecuteNonQuery();


        con.Close();
        //}
        //catch { }
    }
    public void STOCK_TRAN_UPDATE()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        DataTable dt1 = ViewState["ITEM"] as DataTable;
        GridView1.DataSource = dt1;
        GridView1.DataBind();

        SqlDataAdapter da = new SqlDataAdapter("delete from FREEITEM_TABLE where ID='" + TXTID.Text + "'", con);
        DataSet d = new DataSet();
        da.Fill(d);

        foreach (GridViewRow gv1 in GridView1.Rows)
        {
            SqlCommand cmd2 = new SqlCommand("insert into FREEITEM_TABLE (ORGID,ID, COMPANY,NAME,HSNCODE,BATCHNO,CATEGORY,EXPDATE,UNIT,QTY) values (@ORGID,@ID, @COMPANY,@NAME,@HSNCODE,@BATCHNO,@CATEGORY,@EXPDATE,@UNIT,@QTY)", con);
            cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            cmd2.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
            cmd2.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
            cmd2.Parameters.Add("@HSNCODE", SqlDbType.VarChar).Value = gv1.Cells[2].Text;
            cmd2.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
            cmd2.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
            cmd2.Parameters.Add("@EXPDATE", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
            cmd2.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
            cmd2.Parameters.Add("@QTY", SqlDbType.VarChar).Value = gv1.Cells[7].Text;
            cmd2.ExecuteNonQuery();



            using (SqlCommand stock_cmd = new SqlCommand("purchase_Tran", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = txtinvdate.Text;
                stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                stock_cmd.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
                stock_cmd.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
                stock_cmd.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = "0";
                stock_cmd.Parameters.Add("@SPRICE", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@PURCHASE", SqlDbType.VarChar).Value = gv1.Cells[7].Text;
                stock_cmd.Parameters.Add("@PRETURN", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@SALE", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@SRETURN", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@CLOSEING", SqlDbType.VarChar).Value = "0";
                stock_cmd.ExecuteNonQuery();
            }

            using (SqlCommand stock_cmd = new SqlCommand("PUR_STOCK_TABLE", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                stock_cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = TXTID.Text;
                stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                stock_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = gv1.Cells[7].Text;
                stock_cmd.ExecuteNonQuery();


            }
            using (SqlCommand stock_cmd = new SqlCommand("purchase_Tran", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = txtinvdate.Text;
                stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                stock_cmd.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
                stock_cmd.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
                stock_cmd.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = "0";
                stock_cmd.Parameters.Add("@SPRICE", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@PURCHASE", SqlDbType.VarChar).Value = gv1.Cells[7].Text;
                stock_cmd.Parameters.Add("@PRETURN", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@SALE", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@SRETURN", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@CLOSEING", SqlDbType.VarChar).Value = "0";
                stock_cmd.ExecuteNonQuery();
            }

            using (SqlCommand stock_cmd = new SqlCommand("PUR_STOCK_TABLE", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                stock_cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = TXTID.Text;
                stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                stock_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = gv1.Cells[7].Text;
                stock_cmd.ExecuteNonQuery();


            }
        }
        con.Close();
        //}
        //catch { }

    }
    public void STOCK_TABLE_DEL()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter("delete from FSTOCK_TABLE where ID='" + TXTID.Text + "'", con);
        DataSet d = new DataSet();
        da.Fill(d);
        con.Close();
        //}

        //catch { }
    }
    public void STOCK_TRAN_DEL()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        DataTable dt1 = ViewState["ITEM"] as DataTable;
        GridView1.DataSource = dt1;
        GridView1.DataBind();
        SqlDataAdapter da = new SqlDataAdapter("delete from FREEITEM_TABLE where ID='" + TXTID.Text + "'", con);
        DataSet d = new DataSet();
        da.Fill(d);
        foreach (GridViewRow gv1 in GridView1.Rows)
        {
            using (SqlCommand stock_cmd = new SqlCommand("purchase_Tran", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = txtinvdate.Text;
                stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                stock_cmd.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = gv1.Cells[4].Text;
                stock_cmd.Parameters.Add("@PUNIT", SqlDbType.VarChar).Value = gv1.Cells[6].Text;
                stock_cmd.Parameters.Add("@SUNIT", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@PPRICE", SqlDbType.Decimal).Value = "0";
                stock_cmd.Parameters.Add("@SPRICE", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@PURCHASE", SqlDbType.VarChar).Value = gv1.Cells[7].Text;
                stock_cmd.Parameters.Add("@PRETURN", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@SALE", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@SRETURN", SqlDbType.VarChar).Value = "0";
                stock_cmd.Parameters.Add("@CLOSEING", SqlDbType.VarChar).Value = "0";
                stock_cmd.ExecuteNonQuery();
            }

            using (SqlCommand stock_cmd = new SqlCommand("PUR_STOCK_TABLE", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                stock_cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = TXTID.Text;
                stock_cmd.Parameters.Add("@COMPANY", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                stock_cmd.Parameters.Add("@EXP", SqlDbType.VarChar).Value = gv1.Cells[5].Text;
                stock_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = gv1.Cells[7].Text;
                stock_cmd.ExecuteNonQuery();


            }
        }
        con.Close();
        //}
        //catch { }

    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand com = new SqlCommand("select ID,CONVERT(VARCHAR(10),DATE,105) as DATE from FSTOCK_TABLE where ID='" + slno + "'", con);
        dr = com.ExecuteReader();
        if (dr.Read())
        {
            btncreate.Visible = false;
            btnupdate.Visible = true;
            TXTID.Text = dr["ID"].ToString();
            txtinvdate.Text = dr["DATE"].ToString();
         
        }
        dr.Close();
        da = new SqlDataAdapter("select COMPANY,NAME,HSNCODE,BATCHNO,CATEGORY,EXPDATE as EXP,UNIT,QTY FROM FREEITEM_TABLE where ID='" + TXTID.Text + "' and ORGID='" + lblorgid.Text + "'", con);
        DataSet ds2 = new DataSet();
        da.Fill(ds2);
        grvStudentDetails.DataSource = ds2.Tables["Table"];
        grvStudentDetails.DataBind();

        DataTable dt = ds2.Tables["Table"];
        ViewState["ITEM"] = dt;

        SqlDataAdapter da1 = new SqlDataAdapter("select COMPANY,NAME,HSNCODE,BATCHNO,CATEGORY,EXPDATE as EXP,UNIT,QTY FROM PURCHASE_TABLE where ID='" + TXTID.Text + "' and ORGID='" + lblorgid.Text + "'", con);
        DataSet ds3 = new DataSet();
        da1.Fill(ds3);
        GridView1.DataSource = ds3.Tables["Table"];
        GridView1.DataBind();
        btndelete.Visible = true;
        con.Close();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,CONVERT(VARCHAR(10),DATE,105) AS DATE FROM FSTOCK_TABLE WHERE  ORGID='" + lblorgid.Text + "' AND FYEAR='" + lblfyear.Text + "' ORDER BY ID DESC", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView2.SelectedIndex = 0;
        GridView2.DataSource = dt;
        GridView2.DataKeyNames = new string[] { "ID" };
        GridView2.DataBind();

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (txtinvdate.Text == "")
        {
            Response.Write("<script LANGUAGE='JavaScript' >alert('* Fields are mandatory.')</script>");

            return;
        }
        else if (grvStudentDetails.Rows.Count <= 0)
        {
            Response.Write("<script LANGUAGE='JavaScript' >alert('*Add item to purchase.')</script>");

            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        STOCK_TABLE_UPDATE();
        STOCK_TRAN_UPDATE();
        con.Close();
        Response.Redirect("~/PHARMACYSTORE/USER/AddFreestock.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PHARMACYSTORE/USER/AddFreestock.aspx");
    }
    protected void Btndelete_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        STOCK_TABLE_DEL();
        STOCK_TRAN_DEL();
        con.Close();
        Response.Redirect("~/PHARMACYSTORE/USER/AddFreestock.aspx");

    }
}