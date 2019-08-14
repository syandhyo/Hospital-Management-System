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

public partial class ACCOUNTS_account_ambulance_fuel : System.Web.UI.Page
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
            binddata();

           
        }
       
        //  txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
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

            DataSet Dt = OBJ_METHOD.Get_DataSet("select id,AmbulanceNo from tblAmbulanceEntry", false, false);
            dropambulanceno.DataSource = Dt;
            dropambulanceno.DataTextField = "AmbulanceNo";
            dropambulanceno.DataValueField = "id";
            dropambulanceno.DataBind();
            dropambulanceno.Items.Insert(0, new ListItem("Please Select", "0"));


            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());

            DataSet DS1 = OBJ_METHOD.Get_DataSet("Sp_AmbulanceFuel_sel", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
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
    
            // string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();
            //SqlCommand cm = new SqlCommand("delete from tblAmbulanceFuel where id='" + slno + "'", con);
            //cm.ExecuteNonQuery();

            //binddata();
            
       string message = string.Empty;
        try
        {
            string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();

            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@id", SqlDbType.Int, 0, slno);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

            OBJ_METHOD.ExecuteProceedure("Acct_AmbulanceFuel", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
            LinkButton db = (LinkButton)e.Row.Cells[5].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this patient info ?');";
        }
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
            DataSet Dt = OBJ_METHOD.Get_DataSet("SELECT id,AmbulanceNo,FuelissuePrice,FuelConsumption,ADate from tblAmbulanceFuel where id='" + slno + "'", false,false);
            if (Dt.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = Dt.Tables[0].Rows[0]["ID"].ToString();
                dropambulanceno.Text = Dt.Tables[0].Rows[0]["AmbulanceNo"].ToString();
                txtfuelprice.Text = Dt.Tables[0].Rows[0]["FuelissuePrice"].ToString();
                txtfuelconp.Text = Dt.Tables[0].Rows[0]["FuelConsumption"].ToString();
                txtdate.Text = Convert.ToDateTime(Dt.Tables[0].Rows[0]["Adate"]).ToString("dd-MM-yyyy");
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
           
            //using (SqlCommand cmd = new SqlCommand("Acct_AmbulanceFuel", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
            //    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@AmbulanceNo", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@FuelissuePrice", SqlDbType.Decimal).Value = "0";
            //    cmd.Parameters.Add("@FuelConsumption", SqlDbType.Decimal).Value = "0";
            //    cmd.Parameters.Add("@Adate", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
            //    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
            //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    adp.Fill(dt);

            //    GridView1.DataSource = dt;
            //    GridView1.PageIndex = e.NewPageIndex;
            //    GridView1.DataKeyNames = new string[] { "id" };
            //    GridView1.DataBind();
            //}

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());


            DataSet DS1 = OBJ_METHOD.Get_DataSet("Acct_AmbulanceFuel", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
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
            // Validation
            if (dropambulanceno.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Ambulance No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropambulanceno.Focus();
                return;
            }
            else if (txtfuelprice.Text == "" || txtfuelprice.Text == "0" || (Convert.ToDecimal(txtfuelprice.Text) <= 0))
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtfuelprice.Focus();
                return;
            }
            else if (txtfuelconp.Text == "" || txtfuelconp.Text == "0" || (Convert.ToDecimal(txtfuelconp.Text) <= 0))
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtfuelconp.Focus();
                return;
            }

           
            // date split format
            string[] strmfd = txtdate.Text.Split('-');

            DateTime mfd = new DateTime(Convert.ToInt32(strmfd[2]), Convert.ToInt32(strmfd[1]), Convert.ToInt32(strmfd[0]));

            SqlParameter[] SQL_PARAMS = new SqlParameter[9];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@AmbulanceNo", SqlDbType.VarChar, 500, dropambulanceno.SelectedValue.ToString());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@FuelissuePrice", SqlDbType.Decimal, 0, txtfuelprice.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@FuelConsumption", SqlDbType.Decimal, 0, txtfuelconp.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@ADate", SqlDbType.DateTime, 0, mfd);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 100, lbluid.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("Acct_AmbulanceFuel", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
        txtfuelprice.Text = "";
        txtfuelconp.Text = "";
        txtdate.Text = "";
        dropambulanceno.SelectedIndex = 0;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            // Validation
            if (dropambulanceno.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Ambulance No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropambulanceno.Focus();
                return;
            }
            else if (txtfuelprice.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtfuelprice.Focus();
                return;
            }
            else if (txtfuelconp.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtfuelconp.Focus();
                return;
            }
           
            // date split format
            string[] strmfd = txtdate.Text.Split('-');

            DateTime mfd = new DateTime(Convert.ToInt32(strmfd[2]), Convert.ToInt32(strmfd[1]), Convert.ToInt32(strmfd[0]));


            SqlParameter[] SQL_PARAMS = new SqlParameter[7];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@AmbulanceNo", SqlDbType.VarChar, 500, dropambulanceno.SelectedValue.ToString());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@id", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@FuelissuePrice", SqlDbType.Decimal, 0, txtfuelprice.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@FuelConsumption", SqlDbType.Decimal, 0, txtfuelconp.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@ADate", SqlDbType.DateTime, 0, mfd);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 100, lbluid.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

            OBJ_METHOD.ExecuteProceedure("Acct_AmbulanceFuel", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                btnupdate.Visible = false;
                btncreate.Visible = true;
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

        // DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT id,ExpType,Amount,Edate from tblDailyExpense where Branch_ID='" + Session["Branch"].ToString() + "' order by id desc", false, false);
        SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

        SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
        SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());

        DataSet DS1 = OBJ_METHOD.Get_DataSet("Sp_AmbulanceFuel_sel", false, true, SQL_PARAMS1);
        if (DS1.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = DS1;
            GridView1.DataBind();
        }
        dt = DS1.Tables[0];
        return dt;

    }
    
}