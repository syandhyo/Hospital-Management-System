using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class ADMIN_admin_custdiscountcard : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
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
            binddata();
        }
    }
    public void binddata()
    {
        DataSet Ds = OBJ_METHOD.Get_DataSet("select  * from TBL_CUSDISCARD where Branch_ID= " + Session["Branch"] + " order by ID desc", false, false);
        
        grvrooment.DataSource = Ds;
        grvrooment.DataBind();

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
    }
    public DataTable getdata()
    {
        DataTable dt = new DataTable();


        DataSet Ds = OBJ_METHOD.Get_DataSet("select  * from TBL_CUSDISCARD where Branch_ID= " + Session["Branch"] + " order by ID desc", false, false);

        dt = Ds.Tables[0];
        return dt;

    }
    public void clearcontrol()
    {
        txtcname.Text = "";
        txtdate.Text = "";
        txtcardfee.Text = "";
        txtvalid.Text = "";
        chkactive.Checked = false;
        btncreate.Visible = true;
        btnupdate.Visible = false;
        // dropWard.SelectedIndex = 0;
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            if (txtcname.Text.Trim() == "")
            {
                string message = "alert('* Card name are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcname.Focus();
                return;
            }
            else if (txtdate.Text.Trim() == "")
            {
                string message = "alert('* Date are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdate.Focus();
                return;
            }
            else if (txtcardfee.Text.Trim() == "")
            {
                string message = "alert('* Fees are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcardfee.Focus();
                return;
            }
            else if (txtvalid.Text.Trim() == "")
            {
                string message = "alert('* Valid Field are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtvalid.Focus();
                return;
            }
            string[] strdate = txtdate.Text.Split('-');

            DateTime date = new DateTime(Convert.ToInt32(strdate[2]), Convert.ToInt32(strdate[1]), Convert.ToInt32(strdate[0]));

            OBJ_METHOD = new DataMathods();
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[9];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@CDNAME", SqlDbType.VarChar, 0, txtcname.Text);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@CDFEE", SqlDbType.Decimal, 0, txtcardfee.Text);
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@VALID", SqlDbType.Int, 0, txtvalid.Text);
            SQL_PARAMS1[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS1[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS1[6] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS1[7] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, date);
            SQL_PARAMS1[8] = OBJ_METHOD.createParams("@ACTIVE", SqlDbType.Bit, 0, chkactive.Checked);

            OBJ_METHOD.ExecuteProceedure("ADMIN_Custom_DIS_CARD", "", "@msg", SqlDbType.VarChar, SQL_PARAMS1, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            
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
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            if (txtcname.Text.Trim() == "")
            {
                string message = "alert('* Card name are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcname.Focus();
                return;
            }
            else if (txtdate.Text.Trim() == "")
            {
                string message = "alert('* Date are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdate.Focus();
                return;
            }
            else if (txtcardfee.Text.Trim() == "")
            {
                string message = "alert('* Fees are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcardfee.Focus();
                return;
            }
            else if (txtvalid.Text.Trim() == "")
            {
                string message = "alert('* Valid Field are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtvalid.Focus();
                return;
            }
            else
            {
                OBJ_METHOD = new DataMathods();
                SqlParameter[] SQL_PARAMS1 = new SqlParameter[7];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@CDNAME", SqlDbType.VarChar, 0, txtcname.Text);
                SQL_PARAMS1[2] = OBJ_METHOD.createParams("@CDFEE", SqlDbType.Decimal, 0, txtcardfee.Text);
                SQL_PARAMS1[3] = OBJ_METHOD.createParams("@VALID", SqlDbType.Int, 0, txtvalid.Text);
                SQL_PARAMS1[4] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
                SQL_PARAMS1[5] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
                SQL_PARAMS1[6] = OBJ_METHOD.createParams("@ACTIVE", SqlDbType.Bit, 0, chkactive.Checked);

                OBJ_METHOD.ExecuteProceedure("ADMIN_Custom_DIS_CARD", "", "@msg", SqlDbType.VarChar, SQL_PARAMS1, true);

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
            message1 = "alert('Due to some issues, Data not updated.')";
        }
        finally
        {

            clearcontrol();
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

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
                DataSet Ds = OBJ_METHOD.Get_DataSet("select * from TBL_CUSDISCARD where ID='" + slno + "'", false, false);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = slno.ToString();

                    txtcname.Text = Ds.Tables[0].Rows[0]["CDNAME"].ToString();
                    txtcardfee.Text = Ds.Tables[0].Rows[0]["CDFEE"].ToString();
                    txtvalid.Text = Ds.Tables[0].Rows[0]["VALID"].ToString();
                    txtdate.Text = Convert.ToDateTime(Ds.Tables[0].Rows[0]["DATE"]).ToString("dd-MM-yyyy");
                    string chkactv = Ds.Tables[0].Rows[0]["ACTIVE"].ToString();
                    if (chkactv == "True")
                    {
                        chkactive.Checked = true;
                    }
                    else
                    {
                        chkactive.Checked = false;
                    }
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
            LinkButton db = (LinkButton)e.Row.Cells[5].Controls[0];

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

                 OBJ_METHOD.ExecuteProceedure("ADMIN_Custom_DIS_CARD", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
            message1 = "alert('Due to some issues, Data not deleted.')";
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
            DataSet Ds = OBJ_METHOD.Get_DataSet("select  * from TBL_CUSDISCARD where Branch_ID= " + Session["Branch"] + " order by ID desc", false, false);
        
            grvrooment.DataSource = Ds;
            grvrooment.PageIndex = e.NewPageIndex;
            grvrooment.DataKeyNames = new string[] { "ID" };
            grvrooment.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clearcontrol();
        btncreate.Visible = true;
        btnupdate.Visible=false;
        binddata();
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
}