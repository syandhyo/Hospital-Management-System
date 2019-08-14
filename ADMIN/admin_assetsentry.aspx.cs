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

public partial class ADMIN_admin_assetsentry : System.Web.UI.Page
{
    SqlDataReader dr;
   
    DataMathods OBJ_METHOD = new DataMathods();

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
                txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                binddata();
            }
           
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
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


                DataSet Dt = OBJ_METHOD.Get_DataSet("SELECT id,Assets,Quantity,ADate from tblAssetsEntry where Branch_ID = " + Session["Branch"] + " order by id desc", false, false);
                if (Dt.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = Dt;
                    GridView1.DataBind();
                }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message = string.Empty;
        try
        {
            string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();

            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

            OBJ_METHOD.ExecuteProceedure("ADMIN_ASSET_ENTRY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
            message = "alert('Due to some issues, Data not Deleted.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

                db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
            }
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
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
            DataSet dt = OBJ_METHOD.Get_DataSet("SELECT id,Assets,Quantity,ADate from tblAssetsEntry where id='" + slno + "'", false,false);
            if (dt.Tables[0].Rows.Count > 0)
            {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = slno.ToString();
                    txtassets.Text = dt.Tables[0].Rows[0]["Assets"].ToString();
                    txtqty.Text = dt.Tables[0].Rows[0]["Quantity"].ToString();
                    txtdate.Text = Convert.ToDateTime(dt.Tables[0].Rows[0]["ADate"]).ToString("dd-MM-yyyy");
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
           DataSet dt = OBJ_METHOD.Get_DataSet("SELECT id,Assets,Quantity,ADate from tblAssetsEntry where Branch_ID = " + Session["Branch"] + " order by id desc", false, false);
           GridView1.DataSource = dt;
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtassets.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtassets.Focus();
                return;
            }
            else if (txtqty.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtqty.Focus();
                return;
            }
            string[] strdate = txtdate.Text.Split('-');

            DateTime date = new DateTime(Convert.ToInt32(strdate[2]), Convert.ToInt32(strdate[1]), Convert.ToInt32(strdate[0]));

            SqlParameter[] SQL_PARAMS = new SqlParameter[7];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Assets", SqlDbType.VarChar, 500, txtassets.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Quantity", SqlDbType.VarChar, 500, txtqty.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ADate", SqlDbType.DateTime, 0, date);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@BRANCH_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("ADMIN_ASSET_ENTRY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
        txtassets.Text = "";
        txtqty.Text = "";
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtassets.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtassets.Focus();
                return;
            }
            else if (txtqty.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtqty.Focus();
                return;
            }
            string[] strdate = txtdate.Text.Split('-');

            DateTime date = new DateTime(Convert.ToInt32(strdate[2]), Convert.ToInt32(strdate[1]), Convert.ToInt32(strdate[0]));
            
           SqlParameter[] SQL_PARAMS = new SqlParameter[5];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Assets", SqlDbType.VarChar, 500, txtassets.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Quantity", SqlDbType.VarChar, 500, txtqty.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ADate", SqlDbType.DateTime, 0, date);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

            OBJ_METHOD.ExecuteProceedure("ADMIN_ASSET_ENTRY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
            message1 = "alert('Due to some issues, Data not updated.')";
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

        DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT id,Assets,Quantity,ADate from tblAssetsEntry where Branch_ID = " + Session["Branch"] + " order by id desc", false, false);

        dt = Ds.Tables[0];
        return dt;

    }
}