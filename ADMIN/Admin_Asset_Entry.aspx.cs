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

public partial class ADMIN_Admin_Asset_Entry : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    GridViewRow gr;

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
            //bindTGrid();
        }
        con.Close();
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
    }
    public void binddata()
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select Distinct(ASSET_NAME),ASSET_ID from [dbo].[ASSET_MST] where BRANCH_ID =" + Session["Branch"] + " ", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropaaset.DataSource = Ds;
                dropaaset.DataTextField = "ASSET_NAME";
                dropaaset.DataValueField = "ASSET_ID";
                dropaaset.DataBind();
                dropaaset.Items.Insert(0, new ListItem("Please Select", "0"));
            }

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select A.ID,B.ASSET_NAME,C.NAME,A.DATE from TEST_ASSET_ENTRY AS A,ASSET_MST AS B,TEST_COMPONENT_TABLE AS C WHERE B.ASSET_ID=A.Asset And C.slno=A.INV AND B.BRANCH_ID=A.BRANCH_ID AND C.Branch_ID=A.BRANCH_ID AND A.BRANCH_ID= " + Session["Branch"] + " order by A.ID desc", false, false);

            //SqlDataAdapter Adp = new SqlDataAdapter("select * from TEST_WISE_CONSUMPTION", con);
            //DataTable Dt = new DataTable();
            //Adp.Fill(Dt);
            gridasset.DataSource = Ds1;
            gridasset.DataKeyNames = new string[] { "ID" };
            gridasset.DataBind();
            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select INV,slno from TEST_COMPONENT_TABLE where Branch_ID= " + Session["Branch"] + "", false, false);

            if (Ds2.Tables[0].Rows.Count > 0)
            {
                dropinv.DataSource = Ds2;
                dropinv.DataTextField = "INV";
                dropinv.DataValueField = "slno";
                dropinv.DataBind();
                dropinv.Items.Insert(0, new ListItem("Please Select", "0"));
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void clearfield()
    {
        dropinv.SelectedIndex = 0;
        dropaaset.SelectedIndex = 0;
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
    protected void gridasset_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = gridasset.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();

            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from TEST_ASSET_ENTRY where ID=" + slno, false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                dropaaset.SelectedValue = Ds.Tables[0].Rows[0]["Asset"].ToString();
                dropinv.SelectedValue=Ds.Tables[0].Rows[0]["INV"].ToString();
                txtdate.Text= Convert.ToDateTime(Ds.Tables[0].Rows[0]["DATE"]).ToString("dd-MM-yyyy");

            }
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void gridasset_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet Ds1 = OBJ_METHOD.Get_DataSet("select A.ID,B.ASSET_NAME,C.NAME from TEST_ASSET_ENTRY AS A,ASSET_MST AS B,TEST_COMPONENT_TABLE AS C WHERE B.ASSET_ID=A.Asset And C.ID=A.INV AND B.BRANCH_ID=A.BRANCH_ID AND C.Branch_ID=A.BRANCH_ID AND A.BRANCH_ID= " + Session["Branch"] + " order by A.ID desc", false, false);

        //SqlDataAdapter Adp = new SqlDataAdapter("select * from TEST_WISE_CONSUMPTION", con);
        //DataTable Dt = new DataTable();
        //Adp.Fill(Dt);
        gridasset.DataSource = Ds1;
        gridasset.PageIndex = e.NewPageIndex;
        gridasset.DataKeyNames = new string[] { "ID" };
        gridasset.DataBind();
    }
    protected void gridasset_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (dropinv.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Investigation.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropinv.Focus();
                return;
            }
            if (dropaaset.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Asset.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropaaset.Focus();
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[6];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@INV", SqlDbType.Int, 0, dropinv.SelectedValue);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Asset", SqlDbType.VarChar, 500, dropaaset.SelectedValue);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Date", SqlDbType.Date, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));

            OBJ_METHOD.ExecuteProceedure("LAB_ASSET_ENTRY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                clearfield();
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
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (dropinv.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Investigation.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropinv.Focus();
                return;
            }
            if (dropaaset.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Asset.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropaaset.Focus();
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[7];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@INV", SqlDbType.Int, 0, dropinv.SelectedValue);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Asset", SqlDbType.VarChar, 500, dropaaset.SelectedValue);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Date", SqlDbType.Date, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, txtid.Text);

            OBJ_METHOD.ExecuteProceedure("LAB_ASSET_ENTRY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clearfield();
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
            
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/Admin_Asset_Entry.aspx");
    }
    protected void btnrate_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/admin_testnsme_prices.aspx");
    }
    protected void btnconsumption_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/admin_testwiseconsuption.aspx");
        
    }
}