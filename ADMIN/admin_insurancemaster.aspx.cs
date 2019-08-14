using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class ADMIN_admin_insurancemaster : System.Web.UI.Page
{
    string num1 = "SJ000";
    
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
    decimal total_amt = 0;
    decimal total_vat_amt = 0;
    int i, no, no1, sl;
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

        lblid.Text = Session["NAME"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
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


        //-----------------------------------
       // SqlDataAdapter Adp = new SqlDataAdapter("select ID,COMNYNAME,ADRESS,ISACTIVE from INSURANCE_TBL order by ID desc", con);
        DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,COMNYNAME,ADRESS,ISACTIVE from INSURANCE_TBL where Branch_ID= '" + Session["Branch"].ToString() + "'  order by ID desc", false, false);
        if (Ds.Tables[0].Rows.Count > 0)
        {
            grvrooment.DataSource = Ds;
            grvrooment.DataBind();
        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('* Name is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (txtaddress.Text == "")
            {
                string message = "alert('* Address is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtaddress.Focus();
                return;
            }
               
            DataMathods OBJ_METHOD = new DataMathods();

            SqlParameter[] SQL_PARAMS = new SqlParameter[6];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@COMNYNAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ADRESS", SqlDbType.VarChar, 500,txtaddress.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ISACTIVE", SqlDbType.VarChar, 100,  CheckBox1.Checked);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[5]= OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("ADMIN_INSURANCE_MASTER", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
            clear();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    public void clear()
    {
        txtname.Text = "";
        txtaddress.Text = "";
        CheckBox1.Checked = false;
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
    protected void grvrooment_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = grvrooment.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            DataSet Dt = OBJ_METHOD.Get_DataSet("select ID,COMNYNAME,ADRESS,ISACTIVE from INSURANCE_TBL where ID='" + slno + "'", false, false);
            if (Dt.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = slno.ToString();
                txtname.Text = Dt.Tables[0].Rows[0]["COMNYNAME"].ToString();
                txtaddress.Text = Dt.Tables[0].Rows[0]["ADRESS"].ToString();
                string chkDisp = Dt.Tables[0].Rows[0]["ISACTIVE"].ToString();
                if (chkDisp == "True")
                {
                    CheckBox1.Checked = true;
                }
                else
                {
                    CheckBox1.Checked = false;
                }
            }
            
       }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('* Name is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (txtaddress.Text == "")
            {
                string message = "alert('* Address is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtaddress.Focus();
                return;
            }
          
            SqlParameter[] SQL_PARAMS = new SqlParameter[5];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@COMNYNAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ADRESS", SqlDbType.VarChar, 500, txtaddress.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ISACTIVE", SqlDbType.VarChar, 100, CheckBox1.Checked);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, txtid.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

            OBJ_METHOD.ExecuteProceedure("ADMIN_INSURANCE_MASTER", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
            clear();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void grvrooment_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void grvrooment_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            string slno = grvrooment.DataKeys[e.RowIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);
           

            OBJ_METHOD.ExecuteProceedure("ADMIN_INSURANCE_MASTER", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

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
            clear();
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void grvrooment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            
          DataSet Ds=OBJ_METHOD.Get_DataSet("select ID,COMNYNAME,ADRESS,ISACTIVE from INSURANCE_TBL order by ID desc", false,false);
          if (Ds.Tables[0].Rows.Count > 0)
          {
              grvrooment.DataSource = dt;
              grvrooment.PageIndex = e.NewPageIndex;
              grvrooment.DataKeyNames = new string[] { "ID" };
              grvrooment.DataBind();
          }
           
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        clear();
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
        DataView sortedView = new DataView(getdata());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        Session["SortedView"] = sortedView;
        grvrooment.DataSource = sortedView;
        grvrooment.DataBind();
    }
    public DataTable getdata()
    {
        DataTable dt = new DataTable();

        DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,COMNYNAME,ADRESS,ISACTIVE from INSURANCE_TBL where Branch_ID= '" + Session["Branch"].ToString() + "'  order by ID desc", false, false);

        dt = Ds.Tables[0];
        return dt;

    }
}