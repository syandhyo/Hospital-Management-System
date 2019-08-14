using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class ADMIN_Admin_BedCharges : System.Web.UI.Page
{
    string num1 = "SJ0";
    string PRIFIX;
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
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
        Txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
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
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("SELECT [id] AS ID,DeptName FROM tblDepartment WHERE Branch_ID='" + Session["Branch"].ToString() + "' ORDER BY ID DESC", false, false);
            
           
            dropdept.DataSource = Ds1;
            dropdept.DataTextField = "DeptName";
            dropdept.DataValueField = "ID";
            dropdept.DataBind();
            dropdept.Items.Insert(0, new ListItem("Please Select", "0"));
           


            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }

            DataSet Ds = OBJ_METHOD.Get_DataSet("select A.ID,A.PRICE,A.DATE,B.NAME from [dbo].[Ward_Price] As A,WARD_TABLE AS B WHERE A.Branch_ID= " + Session["Branch"] + " and B.ID=A.[WARDID] order by A.ID desc", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                gridwardprice.DataSource = Ds;
                gridwardprice.DataKeyNames = new string[] { "ID" };
                gridwardprice.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message1 = "";
        try
        {
            if (dropdept.SelectedIndex == 0)
            {
                string message = "alert('* select Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropdept.Focus();
                return;
            }
            if (dropward.SelectedIndex == 0)
            {
                string message = "alert('* select Ward.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropward.Focus();
                return;
            }

            else if (Convert.ToDecimal(txtprice.Text) <= 0)
            {
                string message = "alert('* Enter Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtprice.Focus();
                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[6];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@WARDID", SqlDbType.Int, 0, dropward.SelectedValue);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, txtprice.Text);
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Txtdate.Text);
            SQL_PARAMS1[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS1[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

            OBJ_METHOD.ExecuteProceedure("WARD_PRICE_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS1, true);
            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
                binddata();
                clearcontrol();
            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message1 = "alert('Due To Some Issue Data Is Not Saved')";
            }
        }
        catch (Exception ex)
        {
            message1 = "alert('" + ex.Message + "')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = "";
        try
        {
            if (dropdept.SelectedIndex == 0)
            {
                string message = "alert('* select Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropdept.Focus();
                return;
            }
            if (dropward.SelectedIndex == 0)
            {
                string message = "alert('* select Ward.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropward.Focus();
                return;
            }

            else if (Convert.ToDecimal(txtprice.Text) <= 0)
            {
                string message = "alert('* Enter Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtprice.Focus();
                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[4];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@WARDID", SqlDbType.Int, 0, dropward.SelectedValue);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, txtprice.Text);
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, id.Text);


            OBJ_METHOD.ExecuteProceedure("WARD_PRICE_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS1, true);
            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
                binddata();
                clearcontrol();
            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message1 = "alert('Due To Some Issue Data Is Not Saved')";
            }
        }
        catch (Exception ex)
        {
            message1 = "alert('" + ex.Message + "')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/admin_bedchargecorporate.aspx");
    }
    protected void dropdept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT DISTINCT ID, NAME FROM WARD_TABLE WHERE Branch_ID = " + Session["Branch"] + " and DeptID=" + dropdept.SelectedValue + " ORDER BY NAME ASC", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {

                dropward.DataSource = Ds.Tables[0];
                dropward.DataTextField = "NAME";
                dropward.DataValueField = "ID";
                dropward.DataBind();
                dropward.Items.Insert(0, new ListItem("Please Select", "0")); //updated code
                dropward.SelectedIndex = 0;
            }
            else
            {
                string message = "alert('* No Ward Assign to this Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropdept.Focus();
                return;
            
            }
        }
        catch (Exception ex)
        {
        }
    }
    public void clearcontrol()
    {
        dropdept.SelectedIndex = 0;
        Txtdate.Text = "";
        binddata();
        txtprice.Text = "0";
        dropward.SelectedIndex = 0;
        Txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        btncreate.Visible = true;
        btnupdate.Visible = false;
       

    }
    protected void gridwardprice_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void gridwardprice_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gridwardprice_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gridwardprice_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Edit' and selected='False'", false, false);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                string message = "alert('* You Cant Edit.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
                var slno = gridwardprice.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
                DataSet Ds = OBJ_METHOD.Get_DataSet("select A.*,B.DeptID from Ward_Price As A,WARD_TABLE As B where B.ID=A.WARDID and A.ID='" + slno + "'", false, false);
                btncreate.Visible = false;
                btnupdate.Visible = true;
                btnupdate.Visible = true;
                id.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                dropdept.SelectedValue = Ds.Tables[0].Rows[0]["DeptID"].ToString();
                DataSet Ds1 = OBJ_METHOD.Get_DataSet("SELECT DISTINCT ID, NAME FROM WARD_TABLE WHERE Branch_ID = " + Session["Branch"] + " and DeptID=" + dropdept.SelectedValue + " ORDER BY NAME ASC", false, false);
                if (Ds1.Tables[0].Rows.Count > 0)
                {

                    dropward.DataSource = Ds1.Tables[0];
                    dropward.DataTextField = "NAME";
                    dropward.DataValueField = "ID";
                    dropward.DataBind();
                    dropward.Items.Insert(0, new ListItem("Please Select", "0")); //updated code
                    dropward.SelectedIndex = 0;
                }
                dropward.SelectedValue = Ds.Tables[0].Rows[0]["WARDID"].ToString();
                Txtdate.Text = Convert.ToDateTime(Ds.Tables[0].Rows[0]["DATE"]).ToString("dd-MM-yyyy");
                txtprice.Text = Ds.Tables[0].Rows[0]["PRICE"].ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void gridwardprice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet Ds = OBJ_METHOD.Get_DataSet("select A.ID,A.PRICE,A.DATE,B.NAME from [dbo].[Ward_Price] As A,WARD_TABLE AS B WHERE A.Branch_ID= " + Session["Branch"] + " and B.ID=A.[WARDID] order by A.ID desc", false, false);

        if (Ds.Tables[0].Rows.Count > 0)
        {
            gridwardprice.DataSource = Ds;
            gridwardprice.PageIndex = e.NewPageIndex;
            gridwardprice.DataKeyNames = new string[] { "ID" };
            gridwardprice.DataBind();
        }
    }
}