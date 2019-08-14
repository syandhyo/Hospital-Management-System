using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Web.Services;

public partial class ADMIN_admin_testnsme_prices : System.Web.UI.Page
{
    DataMathods OBJ_METHOD = new DataMathods();
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
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

        if (!IsPostBack)
        {
            binddata();
        }
        con.Close();
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
    }
    public void binddata()
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select slno,INV FROM TEST_COMPONENT_TABLE where CID IS NULL and Branch_ID = " + Session["Branch"] + " and STATUS='ACTIVE'", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropprocedure.DataSource = Ds;
                dropprocedure.DataTextField = "INV";
                dropprocedure.DataValueField = "slno";
                dropprocedure.DataBind();
                dropprocedure.Items.Insert(0, new ListItem("Please Select", "0"));
            }

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from TEST_COMPONENT_PRICE where Branch_ID = " + Session["Branch"] + " order by ID desc", false, false);

            
            GridView1.DataSource = Ds1;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (dropprocedure.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Procedure.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropprocedure.Focus();
                return;
            }
            OBJ_METHOD = new DataMathods();
            SqlParameter[] SQL_PARAMS = new SqlParameter[7];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@TESTNAME_ID", SqlDbType.VarChar, 500, lbltestid.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@INV_ID", SqlDbType.Int, 0, dropprocedure.SelectedValue);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@INV", SqlDbType.VarChar, 500, dropprocedure.SelectedItem.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Price", SqlDbType.Decimal, 0, txtprice.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Date", SqlDbType.Date, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));

            OBJ_METHOD.ExecuteProceedure("ADMIN_TESTNAME_PRICE", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
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
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
    }
    public void clearcontrol()
    {
        dropprocedure.SelectedIndex = 0;
        btncreate.Visible = true;
        txtprice.Text = "0";
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet Ds = OBJ_METHOD.Get_DataSet("select ID FROM TEST_COMPONENT_TABLE where CID IS NULL and Branch_ID = " + Session["Branch"] + " and INV='"+dropprocedure.SelectedItem.Text+"'", false, false);

        if (Ds.Tables[0].Rows.Count > 0)
        {
            lbltestid.Text=Ds.Tables[0].Rows[0]["ID"].ToString();
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
           

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from TEST_COMPONENT_PRICE where Branch_ID = " + Session["Branch"] + " order by ID desc", false, false);


            GridView1.DataSource = Ds1;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/admin_testnsme_prices.aspx");
    }
    protected void btnasset_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/Admin_Asset_Entry.aspx"); 
    }
    protected void btnconsumption_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/admin_testwiseconsuption.aspx");
    }
}