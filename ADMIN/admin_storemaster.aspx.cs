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

public partial class ADMIN_admin_storemaster : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;

    DataMathods OBJ_METHOD = new DataMathods();
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
    decimal total_amt = 0;
    decimal total_vat_amt = 0;
    int i, no, no1, sl;
    GridViewRow gr;

    //public void auto()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //    con.Open();
    //    string qry1 = "select ID from PHARMACY_STORE_MASTER";

    //    com = new SqlCommand(qry1, con);
    //    dr = null;

    //    dr = com.ExecuteReader();

    //    while (dr.Read())
    //    {
    //        num1 = dr["ID"].ToString();
    //    }
    //    num1 = string.Format("ST{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
    //    txtid.Text = num1;

    //    dr.Close();
    //    con.Close();
    //}


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
            binddata();
        }
        
    }
    public void binddata()
    {
        try
        {
           SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());

            DataSet DS = OBJ_METHOD.Get_DataSet("phrmc_storeShow", false, true, SQL_PARAMS1);
            if (DS.Tables[0].Rows.Count > 0)
            {
                gridstore.DataSource = DS;
                gridstore.DataKeyNames = new string[] { "sl" };
                gridstore.DataBind();
                //txtid.Text = DS.Tables[0].Rows[0]["ID"].ToString();
                //txtname.Text = DS.Tables[0].Rows[0]["NAME"].ToString();
                //txtphone.Text = DS.Tables[0].Rows[0]["PHONE"].ToString();
                //txtstatecode.Text = DS.Tables[0].Rows[0]["STATECODE"].ToString();
                //txtgst.Text = DS.Tables[0].Rows[0]["GSTNO"].ToString();
                //txtaddress.Text = DS.Tables[0].Rows[0]["ADDRESS"].ToString();
            }


            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS1.Tables[0].Rows[0]["FYEAR"].ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void clearfield()
    {
        txtaddress.Text = txtgst.Text = txtid.Text = txtname.Text = txtphone.Text = txtstatecode.Text = "";
        btncreate.Visible = true;
        btnupdate.Visible = false;
    
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (txtstatecode.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtstatecode.Focus();
                return;
            }
            else if (txtgst.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtgst.Focus();
                return;
            }
            else if (txtaddress.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtaddress.Focus();
                return;
            }

            else if (txtphone.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtphone.Focus();
                return;
            }

         
            SqlParameter[] SQL_PARAMS = new SqlParameter[9];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@PHONE", SqlDbType.VarChar, 500, txtphone.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@GSTNO", SqlDbType.VarChar, 500, txtgst.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@STATECODE", SqlDbType.VarChar, 500, txtstatecode.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@ADDRESS", SqlDbType.VarChar, 500, txtaddress.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@BRANCH_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("phrmc_storeInsUp", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                string SLNO = OBJ_METHOD._objOut.ToString().Split('@')[0];
                string msg = OBJ_METHOD._objOut.ToString().Split('@')[1];
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                message1 = "alert('" + msg + "')";
                clearfield();
            }
            else
            {
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
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
    
    protected void Button2_Click(object sender, EventArgs e)
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
            else if (txtstatecode.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtgst.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtaddress.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtphone.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }


            //SqlCommand cmd1 = new SqlCommand("update PHARMACY_STORE_MASTER set NAME=@NAME,PHONE=@PHONE,GSTNO=@GSTNO,STATECODE=@STATECODE,ADDRESS=@ADDRESS where ID=@ID AND ORGID=@ORGID", con);

        //    using (SqlCommand cmd1 = new SqlCommand("phrmc_storeInsUp", con))
        //    {
        //        cmd1.CommandType = CommandType.StoredProcedure;
        //        cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
        //        cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
        //        cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        //        cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
        //        cmd1.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = txtphone.Text;
        //        cmd1.Parameters.Add("@GSTNO", SqlDbType.VarChar).Value = txtgst.Text;
        //        cmd1.Parameters.Add("@STATECODE", SqlDbType.VarChar).Value = txtstatecode.Text;
        //        cmd1.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = txtaddress.Text;
        //        cmd1.ExecuteNonQuery();
       
            SqlParameter[] SQL_PARAMS = new SqlParameter[7];

           
            SQL_PARAMS[0] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@PHONE", SqlDbType.VarChar, 500, txtphone.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@GSTNO", SqlDbType.VarChar, 500, txtgst.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@STATECODE", SqlDbType.VarChar, 500, txtstatecode.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@ADDRESS", SqlDbType.VarChar, 500, txtaddress.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);

            OBJ_METHOD.ExecuteProceedure("phrmc_storeInsUp", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

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
            // clearcontrol();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        
    }
    protected void gridstore_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void gridstore_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
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
            var slno = gridstore.DataKeys[e.NewSelectedIndex].Values["sl"].ToString();
            DataSet DS = OBJ_METHOD.Get_DataSet("select * from [dbo].[PHARMACY_STORE_MASTER] where sl= " + slno + "", false, false);
            if (DS.Tables[0].Rows.Count > 0)
            {
                txtid.Text = DS.Tables[0].Rows[0]["ID"].ToString();
                txtname.Text = DS.Tables[0].Rows[0]["NAME"].ToString();
                txtphone.Text = DS.Tables[0].Rows[0]["PHONE"].ToString();
                txtstatecode.Text = DS.Tables[0].Rows[0]["STATECODE"].ToString();
                txtgst.Text = DS.Tables[0].Rows[0]["GSTNO"].ToString();
                txtaddress.Text = DS.Tables[0].Rows[0]["ADDRESS"].ToString();
                btncreate.Visible = false;
                btnupdate.Visible = true;
            }
        }
    }
    protected void gridstore_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

        SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
        SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());

        DataSet DS = OBJ_METHOD.Get_DataSet("phrmc_storeShow", false, true, SQL_PARAMS1);
        if (DS.Tables[0].Rows.Count > 0)
        {
            gridstore.DataSource = DS;
            gridstore.DataKeyNames = new string[] { "sl" };
            gridstore.PageIndex = e.NewPageIndex;
            gridstore.DataBind();
            
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/admin_storemaster.aspx");
    }
}