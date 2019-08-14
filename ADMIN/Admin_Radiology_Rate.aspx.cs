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

public partial class ADMIN_Admin_Radiology_Rate : System.Web.UI.Page
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
            DataSet Ds = OBJ_METHOD.Get_DataSet("select slno,INV from RADIOLOGY_COMPONENT_TABLE where Branch_ID = " + Session["Branch"] + "", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropprocedure.DataSource = Ds;
                dropprocedure.DataTextField = "INV";
                dropprocedure.DataValueField = "slno";
                dropprocedure.DataBind();
                dropprocedure.Items.Insert(0, new ListItem("Please Select", "0"));
            }

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select B.ID AS ID,A.INV,B.DATE,B.Price from RADIOLOGY_COMPONENT_TABLE AS A,RADIOLOGY_PRICE_TABLE AS B WHERE A.slno=B.ID AND B.Branch_ID = " + Session["Branch"] + " order by B.ID desc", false, false);


            GridView1.DataSource = Ds1;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet Ds1 = OBJ_METHOD.Get_DataSet("select B.P_ID AS P_ID,A.INV,B.DATE,B.Price from RADIOLOGY_COMPONENT_TABLE AS A,RADIOLOGY_PRICE_TABLE AS B WHERE A.ID=B.P_ID AND B.Branch_ID = " + Session["Branch"] + " order by B.ID desc", false, false);


        GridView1.DataSource = Ds1;
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
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
            SqlParameter[] SQL_PARAMS = new SqlParameter[6];
            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, dropprocedure.SelectedValue);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Price", SqlDbType.Decimal, 0, txtprice.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Date", SqlDbType.Date, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));

            OBJ_METHOD.ExecuteProceedure("RADIOLOGY_MASTER_RATE", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/Admin_Radiology_Rate.aspx");
    }
}