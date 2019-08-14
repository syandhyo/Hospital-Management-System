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

public partial class ADMIN_admin_ot_master : System.Web.UI.Page
{
   
    DataMathods OBJ_METHOD = new DataMathods();
    
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

            DataSet DS = OBJ_METHOD.Get_DataSet("SELECT * from OT_MASTER where Branch_ID='" + Session["Branch"] + "'", false, false);
            if (DS.Tables[0].Rows.Count > 0)
            {
                gridstore.DataSource = DS;
                gridstore.DataKeyNames = new string[] { "ID" };
                gridstore.DataBind();
                
            }


            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS1.Tables[0].Rows[0]["FYEAR"].ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void clearfield()
    {
        txtname.Text = "";
        txtbed.Text = "0";
        btncreate.Visible = true;
        btnupdate.Visible = false;

    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('* Enter Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            if (txtbed.Text == "")
            {
                string message = "alert('* Enter No Of Bed.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtbed.Focus();
                return;
            }
            


            SqlParameter[] SQL_PARAMS = new SqlParameter[5];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@NO_OF_BED", SqlDbType.Int, 500, txtbed.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@BRANCH_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("OT_MASTER_ENTRY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
                clearfield();
            }
            else
            {
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
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
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('* Enter Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            if (txtbed.Text == "")
            {
                string message = "alert('* Enter No Of Bed.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtbed.Focus();
                return;
            }



            SqlParameter[] SQL_PARAMS = new SqlParameter[5];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@NO_OF_BED", SqlDbType.Int, 500, txtbed.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0,txtid.Text);

            OBJ_METHOD.ExecuteProceedure("OT_MASTER_ENTRY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
                clearfield();
            }
            else
            {
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
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
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/admin_ot_master.aspx");
    }
    protected void gridstore_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            DataSet Ds3 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Edit' and selected='False'", false, false);
            if (Ds3.Tables[0].Rows.Count > 0)
            {
                string message = "alert('* You Cant Edit.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
                var slno = gridstore.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
                DataSet DS = OBJ_METHOD.Get_DataSet("select * from OT_MASTER where ID= " + slno + "", false, false);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    txtid.Text = DS.Tables[0].Rows[0]["ID"].ToString();
                    txtname.Text = DS.Tables[0].Rows[0]["NAME"].ToString();

                    txtbed.Text = DS.Tables[0].Rows[0]["NO_OF_BED"].ToString();
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void gridstore_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

        SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
        SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());

        DataSet DS = OBJ_METHOD.Get_DataSet("OT_MASTER_ENTRY", false, true, SQL_PARAMS1);
        if (DS.Tables[0].Rows.Count > 0)
        {
            gridstore.DataSource = DS;
            gridstore.DataKeyNames = new string[] { "ID" };
            gridstore.PageIndex = e.NewPageIndex;
            gridstore.DataBind();
            //txtid.Text = DS.Tables[0].Rows[0]["ID"].ToString();
            //txtname.Text = DS.Tables[0].Rows[0]["NAME"].ToString();
            //txtphone.Text = DS.Tables[0].Rows[0]["PHONE"].ToString();
            //txtstatecode.Text = DS.Tables[0].Rows[0]["STATECODE"].ToString();
            //txtgst.Text = DS.Tables[0].Rows[0]["GSTNO"].ToString();
            //txtaddress.Text = DS.Tables[0].Rows[0]["ADDRESS"].ToString();
        }
    }
    protected void gridstore_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
}