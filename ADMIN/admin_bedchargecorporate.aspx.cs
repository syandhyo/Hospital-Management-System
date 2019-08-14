using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class ADMIN_admin_bedchargecorporate : System.Web.UI.Page
{
    string num1 = "SJ000";
    DataMathods OBJ_METHOD = new DataMathods();
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;


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
        Txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        if (!IsPostBack)
        {
            Session["SortedView"] = null;
            binddata();
            bindgridview();
        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message1 = "";
        try
        {
            if (dropdept.SelectedIndex == 0)
            {
                string message = "alert('* select Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropdept.Focus();
                return;
            }
            if (dropcorp.SelectedIndex == 0)
            {
                string message = "alert('* select Corporate.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropcorp.Focus();
                return;
            }
            if (dropward.SelectedIndex == 0)
            {
                string message = "alert('* select Ward.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropward.Focus();
                return;
            }
            
            else if (Convert.ToDecimal(txtcopro.Text) <= 0)
            {
                string message = "alert('* Enter Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcopro.Focus();
                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[7];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@WARDID", SqlDbType.Int, 0, dropward.SelectedValue);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, txtprice.Text);
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Txtdate.Text);
            SQL_PARAMS1[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS1[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS1[6] = OBJ_METHOD.createParams("@CORPORATE_ID", SqlDbType.Int, 0, dropcorp.SelectedValue);

            OBJ_METHOD.ExecuteProceedure("WARD_PRICE_CORPORATE_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS1, true);
            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
                binddata();
                clearcontrol();
            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message1 = "alert('Due To Some Issue Data Is Not Saved')";
            }
        }
        catch (Exception ex)
        {
            message1 = "alert('" + ex.Message + "')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
     
    }
    public void clearcontrol()
    {
        dropcorp.SelectedIndex = 0;
        dropward.SelectedIndex = 0;
        txtprice.Text = "0";
        btncreate.Visible = true;
        btnupdate.Visible = false;
        
    }
    public void binddata()
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT [id] AS ID,DeptName FROM tblDepartment WHERE Branch_ID='" + Session["Branch"].ToString() + "' AND ISACTIVE='True' ORDER BY ID DESC", false, false);

            dropdept.DataSource = Ds;
            dropdept.DataTextField = "DeptName";
            dropdept.DataValueField = "ID";
            dropdept.DataBind();
            dropdept.Items.Insert(0, new ListItem("Please Select", "0"));

            DataSet dt1 = OBJ_METHOD.Get_DataSet("select ID, CNAME from Corporate_Table", false, false);
            if (dt1.Tables[0].Rows.Count > 0)
            {
                dropcorp.DataSource = dt1;
                dropcorp.DataTextField = "CNAME";
                dropcorp.DataValueField = "ID";
                dropcorp.DataBind();
                //dropcorp.Items.Insert(0, "Please Select");
                dropcorp.Items.Insert(0, new ListItem("Please Select", "0")); //updated code
            }


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


    public void bindgridview()
    {
        try
        {
            DataSet DS = OBJ_METHOD.Get_DataSet("select A.ID,B.CNAME AS CNAME,C.NAME AS NAME,A.PRICE AS PRICE,A.DATE AS DATE from WARD_PRICE_CORPORATE as A,Corporate_Table AS B,WARD_TABLE AS C WHERE A.Branch_FY=B.Branch_ID AND A.Branch_ID=C.Branch_ID AND B.ID=A.CORPORATE_ID AND C.ID=A.WARDID AND A.Branch_ID='" + Session["Branch"].ToString() + "'", false, false);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = DS;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this info ?');";
        }
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message1 = "";
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
                string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();

                GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
                Label name = (Label)row.FindControl("lbl_name");
                string NAME = name.Text.ToString();

                SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATEDELETE");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);


                OBJ_METHOD.ExecuteProceedure("ADMIN_WARD_MASTER_CORPO", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {

                    OBJ_METHOD.commitOrRollbackTran("commit");
                    message1 = "alert('" + OBJ_METHOD._objOut + "')";
                    binddata();
                    bindgridview();

                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Due to some issues, Data not deleted.')";
                }

                // SqlCommand cm1 = new SqlCommand("delete from WARD_TABLE where ID='" + slno + "'", con);
                //update the cid=null


            }
        }
        catch (Exception ex)
        {

            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not deleted.')";
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            string str="select w.ID, w.NAME,w.PRICE,c.CNAME from WARD_TABLE w join Corporate_Table c on c.ID=w.C_ID where w.Branch_ID='" + Session["Branch"].ToString() + "'";
            DataSet ds = OBJ_METHOD.Get_DataSet(str);
            
            DataTable dt = new DataTable();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }

            GridView1.DataSource = dt;
            GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            if (Session["SortedView"] != null)
            {

                GridView1.DataSource = Session["SortedView"];
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void clear()
    {
        txtprice.Text = "0";
        dropcorp.SelectedIndex = 0;
        dropward.SelectedIndex = 0;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {

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

        DataSet DS = OBJ_METHOD.Get_DataSet("select w.ID, w.NAME,w.PRICE,c.CNAME from WARD_TABLE w join Corporate_Table c on c.ID=w.C_ID where w.Branch_ID='" + Session["Branch"].ToString() + "'", false, false);
        if (DS.Tables[0].Rows.Count > 0)
        {
            dt = DS.Tables[0];
        }
        return dt;

    }
    protected void dropdept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT DISTINCT ID, NAME FROM WARD_TABLE WHERE Branch_ID = " + Session["Branch"] + " and DeptID=" + dropdept.SelectedValue + " ORDER BY NAME ASC", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {

                dropward.DataSource = Ds.Tables[0];
                dropward.DataTextField = "NAME";
                dropward.DataValueField = "ID";
                dropward.DataBind();
                dropward.Items.Insert(0, new ListItem("Please Select", "0")); //updated code
                dropward.SelectedIndex = 0;
            }
            else
            {
                string message = "alert('* No Ward Assign to this Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropdept.Focus();
                return;

            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void dropward_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT PRICE FROM Ward_Price WHERE Branch_ID = " + Session["Branch"] + " and WARDID=" + dropward.SelectedValue + " and DATE=(select MAX(DATE) AS DATE from Ward_Price where Branch_ID =  " + Session["Branch"] + " and WARDID=" + dropward.SelectedValue + ") ", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {

                txtprice.Text = Ds.Tables[0].Rows[0]["PRICE"].ToString();
            }
            else
            {
                string message = "alert('* No Rate Available For this Ward.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropward.Focus();
                return;

            }
        }
        catch (Exception ex)
        {
        }
    }
}