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

public partial class ADMIN_admin_radiologyentry : System.Web.UI.Page
{
    string num1 = "PK000";
    SqlCommand com, cmd;
    SqlDataReader dr;
    string SS;
    DataMathods OBJ_METHOD = new DataMathods();
    
    public void auto()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            string qry1 = "select ID as ID from TBL_RADIOLOGYADM";

            com = new SqlCommand(qry1, con);
            dr = null;

            dr = com.ExecuteReader();

            while (dr.Read())
            {
                num1 = dr["ID"].ToString();
            }
            num1 = string.Format("RL{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
            txtid.Text = num1;

            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                Session["SortedView"] = null;
                binddata();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
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

            DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,NAME from RADIOLOGY_CATEGORY_TABLE where ORGID='" + lblorgid.Text + "' and Branch_ID = " + Session["Branch"] + " order by id desc", false, false);
            droptesttype.DataSource = Ds;
            droptesttype.DataTextField = "NAME";
            droptesttype.DataValueField = "ID";
            droptesttype.DataBind();
            droptesttype.Items.Insert(0, new ListItem("Please Select", "0"));

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select SLNO AS ID,RADIOLOGYNAME from  TBL_RADIOLOGYADM  where Branch_ID = " + Session["Branch"] + " order by id desc", false, false);
            grdradiology.DataSource = Ds1;
            grdradiology.DataKeyNames = new string[] { "ID" };
            grdradiology.DataBind();
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
           DataSet Ds = OBJ_METHOD.Get_DataSet("select b.slno,b.ID,b.INV,b.PRICE from RADIOLOGY_CATEGORY_TABLE a,RADIOLOGY_COMPONENT_TABLE b where a.ID=b.ID and a.NAME='" + droptesttype.SelectedItem.Text + "' and b.CID is NULL and a.Branch_ID=" + Session["Branch"] + " order by id desc", false, false);
            
            GridView1.DataSource = Ds;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void clearcontrol()
    {
        binddata();
        txtname.Text = "";
        droptesttype.SelectedIndex = 0;
        btncreate.Visible = true;
        btnupdate.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {

        string message1 = string.Empty;
        DataMathods OBJ_METHOD = new DataMathods();
        try
        {

            if (txtname.Text.Trim() == "")
            {
                string message = "alert('* Please Enter Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            int ischked = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {

                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[2].FindControl("chkRow") as CheckBox);

                    if (chkRow.Checked)
                    {
                        ischked++;
                        break;
                    }
                }
            }

            if (ischked == 0)
            {
                message1 = "alert('Please select Invesigation Prices')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                return;
            }

            else
            {

                auto();
                // package_item();
                try
                {
                    int chkedcounter = 0;
                    int correctinput = 0;
                    SqlParameter[] SQL_PARAMS = new SqlParameter[7];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                    //SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@RADIOLOGYNAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@TESTID", SqlDbType.Int, 0, droptesttype.SelectedValue);
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                    OBJ_METHOD.ExecuteProceedure("ADMIN_RADIOLOGY_PACKAGE_ENTRY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {

                        //OBJ_METHOD = new DataMathods();
                        int themasterId = Convert.ToInt32(OBJ_METHOD._objOut);
                        foreach (GridViewRow row in GridView1.Rows)
                        {

                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                Label invIt = (row.Cells[0].FindControl("lblinv") as Label);
                                TextBox priceIt = (row.Cells[1].FindControl("lblprice") as TextBox);
                                CheckBox chkRow = (row.Cells[2].FindControl("chkRow") as CheckBox);


                                var invID = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

                                if (chkRow.Checked)
                                {
                                    chkedcounter++;
                                    SS = "TRUE";

                                    var INV = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0]);
                                    SQL_PARAMS = new SqlParameter[9];

                                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@INV", SqlDbType.VarChar, 500, invID);//invIt.Text
                                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, themasterId.ToString());
                                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@PRICE", SqlDbType.Decimal, 0, priceIt.Text);
                                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@CHKSEL", SqlDbType.VarChar, 500, chkRow.Checked);
                                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@TESTID", SqlDbType.Int, 0, droptesttype.SelectedValue);
                                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@RADIOLOGYNAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());

                                    OBJ_METHOD.ExecuteProceedure("ADMIN_Radiology_Entry", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);


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
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {

            
            //binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        //binddata();
        clearcontrol();
        
    }
    protected void grdradiology_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[2].Controls[0];

                db.OnClientClick = "return confirm('Are you sure want to delete this info ?');";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grdradiology_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
           
            string slno = grdradiology.DataKeys[e.RowIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);
            

            OBJ_METHOD.ExecuteProceedure("ADMIN_RADIOLOGY_PACKAGE_ENTRY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
    protected void grdradiology_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = grdradiology.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from TBL_RADIOLGADMITEM where ID='" + slno + "'", false, false);
            btncreate.Visible = false;
            btnupdate.Visible = true;
            txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
            //GridView1.DataSource = Ds;
            //GridView1.DataBind();
            //GridView1.Visible = false;

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select SLNO AS ID,TESTID,RADIOLOGYNAME from TBL_RADIOLOGYADM where SLNO='" + slno + "'", false, false);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                droptesttype.SelectedValue = Ds2.Tables[0].Rows[0]["TESTID"].ToString();
                txtname.Text = Ds2.Tables[0].Rows[0]["RADIOLOGYNAME"].ToString();

               // comboBox1_SelectedIndexChanged(comboBox1, new EventArgs());
                droptesttype_SelectedIndexChanged(sender, new EventArgs());
                foreach (GridViewRow row in GridView1.Rows)
                {

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[2].FindControl("chkRow") as CheckBox);

                        if (chkRow.Checked)
                        {
                            //ischked++;
                            //break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    
    protected void grdradiology_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet Ds1 = OBJ_METHOD.Get_DataSet("select SLNO AS ID,RADIOLOGYNAME from  TBL_RADIOLOGYADM  where Branch_ID = " + Session["Branch"] + " order by id desc", false, false);
        //SqlDataAdapter da1 = new SqlDataAdapter("select * from  TBL_RADIOLOGYADM ORDER BY ID DESC", con);
        //DataTable dt1 = new DataTable();
        //da1.Fill(dt1);
        grdradiology.DataSource = Ds1;
        grdradiology.PageIndex = e.NewPageIndex;
        grdradiology.DataKeyNames = new string[] { "ID" };
        grdradiology.DataBind();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            if (txtname.Text.Trim() == "")
            {
                string message = "alert('* Please Enter Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            if (droptesttype.SelectedIndex == 0)
            {
                string message = "alert('* Please select Test Type.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            int ischked = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {

                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[2].FindControl("chkRow") as CheckBox);

                    if (chkRow.Checked)
                    {
                        ischked++;
                        break;
                    }
                }
            }

            if (ischked == 0)
            {
                message1 = "alert('Please select Invesigation Prices')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                return;
            }
            else
            {
                try
                {
                    int chkedcounter = 0;
                    int correctinput = 0;
                    chkedcounter++;
                    SS = "FALSE";
                    SqlParameter[] SQL_PARAMS2 = new SqlParameter[2];

                    SQL_PARAMS2[0] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                    SQL_PARAMS2[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

                    OBJ_METHOD.ExecuteProceedure("ADMIN_Radiology_Entry", "", "", SqlDbType.VarChar, SQL_PARAMS2, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("commit");
                    }
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        OBJ_METHOD = new DataMathods();
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            //Label IdIt = (row.Cells[0].FindControl("lblid") as Label);
                            Label invIt = (row.Cells[0].FindControl("lblinv") as Label);
                            TextBox priceIt = (row.Cells[1].FindControl("lblprice") as TextBox);
                            CheckBox chkRow = (row.Cells[2].FindControl("chkRow") as CheckBox);

                            if (chkRow.Checked)
                            {
                                SS = "TRUE";
                                //if (Inv.Text == "WIDAL AGGULUTINATION TEST")
                                //{

                                var INV = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0]);
                                SqlParameter[] SQL_PARAMS = new SqlParameter[8];

                                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT1");
                                SQL_PARAMS[1] = OBJ_METHOD.createParams("@INV", SqlDbType.VarChar, 500, invIt.Text);
                                SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
                                SQL_PARAMS[3] = OBJ_METHOD.createParams("@PRICE", SqlDbType.VarChar, 500, priceIt.Text.ToString());
                                SQL_PARAMS[4] = OBJ_METHOD.createParams("@CHKSEL", SqlDbType.VarChar, 500, chkRow.Checked);
                                SQL_PARAMS[5] = OBJ_METHOD.createParams("@TESTID", SqlDbType.Int, 0, droptesttype.SelectedValue);
                                SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                                SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));


                                OBJ_METHOD.ExecuteProceedure("ADMIN_Radiology_Entry", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                                if (OBJ_METHOD._RESULT > 0)
                                {
                                    correctinput++;
                                    SQL_PARAMS = new SqlParameter[8];

                                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "Update");
                                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@SLNO", SqlDbType.VarChar, 500, txtid.Text);
                                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
                                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@RADIOLOGYNAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
                                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@TESTID", SqlDbType.Int, 0, droptesttype.SelectedValue);


                                    OBJ_METHOD.ExecuteProceedure("ADMIN_RADIOLOGY_PACKAGE_ENTRY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
                                    if (OBJ_METHOD._RESULT > 0)
                                    {
                                        if (chkedcounter == correctinput && chkedcounter > 0)
                                        {

                                            OBJ_METHOD.commitOrRollbackTran("commit");
                                            message1 = "alert('" + OBJ_METHOD._objOut + "')";

                                        }
                                        else
                                        {
                                            OBJ_METHOD.commitOrRollbackTran("rollback");
                                            message1 = "alert('Error occurred while processing data... Rolling back...')";
                                        }
                                    }

                                }
                            }
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Error occurred while processing data... Rolling back...')";
                }
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, data not updated.')";
        }
        finally
        {

            clearcontrol();
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
    }
    public DataTable bind()
    {
        DataTable dt = new DataTable();
        DataSet Ds = OBJ_METHOD.Get_DataSet("select SLNO AS ID,RADIOLOGYNAME from TBL_RADIOLOGYADM  where Branch_ID = " + Session["Branch"] + " order by id desc", false, false);

        dt = Ds.Tables[0];
        return dt;
    }
    protected void grdradiology_Sorting(object sender, GridViewSortEventArgs e)
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
        grdradiology.DataSource = sortedView;
        grdradiology.DataBind();
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