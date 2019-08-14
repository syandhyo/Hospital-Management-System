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

public partial class LABORATORY_lab_stock_verification : System.Web.UI.Page
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
    DataMathods OBJ_METHOD = new DataMathods();

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
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }

            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_LABSTC_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("LAB_STOCK_VERIFICATION", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                grvlabst.DataSource = Ds;
                grvlabst.DataKeyNames = new string[] { "ID" };
                grvlabst.DataBind();
            }

            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DEPT_MAT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("LAB_STOCK_VERIFICATION", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                dropitem.DataSource = Ds1;
                dropitem.DataTextField = "ITEMNAME";
                dropitem.DataValueField = "SLNO";
                dropitem.DataBind();
                dropitem.Items.Insert(0,new ListItem("Please Select"));
            }
            //using (SqlCommand cmd = new SqlCommand("LAB_STOCK_VERIFICATION", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_LABSTC_PAGE";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter da1 = new SqlDataAdapter("select A.ID ,A.DATE,A.ITEM,A.BATCHNO,A.EXPIRY,A.PACKTYPE,A.NOOFPACK,A.CLOSESTOCK,A.PHYSICALSTOCK,A.REASON,B.ITEMNAME,B.SLNO from LAB_STOCKVERF A,TBL_DEPT_MAT_STOCK B WHERE A.ITEM=B.SLNO ORDER BY A.ID desc", con);
            //    DataTable dt1 = new DataTable();
            //    da1.Fill(dt1);
            //    grvlabst.SelectedIndex = 0;
            //    grvlabst.DataSource = dt1;
            //    grvlabst.DataKeyNames = new string[] { "ID" };
            //    grvlabst.DataBind();
            //}
            ////-----------------------------

            //using (SqlCommand cmd = new SqlCommand("LAB_STOCK_VERIFICATION", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DEPT_MAT";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    //da = new SqlDataAdapter("select distinct SLNO,ITEMNAME from TBL_DEPT_MAT_STOCK", con);
            //    DataTable ds = new DataTable();
            //    da.Fill(ds);
            //    dropitem.DataSource = ds;
            //    dropitem.DataTextField = "ITEMNAME";
            //    dropitem.DataValueField = "SLNO";
            //    dropitem.DataBind();
            //    dropitem.Items.Insert(0, "Please Select");
            //}

            //con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
        
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
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
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[14];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ITEM", SqlDbType.VarChar, 500, dropitem.SelectedValue);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@BATCHNO", SqlDbType.VarChar, 500, txtbatchno.Text);

            SQL_PARAMS[5] = OBJ_METHOD.createParams("@EXPIRY", SqlDbType.DateTime, 0, Convert.ToDateTime(txtexpiry.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@PACKTYPE", SqlDbType.VarChar, 500, txtpacktype.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@NOOFPACK", SqlDbType.VarChar, 500, txtnopacking.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@CLOSESTOCK", SqlDbType.VarChar, 500, txtclostock.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@PHYSICALSTOCK", SqlDbType.VarChar, 500, txtphystck.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@REASON", SqlDbType.VarChar, 500, txtreson.Text);
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);

            OBJ_METHOD.ExecuteProceedure("USP_LAB_STOCKVERF", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clearfield();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //auto();
            //using (SqlCommand cm = new SqlCommand("USP_LAB_STOCKVERF", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
            //    cm.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = dropitem.SelectedValue;
            //    cm.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = txtbatchno.Text;

            //    cm.Parameters.Add("@EXPIRY", SqlDbType.DateTime).Value = Convert.ToDateTime(txtexpiry.Text).ToString("yyyy-MM-dd HH:mm");
            //    //cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LAB BILL " + droptesttype.SelectedItem.Text;
            //    cm.Parameters.Add("@PACKTYPE", SqlDbType.VarChar).Value = txtpacktype.Text;
            //    cm.Parameters.Add("@NOOFPACK", SqlDbType.VarChar).Value = txtnopacking.Text;
            //    cm.Parameters.Add("@CLOSESTOCK", SqlDbType.VarChar).Value = txtclostock.Text;
            //    cm.Parameters.Add("@PHYSICALSTOCK", SqlDbType.VarChar).Value = txtphystck.Text;
            //    cm.Parameters.Add("@REASON", SqlDbType.VarChar).Value = txtreson.Text;
            //    cm.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //    cm.ExecuteNonQuery();
            //}
            //con.Close();
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    public void clearfield()
    {
        txtbatchno.Text=txtclostock.Text=txtdate.Text=txtexpiry.Text=txtnopacking.Text=txtpacktype.Text=txtphystck.Text=txtreson.Text="";
        dropitem.SelectedIndex = 0;
        btncreate.Visible = true;
        btnupdate.Visible = false;
        auto();
    }
    protected void grvlabst_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = grvlabst.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_LABSTC_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("LAB_STOCK_VERIFICATION", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                txtselid.Text = slno.ToString();
                txtdate.Text = Ds.Tables[0].Rows[0]["DATE"].ToString();
                dropitem.Text = Ds.Tables[0].Rows[0]["ITEM"].ToString();
                txtbatchno.Text = Ds.Tables[0].Rows[0]["BATCHNO"].ToString();
                txtexpiry.Text = Ds.Tables[0].Rows[0]["EXPIRY"].ToString();
                txtpacktype.Text = Ds.Tables[0].Rows[0]["PACKTYPE"].ToString();
                txtnopacking.Text = Ds.Tables[0].Rows[0]["NOOFPACK"].ToString();
                txtclostock.Text = Ds.Tables[0].Rows[0]["CLOSESTOCK"].ToString();
                txtphystck.Text = Ds.Tables[0].Rows[0]["PHYSICALSTOCK"].ToString();
                txtreson.Text = Ds.Tables[0].Rows[0]["REASON"].ToString();
            }
            //using (SqlCommand cmd = new SqlCommand("LAB_STOCK_VERIFICATION", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            //    //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    //SqlCommand com = new SqlCommand("select ID ,DATE,ITEM,BATCHNO,EXPIRY,PACKTYPE,NOOFPACK,CLOSESTOCK,PHYSICALSTOCK,REASON from LAB_STOCKVERF  where ID='" + slno + "'", con);
            //    dr = cmd.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        btncreate.Visible = false;
            //        btnupdate.Visible = true;
            //        txtselid.Text = slno.ToString();
            //        txtdate.Text = dr["DATE"].ToString();
            //        dropitem.Text = dr["ITEM"].ToString();
            //        txtbatchno.Text = dr["BATCHNO"].ToString();
            //        txtexpiry.Text = dr["EXPIRY"].ToString();
            //        txtpacktype.Text = dr["PACKTYPE"].ToString();
            //        txtnopacking.Text = dr["NOOFPACK"].ToString();
            //        txtclostock.Text = dr["CLOSESTOCK"].ToString();
            //        txtphystck.Text = dr["PHYSICALSTOCK"].ToString();
            //        txtreson.Text = dr["REASON"].ToString();
            //    }
            //    dr.Close();
            //}
            //con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/LABORATORY/lab_stock_verification.aspx");
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
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
            SqlParameter[] SQL_PARAMS = new SqlParameter[11];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtselid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ITEM", SqlDbType.VarChar, 500, dropitem.SelectedValue);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@BATCHNO", SqlDbType.VarChar, 500, txtbatchno.Text);

            SQL_PARAMS[5] = OBJ_METHOD.createParams("@EXPIRY", SqlDbType.DateTime, 0, Convert.ToDateTime(txtexpiry.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@PACKTYPE", SqlDbType.VarChar, 500, txtpacktype.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@NOOFPACK", SqlDbType.VarChar, 500, txtnopacking.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@CLOSESTOCK", SqlDbType.VarChar, 500, txtclostock.Text);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@PHYSICALSTOCK", SqlDbType.VarChar, 500, txtphystck.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@REASON", SqlDbType.VarChar, 500, txtreson.Text);

            OBJ_METHOD.ExecuteProceedure("USP_LAB_STOCKVERF", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clearfield();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //auto();
            //using (SqlCommand cm = new SqlCommand("USP_LAB_STOCKVERF", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtselid.Text;
            //    cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
            //    cm.Parameters.Add("@ITEM", SqlDbType.VarChar).Value = dropitem.SelectedValue;
            //    cm.Parameters.Add("@BATCHNO", SqlDbType.VarChar).Value = txtbatchno.Text;
            //    cm.Parameters.Add("@EXPIRY", SqlDbType.DateTime).Value = Convert.ToDateTime(txtexpiry.Text).ToString("yyyy-MM-dd HH:mm");
            //    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LAB BILL " + droptesttype.SelectedItem.Text;
            //    cm.Parameters.Add("@PACKTYPE", SqlDbType.VarChar).Value = txtpacktype.Text;
            //    cm.Parameters.Add("@NOOFPACK", SqlDbType.VarChar).Value = txtnopacking.Text;
            //    cm.Parameters.Add("@CLOSESTOCK", SqlDbType.VarChar).Value = txtclostock.Text;
            //    cm.Parameters.Add("@PHYSICALSTOCK", SqlDbType.VarChar).Value = txtphystck.Text;
            //    cm.Parameters.Add("@REASON", SqlDbType.VarChar).Value = txtreson.Text;
            //    cm.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //    cm.ExecuteNonQuery();
            //}
            //con.Close();
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not updated.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
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
        string message1 = string.Empty;
        try
        {
            string slno = grvlabst.DataKeys[e.RowIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

            OBJ_METHOD.ExecuteProceedure("USP_LAB_STOCKVERF", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clearfield();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }

            
            //using (SqlCommand cmd = new SqlCommand("LAB_STOCK_VERIFICATION", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            //    //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    //SqlCommand cm = new SqlCommand("delete from LAB_STOCKVERF where ID='" + slno + "'", con);
            //    cmd.ExecuteNonQuery();
            //}
            //binddata();
            //con.Close();
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not deleted.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void grvlabst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_LABSTC_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("LAB_STOCK_VERIFICATION", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                grvlabst.DataSource = Ds;
                grvlabst.PageIndex = e.NewPageIndex;
                grvlabst.DataKeyNames = new string[] { "ID" };
                grvlabst.DataBind();
            }
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //using (SqlCommand cmd = new SqlCommand("LAB_STOCK_VERIFICATION", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_LABSTC_PAGE";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter Adp = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter Adp = new SqlDataAdapter("select * from LAB_STOCKVERF", con);
            //    DataTable dt = new DataTable();
            //    Adp.Fill(dt);
            //    grvlabst.DataSource = dt;
            //    grvlabst.PageIndex = e.NewPageIndex;
            //    grvlabst.DataKeyNames = new string[] { "ID" };
            //    grvlabst.DataBind();
            //}
            //con.Close();
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
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ITEM");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ITEMNAME", SqlDbType.VarChar, 500, dropitem.SelectedItem.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("LAB_STOCK_VERIFICATION", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                txtclostock.Text = Ds.Tables[0].Rows[0]["STOCK"].ToString();
            }
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //using (SqlCommand cmd = new SqlCommand("LAB_STOCK_VERIFICATION", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ITEM";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = dropitem.SelectedItem.Text;
            //    //SqlDataAdapter Adp = new SqlDataAdapter(cmd);
            //    //SqlCommand com = new SqlCommand("select distinct SLNO,ITEMNAME,STOCK from TBL_DEPT_MAT_STOCK  where ITEMNAME='" + dropitem.SelectedItem.Text + "'", con);
            //    dr = com.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        txtclostock.Text = dr["STOCK"].ToString();
            //    }
            //    dr.Close();
            //}
            //con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}