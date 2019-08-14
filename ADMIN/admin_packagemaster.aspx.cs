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

public partial class ADMIN_admin_packagemaster : System.Web.UI.Page
{
    string num1 = "PK000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
    decimal total_amt = 0;
    decimal total_vat_amt = 0;
    int i, no, no1, sl;
    GridViewRow gr;
    string SS;
    DataMathods OBJ_METHOD = new DataMathods();

    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID as ID from TBLPACKAGE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("PK{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;

        dr.Close();
        con.Close();
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
        // lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        lblid.Text = Session["NAME"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        if (!IsPostBack)
        {
            binddata();
            //  auto();
        }
    }
    public void binddata()
    {
        
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[3] { new DataColumn("NAME"), new DataColumn("INV"), new DataColumn("PRICE") });
        ViewState["ITEM"] = dt;
        this.BindGrid();
        

        DataSet Ds1 = OBJ_METHOD.Get_DataSet("select ID,NAME FROM TEST_CATEGORY_TABLE where ORGID='" + lblorgid.Text + "' and Branch_ID = " + Session["Branch"] + "", false, false);
        droptesttype.DataSource = Ds1;
        droptesttype.DataTextField = "NAME";
        droptesttype.DataValueField = "ID";
        droptesttype.DataBind();
        droptesttype.Items.Insert(0, new ListItem("Please Select", "0"));

        DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT * FROM TBLPACKAGE WHERE Branch_ID = " + Session["Branch"] + " ORDER BY ID DESC", false, false);
        if (Ds.Tables[0].Rows.Count > 0)
        {
            grdPackage.DataSource = Ds;
            grdPackage.DataBind();
        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        DataMathods OBJ_METHOD = new DataMathods();
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('* Please Enter Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            auto();
            try
            {
                int chkedcounter = 0;
                int correctinput = 0;
                SqlParameter[] SQL_PARAMS = new SqlParameter[7];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
                SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));


                OBJ_METHOD.ExecuteProceedure("ADMIN_LAB_PACKAGEENTRY", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {

                    //OBJ_METHOD = new DataMathods();
                    //int themasterId = Convert.ToInt32(OBJ_METHOD._objOut);
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            Label nameIt = (row.Cells[0].FindControl("lblname") as Label);
                            Label invIt = (row.Cells[1].FindControl("lblinv") as Label);
                            TextBox priceIt = (row.Cells[2].FindControl("lblprice") as TextBox);
                            CheckBox chkRow = (row.Cells[3].FindControl("chkRow") as CheckBox);

                            if (chkRow.Checked)
                            {

                                chkedcounter++;
                                SS = "TRUE";

                                var INV = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0]);
                                SQL_PARAMS = new SqlParameter[8];

                                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                                SQL_PARAMS[1] = OBJ_METHOD.createParams("@INV", SqlDbType.VarChar, 500, invIt.Text);//invIt.Text
                                SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                                SQL_PARAMS[3] = OBJ_METHOD.createParams("@INVID", SqlDbType.VarChar, 500, INV.ToString());
                                SQL_PARAMS[4] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, priceIt.Text.ToString());
                                SQL_PARAMS[5] = OBJ_METHOD.createParams("@CHKTYPE", SqlDbType.VarChar, 500, chkRow.Checked);
                                SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                                SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                                OBJ_METHOD.ExecuteProceedure("ADMIN_packageEntry", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);


                                if (OBJ_METHOD._RESULT > 0)
                                {
                                    correctinput++;
                                    // OBJ_METHOD.commitOrRollbackTran("commit");

                                }
                            }
                        }
                    }
                    if (chkedcounter == correctinput && chkedcounter > 0)
                    {

                        OBJ_METHOD.commitOrRollbackTran("commit");
                        message1 = "alert('" + OBJ_METHOD._objOut + "')";
                        clearcontrol();

                    }
                    else
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        message1 = "alert('Error occurred while processing data... Rolling back...')";
                    }
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");

                    message1 = "alert('" + OBJ_METHOD._objOut + "')";
                }

            }
            catch (Exception ex)
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message1 = "alert('Error occurred while processing data... Rolling back...')";
            }
            
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
    public void clearcontrol()
    {
        txtname.Text = "";
        droptesttype.SelectedIndex = 0;
        ViewState["ITEM"] = null;
        this.BindGrid();
        btncreate.Visible = true;
        btnupdate.Visible = false;
        binddata();
    }
    
    protected void BindGrid()
    {
        try
        {
            GridView1.DataSource = (DataTable)ViewState["ITEM"];
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void droptesttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
             DataTable dt = (DataTable)ViewState["ITEM"];
            //using (SqlCommand stock_cmd = new SqlCommand("ADMIN_Drop_packageEntry", con))
            //{
            //    stock_cmd.CommandType = CommandType.StoredProcedure;
            //    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DROP";
            //    stock_cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = droptesttype.SelectedValue.ToString();
            //    SqlDataAdapter da = new SqlDataAdapter(stock_cmd);
            //    da.Fill(dt);
            //    ViewState["ITEM"] = dt;
            //    this.BindGrid();
            //}
             SqlParameter[] SQL_PARAMS = new SqlParameter[3];

             SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
             SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, droptesttype.SelectedValue.ToString());
             SQL_PARAMS[2] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DROP");

             DataSet DS = OBJ_METHOD.Get_DataSet("ADMIN_Drop_packageEntry", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                ViewState["ITEM"] = DS;
                  this.BindGrid();
            }

            
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void grdPackage_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = grdPackage.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();

            DataSet DS1 = OBJ_METHOD.Get_DataSet("select a.ID,a.PACKGNAME,b.SELECTYPE,b.TESTNAME,b.INV,b.PRICE,b.CHKTYPE from  TBLPACKAGE a,TBLPACKGITEM b where a.ID=b.ID and a.ID='" + slno + "'", false,false);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                txtname.Text = DS1.Tables[0].Rows[0]["PACKGNAME"].ToString();
                droptesttype.Text = DS1.Tables[0].Rows[0]["SELECTYPE"].ToString();
            }
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    txtname.Text = dr["PACKGNAME"].ToString();
            //    droptesttype.Text = dr["SELECTYPE"].ToString();
            //}
           
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clearcontrol();
    }
    protected void grdPackage_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            string slno = grdPackage.DataKeys[e.RowIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);


            OBJ_METHOD.ExecuteProceedure("ADMIN_packageEntry", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
                binddata();
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
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
    }
    protected void grdPackage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[3].Controls[0];

                db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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
        DataTable dt = new DataTable();
        DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT * FROM TBLPACKAGE WHERE Branch_ID = " + Session["Branch"] + " ORDER BY ID DESC", false, false);
        dt = Ds.Tables[0];
        return dt;

    }
    protected void grdPackage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT * FROM TBLPACKAGE WHERE Branch_ID = " + Session["Branch"] + " ORDER BY ID DESC", false, false);
        //SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM TBLPACKAGE ORDER BY ID DESC", con);
        //DataTable dt1 = new DataTable();
        //da1.Fill(dt1);
        grdPackage.DataSource = Ds;
        grdPackage.PageIndex = e.NewPageIndex;
        grdPackage.DataBind();
    }
}