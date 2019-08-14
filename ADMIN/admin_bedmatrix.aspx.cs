using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class ADMIN_admin_bedmatrix : System.Web.UI.Page
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
        // lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy");
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
        }
        catch (Exception ex)
        {
            
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[3].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this info ?');";
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropdept.SelectedIndex==0)
            {
                string message = "alert('* Select Department.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropdept.Focus();
                return;
            }
            string str = "SELECT bmt.ID, wt.NAME,bmt.BEDNO,bmt.STATUS FROM BED_MATRIX_TABLE bmt join ward_table wt on bmt.name=wt.id WHERE bmt.NAME='" + dropward.SelectedValue + "' and wt.DeptID="+dropdept.SelectedValue+" ORDER BY ID ASC";
            DataTable dt = OBJ_METHOD.Get_DataSet(str).Tables[0];
            GridView1.SelectedIndex = 0;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            try
            {
                string avlstr = "SELECT COUNT(ISNULL(BEDNO,0)) AS TOTALAVBED FROM BED_MATRIX_TABLE WHERE STATUS='AVAILABLE' AND NAME='" + dropward.SelectedValue + "' GROUP BY NAME";
                DataSet ds = OBJ_METHOD.Get_DataSet(avlstr);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblavailablebed.Text = ds.Tables[0].Rows[0]["TOTALAVBED"].ToString();
                }
                string strocc = "SELECT COUNT(ISNULL(BEDNO,0)) AS TOTALOCBED FROM BED_MATRIX_TABLE WHERE STATUS='OCCUPIED' AND NAME='" + dropward.SelectedValue + "' GROUP BY NAME";
                 ds = OBJ_METHOD.Get_DataSet(strocc);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbloccupaidbed.Text = ds.Tables[0].Rows[0]["TOTALOCBED"].ToString();
                }
                
                
            }
            catch (Exception ex)
            {
                
            }
            
        }
        catch (Exception ex)
        {
            
        }
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message1 = "";
        try
        {
            

            string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();

            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETEBM");

            OBJ_METHOD.ExecuteProceedure("ADMIN_WARD_MASTER", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                bindgrid();
                
            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
        }
        catch (Exception ex)
        {
            
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

            string str = "SELECT bmt.ID, wt.NAME,bmt.BEDNO,bmt.STATUS FROM BED_MATRIX_TABLE bmt join ward_table wt on bmt.name=wt.id WHERE bmt.NAME='" + dropward.SelectedValue + "' and wt.DeptID=" + dropdept.SelectedValue + " ORDER BY ID ASC";
            DataSet ds= OBJ_METHOD.Get_DataSet(str);
            DataTable dt1 = new DataTable();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataBind();
                if (Session["SortedView"] != null)
                {

                    GridView1.DataSource = Session["SortedView"];
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
            }
            
        }
        catch (Exception ex)
        {
            
        }
    }
    protected void btnaddbed_Click(object sender, EventArgs e)
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
            if (txtbed.Text == "")
            {
                string message = "alert('* Enter No.of Bed.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtbed.Focus();
                return;
            }

            else if (Convert.ToDecimal(txtbed.Text) <= 0)
            {
                string message = "alert('* Enter No.of Bed.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtbed.Focus();
                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            string qry1 = "select PRIFIX,BEDNO from BED_MATRIX_TABLE WHERE NAME='" + dropward.Text + "' AND ORGID='" + lblorgid.Text + "'";
            DataSet ds = OBJ_METHOD.Get_DataSet(qry1);

            if (ds.Tables[0].Rows.Count > 0)
            {
                num1 = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["BEDNO"].ToString();
                PRIFIX = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["PRIFIX"].ToString();

                int cnt = 0;
                for (i = 1; i <= Convert.ToInt32(txtbed.Text); i++)
                {
                    num1 = string.Format(PRIFIX + "{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D1"));
                    string S = num1;


                    SqlParameter[] SQL_PARAMS1 = new SqlParameter[7];

                    SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                    SQL_PARAMS1[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                    SQL_PARAMS1[2] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, dropward.SelectedValue);
                    SQL_PARAMS1[3] = OBJ_METHOD.createParams("@PRIFIX", SqlDbType.VarChar, 500, PRIFIX);
                    SQL_PARAMS1[4] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 100, S);
                    SQL_PARAMS1[5] = OBJ_METHOD.createParams("@STATUS", SqlDbType.VarChar, 500, "AVAILABLE");
                    SQL_PARAMS1[6] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, "0");

                    OBJ_METHOD.ExecuteProceedure("ADMIN_BED_MATRIX", "", "", SqlDbType.VarChar, SQL_PARAMS1, true);
                    if (OBJ_METHOD._RESULT > 0)
                    {
                        cnt++;
                    }
                }
                if (cnt == Convert.ToInt32(txtbed.Text))
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    message1 = "alert('Data saved successfully.')";
                    bindgrid();
                }
                else
                {
                    message1 = "alert('Due to some issues, Data not saved.')";
                }
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

    protected void bindgrid()
    {
        try
        {
            string str = "SELECT bmt.ID, wt.NAME,bmt.BEDNO,STATUS FROM BED_MATRIX_TABLE bmt join ward_table wt on bmt.name=wt.id WHERE bmt.NAME='" + dropward.SelectedValue + "' ORDER BY ID ASC";
            DataTable dt = OBJ_METHOD.Get_DataSet(str).Tables[0];
            GridView1.SelectedIndex = 0;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
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
        string str = "SELECT bmt.ID, wt.NAME,bmt.BEDNO,STATUS FROM BED_MATRIX_TABLE bmt join ward_table wt on bmt.name=wt.id WHERE bmt.NAME='" + dropward.SelectedValue + "' ORDER BY ID ASC";
        DataTable dt = OBJ_METHOD.Get_DataSet(str).Tables[0];
        
        return dt;
    }
    protected void dropdept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT DISTINCT ID, NAME FROM WARD_TABLE WHERE Branch_ID = " + Session["Branch"] + " and  ORGID='" + lblorgid.Text + "' and DeptID="+dropdept.SelectedValue+" ORDER BY NAME ASC", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {

                dropward.DataSource = Ds.Tables[0];
                dropward.DataTextField = "NAME";
                dropward.DataValueField = "ID";
                dropward.DataBind();
                dropward.Items.Insert(0, new ListItem("Please Select", "0")); //updated code
                dropward.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
        }
    }
}