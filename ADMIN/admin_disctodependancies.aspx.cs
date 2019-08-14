using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class ADMIN_admin_disctodependancies : System.Web.UI.Page
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
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            DataSet DS = OBJ_METHOD.Get_DataSet("select * From Dependancy_Table Where Branch_ID= " + Session["Branch"] + "", false, false);
            if (DS.Tables[0].Rows.Count > 0)
            {
                txtdiscpharmacy.Text = DS.Tables[0].Rows[0]["PHARMACY"].ToString();
                txtdisclab.Text = DS.Tables[0].Rows[0]["LAB"].ToString();
                txtroomcharge.Text = DS.Tables[0].Rows[0]["ROOMRENT"].ToString();
                discothers.Text = DS.Tables[0].Rows[0]["OTHERS"].ToString();
                txtid.Text = DS.Tables[0].Rows[0]["ID"].ToString();
            }
            
            if (txtroomcharge.Text == "0.00" && txtdisclab.Text == "0.00" && txtdiscpharmacy.Text == "0.00" && discothers.Text == "0.00")
            {
                btncreate.Text = "Create";
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        DataMathods OBJ_METHOD = new DataMathods();
        try
        {
            // Validation
            if (txtdiscpharmacy.Text == "" || Convert.ToDecimal(txtdiscpharmacy.Text) <= 0)
            {
                string message = "alert('*Fields are mandatory ')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtdisclab.Text == "" || Convert.ToDecimal(txtdiscpharmacy.Text) <= 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtroomcharge.Text == "" || Convert.ToDecimal(txtdiscpharmacy.Text) <= 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (discothers.Text == "" || Convert.ToDecimal(txtdiscpharmacy.Text) <= 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
             SqlParameter[] SQL_PARAMS = new SqlParameter[8];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ROOMRENT", SqlDbType.VarChar, 500, txtroomcharge.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@PHARMACY", SqlDbType.VarChar, 500, txtdiscpharmacy.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@OTHERS", SqlDbType.VarChar, 500, discothers.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@LAB", SqlDbType.VarChar, 500, txtdisclab.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, txtid.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, txtid.Text);

            OBJ_METHOD.ExecuteProceedure("ADMIN_DISC_TO_DEPENDENCY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);


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
            message1 = "alert('Due to some issues, Data not Updated.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }

         
    }
    public void clearcontrol()
    {
        btncreate.Text = "Create";
    }
}