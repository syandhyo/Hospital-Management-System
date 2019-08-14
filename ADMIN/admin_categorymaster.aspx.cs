using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class ADMIN_admin_categorymaster : System.Web.UI.Page
{

    DataMathods OBJ_METHOD = new DataMathods();
    DataTable dt;
    string message;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void binddata()
    {
        SqlParameter[] SQL_PARAMS = new SqlParameter[1];

        SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

        DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
        if (DS.Tables[0].Rows.Count > 0)
        {
            lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
        }
      
        DataSet Dt = OBJ_METHOD.Get_DataSet("select * from CATEGORY_MASTER WHERE Branch_ID = " + Session["Branch"] + " order by ID desc", false, false);
        if (Dt.Tables[0].Rows.Count > 0)
        {
            grvcorprte.DataSource = Dt;
            grvcorprte.DataBind();
        }

    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message = string.Empty;
        try
        {
            // Validation
            if (txtname.Text == "")
            {
                string message1 = "alert('* Name Is Mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                txtname.Focus();
                return;
            }

            SqlParameter[] SQL_PARAMS = new SqlParameter[5];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@BRANCH_FYR", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("ADMIN_Category_Master", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
            resetCt();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    public void resetCt()
    {
        txtname.Text = "";
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message = string.Empty;
        try
        {
            if (txtname.Text == "")
            {
                string message1 = "alert('* Fields Are Mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                txtname.Focus();
                return;
            }

            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, txtid.Text);

            // SQL_PARAMS[2] = OBJ_METHOD.createParams("@BRANCH_FYR", SqlDbType.Int, 0, );
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

            OBJ_METHOD.ExecuteProceedure("ADMIN_Category_Master", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                btncreate.Visible = true;
                btnupdate.Visible = false;
            }

            message = "alert('" + OBJ_METHOD._objOut + "')";

        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message = "alert('Due to some issues, Data not Updated.')";
        }
        finally
        {
            resetCt();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        resetCt();
    }
    protected void grvcorprte_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
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
                  var slno = grvcorprte.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();

                  DataSet Dt = OBJ_METHOD.Get_DataSet("select * from CATEGORY_MASTER  where id='" + slno + "' ", false, false);
                  //    OBJ_METHOD.Get_DataSet("ADMIN_Category_Master", false, true);
                  if (Dt.Tables[0].Rows.Count > 0)
                  {
                      // lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
                      btncreate.Visible = false;
                      btnupdate.Visible = true;
                      txtid.Text = slno.ToString();
                      txtname.Text = Dt.Tables[0].Rows[0]["CATEGORY"].ToString();
                  }
              }
        }
        catch (Exception ex)
        {
            //con.Close();
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void grvcorprte_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[2].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    protected void grvcorprte_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message = string.Empty;
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False'", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                string message1 = "alert('* You Cant Delete.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                return;
            }
            else
            {
                OBJ_METHOD = new DataMathods();
                string slno = grvcorprte.DataKeys[e.RowIndex].Values["ID"].ToString();

                SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

                OBJ_METHOD.ExecuteProceedure("ADMIN_Category_Master", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
    protected void grvcorprte_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet dt = OBJ_METHOD.Get_DataSet("select * from CATEGORY_MASTER order by ID desc", false, false);
            grvcorprte.DataSource = dt;
            grvcorprte.PageIndex = e.NewPageIndex;
            grvcorprte.DataKeyNames = new string[] { "id" };
            grvcorprte.DataBind();
            if (Session["SortedView"] != null)
            {

                grvcorprte.DataSource = Session["SortedView"];
                grvcorprte.DataBind();
            }
            else
            {
                grvcorprte.DataSource = dt;
                grvcorprte.DataBind();

            }

        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }


    }
    protected void grvcorprte_Sorting(object sender, GridViewSortEventArgs e)
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
        grvcorprte.DataSource = sortedView;
        grvcorprte.DataBind();
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

        DataSet Ds = OBJ_METHOD.Get_DataSet("select * from CATEGORY_MASTER WHERE Branch_ID = " + Session["Branch"] + " order by ID desc", false, false);

        dt = Ds.Tables[0];
        return dt;

    }
}