using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public partial class ADMIN_admin_charges : System.Web.UI.Page
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

            if (IsPostBack != true)
            {
                //DataTable dt = new DataTable();
                //dt.Columns.AddRange(new DataColumn[6] { new DataColumn("ID"), new DataColumn("NAME"), new DataColumn("INV"), new DataColumn("RANGE"), new DataColumn("UNIT"), new DataColumn("PRICE") });
                //ViewState["ITEM"] = dt;
            
                SqlParameter[] SQL_PARAMS = new SqlParameter[1];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECTDROP");

                DataSet DS = OBJ_METHOD.Get_DataSet("sp_Admin_Charge_DRP", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ddlcategory.DataSource = DS;
                    ddlcategory.DataTextField = "CATEGORY";
                    ddlcategory.DataValueField = "ID";
                    ddlcategory.DataBind();
                    ddlcategory.Items.Insert(0, "Please Select");
                }
            }
            if (!IsPostBack)
            {
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
        try
        {

            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }


            DataSet DS1 = OBJ_METHOD.Get_DataSet("select SLNO,Charge,Price,catagory from tblChargeMaster where  CorporateID ='0' order by SLNO desc", false, false);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = DS1;
                GridView1.DataKeyNames = new string[] { "SLNO" };
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLog.Write(ex);
        }
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message = string.Empty;
        try
        {
            OBJ_METHOD = new DataMathods();
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
                 string slno = GridView1.DataKeys[e.RowIndex].Values["SLNO"].ToString();

                 SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                 SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);
                 SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

                 OBJ_METHOD.ExecuteProceedure("ADMIN_Charges_Master", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this info ?');";
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       try
        {
            DataSet dt = OBJ_METHOD.Get_DataSet("select SLNO,Charge,Price,catagory from tblChargeMaster where  CorporateID ='0' order by SLNO desc", false, false);
            GridView1.DataSource = dt;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataKeyNames = new string[] { "SLNO" };
            GridView1.DataBind();
            if (Session["SortedView"] != null)
            {
                GridView1.DataSource = Session["SortedView"];
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
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
             DataSet Ds3 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Edit' and selected='False'", false, false);
             if (Ds3.Tables[0].Rows.Count > 0)
             {
                 string message = "alert('* You Cant Edit.')";
                 ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                 return;
             }
             else
             {
                 var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["SLNO"].ToString();

                 DataSet Dt = OBJ_METHOD.Get_DataSet("select SLNO,Charge,Price,catagory from tblChargeMaster where SLNO= '" + slno + "'", false, false);
                 if (Dt.Tables[0].Rows.Count > 0)
                 {
                     btnSubmit.Visible = false;
                     btnupdate.Visible = true;
                     txtid.Text = Dt.Tables[0].Rows[0]["SLNO"].ToString();
                     txtcharge.Text = Dt.Tables[0].Rows[0]["Charge"].ToString();
                     txtprice.Text = Dt.Tables[0].Rows[0]["Price"].ToString();
                     ddlcategory.SelectedItem.Text = Dt.Tables[0].Rows[0]["catagory"].ToString();
                 }
             }
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
            if (ddlcategory.SelectedIndex == 0)
            {
                string message = "alert('* Please Select The Category..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                ddlcategory.Focus();
                return;
            }
            else if (txtcharge.Text == "")
            {
                string message = "alert('* Please Enter Charge Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcharge.Focus();
                return;
            }
            else if (txtprice.Text == "" || Convert.ToDecimal(txtprice.Text) <= 0)
            {
                string message = "alert('* Please Enter Price..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtprice.Focus();
                return;
            }

            SqlParameter[] SQL_PARAMS = new SqlParameter[7];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, ddlcategory.SelectedItem.Text.ToUpper());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Charge", SqlDbType.VarChar, 500, txtcharge.Text.ToUpper());
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Price", SqlDbType.Decimal, 500, txtprice.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@BRANCH_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("ADMIN_Charges_Master", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
        ddlcategory.SelectedIndex = 0;
        txtcharge.Text = "";
        txtprice.Text = "0";
        btnSubmit.Visible = true;
        btnupdate.Visible = false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            
            if (txtcharge.Text == "")
            {
                string message = "alert('* Please Enter Charge Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcharge.Focus();
                return;
            }
            else if (txtprice.Text == "" || Convert.ToDecimal(txtprice.Text) <= 0)
            {
                string message = "alert('* Please Enter Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtprice.Focus();
                return;
            }

            SqlParameter[] SQL_PARAMS = new SqlParameter[5];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, ddlcategory.SelectedItem.Text.ToUpper());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, txtid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Charge", SqlDbType.VarChar, 500, txtcharge.Text.ToUpper());
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Price", SqlDbType.Decimal, 500, txtprice.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

            OBJ_METHOD.ExecuteProceedure("ADMIN_Charges_Master", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                btnSubmit.Visible = true;
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
        clearcontrol();
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

        DataSet Ds = OBJ_METHOD.Get_DataSet("select SLNO,Charge,Price,catagory from tblChargeMaster where  CorporateID ='0' order by SLNO desc", false, false);

        dt = Ds.Tables[0];
        return dt;

    }
}