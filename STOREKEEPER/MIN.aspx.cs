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

public partial class STOREKEEPER_MIN : System.Web.UI.Page
{
    string num1 = "SJ000";
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
            using (SqlCommand cmd = new SqlCommand("STORE_MIN", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_STOCK_TABLE";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = prefix;
                //cmd.CommandText = "select DISTINCT NAME from STOCK_TABLE where NAME like @SearchText+'%'";
                //cmd.Parameters.AddWithValue("@SearchText", prefix);
                //cmd.Connection = con;

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
        string qry1 = "select  slno from MIN_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["slno"].ToString();
        }
        //num1 = string.Format("PU{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtminnumber.Text = "MIN-"+num1+1+"-"+lblfyear.Text;
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
        //using (SqlCommand cmd = new SqlCommand("STORE_MIN", con))
        //{
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_GRID_PAGE";
        //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
        //    cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = lblfyear.Text;
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,CONVERT(VARCHAR(10),MINDATE,105) AS DATE FROM MIN_TABLE WHERE   FYEAR='" + lblfyear.Text + "' ORDER BY ID DESC", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //GridView2.SelectedIndex = 0;
            //GridView2.DataSource = dt;
            //GridView2.DataKeyNames = new string[] { "ID" };
            //GridView2.DataBind();
        //}
        con.Close();
    }
    protected void BindGrid()
    {
        try
        {
            grvStudentDetails.DataSource = (DataTable)ViewState["ITEM"];
            grvStudentDetails.DataBind();

        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
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
        TextBox1.Text = Session["MRINDID"].ToString();
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("MRNO"), new DataColumn("NAME"), new DataColumn("QTY"), new DataColumn("UNIT") });
            ViewState["ITEM"] = dt;
            this.BindGrid();

            binddata();
            using (SqlCommand cmd = new SqlCommand("STORE_MIN", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BIND1";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = TextBox1.Text;
                da = new SqlDataAdapter(cmd);
                //da = new SqlDataAdapter("select B.DeptName AS DeptName,A.DEPTID AS id FROM TBLMR A, tblDepartment B WHERE A.DEPTID=B.id and A.INVNO='" + TextBox1.Text + "'", con);
                DataTable ds = new DataTable();
                da.Fill(ds);
                dropissuedto.DataSource = ds;
                dropissuedto.DataTextField = "DeptName";
                dropissuedto.DataValueField = "id";
                dropissuedto.DataBind();
            }
            //dropissuedto.Items.Insert(0, "Please Select");
            using (SqlCommand cmd1 = new SqlCommand("STORE_MIN", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BIND2";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
                da2 = new SqlDataAdapter(cmd1);
                //da2 = new SqlDataAdapter("select distinct MRNO FROM TBLMR_OLDSTOCK WHERE QTY>0", con);
                DataTable ds2 = new DataTable();
                da2.Fill(ds2);
                DropMrno.DataSource = ds2;
                DropMrno.DataTextField = "MRNO";
                DropMrno.DataValueField = "MRNO";
                DropMrno.DataBind();
                DropMrno.Items.Insert(0, "Please Select");
            }
          
            //txtitemname.Items.Insert(0, "Please Select");
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
    public void clearinercontrol()
    {
        txtunit.Text = "";
        txtqty.Text = "0";
        //txtitemname.Text = "";
    }
    protected void txtitemname_TextChanged(object sender, EventArgs e)
    {
         
       
    }
    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        DataTable dt = (DataTable)ViewState["ITEM"];
        GridViewRow row = (GridViewRow)grvStudentDetails.Rows[e.RowIndex];
        dt.Rows[index].Delete();
        ViewState["ITEM"] = dt;
        this.BindGrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdate.Text == "")
            {
                string message = "alert('* Insert Min Date.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtrecivedperson.Text == "")
            {
                string message = "alert('* Insert personname.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (grvStudentDetails.Rows.Count <= 0)
            {

                string message = "alert('*Add Item to Save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("STORE_MIN", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BUTTON1";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtminnumber.Text;
                cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
                //SqlCommand cm = new SqlCommand("select * from MIN_TABLE where ID ='" + txtminnumber.Text + "'", con);
                dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string message = "alert('*Cant enter duplicate MIN number.MIN number alredy exist.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                auto();
            }

            dr.Close();
            STOCK_TABLE();
            STOCK_TRAN();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        
        }
        Response.Redirect("~/STOREKEEPER/MREQUEST.aspx");
    }
    public void STOCK_TABLE()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand stock_cmd = new SqlCommand("MIN_OPERATION", con))
        {
            stock_cmd.CommandType = CommandType.StoredProcedure;
            stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtminnumber.Text;
            stock_cmd.Parameters.Add("@SID ", SqlDbType.VarChar).Value = "";
            stock_cmd.Parameters.Add("@MINDATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            stock_cmd.Parameters.Add("@RECBY", SqlDbType.VarChar).Value = txtrecivedperson.Text;
            stock_cmd.Parameters.Add("@ISSUEDTO", SqlDbType.VarChar).Value = dropissuedto.SelectedValue;
            stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value ="";
            stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value ="";
            stock_cmd.Parameters.Add("@MRNO", SqlDbType.VarChar).Value = TextBox1.Text;
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
            using (SqlCommand stock_cmd = new SqlCommand("MIN_OPERATION", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
                stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtminnumber.Text;
                stock_cmd.Parameters.Add("@SID ", SqlDbType.VarChar).Value = "";
                stock_cmd.Parameters.Add("@MINDATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                stock_cmd.Parameters.Add("@RECBY", SqlDbType.VarChar).Value = txtrecivedperson.Text;
                stock_cmd.Parameters.Add("@ISSUEDTO", SqlDbType.VarChar).Value = dropissuedto.SelectedValue;
                stock_cmd.Parameters.Add("@MRNO", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = gv1.Cells[2].Text;
                stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = gv1.Cells[3].Text;
              
                stock_cmd.ExecuteNonQuery();
            }

            using (SqlCommand stock_cmd = new SqlCommand("STORE_MIN_Tran", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                stock_cmd.Parameters.Add("@DATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@PURCHES", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@RETN", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@ISSUE", SqlDbType.VarChar).Value = gv1.Cells[2].Text;
                stock_cmd.Parameters.Add("@CLOSING", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@REF", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.ExecuteNonQuery();
            }
        }
        //}
        //catch { }
        con.Close();
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand com = new SqlCommand("STORE_MIN", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT1";
                com.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                com.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
                //SqlCommand com = new SqlCommand("select ID,ISSUEDTO,RECBY,CONVERT TO(VARCHAR(10),MINDATE,105) AS INVDATE from MIN_TABLE where ID='" + slno + "'", con);
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtminnumber.Text = dr["ID"].ToString();
                    txtdate.Text = dr["INVDATE"].ToString();
                    txtrecivedperson.Text = dr["RECBY"].ToString();
                    dropissuedto.Text = dr["ISSUEDTO"].ToString();

                }
                dr.Close();
            }
            using (SqlCommand cmd = new SqlCommand("STORE_MIN", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_2EVENT2";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtminnumber.Text;
                cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
                da = new SqlDataAdapter(cmd);
                //da = new SqlDataAdapter("select NAME,QTY,UNIT FROM MINITEM_TABLE where ID='" + txtminnumber.Text + "'", con);
                DataSet ds2 = new DataSet();
                da.Fill(ds2);
                grvStudentDetails.DataSource = ds2.Tables["Table"];
                grvStudentDetails.DataBind();


                DataTable dt = ds2.Tables["Table"];
                ViewState["ITEM"] = dt;
            }
            using (SqlCommand cmd1 = new SqlCommand("STORE_MIN", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_2EVENT2";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtminnumber.Text;
                cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "";
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                //SqlDataAdapter da1 = new SqlDataAdapter("select NAME,QTY,UNIT FROM MINITEM_TABLE where ID='" + txtminnumber.Text + "'", con);
                DataSet ds3 = new DataSet();
                da1.Fill(ds3);
                GridView1.DataSource = ds3.Tables["Table"];
                GridView1.DataBind();
                btndelete.Visible = true;
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //try
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //    con.Open();
        //    using (SqlCommand cmd1 = new SqlCommand("STORE_MIN", con))
        //    {
        //        cmd1.CommandType = CommandType.StoredProcedure;
        //        cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_GRID_PAGE";
        //        cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtminnumber.Text;
        //        cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = lblfyear.Text;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        //        //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,CONVERT(VARCHAR(10),MINDATE,105) AS DATE FROM MIN_TABLE WHERE   FYEAR='" + lblfyear.Text + "' ORDER BY ID DESC", con);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        GridView2.SelectedIndex = 0;
        //        GridView2.DataSource = dt;
        //        GridView2.PageIndex = e.NewPageIndex;
        //        GridView2.DataKeyNames = new string[] { "ID" };
        //        GridView2.DataBind();
        //    }
        //    con.Close();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);
        //}
    }
    protected void txtitemname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("STORE_MIN", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_TEXT";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = DropMrno.Text;
                cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = txtitemname.Text;
                //SqlCommand com = new SqlCommand("select QTY AS QTY,UNIT from TBLMR_OLDSTOCK where ITEMNAME='" + txtitemname.Text + "'AND MRNO='" + DropMrno.Text + "'", con);
                dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    clearinercontrol();
                    txtunit.Text = dr["UNIT"].ToString();
                    txtqty.Text = dr["QTY"].ToString();
                    dr.Close();
                }


                else
                {
                    dr.Close();
                    string message = "alert('* Incorrect Name.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    
    }
    protected void DropMrno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("STORE_MIN", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DROP_EVENT";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = DropMrno.Text;
                cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = txtitemname.Text;
                da1 = new SqlDataAdapter(cmd1);
                //da1 = new SqlDataAdapter("select distinct ITEMNAME FROM TBLMR_OLDSTOCK WHERE MRNO='" + DropMrno.Text + "'", con);
                DataTable ds1 = new DataTable();
                da1.Fill(ds1);
                txtitemname.DataSource = ds1;
                txtitemname.DataTextField = "ITEMNAME";
                txtitemname.DataValueField = "ITEMNAME";
                txtitemname.DataBind();
                txtitemname.Items.Insert(0, "Please Select");
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtitemname.Text == "")
            {
                string message = "alert('* Select Item First.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtqty.Text == "")
            {
                string message = "alert('* Select Quantity First.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (Convert.ToDecimal(txtqty.Text) <= 0)
            {
                string message = "alert('* Select Quantity First.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtunit.Text == "")
            {
                string message = "alert('* Select unit First.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("STORE_MIN", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ADD";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = DropMrno.Text;
                cmd1.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = txtitemname.Text;
                da1 = new SqlDataAdapter(cmd1);
                //SqlCommand com = new SqlCommand("select QTY from MATERIAL_STOCK_TABLE where  MATERIAL_NAME='" + txtitemname.Text + "'", con);
                dr = cmd1.ExecuteReader();
                decimal QTY1 = 0;
                if (dr.Read())
                {

                    QTY1 = Convert.ToDecimal(dr["QTY"].ToString());
                }
                dr.Close();
                if (QTY1 < Convert.ToDecimal(txtqty.Text))
                {
                    string message = "alert('Insufficient Stock.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                DataTable dt = (DataTable)ViewState["ITEM"];
                dt.Rows.Add(DropMrno.Text.Trim(), txtitemname.Text.Trim(), txtqty.Text.Trim(), txtunit.Text.Trim());
                ViewState["ITEM"] = dt;
                this.BindGrid();
            }

            clearinercontrol();
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
        Response.Redirect("~/STOREKEEPER/MREQUEST.aspx");
    }
}