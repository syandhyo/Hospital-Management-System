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


public partial class RECEPTION_reception_insurance_data : System.Web.UI.Page
{
    SqlDataAdapter da, da1;
    SqlDataReader dr;
    DataMathods OBJ_METHOD = new DataMathods();

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

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[1];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BEDTABLE");

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_INSURANCE_PATIENTDATA", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                dropinsurance.DataSource = DS1;
                dropinsurance.DataTextField = "CNAME";
                dropinsurance.DataValueField = "ID";
                dropinsurance.DataBind();
                dropinsurance.Items.Insert(0, new ListItem("Please Select","0"));
            }

            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

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
        lbluid.Text = Session["UID"].ToString();
        if (!IsPostBack)
        {
            binddata();
            
        }
       
    }
    protected void dropinsurance_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@INSURANCE", SqlDbType.VarChar, 500, dropinsurance.SelectedValue);
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DROP");

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_INSURANCE_PATIENTDATA", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.DataBind();
            }
            else
            {
                string message = "alert('No Record Found..')";
                GridView1.DataSource = null;
                GridView1.DataBind();
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[1];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_SHOW");

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_INSURANCE_PATIENTDATA", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.DataBind();
            }
            else
            {
                string message = "alert('No Record Found..')";
               
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            //using (SqlCommand cmd = new SqlCommand("RECP_INSURANCE_PATIENTDATA", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_SHOW";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@INSURANCE", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter Adp = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter Adp = new SqlDataAdapter("SELECT BED_TABLE.WARD,BED_TABLE.BEDNO,BED_TABLE.PNAME,BED_TABLE.VN,ADMISSION_TABLE.INSURANCENAME FROM ADMISSION_TABLE INNER JOIN BED_TABLE ON ADMISSION_TABLE.VN = BED_TABLE.VN", con);
            //    DataTable Dt = new DataTable();
            //    Adp.Fill(Dt);
            //    GridView1.DataSource = Dt;
            //    GridView1.DataBind();
            //}
           
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}