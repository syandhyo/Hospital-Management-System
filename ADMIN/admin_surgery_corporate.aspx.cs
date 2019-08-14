using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class ADMIN_admin_surgery_corporate : System.Web.UI.Page
{
    string num1 = "SJ000";
    DataMathods OBJ_METHOD = new DataMathods();
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;

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
        Txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        if (!IsPostBack)
        {
            Session["SortedView"] = null;
            binddata();
            bindgridview();
        }
    }
    public void binddata()
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT ID,Name FROM OT_SURGERY_MST WHERE Branch_ID='" + Session["Branch"].ToString() + "'  ORDER BY ID DESC", false, false);

            ddltestname.DataSource = Ds;
            ddltestname.DataTextField = "Name";
            ddltestname.DataValueField = "ID";
            ddltestname.DataBind();
            ddltestname.Items.Insert(0, new ListItem("Please Select", "0"));

            DataSet Dt = OBJ_METHOD.Get_DataSet("select ID,CNAME from Corporate_Table", false, false);
            if (Dt.Tables[0].Rows.Count > 0)
            {
                dropcorp.DataSource = Dt;
                dropcorp.DataTextField = "CNAME";
                dropcorp.DataValueField = "ID";
                dropcorp.DataBind();
                //dropcorp.Items.Insert(0, "Please Select");
                dropcorp.Items.Insert(0, new ListItem("Please Select", "0")); //updated code
            }

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
    public void bindgridview()
    {
        try
        {
            DataSet DS = OBJ_METHOD.Get_DataSet("select A.OTC_ID,B.CNAME AS CNAME,C.Name AS Name,A.PRICE AS PRICE,A.DATE AS DATE from OT_SURGERY_CORPRATE as A,Corporate_Table AS B,OT_SURGERY_MST AS C WHERE A.Branch_FY=B.Branch_ID AND A.Branch_ID=C.Branch_ID AND B.ID=A.CORP_ID AND A.Branch_ID='" + Session["Branch"].ToString() + "'", false, false);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = DS;
                GridView1.DataKeyNames = new string[] { "OTC_ID" };
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void clearcontrol()
    {
        dropcorp.SelectedIndex = 0;
        ddltestname.SelectedIndex = 0;
        txtprice.Text = "0";
        btncreate.Visible = true;
    }

    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message1 = "";
        try
        {
            if (dropcorp.SelectedIndex == 0)
            {
                string message = "alert('* select Corporate.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropcorp.Focus();
                return;
            }
            if (ddltestname.SelectedIndex == 0)
            {
                string message = "alert('* select Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                ddltestname.Focus();
                return;
            }

            else if (Convert.ToDecimal(txtcopro.Text) <= 0)
            {
                string message = "alert('* Enter Price.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcopro.Focus();
                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[7];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@SUG_ID", SqlDbType.Int, 0, ddltestname.SelectedValue);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, txtcopro.Text);
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Txtdate.Text);
            SQL_PARAMS1[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS1[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS1[6] = OBJ_METHOD.createParams("@CORP_ID", SqlDbType.Int, 0, dropcorp.SelectedValue);

            OBJ_METHOD.ExecuteProceedure("OT_CORPORATE_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS1, true);
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
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            string str = "select w.ID, w.Name,c.CNAME from OT_SURGERY_MST w join Corporate_Table c on c.ID=w.C_ID where w.Branch_ID='" + Session["Branch"].ToString() + "'";
            DataSet ds = OBJ_METHOD.Get_DataSet(str);

            DataTable dt = new DataTable();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }

            GridView1.DataSource = dt;
            GridView1.DataKeyNames = new string[] { "RADC_ID" };
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            if (Session["SortedView"] != null)
            {

                GridView1.DataSource = Session["SortedView"];
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void ddltestname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT Price FROM OT_SURGERY_RATE WHERE Branch_ID = " + Session["Branch"] + " and ID=" + ddltestname.SelectedValue + " and DATE=(select MAX(DATE) AS DATE from OT_SURGERY_RATE where Branch_ID =  " + Session["Branch"] + " and ID=" + ddltestname.SelectedValue + ") ", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {

                txtprice.Text = Ds.Tables[0].Rows[0]["Price"].ToString();
            }
            else
            {
                string message = "alert('* No Rate Available For this Test.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                ddltestname.Focus();
                return;

            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}