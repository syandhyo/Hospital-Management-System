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

public partial class ADMIN_admin_surgery_master : System.Web.UI.Page
{
    string num1 = "SJ000";
    DataSet ds = new DataSet();
    SqlCommand com;
    SqlDataReader dr;
    DataMathods OBJ_METHOD = new DataMathods();
    public void binddata()
    {
        try
        {

            DataSet DS = OBJ_METHOD.Get_DataSet("SELECT A.ID AS ID,a.Name AS NAME,B.NAME AS OT,C.SurgeryType AS TYPE from OT_SURGERY_MST AS A,OT_MASTER AS B,tblSurgeryType AS C WHERE B.ID=A.OT_MSTID AND C.id=A.SUR_TYPE_ID AND A.Branch_ID='" + Session["Branch"] + "'", false, false);
            if (DS.Tables[0].Rows.Count > 0)
            {
                gridstore.DataSource = DS;
                gridstore.DataKeyNames = new string[] { "ID" };
                gridstore.DataBind();
            }
            DataSet Ds = OBJ_METHOD.Get_DataSet("select id,SurgeryType from tblSurgeryType WHERE Branch_ID = " + Session["Branch"] + "", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                droptype.DataSource = Ds;
                droptype.DataTextField = "SurgeryType";
                droptype.DataValueField = "id";
                droptype.DataBind();
                droptype.Items.Insert(0, new ListItem("Please Select", "0"));
            }

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select ID,NAME from OT_MASTER WHERE Branch_ID = " + Session["Branch"] + "", false, false);

            if (Ds1.Tables[0].Rows.Count > 0)
            {
                dropot.DataSource = Ds1;
                dropot.DataTextField = "NAME";
                dropot.DataValueField = "ID";
                dropot.DataBind();
                dropot.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {

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

        lblorgid.Text = Session["ORGID"].ToString();
        lblid.Text = Session["NAME"].ToString();
        if (!IsPostBack)
        {
            Session["SortedView"] = null;
            binddata();
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message = string.Empty;
        OBJ_METHOD = new DataMathods();
        try
        {
            // Validation
            if (txtname.Text == "")
            {
                string message1 = "alert('* Please Enter Surgery Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                txtname.Focus();
                return;
            }
            if (droptype.SelectedIndex == 0)
            {
                string message1 = "alert('* Please Select Type.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                droptype.Focus();
                return;
            }
            if (txtdur.Text == "")
            {
                string message1 = "alert('* Please Enter Duration.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                txtdur.Focus();
                return;
            }
            if (dropot.SelectedIndex == 0)
            {
                string message1 = "alert('* Please Select Ot Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                droptype.Focus();
                return;
            }

            SqlParameter[] SQL_PARAMS = new SqlParameter[7];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@Name", SqlDbType.VarChar, 500, txtname.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@SUR_TYPE_ID", SqlDbType.Int, 0, droptype.SelectedValue);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, txtid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@OT_MSTID", SqlDbType.Int, 0, dropot.SelectedValue);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Duration", SqlDbType.VarChar, 50, txtdur.Text);

            OBJ_METHOD.ExecuteProceedure("OT_SRGERY_MASTER", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
            }
            message = "alert('" + OBJ_METHOD._objOut + "')";
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            clearcontrol();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/admin_surgery_master.aspx");
    }
    protected void gridstore_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void gridstore_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
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
                var slno = gridstore.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
                DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT * from OT_SURGERY_MST where id='" + slno + "'", false, false);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                    txtname.Text = Ds.Tables[0].Rows[0]["Name"].ToString();
                    txtdur.Text = Ds.Tables[0].Rows[0]["OT_MSTID"].ToString();
                    dropot.SelectedValue = Ds.Tables[0].Rows[0]["OT_MSTID"].ToString();
                    droptype.SelectedValue = Ds.Tables[0].Rows[0]["SUR_TYPE_ID"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void gridstore_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message = string.Empty;
        OBJ_METHOD = new DataMathods();
        try
        {
            // Validation
            if (txtname.Text == "")
            {
                string message1 = "alert('* Please Enter Surgery Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                txtname.Focus();
                return;
            }
            if (droptype.SelectedIndex == 0)
            {
                string message1 = "alert('* Please Select Type.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                droptype.Focus();
                return;
            }
            if (txtdur.Text == "")
            {
                string message1 = "alert('* Please Enter Duration.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                txtdur.Focus();
                return;
            }
            if (dropot.SelectedIndex == 0)
            {
                string message1 = "alert('* Please Select Ot Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                droptype.Focus();
                return;
            }

            SqlParameter[] SQL_PARAMS = new SqlParameter[7];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@Name", SqlDbType.VarChar, 500, txtname.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@SUR_TYPE_ID", SqlDbType.Int, 0, droptype.SelectedValue);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@OT_MSTID", SqlDbType.Int, 0, dropot.SelectedValue);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Duration", SqlDbType.VarChar, 50, txtdur.Text);

            OBJ_METHOD.ExecuteProceedure("OT_SRGERY_MASTER", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
            }
            message = "alert('" + OBJ_METHOD._objOut + "')";
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            clearcontrol();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void clearcontrol()
    {
        txtdur.Text = "";
        txtid.Text = "";
        txtname.Text = "";
        dropot.SelectedIndex = 0;
        droptype.SelectedIndex = 0;
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
}