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

public partial class ADMIN_admin_suppliermaster : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
    decimal total_amt = 0;
    decimal total_vat_amt = 0;
    int i, no, no1, sl;
    GridViewRow gr;
    DataMathods OBJ_METHOD = new DataMathods();

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton db = (LinkButton)e.Row.Cells[10].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
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

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");

            DataSet DS1 = OBJ_METHOD.Get_DataSet("phrmc_supplGrShow", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    public void clear_control()
    {
        txtid.Text = "";
        txtname.Text = "";
        txtaddress.Text = "";
        txtcity.Text = "";
        txtcontactno.Text = "";
        txtgstno.Text = "";
        txtopening.Text = "0";
        txtpin.Text = "";
        txtstate.Text = "";
        txtlicno.Text = "";
        Dropcategory.SelectedIndex = 0;
        btncreate.Visible = true;
        btnupdate.Visible = false;
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message = string.Empty;
        try
        {
            
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False'", false, false);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                string message1 = "alert('* You Cant Delete.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                return;
            }
            else
            {
                OBJ_METHOD = new DataMathods();
                string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();

                SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 200, slno);
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

                OBJ_METHOD.ExecuteProceedure("phrmc_supplMstInsUp", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
  
    {
        try
        {
           
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,NAME,CITY,STATE,CONTACT,GST,OPENING,PIN,ADDRESS FROM SUPPLIER_TABLE WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);

            //using (SqlCommand cmd = new SqlCommand("phrmc_supplMstSeldel", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INDEXCHG";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    GridView1.SelectedIndex = 0;
            //    GridView1.DataSource = dt;
            //    GridView1.PageIndex = e.NewPageIndex;
            //    GridView1.DataKeyNames = new string[] { "ID" };
            //    GridView1.DataBind();
            //}
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, "");
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INDEXCHG");

            DataSet DS1 = OBJ_METHOD.Get_DataSet("phrmc_supplMstSeldel", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
   
           
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('Please!!Enter The Name Of The Supplier..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (txtcity.Text == "")
            {
                string message = "alert('* Please!!Enter City..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcity.Focus();
                return;
            }
            else if (txtstate.Text == "")
            {
                string message = "alert('*Please!! Enter State ..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtpin.Text == "")
            {
                string message = "alert('* Please!! Enter PIN Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpin.Focus();
                return;
            }
            else if (txtcontactno.Text == "")
            {
                string message = "alert('* Please!! Enter Contact Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcontactno.Focus();
                return;
            }

            else if (txtgstno.Text == "")
            {
                string message = "alert('* Please!!Enter GST Number.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtgstno.Focus();
                return;
            }
            else if (txtaddress.Text == "")
            {
                string message = "alert('*Please!!Enter Address..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtstatecode.Text == "")
            {
                string message = "alert('*Please!!Enter State Code.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtaddress.Focus();
                return;
            }
            else if (txtlicno.Text == "")
            {
                string message = "alert('*Please!!Enter License number.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtstatecode.Focus();
                return;
            }
            else if (Dropcategory.SelectedValue == "")
            {
                string message = "alert('*Please!!Select Category.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtstatecode.Focus();
                return;
            }
           

            //------------new code=======================

            SqlParameter[] SQL_PARAMS = new SqlParameter[14];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@CITY", SqlDbType.VarChar, 500, txtcity.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@STATE", SqlDbType.VarChar, 500, txtstate.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@CONTACT", SqlDbType.VarChar, 500, txtcontactno.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 200, txtid.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@GST", SqlDbType.VarChar, 50, txtgstno.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@PIN", SqlDbType.VarChar, 200, txtpin.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@ADDRESS", SqlDbType.VarChar, 500, txtaddress.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@STATECODE", SqlDbType.VarChar, 50, txtstatecode.Text);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@SUPCATEGORY", SqlDbType.VarChar, 50, Dropcategory.SelectedValue);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@LICENCE", SqlDbType.VarChar, 50, txtlicno.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            OBJ_METHOD.ExecuteProceedure("phrmc_supplMstInsUp", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                clear();
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
    public void clear()
    {
        txtaddress.Text = txtcity.Text = txtcontactno.Text = txtgstno.Text = txtid.Text = txtname.Text = txtopening.Text = txtpin.Text = txtstate.Text = txtstatecode.Text = txtlicno.Text = "";
        Dropcategory.SelectedIndex = 0;
        btncreate.Visible = true;
        btnupdate.Visible = false;
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

                  DataSet Dt = OBJ_METHOD.Get_DataSet("select * from SUPPLIER_TABLE where ID='" + slno + "'", false, false);
                  if (Dt.Tables[0].Rows.Count > 0)
                  {
                      btncreate.Visible = false;
                      btnupdate.Visible = true;
                      txtid.Text = Dt.Tables[0].Rows[0]["ID"].ToString();
                      txtname.Text = Dt.Tables[0].Rows[0]["NAME"].ToString();
                      txtcity.Text = Dt.Tables[0].Rows[0]["CITY"].ToString();
                      txtstate.Text = Dt.Tables[0].Rows[0]["STATE"].ToString();
                      txtcontactno.Text = Dt.Tables[0].Rows[0]["CONTACT"].ToString();
                      txtgstno.Text = Dt.Tables[0].Rows[0]["GST"].ToString();
                      // txtopening.Text = dr["OPENING"].ToString();
                      txtpin.Text = Dt.Tables[0].Rows[0]["PIN"].ToString();
                      txtaddress.Text = Dt.Tables[0].Rows[0]["ADDRESS"].ToString();
                      txtstatecode.Text = Dt.Tables[0].Rows[0]["STATECODE"].ToString();
                      txtlicno.Text = Dt.Tables[0].Rows[0]["LICENCE"].ToString();
                      Dropcategory.SelectedValue = Dt.Tables[0].Rows[0]["SUPCATEGORY"].ToString();
                  }
              }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('Please!!Enter The Name Of The Supplier..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (txtcity.Text == "")
            {
                string message = "alert('* Please!!Enter City..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcity.Focus();
                return;
            }
            else if (txtstate.Text == "")
            {
                string message = "alert('*Please!! Enter State ..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtpin.Text == "")
            {
                string message = "alert('* Please!! Enter PIN Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpin.Focus();
                return;
            }
            else if (txtcontactno.Text == "")
            {
                string message = "alert('* Please!! Enter Contact Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcontactno.Focus();
                return;
            }

            else if (txtgstno.Text == "")
            {
                string message = "alert('*Please!! Enter GST Number.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtaddress.Text == "")
            {
                string message = "alert('*Please!!Enter Address..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtaddress.Focus();
                return;
            }
            else if (txtstatecode.Text == "")
            {
                string message = "alert('*Please!!Enter State Code.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtstatecode.Focus();
                return;
            }
            else if (txtlicno.Text == "")
            {
                string message = "alert('*Please!!Enter License number.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtstatecode.Focus();
                return;
            }
            else if (Dropcategory.SelectedValue == "")
            {
                string message = "alert('*Please!!Select Category.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtstatecode.Focus();
                return;
            }
          

            //using (SqlCommand cmd1 = new SqlCommand("phrmc_supplMstInsUp", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
            //    cmd1.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtcity.Text;
            //    cmd1.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
            //    cmd1.Parameters.Add("@CONTACT", SqlDbType.VarChar).Value = txtcontactno.Text;
            //    cmd1.Parameters.Add("@GST", SqlDbType.VarChar).Value = txtgstno.Text;
            //    cmd1.Parameters.Add("@OPENING", SqlDbType.Decimal).Value = "0";
            //    cmd1.Parameters.Add("@PIN", SqlDbType.VarChar).Value = txtpin.Text;
            //    cmd1.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = txtaddress.Text;
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtstatecode.Text;
            //    cmd1.ExecuteNonQuery();
            //}
            //binddata();
            //con.Close();
            //string message1 = "alert('Successfully Updated..')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            //clear();
            //return;
            SqlParameter[] SQL_PARAMS = new SqlParameter[14];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@CITY", SqlDbType.VarChar, 500, txtcity.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@STATE", SqlDbType.VarChar, 500, txtstate.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@CONTACT", SqlDbType.VarChar, 500, txtcontactno.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 200, txtid.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@GST", SqlDbType.VarChar, 50, txtgstno.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@PIN", SqlDbType.VarChar, 200, txtpin.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@ADDRESS", SqlDbType.VarChar, 500, txtaddress.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@STATECODE", SqlDbType.VarChar, 50, txtstatecode.Text);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@SUPCATEGORY", SqlDbType.VarChar, 50, Dropcategory.SelectedValue);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@LICENCE", SqlDbType.VarChar, 50, txtlicno.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");

            OBJ_METHOD.ExecuteProceedure("phrmc_supplMstInsUp", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

          
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
            clear();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        clear();
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

        DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT ID as ID,NAME,CITY,STATE,CONTACT,GST,OPENING,PIN,ADDRESS,SUPCATEGORY,LICENCE FROM SUPPLIER_TABLE  where Branch_ID='" + Session["Branch"].ToString() + "' order by ID desc", false, false);

        dt = Ds.Tables[0];
        return dt;

    }
}