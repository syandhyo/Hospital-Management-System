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

public partial class ADMIN_admin_testname : System.Web.UI.Page
{
    string num1 = "SJ000";
    DataSet ds = new DataSet();
    SqlCommand com;
    SqlDataReader dr;
    DataMathods OBJ_METHOD = new DataMathods();

    [WebMethod]
    public void auto()
    {

        string qry1 = "select max(ID) AS ID from TEST_NAME_TABLE";

        DataSet ds = OBJ_METHOD.Get_DataSet(qry1);

        if (ds.Tables[0].Rows.Count > 0)
        {
            num1 = ds.Tables[0].Rows[0]["ID"].ToString();
        }
        num1 = string.Format("TS{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        TXTID.Text = num1;

    }
    public void binddata()
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 500, Session["Branch"]);

            DataSet DS = OBJ_METHOD.Get_DataSet("ADMIN_TestNAmE_PAGEINDX", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = DS;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
            }
            //using (SqlCommand cmd1 = new SqlCommand("ADMIN_TestNAmE_PAGEINDX", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    SqlDataAdapter da = new SqlDataAdapter(cmd1);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    GridView2.SelectedIndex = 0;
            //    GridView2.DataSource = dt;
            //    GridView2.DataKeyNames = new string[] { "ID" };
            //    GridView2.DataBind();
            //}
        }
        catch (Exception ex)
        {
            
        }

    }
    protected void BindGrid()
    {
        try
        {
            grvStudentDetails.DataSource = (DataTable)ViewState["ITEM"];
            grvStudentDetails.DataBind();

        }
        catch (Exception ex)
        {
            
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[2].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to Hide this item ?');";
        }
    }
    public void clearcontrol()
    {
        txtinv.Text = "";
        txtref.Text = "";
        dropunit.SelectedIndex = 0;
        txtname.Text = "";
        txtrefmin.Text = "";
        //ViewState["ITEM"] = null;
        DataTable dt = (DataTable)ViewState["ITEM"];
        dt.Clear();
        grvStudentDetails.DataSource = dt;
        grvStudentDetails.DataBind();

        GridView1.DataSource = null;
        GridView1.DataBind();
        btncreate.Visible = true;
        btnupdate.Visible = false;
        Dropcategory.SelectedIndex = 0;
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
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("INV"), new DataColumn("REF"), new DataColumn("REFMIN"), new DataColumn("UNIT"), new DataColumn("UNITID") });
            ViewState["ITEM"] = dt;
            this.BindGrid();
            binddata();

            DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,NAME FROM TEST_CATEGORY_TABLE where ORGID='" + lblorgid.Text + "' and Branch_ID = " + Session["Branch"] + " order by id desc", false, false);
            
            Dropcategory.DataSource = Ds;
            Dropcategory.DataTextField = "NAME";
            Dropcategory.DataValueField = "ID";
            Dropcategory.DataBind();
            //Dropcategory.Items.Insert(0, "Please Select");
            Dropcategory.Items.Insert(0, new ListItem("Please Select", "0")); //updated code

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select LAB_UNIT_ID,LAB_UNIT_NAME FROM Lab_Unit_Master where Branch_ID = " + Session["Branch"] + " order by LAB_UNIT_ID desc", false, false);

            dropunit.DataSource = Ds1;
            dropunit.DataTextField = "LAB_UNIT_NAME";
            dropunit.DataValueField = "LAB_UNIT_ID";
            dropunit.DataBind();
            //Dropcategory.Items.Insert(0, "Please Select");
            dropunit.Items.Insert(0, new ListItem("Please Select", "0"));
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (txtinv.Text.Trim() == "")
        {
            string message = "alert('Please enter The Investigation.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            txtinv.Focus();
            return;
        }

        else if (dropunit.SelectedIndex == 0)
        {
            dropunit.SelectedItem.Text = "";
            
            //dropunit.SelectedValue = "0";
        }
        
        
        DataTable dt = (DataTable)ViewState["ITEM"];
        dt.Rows.Add(txtinv.Text.ToUpper(), txtref.Text.Trim(), txtrefmin.Text.Trim(), dropunit.SelectedItem.Text,dropunit.SelectedValue);
        ViewState["ITEM"] = dt;
        this.BindGrid();
        txtinv.Text = txtref.Text = txtrefmin.Text = "";
        DataSet Ds1 = OBJ_METHOD.Get_DataSet("select LAB_UNIT_ID,LAB_UNIT_NAME FROM Lab_Unit_Master where Branch_ID = " + Session["Branch"] + " order by LAB_UNIT_ID desc", false, false);

        dropunit.DataSource = Ds1;
        dropunit.DataTextField = "LAB_UNIT_NAME";
        dropunit.DataValueField = "LAB_UNIT_ID";
        dropunit.DataBind();
        //Dropcategory.Items.Insert(0, "Please Select");
        dropunit.Items.Insert(0, new ListItem("Please Select", "0"));
        dropunit.SelectedIndex = 0;
    }
    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        DataTable dt = (DataTable)ViewState["ITEM"];
        GridViewRow row = (GridViewRow)grvStudentDetails.Rows[e.RowIndex];
        dt.Rows[index].Delete();
        ViewState["ITEM"] = dt;
        this.BindGrid();

    }
    protected void GVOnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lbl_slno") as Label).Text = (e.Row.RowIndex + 1).ToString();
        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 500, Session["Branch"]);

            DataSet DS = OBJ_METHOD.Get_DataSet("ADMIN_TestNAmE_PAGEINDX", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = DS;
                GridView2.PageIndex = e.NewPageIndex;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
        string message1 = string.Empty;
        try
        {
            OBJ_METHOD = new DataMathods();
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False'", false, false);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                string message = "alert('* No Permission To Hide.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
                OBJ_METHOD = new DataMathods();
                string slno = GridView2.DataKeys[e.RowIndex].Values["ID"].ToString();
                SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);


                OBJ_METHOD.ExecuteProceedure("ADMIN_TESTNAME", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {

                    OBJ_METHOD.commitOrRollbackTran("commit");
                    clearcontrol();
                }
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not deleted.')";
        }
        finally
        {
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select ID,CATEGORY,NAME from TEST_NAME_TABLE where ID='" + slno + "'", false, false);
            if (Ds2.Tables[0].Rows.Count > 0)
            
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                TXTID.Text = Ds2.Tables[0].Rows[0]["ID"].ToString();
                Dropcategory.SelectedValue = Ds2.Tables[0].Rows[0]["CATEGORY"].ToString();
                txtname.Text = Ds2.Tables[0].Rows[0]["NAME"].ToString();

            }

            DataSet Ds = OBJ_METHOD.Get_DataSet("select INV,REF,REFMIN,PRICE FROM TEST_COMPONENT_TABLE where ID='" + TXTID.Text + "' and ORGID='" + lblorgid.Text + "' and Branch_ID='" + Session["Branch"] + "'", false, false);

            grvStudentDetails.DataSource = Ds;
            grvStudentDetails.DataBind();

            DataTable dt = Ds.Tables["Table"];
            ViewState["ITEM"] = dt;

            
            GridView1.DataSource = Ds.Tables["Table"];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
       
            if (Dropcategory.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                Dropcategory.Focus();
                return;
            }
            else if (txtname.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (grvStudentDetails.Rows.Count <= 0)
            {
                string message = "alert('* Add item to purchase.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtinv.Focus();
                return;
            }
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[7];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, Dropcategory.SelectedValue.ToString());
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));


            OBJ_METHOD.ExecuteProceedure("ADMIN_TESTNAME", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
            int chkedcounter = 0;
            int correctinput = 0;
            if (OBJ_METHOD._RESULT > 0)
            {
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
                DataTable dt1 = ViewState["ITEM"] as DataTable;
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                foreach (GridViewRow gv1 in GridView1.Rows)
                {
                    chkedcounter++;
                    SQL_PARAMS = new SqlParameter[11];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);//invIt.Text
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@INV", SqlDbType.VarChar, 500, gv1.Cells[0].Text);
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@REF", SqlDbType.VarChar, 500, gv1.Cells[1].Text);
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, gv1.Cells[3].Text);
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@REFMIN", SqlDbType.VarChar, 500, gv1.Cells[2].Text);
                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
                    SQL_PARAMS[10] = OBJ_METHOD.createParams("@UNITID", SqlDbType.VarChar, 500, gv1.Cells[4].Text);
                    
                    OBJ_METHOD.ExecuteProceedure("ADMIN_LAB_TESTNAME", "", "", SqlDbType.VarChar, SQL_PARAMS, true);


                    if (OBJ_METHOD._RESULT > 0)
                    {
                        correctinput++;
                    }
                }
                if (chkedcounter == correctinput && chkedcounter > 0)
                {

                    OBJ_METHOD.commitOrRollbackTran("commit");
                    clearcontrol();
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Error occurred while processing data... Rolling back...')";
                }
            }
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            int chkedcounter = 0;
            int correctinput = 0;
            if (txtname.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (grvStudentDetails.Rows.Count <= 0)
            {
                string message = "alert('* Add item to purchase.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtinv.Focus();
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlParameter[] SQL_PARAMS = new SqlParameter[5];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, Dropcategory.SelectedValue.ToString());

            OBJ_METHOD.ExecuteProceedure("ADMIN_TESTNAME", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
                DataTable dt1 = ViewState["ITEM"] as DataTable;
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                foreach (GridViewRow gv1 in GridView1.Rows)
                {
                    chkedcounter++;
                    SQL_PARAMS = new SqlParameter[11];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);//invIt.Text
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, TXTID.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@INV", SqlDbType.VarChar, 500, gv1.Cells[0].Text);
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@REF", SqlDbType.VarChar, 500, gv1.Cells[1].Text);
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@UNIT", SqlDbType.VarChar, 500, gv1.Cells[3].Text);
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@REFMIN", SqlDbType.VarChar, 500, gv1.Cells[2].Text);
                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
                    SQL_PARAMS[10] = OBJ_METHOD.createParams("@UNITID", SqlDbType.VarChar, 500, gv1.Cells[4].Text);

                    OBJ_METHOD.ExecuteProceedure("ADMIN_LAB_TESTNAME", "", "", SqlDbType.VarChar, SQL_PARAMS, true);


                    if (OBJ_METHOD._RESULT > 0)
                    {
                        correctinput++;
                    }
                }
                if (chkedcounter == correctinput && chkedcounter > 0)
                {

                    OBJ_METHOD.commitOrRollbackTran("commit");

                    clearcontrol();
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Error occurred while processing data... Rolling back...')";
                } 

            }
            
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not upadated.')";
        }
        finally
        {
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        clearcontrol();
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
        DataView sortedView = new DataView(getdata());
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
    public DataTable getdata()
    {
        DataTable dt = new DataTable();
        SqlParameter[] SQL_PARAMS = new SqlParameter[3];

        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT");
        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
        SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 500, Session["Branch"]);

        DataSet DS = OBJ_METHOD.Get_DataSet("ADMIN_TestNAmE_PAGEINDX", false, true, SQL_PARAMS);
        
        dt = DS.Tables[0];
        return dt;
    }
}