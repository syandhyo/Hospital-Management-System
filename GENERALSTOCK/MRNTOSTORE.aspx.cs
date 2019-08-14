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

public partial class STOREKEEPER_DeptMRN : System.Web.UI.Page
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
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            string qry1 = "select slno from MRN_STORE_TABLE";

            com = new SqlCommand(qry1, con);
            dr = null;

            dr = com.ExecuteReader();

            while (dr.Read())
            {
                num1 = dr["slno"].ToString();
            }
            //num1 = string.Format("PU{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
            txtmrnno.Text = "SMRN-" + num1 + 1 + "-" + lblfyear.Text;
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
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
    public void binddata()
    {
        try
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

            da = new SqlDataAdapter("select distinct DeptName,id FROM tblDepartment", con);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dropreturnto.DataSource = ds;
            dropreturnto.DataTextField = "DeptName";
            dropreturnto.DataValueField = "id";
            dropreturnto.DataBind();
            dropreturnto.Items.Insert(0, "Please Select");

            da1 = new SqlDataAdapter("select distinct ITEMNAME FROM TBL_DEPT_MAT_MST", con);
            DataTable ds1 = new DataTable();
            da1.Fill(ds1);
            txtitemname.DataSource = ds1;
            txtitemname.DataTextField = "ITEMNAME";
            txtitemname.DataValueField = "ITEMNAME";
            txtitemname.DataBind();
            txtitemname.Items.Insert(0, "Please Select");

            SqlDataAdapter da2 = new SqlDataAdapter("select * from MRN_STORE_TABLE", con);
            DataTable dt = new DataTable();
            da2.Fill(dt);
            GridView2.SelectedIndex = 0;
            GridView2.DataSource = dt;
            GridView2.DataKeyNames = new string[] { "ID" };
            GridView2.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
            //TextBox1.Text = Session["MRINDID"].ToString();

            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("NAME"), new DataColumn("QTY"), new DataColumn("UNIT") });
                ViewState["ITEM"] = dt;
                this.BindGrid();
                binddata();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
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
        txtitemname.SelectedIndex = 0;
        txtunit.Text = "";
        txtqty.Text = "0";
        //txtitemname.Text = "";
    }
    //protected void btnadd_Click(object sender, ImageClickEventArgs e)
    //{
    //    if (txtitemname.SelectedIndex == 0)
    //    {
    //        string message = "alert('* Select Item First.')";
    //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
    //        return;
    //    }

    //    else if (txtqty.Text == "")
    //    {
    //        string message = "alert('* Select Quantity First.')";
    //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
    //        return;
    //    }
    //    else if (Convert.ToDecimal(txtqty.Text) <= 0)
    //    {
    //        string message = "alert('* Select Quantity First.')";
    //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
    //        return;
    //    }

    //    else if (txtunit.Text == "")
    //    {
    //        string message = "alert('* Select unit First.')";
    //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
    //        return;
    //    }
    //    SqlCommand com = new SqlCommand("select QTY from MATERIALSTOCK_TABLE where  NAME='" + txtitemname.Text + "'", con);
    //    dr = com.ExecuteReader();
    //    decimal QTY1 = 0;
    //    if (dr.Read())
    //    {

    //        QTY1 = Convert.ToDecimal(dr["QTY"].ToString());
    //    }
    //    dr.Close();
    //    if (QTY1 < Convert.ToDecimal(txtqty.Text))
    //    {
    //        string message = "alert('Insufficient Stock.')";
    //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
    //        return;
    //    }
    //    DataTable dt = (DataTable)ViewState["ITEM"];
    //    dt.Rows.Add(txtitemname.Text.Trim(), txtqty.Text.Trim(), txtunit.Text.Trim());
    //    ViewState["ITEM"] = dt;
    //   // this.BindGrid();

    //    clearinercontrol();
    //}
    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = (DataTable)ViewState["ITEM"];
            GridViewRow row = (GridViewRow)grvStudentDetails.Rows[e.RowIndex];
            dt.Rows[index].Delete();
            ViewState["ITEM"] = dt;
            this.BindGrid();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtmrndate.Text == "")
            {
                string message = "alert('* Insert Mrn Date.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtmrndate.Focus();
                return;
            }
            else if (dropreturnto.SelectedIndex == 0)
            {
                string message = "alert('* Select Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropreturnto.Focus();
                return;
            }

            else if (txtrecivedby.Text == "")
            {
                string message = "alert('* Insert personname.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtrecivedby.Focus();
                return;
            }

            //else if (grvStudentDetails.Rows.Count <= 0)
            //{

            //    string message = "alert('*Add Item to Save.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            SqlCommand cm = new SqlCommand("select * from MRN_DEPT_TABLE where ID ='" + txtmrnno.Text + "'", con);
            dr = cm.ExecuteReader();
            if (dr.Read())
            {
                string message = "alert('*Cant enter duplicate MRN number.MRN number alredy exist.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            auto();

            dr.Close();
            STOCK_TABLE();
            STOCK_TRAN();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/GENERALSTOCK/MRNTOSTORE.aspx");
    }
    public void STOCK_TABLE()
    {
        try
        {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand stock_cmd = new SqlCommand("DEPT_MRN_OPERATION_store", con))
        {
            stock_cmd.CommandType = CommandType.StoredProcedure;
            stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            stock_cmd.Parameters.Add("@AUTOID", SqlDbType.VarChar).Value = txtmrnno.Text;
            stock_cmd.Parameters.Add("@SID ", SqlDbType.VarChar).Value = "";
            stock_cmd.Parameters.Add("@MRNDATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtmrndate.Text).ToString("yyyy-MM-dd");
            stock_cmd.Parameters.Add("@RECBY", SqlDbType.VarChar).Value = txtrecivedby.Text;
            stock_cmd.Parameters.Add("@RETURN_TO", SqlDbType.VarChar).Value = dropreturnto.SelectedValue;
            stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
            stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = "";
            stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
            stock_cmd.ExecuteNonQuery();
        }
        con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void STOCK_TRAN()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            DataTable dt1 = ViewState["ITEM"] as DataTable;
            GridView1.DataSource = dt1;
            GridView1.DataBind();

            foreach (GridViewRow gv1 in GridView1.Rows)
            {
                using (SqlCommand stock_cmd = new SqlCommand("DEPT_MRN_OPERATION_store", con))
                {
                    stock_cmd.CommandType = CommandType.StoredProcedure;
                    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
                    stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtmrnno.Text;
                    stock_cmd.Parameters.Add("@SID ", SqlDbType.VarChar).Value = "";
                    stock_cmd.Parameters.Add("@MRNDATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtmrndate.Text).ToString("yyyy-MM-dd");
                    stock_cmd.Parameters.Add("@RECBY", SqlDbType.VarChar).Value = txtrecivedby.Text;
                    stock_cmd.Parameters.Add("@RETURN_TO", SqlDbType.VarChar).Value = dropreturnto.SelectedValue;
                    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                    stock_cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                    stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = gv1.Cells[2].Text;
                    stock_cmd.ExecuteNonQuery();
                }
                using (SqlCommand stock_cmd = new SqlCommand("DEPT_MRN_Tran_STORE", con))
                {
                    stock_cmd.CommandType = CommandType.StoredProcedure;
                    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    stock_cmd.Parameters.Add("@DATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtmrndate.Text).ToString("yyyy-MM-dd");
                    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                    stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0.00";
                    stock_cmd.Parameters.Add("@GBST", SqlDbType.VarChar).Value = "0.00";
                    stock_cmd.Parameters.Add("@GRST", SqlDbType.VarChar).Value = gv1.Cells[1].Text;
                    stock_cmd.Parameters.Add("@ISSBDT", SqlDbType.VarChar).Value = "0.00";
                    stock_cmd.Parameters.Add("@ISSRDT", SqlDbType.VarChar).Value = "0.00";
                    stock_cmd.Parameters.Add("@CLOSING", SqlDbType.VarChar).Value = "0.00";
                    stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = dropreturnto.SelectedValue;
                    stock_cmd.ExecuteNonQuery();
                }
            }

            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
           // SqlCommand com = new SqlCommand("select AUTOID,CONVERT(VARCHAR(10),MRNDATE,105) AS MRNDATE,RETURN_TO,RECBY from MRN_STORE_TABLE where ID='" + slno + "'", con);
          //  dr = com.ExecuteReader();
            using (SqlCommand com = new SqlCommand("DTSTOCK_MATMRNTOSRORE", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
                com.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                com.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                com.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    btncreate.Visible = false;
                    // btnupdate.Visible = true;
                    txtmrnno.Text = dr["AUTOID"].ToString();
                    txtmrndate.Text = dr["MRNDATE"].ToString();
                    txtrecivedby.Text = dr["RECBY"].ToString();
                    dropreturnto.Text = dr["RETURN_TO"].ToString();
                }
                dr.Close();
            }
           // da = new SqlDataAdapter("select NAME,QTY,UNIT FROM MRN_ITEM_DEPT_TABLE where ID='" + txtmrnno.Text + "'", con);
            using (SqlCommand cmd = new SqlCommand("DTSTOCK_MATMRNTOSRORE", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECTITEM";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtmrnno.Text;
                cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
                // SqlDataReader dr = com.ExecuteReader();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                da.Fill(ds2);
                grvStudentDetails.DataSource = ds2.Tables["Table"];
                grvStudentDetails.DataBind();


                DataTable dt = ds2.Tables["Table"];
                ViewState["ITEM"] = dt;
            }

            //SqlDataAdapter da1 = new SqlDataAdapter("select NAME,QTY,UNIT FROM MRN_ITEM_DEPT_TABLE where ID='" + txtmrnno.Text + "'", con);
            using (SqlCommand cmd1 = new SqlCommand("DTSTOCK_MATMRNTOSRORE", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECTITEM";
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtmrnno.Text;
                cmd1.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
                // SqlDataReader dr = com.ExecuteReader();
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds3 = new DataSet();
                da1.Fill(ds3);
                GridView1.DataSource = ds3.Tables["Table"];
                GridView1.DataBind();
            }
            btndelete.Visible = true;
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
           // SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,CONVERT(VARCHAR(10),MRNDATE,105) AS DATE FROM MRN_DEPT_TABLE WHERE   FYEAR='" + lblfyear.Text + "' ORDER BY ID DESC", con);
            using (SqlCommand cmd1 = new SqlCommand("DTSTOCK_MATMRNTOSRORE", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INDEXING";
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                // SqlDataReader dr = com.ExecuteReader();  
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //  GridView2.SelectedIndex = 0;
                GridView2.DataSource = dt;
                GridView2.PageIndex = e.NewPageIndex;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void txtitemname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            //SqlCommand com = new SqlCommand("select  QUANTITY,UNIT from TBL_DEPT_MAT_MST where ITEMNAME='" + txtitemname.SelectedItem.Text + "'", con);
            using (SqlCommand com = new SqlCommand("DTSTOCK_MATMRNTOSRORE", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "NMTEXTCHG";
                com.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtitemname.Text;
                com.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                com.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
                SqlDataReader dr = com.ExecuteReader();
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    //clearinercontrol();
                    txtunit.Text = dr["UNIT"].ToString();
                    //txtqty.Text = dr["QTY"].ToString();
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
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtitemname.Text == "")
            {
                string message = "alert('* Select Item First.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtitemname.Focus();
                return;
            }
            else if (txtqty.Text == "")
            {
                string message = "alert('* Select Quantity First.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtqty.Focus();
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
                txtunit.Focus();
                return;
            } SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand com = new SqlCommand("select STOCK from TBL_DEPT_MAT_STOCK where  ITEMNAME='" + txtitemname.Text + "'", con);
            dr = com.ExecuteReader();
            decimal QTY1 = 0;
            if (dr.Read())
            {

                QTY1 = Convert.ToDecimal(dr["STOCK"].ToString());
            }
            dr.Close();
            if (QTY1 < Convert.ToDecimal(txtqty.Text))
            {
                string message = "alert('Insufficient Stock.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            DataTable dt = (DataTable)ViewState["ITEM"];
            dt.Rows.Add(txtitemname.Text.Trim(), txtqty.Text.Trim(), txtunit.Text.Trim());
            ViewState["ITEM"] = dt;
            this.BindGrid();

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
        Response.Redirect("~/GENERALSTOCK/MRNTOSTORE.aspx");
    }
}