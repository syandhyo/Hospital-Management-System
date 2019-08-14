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

public partial class RECEPTION_reception_staff_search : System.Web.UI.Page
{
    SqlDataReader dr;
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
                binddata();
            }

           
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


            SqlParameter[] SQL_PARAMS2 = new SqlParameter[1];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DESIGNATION");

            DataSet DS2 = OBJ_METHOD.Get_DataSet("RECP_STAFFSEARCH", false, true, SQL_PARAMS2);
            if (DS2.Tables[0].Rows.Count > 0)
            {
                dropdesg.DataSource = DS2;
                dropdesg.DataTextField = "DesgName";
                dropdesg.DataValueField = "id";
                dropdesg.DataBind();
                dropdesg.Items.Insert(0, new ListItem("Please Select", "0"));
               
            }

            SqlParameter[] SQL_PARAMS3 = new SqlParameter[1];

            SQL_PARAMS3[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DEPARTMENT");

            DataSet DS3 = OBJ_METHOD.Get_DataSet("RECP_STAFFSEARCH", false, true, SQL_PARAMS3);
            if (DS3.Tables[0].Rows.Count > 0)
            {
                dropdept.DataSource = DS3;
                dropdept.DataTextField = "DeptName";
                dropdept.DataValueField = "id";
                dropdept.DataBind();
                dropdept.Items.Insert(0, new ListItem("Please Select", "0"));
                
            }
           
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
       
       
    }

    protected void btnName_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('Please!! Enter The Name And Search..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
         
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SHOW_NAME");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Sname", SqlDbType.VarChar, 500, txtname.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_STAFFSEARCH", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.DataBind();
                dropdept.SelectedIndex = 0;
                dropdesg.SelectedIndex = 0;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
          
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnDesg_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropdesg.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Designation And Search..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
           
           
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DESGID");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@DesgId", SqlDbType.VarChar, 500, dropdesg.SelectedValue);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_STAFFSEARCH", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.DataBind();
                txtname.Text = "";
                dropdept.SelectedIndex = 0;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnDept_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropdept.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Department And Search..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            
           
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DEPTID");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Deptid", SqlDbType.VarChar, 500, dropdept.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_STAFFSEARCH", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.DataBind();
                dropdesg.SelectedIndex = 0;
                txtname.Text = "";
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
                
            }
            
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
   
}