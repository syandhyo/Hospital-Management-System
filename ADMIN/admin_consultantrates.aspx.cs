using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;

public partial class ADMIN_admin_consultantrates : System.Web.UI.Page
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
        lblid.Text = Session["NAME"].ToString();
        lbluid.Text = Session["UID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {
            Session["SortedView"] = null;
            binddata();
        }

        
        txtwef.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
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
       
       

            DataSet Dt1 = OBJ_METHOD.Get_DataSet("select * from INSURANCE_TBL", false, false);
            dropappl.DataSource = Dt1;
            dropappl.DataTextField = "COMNYNAME";
            dropappl.DataValueField = "ID";
            dropappl.DataBind();
            dropappl.Items.Insert(0, new ListItem("Please Select", "0"));

            DataSet Ds = OBJ_METHOD.Get_DataSet("select c.ID,c.WEF,i.COMNYNAME,C.RATES from CONS_RATES_TABLE C join INSURANCE_TBL i on c.APPLICABLE=i.ID WHERE c.Branch_ID = " + Session["Branch"] + " order by c.ID desc", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds;
                GridView1.DataBind();

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            // Validation
            if (txtrates.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (dropappl.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
           
            SqlParameter[] SQL_PARAMS = new SqlParameter[6];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@WEF", SqlDbType.Date, 0, Convert.ToDateTime(txtwef.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@APPLICABLE", SqlDbType.VarChar, 500, dropappl.SelectedValue);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@RATES", SqlDbType.Decimal, 0, txtrates.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));


            OBJ_METHOD.ExecuteProceedure("USP_CONS_RATES", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
        dropappl.SelectedIndex = 0;
        txtrates.Text = "";
        btnSubmit.Visible = true;
        btnupdate.Visible = false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtrates.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtrates.Focus();
                return;
            }
            else if (dropappl.SelectedItem.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropappl.Focus();
                return;
            }
                  
            SqlParameter[] SQL_PARAMS = new SqlParameter[5];
          
            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, txtid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@WEF", SqlDbType.Date, 0, Convert.ToDateTime(txtwef.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@APPLICABLE", SqlDbType.VarChar, 500, dropappl.SelectedValue);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@RATES", SqlDbType.Decimal, 0, txtrates.Text);

            OBJ_METHOD.ExecuteProceedure("USP_CONS_RATES", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                btnSubmit.Visible = true;
                btnupdate.Visible = false;
                binddata();
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
            
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
       
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        clearcontrol();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "Delete");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);

            OBJ_METHOD.ExecuteProceedure("USP_CONS_RATES", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                binddata();
                OBJ_METHOD.commitOrRollbackTran("commit");

            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";


        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not deleted.')";
        }
        finally
        {

            clearcontrol();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
        
             
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT * from CONS_RATES_TABLE where id=" + slno, false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                txtwef.Text = Ds.Tables[0].Rows[0]["WEF"].ToString();
                dropappl.SelectedValue = Ds.Tables[0].Rows[0]["APPLICABLE"].ToString();
                txtrates.Text = Ds.Tables[0].Rows[0]["RATES"].ToString();
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
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT * from CONS_RATES_TABLE", false, false);
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
        DataSet Ds = OBJ_METHOD.Get_DataSet("select c.ID,c.WEF,i.COMNYNAME,C.RATES from CONS_RATES_TABLE C join INSURANCE_TBL i on c.APPLICABLE=i.ID WHERE c.Branch_ID = " + Session["Branch"] + " order by c.ID desc", false, false);

        dt = Ds.Tables[0];
        return dt;
    }
}