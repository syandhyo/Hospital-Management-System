using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ADMIN_admin_corporatecharges : System.Web.UI.Page
{
    string num1 = "PR000";
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    SqlCommand com;
    SqlDataReader dr;
    DataMathods OBJ_METHOD = new DataMathods();

    public void auto()
    {
        con.Open();
        string qry1 = "select ID from CORPORATE_I_CHARGE";
        com = new SqlCommand(qry1, con);
        dr = null;
        dr = com.ExecuteReader();
       while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("PR{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;
        dr.Close();
        con.Close();
       
    }
    public DataTable bind()
    {
        DataTable dt = new DataTable();
        DataSet Ds = OBJ_METHOD.Get_DataSet("select * from CORPORATE_I_CHARGE WHERE Branch_ID= " + Session["Branch"] + " order by SLNO desc", false, false);

        dt = Ds.Tables[0];
        return dt;
    }
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
        if (IsPostBack != true)
        {
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

            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from CORPORATE_I_CHARGE WHERE Branch_ID= " + Session["Branch"] + " order by SLNO desc", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = Ds;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
            }

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from Corporate_Table where Branch_ID= " + Session["Branch"] + " AND ISACTIVE='true'", false, false);
            ddlcorporate.DataSource = Ds1;
            ddlcorporate.DataTextField = "CNAME";
            ddlcorporate.DataValueField = "ID";
            ddlcorporate.DataBind();
            ddlcorporate.Items.Insert(0, new ListItem("Please Select", "0"));
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
            ErrorLog.Write(ex);
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        string message1 = string.Empty;
        try
        {
            if (ddlcorporate.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Corporate..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                ddlcorporate.Focus();
                return;
            }
            else if (Txtdate.Text == "")
            {
                string message = "alert('Please Select The Apply Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                Txtdate.Focus();
                return;
            }
            else
            {
                auto();
                string[] strdate = Txtdate.Text.Split('-');

                DateTime date = new DateTime(Convert.ToInt32(strdate[2]), Convert.ToInt32(strdate[1]), Convert.ToInt32(strdate[0]));

                foreach (GridViewRow row in GridView1.Rows)
                {
                    OBJ_METHOD = new DataMathods();
                    var cata = row.FindControl("lblcata") as Label;
                    var cname = row.FindControl("lblcharge") as Label;
                    var price = row.FindControl("lblprice") as Label;
                    var AMT = row.FindControl("txtprice") as TextBox;
                    if (AMT.Text == "")
                    {
                        AMT.Text = "0.00";
                    }
                    SqlParameter[] SQL_PARAMS = new SqlParameter[10];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@Charge", SqlDbType.VarChar, 500, cname.Text.ToString());
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@Price", SqlDbType.VarChar, 20, AMT.Text.ToString());
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@CorporateID", SqlDbType.VarChar, 20, ddlcorporate.SelectedValue);
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@Catagory", SqlDbType.VarChar, 20, cata.Text.ToString());
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@APPLY_DATE", SqlDbType.Date, 0, Txtdate.Text);

                    OBJ_METHOD.ExecuteProceedure("ADMIN_Corporate_Charge", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("commit");
                    }
                }
                OBJ_METHOD = new DataMathods();
                SqlParameter[] SQL_PARAMS1 = new SqlParameter[8];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text.ToString());
                SQL_PARAMS1[2] = OBJ_METHOD.createParams("@CName", SqlDbType.VarChar, 20, ddlcorporate.SelectedItem.Text);//IT IS NOT IN STORED PROCEDURE
                SQL_PARAMS1[3] = OBJ_METHOD.createParams("@Date", SqlDbType.Date, 0, DateTime.Now.Date);
                SQL_PARAMS1[4] = OBJ_METHOD.createParams("@ApplyDate", SqlDbType.Date, 0, date);
                SQL_PARAMS1[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
                SQL_PARAMS1[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
                SQL_PARAMS1[7] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

                OBJ_METHOD.ExecuteProceedure("ADMIN_COPRPORATE_ICHARGE", "", "@msg", SqlDbType.VarChar, SQL_PARAMS1, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                }
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

            clearcontrol();
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }

    }
    public void clearcontrol()
    {
        ddlcorporate.SelectedIndex = 0;
        Txtdate.Text = "";
        binddata();
        GridView3.DataSource = null;
        GridView3.DataBind();
        btndelete.Visible = false;
        btnupdate.Visible = false;
        btnSubmit.Visible = true;
        GridView1.DataSource = null;
        GridView1.DataBind();

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            if (Txtdate.Text == "")
            {
                string message = "alert('Please Select The Apply Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                Txtdate.Focus();
                return;
            }
            string[] strdate = Txtdate.Text.Split('-');

            DateTime date = new DateTime(Convert.ToInt32(strdate[2]), Convert.ToInt32(strdate[1]), Convert.ToInt32(strdate[0]));
            SqlParameter[] SQL_PARAMS2 = new SqlParameter[2];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

            OBJ_METHOD.ExecuteProceedure("ADMIN_Corporate_Charge", "", "", SqlDbType.VarChar, SQL_PARAMS2, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
            }
            foreach (GridViewRow row in GridView3.Rows)
            {
                OBJ_METHOD = new DataMathods();
                var cata = row.FindControl("lblcata1") as Label;
                var cname = row.FindControl("lblcharge1") as Label;
                var price = row.FindControl("lblprice1") as Label;
                var AMT = row.FindControl("txtprice1") as TextBox;
                if (AMT.Text == "")
                {
                    AMT.Text = "0";
                }
                SqlParameter[] SQL_PARAMS = new SqlParameter[10];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@Charge", SqlDbType.VarChar, 500, cname.Text.ToString());
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@Price", SqlDbType.VarChar, 20, AMT.Text.ToString());
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@CorporateID", SqlDbType.VarChar, 20, ddlcorporate.SelectedValue);
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@Catagory", SqlDbType.VarChar, 20, cata.Text.ToString());
                SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
                SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
                SQL_PARAMS[7] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                SQL_PARAMS[8] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                SQL_PARAMS[9] = OBJ_METHOD.createParams("@APPLY_DATE", SqlDbType.Date, 0, Txtdate.Text);

                OBJ_METHOD.ExecuteProceedure("ADMIN_Corporate_Charge", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                }
            }
            OBJ_METHOD = new DataMathods();
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[8];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text.ToString());
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@CName", SqlDbType.VarChar, 20, ddlcorporate.SelectedItem.Text);
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@Date", SqlDbType.Date, 0, DateTime.Now.ToString("yyyy-MM-dd"));
            SQL_PARAMS1[4] = OBJ_METHOD.createParams("@ApplyDate", SqlDbType.Date, 0, date);
            SQL_PARAMS1[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS1[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS1[7] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

            OBJ_METHOD.ExecuteProceedure("ADMIN_COPRPORATE_ICHARGE", "", "@msg", SqlDbType.VarChar, SQL_PARAMS1, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
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
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }

        
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clearcontrol();
        binddata();
    }
    protected void ddlcorporate_SelectedIndexChanged(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("Select * from tblChargeMaster where ID is NULL AND Branch_ID= " + Session["Branch"] + "", false, false);
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.DataSource = Ds;
            GridView1.DataKeyNames = new string[] { "SLNO" };
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from tblChargeMaster where ID='" + slno + "'", false, false);
            btnSubmit.Visible = false;
            btndelete.Visible = true;
            btnupdate.Visible = true;
            txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
            GridView3.DataSource = Ds;
            GridView3.DataBind();
            GridView1.Visible = false;

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select * from CORPORATE_I_CHARGE where ID='" + slno + "'", false, false);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                ddlcorporate.SelectedItem.Text = Ds2.Tables[0].Rows[0]["CName"].ToString();
                Txtdate.Text = Convert.ToDateTime(Ds2.Tables[0].Rows[0]["ApplyDate"]).ToString("dd-MM-yyyy");
            }

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from Corporate_Table where ISACTIVE='true' and CNAME='" + ddlcorporate.SelectedItem.Text + "' AND Branch_ID= " + Session["Branch"] + "", false, false);
            
            ddlcorporate.DataSource = Ds1;
            ddlcorporate.DataTextField = "CNAME";
            ddlcorporate.DataValueField = "ID";
            ddlcorporate.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);

            OBJ_METHOD.ExecuteProceedure("ADMIN_COPRPORATE_ICHARGE", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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

            clearcontrol();
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from CORPORATE_I_CHARGE WHERE Branch_ID= " + Session["Branch"] + " order by SLNO desc", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = Ds;
                GridView2.PageIndex = e.NewPageIndex;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
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
        DataView sortedView = new DataView(bind());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        Session["SortedView"] = sortedView;
        GridView2.DataSource = sortedView;
        GridView2.DataBind();
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