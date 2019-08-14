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


public partial class ACCOUNTS_account_new_assets_entry : System.Web.UI.Page
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
       
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
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

           // DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT tblAssets.id,tblAssetsEntry.Assets,tblAssets.Quantity, tblAssets.Amount,tblAssets.TotalAmount,  tblAssets.Adate, tblAssetsEntry.Assets FROM tblAssets INNER JOIN tblAssetsEntry ON tblAssets.AssetsId = tblAssetsEntry.id  order by tblAssets.id desc", false,false);
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"].ToString());
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("Sp_New_AssestSelect", false, true, SQL_PARAMS1);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds1;
                GridView1.DataBind();
            }

            DataSet Dt1 = OBJ_METHOD.Get_DataSet("select id,Assets from tblAssetsEntry", false, false);
            dropassets.DataSource = Dt1;
            dropassets.DataTextField = "Assets";
            dropassets.DataValueField = "id";
            dropassets.DataBind();
            dropassets.Items.Insert(0, new ListItem("Please Select", "0"));
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

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@id", SqlDbType.Int, 0, slno);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

            OBJ_METHOD.ExecuteProceedure("Acct_NewAssets", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[6].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this patient info ?');";
        }
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();

            DataSet Dt = OBJ_METHOD.Get_DataSet("SELECT id,AssetsId,Quantity,Amount,TotalAmount,Adate from tblAssets where id='" + slno + "'", false,false);
            if (Dt.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = Dt.Tables[0].Rows[0]["ID"].ToString();
                dropassets.Text = Dt.Tables[0].Rows[0]["AssetsId"].ToString();
                txtqty.Text = Dt.Tables[0].Rows[0]["Quantity"].ToString();
                txtamount.Text = Dt.Tables[0].Rows[0]["Amount"].ToString();
                txttamount.Text = Dt.Tables[0].Rows[0]["TotalAmount"].ToString();
                txtdate.Text = Dt.Tables[0].Rows[0]["Adate"].ToString();
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

            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"].ToString());
            DataSet DS = OBJ_METHOD.Get_DataSet("Sp_New_AssestSelect", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "id" };
                GridView1.DataBind();
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
    
        if (dropassets.SelectedIndex == 0)
        {
            string message = "alert('* Please Select Assets.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            dropassets.Focus();
            return;
        }
        else if (txtqty.Text == "0")
        {
            string message = "alert('* Quanity is mandatory.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            txtqty.Focus();
            return;
        }
        else if (txtamount.Text == "")
        {
            string message = "alert('* Amount is mandatory.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            txtamount.Focus();
            return;
        }
        else if (Convert.ToDecimal(txtamount.Text) <= 0)
        {
            string str = "Amounts can't be less than 0";
            str = str.Replace("'", @"\'");

            string message = "alert('" + str + "')";
            //string message = "alert('*Amounts can't be less than 0')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }

       
        // date with time split format

        //string[] strmfd = txtdate.Text.Split('-');


        //int day = Convert.ToInt32(strmfd[0]);
        //int mon = Convert.ToInt32(strmfd[1]);
        //string yrwithtime = strmfd[2].ToString();

        //int yr = Convert.ToInt32(yrwithtime.Split(' ')[0]);
        //string times = yrwithtime.Split(' ')[1].ToString();

        //int hr = Convert.ToInt32(times.Split(':')[0]);
        //int min = Convert.ToInt32(times.Split(':')[1]);


        //DateTime mfd = new DateTime(yr, mon, day, hr, min, 0);
        string[] strexp = txtdate.Text.Split('-');

        DateTime mfd = new DateTime(Convert.ToInt32(strexp[2]), Convert.ToInt32(strexp[1]), Convert.ToInt32(strexp[0]));


        SqlParameter[] SQL_PARAMS = new SqlParameter[10];

        SQL_PARAMS[0] = OBJ_METHOD.createParams("@AssetsId", SqlDbType.VarChar, 500, dropassets.SelectedValue.ToString());
        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
        SQL_PARAMS[2] = OBJ_METHOD.createParams("@Quantity", SqlDbType.VarChar, 100, txtqty.Text);
        SQL_PARAMS[3] = OBJ_METHOD.createParams("@Amount", SqlDbType.Decimal, 0, txtamount.Text);
        SQL_PARAMS[4] = OBJ_METHOD.createParams("@TotalAmount", SqlDbType.Decimal, 0, txttamount.Text);
        SQL_PARAMS[5] = OBJ_METHOD.createParams("@ADate", SqlDbType.DateTime, 0, mfd);
        SQL_PARAMS[6] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 100, lbluid.Text);
        SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
        SQL_PARAMS[8] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
        SQL_PARAMS[9] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

        OBJ_METHOD.ExecuteProceedure("Acct_NewAssets", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
        dropassets.SelectedIndex = 0;
        txtqty.Text = "";
        txtamount.Text = "";
        txttamount.Text = "";
        //txtdate.Text = "";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (dropassets.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Assets.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropassets.Focus();
                return;
            }
            else if (txtqty.Text == "")
            {
                string message = "alert('* Quanity is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtqty.Focus();
                return;
            }
            else if (txtamount.Text == "")
            {
                string message = "alert('* Amount is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtamount.Focus();
                return;
            }
            
           
            SqlParameter[] SQL_PARAMS = new SqlParameter[6];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@AssetsId", SqlDbType.VarChar, 500, dropassets.SelectedValue.ToString());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@id", SqlDbType.Int, 0, txtid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Quantity", SqlDbType.VarChar, 100, txtqty.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Amount", SqlDbType.Decimal, 0, txtamount.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@TotalAmount", SqlDbType.Decimal, 0, txttamount.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

            OBJ_METHOD.ExecuteProceedure("Acct_NewAssets", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
            clearcontrol();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
       
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        clearcontrol();
        //binddata();
    }
    protected void txtqty_TextChanged(object sender, EventArgs e)
    {
        if (txtamount.Text == "")
        {
            txtamount.Text = "0";
        }
        else if (txtqty.Text == "")
        {
            txtqty.Text = "0";
        }
        else if (txtamount.Text == "")
        {
            txtamount.Text = "0";
        }
        try
        {
            txttamount.Text = Math.Round((Convert.ToDouble(txtqty.Text)) * Convert.ToDouble(txtamount.Text)).ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void txtprice_TextChanged(object sender, EventArgs e)
    {
        if (txtamount.Text == "")
        {
            txtamount.Text = "0";
        }
        else if (txtqty.Text == "")
        {
            txtqty.Text = "0";
        }
        else if (txtamount.Text == "")
        {
            txtamount.Text = "0";
        }
        try
        {
            txttamount.Text = Math.Round((Convert.ToDouble(txtqty.Text)) * Convert.ToDouble(txtamount.Text)).ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public DataTable getdata()
    {
        DataTable dt = new DataTable();

       // DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT id,ExpType,Amount,Edate from tblDailyExpense where Branch_ID='" + Session["Branch"].ToString() + "' order by id desc", false, false);
        SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

        SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
        SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"].ToString());
        DataSet Ds1 = OBJ_METHOD.Get_DataSet("Sp_New_AssestSelect", false, true, SQL_PARAMS1);
        if (Ds1.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = Ds1;
            GridView1.DataBind();
        }
        dt = Ds1.Tables[0];
        return dt;

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
}