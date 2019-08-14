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

public partial class ADMIN_admin_Surgery : System.Web.UI.Page
{
    SqlDataReader dr;
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

        lblorgid.Text = Session["ORGID"].ToString();
        lblid.Text = Session["NAME"].ToString();
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
            //DataMathods OBJ_METHOD = new DataMathods();

            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }


            DataSet Ds = OBJ_METHOD.Get_DataSet("select id,SurgeryType from tblSurgeryType WHERE Branch_ID = " + Session["Branch"] + " order by id desc", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds;
                GridView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
            ErrorLog.Write(ex);
        }
    
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
        string message1 = string.Empty;
        try
        {
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False'", false, false);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                string message = "alert('* You Cant Delete.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
                OBJ_METHOD = new DataMathods();
                string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();
                SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

                //SQL_PARAMS[2] = OBJ_METHOD.createParams("@DeptName", SqlDbType.VarChar, 500, txtdept.Text);

                OBJ_METHOD.ExecuteProceedure("ADMIN_SurgeryType", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[2].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
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
                 var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
                 DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT id,SurgeryType from tblSurgeryType where id='" + slno + "'", false, false);
                 if (Ds.Tables[0].Rows.Count > 0)
                 {
                     btncreate.Visible = false;
                     btnupdate.Visible = true;
                     txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                     txtsurgery.Text = Ds.Tables[0].Rows[0]["SurgeryType"].ToString();
                 }
             }      
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT id,SurgeryType from tblSurgeryType WHERE Branch_ID = " + Session["Branch"] + " order by id desc", false, false);
            GridView1.DataSource = Ds;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
           
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            ErrorLog.Write(ex);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string message = string.Empty;
        OBJ_METHOD = new DataMathods();
        try
        {
            // Validation
            if (txtsurgery.Text == "")
            {
                string message1 = "alert('* Please Enter Surgerytype.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                txtsurgery.Focus();
                return;
            }
                   
            SqlParameter[] SQL_PARAMS = new SqlParameter[5];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@SurgeryType", SqlDbType.VarChar, 500, txtsurgery.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("ADMIN_SurgeryType", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
            }

            message = "alert('" + OBJ_METHOD._objOut + "')";
           
            
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            clearcontrol();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void clearcontrol()
    {
        txtsurgery.Text = "";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message = string.Empty;
        OBJ_METHOD = new DataMathods();
        try
        {
            // Validation
            if (txtsurgery.Text == "")
            {
                string message1 = "alert('* Please Enter Surgerytype.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                txtsurgery.Focus();
                return;
            }
           
         
            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@SurgeryType", SqlDbType.VarChar, 500, txtsurgery.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 500, txtid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

            OBJ_METHOD.ExecuteProceedure("ADMIN_SurgeryType", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                btnupdate.Visible = false;
                btncreate.Visible = true;
            }

            message = "alert('" + OBJ_METHOD._objOut + "')";
           
        }
        catch (Exception ex)
        {

            OBJ_METHOD.commitOrRollbackTran("rollback");
            message = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            clearcontrol();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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
    public DataTable getdata()
    {
        DataTable dt = new DataTable();
        DataSet Ds = OBJ_METHOD.Get_DataSet("select id,SurgeryType from tblSurgeryType WHERE Branch_ID = " + Session["Branch"] + " order by id desc", false, false);

        dt = Ds.Tables[0];
        return dt;
    }
}