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


public partial class PHARMACYSTORE_pharmacy_patient_search : System.Web.UI.Page
{
    SqlDataReader dr;
    DataMathods OBJ_METHOD = new DataMathods();
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
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
        con.Close();
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

            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT DISTINCT BEDNO FROM BED_TABLE WHERE Branch_ID = " + Session["Branch"] + "", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                DropDownList1.DataSource = Ds;
                DropDownList1.DataTextField = "BEDNO";
                DropDownList1.DataValueField = "BEDNO";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "Please Select");
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            //validation
            if (txtspid.Text == "")
            {
                string message = "alert('* Please Select Patient OPD Number.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_OP");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtspid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS = OBJ_METHOD.Get_DataSet("SP_OT_OP_PatientSrch", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = DS;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            else
            {
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
           
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnshowph_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtsmobile.Text == "")
            {
                string message = "alert('* Please Select Patient Mobile Number.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "Select_Phn");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@TELPHNO", SqlDbType.VarChar, 500, txtsmobile.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS = OBJ_METHOD.Get_DataSet("SP_OT_OP_PatientSrch", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = DS;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            else
            {
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnshowip_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtip.Text == "")
            {
                string message = "alert('* Please Select Patient IP No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_IP");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, txtip.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS = OBJ_METHOD.Get_DataSet("SP_OT_IP_PatientSrch", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView2.SelectedIndex = 0;
                GridView2.DataSource = DS;
                GridView2.DataBind();
            }
            else
            {
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnshowbed_Click(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList1.SelectedIndex == 0)
            {
                string message = "alert('* Please Select BED NO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BED");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, DropDownList1.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS = OBJ_METHOD.Get_DataSet("SP_OT_IP_PatientSrch", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView2.SelectedIndex = 0;
                GridView2.DataSource = DS;
                GridView2.DataBind();
            }
            else
            {
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}