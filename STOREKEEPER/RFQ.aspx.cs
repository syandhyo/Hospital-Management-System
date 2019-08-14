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

public partial class STOREKEEPER_RFQ : System.Web.UI.Page
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
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select max(RFQID) as ID from RFQ_TABLE";

        //com = new SqlCommand(qry1, con);
        //dr = null;

        //dr = com.ExecuteReader();

        //while (dr.Read())
        //{
        //    num1 = dr["ID"].ToString();
        //}
        ////num1 = string.Format("PU{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        //txtContrtno.Text = "RFQ-" + num1 + 1 + "-" + lblfyear.Text;
        //dr.Close();
        //--------------------------------------------------
        com = new SqlCommand(qry1, con);
        dr = null;
        dr = com.ExecuteReader();
        string str1 = "1";
        if (dr.Read() && dr["ID"].ToString() != "")
        {          
            num1 = dr["ID"].ToString();          
            string str = num1.Substring(0, num1.Length - 10);//delete last 10 record
            string d = str.Substring(4);//delete first 3 record
            str1 = (Convert.ToInt64(d) + 1).ToString();//add in numbers
        }       
        //num1 = string.Format("INV{0}", (Convert.ToUInt64(num1.Substring(3)) + 1).ToString("D4"));
        txtContrtno.Text = "RFQ-" + str1 + "-" + lblfyear.Text;

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

        da1 = new SqlDataAdapter("select distinct NAME1,ID from VENDER_MASTER_TABLE ", con);
        DataTable ds1 = new DataTable();
        da1.Fill(ds1);
        dropvendor.DataSource = ds1;
        dropvendor.DataTextField = "NAME1";
        dropvendor.DataValueField = "ID";
        dropvendor.DataBind();
        dropvendor.Items.Insert(0, "Please Select");
        //-----------------------------------------------
        SqlCommand comid = new SqlCommand("select max(RFQID) as rfqid from RFQ_TABLE ", con);
        dr1 = comid.ExecuteReader();
        if (dr1.Read())
        {
            txtrfqid.Text = dr1["rfqid"].ToString();
        }
        dr1.Close();
        //------------FOR GRIDVIEW DATASHOW-----------------------//
       // SqlDataAdapter Adp = new SqlDataAdapter("select CONVERT(varchar, a.DATE, 105) DATE,a.RFQID,b.NAME1 from RFQ_TABLE a,VENDER_MASTER_TABLE b where a.VENDORID=b.ID ORDER BY a.RFQID DESC", con);

        using (SqlCommand cmd1 = new SqlCommand("SP_RFQ_PAGING", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
            SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
            DataTable Dt = new DataTable();
            Adp.Fill(Dt);
            grdrfq.DataSource = Dt;
            grdrfq.DataBind();
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
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        dropvendor.Focus();
        if (!IsPostBack)
        {
            binddata();
            bindTGrid();           
        }
        con.Close();
    }
    public void bindTGrid()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[3] { new DataColumn("NAME"), new DataColumn("UNIT"), new DataColumn("QTY") });
        ViewState["ITEM"] = dt;
        this.BindGrid();
    }
    protected void BindGrid()
    {
        try
        {
            grdMaterial.DataSource = (DataTable)ViewState["ITEM"];
            grdMaterial.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            DataTable dt = (DataTable)ViewState["ITEM"];
            dt.Rows.Add(txtMaterial.Text.Trim(), txtUnit.Text.Trim(), txtqty.Text.Trim());
            ViewState["ITEM"] = dt;
            this.BindGrid();

            con.Close();
            txtMaterial.Text = "";
            txtqty.Text = "";
            txtUnit.Text = "";
        }
        catch (Exception ex)
        {
            //Console.WriteLine("An error occurred: '{0}'", ex);
            Trace.Write(ex.Message);
        }
    }
    protected void grdMaterial_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = (DataTable)ViewState["ITEM"];
            GridViewRow row = (GridViewRow)grdMaterial.Rows[e.RowIndex];

            Label nameit = (Label)row.FindControl("lbl_Name");
            Label unitit = (Label)row.FindControl("lbl_Unit");
            Label quantityit = (Label)row.FindControl("lbl_Qty");

            string NAME = nameit.Text.ToString();
            string UNIT = unitit.Text.ToString();
            string QTY = quantityit.Text.ToString();

            dt.Rows[index].Delete();
            ViewState["ITEM"] = dt;
            //  lblgrandtotal.Text = (Convert.ToDecimal(lblgrandtotal.Text) - Convert.ToDecimal(TOTALAMT)).ToString();

            this.BindGrid();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void txtMaterial_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            SqlCommand com = new SqlCommand("select ID, UNIT,NAME from MATERIAL_MASTER_TABLE where NAME='" + txtMaterial.Text + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                txtUnit.Text = dr["UNIT"].ToString();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropvendor.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Vendor..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (grdMaterial.Rows.Count <= 0)
            {

                string message = "alert('*Add item to Save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand CM_cmd = new SqlCommand("USP_RFQ", con))
            {
                CM_cmd.CommandType = CommandType.StoredProcedure;
                CM_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";

                CM_cmd.Parameters.Add("@RFQID", SqlDbType.VarChar).Value = txtContrtno.Text;
                CM_cmd.Parameters.Add("@VENDORID", SqlDbType.VarChar).Value = dropvendor.SelectedValue;
                CM_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                CM_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                CM_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "";
                CM_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
                CM_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = 0.00;

                CM_cmd.ExecuteNonQuery();
            }
            ItemCM();
            con.Close();
            binddata();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/STOREKEEPER/RFQ.aspx");
    }
    public void ItemCM()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        foreach (GridViewRow gv1 in grdMaterial.Rows)
        {
            //var lbname = (gv1.FindControl("chkRow") as CheckBox);
            var nameGr = (gv1.FindControl("lbl_Name") as Label);
            var unitGr = (gv1.FindControl("lbl_Unit") as Label);
            var quantyGr = (gv1.FindControl("lbl_Qty") as Label);

            using (SqlCommand MRitem_cmd = new SqlCommand("USP_RFQ", con))
            {
                MRitem_cmd.CommandType = CommandType.StoredProcedure;
                MRitem_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
                MRitem_cmd.Parameters.Add("@RFQID", SqlDbType.VarChar).Value = txtContrtno.Text;
                MRitem_cmd.Parameters.Add("@VENDORID", SqlDbType.VarChar).Value = dropvendor.SelectedValue;
                MRitem_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                MRitem_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                MRitem_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = nameGr.Text;
                MRitem_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitGr.Text;
                MRitem_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = quantyGr.Text;

                MRitem_cmd.ExecuteNonQuery();
            }
        }
        con.Close();
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/STOREKEEPER/RFQ.aspx");
    }
    protected void grdrfq_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = grdrfq.DataKeys[e.NewSelectedIndex].Values["RFQID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand com = new SqlCommand("select RFQID,CONVERT(varchar, DATE, 105) DATE,VENDORID from RFQ_TABLE where  RFQID='" + slno + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                btncancel.Visible = true;
                btndelete.Visible = true;
                lblEditgrd.Text = slno.ToString();
                txtdate.Text = dr["DATE"].ToString();
                txtrfqid.Text = dr["RFQID"].ToString();
                dropvendor.Text = dr["VENDORID"].ToString();

                dr.Close();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("select ITEMNAME NAME,UNIT,QTY  from RFQ_ITEM_TABLE where RFQID='" + slno + "'", con);
                da.Fill(dt);
                grdMaterial.DataSource = dt;
                // grdrfq.DataKeyNames = new string[] { "ID" };
                grdMaterial.DataBind();
                // DataTable dt = ds2.Tables["Table"];
                ViewState["ITEM"] = dt;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            // Validation
            if (dropvendor.SelectedItem.Text == "")
            {
                string message = "alert('* Please Select Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            using (SqlCommand CM_cmd = new SqlCommand("USP_RFQ", con))
            {
                CM_cmd.CommandType = CommandType.StoredProcedure;
                CM_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";

                CM_cmd.Parameters.Add("@RFQID", SqlDbType.VarChar).Value = lblEditgrd.Text;
                CM_cmd.Parameters.Add("@VENDORID", SqlDbType.VarChar).Value = dropvendor.SelectedValue;
                CM_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                CM_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                CM_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "";
                CM_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
                CM_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = 0.00;
                CM_cmd.ExecuteNonQuery();
            }
            ItemCMUpd();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/STOREKEEPER/RFQ.aspx");
    }
    public void ItemCMUpd()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        foreach (GridViewRow gv1 in grdMaterial.Rows)
        {
            //var lbname = (gv1.FindControl("chkRow") as CheckBox);
            var nameGr = (gv1.FindControl("lbl_Name") as Label);
            var unitGr = (gv1.FindControl("lbl_Unit") as Label);
            var quantyGr = (gv1.FindControl("lbl_Qty") as Label);

            using (SqlCommand MRitem_cmd = new SqlCommand("USP_RFQ", con))
            {
                MRitem_cmd.CommandType = CommandType.StoredProcedure;
                MRitem_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDUPDATE";
                MRitem_cmd.Parameters.Add("@RFQID", SqlDbType.VarChar).Value = lblEditgrd.Text;
                MRitem_cmd.Parameters.Add("@VENDORID", SqlDbType.VarChar).Value = dropvendor.SelectedValue;
                MRitem_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                MRitem_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                MRitem_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = nameGr.Text;
                MRitem_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitGr.Text;
                MRitem_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = quantyGr.Text;

                MRitem_cmd.ExecuteNonQuery();
            }
        }
        con.Close();
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand CM_cmd = new SqlCommand("USP_RFQ", con))
            {
                CM_cmd.CommandType = CommandType.StoredProcedure;
                CM_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";

                CM_cmd.Parameters.Add("@RFQID", SqlDbType.VarChar).Value = lblEditgrd.Text;
                CM_cmd.Parameters.Add("@VENDORID", SqlDbType.VarChar).Value = dropvendor.SelectedValue;
                CM_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                CM_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                CM_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "";
                CM_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
                CM_cmd.Parameters.Add("@QTY", SqlDbType.Decimal).Value = 0.00;
                CM_cmd.ExecuteNonQuery();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/STOREKEEPER/RFQ.aspx");
    }
    protected void grdrfq_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //SqlDataAdapter adp = new SqlDataAdapter("select CONVERT(varchar, a.DATE, 105) DATE,a.RFQID,b.NAME1 from RFQ_TABLE a,VENDER_MASTER_TABLE b where a.VENDORID=b.ID ORDER BY a.RFQID DESC", con);
            using (SqlCommand cmd1 = new SqlCommand("SP_RFQ_PAGING", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
                SqlDataAdapter adp = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                adp.Fill(dt1);
                grdrfq.DataSource = dt1;
                grdrfq.PageIndex = e.NewPageIndex;
                // grdrfq.DataKeyNames = new string[] { "id" };
                grdrfq.DataKeyNames = new string[] { "RFQID" };
                grdrfq.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}