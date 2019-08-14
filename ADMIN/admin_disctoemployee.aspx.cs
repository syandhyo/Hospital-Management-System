using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
public partial class ADMIN_admin_disctoemployee : System.Web.UI.Page
{
    
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

        lblid.Text = Session["NAME"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        if (!IsPostBack)
        {
            binddata();
        }
    }
    public void binddata()
    {
        try
        {
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select ID,CARDNAME,ROOMRENT,PHARMACY,LAB,RADIOLOGY,OT from STAFF_DISC_MASTER where Branch_ID='" + Session["Branch"] + "' order by ID desc", false, false);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                grvdoctor.DataSource = Ds1;
                grvdoctor.DataKeyNames = new string[] { "ID" };
                grvdoctor.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        DataMathods OBJ_METHOD = new DataMathods();
        try
        {
            
            if (txtcardname.Text == "")
            {
                string message = "alert('* Card Type are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtdiscpharmacy.Text == "" || txtdiscpharmacy.Text == "0")
            {
                txtdisclab.Text = "0.00";
            }
            else if (txtdisclab.Text == "" || txtdisclab.Text == "0")
            {
                txtdisclab.Text = "0.00";
            }
            else if (txtroomcharge.Text == "" || txtroomcharge.Text == "0")
            {
                txtdisclab.Text = "0.00";
            }
            else if (txtdiscradilogy.Text == "" || txtdiscradilogy.Text == "0")
            {
                txtdisclab.Text = "0.00";
            }
            else if (txtdiscot.Text == "" || txtdiscot.Text == "0")
            {
                txtdisclab.Text = "0.00";
            }
            
            SqlParameter[] SQL_PARAMS = new SqlParameter[9];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@CARDNAME", SqlDbType.VarChar, 500, txtcardname.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@RADIOLOGY", SqlDbType.VarChar, 500, txtdiscradilogy.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ROOMRENT", SqlDbType.VarChar, 500, txtroomcharge.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@PHARMACY", SqlDbType.VarChar, 500, txtdiscpharmacy.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@LAB", SqlDbType.VarChar, 500, txtdisclab.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@OT", SqlDbType.VarChar, 500, txtdiscot.Text);

            OBJ_METHOD.ExecuteProceedure("ADMIN_DISC_EMPLOYEE", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);


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
    public void clearcontrol()
    {
        txtcardname.Text = "";
        txtroomcharge.Text = "0.00";
        txtdiscpharmacy.Text = "0.00";
        txtdisclab.Text = "0.00";
        txtdiscradilogy.Text = "0.00";
        txtdiscot.Text = "0.00";
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        DataMathods OBJ_METHOD = new DataMathods();
        try
        {
            // Validation
            if (txtcardname.Text == "")
            {
                string message = "alert('* Card Type are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtdiscpharmacy.Text == "" || txtdiscpharmacy.Text == "0")
            {
                txtdisclab.Text = "0.00";
            }
            else if (txtdisclab.Text == "" || txtdisclab.Text == "0")
            {
                txtdisclab.Text = "0.00";
            }
            else if (txtroomcharge.Text == "" || txtroomcharge.Text == "0")
            {
                txtdisclab.Text = "0.00";
            }
            else if (txtdiscradilogy.Text == "" || txtdiscradilogy.Text == "0")
            {
                txtdisclab.Text = "0.00";
            }
            else if (txtdiscot.Text == "" || txtdiscot.Text == "0")
            {
                txtdisclab.Text = "0.00";
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[8];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@CARDNAME", SqlDbType.VarChar, 500, txtcardname.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@RADIOLOGY", SqlDbType.VarChar, 500, txtdiscradilogy.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@ROOMRENT", SqlDbType.VarChar, 500, txtroomcharge.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@PHARMACY", SqlDbType.VarChar, 500, txtdiscpharmacy.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@LAB", SqlDbType.VarChar, 500, txtdisclab.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@OT", SqlDbType.VarChar, 500, txtdiscot.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);

            OBJ_METHOD.ExecuteProceedure("ADMIN_DISC_EMPLOYEE", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);


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
    protected void grvdoctor_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
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
                var slno = grvdoctor.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
                DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from STAFF_DISC_MASTER where ID='" + slno + "' ", false, false);
                if (Ds1.Tables[0].Rows.Count > 0)
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = slno.ToString();
                    txtcardname.Text = Ds1.Tables[0].Rows[0]["CARDNAME"].ToString();
                    txtroomcharge.Text = Ds1.Tables[0].Rows[0]["ROOMRENT"].ToString();
                    txtdiscpharmacy.Text = Ds1.Tables[0].Rows[0]["PHARMACY"].ToString();
                    txtdisclab.Text = Ds1.Tables[0].Rows[0]["LAB"].ToString();
                    txtdiscradilogy.Text = Ds1.Tables[0].Rows[0]["RADIOLOGY"].ToString();
                    txtdiscot.Text = Ds1.Tables[0].Rows[0]["OT"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grvdoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton db = (LinkButton)e.Row.Cells[7].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    protected void grvdoctor_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                 string slno = grvdoctor.DataKeys[e.RowIndex].Values["ID"].ToString();
                 SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                 SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                 SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

                 OBJ_METHOD.ExecuteProceedure("ADMIN_DISC_EMPLOYEE", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
    protected void grvdoctor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select ID,CARDNAME,ROOMRENT,PHARMACY,LAB,RADIOLOGY,OT from STAFF_DISC_MASTER where Branch_ID='" + Session["Branch"] + "' order by ID desc", false, false);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                grvdoctor.DataSource = Ds1;
                grvdoctor.PageIndex = e.NewPageIndex;
                grvdoctor.DataKeyNames = new string[] { "ID" };
                grvdoctor.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
            
    }
    protected void grvdoctor_Sorting(object sender, GridViewSortEventArgs e)
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
        grvdoctor.DataSource = sortedView;
        grvdoctor.DataBind();
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

        DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,CARDNAME,ROOMRENT,PHARMACY,LAB,RADIOLOGY,OT from STAFF_DISC_MASTER where Branch_ID='" + Session["Branch"] + "' order by ID desc", false, false);

        dt = Ds.Tables[0];
        return dt;

    }
}