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

public partial class GENERALSTOCK_StockTransfer : System.Web.UI.Page
{
    string num1 = "0";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
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
                cmd.CommandText = "select DISTINCT NAME from MATERIAL_MASTER_TABLE where NAME like @SearchText+'%'";
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
            string qry1 = "select ID from STOCK_TRANSFER_DEPT";

            com = new SqlCommand(qry1, con);
            dr = null;

            dr = com.ExecuteReader();

            while (dr.Read())
            {
                num1 = dr["ID"].ToString();
            }
            //num1 = string.Format("PU{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
            txttransferno.Text = "ST-" + num1 + 1 + "-" + lblfyear.Text;
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void binddata()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
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
            da1 = new SqlDataAdapter("select id,deptName FROM tblDepartment ", con);
            DataTable ds1 = new DataTable();
            da1.Fill(ds1);
            dropfrdept.DataSource = ds1;
            dropfrdept.DataTextField = "deptName";
            dropfrdept.DataValueField = "id";
            dropfrdept.DataBind();
            dropfrdept.Items.Insert(0, "Please Select");

            da2 = new SqlDataAdapter("select id,deptName FROM tblDepartment ", con);
            DataTable ds2 = new DataTable();
            da2.Fill(ds2);
            droptransfer.DataSource = ds2;
            droptransfer.DataTextField = "deptName";
            droptransfer.DataValueField = "id";
            droptransfer.DataBind();
            droptransfer.Items.Insert(0, "Please Select");

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

            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("NAME"), new DataColumn("UNIT"), new DataColumn("QTY") });
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
    protected void BindGrid()
    {
        try
        {
            grvStTrans.DataSource = (DataTable)ViewState["ITEM"];
            grvStTrans.DataBind();

        }
        catch
        {
        }
    }
   
    protected void txtopening_TextChanged(object sender, EventArgs e)
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //con.Open();

        //SqlCommand com = new SqlCommand("select NAME,HSNCODE,PRICE,GST,GST/2 AS GST2,UNIT from MATERIAL_MASTER_TABLE where NAME='" + txtitemname.Text + "'", con);
        //dr = com.ExecuteReader();
        //if (dr.Read())
        //{
        //    //clearinercontrol();
        //    txthsncode.Text = dr["HSNCODE"].ToString();
        //    txtitemname.Text = dr["NAME"].ToString();
        //    txtpprice.Text = dr["PRICE"].ToString();
        //    txtunit.Text = dr["UNIT"].ToString();
        //    if (txtstatecode.Text == "21")
        //    {
        //        txtcgst.Text = dr["GST2"].ToString();
        //        txtSgst.Text = dr["GST2"].ToString();
        //    }
        //    else
        //    {
        //        txtIGST.Text = dr["GST"].ToString();
        //    }


        //    dr.Close();
        //    try
        //    {
        //        dis = 0;
        //        dism = 0;
        //        totalamt1 = 0;
        //        totamt = 0;
        //        totalgstamt = 0;
        //        //txtdiscamt.Text = Math.Round((Convert.ToDouble(txtopening.Text)) - (Convert.ToDouble(txtfeeqty.Text)) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtdisc.Text)) / 100).ToString();
        //        txtamount.Text = Math.Round((Convert.ToDouble(txtopening.Text)) * Convert.ToDouble(txtpprice.Text)).ToString();

        //        if (txtstatecode.Text == "21")
        //        {
        //            txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text))) * ((Convert.ToDouble(txtcgst.Text)) + (Convert.ToDouble(txtSgst.Text)))) / 100).ToString();
        //        }
        //        else
        //        {
        //            txtgstamount.Text = Math.Round((((Convert.ToDouble(txtopening.Text)) * (Convert.ToDouble(txtpprice.Text)) * (Convert.ToDouble(txtIGST.Text)))) / 100).ToString();
        //        }

        //        txttotalamount.Text = Math.Round((Convert.ToDouble(txtamount.Text) + Convert.ToDouble(txtgstamount.Text))).ToString();
        //    }
        //    catch
        //    {
        //    }

        //}

        //else
        //{
        //    dr.Close();
        //    string message = "alert('* Incorrect Name.')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //    return;
        //}
        //con.Close();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtitemname.Text == "")
            {
                string message = "alert('Please!! Enter The Item name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtqty.Text == "" || txtqty.Text == "0")
            {
                string message = "alert('Please!! Enter The Quantity. ')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtunit.Text == "")
            {
                string message = "alert('Please!! Enter The Unit.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            DataTable dt = (DataTable)ViewState["ITEM"];
            dt.Rows.Add(txtitemname.Text.Trim(), txtunit.Text.Trim(), txtqty.Text.Trim());
            ViewState["ITEM"] = dt;
            this.BindGrid();

            con.Close();
            txtitemname.Text = "";
            txtqty.Text = "";
            txtunit.Text = "";
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
       // btncreate.Visible = true;
      //  btncancel.Visible = true;
    }
    public void STOCK_TABLE()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            DataTable dt1 = ViewState["ITEM"] as DataTable;
            GridView1.DataSource = dt1;
            GridView1.DataBind();

            foreach (GridViewRow gv1 in grvStTrans.Rows)
            {
                var nameGr = (gv1.FindControl("lbl_name") as Label);
                var unitGr = (gv1.FindControl("lbl_unit") as Label);
                var quantyGr = (gv1.FindControl("lbl_qty") as Label);

                using (SqlCommand stock_cmd = new SqlCommand("STOCK_TRANSFER_OPERATION", con))
                {
                    stock_cmd.CommandType = CommandType.StoredProcedure;
                    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
                    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                    stock_cmd.Parameters.Add("@TRANSFER_NO", SqlDbType.VarChar).Value = txttransferno.Text;
                    stock_cmd.Parameters.Add("@FROM_DEPT", SqlDbType.VarChar).Value = dropfrdept.SelectedValue;
                    stock_cmd.Parameters.Add("@TRANSFER_TO", SqlDbType.VarChar).Value = droptransfer.SelectedValue;
                    stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");

                    stock_cmd.Parameters.Add("@ITEM_NAME", SqlDbType.VarChar).Value = nameGr.Text;
                    stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitGr.Text;
                    stock_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = quantyGr.Text;
                    stock_cmd.ExecuteNonQuery();
                }
                using (SqlCommand stock_cmd = new SqlCommand("DEPT_MIN_Tran", con))
                {
                    stock_cmd.CommandType = CommandType.StoredProcedure;
                    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = gv1.Cells[0].Text;
                    stock_cmd.Parameters.Add("@OPENING", SqlDbType.Decimal).Value = "0.00";
                    stock_cmd.Parameters.Add("@GBST", SqlDbType.Decimal).Value = "0.00";
                    stock_cmd.Parameters.Add("@GRST", SqlDbType.Decimal).Value = "0.00";
                    stock_cmd.Parameters.Add("@ISSBDT", SqlDbType.Decimal).Value = gv1.Cells[1].Text;
                    stock_cmd.Parameters.Add("@ISSRDT", SqlDbType.Decimal).Value = "0.00";
                    stock_cmd.Parameters.Add("@CLOSING", SqlDbType.Decimal).Value = "0.00";
                    stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = dropfrdept.SelectedValue;
                    stock_cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        //}
        //catch { }
    }
  
    protected void Btncreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdate.Text == "")
            {
                string message = "alert('Please!! Enter The Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (dropfrdept.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Department.. ')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (droptransfer.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Department Transfer To..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (grvStTrans.Rows.Count <= 0)
            {

                string message = "alert('*Add Item to Save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand stock_cmd = new SqlCommand("STOCK_TRANSFER_OPERATION", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@TRANSFER_NO", SqlDbType.VarChar).Value = txttransferno.Text;
                stock_cmd.Parameters.Add("@FROM_DEPT", SqlDbType.VarChar).Value = dropfrdept.SelectedValue;
                stock_cmd.Parameters.Add("@TRANSFER_TO", SqlDbType.VarChar).Value = droptransfer.SelectedValue;
                stock_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");

                stock_cmd.Parameters.Add("@ITEM_NAME", SqlDbType.VarChar).Value = "";
                stock_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
                stock_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = 0.00;
                stock_cmd.ExecuteNonQuery();
            }
            STOCK_TABLE();
            con.Close();
            string message1 = "alert('Inserted Successfully.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("StockTransfer.aspx");
    }
    protected void txtitemname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            //SqlCommand com = new SqlCommand("select ID, UNIT,NAME from MATERIAL_MASTER_TABLE where NAME='" + txtitemname.Text + "'", con);
            using (SqlCommand com = new SqlCommand("DTSTOCK_MATSTOCKTRANS", con))
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
                    txtunit.Text = dr["UNIT"].ToString();
                }
            }
            con.Close();

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void dropfrdept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            da2 = new SqlDataAdapter("select id,deptName FROM tblDepartment where id != '" + dropfrdept.SelectedValue + "'", con);
            DataTable ds2 = new DataTable();
            da2.Fill(ds2);
            droptransfer.DataSource = ds2;
            droptransfer.DataTextField = "deptName";
            droptransfer.DataValueField = "id";
            droptransfer.DataBind();
            droptransfer.Items.Insert(0, "Please Select");
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/GENERALSTOCK/StockTransfer.aspx");
    }
    protected void grvStTrans_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("lbl_slno") as Label).Text = (e.Row.RowIndex + 1).ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grvStTrans_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        //int index = Convert.ToInt32(e.RowIndex);
        //DataTable dt = (DataTable)ViewState["ITEM"];
        //GridViewRow row = (GridViewRow)grvStTrans.Rows[e.RowIndex];
        //dt.Rows[index].Delete();
        //ViewState["ITEM"] = dt;
        //this.BindGrid();
    }
}