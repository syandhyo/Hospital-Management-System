using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class ADMIN_admin_referalmaster : System.Web.UI.Page
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


        SqlParameter[] SQL_PARAMS = new SqlParameter[1];

        SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

        DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
        if (DS.Tables[0].Rows.Count > 0)
        {
            lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
        }
       
        if (!IsPostBack)
        {
            Session["SortedView"] = null;
            binddata();
        }
       
    }
    public void binddata()
    {
        SqlParameter[] SQL_PARAMS = new SqlParameter[2];

        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
        SQL_PARAMS[1] = OBJ_METHOD.createParams("@BRANCH_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
       
        DataSet Ds = OBJ_METHOD.Get_DataSet("ADMIN_REFERAL_MASTERSEL", false, true, SQL_PARAMS);
        if (Ds.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = Ds;
            GridView1.DataBind();
        }
      
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtname.Text.Trim() == "")
            {
                string message = "alert('* Name is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (txtphone.Text.Trim() == "")
            {
                string message = "alert('* Phone is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtphone.Focus();
                return;
            }
            DataMathods OBJ_METHOD = new DataMathods();
         
            SqlParameter[] SQL_PARAMS = new SqlParameter[7];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ISACTIVE", SqlDbType.VarChar, 500, CheckBox1.Checked);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@PHONE", SqlDbType.VarChar, 100, txtphone.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ADDRESS", SqlDbType.VarChar, 500, txtaddress.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("ADMIN_REFERAL_MASTER", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtname.Text.Trim() == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (txtphone.Text.Trim() == "")
            {
                string message = "alert('* Phone is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtphone.Focus();
                return;
            }

            DataMathods OBJ_METHOD = new DataMathods();
            //using (SqlCommand cmd1 = new SqlCommand("ADMIN_REFERAL_MASTER", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cmd1.Parameters.Add("@ID", SqlDbType.Int).Value = txtid.Text;
            //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
            //    cmd1.Parameters.Add("@ISACTIVE", SqlDbType.VarChar).Value = CheckBox1.Checked;
            //    cmd1.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = txtphone.Text;
            //    cmd1.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = txtaddress.Text;
            //    cmd1.ExecuteNonQuery();
            //}
            SqlParameter[] SQL_PARAMS = new SqlParameter[6];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ISACTIVE", SqlDbType.VarChar, 500, CheckBox1.Checked);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@PHONE", SqlDbType.VarChar, 100, txtphone.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ADDRESS", SqlDbType.VarChar, 500, txtaddress.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, txtid.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

            OBJ_METHOD.ExecuteProceedure("ADMIN_REFERAL_MASTER", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                btncreate.Visible = true;
                btnupdate.Visible = false;
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();

                message1 = "alert('" + OBJ_METHOD._objOut + "')";

            }
           
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

                OBJ_METHOD.ExecuteProceedure("ADMIN_REFERAL_MASTER", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@BRANCH_ID", SqlDbType.Int, 0, Session["Branch"].ToString());

            DataSet Ds = OBJ_METHOD.Get_DataSet("ADMIN_REFERAL_MASTERSEL", false, true, SQL_PARAMS);
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this Referal Master ?');";
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
                 var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();

                 DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT ID,NAME,PHONE,ISACTIVE,ADDRESS from Broker_Table where ID ='" + slno + "'", false, false);
                 if (Ds.Tables[0].Rows.Count > 0)
                 {
                     btncreate.Visible = false;
                     btnupdate.Visible = true;
                     txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                     txtname.Text = Ds.Tables[0].Rows[0]["NAME"].ToString();
                     txtphone.Text = Ds.Tables[0].Rows[0]["PHONE"].ToString();
                     txtaddress.Text = Ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                     string chkactv = Ds.Tables[0].Rows[0]["ISACTIVE"].ToString();
                     if (chkactv == "True")
                     {
                         CheckBox1.Checked = true;
                     }
                     else
                     {
                         CheckBox1.Checked = false;
                     }
                 }
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
    public void clearcontrol()
    {
        txtaddress.Text = "";
        txtname.Text = "";
        txtphone.Text = "";
        CheckBox1.Checked = false;
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

        DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,NAME,PHONE,ISACTIVE,ADDRESS from Broker_Table where Branch_ID='" + Session["Branch"].ToString() + "' order by ID desc", false, false);

        dt = Ds.Tables[0];
        return dt;

    }
}