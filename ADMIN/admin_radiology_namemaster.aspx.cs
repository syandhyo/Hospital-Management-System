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

public partial class ADMIN_admin_radiology_namemaster : System.Web.UI.Page
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
        // lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy");
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
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_CATA_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RADIO_TESTTYPE", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
    }
     protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
     {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
             // reference the Delete LinkButton
             LinkButton db = (LinkButton)e.Row.Cells[1].Controls[0];

             db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
         }
     }
     protected void btncreate_Click(object sender, EventArgs e)
     {
         string message1 = string.Empty;
         try
         {
             if (txtname.Text == "")
             {
                 string message = "alert('* Please Enter Name.')";
                 ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                 return;
             }
             SqlParameter[] SQL_PARAMS = new SqlParameter[5];

             SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
             SQL_PARAMS[1] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text);
             SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
             SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
             SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

             OBJ_METHOD.ExecuteProceedure("RADIO_TESTTYPE_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

             if (OBJ_METHOD._RESULT > 0)
             {
                 OBJ_METHOD.commitOrRollbackTran("commit");
                 binddata();
                 clearfield();
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
     public void clearfield()
     {
         txtname.Text = "";
         btncreate.Visible = true;
         btnupdate.Visible = false;
     }

     protected void btnupdate_Click(object sender, EventArgs e)
     {
         string message1 = string.Empty;
         try
         {
             if (txtname.Text == "")
             {
                 string message = "alert('* Fields are mandatory.')";
                 ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                 return;
             }
             SqlParameter[] SQL_PARAMS = new SqlParameter[4];

             SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
             SQL_PARAMS[1] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text);
             SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
             SQL_PARAMS[3] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);

             OBJ_METHOD.ExecuteProceedure("RADIO_TESTTYPE_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

             if (OBJ_METHOD._RESULT > 0)
             {
                 OBJ_METHOD.commitOrRollbackTran("commit");
                 binddata();
                 clearfield();
             }

             message1 = "alert('" + OBJ_METHOD._objOut + "')";
         }
         catch (Exception ex)
         {
             OBJ_METHOD.commitOrRollbackTran("rollback");
             message1 = "alert('Due to some issues, Data not updated.')";
         }
         finally
         {
             ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
         }
     }

     protected void btncancel_Click(object sender, EventArgs e)
     {
         binddata();
         clearfield();
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
                 var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
                 SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                 SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_EVENT");
                 SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

                 DataSet Ds = OBJ_METHOD.Get_DataSet("RADIO_TESTTYPE", false, true, SQL_PARAMS);
                 if (Ds.Tables[0].Rows.Count > 0)
                 {
                     btncreate.Visible = false;
                     btnupdate.Visible = true;
                     txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                     txtname.Text = Ds.Tables[0].Rows[0]["NAME"].ToString();
                 }
             }
         }
         catch (Exception ex)
         {
             Console.WriteLine("An error occurred: '{0}'", ex);

         }
     }
     protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
         string message1 = string.Empty;
         try
         {

             DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False'", false, false);
             if (Ds1.Tables[0].Rows.Count > 0)
             {
                 string message = "alert('* You Cant Delete.')";
                 ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                 return;
             }
             else
             {
                 SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                 SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                 SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);


                 OBJ_METHOD.ExecuteProceedure("RADIO_TESTTYPE_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS);

                 if (OBJ_METHOD._RESULT > 0)
                 {
                     OBJ_METHOD.commitOrRollbackTran("commit");
                     binddata();
                     clearfield();
                 }

                 message1 = "alert('" + OBJ_METHOD._objOut + "')";
             }
         }
         catch (Exception ex)
         {
             OBJ_METHOD.commitOrRollbackTran("rollback");
             message1 = "alert('Due to some issues, Data not updated.')";
         }
         finally
         {
             ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
         }
     }

     protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
     {
          try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_CATA_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RADIO_TESTTYPE", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
        }
          catch (Exception ex)
          {
              Console.WriteLine("An error occurred: '{0}'", ex);
          }
     }
}