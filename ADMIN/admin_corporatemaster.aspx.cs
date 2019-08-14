using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class ADMIN_admin_corporatemaster : System.Web.UI.Page
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


            DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,CNAME,ISACTIVE from Corporate_Table order by ID desc", false, false);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                grvcorprte.DataSource = Ds;
                grvcorprte.DataBind();
            }

          
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
       
        
        //----------------------
        //DataTable dt = new DataTable();
        //dt.Columns.AddRange(new DataColumn[3] { new DataColumn("ID"), new DataColumn("INV"), new DataColumn("PRICE") });
        //ViewState["ITEM"] = dt;
        //this.BindGrid();
 
    protected void Button1_Click(object sender, EventArgs e)
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
            SqlParameter[] SQL_PARAMS = new SqlParameter[5];
           
            SQL_PARAMS[0] = OBJ_METHOD.createParams("@CNAME", SqlDbType.VarChar, 0, txtname.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ISACTIVE", SqlDbType.VarChar, 0, CheckBox1.Checked);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("ADMIN_CORPORATE_Master", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
    protected void Button2_Click(object sender, EventArgs e)
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
            
            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@CNAME", SqlDbType.VarChar, 0, txtname.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ISACTIVE", SqlDbType.VarChar, 0, CheckBox1.Checked);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, txtid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

            OBJ_METHOD.ExecuteProceedure("ADMIN_CORPORATE_Master", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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

    protected void grvcorprte_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
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
                  var slno = grvcorprte.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
                  DataSet Dt = OBJ_METHOD.Get_DataSet("select * from Corporate_Table where ID='" + slno + "'", false, false);
                  if (Dt.Tables[0].Rows.Count > 0)
                  {
                      btncreate.Visible = false;
                      btnupdate.Visible = true;
                      txtid.Text = slno.ToString();
                      txtname.Text = Dt.Tables[0].Rows[0]["CNAME"].ToString();
                      string chkactv = Dt.Tables[0].Rows[0]["ISACTIVE"].ToString();
                      if (chkactv == "True")
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
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void grvcorprte_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[3].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    protected void grvcorprte_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
          //string message = string.Empty;

          //try
          //{
          //    DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False'", false, false);
          //    if (Ds1.Tables[0].Rows.Count > 0)
          //    {
          //        string message1 = "alert('* You Cant Delete.')";
          //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
          //        return;
          //    }
          //    else
          //    {
          //        OBJ_METHOD = new DataMathods();
          //        string slno = grvcorprte.DataKeys[e.RowIndex].Values["ID"].ToString();

          //        SqlParameter[] SQL_PARAMS = new SqlParameter[2];

          //        SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);
          //        SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

          //        OBJ_METHOD.ExecuteProceedure("ADMIN_CORPORATE_Master", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

          //        if (OBJ_METHOD._RESULT > 0)
          //        {
          //            OBJ_METHOD.commitOrRollbackTran("commit");
          //            binddata();
          //        }
          //        message = "alert('" + OBJ_METHOD._objOut + "')";
          //    }
          //}
          //catch (Exception ex)
          //{
          //    OBJ_METHOD.commitOrRollbackTran("rollback");
          //    message = "alert('Due to some issues, Data not Deleted.')";
          //}
          //  finally
          //  {
          //      ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
          //  } 
    }
    protected void grvcorprte_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
           
            DataSet dt = OBJ_METHOD.Get_DataSet("select ID,CNAME,ISACTIVE from Corporate_Table order by ID desc", false, false);
            grvcorprte.DataSource = dt;
            grvcorprte.PageIndex = e.NewPageIndex;
            grvcorprte.DataKeyNames = new string[] { "ID" };
            grvcorprte.DataBind();
            if (Session["SortedView"] != null)
            {

                grvcorprte.DataSource = Session["SortedView"];
                grvcorprte.DataBind();
            }
            else
            {
                grvcorprte.DataSource = dt;
                grvcorprte.DataBind();
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
    protected void grvcorprte_Sorting(object sender, GridViewSortEventArgs e)
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
        grvcorprte.DataSource = sortedView;
        grvcorprte.DataBind();
    }
    public DataTable getdata()
    {
        DataTable dt = new DataTable();

        DataSet Ds = OBJ_METHOD.Get_DataSet("select ID,CNAME,ISACTIVE from Corporate_Table order by ID desc", false, false);

        dt = Ds.Tables[0];
        return dt;

    }
}