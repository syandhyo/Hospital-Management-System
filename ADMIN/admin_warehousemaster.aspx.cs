using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class ADMIN_admin_warehousemaster : System.Web.UI.Page
{
    string num1 = "SJ000";
    DataMathods OBJ_METHOD = new DataMathods();
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
    decimal total_amt = 0;
    decimal total_vat_amt = 0;
    int i, no, no1, sl;
    GridViewRow gr;

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
            Session["SortedView"] = null;
            binddata();
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


            DataSet Ds = OBJ_METHOD.Get_DataSet("select WARE_ID,WARE_NAME,WARE_STATUS from Warehouse_MST WHERE WARE_STATUS = 'ACTIVE' order by WARE_ID desc", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                grvwarehouse.DataSource = Ds;
                grvwarehouse.DataBind();
            }


        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            // Validation
            if (txtname.Text == "")
            {
                string message = "alert('* Name Is Mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            string sts = "";
            if (CheckBox1.Checked)
            {
                sts = "ACTIVE";
            }
            else
            {
                sts = "INACTIVE";
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@WARE_NAME", SqlDbType.VarChar, 0, txtname.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@WARE_STATUS", SqlDbType.VarChar, 50, sts);
           
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("ADMIN_WAREHOUSE_Master", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            resetCt();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }      
    }
    public void resetCt()
    {
        txtname.Text = "";
        CheckBox1.Checked = false;
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            // Validation
            if (txtname.Text == "")
            {
                string message = "alert('* Fields Are Mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            string sts = "";
            if (CheckBox1.Checked)
            {
                sts = "ACTIVE";
            }
            else
            {
                sts = "INACTIVE";
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@WARE_NAME", SqlDbType.VarChar, 0, txtname.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@WARE_STATUS", SqlDbType.VarChar, 50, sts);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@WARE_ID", SqlDbType.Int, 0, txtid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

            OBJ_METHOD.ExecuteProceedure("ADMIN_WAREHOUSE_Master", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            resetCt();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }

    protected void grvwarehouse_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
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
            var slno = grvwarehouse.DataKeys[e.NewSelectedIndex].Values["WARE_ID"].ToString();
            DataSet Dt = OBJ_METHOD.Get_DataSet("select * from Warehouse_MST where WARE_ID='" + slno + "'", false, false);
            if (Dt.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = slno.ToString();
                txtname.Text = Dt.Tables[0].Rows[0]["WARE_NAME"].ToString();
                string chkactv = Dt.Tables[0].Rows[0]["WARE_STATUS"].ToString();
                if (chkactv == "ACTIVE")
                {
                    CheckBox1.Checked = true;
                }
                else
                {
                    CheckBox1.Checked = false;
                }
            }
        }
    }
     protected void grvwarehouse_Sorting(object sender, GridViewSortEventArgs e)
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
          grvwarehouse.DataSource = sortedView;
          grvwarehouse.DataBind();
      }
     public DataTable getdata()
     {
         DataTable dt = new DataTable();

         DataSet Ds = OBJ_METHOD.Get_DataSet("select WARE_ID,WARE_NAME,WARE_STATUS from Warehouse_MST order by WARE_ID desc", false, false);

         dt = Ds.Tables[0];
         return dt;

     }
     protected void grvwarehouse_RowDataBound(object sender, GridViewRowEventArgs e)
     {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
             // reference the Delete LinkButton
             LinkButton db = (LinkButton)e.Row.Cells[3].Controls[0];

             db.OnClientClick = "return confirm('Are you sure want to hide this item ?');";
         }
     }

     protected void grvwarehouse_PageIndexChanging(object sender, GridViewPageEventArgs e)
     {
         try
         {

             DataSet dt = OBJ_METHOD.Get_DataSet("select WARE_ID,WARE_NAME,WARE_STATUS from Warehouse_MST WHERE WARE_STATUS = 'ACTIVE' order by WARE_ID desc", false, false);
             grvwarehouse.DataSource = dt;
             grvwarehouse.PageIndex = e.NewPageIndex;
             grvwarehouse.DataKeyNames = new string[] { "WARE_ID" };
             grvwarehouse.DataBind();
             if (Session["SortedView"] != null)
             {

                 grvwarehouse.DataSource = Session["SortedView"];
                 grvwarehouse.DataBind();
             }
             else
             {
                 grvwarehouse.DataSource = dt;
                 grvwarehouse.DataBind();
             }

         }
         catch (Exception ex)
         {
             Console.WriteLine("An error occurred: '{0}'", ex);
         }
     }

     protected void btncancel_Click(object sender, EventArgs e)
     {
         resetCt();
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
     protected void grvwarehouse_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                 string slno = grvwarehouse.DataKeys[e.RowIndex].Values["WARE_ID"].ToString();
                 SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                 SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                 SQL_PARAMS[1] = OBJ_METHOD.createParams("@WARE_ID", SqlDbType.VarChar, 500, slno);


                 OBJ_METHOD.ExecuteProceedure("ADMIN_WAREHOUSE_Master", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                 if (OBJ_METHOD._RESULT > 0)
                 {

                     OBJ_METHOD.commitOrRollbackTran("commit");
                     resetCt();
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
}


 