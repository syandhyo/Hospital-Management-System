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

public partial class ADMIN_admin_visitingcharges : System.Web.UI.Page
{
    SqlDataReader dr;
    DataMathods OBJ_METHOD = new DataMathods();
    string message = string.Empty;

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
            lbluid.Text = Session["UID"].ToString();
            lblorgid.Text = Session["ORGID"].ToString();

            if (!IsPostBack)
            {
                binddata();
                Session["SortedView"] = null;

                DataSet Dt1 = OBJ_METHOD.Get_DataSet("select * from tblStaff where DesgId=(select id from tblDesignation where DesgName='Doctor')", false, false);
                dropdoctor.DataSource = Dt1;
                dropdoctor.DataTextField = "Sname";
                dropdoctor.DataValueField = "id";
                dropdoctor.DataBind();
                dropdoctor.Items.Insert(0, new ListItem("Please Select", "0"));
               
                DataSet Dt2 = OBJ_METHOD.Get_DataSet("select * from INSURANCE_TBL", false, false);
                dropcompany.DataSource = Dt2;
                dropcompany.DataTextField = "COMNYNAME";
                dropcompany.DataValueField = "ID";
                dropcompany.DataBind();
                dropcompany.Items.Insert(0, new ListItem("Please Select","0"));
             
            }
          txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
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


            DataSet Ds = OBJ_METHOD.Get_DataSet("SP_DESIGNATION_MASTER_View", false, true,null);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds;
                GridView1.DataBind();
            }

          
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    public DataTable getdata()
    {
        DataTable dt = new DataTable();

        DataSet Ds = OBJ_METHOD.Get_DataSet("SP_DESIGNATION_MASTER_View", false, true);

        dt = Ds.Tables[0];
        return dt;

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
         try
        {
            // Validation
            if (dropcompany.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Company.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (dropdoctor.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Doctor Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtop.Text == "" || Convert.ToDecimal(txtop.Text) <= 0)
            {
                string message = "alert('*Enter OP Charges.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtip.Text == "" || Convert.ToDecimal(txtip.Text) <= 0)
            {
                string message = "alert('*Enter IP Charges.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[8];
            string message1 = string.Empty;

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@INSURANCE", SqlDbType.VarChar, 500, dropcompany.SelectedValue);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, dropdoctor.SelectedValue);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@OP_CHARGES", SqlDbType.Decimal, 0, txtop.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@IP_CHARGES", SqlDbType.Decimal, 0, txtip.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("USP_CON_CHARGES", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
            }

            message1 = "alert('" + OBJ_METHOD._objOut + "')";
           
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
        dropcompany.SelectedIndex = 0;
        dropdoctor.SelectedIndex = 0;
        txtop.Text = "";
        txtip.Text = "";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            // Validation
            if (dropcompany.SelectedValue == "0")
            {
                string message = "alert('* Please Select Company.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (dropdoctor.SelectedValue == "0")
            {
                string message = "alert('* Please Select Doctor Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtop.Text == "" || Convert.ToDecimal(txtop.Text) <= 0)
            {
                string message = "alert('*Enter OP Charges.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtip.Text == "" || Convert.ToDecimal(txtip.Text) <= 0)
            {
                string message = "alert('*Enter IP Charges.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            SqlParameter[] SQL_PARAMS = new SqlParameter[7];
            
            SQL_PARAMS[0] = OBJ_METHOD.createParams("@INSURANCE", SqlDbType.VarChar, 500, dropcompany.SelectedValue);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, dropdoctor.SelectedValue);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@OP_CHARGES", SqlDbType.Decimal, 0, txtop.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@IP_CHARGES", SqlDbType.Decimal, 0, txtip.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 0, txtid.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
           
            OBJ_METHOD.ExecuteProceedure("USP_CON_CHARGES", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                btnupdate.Visible = false;
                btnSubmit.Visible = true;
            }

            message1 = "alert('" + OBJ_METHOD._objOut + "')";
           
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not Updated.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            clearcontrol();
            
        }
      
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        clearcontrol();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[6].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message1 = string.Empty;

        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False'", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                string message = "alert('* You Cant Delete.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
                OBJ_METHOD = new DataMathods();
                string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();
                SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "Delete");

                OBJ_METHOD.ExecuteProceedure("USP_CON_CHARGES", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    binddata();


                }
                message = "alert('" + OBJ_METHOD._objOut + "')";
            }
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message = "alert('Due to some issues, Data not Deleted.')";
        }
            finally
            {
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
           
           
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
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
                 var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();

                 DataSet Dt = OBJ_METHOD.Get_DataSet("SELECT * from CONSULTATION_CHARGES_TABLE where id='" + slno + "'", false, false);
                 if (Dt.Tables[0].Rows.Count > 0)
                 //SqlCommand com = new SqlCommand("SELECT * from CONSULTATION_CHARGES_TABLE where id='" + slno + "'", con);
                 {
                     btnSubmit.Visible = false;
                     btnupdate.Visible = true;
                     txtid.Text = Dt.Tables[0].Rows[0]["ID"].ToString();
                     dropcompany.SelectedValue = Dt.Tables[0].Rows[0]["INSURANCE"].ToString();
                     txtdate.Text = Convert.ToDateTime(Dt.Tables[0].Rows[0]["DATE"]).ToString("dd-MM-yyyy");
                     dropdoctor.SelectedValue = Dt.Tables[0].Rows[0]["NAME"].ToString();
                     txtop.Text = Dt.Tables[0].Rows[0]["OP_CHARGES"].ToString();
                     txtip.Text = Dt.Tables[0].Rows[0]["IP_CHARGES"].ToString();
                 }
             }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet dt = OBJ_METHOD.Get_DataSet("SP_DESIGNATION_MASTER_View", false, true,null);
            //SqlDataAdapter da = new SqlDataAdapter("SELECT * from CONSULTATION_CHARGES_TABLE", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            //con.Close();
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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
}