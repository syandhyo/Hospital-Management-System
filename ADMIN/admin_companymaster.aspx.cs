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

public partial class ADMIN_admin_companymaster : System.Web.UI.Page
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
        // lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        lblid.Text = Session["NAME"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        if (!IsPostBack)
        {
            Session["SortedView"] = null;
            binddata();

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
    public void binddata()
    {

        SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

        SQL_PARAMS1[0] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
        SQL_PARAMS1[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 200, "DISPLAY");


        DataSet DS1 = OBJ_METHOD.Get_DataSet("phrmc_CompnyMstSeldel", false, true, SQL_PARAMS1);
        if (DS1.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = DS1;
            GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();
        }

        SqlParameter[] SQL_PARAMS = new SqlParameter[1];

        SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

        DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
        if (DS.Tables[0].Rows.Count > 0)
        {
            lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('Please!!Enter Company Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            
            SqlParameter[] SQL_PARAMS = new SqlParameter[5];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@Name", SqlDbType.VarChar, 500, txtname.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("phrmc_CompnyMstInsUp", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
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
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
       
    }
    public void clearcontrol()
    {
        txtname.Text = "";
        txtsearchname.Text = "";
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('Please!! Enter Company Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@Name", SqlDbType.VarChar, 500, txtname.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, txtid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

            OBJ_METHOD.ExecuteProceedure("phrmc_CompnyMstInsUp", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                btncreate.Visible = true;
                btnupdate.Visible = false;
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
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
       
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        clearcontrol();
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
                var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();

                SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");

                DataSet DS1 = OBJ_METHOD.Get_DataSet("phrmc_CompnyMstSeldel", false, true, SQL_PARAMS1);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = DS1.Tables[0].Rows[0]["ID"].ToString();
                    txtname.Text = DS1.Tables[0].Rows[0]["NAME"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
       
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
        string message = string.Empty;
        try
        {
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False'", false, false);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                string message1 = "alert('* You Cant Delete.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                return;
            }
            else
            {
                OBJ_METHOD = new DataMathods();
                string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();

                SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

                OBJ_METHOD.ExecuteProceedure("phrmc_CompnyMstInsUp ", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    binddata();
                }
                message = "alert('" + OBJ_METHOD._objOut + "')";
            }
        }
        catch (Exception ex)
        {

            OBJ_METHOD.commitOrRollbackTran("rollback");
            message = "alert('Due to some issues, Data not Deleted.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, "");
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INDEXCHG");

            DataSet DS1 = OBJ_METHOD.Get_DataSet("phrmc_CompnyMstSeldel", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
   
         
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void Btnsearch_click(object sender, EventArgs e)
    {
        try
        {
          
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME FROM COMPANY_TABLE WHERE NAME LIKE '" + txtsearchname.Text + "%' AND ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
            if (txtsearchname.Text == "")
            {
                string message = "alert('Search Company Name Is Mandatory..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
           
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtsearchname.Text);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SEARCH");

            DataSet DS1 = OBJ_METHOD.Get_DataSet("phrmc_CompnyMstSeldel", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            else
            {
                string message = "alert('No record Found..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
          
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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
    public DataTable getdata()
    {
        DataTable dt = new DataTable();

        DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT ID as ID,NAME FROM COMPANY_TABLE  where Branch_ID='" + Session["Branch"].ToString() + "' order by ID desc", false, false);

        dt = Ds.Tables[0];
        return dt;

    }
}