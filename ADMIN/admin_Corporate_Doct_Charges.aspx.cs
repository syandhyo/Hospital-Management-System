using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ADMIN_admin_Corporate_Doct_Charges : System.Web.UI.Page
{
    string num1 = "PR000";
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    SqlCommand com, cmd, cmd1, cmd2;
    SqlDataReader dr;
    SqlDataAdapter da, da1;
    DataTable dt;
    DataMathods OBJ_METHOD = new DataMathods();

    public void auto()
    {
        con.Open();
        string qry1 = " select ID from CORPORATE_DOCTOR_CHARGE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("CD{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;


        dr.Close();
        con.Close();
    }
    public void binddata()
    {

        DataSet DS = OBJ_METHOD.Get_DataSet("select A.ID AS ID,B.CNAME,A.DATE,A.APPLY_DATE FROM CORPORATE_DOCTOR_CHARGE A,Corporate_Table B WHERE A.CORPORATE=B.ID AND A.Branch_ID = " + Session["Branch"] + " order by ID desc", false, false);
        if (DS.Tables[0].Rows.Count > 0)
        {
            GridView2.DataSource = DS;

            GridView2.DataKeyNames = new string[] { "ID" };
            GridView2.DataBind();
        }
            
        
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

            DataSet DS = OBJ_METHOD.Get_DataSet("select * from Corporate_Table where ISACTIVE='true' and Branch_ID = " + Session["Branch"] + " order by ID desc", false, false);
            if (DS.Tables[0].Rows.Count > 0)
            {
                ddlcorporate.DataSource = DS;
                ddlcorporate.DataTextField = "CNAME";
                ddlcorporate.DataValueField = "ID";
                ddlcorporate.DataBind();
                ddlcorporate.Items.Insert(0, new ListItem("Please Select" , "0"));
            }
            binddata();

        }

        
    }
    protected void ddlcorporate_SelectedIndexChanged(object sender, EventArgs e)
    {
        con.Open();
        if (ddlcorporate.SelectedIndex != 0)
        {
            DataSet DS = OBJ_METHOD.Get_DataSet("select * from Doctor_charges where CID is null and Branch_ID = " + Session["Branch"] + " order by ID desc", false, false);
            if (DS.Tables[0].Rows.Count > 0)
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.DataSource = DS;
            GridView1.DataBind();
        }
        con.Close();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        OBJ_METHOD = new DataMathods();
        try
        {
            if (ddlcorporate.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Corporate..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (Txtdate.Text == "")
            {
                string message = "alert('Please Select The Apply Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
                auto();
                string[] strdate = Txtdate.Text.Split('-');
                DateTime date = new DateTime(Convert.ToInt32(strdate[2]), Convert.ToInt32(strdate[1]), Convert.ToInt32(strdate[0]));
                try
                {
                    int chkedcounter = 0;
                    int correctinput = 0;
                    SqlParameter[] SQL_PARAMS = new SqlParameter[8];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE_ID", SqlDbType.VarChar, 500, ddlcorporate.SelectedValue);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text.ToString());
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, DateTime.Now.ToString("yyyy-MM-dd"));
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@APPLY_DATE", SqlDbType.Date, 0, date);
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);


                    OBJ_METHOD.ExecuteProceedure("ADMIN_corpo_charge_doctor", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            var id = row.FindControl("lblid") as Label;
                            var doctor = row.FindControl("lbldoctor") as Label;
                            var follow_up = row.FindControl("lblfollow") as Label;
                            var AMT = row.FindControl("txtNchrage") as TextBox;
                            if (AMT.Text == "")
                            {
                                AMT.Text = "0.00";
                            }

                            chkedcounter++;
                            SQL_PARAMS = new SqlParameter[10];

                            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERTGRID");
                            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DOCTOR", SqlDbType.VarChar, 500, doctor.Text.ToString());//invIt.Text
                            SQL_PARAMS[2] = OBJ_METHOD.createParams("@idt", SqlDbType.VarChar, 500, id.Text.ToString());
                            SQL_PARAMS[3] = OBJ_METHOD.createParams("@FOLLOW_UP", SqlDbType.VarChar, 500, follow_up.Text.ToString());
                            SQL_PARAMS[4] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, AMT.Text.ToString());
                            SQL_PARAMS[5] = OBJ_METHOD.createParams("@CID", SqlDbType.VarChar, 500, txtid.Text);
                            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                            SQL_PARAMS[8] = OBJ_METHOD.createParams("@CORPORATE_ID", SqlDbType.VarChar, 500, ddlcorporate.SelectedValue);
                            SQL_PARAMS[9] = OBJ_METHOD.createParams("@APPLY_DATE", SqlDbType.Date, 0, date);

                            OBJ_METHOD.ExecuteProceedure("ADMIN_corpo_charge_doctor_Grid", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);


                            if (OBJ_METHOD._RESULT > 0)
                            {
                                correctinput++;
                            }
                                
                        }
                        if (chkedcounter == correctinput && chkedcounter > 0)
                        {

                            OBJ_METHOD.commitOrRollbackTran("commit");
                            message1 = "alert('" + OBJ_METHOD._objOut + "')";
                            clearcontrol();
                            binddata();

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
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        OBJ_METHOD = new DataMathods();
        try
        {
            if (Txtdate.Text == "")
            {
                string message = "alert('Please Select The Apply Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            try
            {
                string[] strdate = Txtdate.Text.Split('-');
                DateTime date = new DateTime(Convert.ToInt32(strdate[2]), Convert.ToInt32(strdate[1]), Convert.ToInt32(strdate[0]));
                int chkedcounter = 0;
                int correctinput = 0;
                SqlParameter[] SQL_PARAMS = new SqlParameter[5];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE_ID", SqlDbType.VarChar, 500, ddlcorporate.SelectedValue);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text.ToString());
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, DateTime.Now.Date);
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@APPLY_DATE", SqlDbType.Date, 0, date);

                OBJ_METHOD.ExecuteProceedure("ADMIN_corpo_charge_doctor", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    SQL_PARAMS = new SqlParameter[2];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE1");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);

                    OBJ_METHOD.ExecuteProceedure("ADMIN_corpo_charge_doctor", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        foreach (GridViewRow row in GridView3.Rows)
                        {
                            var id = row.FindControl("lblid1") as Label;
                            var doctor = row.FindControl("lbldoctor1") as Label;
                            var follow_up = row.FindControl("lblfollow1") as Label;
                            var AMT = row.FindControl("txtNchrage1") as TextBox;
                            if (AMT.Text == "")
                            {
                                AMT.Text = "0.00";
                            }

                            chkedcounter++;
                            SQL_PARAMS = new SqlParameter[10];

                            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATEGRID");
                            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DOCTOR", SqlDbType.VarChar, 500, doctor.Text.ToString());//invIt.Text
                            SQL_PARAMS[2] = OBJ_METHOD.createParams("@idt", SqlDbType.VarChar, 500, id.Text.ToString());
                            SQL_PARAMS[3] = OBJ_METHOD.createParams("@FOLLOW_UP", SqlDbType.VarChar, 500, follow_up.Text.ToString());
                            SQL_PARAMS[4] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, AMT.Text.ToString());
                            SQL_PARAMS[5] = OBJ_METHOD.createParams("@CID", SqlDbType.VarChar, 500, txtid.Text);
                            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                            SQL_PARAMS[8] = OBJ_METHOD.createParams("@CORPORATE_ID", SqlDbType.VarChar, 500, ddlcorporate.SelectedValue);
                            SQL_PARAMS[9] = OBJ_METHOD.createParams("@APPLY_DATE", SqlDbType.Date, 0, date);

                            OBJ_METHOD.ExecuteProceedure("ADMIN_corpo_charge_doctor_Grid", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);


                            if (OBJ_METHOD._RESULT > 0)
                            {
                                correctinput++;
                            }
                                
                        }
                        if (chkedcounter == correctinput && chkedcounter > 0)
                        {

                            OBJ_METHOD.commitOrRollbackTran("commit");
                            message1 = "alert('" + OBJ_METHOD._objOut + "')";
                            clearcontrol();
                            binddata();

                        }
                        else
                        {
                            OBJ_METHOD.commitOrRollbackTran("rollback");
                            message1 = "alert('Error occurred while processing data... Rolling back...')";
                        }
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
    protected void btndelete_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        OBJ_METHOD = new DataMathods();
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text.ToString());

            OBJ_METHOD.ExecuteProceedure("ADMIN_corpo_charge_doctor_Grid", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clearcontrol();
        binddata();
    }
    public void clearcontrol()
    {
        DataSet DS = OBJ_METHOD.Get_DataSet("select * from Corporate_Table where ISACTIVE='true' and Branch_ID = " + Session["Branch"] + " order by ID desc", false, false);
        if (DS.Tables[0].Rows.Count > 0)
        {
            ddlcorporate.DataSource = DS;
            ddlcorporate.DataTextField = "CNAME";
            ddlcorporate.DataValueField = "ID";
            ddlcorporate.DataBind();
            ddlcorporate.Items.Insert(0, new ListItem("Please Select", "0"));
        }
        Txtdate.Text = "";
        ddlcorporate.SelectedIndex = 0;
        btndelete.Visible = false;
        btnupdate.Visible = false;
        btnSubmit.Visible = true;
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView3.DataSource = null;
        GridView3.DataBind();
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            DataSet Ds3 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Edit' and selected='False'", false, false);
            if (Ds3.Tables[0].Rows.Count > 0)
            {
                string message = "alert('* You Cant Edit.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
                var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
                DataSet DS = OBJ_METHOD.Get_DataSet("select * from CORPORATE_DOCTOR_CHARGE where ID='" + slno + "'", false, false);
                if (DS.Tables[0].Rows.Count > 0)
                //SqlCommand C2m = new SqlCommand("select * from CORPORATE_DOCTOR_CHARGE where ID='" + slno + "'", con);
                //dr = C2m.ExecuteReader();
                //if (dr.Read() == true)
                {
                    ddlcorporate.SelectedValue = DS.Tables[0].Rows[0]["CORPORATE"].ToString();
                    Txtdate.Text = Convert.ToDateTime(DS.Tables[0].Rows[0]["APPLY_DATE"]).ToString("dd-MM-yyyy");
                }

                DataSet DS1 = OBJ_METHOD.Get_DataSet("select * from Doctor_charges where CID='" + slno + "' and APPLY_DATE='" + Convert.ToDateTime(Txtdate.Text).ToString("yyyy-MM-dd") + "'", false, false);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    btnSubmit.Visible = false;
                    btndelete.Visible = true;
                    btnupdate.Visible = true;
                    txtid.Text = DS1.Tables[0].Rows[0]["CID"].ToString();
                    GridView3.DataSource = DS1;
                    GridView3.DataBind();
                    GridView1.Visible = false;



                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet DS = OBJ_METHOD.Get_DataSet("select A.ID AS ID,B.CNAME,A.DATE,A.APPLY_DATE FROM CORPORATE_DOCTOR_CHARGE A,Corporate_Table B WHERE A.CORPORATE=B.ID AND A.Branch_ID = " + Session["Branch"] + " order by ID desc", false, false);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = DS;
                GridView2.PageIndex = e.NewPageIndex;
                GridView2.DataKeyNames = new string[] { "ID" };
                GridView2.DataBind();
            }

            con.Close();
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

        DataSet DS = OBJ_METHOD.Get_DataSet("select A.ID AS ID,B.CNAME,A.DATE,A.APPLY_DATE FROM CORPORATE_DOCTOR_CHARGE A,Corporate_Table B WHERE A.CORPORATE=B.ID AND A.Branch_ID = " + Session["Branch"] + " order by ID desc", false, false);

        dt = DS.Tables[0];
        return dt;
    }
}