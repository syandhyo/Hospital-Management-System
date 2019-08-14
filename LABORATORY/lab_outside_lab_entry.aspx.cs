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

public partial class LABORATORY_lab_outside_lab_entry : System.Web.UI.Page
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
        string qry1 = "select ID from OUTSIDE_LAB";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("OL{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
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

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_OUTLAB_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("LAB_OUTSIDE_ENTRY", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                grvlabst.SelectedIndex = 0;
                grvlabst.DataSource = Ds1;
                grvlabst.DataKeyNames = new string[] { "ID" };
                grvlabst.DataBind();
            }
            //using (SqlCommand cmd4 = new SqlCommand("LAB_OUTSIDE_ENTRY", con))
            //{
            //    cmd4.CommandType = CommandType.StoredProcedure;
            //    cmd4.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_OUTLAB_PAGE";
            //    cmd4.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter da1 = new SqlDataAdapter(cmd4);
            //    //SqlDataAdapter da1 = new SqlDataAdapter("select ID,CONVERT(varchar, DATE, 105) AS DATE,PATIENTYPE,OPDNO,FIRSTNAME from OUTSIDE_LAB ORDER BY ID asc", con);
            //    DataTable dt1 = new DataTable();
            //    da1.Fill(dt1);
            //    grvlabst.SelectedIndex = 0;
            //    grvlabst.DataSource = dt1;
            //    grvlabst.DataKeyNames = new string[] { "ID" };
            //    grvlabst.DataBind();
            //}
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
                string message = "alert('*Date Field Is Mandatory..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            if (txtsentolab.Text == "")
            {
                string message = "alert('*Sent To Lab Is Mandatory..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            if (txtsamplfm.Text == "")
            {
                string message = "alert('* Sample From Is Mandatory')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            DateTime datetime = Convert.ToDateTime(txtdate.Text);
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[23];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, datetime.ToString("yyyy-MM-dd"));
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@SENTNOTENO", SqlDbType.VarChar, 500, txtsenoteno.Text);

            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@SENTOLAB", SqlDbType.VarChar, 500, txtsentolab.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@SAMPLEFORM", SqlDbType.VarChar, 500, txtsamplfm.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@LABREQNO", SqlDbType.VarChar, 500, txtlabreqno.Text);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@PATIENTYPE", SqlDbType.VarChar, 500, droppatype.SelectedItem.Text);

            SQL_PARAMS[10] = OBJ_METHOD.createParams("@OPDNO", SqlDbType.VarChar, 500, txtopdno.Text.ToUpper());
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@REQSITNAT", SqlDbType.VarChar, 500, txtreqat.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@FIRSTNAME", SqlDbType.VarChar, 500, txtFname.Text);
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@AGE", SqlDbType.VarChar, 500, txtage.Text);

            SQL_PARAMS[14] = OBJ_METHOD.createParams("@GENDER", SqlDbType.VarChar, 500, dropgender.SelectedItem.Text);
            SQL_PARAMS[15] = OBJ_METHOD.createParams("@CONTACTNO", SqlDbType.VarChar, 500, txtcontactno.Text);
            SQL_PARAMS[16] = OBJ_METHOD.createParams("@ADRESSP", SqlDbType.VarChar, 500, txtadress.Text);
            SQL_PARAMS[17] = OBJ_METHOD.createParams("@ADRESSPRT", SqlDbType.VarChar, 500, txtaddresPermnt.Text);

            SQL_PARAMS[18] = OBJ_METHOD.createParams("@TREATINGDR", SqlDbType.VarChar, 500, txttreatingdr.Text);
            SQL_PARAMS[19] = OBJ_METHOD.createParams("@ADVPAY", SqlDbType.Decimal, 0, txtAdvpaid.Text);
            SQL_PARAMS[20] = OBJ_METHOD.createParams("@SENTBY", SqlDbType.VarChar, 500, txtsentby.Text);
            SQL_PARAMS[21] = OBJ_METHOD.createParams("@TOTAMT", SqlDbType.Decimal, 0, txttotamt.Text);

            SQL_PARAMS[22] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);


            OBJ_METHOD.ExecuteProceedure("USP_OUTSIDELAB", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
                binddata();
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            #region oldcode
            //using (SqlCommand cm = new SqlCommand("USP_OUTSIDELAB", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = datetime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //    cm.Parameters.Add("@SENTNOTENO", SqlDbType.VarChar).Value = txtsenoteno.Text;

            //    cm.Parameters.Add("@SENTOLAB", SqlDbType.VarChar).Value = txtsentolab.Text;
            //    cm.Parameters.Add("@SAMPLEFORM", SqlDbType.VarChar).Value = txtsamplfm.Text;
            //    cm.Parameters.Add("@LABREQNO", SqlDbType.VarChar).Value = txtlabreqno.Text;
            //    cm.Parameters.Add("@PATIENTYPE", SqlDbType.VarChar).Value = droppatype.SelectedItem.Text;
            //    cm.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtopdno.Text.ToUpper();

            //    cm.Parameters.Add("@REQSITNAT", SqlDbType.VarChar).Value = txtreqat.Text;
            //    cm.Parameters.Add("@FIRSTNAME", SqlDbType.VarChar).Value = txtFname.Text;
            //    cm.Parameters.Add("@AGE", SqlDbType.VarChar).Value = txtage.Text;
            //    cm.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = dropgender.SelectedItem.Text;
            //    cm.Parameters.Add("@CONTACTNO", SqlDbType.VarChar).Value = txtcontactno.Text;

            //    cm.Parameters.Add("@ADRESSP ", SqlDbType.VarChar).Value = txtadress.Text;
            //    cm.Parameters.Add("@ADRESSPRT ", SqlDbType.VarChar).Value = txtaddresPermnt.Text;
            //    cm.Parameters.Add("@TREATINGDR", SqlDbType.VarChar).Value = txttreatingdr.Text;
            //    cm.Parameters.Add("@ADVPAY", SqlDbType.Decimal).Value = txtAdvpaid.Text;
            //    cm.Parameters.Add("@SENTBY", SqlDbType.VarChar).Value = txtsentby.Text;
            //    cm.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = txttotamt.Text;

            //    cm.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //    // cm.Parameters.Add("@DOB", SqlDbType.VarChar).Value = Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd HH:mm");

            //    cm.ExecuteNonQuery();
            //}
            //con.Close();
            #endregion
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
    public void clearcontrol()
    {
        txtaddresPermnt.Text = "";
        txtadress.Text = "";
        txtAdvpaid.Text = "0";
        txtage.Text = "0";
        txtcontactno.Text = "";
        txtdate.Text = "";
        txtFname.Text = "";
        txtlabreqno.Text = "";
        txtopdno.Text = "";
        txtreqat.Text="";
        txtsamplfm.Text = "";
        txtsenoteno.Text = "";
        txtsentby.Text = "";
        txtsentolab.Text = "";
        txttotamt.Text = "";
        txttreatingdr.Text = "";
        dropgender.SelectedIndex = 0;
        droppatype.SelectedIndex = 0;
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        binddata();
        clearcontrol();
    }
    protected void grvlabst_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = grvlabst.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_OUTLAB_ID");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID",SqlDbType.VarChar, 500 , slno);

            DataSet DS = OBJ_METHOD.Get_DataSet("LAB_OUTSIDE_ENTRY", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                btncancel.Visible = true;
                txtselid.Text = slno.ToString();
                txtdate.Text = Convert.ToDateTime(DS.Tables[0].Rows[0]["DATE"]).ToString("dd-MM-yyyy");
                txtsenoteno.Text = DS.Tables[0].Rows[0]["SENTNOTENO"].ToString();
                txtsentolab.Text = DS.Tables[0].Rows[0]["SENTOLAB"].ToString();
                txtsamplfm.Text = DS.Tables[0].Rows[0]["SAMPLEFORM"].ToString();

                txtlabreqno.Text = DS.Tables[0].Rows[0]["LABREQNO"].ToString();
                droppatype.Text = DS.Tables[0].Rows[0]["PATIENTYPE"].ToString();
                txtopdno.Text = DS.Tables[0].Rows[0]["OPDNO"].ToString();
                txtreqat.Text = DS.Tables[0].Rows[0]["REQSITNAT"].ToString();
                txtFname.Text = DS.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                txtage.Text = DS.Tables[0].Rows[0]["AGE"].ToString();
                dropgender.Text = DS.Tables[0].Rows[0]["GENDER"].ToString();
                txtcontactno.Text = DS.Tables[0].Rows[0]["CONTACTNO"].ToString();
                txtadress.Text = DS.Tables[0].Rows[0]["ADRESSP"].ToString();
                txtaddresPermnt.Text = DS.Tables[0].Rows[0]["ADRESSPRT"].ToString();
                txttreatingdr.Text = DS.Tables[0].Rows[0]["TREATINGDR"].ToString();
                txtAdvpaid.Text = DS.Tables[0].Rows[0]["ADVPAY"].ToString();
                txtsentby.Text = DS.Tables[0].Rows[0]["SENTBY"].ToString();
                txttotamt.Text = DS.Tables[0].Rows[0]["TOTAMT"].ToString();

            }
            #region oldcode
            //using (SqlCommand com = new SqlCommand("LAB_OUTSIDE_ENTRY", con))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_OUTLAB_ID";
            //    com.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    //SqlDataAdapter da1 = new SqlDataAdapter(cmd4);
            //    //SqlCommand com = new SqlCommand("select * from OUTSIDE_LAB  where ID='" + slno + "'", con);
            //    dr = com.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        btncreate.Visible = false;
            //        btnupdate.Visible = true;
            //        btncancel.Visible = true;
            //        txtselid.Text = slno.ToString();
            //        txtdate.Text = Convert.ToDateTime(dr["DATE"]).ToString("dd-MM-yyyy");
            //        txtsenoteno.Text = dr["SENTNOTENO"].ToString();
            //        txtsentolab.Text = dr["SENTOLAB"].ToString();
            //        txtsamplfm.Text = dr["SAMPLEFORM"].ToString();

            //        txtlabreqno.Text = dr["LABREQNO"].ToString();
            //        droppatype.Text = dr["PATIENTYPE"].ToString();
            //        txtopdno.Text = dr["OPDNO"].ToString();
            //        txtreqat.Text = dr["REQSITNAT"].ToString();
            //        txtFname.Text = dr["FIRSTNAME"].ToString();
            //        txtage.Text = dr["AGE"].ToString();
            //        dropgender.Text = dr["GENDER"].ToString();
            //        txtcontactno.Text = dr["CONTACTNO"].ToString();
            //        txtadress.Text = dr["ADRESSP"].ToString();
            //        txtaddresPermnt.Text = dr["ADRESSPRT"].ToString();
            //        txttreatingdr.Text = dr["TREATINGDR"].ToString();
            //        txtAdvpaid.Text = dr["ADVPAY"].ToString();
            //        txtsentby.Text = dr["SENTBY"].ToString();
            //        txttotamt.Text = dr["TOTAMT"].ToString();

            //    }
            //}
            //dr.Close();
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtdate.Text == "")
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('* Date Fields are mandatory.')</script>");

                return;
            }

            if (txtsentolab.Text == "")
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('*  Fields are mandatory.')</script>");

                return;
            }

            if (txtsamplfm.Text == "")
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('* Batch No are mandatory.')</script>");

                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[21];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtselid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@SENTNOTENO", SqlDbType.VarChar, 500, txtsenoteno.Text);
           
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@SENTOLAB", SqlDbType.VarChar, 500, txtsentolab.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@SAMPLEFORM", SqlDbType.VarChar, 500, txtsamplfm.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@LABREQNO", SqlDbType.VarChar, 500, txtlabreqno.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@PATIENTYPE", SqlDbType.VarChar, 500, droppatype.SelectedItem.Text);

            SQL_PARAMS[8] = OBJ_METHOD.createParams("@OPDNO", SqlDbType.VarChar, 500, txtopdno.Text.ToUpper());
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@REQSITNAT", SqlDbType.VarChar, 500, txtreqat.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@FIRSTNAME", SqlDbType.VarChar, 500, txtFname.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@AGE", SqlDbType.VarChar, 500, txtage.Text);

            SQL_PARAMS[12] = OBJ_METHOD.createParams("@GENDER", SqlDbType.VarChar, 500, dropgender.SelectedItem.Text);
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@CONTACTNO", SqlDbType.VarChar, 500, txtcontactno.Text);
            SQL_PARAMS[14] = OBJ_METHOD.createParams("@ADRESSP", SqlDbType.VarChar, 500, txtadress.Text);
            SQL_PARAMS[15] = OBJ_METHOD.createParams("@ADRESSPRT", SqlDbType.VarChar, 500, txtaddresPermnt.Text);

            SQL_PARAMS[16] = OBJ_METHOD.createParams("@TREATINGDR", SqlDbType.VarChar, 500, txttreatingdr.Text);
            SQL_PARAMS[17] = OBJ_METHOD.createParams("@ADVPAY", SqlDbType.Decimal, 0, txtAdvpaid.Text);
            SQL_PARAMS[18] = OBJ_METHOD.createParams("@SENTBY", SqlDbType.VarChar, 500, txtsentby.Text);
            SQL_PARAMS[19] = OBJ_METHOD.createParams("@TOTAMT", SqlDbType.Decimal, 0, txttotamt.Text);

            SQL_PARAMS[20] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);


            OBJ_METHOD.ExecuteProceedure("USP_OUTSIDELAB", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
                binddata();
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";

            #region oldcode
            //using (SqlCommand cm = new SqlCommand("USP_OUTSIDELAB", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtselid.Text;
            //    cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
            //    cm.Parameters.Add("@SENTNOTENO", SqlDbType.VarChar).Value = txtsenoteno.Text;
            //    cm.Parameters.Add("@SENTOLAB", SqlDbType.VarChar).Value = txtsentolab.Text;
            //    cm.Parameters.Add("@SAMPLEFORM", SqlDbType.VarChar).Value = txtsamplfm.Text;
            //    // cm.Parameters.Add("@EXPIRY", SqlDbType.DateTime).Value = Convert.ToDateTime(txtexpiry.Text).ToString("yyyy-MM-dd HH:mm");
            //    //cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "LAB BILL " + droptesttype.SelectedItem.Text;
            //    cm.Parameters.Add("@LABREQNO", SqlDbType.VarChar).Value = txtlabreqno.Text;
            //    cm.Parameters.Add("@PATIENTYPE", SqlDbType.VarChar).Value = droppatype.SelectedItem.Text;
            //    cm.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtopdno.Text.ToUpper();
            //    cm.Parameters.Add("@REQSITNAT", SqlDbType.VarChar).Value = txtreqat.Text;
            //    cm.Parameters.Add("@FIRSTNAME", SqlDbType.VarChar).Value = txtFname.Text;
            //    cm.Parameters.Add("@AGE", SqlDbType.VarChar).Value = txtage.Text;
            //    cm.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = dropgender.SelectedItem.Text;
            //    cm.Parameters.Add("@CONTACTNO", SqlDbType.VarChar).Value = txtcontactno.Text;
            //    cm.Parameters.Add("@ADRESSP ", SqlDbType.VarChar).Value = txtadress.Text;
            //    cm.Parameters.Add("@ADRESSPRT ", SqlDbType.VarChar).Value = txtaddresPermnt.Text;
            //    cm.Parameters.Add("@TREATINGDR", SqlDbType.VarChar).Value = txttreatingdr.Text;
            //    cm.Parameters.Add("@ADVPAY", SqlDbType.Decimal).Value = txtAdvpaid.Text;
            //    cm.Parameters.Add("@SENTBY", SqlDbType.VarChar).Value = txtsentby.Text;
            //    cm.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = txttotamt.Text;

            //    cm.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //    // cm.Parameters.Add("@DOB", SqlDbType.VarChar).Value = Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd HH:mm");

            //    cm.ExecuteNonQuery();
            //}
            //string message = "alert('Updated Successfuly !')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            ////Session["LABID"] = TXTID.Text;
            //con.Close();
            #endregion
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

            OBJ_METHOD.ExecuteProceedure("USP_OUTSIDELAB", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
                binddata();
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            //using (SqlCommand com = new SqlCommand("LAB_OUTSIDE_ENTRY", con))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //    com.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    com.ExecuteNonQuery();
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

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_OUTLAB_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("LAB_OUTSIDE_ENTRY", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                grvlabst.SelectedIndex = 0;
                grvlabst.DataSource = Ds1;
                grvlabst.PageIndex = e.NewPageIndex;
                grvlabst.DataKeyNames = new string[] { "ID" };
                grvlabst.DataBind();
            }
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //using (SqlCommand cmd4 = new SqlCommand("LAB_OUTSIDE_ENTRY", con))
            //{
            //    cmd4.CommandType = CommandType.StoredProcedure;
            //    cmd4.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_OUTLAB_PAGE";
            //    cmd4.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter Adp = new SqlDataAdapter(cmd4);
            //    //SqlDataAdapter Adp = new SqlDataAdapter("select ID,CONVERT(varchar, DATE, 105) AS DATE,PATIENTYPE,OPDNO,FIRSTNAME from OUTSIDE_LAB ORDER BY ID asc", con);
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
    protected void txtlabreqno_TextChanged(object sender, EventArgs e)
    {

    }
}