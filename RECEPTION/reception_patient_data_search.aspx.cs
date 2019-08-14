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

public partial class RECEPTION_reception_patient_data_search : System.Web.UI.Page
{
    DataMathods OBJ_METHOD = new DataMathods();
    SqlDataReader dr;

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
            }
           
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
       
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            
            if (txtname.Text == "")
            {
                string message1 = "alert('Please!! Enter The Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                return;
            }
            else
            {
                
                SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "insert_advnce");
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@name", SqlDbType.VarChar, 500, txtname.Text);

                DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_SEARCH_PATIENT", false, true, SQL_PARAMS1);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    grSearchPatient.DataSource = DS1;
                    grSearchPatient.DataBind();
                }
                else
                {
                    string message = "alert(' No Record Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }

            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
            Session["IPDNO"] = gr.Cells[1].Text;
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        //var UB = gr.Cells[1].Text;
        //string name = Session["IPDNO"].Text.Trim();
        //Session["name"] = name;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[1];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "advnce");
          

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_SEARCH_PATIENT", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                txtname.Text = "";
                grSearchPatient.DataSource = DS1;
                grSearchPatient.DataBind();
            }
            else
            {
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                
            }

        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}