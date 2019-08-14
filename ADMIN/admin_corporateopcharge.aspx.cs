using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class ADMIN_admin_corporateopcharge : System.Web.UI.Page
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
            DataSet Ds = OBJ_METHOD.Get_DataSet("select A.ID AS ID,CNAME AS CORPORATE,C.ProcedureName As PROCEDNAME,A.PRICE AS PRICE FROM TBL_CORPOTCHG A join Corporate_Table B on A.CORPRATE_ID=B.ID join tblProcedureCost c on c.id=a.PROCEDNAME_ID where a.Branch_ID= " + Session["Branch"] + " order by ID desc", false, false);
            grvrooment.DataSource = Ds;
            grvrooment.DataBind();
            //----------------------
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select distinct ID,CNAME from Corporate_Table WHERE Branch_ID= " + Session["Branch"] + "", false, false);
            dropCorport.DataSource = Ds1;
            dropCorport.DataTextField = "CNAME";
            dropCorport.DataValueField = "ID";
            dropCorport.DataBind();
            dropCorport.Items.Insert(0, new ListItem("Please Select", "0"));

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select distinct id,ProcedureName from tblProcedureCost WHERE Branch_ID= " + Session["Branch"] + "", false, false);
            dropproc.DataSource = Ds2;
            dropproc.DataTextField = "ProcedureName";
            dropproc.DataValueField = "id";
            dropproc.DataBind();
            dropproc.Items.Insert(0, new ListItem("Please Select", "0"));


            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grvrooment_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
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
            var slno = grvrooment.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from TBL_CORPOTCHG where ID='" + slno + "'", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = slno.ToString();
                dropCorport.SelectedValue = Ds.Tables[0].Rows[0]["CORPRATE_ID"].ToString();
                dropproc.SelectedValue = Ds.Tables[0].Rows[0]["PROCEDNAME_ID"].ToString();
                txtprice.Text = Ds.Tables[0].Rows[0]["PRICE"].ToString();
            }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grvrooment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    protected void grvrooment_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                string slno = grvrooment.DataKeys[e.RowIndex].Values["ID"].ToString();
                SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);

                OBJ_METHOD.ExecuteProceedure("ADMIN_Corporate_OP_Charge", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {

                    OBJ_METHOD.commitOrRollbackTran("commit");

                }
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
        }
        catch (Exception ex)
        {
            clearcontrol();
            binddata();
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
    protected void grvrooment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select A.ID AS ID,CNAME AS CORPORATE,C.ProcedureName As PROCEDNAME,A.PRICE AS PRICE FROM TBL_CORPOTCHG A join Corporate_Table B on A.CORPRATE_ID=B.ID join tblProcedureCost c on c.id=a.PROCEDNAME_ID where a.Branch_ID=" + Session["Branch"] + " order by ID desc", false, false);
            grvrooment.DataSource = Ds;
            grvrooment.PageIndex = e.NewPageIndex;
            grvrooment.DataKeyNames = new string[] { "ID" };
            grvrooment.DataBind();
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
            if (dropCorport.SelectedIndex == 0)
            {
                string message = "alert('* Corporate Name is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropCorport.Focus();
                return;
            }
            else if (dropproc.SelectedIndex == 0)
            {
                string message = "alert('* Procedure Name is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropproc.Focus();
                return;
            }
            else if (txtprice.Text == "")
            {
                string message = "alert('* Price is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtprice.Focus();
                return;
            }
            else
            {

                OBJ_METHOD = new DataMathods();
                SqlParameter[] SQL_PARAMS1 = new SqlParameter[7];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@CORPRATE_ID", SqlDbType.Int, 0, dropCorport.SelectedValue);
                SQL_PARAMS1[2] = OBJ_METHOD.createParams("@PROCEDNAME_ID", SqlDbType.Int, 0, dropproc.SelectedValue);
                SQL_PARAMS1[3] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, txtprice.Text);
                SQL_PARAMS1[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
                SQL_PARAMS1[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
                SQL_PARAMS1[6] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

                OBJ_METHOD.ExecuteProceedure("ADMIN_Corporate_OP_Charge", "", "@msg", SqlDbType.VarChar, SQL_PARAMS1, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                }
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
                
            }
        }
        catch (Exception ex)
        {
            clearcontrol();
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
        txtprice.Text = "0.00";
        dropCorport.SelectedIndex = 0;
        dropproc.SelectedIndex = 0;
        binddata();
        btnupdate.Visible = false;
        btncreate.Visible = true;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            if (dropCorport.SelectedItem.Text == "")
            {
                string message = "alert('* Corporate Name is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropCorport.Focus();
                return;
            }
            else if (dropproc.SelectedIndex == 0)
            {
                string message = "alert('* Procedure Name is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropproc.Focus();
                return;
            }
            else if (txtprice.Text == "")
            {
                string message = "alert('* Price are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtprice.Focus();
                return;
            }
            else
            {
                SqlParameter[] SQL_PARAMS1 = new SqlParameter[8];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, txtid.Text);
                SQL_PARAMS1[2] = OBJ_METHOD.createParams("@CORPRATE_ID", SqlDbType.VarChar, 200, dropCorport.SelectedValue);
                SQL_PARAMS1[3] = OBJ_METHOD.createParams("@PROCEDNAME_ID", SqlDbType.VarChar, 200, dropproc.SelectedValue);
                SQL_PARAMS1[4] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, txtprice.Text);
                SQL_PARAMS1[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
                SQL_PARAMS1[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
                SQL_PARAMS1[7] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

                OBJ_METHOD.ExecuteProceedure("ADMIN_Corporate_OP_Charge", "", "@msg", SqlDbType.VarChar, SQL_PARAMS1, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                }
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
        }
        catch (Exception ex)
        {
            clearcontrol();
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

    protected void btncancel_Click(object sender, EventArgs e)
    {
        clearcontrol();
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
    public DataTable bind()
    {
        DataTable dt = new DataTable();
        DataSet Ds = OBJ_METHOD.Get_DataSet("select A.ID AS ID,CNAME AS CORPORATE,C.ProcedureName As PROCEDNAME,A.PRICE AS PRICE FROM TBL_CORPOTCHG A join Corporate_Table B on A.CORPRATE_ID=B.ID join tblProcedureCost c on c.id=a.PROCEDNAME_ID where a.Branch_ID= " + Session["Branch"] + " order by ID desc", false, false);

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
    protected void grvrooment_Sorting(object sender, GridViewSortEventArgs e)
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
        DataView sortedView = new DataView(bind());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        Session["SortedView"] = sortedView;
        grvrooment.DataSource = sortedView;
        grvrooment.DataBind();
    }
}