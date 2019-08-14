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

public partial class ADMIN_admin_ambulanceentry : System.Web.UI.Page
{
    //SqlDataReader dr;
    DataMathods OBJ_METHOD = new DataMathods();
    string num1 = "S";
    string num2 = "SJ00000000000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2;
    DataSet ds = new DataSet();
    SqlCommand com;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
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
            //lbluid.Text = Session["UID"].ToString();
            lblorgid.Text = Session["ORGID"].ToString();

            if (!IsPostBack)
            {
                Session["SortedView"] = null;
                binddata();
                auto();
            }
            //binddata();

        }

        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    public void auto()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //string qry1 = "select Max(APD_APNT_NO) as ID from APPOINTMENT_DTL";
            //string qry1 = "select max(convert(Int,substring(ID,3,len(id)-2))) AS ID  from APPOINTMENT_DTL ";
            string qry1 = "select MAX([AmbulanceNo]) As ID from tblAmbulanceEntry";

            com = new SqlCommand(qry1, con);
            dr = null;
            dr = com.ExecuteReader();
            string str1 = "1";
            string result = "";
            if (dr.Read() && dr["ID"].ToString() != "")
            {
                // num1 = dr["ID"].ToString();
                //------------------
                num1 = dr["ID"].ToString();
                // string str="0";
                ////string str = num1.Substring(0, num1.Length - 0);//delete last 10 record
                ////string d = str.Substring(3);//delete first 3 record
                string d = num1;
                str1 = (Convert.ToInt64(d) + 1).ToString();//add in numbers
                result = str1.ToString().PadLeft(4, '0');
            }
            else
            {
                str1 = "1";
                result = str1.ToString().PadLeft(4, '0');
            }
            
            txtambulanceno.Text = result;

            dr.Close();
            con.Close();
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
            LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this info ?');";
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

            string str = "SELECT id,AmbulanceNo,VehicleName,VehicleRegNo from tblAmbulanceEntry WHERE Branch_ID = " + Session["Branch"] + " order by ID desc";
            DataSet Dt = OBJ_METHOD.Get_DataSet(str, false, false);
                 if (Dt.Tables[0].Rows.Count > 0)
                  {
                     GridView1.DataSource = Dt;
                     GridView1.DataBind();
                  }
           
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
            
    }

    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message = string.Empty;
        try
        {
            
            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False'", false, false);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                string message1 = "alert('* You Cant Delete.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                return;
            }
            else
            {
                OBJ_METHOD = new DataMathods();
                string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();

                SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@id", SqlDbType.Int, 0, slno);
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

                OBJ_METHOD.ExecuteProceedure("ADMIN_Ambulance_entry", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    binddata();
                    auto();
                }
                message = "alert('" + OBJ_METHOD._objOut + "')";

            }
        }
        catch (Exception ex)
        {
            message = ex.ToString();
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
            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Edit' and selected='False'", false, false);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                string message = "alert('* You Cant Edit.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
                var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
                DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT id,AmbulanceNo,VehicleName,VehicleRegNo,Category,Rate,Range from tblAmbulanceEntry where id='" + slno + "' ", false, false);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    btnSubmit.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                    txtambulanceno.Text = Ds.Tables[0].Rows[0]["AmbulanceNo"].ToString();
                    txtvehiclename.Text = Ds.Tables[0].Rows[0]["VehicleName"].ToString();
                    txtvehicleregno.Text = Ds.Tables[0].Rows[0]["VehicleRegNo"].ToString();
                    dropcategory.SelectedValue = Ds.Tables[0].Rows[0]["Category"].ToString();
                    txtrate.Text = Ds.Tables[0].Rows[0]["Rate"].ToString();
                    txtrange.Text = Ds.Tables[0].Rows[0]["Range"].ToString();
                    auto();
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
          DataSet ds = OBJ_METHOD.Get_DataSet("SELECT id,AmbulanceNo,VehicleName,VehicleRegNo from tblAmbulanceEntry order by id desc", false, false);
          
                GridView1.DataSource = ds;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "id" };
                GridView1.DataBind();
                if (Session["SortedView"] != null)
                {
                    GridView1.DataSource = Session["SortedView"];
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    auto();
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
        string message1 = "";
        try
        {
            if (txtambulanceno.Text == "")
            {
                string message = "alert('* Ambulance No. is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtambulanceno.Focus();
                return;
            }
            else if (txtvehiclename.Text == "")
            {
                string message = "alert('* Vehicle Name is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtvehiclename.Focus();
                return;
            }
            else if (txtvehicleregno.Text == "")
            {
                string message = "alert('* Vehicle Registration No is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtvehicleregno.Focus();
                return;
            }
            else if (txtrate.Text == "")
            {
                string message = "alert('* Rate is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtvehicleregno.Focus();
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[10];
                        
            SQL_PARAMS[0] = OBJ_METHOD.createParams("@AmbulanceNo", SqlDbType.VarChar, 500, txtambulanceno.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@VehicleName", SqlDbType.VarChar, 500, txtvehiclename.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@BRANCH_FYR", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@VehicleRegNo", SqlDbType.VarChar, 500, txtvehicleregno.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Category", SqlDbType.VarChar, 500, dropcategory.SelectedValue);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@Rate", SqlDbType.Decimal, 0, txtrate.Text);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@Range", SqlDbType.VarChar, 500, txtrange.Text);

            OBJ_METHOD.ExecuteProceedure("ADMIN_Ambulance_entry", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
                auto();
            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
            }
            
            binddata();
           
            message1 = "alert('" + OBJ_METHOD._objOut + "')";

        }
        catch (Exception ex)
        {

             message1 = ex.ToString();
            
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        // Validation

    }
    public void clearcontrol()
    {
        txtambulanceno.Text = "";
        txtvehiclename.Text = "";
        txtvehicleregno.Text = "";
        btnSubmit.Visible = true;
        btnupdate.Visible = false;
        txtrate.Text = "";
        txtrange.Text = "";
        dropcategory.SelectedIndex = 0;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = "";
        try
        {
            if (txtambulanceno.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtambulanceno.Focus();
                return;
            }
            else if (txtvehiclename.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtvehiclename.Focus();
                return;
            }
            else if (txtvehicleregno.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtvehiclename.Focus();
                return;
            }

            SqlParameter[] SQL_PARAMS = new SqlParameter[9];
            SQL_PARAMS[0] = OBJ_METHOD.createParams("@AmbulanceNo", SqlDbType.VarChar, 500, txtambulanceno.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@VehicleName", SqlDbType.VarChar, 500, txtvehiclename.Text);
           
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@VehicleRegNo", SqlDbType.VarChar, 500, txtvehicleregno.Text);
            
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@id", SqlDbType.Int, 0, txtid.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Category", SqlDbType.VarChar, 500, dropcategory.SelectedValue);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Rate", SqlDbType.Decimal, 0, txtrate.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@Range", SqlDbType.VarChar, 500, txtrange.Text);

            OBJ_METHOD.ExecuteProceedure("ADMIN_Ambulance_entry", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
                btnSubmit.Visible = true;
                btnupdate.Visible = false;
                auto();
            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
            }

            binddata();
            
            message1 = "alert('" + OBJ_METHOD._objOut + "')";

        }
        catch (Exception ex)
        {
             message1 = ex.ToString();
            
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }

        // Validation

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        clearcontrol();
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

        DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT id,AmbulanceNo,VehicleName,VehicleRegNo from tblAmbulanceEntry WHERE Branch_ID = " + Session["Branch"] + " order by ID desc", false, false);

        dt = Ds.Tables[0];
        return dt;

    }
}