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

public partial class RECEPTION_reception_visitor_management : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataReader dr;
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
        lblid.Text = Session["NAME"].ToString();
        lbluid.Text = Session["UID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");

        if (!IsPostBack)
        {
            binddata();

            div2.Visible = true;
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

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BED");

            DataSet DS2 = OBJ_METHOD.Get_DataSet("RECP_VISITOR", false, true, SQL_PARAMS2);
            if (DS2.Tables[0].Rows.Count > 0)
            {
                DropDownList1.DataSource = DS2;
                DropDownList1.DataTextField = "BEDNO";
                DropDownList1.DataValueField = "BEDNO";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("Please Select", "0"));
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
           
            SqlParameter[] SQL_PARAMS2 = new SqlParameter[2];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_UHID");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@UHID", SqlDbType.VarChar, 500, txtip.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_VISITOR", false, true, SQL_PARAMS2);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                lblpname.Text = Ds.Tables[0].Rows[0]["NAME"].ToString();
                lblbedno.Text = Ds.Tables[0].Rows[0]["BEDNO"].ToString();
                lbluhid.Text = Ds.Tables[0].Rows[0]["UHID"].ToString();
            }
            else
            {
                string message = "alert('Incorrect UHID')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }


            SqlParameter[] SQL_PARAMS3 = new SqlParameter[2];

            SQL_PARAMS3[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_VISITOR_IPBED");
            SQL_PARAMS3[1] = OBJ_METHOD.createParams("@UHID", SqlDbType.VarChar, 500, txtip.Text);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RECP_VISITOR", false, true, SQL_PARAMS3);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                lblvisitorno.Text = Ds1.Tables[0].Rows[0]["NOOFVISIT"].ToString();
                if (Convert.ToInt32(lblvisitorno.Text) > 2)
                {
                    string message = "alert('Maximum Visitor Number Reached.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }

            }
            
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnshowbed_Click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS2 = new SqlParameter[2];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BEDNO");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, DropDownList1.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_VISITOR", false, true, SQL_PARAMS2);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                lblpname.Text = Ds.Tables[0].Rows[0]["PNAME"].ToString();
                lblbedno.Text = Ds.Tables[0].Rows[0]["BEDNO"].ToString();
                lbluhid.Text = Ds.Tables[0].Rows[0]["UHID"].ToString();
            }


            SqlParameter[] SQL_PARAMS3 = new SqlParameter[3];

            SQL_PARAMS3[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_VISITOR_IPBED");
            SQL_PARAMS3[1] = OBJ_METHOD.createParams("@UHID", SqlDbType.VarChar, 500, txtip.Text);
            SQL_PARAMS3[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, DateTime.Now.ToString("dd-MM-yyyy"));

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RECP_VISITOR", false, true, SQL_PARAMS2);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                lblvisitorno.Text = Ds1.Tables[0].Rows[0]["NOOFVISIT"].ToString();
                if (Convert.ToInt32(lblvisitorno.Text) > 2)
                {
                    string message = "alert('Maximum Visitor Number Reached.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
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
            if (txtvisitorname.Text == "")
            {
                string message = "alert('* Enter Visitor Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtrelation.Text == "")
            {
                string message = "alert('* Enter Relation.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (lblbedno.Text == "0")
            {
                string message = "alert('* Select Patient First.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (Convert.ToInt32(lblvisitorno.Text) > 2)
            {
                string message = "alert('Maximum Visitor Number Reached.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }


            SqlParameter[] SQL_PARAMS2 = new SqlParameter[8];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@VNO", SqlDbType.VarChar, 100, lblvisitorno.Text);
            SQL_PARAMS2[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtdate.Text).ToString("dd-MM-yyyy"));
            SQL_PARAMS2[3] = OBJ_METHOD.createParams("@VNAME", SqlDbType.VarChar, 200, lblpname.Text);
            SQL_PARAMS2[4] = OBJ_METHOD.createParams("@UHID", SqlDbType.VarChar, 200, lbluhid.Text);
            SQL_PARAMS2[5] = OBJ_METHOD.createParams("@PNAME", SqlDbType.VarChar, 200, txtvisitorname.Text);
            SQL_PARAMS2[6] = OBJ_METHOD.createParams("@RELATION", SqlDbType.VarChar, 200, txtrelation.Text);
            SQL_PARAMS2[7] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 200, lblbedno.Text);

            OBJ_METHOD.ExecuteProceedure("RECP_VISITOR_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS2, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }

            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message1 = "alert('Due to some issues, Data not saved.')";
            }
          
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            //reset();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        Response.Redirect("~/RECEPTION/visitor_reciept.aspx");
    }
}