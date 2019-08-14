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

public partial class ADMIN_admin_testcategory : System.Web.UI.Page
{
    string num1 = "SJ000";
    //SqlConnection con;
    //SqlCommand com, cmd;
    //SqlDataReader dr;
    //SqlDataAdapter da;
    //DataTable dt;
    //decimal total_amt = 0;
    //decimal total_vat_amt = 0;
    //int i, no, no1, sl;
    //GridViewRow gr;
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
            binddata();

        }
    }
    public void binddata()
    {
        SqlParameter[] SQL_PARAMS = new SqlParameter[3];

        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
        SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 500, Session["Branch"]);

        DataSet DS = OBJ_METHOD.Get_DataSet("ADMIN_LAB_TESTCATAGORY", false, true, SQL_PARAMS);
        if (DS.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = DS;
            GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();
        }
    }
    public DataTable getdata()
    {
        DataTable dt = new DataTable();
        SqlParameter[] SQL_PARAMS = new SqlParameter[3];

        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
        SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 500, Session["Branch"]);

        DataSet DS = OBJ_METHOD.Get_DataSet("ADMIN_LAB_TESTCATAGORY", false, true, SQL_PARAMS);

        dt = DS.Tables[0];
        return dt;

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[1].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('* Please Enter Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[5];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));


            OBJ_METHOD.ExecuteProceedure("ADMIN_TestCategory1", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
        txtname.Text = "";
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
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, txtid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());

            OBJ_METHOD.ExecuteProceedure("ADMIN_TestCategory1", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
        clearcontrol();
        //binddata();
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
                var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();

                DataSet Ds = OBJ_METHOD.Get_DataSet("select * from TEST_CATEGORY_TABLE where ID=" + slno, false, false);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                    txtname.Text = Ds.Tables[0].Rows[0]["NAME"].ToString();
                }
                //dr.Close();
            }
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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
                string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
                SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);

                OBJ_METHOD.ExecuteProceedure("ADMIN_TestCategory1", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 500, Session["Branch"]);

            DataSet DS = OBJ_METHOD.Get_DataSet("ADMIN_LAB_TESTCATAGORY", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
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
}