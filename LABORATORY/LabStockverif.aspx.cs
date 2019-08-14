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

public partial class LABORATORY_LabStockverif : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    GridViewRow gr;
    string PAIDMAT;
    decimal amount = 0;

    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from LAB_STOCKVERF";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("LS{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;
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
     //   txtinvdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        if (!IsPostBack)
        {
            binddata();
        }
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
        using (SqlCommand cmd = new SqlCommand("LAB_STOCK_VERIFICATION", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_LABSTC_PAGE";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            //SqlDataAdapter da1 = new SqlDataAdapter("select A.ID ,A.DATE,A.ITEM,A.BATCHNO,A.EXPIRY,A.PACKTYPE,A.NOOFPACK,A.CLOSESTOCK,A.PHYSICALSTOCK,A.REASON,B.ITEMNAME,B.SLNO from LAB_STOCKVERF A,TBL_DEPT_MAT_STOCK B WHERE A.ITEM=B.SLNO ORDER BY A.ID desc", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            grvlabst.SelectedIndex = 0;
            grvlabst.DataSource = dt1;
            grvlabst.DataKeyNames = new string[] { "ID" };
            grvlabst.DataBind();
        }
        //-----------------------------

        using (SqlCommand cmd = new SqlCommand("LAB_STOCK_VERIFICATION", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DEPT_MAT";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da = new SqlDataAdapter("select distinct SLNO,ITEMNAME from TBL_DEPT_MAT_STOCK", con);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dropitem.DataSource = ds;
            dropitem.DataTextField = "ITEMNAME";
            dropitem.DataValueField = "SLNO";
            dropitem.DataBind();
            dropitem.Items.Insert(0, "Please Select");
        }

        con.Close();
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdate.Text == "")
            {
                string message = "alert('*Date Is Mandatory..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (dropitem.SelectedIndex == 0)
            {
                string message = "alert('*Item Field Is Mandatory..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtbatchno.Text == "")
            {
                string message = "alert('*Batch Number Is Mandatory..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtexpiry.Text == "")
            {
                string message = "alert('Please!! Enter The Expiry Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtpacktype.Text == "")
            {
                string message = "alert('Please!! Enter Type Of Packaging..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtnopacking.Text == "")
            {
                string message = "alert('Please!! Enter Number Of Packing..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtphystck.Text == "")
            {
                string message = "alert('Please!! Enter Number Of Physical Stock..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtreson.Text == "")
            {
                string message = "alert('Please!! Enter Reason..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand cm = new SqlCommand("USP_LAB_STOCKVERF", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
                cm.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = dropitem.SelectedValue;
                cm.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = txtbatchno.Text;
                cm.Parameters.Add("@EXPIRY", SqlDbType.DateTime).Value = Convert.ToDateTime(txtexpiry.Text).ToString("yyyy-MM-dd HH:mm");
                //cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LAB BILL " + droptesttype.SelectedItem.Text;
                cm.Parameters.Add("@PACKTYPE", SqlDbType.VarChar).Value = txtpacktype.Text;
                cm.Parameters.Add("@NOOFPACK", SqlDbType.VarChar).Value = txtnopacking.Text;
                cm.Parameters.Add("@CLOSESTOCK", SqlDbType.VarChar).Value = txtclostock.Text;
                cm.Parameters.Add("@PHYSICALSTOCK", SqlDbType.VarChar).Value = txtphystck.Text;
                cm.Parameters.Add("@REASON", SqlDbType.VarChar).Value = txtreson.Text;
                cm.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                cm.ExecuteNonQuery();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/LABORATORY/LabStockverif.aspx");
    }

    protected void grvlabst_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = grvlabst.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("LAB_STOCK_VERIFICATION", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlCommand com = new SqlCommand("select ID ,DATE,ITEM,BATCHNO,EXPIRY,PACKTYPE,NOOFPACK,CLOSESTOCK,PHYSICALSTOCK,REASON from LAB_STOCKVERF  where ID='" + slno + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtselid.Text = slno.ToString();
                    txtdate.Text = dr["DATE"].ToString();
                    dropitem.Text = dr["ITEM"].ToString();
                    txtbatchno.Text = dr["BATCHNO"].ToString();
                    txtexpiry.Text = dr["EXPIRY"].ToString();
                    txtpacktype.Text = dr["PACKTYPE"].ToString();
                    txtnopacking.Text = dr["NOOFPACK"].ToString();
                    txtclostock.Text = dr["CLOSESTOCK"].ToString();
                    txtphystck.Text = dr["PHYSICALSTOCK"].ToString();
                    txtreson.Text = dr["REASON"].ToString();
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
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/LABORATORY/LabStockverif.aspx");
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdate.Text == "")
            {
                string message = "alert('*Date Is Mandatory..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (dropitem.SelectedIndex == 0)
            {
                string message = "alert('*Item Field Is Mandatory..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtbatchno.Text == "")
            {
                string message = "alert('*Batch Number Is Mandatory..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtexpiry.Text == "")
            {
                string message = "alert('Please!! Enter The Expiry Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtpacktype.Text == "")
            {
                string message = "alert('Please!! Enter Type Of Packaging..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtnopacking.Text == "")
            {
                string message = "alert('Please!! Enter Number Of Packing..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtphystck.Text == "")
            {
                string message = "alert('Please!! Enter Number Of Physical Stock..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtreson.Text == "")
            {
                string message = "alert('Please!! Enter Reason..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand cm = new SqlCommand("USP_LAB_STOCKVERF", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtselid.Text;
                cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
                cm.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = dropitem.SelectedValue;
                cm.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = txtbatchno.Text;
                cm.Parameters.Add("@EXPIRY", SqlDbType.DateTime).Value = Convert.ToDateTime(txtexpiry.Text).ToString("yyyy-MM-dd HH:mm");
                //cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LAB BILL " + droptesttype.SelectedItem.Text;
                cm.Parameters.Add("@PACKTYPE", SqlDbType.VarChar).Value = txtpacktype.Text;
                cm.Parameters.Add("@NOOFPACK", SqlDbType.VarChar).Value = txtnopacking.Text;
                cm.Parameters.Add("@CLOSESTOCK", SqlDbType.VarChar).Value = txtclostock.Text;
                cm.Parameters.Add("@PHYSICALSTOCK", SqlDbType.VarChar).Value = txtphystck.Text;
                cm.Parameters.Add("@REASON", SqlDbType.VarChar).Value = txtreson.Text;
                cm.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                cm.ExecuteNonQuery();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/LABORATORY/LabStockverif.aspx");
    }
    protected void grvlabst_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[5].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    protected void grvlabst_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        string slno = grvlabst.DataKeys[e.RowIndex].Values["ID"].ToString();
        using (SqlCommand cmd = new SqlCommand("LAB_STOCK_VERIFICATION", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //SqlCommand cm = new SqlCommand("delete from LAB_STOCKVERF where ID='" + slno + "'", con);
            cmd.ExecuteNonQuery();
        }
        binddata();
        con.Close();
    }
    protected void grvlabst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("LAB_STOCK_VERIFICATION", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_LABSTC_PAGE";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
                SqlDataAdapter Adp = new SqlDataAdapter(cmd);
                //SqlDataAdapter Adp = new SqlDataAdapter("select * from LAB_STOCKVERF", con);
                DataTable dt = new DataTable();
                Adp.Fill(dt);
                grvlabst.DataSource = dt;
                grvlabst.PageIndex = e.NewPageIndex;
                grvlabst.DataKeyNames = new string[] { "ID" };
                grvlabst.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void dropitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("LAB_STOCK_VERIFICATION", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ITEM";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = dropitem.SelectedItem.Text;
                //SqlDataAdapter Adp = new SqlDataAdapter(cmd);
                //SqlCommand com = new SqlCommand("select distinct SLNO,ITEMNAME,STOCK from TBL_DEPT_MAT_STOCK  where ITEMNAME='" + dropitem.SelectedItem.Text + "'", con);
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    txtclostock.Text = dr["STOCK"].ToString();
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
}