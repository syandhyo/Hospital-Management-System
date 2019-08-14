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


public partial class ADMIN_admin_departmententry : System.Web.UI.Page
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
            Session["SortedView"] = null;
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


            DataSet Ds = OBJ_METHOD.Get_DataSet("select id,DeptName from tblDepartment WHERE Branch_ID = "+Session["Branch"]+" order by id desc", false, false);
            
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            
            ErrorLog.Write(ex);
        }
           
    }

    public DataTable getdata()
    {
            DataTable dt=new DataTable();

        DataSet Ds = OBJ_METHOD.Get_DataSet("select id,DeptName from tblDepartment WHERE Branch_ID = " + Session["Branch"] + " order by id desc", false, false);

            dt = Ds.Tables[0];
            return dt;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            if (Session["denyper"] != null)
            {
                string[] per = Session["denyper"].ToString().Split(',');
                bool theeditdeny = false;
                bool thedeletedeny = false;
                for (int i = 0; i < per.Length; i++)
                {
                    string theper = per[i];
                    if (theper == "Edit")
                    {
                        theeditdeny = true;
                    }
                    else if (theper == "Delete")
                    {
                        thedeletedeny = true;
                    }
                }

                if (thedeletedeny)
                {
                    LinkButton dbdelete = (LinkButton)e.Row.Cells[1].Controls[0];
                    dbdelete.Visible = false;
                }
                else
                {
                    LinkButton dbdelete = (LinkButton)e.Row.Cells[1].Controls[0];
                    dbdelete.Visible = true;
                    dbdelete.OnClientClick = "return confirm('Are you sure want to delete this info ?');";
                }

                if (theeditdeny)
                {
                    LinkButton dbedit = new LinkButton();
                    if (thedeletedeny)
                    {
                        dbedit = (LinkButton)e.Row.Cells[1].Controls[0];
                    }
                    else
                    {
                        dbedit = (LinkButton)e.Row.Cells[1].Controls[2];
                    }
                    dbedit.Visible = false;
                }

                if (theeditdeny == true && thedeletedeny == true)
                {
                    e.Row.Cells[1].Text = "Not Authorized";
                }

            }
            
        }
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            OBJ_METHOD = new DataMathods();
            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False'", false, false);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                string message = "alert('* You Cant Delete.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
                OBJ_METHOD = new DataMathods();
                string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();
                SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);
                //SQL_PARAMS[2] = OBJ_METHOD.createParams("@DeptName", SqlDbType.VarChar, 500, txtdept.Text);

                OBJ_METHOD.ExecuteProceedure("SP_DEPT_master", "", "@msg", SqlDbType.VarChar, SQL_PARAMS1, true);

                if (OBJ_METHOD._RESULT > 0)
                {

                    OBJ_METHOD.commitOrRollbackTran("commit");
                    clearcontrol();
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
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select id,DeptName from tblDepartment WHERE Branch_ID = " + Session["Branch"] + " order by id desc", false, false);
            
            GridView1.DataSource = Ds;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        try
        {
            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Edit' and selected='False'", false, false);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                string message = "alert('* You Cant Edit.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
                var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();

                DataSet Ds = OBJ_METHOD.Get_DataSet("select * from tblDepartment where id=" + slno, false, false);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                    txtdept.Text = Ds.Tables[0].Rows[0]["DeptName"].ToString();

                }
            }
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
  

    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
          
            if (txtdept.Text.Trim() == "")
            {
                string message = "alert('* Please Enter Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdept.Focus();
                return;
            }

            SqlParameter[] SQL_PARAMS = new SqlParameter[6];
          
            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DeptName", SqlDbType.VarChar, 500, txtdept.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@DateStamp", SqlDbType.DateTime, 0, DateTime.Now.ToString());
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));


            OBJ_METHOD.ExecuteProceedure("SP_DEPT_master", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
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
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
      
    }

    public void clearcontrol()
    {
        txtdept.Text = "";
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtdept.Text.Trim() == "")
            {
                string message = "alert('* Please Enter Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdept.Focus();
                return;
            }
             SqlParameter[] SQL_PARAMS = new SqlParameter[3];
          
            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, txtid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DeptName", SqlDbType.VarChar, 500, txtdept.Text);

            OBJ_METHOD.ExecuteProceedure("SP_DEPT_master", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
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
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        clearcontrol();
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
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
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
        GridView1.DataSource = sortedView;
        GridView1.DataBind();
    }
}