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


public partial class ADMIN_admin_Branch_Master : System.Web.UI.Page
{
    DataMathods OBJ_METHOD = new DataMathods();
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
                clearcontrol();

                if (Convert.ToInt32(Session["USLNO"]) != 1)
                {
                    populateonload();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void populateonload()
    {
        try
        {

            var slno = Session["Branch"].ToString();

            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from BRANCH_MST where BRANCH_ID=" + slno, false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = Ds.Tables[0].Rows[0]["BRANCH_ID"].ToString();

                txtname.Text = Ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
                txtphone.Text = Ds.Tables[0].Rows[0]["BRANCH_PHONE"].ToString();
                txtgst.Text = Ds.Tables[0].Rows[0]["BRANCH_GSTNO"].ToString();
                txtstatecode.Text = Ds.Tables[0].Rows[0]["BRANCH_STATECODE"].ToString();
                txtaddress.Text = Ds.Tables[0].Rows[0]["BRANCH_ADRRES"].ToString();
                txtregdnumber.Text = Ds.Tables[0].Rows[0]["BRANCH_REGNO"].ToString();
                txtmailid.Text = Ds.Tables[0].Rows[0]["BRANCH_EMAIL"].ToString();
                txtmobilenumber.Text = Ds.Tables[0].Rows[0]["BRANCH_PHONE2"].ToString();
                txtbranchusercode.Text = Ds.Tables[0].Rows[0]["BRANCH_USER_CODE"].ToString();

                string chkusr = "select * from dbo.LOGIN_TABLE where USERNAME like '" + Ds.Tables[0].Rows[0]["BRANCH_USER_CODE"].ToString() + "-%'";
                DataSet Ds1 = OBJ_METHOD.Get_DataSet(chkusr, false, false);
                if (Ds1.Tables[0].Rows.Count > 0)
                {
                    txtbranchusercode.Enabled = false;
                }
                else
                {
                    txtbranchusercode.Enabled = true;
                }

            }
        }
        catch (Exception ex)
        {

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
            string sqlstr="";
            if(Convert.ToInt32(Session["USLNO"])==1){
             sqlstr="select BRANCH_ID,BRANCH_NAME,BRANCH_USER_CODE,BRANCH_PHONE,BRANCH_GSTNO,BRANCH_ADRRES,BRANCH_STATECODE,BRANCH_REGNO,BRANCH_EMAIL,BRANCH_PHONE2 from BRANCH_MST order by BRANCH_ID desc";
            }
            else{
                sqlstr="select BRANCH_ID,BRANCH_NAME,BRANCH_USER_CODE,BRANCH_PHONE,BRANCH_GSTNO,BRANCH_ADRRES,BRANCH_STATECODE,BRANCH_REGNO,BRANCH_EMAIL,BRANCH_PHONE2 from BRANCH_MST  WHERE Branch_ID = "+Session["Branch"]+" order by BRANCH_ID desc";
            }
            DataSet Ds = OBJ_METHOD.Get_DataSet(sqlstr, false, false);
            
            if (Ds.Tables[0].Rows.Count > 0)
            {
                grdvwbranch.DataSource = Ds;
                grdvwbranch.DataBind();
            }
        }
        catch (Exception ex)
        {
            
            ErrorLog.Write(ex);
        }

   
    }

    protected void btncreate_Click(object sender, EventArgs e)
    {

        DataMathods OBJ_METHOD = new DataMathods();
        string message = string.Empty;
        if (txtname.Text.Trim() == "")
        {
             message = "alert('* Please Enter Branch Name.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }
        if (txtbranchusercode.Text.Trim() == "")
        {
             message = "alert('* Please Enter Branch User Code.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }
        if (txtphone.Text.Trim() == "")
        {
             message = "alert('* Please Enter Phone No.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }
        if (txtgst.Text.Trim() == "")
        {
             message = "alert('* Please Enter GST')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }
        if (txtstatecode.Text.Trim() == "")
        {
             message = "alert('* Please Enter State Code.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }
        if (txtaddress.Text.Trim() == "")
        {
             message = "alert('* Please Enter Address.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }
        try
        {

            SqlParameter[] SQL_PARAMS = new SqlParameter[12];
            
            SQL_PARAMS[0] = OBJ_METHOD.createParams("@BRANCH_NAME", SqlDbType.VarChar, 500, txtname.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@BRANCH_PHONE", SqlDbType.VarChar, 500, txtphone.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@BRANCH_GSTNO", SqlDbType.VarChar, 500, txtgst.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@BRANCH_ADRRES", SqlDbType.VarChar, 5000, txtaddress.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@BRANCH_STATECODE", SqlDbType.VarChar, 500, txtstatecode.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@BRANCH_REGNO", SqlDbType.VarChar, 500,txtregdnumber );
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@BRANCH_EMAIL", SqlDbType.VarChar, 500, txtmailid.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@BRANCH_PHONE2", SqlDbType.VarChar, 500, txtmobilenumber.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@ORG_ID", SqlDbType.VarChar, 500, 1);//default as its for 1 org and n branch
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@CREATED_BY", SqlDbType.VarChar, 500, Session["USLNO"]);//user session value
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@BRANCH_USER_CODE", SqlDbType.VarChar, 5, txtbranchusercode.Text);

            OBJ_METHOD.ExecuteProceedure("USP_BRANCH_MASTER", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    binddata();
                    
                    message = "alert('" + OBJ_METHOD._objOut + "')";
            }
        }
        catch (Exception ex)
        {

            OBJ_METHOD.commitOrRollbackTran("rollback");
            message = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {

            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message = string.Empty;
        try
        {
            if (txtname.Text.Trim() == "")
            {
                 message = "alert('* Please Enter Branch Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            if (txtbranchusercode.Text.Trim() == "")
            {
                message = "alert('* Please Enter Branch User Code.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            if (txtphone.Text.Trim() == "")
            {
                 message = "alert('* Please Enter Phone No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            if (txtgst.Text.Trim() == "")
            {
                 message = "alert('* Please Enter GST')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            if (txtstatecode.Text.Trim() == "")
            {
                 message = "alert('* Please Enter State Code.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            if (txtaddress.Text.Trim() == "")
            {
                 message = "alert('* Please Enter Address.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            
            SqlParameter[] SQL_PARAMS = new SqlParameter[13];
            
            SQL_PARAMS[0] = OBJ_METHOD.createParams("@BRANCH_NAME", SqlDbType.VarChar, 500, txtname.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@BRANCH_PHONE", SqlDbType.VarChar, 500, txtphone.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@BRANCH_GSTNO", SqlDbType.VarChar, 500, txtgst.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@BRANCH_ADRRES", SqlDbType.VarChar, 5000, txtaddress.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@BRANCH_STATECODE", SqlDbType.VarChar, 500, txtstatecode.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@BRANCH_REGNO", SqlDbType.VarChar, 500, txtregdnumber.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@BRANCH_EMAIL", SqlDbType.VarChar, 500, txtmailid.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@BRANCH_PHONE2", SqlDbType.VarChar, 500, txtmobilenumber.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@ORG_ID", SqlDbType.VarChar, 500, 1);//default as its for 1 org and n branch
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@CREATED_BY", SqlDbType.VarChar, 500,Convert.ToString(Session["USLNO"]));//user session value
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@BRANCH_ID", SqlDbType.Int, 0, txtid.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@BRANCH_USER_CODE", SqlDbType.VarChar, 5, txtbranchusercode.Text);;

            OBJ_METHOD.ExecuteProceedure("USP_BRANCH_MASTER", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                populateonload();
                if (Convert.ToInt32(Session["USLNO"]) == 1)
                {
                    btncreate.Visible = true;
                    btnupdate.Visible = false;
                }
                else
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                }
            }
            message = "alert('" + OBJ_METHOD._objOut + "')";
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message = "alert('Due to some issues, Data not Updated.')";
        }
        finally
        {

            
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

        }
    
    }
    public DataTable getdata()
    {
            string sqlstr="";
            if(Convert.ToInt32(Session["USLNO"])==1){
             sqlstr="select BRANCH_ID,BRANCH_NAME,BRANCH_USER_CODE,BRANCH_PHONE,BRANCH_GSTNO,BRANCH_ADRRES,BRANCH_STATECODE,BRANCH_REGNO,BRANCH_EMAIL,BRANCH_PHONE2 from BRANCH_MST order by BRANCH_ID desc";
            }
            else{
                sqlstr="select BRANCH_ID,BRANCH_NAME,BRANCH_USER_CODE,BRANCH_PHONE,BRANCH_GSTNO,BRANCH_ADRRES,BRANCH_STATECODE,BRANCH_REGNO,BRANCH_EMAIL,BRANCH_PHONE2 from BRANCH_MST WHERE Branch_ID = "+Session["Branch"]+" order by BRANCH_ID desc";
            }
            DataSet Ds = OBJ_METHOD.Get_DataSet(sqlstr, false, false);
            DataTable dt = Ds.Tables[0];
            return dt;
        
    }
    protected void grdvwbranch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[6].Controls[0];
            if (Convert.ToInt32(Session["USLNO"]) == 1)
            {
                db.Visible = true;
                db.OnClientClick = "return confirm('Are you sure want to delete this info ?');";
            }
            else
            {
                db.Visible = false;
            }
        }
    }
    protected void grdvwbranch_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            string slno = grdvwbranch.DataKeys[e.RowIndex].Values["BRANCH_ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];
          
            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);
            //SQL_PARAMS[2] = OBJ_METHOD.createParams("@DeptName", SqlDbType.VarChar, 500, txtdept.Text);

            OBJ_METHOD.ExecuteProceedure("USP_BRANCH_MASTER", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
               
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
            clearcontrol();
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        
    }
    protected void grdvwbranch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            string sqlstr="";
            if(Convert.ToInt32(Session["USLNO"])==1)
            {
             sqlstr="select BRANCH_ID,BRANCH_NAME,BRANCH_USER_CODE,BRANCH_PHONE,BRANCH_GSTNO,BRANCH_ADRRES,BRANCH_STATECODE,BRANCH_REGNO,BRANCH_EMAIL,BRANCH_PHONE2 from BRANCH_MST order by BRANCH_ID desc";
            }
            else
            {
                sqlstr="select BRANCH_ID,BRANCH_NAME,BRANCH_USER_CODE,BRANCH_PHONE,BRANCH_GSTNO,BRANCH_ADRRES,BRANCH_STATECODE,BRANCH_REGNO,BRANCH_EMAIL,BRANCH_PHONE2 from BRANCH_MST  WHERE Branch_ID = "+Session["Branch"]+" order by BRANCH_ID desc";
            }
            DataSet Ds = OBJ_METHOD.Get_DataSet(sqlstr, false, false);

            grdvwbranch.DataSource = Ds;
            grdvwbranch.PageIndex = e.NewPageIndex;
            grdvwbranch.DataKeyNames = new string[] { "id" };
            grdvwbranch.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void grdvwbranch_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        try
        {
            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Edit' and selected='False'", false, false);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                string message = "alert('* You Cant Edit.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
                var slno = grdvwbranch.DataKeys[e.NewSelectedIndex].Values["BRANCH_ID"].ToString();

                DataSet Ds = OBJ_METHOD.Get_DataSet("select * from BRANCH_MST where BRANCH_ID=" + slno, false, false);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = Ds.Tables[0].Rows[0]["BRANCH_ID"].ToString();

                    txtname.Text = Ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
                    txtphone.Text = Ds.Tables[0].Rows[0]["BRANCH_PHONE"].ToString();
                    txtgst.Text = Ds.Tables[0].Rows[0]["BRANCH_GSTNO"].ToString();
                    txtstatecode.Text = Ds.Tables[0].Rows[0]["BRANCH_STATECODE"].ToString();
                    txtaddress.Text = Ds.Tables[0].Rows[0]["BRANCH_ADRRES"].ToString();
                    txtregdnumber.Text = Ds.Tables[0].Rows[0]["BRANCH_REGNO"].ToString();
                    txtmailid.Text = Ds.Tables[0].Rows[0]["BRANCH_EMAIL"].ToString();
                    txtmobilenumber.Text = Ds.Tables[0].Rows[0]["BRANCH_PHONE2"].ToString();
                    txtbranchusercode.Text = Ds.Tables[0].Rows[0]["BRANCH_USER_CODE"].ToString();

                    string chkusr = "select * from dbo.LOGIN_TABLE where USERNAME like '" + Ds.Tables[0].Rows[0]["BRANCH_USER_CODE"].ToString() + "-%'";
                    DataSet Ds1 = OBJ_METHOD.Get_DataSet(chkusr, false, false);
                    if (Ds1.Tables[0].Rows.Count > 0)
                    {
                        txtbranchusercode.Enabled = false;
                    }
                    else
                    {
                        txtbranchusercode.Enabled = true;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            
        }
    }


    public void clearcontrol()
    {
        txtname.Text = "";
        txtphone.Text = "";
        txtgst.Text = "";
        txtstatecode.Text = "";
        txtaddress.Text = "";
        txtregdnumber.Text = "";
        txtmailid.Text = "";
        txtmobilenumber.Text = "";
        if (Convert.ToInt32(Session["USLNO"]) == 1)
        {
            btncreate.Visible = true;
            btnupdate.Visible = false;
        }
        else
        {
            btncreate.Visible = false;
            btnupdate.Visible = true;
        }
        
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
    protected void grdvwbranch_Sorting(object sender, GridViewSortEventArgs e)
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
        grdvwbranch.DataSource = sortedView;
        grdvwbranch.DataBind();
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        clearcontrol();
        binddata();
    }
}