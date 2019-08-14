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

public partial class ADMIN_admin_outsidelaboratory : System.Web.UI.Page
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
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }

            DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,NAME,ADRESSS,PHONENO,CITY,STATE from TBL_OUTSIDELAB WHERE Branch_ID= " + Session["Branch"] + " order by ID Desc", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                grvoutlab.DataSource = Ds;
                grvoutlab.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void btncreate_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('* Name are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtadress.Text == "")
            {
                string message = "alert('*Address Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtphoneno.Text == "")
            {
                string message = "alert('*Phone No are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtstate.Text == "")
            {
                string message = "alert('*State Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[9];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ADRESSS", SqlDbType.VarChar, 500, txtadress.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@PHONENO", SqlDbType.VarChar, 500, txtphoneno.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@CITY", SqlDbType.VarChar, 500, txtcity.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@STATE", SqlDbType.VarChar, 500, txtstate.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));


            OBJ_METHOD.ExecuteProceedure("USP_OUTSIDELAB_ADMN", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");

            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {

            clearcontrol();
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
    }
    public void clearcontrol()
    {
        txtname.Text = "";
        txtadress.Text = "";
        txtphoneno.Text = "";
        txtcity.Text = "";
        txtstate.Text = "";
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('* Name are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtadress.Text == "")
            {
                string message = "alert('*Address Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtphoneno.Text == "")
            {
                string message = "alert('*Phone No are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtstate.Text == "")
            {
                string message = "alert('*State Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
             SqlParameter[] SQL_PARAMS = new SqlParameter[7];

             SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
             SQL_PARAMS[1] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text);
             SQL_PARAMS[2] = OBJ_METHOD.createParams("@ADRESSS", SqlDbType.VarChar, 500, txtadress.Text);
             SQL_PARAMS[3] = OBJ_METHOD.createParams("@PHONENO", SqlDbType.VarChar, 500, txtphoneno.Text);
             SQL_PARAMS[4] = OBJ_METHOD.createParams("@CITY", SqlDbType.VarChar, 500, txtcity.Text);
             SQL_PARAMS[5] = OBJ_METHOD.createParams("@STATE", SqlDbType.VarChar, 500, txtstate.Text);
             SQL_PARAMS[6] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, txtid.Text);
            OBJ_METHOD.ExecuteProceedure("USP_OUTSIDELAB_ADMN", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");

            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not Updated.')";
        }
        finally
        {

            clearcontrol();
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clearcontrol();
        binddata();
    }
    protected void grvoutlab_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
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
                 var slno = grvoutlab.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
                 DataSet Ds = OBJ_METHOD.Get_DataSet("select * from TBL_OUTSIDELAB where ID=" + slno, false, false);
                 if (Ds.Tables[0].Rows.Count > 0)
                 {
                     btncreate.Visible = false;
                     btnupdate.Visible = true;
                     txtid.Text = slno.ToString();

                     txtname.Text = Ds.Tables[0].Rows[0]["NAME"].ToString();
                     txtadress.Text = Ds.Tables[0].Rows[0]["ADRESSS"].ToString();
                     txtphoneno.Text = Ds.Tables[0].Rows[0]["PHONENO"].ToString();
                     txtcity.Text = Ds.Tables[0].Rows[0]["CITY"].ToString();
                     txtstate.Text = Ds.Tables[0].Rows[0]["STATE"].ToString();
                 }
             }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grvoutlab_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[5].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    protected void grvoutlab_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
        string message1 = string.Empty;
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False'", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                string message = "alert('* You Cant Delete.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
                OBJ_METHOD = new DataMathods();
                string slno = grvoutlab.DataKeys[e.RowIndex].Values["ID"].ToString();
                SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);

                OBJ_METHOD.ExecuteProceedure("USP_OUTSIDELAB_ADMN", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {

                    OBJ_METHOD.commitOrRollbackTran("commit");

                }
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }

        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not deleted.')";
        }
        finally
        {

            clearcontrol();
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
    }
    protected void grvoutlab_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,NAME,ADRESSS,PHONENO,CITY,STATE from TBL_OUTSIDELAB WHERE Branch_ID= " + Session["Branch"] + " order by ID Desc", false, false);

            grvoutlab.DataSource = Ds;
            grvoutlab.PageIndex = e.NewPageIndex;
            grvoutlab.DataKeyNames = new string[] { "id" };
            grvoutlab.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public DataTable getdata()
    {
        DataTable dt = new DataTable();
        DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,NAME,ADRESSS,PHONENO,CITY,STATE from TBL_OUTSIDELAB WHERE Branch_ID= " + Session["Branch"] + " order by ID Desc", false, false);

        dt = Ds.Tables[0];
        return dt;

    }
    public SortDirection direction
    {
        get
        {
            if (ViewState["directionState"] == null)
            {
                ViewState["directionState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["directionState"];
        }
        set
        {
            ViewState["directionState"] = value;
        }
    }
    protected void grvoutlab_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (direction == SortDirection.Ascending)
        {
            direction = SortDirection.Descending;
            sortingDirection = "Desc";

        }
        else
        {
            direction = SortDirection.Ascending;
            sortingDirection = "Asc";

        }
        DataView sortedView = new DataView(getdata());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        Session["SortedView"] = sortedView;
        grvoutlab.DataSource = sortedView;
        grvoutlab.DataBind();
    }
}