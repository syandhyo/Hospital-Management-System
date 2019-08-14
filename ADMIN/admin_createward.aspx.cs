using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class ADMIN_admin_createward : System.Web.UI.Page
{
    string num1 = "SJ000";

    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
    int i, no, no1, sl,j;
    GridViewRow gr;
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
            LinkButton db = (LinkButton)e.Row.Cells[5].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this info ?');";
        }
    }
    public void binddata()
    {
        try
        {
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("SELECT [id] AS ID,DeptName FROM tblDepartment WHERE Branch_ID='" + Session["Branch"].ToString() + "' ORDER BY ID DESC", false, false);

            dropdept.DataSource = Ds1;
            dropdept.DataTextField = "DeptName";
            dropdept.DataValueField = "ID";
            dropdept.DataBind();
            dropdept.Items.Insert(0, new ListItem("Please Select", "0"));

            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT A.ID as ID,A.NAME,A.BED,A.PRIFIX,B.DeptName FROM WARD_TABLE AS A,[dbo].[tblDepartment] AS B where A.Branch_ID=B.Branch_ID AND A.STATUS='AVAILABLE' and B.ID=A.[DeptID] and A.Branch_ID='" + Session["Branch"].ToString() + "' ORDER BY ID DESC", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds;
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
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    public void clear_control()
    {
        txtname.Text = "";
        txtnoofbed.Text = "0";
        txtprifix.Text = "";
        droptype.SelectedIndex = 0;
        dropdept.SelectedIndex = 0;        
        btncreate.Visible = true;
        btnupdate.Visible = false;
        binddata();
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {

            if (txtname.Text.Trim() == "")
            {
                string message = "alert('* Ward name is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (txtnoofbed.Text == "")
            {
                string message = "alert('* No of Bed mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtnoofbed.Focus();
                return;
            }
            else if (txtprifix.Text.Trim() == "")
            {
                string message = "alert('* Prefix is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtprifix.Focus();
                return;
            }
            
            else if (Convert.ToDecimal(txtnoofbed.Text) <= 0)
            {
                string message = "alert('* Bed is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtnoofbed.Focus();
                return;
            }

            SqlParameter[] SQL_PARAMS = new SqlParameter[9];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@BED", SqlDbType.VarChar, 500, txtnoofbed.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@PRIFIX", SqlDbType.VarChar, 500, txtprifix.Text.ToUpper());
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DeptID", SqlDbType.Int, 0, dropdept.SelectedValue);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@Type", SqlDbType.VarChar, 50, droptype.SelectedValue);
            

            OBJ_METHOD.ExecuteProceedure("ADMIN_WARD_MASTER", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                string SLNO = OBJ_METHOD._objOut.ToString().Split('@')[0];
                string msg = OBJ_METHOD._objOut.ToString().Split('@')[1];
                no = 0;
                int cnt = 0;
                
                    for (i = 1; i <= Convert.ToInt32(txtnoofbed.Text); i++)
                    {
                        no = i;

                        SqlParameter[] SQL_PARAMS1 = new SqlParameter[8];

                        SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                        SQL_PARAMS1[1] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, SLNO);
                        SQL_PARAMS1[2] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, txtprifix.Text.ToUpper() + i);
                        SQL_PARAMS1[3] = OBJ_METHOD.createParams("@PRIFIX", SqlDbType.VarChar, 500, txtprifix.Text.ToUpper());
                        SQL_PARAMS1[4] = OBJ_METHOD.createParams("@STATUS", SqlDbType.VarChar, 100, "AVAILABLE");
                        SQL_PARAMS1[5] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                        SQL_PARAMS1[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS1[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                        OBJ_METHOD.ExecuteProceedure("ADMIN_BED_MATRIX_TABLE", "", "@msg", SqlDbType.VarChar, SQL_PARAMS1, true);
                        if (OBJ_METHOD._RESULT > 0)
                        {
                            cnt++;
                        }
                    }
                

                if (cnt == Convert.ToInt32(txtnoofbed.Text))
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    message1 = "alert('" + msg + "')";
                    binddata();
                }
                
              

            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
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
            clear_control();
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
 
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
       
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
        string message1 = string.Empty;
        try
        {
            OBJ_METHOD = new DataMathods();
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False'", false, false);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                string message = "alert('* You Cant Delete.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
                //DataMathods OBJ_METHOD = new DataMathods();
                string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();

                GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
                Label name = (Label)row.FindControl("lbl_name");
                string NAME = name.Text.ToString();

                //DataSet ds = OBJ_METHOD.Get_DataSet("select * from BED_MATRIX_TABLE where NAME='" + NAME + "' and STATUS='OCCUPIED' AND ORGID='" + lblorgid.Text + "'", false,false);
                DataSet ds = OBJ_METHOD.Get_DataSet("select * from BED_MATRIX_TABLE where NAME='" + slno + "' and STATUS='OCCUPIED' AND ORGID='" + lblorgid.Text + "'", false, false);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string message = "alert('*Some of the bed of this ward are in use.Unable to delete this ward.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                else
                {
                    OBJ_METHOD = new DataMathods();




                    SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "HIDE");

                    OBJ_METHOD.ExecuteProceedure("ADMIN_WARD_MASTER", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("commit");
                        binddata();
                        message1 = "alert('" + OBJ_METHOD._objOut + "')";
                    }

                }


            }
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not Deleted.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
       
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        clear_control();
       
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT A.ID as ID,A.NAME,A.BED,A.PRIFIX,B.DeptName FROM WARD_TABLE AS A,[dbo].[tblDepartment] AS B where A.Branch_ID=B.Branch_ID AND A.STATUS='AVAILABLE' and B.ID=A.[DeptID] and A.Branch_ID='" + Session["Branch"].ToString() + "' ORDER BY ID DESC", false, false);
           if (Ds.Tables[0].Rows.Count > 0)
            {
            GridView1.DataSource = Ds;
            GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            }
            
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

        DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT A.ID as ID,A.NAME,A.BED,A.PRIFIX,B.DeptName FROM WARD_TABLE AS A,[dbo].[tblDepartment] AS B where A.Branch_ID=B.Branch_ID AND A.STATUS='AVAILABLE' and B.ID=A.[DeptID] and A.Branch_ID='" + Session["Branch"].ToString() + "' ORDER BY ID DESC", false, false);

        dt = Ds.Tables[0];
        return dt;

    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            DataSet Dt = OBJ_METHOD.Get_DataSet("SELECT ID as ID,NAME,BED,PRIFIX,PRICE FROM WARD_TABLE where ID='" + slno + "' ", false, false);
            if (Dt.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = slno.ToString();
                txtname.Text = Dt.Tables[0].Rows[0]["NAME"].ToString();
                txtnoofbed.Text = Dt.Tables[0].Rows[0]["BED"].ToString();
                txtprifix.Text = Dt.Tables[0].Rows[0]["PRIFIX"].ToString();
                //txtprice.Text = Dt.Tables[0].Rows[0]["PRICE"].ToString();
            }

        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    
}