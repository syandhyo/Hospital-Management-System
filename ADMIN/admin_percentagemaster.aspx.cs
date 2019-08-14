using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class ADMIN_admin_percentagemaster : System.Web.UI.Page
{
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
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }

           
            binddata();

        }

    }
    protected void drptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (drptype.SelectedValue == "1")
            {
                datacallstaff();
            }
            else if (drptype.SelectedValue == "2")
            {
                datacallbroker();
            }

        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void datacallstaff()
    {
        DataSet dt = OBJ_METHOD.Get_DataSet("Select EMPID,Sname from tblStaff ", false, false);
        if (dt.Tables[0].Rows.Count > 0)
        {
            droprecipient.DataSource = dt;
            droprecipient.DataTextField = "Sname";
            droprecipient.DataValueField = "EMPID";
            droprecipient.DataBind();
            droprecipient.Items.Insert(0, new ListItem("Please Select", "0"));
        }
    }
    public void datacallbroker()
    {
        DataSet dt1 = OBJ_METHOD.Get_DataSet("select ID,NAME from Broker_Table", false, false);
        if (dt1.Tables[0].Rows.Count > 0)
        {
            droprecipient.DataSource = dt1;
            droprecipient.DataTextField = "NAME";
            droprecipient.DataValueField = "ID";
            droprecipient.DataBind();
            droprecipient.Items.Insert(0, new ListItem("Please Select", "0"));
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (droprecipient.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Recipient .')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                droprecipient.Focus();
                return;
            }
            else if (txtroomcharge.Text.Trim() == "")
            {
                string message = "alert('*Field is Mandatory .')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtroomcharge.Focus();
                return;
            }
            else if (txtdiscpharmacy.Text.Trim() == "")
            {
                string message = "alert('*Field is Mandatory ')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdiscpharmacy.Focus();
                return;
            }
            else if (txtdisclab.Text.Trim() == "")
            {
                string message = "alert('* Field is Mandatory')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdisclab.Focus();
                return;
            }

          
            DataMathods OBJ_METHOD = new DataMathods();

            SqlParameter[] SQL_PARAMS = new SqlParameter[10];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@RES", SqlDbType.VarChar, 500, droprecipient.SelectedValue);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Type", SqlDbType.Int, 0, drptype.SelectedValue);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ROOMRENT", SqlDbType.Decimal, 0, txtroomcharge.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@PHARMACY", SqlDbType.Decimal, 0, txtdiscpharmacy.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@LAB", SqlDbType.Decimal, 0, txtdisclab.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@OTHERS", SqlDbType.Decimal, 0, txtdiscothers.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@RADIO", SqlDbType.Decimal, 0, txtradiology.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("ADMIN_PERCNT_ENTRY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
        droprecipient.SelectedIndex = 0;
        drptype.SelectedIndex = 0;
        txtdisclab.Text = "0.00";
        txtdiscpharmacy.Text = "0.00";
        txtradiology.Text = "0.00";
        txtroomcharge.Text = "0.00";
        txtdiscothers.Text = "0.00";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (droprecipient.SelectedItem.Text.Trim() == "")
            {
                string message = "alert('* Please Select Recipient .')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                droprecipient.Focus();
                return;
            }
            else if (txtroomcharge.Text.Trim() == "")
            {
                string message = "alert('*Field is Mondatary .')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtroomcharge.Focus();
                return;
            }
            else if (txtdiscpharmacy.Text.Trim() == "")
            {
                string message = "alert('*Field is Mondatary ')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdiscpharmacy.Focus();
                return;
            }
            else if (txtdisclab.Text.Trim() == "")
            {
                string message = "alert('* Field is Mondatary')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdisclab.Focus();
                return;
            }

            DataMathods OBJ_METHOD = new DataMathods();

            SqlParameter[] SQL_PARAMS = new SqlParameter[9];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@RES", SqlDbType.VarChar, 500, droprecipient.SelectedValue);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Type", SqlDbType.Int, 0, drptype.SelectedValue);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ROOMRENT", SqlDbType.Decimal, 0, txtroomcharge.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@PHARMACY", SqlDbType.Decimal, 0, txtdiscpharmacy.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@LAB", SqlDbType.Decimal, 0, txtdisclab.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@OTHERS", SqlDbType.Decimal, 0, txtdiscothers.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@RADIO", SqlDbType.Decimal, 0, txtradiology.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@ID", SqlDbType.Decimal, 0, txtid.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

            OBJ_METHOD.ExecuteProceedure("ADMIN_PERCNT_ENTRY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                btncreate.Visible = true;
                btnupdate.Visible = false;
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
    public void binddata()
    {
       
        SqlParameter[] SQL_PARAMS = new SqlParameter[2];

        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
        SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
    

        DataSet DS = OBJ_METHOD.Get_DataSet("USP_Percentage_View", false, true, SQL_PARAMS);
        if (DS.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = DS;
            GridView1.DataBind();
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[8].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this Percentage Master ?');";
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableCell statusCell = e.Row.Cells[1];
            if (statusCell.Text == "1")
            {
                statusCell.Text = "Staff";
            }
            if (statusCell.Text == "2")
            {
                statusCell.Text = "Broker";
            }
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

        SqlParameter[] SQL_PARAMS = new SqlParameter[2];

        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
        SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());


        DataSet DS = OBJ_METHOD.Get_DataSet("USP_Percentage_View", false, true, SQL_PARAMS);
        if (DS.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = DS;
            GridView1.DataBind();
        }

        dt = DS.Tables[0];
        return dt;

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

                 DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,Type,RES,ROOMRENT,PHARMACY,LAB,OTHERS,RADIO from Percentage_Table where ID ='" + slno + "'", false, false);
                 if (Ds.Tables[0].Rows.Count > 0)
                 {
                     btncreate.Visible = false;
                     btnupdate.Visible = true;
                     txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                     drptype.SelectedValue = Ds.Tables[0].Rows[0]["Type"].ToString();
                     var slmn = Ds.Tables[0].Rows[0]["Type"].ToString();
                     if (slmn == "1")
                     {
                         datacallstaff();
                         // droprecipient.SelectedValue = Ds.Tables[0].Rows[0]["RES"].ToString();
                     }
                     else
                     {
                         datacallbroker();
                         // droprecipient.SelectedValue = Ds.Tables[0].Rows[0]["RES"].ToString();
                     }
                     droprecipient.SelectedValue = Ds.Tables[0].Rows[0]["RES"].ToString();
                     txtroomcharge.Text = Ds.Tables[0].Rows[0]["ROOMRENT"].ToString();
                     txtdiscpharmacy.Text = Ds.Tables[0].Rows[0]["PHARMACY"].ToString();
                     txtdisclab.Text = Ds.Tables[0].Rows[0]["LAB"].ToString();
                     txtdiscothers.Text = Ds.Tables[0].Rows[0]["OTHERS"].ToString();
                     txtradiology.Text = Ds.Tables[0].Rows[0]["RADIO"].ToString();

                 }
             }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message = string.Empty;
        DataMathods OBJ_METHOD = new DataMathods();
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
                 string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
                 //SqlCommand cm = new SqlCommand("delete from Broker_Table where ID='" + slno + "'", con);
                 SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                 SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);
                 SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

                 OBJ_METHOD.ExecuteProceedure("ADMIN_PERCNT_ENTRY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
            DataSet Ds = OBJ_METHOD.Get_DataSet("select Type,RES,ROOMRENT,PHARMACY,LAB,OTHERS,RADIO from Percentage_Table where Branch_ID='" + Session["Branch"].ToString() + "' ", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds;
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
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clearcontrol();
    }
}